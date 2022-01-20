using API.PropertiesUS.DAL.Dominio;
using API.PropertiesUS.DAL.Repo;
using API.PropertiesUS.DTO;
using System;

namespace API.PropertiesUS.BL
{
    /// <summary>
    /// Class for business validations for table: PropertyImage 
    /// </summary>
    public class PropertyImagesBL : IPropertyImagesBL
    {
        private readonly IBaseRepo<PropertyImages> _propertyImagesRepo;
        private readonly IBaseRepo<Properties> _propertiesRepo;
        private readonly UtilBL _utilBL;
        /// <summary>
        /// Class constructor
        /// </summary>
        public PropertyImagesBL(string connectionString = null)
        {
            _propertyImagesRepo = new BaseRepo<PropertyImages>(connectionString);
            _propertiesRepo = new BaseRepo<Properties>(connectionString);
            _utilBL = new UtilBL();
        }

        /// <summary>
        /// Method to add an image record for a property
        /// </summary>
        /// <param name="propertyImage">PropertyImageDTO with property data to query and image values</param>
        /// <returns>Identifier of the new record</returns>
        public long AddImageByProperty(PropertyImageDTO propertyImage)
        {
            try
            {
                if ((string.IsNullOrWhiteSpace(propertyImage.CodeInternalProperty) 
                    && string.IsNullOrWhiteSpace(propertyImage.NameProperty) && (!propertyImage.IdProperty.HasValue || propertyImage.IdProperty.Value == 0)) || (propertyImage.FileImage == null && string.IsNullOrWhiteSpace(propertyImage.ImageBase64)))
                {
                    throw new Exception("Error in registration data for property image.");
                }
                var property = _propertiesRepo.GetByFilter(x => !string.IsNullOrWhiteSpace(propertyImage.CodeInternalProperty) ? x.CodeInternal.Contains(propertyImage.CodeInternalProperty) : x.Name.Contains(propertyImage.NameProperty));
                if (property == null || property.IdProperty == 0)
                {
                    throw new Exception("Failed to get property.");
                }
                PropertyImages newPropertyImage = new PropertyImages()
                {
                    File = string.IsNullOrWhiteSpace(propertyImage.ImageBase64) ? _utilBL.GetBase64(propertyImage.FileImage) : propertyImage.ImageBase64,
                    IdProperty = property.IdProperty,
                    Enabled = true
                };
                newPropertyImage = _propertyImagesRepo.Add(newPropertyImage);
                return newPropertyImage.IdPropertyImage;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Method to update a Property Image
        /// </summary>
        /// <param name="propertyImage">PropertyImageSimpleDTO with data to query the registry and change the image</param>
        /// <returns>Successful update indicator</returns>
        public bool UpdateImageById(PropertyImageSimpleDTO propertyImage)
        {
            try
            {
                if (string.IsNullOrEmpty(propertyImage.ImageBase64) && propertyImage.FileImage == null)
                {
                    throw new Exception("There is no image to update record.");
                }
                var propertyImageExist = _propertyImagesRepo.GetByFilter(x => x.IdPropertyImage == propertyImage.IdPropertyImage);
                if (propertyImageExist != null && propertyImageExist.IdPropertyImage > 0)
                {
                    propertyImageExist.File = !string.IsNullOrWhiteSpace(propertyImage.ImageBase64) ? propertyImage.ImageBase64 : _utilBL.GetBase64(propertyImage.FileImage);
                    propertyImageExist = _propertyImagesRepo.Update(propertyImageExist);
                    return true;
                }
                else
                {
                    throw new Exception("Property Image query error.");
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }

}
