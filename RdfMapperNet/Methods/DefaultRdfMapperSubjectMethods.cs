using System.Linq;
using System.Text.RegularExpressions;

namespace RdfMapperNet.Methods
{
    /// <summary>
    /// The default methods used to map the model to the RDF subject.
    /// </summary>
    public class DefaultRdfMapperSubjectMethods : IRdfMapperSubjectMethods
    {
        /// <summary>
        /// Maps the subject URI by combining the provided template with the subject identifiers.
        /// This implementation supports subjects with one identifier only. The template must
        /// contain the following placeholder: {id}.
        /// </summary>
        /// <param name="subjectUriTemplate">The subject URI template.</param>
        /// <param name="ids">The identifiers of the subject.</param>
        /// <returns>The subject URI.</returns>
        public string MapSubjectUri(string subjectUriTemplate, string[] ids)
        {
            string idPlaceholder = "{id}";

            if (string.IsNullOrWhiteSpace(subjectUriTemplate) || Regex.Matches(subjectUriTemplate, idPlaceholder).Count != 1)
                return string.Empty;
            else if (ids == null || ids.Length != 1)
                return string.Empty;

            return subjectUriTemplate.Replace(idPlaceholder, ids.First());
        }
    }
}
