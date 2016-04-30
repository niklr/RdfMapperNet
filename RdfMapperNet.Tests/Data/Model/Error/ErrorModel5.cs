using RdfMapperNet.Attributes;
using RdfMapperNet.Methods;

namespace RdfMapperNet.Tests.Data.Model.Error
{
    [RdfMapperSubject("s_file_uri")]
    public class ErrorModel5
    {
        [RdfMapperSubjectId]
        [RdfMapperPredicate("p_file_id", IRdfMapperObjectMethods = typeof(IRdfMapperObjectMethods))]
        public int Id { get; set; }
    }
}
