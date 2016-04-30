using RdfMapperNet.Methods;
using System;

namespace RdfMapperNet.Attributes
{
    /// <summary>
    /// The predicate attribute can be used to handle the mapping between model properties and RDF predicates.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RdfMapperPredicateAttribute : Attribute
    {
        private readonly string _name;
        private Type _typeIRdfMapperObjectMethods; 

        public RdfMapperPredicateAttribute(string name)
        {
            _name = name;
            _typeIRdfMapperObjectMethods = typeof(DefaultRdfMapperObjectMethods);
        }

        /// <summary>
        /// The name used to map the model property to the RDF predicate.
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// The methods used to map the model property value to the RDF object.
        /// </summary>
        public Type IRdfMapperObjectMethods
        {
            get
            {
                return _typeIRdfMapperObjectMethods;
            }
            set
            {
                _typeIRdfMapperObjectMethods = value;
            }
        }
    }
}
