using RdfMapperNet.Methods;
using System;

namespace RdfMapperNet.Attributes
{
    /// <summary>
    /// The subject attribute can be used to handle the mapping between models and RDF subjects.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RdfMapperSubjectAttribute : Attribute
    {
        private readonly string _name;
        private Type _typeIRdfMapperSubjectMethods;

        public RdfMapperSubjectAttribute(string name)
        {
            _name = name;
            _typeIRdfMapperSubjectMethods = typeof(DefaultRdfMapperSubjectMethods);
        }

        /// <summary>
        /// The name used to map the model to the RDF subject.
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// The RDF type used to represent the model.
        /// </summary>
        public string RdfType { get; set; }

        /// <summary>
        /// The methods used to map the model to the RDF subject.
        /// </summary>
        public Type IRdfMapperSubjectMethods
        {
            get
            {
                return _typeIRdfMapperSubjectMethods;
            }
            set
            {
                _typeIRdfMapperSubjectMethods = value;
            }
        }
    }
}
