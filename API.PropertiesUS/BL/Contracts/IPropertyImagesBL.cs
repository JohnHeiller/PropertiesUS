using API.PropertiesUS.DTO;

namespace API.PropertiesUS.BL
{
    /// <summary>
    /// Interface for business validations of the entity PropertyImage
    /// </summary>
    public interface IPropertyImagesBL
    {
        /// <summary>
        /// Method to add an image record for a property
        /// </summary>
        /// <param name="propertyImage">PropertyImageDTO with property data to query and image values</param>
        /// <returns>Identifier of the new record</returns>
        long AddImageByProperty(PropertyImageDTO propertyImage);

        /// <summary>
        /// Method to update a Property Image
        /// </summary>
        /// <param name="propertyImage">PropertyImageSimpleDTO with data to query the registry and change the image</param>
        /// <returns>Successful update indicator</returns>
        bool UpdateImageById(PropertyImageSimpleDTO propertyImage);
    }
}
