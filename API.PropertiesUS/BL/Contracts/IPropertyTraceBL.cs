using API.PropertiesUS.DTO;

namespace API.PropertiesUS.BL
{
    /// <summary>
    /// Interface for business validations of the entity PropertyTrace
    /// </summary>
    public interface IPropertyTraceBL
    {
        /// <summary>
        /// Method to create a record of sale or trace of a property
        /// </summary>
        /// <param name="propertyTrace">PropertyTraceDTO with property query data and, sale or trace to record</param>
        /// <returns>New record identifier</returns>
        long CreateTrace(PropertyTraceDTO propertyTrace);

        /// <summary>
        /// Method for updating a record of sale or trace of property
        /// </summary>
        /// <param name="propertySimple">PropertySimpleDTO with property trace query data and values to update</param>
        /// <returns>Successful update indicator</returns>
        bool UpdatePropertyTrace(PropertyTraceSimpleDTO propertySimple);
    }
}
