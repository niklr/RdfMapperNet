namespace RdfMapperNet.Model
{
    /// <summary>
    /// Statements about resources in the form of subject–predicate–object expressions.
    /// </summary>
    public class Triple
    {
        public Triple(string tripleSubject, string triplePredicate, string tripleObject)
        {
            Subject = tripleSubject;
            Predicate = triplePredicate;
            Object = tripleObject; 
        }

        /// <summary>
        /// The subject is an RDF URI reference or a blank node.
        /// </summary>
        public string Subject { get; private set; }

        /// <summary>
        /// The predicate is an RDF URI reference.
        /// </summary>
        public string Predicate { get; private set; }

        /// <summary>
        /// The object is an RDF URI reference, a literal or a blank node.
        /// </summary>
        public string Object { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Triple p = obj as Triple;
            if ((object)p == null)
                return false;

            return Subject.Equals(p.Subject) && Predicate.Equals(p.Predicate) && Object.Equals(p.Object);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
