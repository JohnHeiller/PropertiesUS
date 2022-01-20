using API.PropertiesUS.DAL.Dominio;
using API.PropertiesUS.DTO;
using System.Collections.Generic;

namespace API.PropertiesUS.BL
{
    /// <summary>
    /// Interface for business validations for the entity Property
    /// </summary>
    public interface IPropertyBL
    {
        /// <summary>
        /// Method to validate the correct creation of a property registry
        /// </summary>
        /// <param name="property">PropertyDTO with Owner data to register</param>
        /// <returns>Identifier of the new record</returns>
        long CreateProperty(PropertyDTO property);

        /// <summary>
        /// Method to get a list of properties according to query filters
        /// </summary>
        /// <param name="property">Property DTO with data used as query filters</param>
        /// <returns>List of queried property records Properties</returns>
        List<Properties> GetPropertiesByFilter(PropertyDTO property);

        /// <summary>
        /// Method to update the price of a property
        /// </summary>
        /// <param name="propertyPrice">PropertyPriceDTO with data to consult property and change price</param>
        /// <returns>Successful update indicator</returns>
        bool UpdatePropertyPrice(PropertyPriceDTO propertyPrice);

        /// <summary>
        /// Method to update the record of the property
        /// </summary>
        /// <param name="propertyDto">PropertyDTO with data to update property</param>
        /// <returns>Successful update indicator</returns>
        bool UpdateProperty(PropertyDTO propertyDto);
    }
}
