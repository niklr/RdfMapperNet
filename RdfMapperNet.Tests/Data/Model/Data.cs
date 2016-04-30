using RdfMapperNet.Attributes;
using System;

namespace RdfMapperNet.Tests.Data.Model
{
    [RdfMapperSubject("s_file_uri", RdfType = "o_file_rdf_type")]
    public class Data
    {
        [RdfMapperSubjectId]
        [RdfMapperPredicate("p_file_id")]
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public string OriginalFileName { get; set; }

        public string Extension { get; set; }
    }
}
