using Microsoft.VisualStudio.TestTools.UnitTesting;
using RdfMapperNet.Methods;

namespace RdfMapperNet.Tests.Unit.Methods
{
    [TestClass]
    public class DefaultRdfMapperSubjectMethodsTest
    {
        [TestMethod]
        public void DefaultRdfMapperSubjectMethods_MapSubjectUri()
        {
            //assemble
            DefaultRdfMapperSubjectMethods methods = new DefaultRdfMapperSubjectMethods();
            string subjectUriTemplate = "http://www.smir.ch/api/objects/{id}";
            string[] ids = new string[] { "1234" };

            //act
            string subjectUri = methods.MapSubjectUri(subjectUriTemplate, ids);

            //assert
            Assert.AreEqual("http://www.smir.ch/api/objects/1234", subjectUri);
        }

        [TestMethod]
        public void DefaultRdfMapperSubjectMethods_MapSubjectUri_EmptySubjectUriTemplate()
        {
            //assemble
            DefaultRdfMapperSubjectMethods methods = new DefaultRdfMapperSubjectMethods();
            string subjectUriTemplate = string.Empty;
            string[] ids = new string[] { "1234" };

            //act
            string subjectUri = methods.MapSubjectUri(subjectUriTemplate, ids);

            //assert
            Assert.AreEqual(string.Empty, subjectUri);
        }

        [TestMethod]
        public void DefaultRdfMapperSubjectMethods_MapSubjectUri_EmptyIds()
        {
            //assemble
            DefaultRdfMapperSubjectMethods methods = new DefaultRdfMapperSubjectMethods();
            string subjectUriTemplate = "http://www.smir.ch/api/objects/{id}";
            string[] ids = new string[] { };

            //act
            string subjectUri = methods.MapSubjectUri(subjectUriTemplate, ids);

            //assert
            Assert.AreEqual(string.Empty, subjectUri);
        }
    }
}
