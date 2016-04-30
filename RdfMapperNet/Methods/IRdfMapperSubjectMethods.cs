namespace RdfMapperNet.Methods
{
    /// <summary>
    /// Methods used to map the model to the RDF subject.
    /// </summary>
    public interface IRdfMapperSubjectMethods
    {
        /// <summary>
        /// Maps the subject URI by combining the provided template with the subject identifiers.
        /// </summary>
        /// <param name="subjectUriTemplate">The subject URI template.</param>
        /// <param name="ids">The identifiers of the subject.</param>
        /// <returns>The subject URI.</returns>
        string MapSubjectUri(string subjectUriTemplate, string[] ids);
    }
}
