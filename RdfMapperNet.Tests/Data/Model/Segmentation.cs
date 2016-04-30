using RdfMapperNet.Attributes;

namespace RdfMapperNet.Tests.Data.Model
{
    public class Segmentation
    {
        public int ObjectVersionId { get; set; }

        public int? Method { get; set; }

        [RdfMapperPredicate("p_object_segmentation_method_description")]
        public string MethodDescription { get; set; }

        public string SerieInstanceUId { get; set; }
    }
}
