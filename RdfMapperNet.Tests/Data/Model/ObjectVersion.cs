using RdfMapperNet.Attributes;
using System;
using System.Collections.Generic;

namespace RdfMapperNet.Tests.Data.Model
{
    [RdfMapperSubject("s_object_uri", RdfType = "o_object_rdf_type")]
    public class ObjectVersion
    {
        public ObjectVersion()
        {
            Data = new HashSet<Data>();
            AnatomicalRegions = new HashSet<AnatomicalRegion>();
            Transfers = new HashSet<Transfer>();
        }

        [RdfMapperSubjectId]
        [RdfMapperPredicate("p_object_id")]
        public int Id { get; set; }

        [RdfMapperPredicate("p_object_created_date")]
        public DateTime CreatedDate { get; set; }

        [RdfMapperPredicate("p_object_description")]
        public string Description { get; set; }

        [RdfMapperExtendedSubject]
        public virtual ObjectVersionType ObjectVersionType { get; set; }

        [RdfMapperExtendedSubject]
        public virtual Segmentation Segmentation { get; set; }

        [RdfMapperPredicate("p_object_file")]
        public virtual ICollection<Data> Data { get; set; }

        [RdfMapperExtendedSubject]
        public virtual ICollection<AnatomicalRegion> AnatomicalRegions { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}
