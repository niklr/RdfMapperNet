using RdfMapperNet.Model;
using System.Collections.Generic;

namespace RdfMapperNet.Helpers.Reader
{
    public interface IRdfMapperReader
    {
        /// <summary>
        /// Opens a text file, reads all lines of the file, and then closes the file. Generates the RDF mapping dictionary.
        /// </summary>
        /// <param name="path">The file to open for reading.</param>
        /// <returns>RDF mapping dictionary</returns>
        Dictionary<string, string> Read(string path);
    }
}
