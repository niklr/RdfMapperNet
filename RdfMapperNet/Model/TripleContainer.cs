using System.Collections.Generic;

namespace RdfMapperNet.Model
{
    /// <summary>
    /// A container of RDF triples about exactly one subject.
    /// </summary>
    public class TripleContainer
    {
        public TripleContainer()
        {
            Triples = new HashSet<Triple>();
        }

        /// <summary>
        /// The subject all triples in this container have in common.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// A collection of triples about exactly one subject.
        /// </summary>
        public ICollection<Triple> Triples { get; set; }
    }
}
