using RdfMapperNet.Attributes;
using RdfMapperNet.Exceptions;
using RdfMapperNet.Helpers.Reader;
using RdfMapperNet.Helpers.Reflection;
using RdfMapperNet.Methods;
using RdfMapperNet.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RdfMapperNet
{
    /// <summary>
    /// Maps classes to RDF triples.
    /// </summary>
    public class RdfMapper
    {
        private readonly string _mappingNamespace;
        private readonly IDictionary<string, string> _dict;

        /// <summary>
        /// Initialize the RdfMapper.
        /// </summary>
        /// <param name="reader">The RDF reader.</param>
        /// <param name="mappingDocumentPath">The path to the mapping document.</param>
        /// <param name="mappingNamespace">The name of the mapping namespace.</param>
        public RdfMapper(IRdfMapperReader reader, string mappingDocumentPath, string mappingNamespace)
        {
            _mappingNamespace = mappingNamespace;
            _dict = reader.Read(mappingDocumentPath);
        }

        /// <summary>
        /// Initialize the RdfMapper.
        /// </summary>
        /// <param name="dict">The mapping document as dictionary.</param>
        /// <param name="mappingNamespace">The name of the mapping namespace.</param>
        public RdfMapper(IDictionary<string, string> dict, string mappingNamespace)
        {
            _mappingNamespace = mappingNamespace;
            _dict = dict;
        }

        /// <summary>
        /// Maps the provided class to RDF triples.
        /// </summary>
        /// <typeparam name="T">The type of the class to be mapped.</typeparam>
        /// <param name="entity">The class to be mapped.</param>
        /// <returns>A container of RDF triples.</returns>
        public TripleContainer MapTriples<T>(T entity) where T : class
        {
            RdfMapperSubjectAttribute subjectAttribute;
            ValidateSubject(entity, out subjectAttribute);

            TripleContainer container = new TripleContainer();
            container.Subject = MapSubjectUri(entity);

            if (!string.IsNullOrWhiteSpace(subjectAttribute.RdfType))
            {
                string mappedRdfType = MapName(subjectAttribute.RdfType);
                if (!string.IsNullOrWhiteSpace(mappedRdfType))
                    container.Triples.Add(new Triple(container.Subject, RdfMapperConstants.RDF_TYPE, mappedRdfType));
            }

            AddTriplesRecursive(entity, container);

            return container;
        }

        #region Helper Methods

        private void Validate<T>(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Type type = entity.GetType();
            if (!type.IsClass)
                throw new ArgumentException(string.Format("{0} must be a class", entity.ToString()));
        }

        private void ValidateSubject<T>(T entity, out RdfMapperSubjectAttribute attribute)
        {
            Validate(entity);

            StaticReflection.TryGetAttribute(entity, out attribute);

            if (attribute == null)
                throw new ArgumentException(string.Format("{0} is missing", typeof(RdfMapperSubjectAttribute).Name));
            else if (!attribute.IRdfMapperSubjectMethods.IsClass)
                throw new ArgumentException(string.Format("{0} must be a class",
                    StaticReflection.GetFullMemberName((RdfMapperSubjectAttribute a) => a.IRdfMapperSubjectMethods)));
            else if (!typeof(IRdfMapperSubjectMethods).IsAssignableFrom(attribute.IRdfMapperSubjectMethods))
                throw new ArgumentException(string.Format("{0} must implement the interface {1}",
                    StaticReflection.GetFullMemberName((RdfMapperSubjectAttribute a) => a.IRdfMapperSubjectMethods), typeof(IRdfMapperSubjectMethods).Name));
        }

        private void ValidatePredicate(PropertyInfo property, out RdfMapperPredicateAttribute attribute)
        {
            property.TryGetAttribute(out attribute);

            if (attribute == null)
                throw new ArgumentException(string.Format("{0} is missing", typeof(RdfMapperPredicateAttribute).Name));
            else if (!attribute.IRdfMapperObjectMethods.IsClass)
                throw new ArgumentException(string.Format("{0} must be a class",
                    StaticReflection.GetFullMemberName((RdfMapperPredicateAttribute a) => a.IRdfMapperObjectMethods)));
            else if (!typeof(IRdfMapperObjectMethods).IsAssignableFrom(attribute.IRdfMapperObjectMethods))
                throw new ArgumentException(string.Format("{0} must implement the interface {1}",
                    StaticReflection.GetFullMemberName((RdfMapperPredicateAttribute a) => a.IRdfMapperObjectMethods), typeof(IRdfMapperObjectMethods).Name));
        }

        private void AddTriplesRecursive<T>(T entity, TripleContainer container)
        {
            Validate(entity);

            PropertyInfo[] properties = entity.GetType().GetProperties();

            // Predicates
            foreach (PropertyInfo property in properties.Where(p => p.IsDefined(typeof(RdfMapperPredicateAttribute))))
            {
                AddTriples(entity, container, property);
            }

            // Extended subjects
            foreach (PropertyInfo property in properties.Where(p => p.IsDefined(typeof(RdfMapperExtendedSubjectAttribute))))
            {
                var value = property.GetValue(entity, null);
                if (value != null)
                {
                    if (property.IsCollection())
                    {
                        foreach (var item in (IEnumerable)value)
                            AddTriplesRecursive(item, container);
                    }
                    else
                    {
                        AddTriplesRecursive(value, container);
                    }
                }
            }
        }

        private void AddTriples<T>(T entity, TripleContainer container, PropertyInfo property)
        {
            RdfMapperPredicateAttribute attribute;
            ValidatePredicate(property, out attribute);

            string mappedPredicate = MapName(attribute.Name);
            if (string.IsNullOrWhiteSpace(mappedPredicate))
                throw new RdfMapperException(string.Format("{0} '{1}' could not be mapped", typeof(RdfMapperPredicateAttribute).Name, attribute.Name));

            var value = property.GetValue(entity, null);
            if (value != null)
            {
                if (property.IsCollection())
                {
                    foreach (var item in (IEnumerable)value)
                    {
                        if (entity.ContainsAttribute(typeof(RdfMapperSubjectAttribute)))
                            container.Triples.Add(new Triple(container.Subject, mappedPredicate, MapSubjectUri(item)));
                    }
                }
                else if (property.IsString() || property.IsValueType())
                {
                    container.Triples.Add(new Triple(container.Subject, mappedPredicate, MapObjectToString(value, attribute)));
                }
            }
        }

        internal string MapName(string name)
        {
            string mappedName = string.Empty;
            _dict.TryGetValue(_mappingNamespace + name, out mappedName);
            return mappedName;
        }

        internal string MapSubjectUri<T>(T entity)
        {
            RdfMapperSubjectAttribute subjectAttribute;
            ValidateSubject(entity, out subjectAttribute);

            string subjectUriTemplate = MapName(subjectAttribute.Name);

            string[] ids = GetIds(entity).ToArray();

            var subjectRdfMapperMethods = Activator.CreateInstance(subjectAttribute.IRdfMapperSubjectMethods);

            return subjectAttribute.IRdfMapperSubjectMethods
                .GetMethod(StaticReflection.GetMemberName((IRdfMapperSubjectMethods m) => m.MapSubjectUri(string.Empty, default(string[]))))
                .Invoke(subjectRdfMapperMethods, new object[] { subjectUriTemplate, ids }).ToString();
        }

        internal string MapObjectToString(object value, RdfMapperPredicateAttribute attribute)
        {
            var objectRdfMapperMethods = Activator.CreateInstance(attribute.IRdfMapperObjectMethods);

            return attribute.IRdfMapperObjectMethods
                .GetMethod(StaticReflection.GetMemberName((IRdfMapperObjectMethods m) => m.MapObjectToString(value)))
                .Invoke(objectRdfMapperMethods, new object[] { value }).ToString();
        }

        internal IEnumerable<string> GetIds<T>(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            IEnumerable<PropertyInfo> subjectIdProperties = entity.GetType().GetProperties().Where(p => p.IsDefined(typeof(RdfMapperSubjectIdAttribute)));

            if (subjectIdProperties == null || subjectIdProperties.Count() <= 0)
                throw new ArgumentException(string.Format("{0} is missing", typeof(RdfMapperSubjectIdAttribute).Name));

            List<string> ids = new List<string>();

            foreach (var item in subjectIdProperties)
                ids.Add(item.GetValue(entity).ToString());

            return ids;
        }

        #endregion
    }
}
