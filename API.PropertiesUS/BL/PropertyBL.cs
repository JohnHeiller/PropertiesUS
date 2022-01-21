using API.PropertiesUS.DAL.Dominio;
using API.PropertiesUS.DAL.Repo;
using API.PropertiesUS.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace API.PropertiesUS.BL
{
    /// <summary>
    /// Class for business validations table: Property
    /// </summary>
    public class PropertyBL : IPropertyBL
    {
        private readonly IBaseRepo<Properties> _propertiesRepo;
        private readonly IBaseRepo<Owners> _ownersRepo;
        /// <summary>
        /// Class constructor
        /// </summary>
        public PropertyBL(string connectionString = null)
        {
            _propertiesRepo = new BaseRepo<Properties>(connectionString);
            _ownersRepo = new BaseRepo<Owners>(connectionString);
        }

        /// <summary>
        /// Method to validate the correct creation of a property registry
        /// </summary>
        /// <param name="property">PropertyDTO with Owner data to register</param>
        /// <returns>Identifier of the new record</returns>
        public long CreateProperty(PropertyDTO property)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(property.AddressProperty)
                    || string.IsNullOrWhiteSpace(property.NameProperty) || string.IsNullOrWhiteSpace(property.PriceProperty))
                {
                    throw new Exception("Error in registration data for property.");
                }
                if (string.IsNullOrWhiteSpace(property.NameOwner) && (!property.IdOwner.HasValue || property.IdOwner.Value == 0))
                {
                    throw new Exception("Error in registration data of the owner of the property.");
                }
                decimal newPrice = decimal.Parse(property.PriceProperty);
                if (newPrice == 0)
                {
                    throw new Exception("The price of the property must be greater than zero.");
                }
                var owner = _ownersRepo.GetByFilter(x => (property.IdOwner.HasValue && property.IdOwner.Value > 0) ? x.IdOwner == property.IdOwner.Value : x.Name.Contains(property.NameOwner));
                if (owner == null || owner.IdOwner == 0)
                {
                    throw new Exception("Failed to get owner.");
                }
                Properties newProperty = new Properties()
                {
                    Address = property.AddressProperty,
                    CodeInternal = !string.IsNullOrWhiteSpace(property.CodeInternalProperty) ? property.CodeInternalProperty : Guid.NewGuid().ToString(),
                    Name = property.NameProperty,
                    Price = newPrice,
                    Year = !string.IsNullOrWhiteSpace(property.Year) ? property.Year : DateTime.Today.Year.ToString(),
                    IdOwner = owner.IdOwner                    
                };
                newProperty = _propertiesRepo.Add(newProperty);
                return newProperty.IdProperty;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Method to get list of properties according to query filters
        /// </summary>
        /// <param name="property">Property DTO with data used as query filters</param>
        /// <returns>List of property records consulted</returns>
        public List<Properties> GetPropertiesByFilter(PropertyDTO property)
        {
            try
            {
                List<Properties> propertiesResp = new List<Properties>();
                decimal newPrice = 0;
                bool addressWhere = false, codeWhere = false, nameWhere = false, priceWhere = false, yearWhere = false, ownerWhere = false;
                Owners owner = new Owners();
                if (property.PriceProperty.Length > 0)
                {
                    newPrice = decimal.Parse(property.PriceProperty);
                }
                Expression<Func<Properties, bool>> where = x => true;
                var param = Expression.Parameter(typeof(Properties), "x");
                if ((property.IdOwner.HasValue && property.IdOwner.Value > 0) || !string.IsNullOrWhiteSpace(property.NameOwner))
                {
                    owner = _ownersRepo.GetByFilter(x => property.IdOwner.HasValue ? x.IdOwner == property.IdOwner.Value : x.Name.Contains(property.NameOwner));
                }

                if (!string.IsNullOrWhiteSpace(property.AddressProperty))
                {
                    addressWhere = true;
                }
                if (!string.IsNullOrWhiteSpace(property.CodeInternalProperty))
                {
                    codeWhere = true;
                }
                if (!string.IsNullOrWhiteSpace(property.NameProperty))
                {
                    nameWhere = true;
                }
                if (newPrice > 0)
                {
                    priceWhere = true;
                }
                if (!string.IsNullOrWhiteSpace(property.Year))
                {
                    yearWhere = true;
                }
                if (owner != null && owner.IdOwner > 0)
                {
                    ownerWhere = true;
                }
                where = x => true && (addressWhere ? x.Address.Contains(property.AddressProperty) : true) 
                && (codeWhere ? x.CodeInternal.Contains(property.CodeInternalProperty) : true) 
                && (nameWhere ? x.Name.Contains(property.NameProperty) : true)
                && (priceWhere ? x.Price == newPrice : true)
                && (yearWhere ? x.Year.Contains(property.Year) : true)
                && (ownerWhere ? x.IdOwner == owner.IdOwner : true);
                propertiesResp = _propertiesRepo.GetListByFilter(where.Compile());
                return propertiesResp;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Method to update the price of the property, filtered by name, id or internal code
        /// </summary>
        /// <param name="propertyPrice">PropertyPriceDTO with data to consult property and change price</param>
        /// <returns>Successful update indicator</returns>
        public bool UpdatePropertyPrice(PropertyPriceDTO propertyPrice)
        {
            try
            {
                decimal newPrice = 0;
                Expression<Func<Properties, bool>> where = null;
                if (string.IsNullOrWhiteSpace(propertyPrice.Price))
                {
                    throw new Exception("The new price of the property is required.");
                }
                else
                {
                    newPrice = decimal.Parse(propertyPrice.Price);
                    if (newPrice == 0)
                    {
                        throw new Exception("The price of the property must be greater than zero.");
                    }
                }
                if (string.IsNullOrWhiteSpace(propertyPrice.CodeInternal) && string.IsNullOrWhiteSpace(propertyPrice.Name) && (!propertyPrice.IdProperty.HasValue || propertyPrice.IdProperty.Value == 0))
                {
                    throw new Exception("Property query data error.");
                }
                else
                {
                    if (propertyPrice.IdProperty.HasValue)
                    {
                        where = x => x.IdProperty == propertyPrice.IdProperty.Value;
                    }
                    else if (!string.IsNullOrWhiteSpace(propertyPrice.CodeInternal))
                    {
                        where = x => x.CodeInternal.Contains(propertyPrice.CodeInternal);
                    }
                    else if (!string.IsNullOrWhiteSpace(propertyPrice.Name))
                    {
                        where = x => x.Name.Contains(propertyPrice.Name);
                    }
                }
                var property = _propertiesRepo.GetByFilter(where.Compile());
                if (property != null && property.IdProperty > 0)
                {
                    property.Price = newPrice;
                    property = _propertiesRepo.Update(property);
                }
                else
                {
                    throw new Exception("Property query error.");
                }
                return true;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Method to update the record of the property
        /// </summary>
        /// <param name="propertyDto">PropertyDTO with data to update property</param>
        /// <returns>Successful update indicator</returns>
        public bool UpdateProperty(PropertyDTO propertyDto)
        {
            try
            {
                decimal newPrice = 0;
                if (propertyDto.IdProperty == null || propertyDto.IdProperty == 0)
                {
                    throw new Exception("The ID of the property is required.");
                }
                newPrice = decimal.Parse(propertyDto.PriceProperty);
                if (newPrice == 0)
                {
                    throw new Exception("The price of the property must be greater than zero.");
                }
                if (!propertyDto.IdOwner.HasValue || propertyDto.IdOwner == 0)
                {
                    throw new Exception("The ID of the property owner is required.");
                }
                var property = _propertiesRepo.GetByFilter(x => x.IdProperty == propertyDto.IdProperty);
                if (property != null && property.IdProperty > 0)
                {
                    property.Name = propertyDto.NameProperty;
                    property.Price = newPrice;
                    property.Year = propertyDto.Year;
                    property.Address = propertyDto.AddressProperty;
                    property.CodeInternal = propertyDto.CodeInternalProperty;
                    property.IdOwner = propertyDto.IdOwner.Value;
                }
                else
                {
                    throw new Exception("Property query error.");
                }
                return true;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

    }
}
