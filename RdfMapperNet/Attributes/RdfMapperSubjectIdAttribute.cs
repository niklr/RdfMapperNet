using System;

namespace RdfMapperNet.Attributes
{
    /// <summary>
    /// The subject id attribute can be used to annotate the identifier of the model.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RdfMapperSubjectIdAttribute : Attribute
    {

    }
}
