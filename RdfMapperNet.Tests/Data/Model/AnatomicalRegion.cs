using RdfMapperNet.Attributes;
using RdfMapperNet.Tests.Data.Methods;

namespace RdfMapperNet.Tests.Data.Model
{
    public class AnatomicalRegion
    {
        [RdfMapperPredicate("p_object_anatomical_region", IRdfMapperObjectMethods = typeof(AnatomicalRegionRdfMapperObjectMethods))]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
