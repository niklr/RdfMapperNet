namespace RdfMapperNet.Methods
{
    /// <summary>
    /// The default methods used to map the model property value to the RDF object.
    /// </summary>
    public class DefaultRdfMapperObjectMethods : IRdfMapperObjectMethods
    {
        /// <summary>
        /// Maps the provided object to a string representation.
        /// </summary>
        /// <param name="value">The object to be mapped.</param>
        /// <returns>String representation of the object.</returns>
        public string MapObjectToString(object value)
        {
            return value.ToString();
        }
    }
}
