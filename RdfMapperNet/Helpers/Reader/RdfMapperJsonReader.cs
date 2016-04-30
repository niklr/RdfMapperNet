using Newtonsoft.Json;
using RdfMapperNet.Model;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace RdfMapperNet.Helpers.Reader
{
    /*
        A reader that supports RDF mapping information in JSON format as illustrated in the example below.
        It is possible to define multiple namespaces which will be replaced automatically. The key entry
        can be used as name in the subject and predicate attribute. The value entry is the corresponding 
        RDF representation.
        {
          "namespaces": {
            "ns1": "http://www.smir.ch/api/",
            "ns2": "http://www.virtualskeleton.ch/schema/rdf-mapping#",
            "ns3": "http://www.virtualskeleton.ch/schema/metadata4data#"
          },
          "map": {
            "ns2:s_object_uri": "ns1:objects/{id}",
            "ns2:o_object_rdf_type": "ns3:object",
            "ns2:p_object_id": "ns3:object_id",
            "ns2:p_object_type": "ns3:object_type",
            "ns2:p_object_created_date": "ns3:object_created_date",
            "ns2:p_object_description": "ns3:object_description",
            "ns2:p_object_file": "ns3:object_file",
            "ns2:p_object_anatomical_region": "ns3:object_anatomical_region",
            "ns2:p_object_segmentation_method_description": "ns3:object_segmentation_method_description",

            "ns2:s_file_uri": "ns1:files/{id}",
            "ns2:o_file_rdf_type": "ns3:file",
            "ns2:p_file_id": "ns3:file_id"
          }
        }
    */
    public class RdfMapperJsonReader : IRdfMapperReader
    {
        /// <summary>
        /// Opens a text file, reads all lines of the file, and then closes the file. Generates the RDF mapping dictionary.
        /// </summary>
        /// <param name="path">The file to open for reading.</param>
        /// <returns>RDF mapping dictionary</returns>
        public Dictionary<string, string> Read(string path)
        {
            MappingDocument document = JsonConvert.DeserializeObject<MappingDocument>(File.ReadAllText(path));
            Dictionary<string, string> map = new Dictionary<string, string>();

            foreach (var item in document.Map)
                map.Add(ReplaceNamespace(document, item.Key), ReplaceNamespace(document, item.Value));

            return map;
        }

        private string ReplaceNamespace(MappingDocument document, string value)
        {
            foreach (string ns in document.Namespaces.Keys)
            {
                string formattedNamespace = string.Format("{0}:", ns);
                if (value.StartsWith(formattedNamespace))
                    return Regex.Replace(value, string.Format(@"^{0}\s*", formattedNamespace), document.Namespaces[ns]);
            }

            return value;
        }
    }
}
