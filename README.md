RdfMapperNet
=========

RdfMapperNet is a .NET library to map classes to RDF triples. Attributes are used to annotate classes and their properties in order to map them to subjects, predicates and objects.

### Dependencies:
* [Json.NET](http://www.newtonsoft.com/json)

## Example
### The mapping document in JSON format
```json
{
  "namespaces": {
    "ns1": "http://www.smir.ch/api/",
    "ns2": "http://www.virtualskeleton.ch/schema/rdf-mapping#",
    "ns3": "http://www.virtualskeleton.ch/schema/metadata4data#"
  },
  "map": {
    "ns2:s_file_uri": "ns1:files/{id}",
    "ns2:o_file_rdf_type": "ns3:file",
    "ns2:p_file_id": "ns3:file_id",
	"ns2:p_file_extension": "ns3:file_extension"
  }
}
```

### The class to be mapped
```csharp
[RdfMapperSubject("s_file_uri", RdfType = "o_file_rdf_type")]
public class File
{
	[RdfMapperSubjectId]
	[RdfMapperPredicate("p_file_id")]
	public int Id { get; set; }

	[RdfMapperPredicate("p_file_extension")]
	public string Extension { get; set; }
}
```

### Map the class to RDF triples
```csharp
var mapper = new RdfMapper(new RdfMapperJsonReader(), mappingDocumentPath, mappingNamespace);
var triples = mapper.MapTriples(model);
```

### Output
```
'http://www.smir.ch/api/files/1'	'http://www.w3.org/1999/02/22-rdf-syntax-ns#type'						'http://www.virtualskeleton.ch/schema/metadata4data#file'
'http://www.smir.ch/api/files/1'	'http://www.virtualskeleton.ch/schema/metadata4data#file_id'			'1'
'http://www.smir.ch/api/files/1'	'http://www.virtualskeleton.ch/schema/metadata4data#file_extension'		'dcm'
```