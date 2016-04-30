using RdfMapperNet.Attributes;

namespace RdfMapperNet.Tests.Data.Model.Error
{
    [RdfMapperSubject("s_file_uri")]
    public class ErrorModel6
    {
        [RdfMapperSubjectId]
        [RdfMapperPredicate("p_file_id", IRdfMapperObjectMethods = typeof(ErrorModel1))]
        public int Id { get; set; }
    }
}
