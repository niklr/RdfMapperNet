using RdfMapperNet.Methods;

namespace RdfMapperNet.Tests.Data.Methods
{
    public class AnatomicalRegionRdfMapperObjectMethods : IRdfMapperObjectMethods
    {
        public string MapObjectToString(object value)
        {
            return "http://purl.org/obo/owlapi/fma#FMA_" + value.ToString();
        }
    }
}
