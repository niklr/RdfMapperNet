using System;

namespace RdfMapperNet.Attributes
{
    /// <summary>
    /// The extended subject attribute can be used to include navigation properties in the same RDF subject.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RdfMapperExtendedSubjectAttribute : Attribute
    {

    }
}
