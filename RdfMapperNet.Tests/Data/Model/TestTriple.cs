using RdfMapperNet.Model;

namespace RdfMapperNet.Tests.Data.Model
{
    public class TestTriple : Triple
    {
        public TestTriple(Triple triple) :
            this(triple.Subject, triple.Predicate, triple.Object)
        {

        }

        public TestTriple(string tripleSubject, string triplePredicate, string tripleObject) :
            base(tripleSubject, triplePredicate, tripleObject)
        {

        }

        public override string ToString()
        {
            return string.Format("'{0}'\t'{1}'\t'{2}'", Subject, Predicate, Object);
        }
    }
}
