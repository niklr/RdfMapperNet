using System.Collections.Generic;

namespace RdfMapperNet.Model
{
    /// <summary>
    /// A document containing the RDF mapping information.
    /// </summary>
    public class MappingDocument
    {
        public MappingDocument()
        {
            Namespaces = new Dictionary<string, string>();
            Map = new Dictionary<string, string>();
        }

        /// <summary>
        /// A dictionary consisting of namespace prefixes as key entries and namespace names as value entries.
        /// </summary>
        public virtual Dictionary<string, string> Namespaces { get; set; }

        /// <summary>
        /// A dicitionary consisting of subject and predicate attribute names as key entries and
        /// their corresponding RDF representations as value entries. The namespace delimiter is a colon ":".
        /// </summary>
        public virtual Dictionary<string, string> Map { get; set; }
    }
}
