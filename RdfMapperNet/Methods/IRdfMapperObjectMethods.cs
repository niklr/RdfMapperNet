namespace RdfMapperNet.Methods
{
    /// <summary>
    /// Methods used to map the model property value to the RDF object.
    /// </summary>
    public interface IRdfMapperObjectMethods
    {
        /// <summary>
        /// Maps the provided object to a string representation.
        /// </summary>
        /// <param name="value">The object to be mapped.</param>
        /// <returns>String representation of the object.</returns>
        string MapObjectToString(object value);
    }
}
