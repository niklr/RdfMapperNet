using RdfMapperNet.Attributes;

namespace RdfMapperNet.Tests.Data.Model.Error
{
    [RdfMapperSubject("s_file_uri")]
    public class ErrorModel4
    {
        [RdfMapperSubjectId]
        public int Id { get; set; }

        [RdfMapperPredicate("p_does_not_exist")]
        public string Message { get; set; }
    }
}
