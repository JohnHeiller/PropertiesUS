using API.PropertiesUS.DAL.Dominio;
using API.PropertiesUS.DTO;
using System.Collections.Generic;

namespace API.PropertiesUS.BL
{
    /// <summary>
    /// Interface for business validations of the Owner entity
    /// </summary>
    public interface IOwnerBL
    {
        /// <summary>
        /// Method to validate the correct creation of an Owner record
        /// </summary>
        /// <param name="owner">OwnerDTO with Owner data to register</param>
        /// <returns>New record identifier</returns>
        long CreateOwner(OwnerDTO owner);

        /// <summary>
        /// Method to get all existing Owner records
        /// </summary>
        /// <returns>List of records consulted Owners</returns>
        List<Owners> GetAll();

    }
}
