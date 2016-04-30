using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RdfMapperNet.Tests.Data.Model;
using System.Collections.Generic;
using System.Linq;
using RdfMapperNet.Tests.Data.Model.Error;
using RdfMapperNet.Model;
using System.Diagnostics;
using RdfMapperNet.Tests.Data.Attributes;
using RdfMapperNet.Exceptions;
using RdfMapperNet.Helpers.Reader;

namespace RdfMapperNet.Tests.Unit
{
    [TestClass]
    public class RdfMapperTest
    {
        private const string MappingDocumentPath = @"Data\mapping_document.json";
        private const string ns1 = "http://www.smir.ch/api/";
        private const string ns2 = "http://www.virtualskeleton.ch/schema/rdf-mapping#";
        private const string ns3 = "http://www.virtualskeleton.ch/schema/metadata4data#";
        private const string ns4 = "http://www.virtualskeleton.ch/ont/object-type#";

        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "RdfMapperSubjectAttribute is missing")]
        public void RdfMapper_MapTriples_Exception1()
        {
            //assemble
            var mapper = new RdfMapper(new RdfMapperJsonReader(), MappingDocumentPath, ns2);
            var model = new ErrorModel1();

            //act
            mapper.MapTriples(model);
        }

        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "RdfMapperNet.Attributes.RdfMapperSubjectAttribute.IRdfMapperSubjectMethods must be a class")]
        public void RdfMapper_MapTriples_Exception2()
        {
            //assemble
            var mapper = new RdfMapper(new RdfMapperJsonReader(), MappingDocumentPath, ns2);
            var model = new ErrorModel2();

            //act
            mapper.MapTriples(model);
        }

        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "RdfMapperNet.Attributes.RdfMapperSubjectAttribute.IRdfMapperSubjectMethods must implement the interface IRdfMapperSubjectMethods")]
        public void RdfMapper_MapTriples_Exception3()
        {
            //assemble
            var mapper = new RdfMapper(new RdfMapperJsonReader(), MappingDocumentPath, ns2);
            var model = new ErrorModel3();

            //act
            mapper.MapTriples(model);
        }

        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(RdfMapperException), "RdfMapperPredicateAttribute 'p_does_not_exist' could not be mapped")]
        public void RdfMapper_MapTriples_Exception4()
        {
            //assemble
            var mapper = new RdfMapper(new RdfMapperJsonReader(), MappingDocumentPath, ns2);
            var model = new ErrorModel4();

            //act
            mapper.MapTriples(model);
        }

        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "RdfMapperNet.Attributes.RdfMapperPredicateAttribute.IRdfMapperObjectMethods must be a class")]
        public void RdfMapper_MapTriples_Exception5()
        {
            //assemble
            var mapper = new RdfMapper(new RdfMapperJsonReader(), MappingDocumentPath, ns2);
            var model = new ErrorModel5();

            //act
            mapper.MapTriples(model);
        }

        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "RdfMapperNet.Attributes.RdfMapperPredicateAttribute.IRdfMapperObjectMethods must implement the interface IRdfMapperObjectMethods")]
        public void RdfMapper_MapTriples_Exception6()
        {
            //assemble
            var mapper = new RdfMapper(new RdfMapperJsonReader(), MappingDocumentPath, ns2);
            var model = new ErrorModel6();

            //act
            mapper.MapTriples(model);
        }

        [TestMethod]
        public void RdfMapper_MapName()
        {
            //assemble
            var mapper = new RdfMapper(new RdfMapperJsonReader(), MappingDocumentPath, ns2);

            //act & assert
            Assert.AreEqual(ns1 + "objects/{id}", mapper.MapName("s_object_uri"));
            Assert.AreEqual(ns3 + "object_id", mapper.MapName("p_object_id"));
            Assert.AreEqual(ns3 + "object_description", mapper.MapName("p_object_description"));
        }

        [TestMethod]
        public void RdfMapper_MapTriples_ObjectVersion1()
        {
            //assemble
            var mapper = new RdfMapper(new RdfMapperJsonReader(), MappingDocumentPath, ns2);
            var model = new ObjectVersion()
            {
                Id = 11,
                Description = "test description",
                CreatedDate = new DateTime(2016, 4, 27),
                Segmentation = new Segmentation()
                {
                    MethodDescription = "test segmentation method description"
                },
                ObjectVersionType = new ObjectVersionType()
                {
                    RdfUri = ns4 + "raw_image"
                }
            };
            model.Data.Add(new Data.Model.Data()
            {
                Id = 21,
                OriginalFileName = "test1.dcm",
                Extension = ".dcm"
            });
            model.Data.Add(new Data.Model.Data()
            {
                Id = 22,
                OriginalFileName = "test2.dcm",
                Extension = ".dcm"
            });
            model.AnatomicalRegions.Add(new AnatomicalRegion()
            {
                Id = 50801,
                Name = "Brain"
            });
            model.AnatomicalRegions.Add(new AnatomicalRegion()
            {
                Id = 9611,
                Name = "Femur"
            });

            //act
            TripleContainer container = mapper.MapTriples(model);
            IEnumerable<TestTriple> triples = container.Triples.Select(t => new TestTriple(t));
            foreach (TestTriple triple in triples)
                Debug.WriteLine(triple.ToString());

            //assert
            Assert.AreEqual(ns1 + "objects/11", container.Subject);
            Assert.IsFalse(triples.Contains(new TestTriple("", "", "")));
            Assert.IsTrue(triples.Contains(new TestTriple(ns1 + "objects/11", RdfMapperConstants.RDF_TYPE, ns3 + "object")));
            Assert.IsTrue(triples.Contains(new TestTriple(ns1 + "objects/11", ns3 + "object_id", "11")));
            Assert.IsTrue(triples.Contains(new TestTriple(ns1 + "objects/11", ns3 + "object_created_date", "27.04.2016 00:00:00")));
            Assert.IsTrue(triples.Contains(new TestTriple(ns1 + "objects/11", ns3 + "object_description", "test description")));
            Assert.IsTrue(triples.Contains(new TestTriple(ns1 + "objects/11", ns3 + "object_file", ns1 + "files/21")));
            Assert.IsTrue(triples.Contains(new TestTriple(ns1 + "objects/11", ns3 + "object_file", ns1 + "files/22")));
            Assert.IsTrue(triples.Contains(new TestTriple(ns1 + "objects/11", ns3 + "object_type", ns4 + "raw_image")));
            Assert.IsTrue(triples.Contains(new TestTriple(ns1 + "objects/11", ns3 + "object_segmentation_method_description", "test segmentation method description")));
            Assert.IsTrue(triples.Contains(new TestTriple(ns1 + "objects/11", ns3 + "object_anatomical_region", "http://purl.org/obo/owlapi/fma#FMA_50801")));
            Assert.IsTrue(triples.Contains(new TestTriple(ns1 + "objects/11", ns3 + "object_anatomical_region", "http://purl.org/obo/owlapi/fma#FMA_9611")));
        }

        [TestMethod]
        public void RdfMapper_MapTriples_ObjectVersion2()
        {
            //assemble
            var mapper = new RdfMapper(new RdfMapperJsonReader(), MappingDocumentPath, ns2);
            var model = new ObjectVersion()
            {
                Id = 11
            };

            //act
            TripleContainer container = mapper.MapTriples(model);
            foreach (TestTriple triple in container.Triples.Select(t => new TestTriple(t)))
                Debug.WriteLine(triple.ToString());

            //assert
            Assert.AreEqual(ns1 + "objects/11", container.Subject);
        }

        [TestMethod]
        public void RdfMapper_GetIds()
        {
            //assemble
            var mapper = new RdfMapper(new RdfMapperJsonReader(), MappingDocumentPath, ns2);
            var model = new ObjectVersion()
            {
                Id = 1234
            };

            //act
            IEnumerable<string> ids = mapper.GetIds(model);

            //assert
            Assert.AreEqual(1, ids.Count());
            Assert.IsTrue(ids.Contains(model.Id.ToString()));
        }

        [TestMethod]
        [ExpectedExceptionWithMessage(typeof(ArgumentException), "RdfMapperSubjectIdAttribute is missing")]
        public void RdfMapper_GetIds_Exception1()
        {
            //assemble
            var mapper = new RdfMapper(new RdfMapperJsonReader(), MappingDocumentPath, ns2);
            var model = new ErrorModel1();

            //act
            IEnumerable<string> ids = mapper.GetIds(model);
        }
    }
}
