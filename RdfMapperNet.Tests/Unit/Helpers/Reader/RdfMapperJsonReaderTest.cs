using Microsoft.VisualStudio.TestTools.UnitTesting;
using RdfMapperNet.Helpers.Reader;

namespace RdfMapperNet.Tests.Unit.Helpers.Reader
{
    [TestClass]
    public class RdfMapperJsonReaderTest
    {
        private const string MappingDocumentPath = @"Data\mapping_document.json";

        [TestMethod]
        public void RdfMapperJsonReader_Read()
        {
            //assemble
            var reader = new RdfMapperJsonReader();

            //act
            var dictionary = reader.Read(MappingDocumentPath);


            //assert
            Assert.IsNotNull(dictionary);
            Assert.AreEqual("http://www.smir.ch/api/objects/{id}", dictionary["http://www.virtualskeleton.ch/schema/rdf-mapping#s_object_uri"]);
        }
    }
}
