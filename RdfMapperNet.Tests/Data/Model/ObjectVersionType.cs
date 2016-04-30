using RdfMapperNet.Attributes;

namespace RdfMapperNet.Tests.Data.Model
{
    public class ObjectVersionType
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        [RdfMapperPredicate("p_object_type")]
        public string RdfUri { get; set; }
    }
}
