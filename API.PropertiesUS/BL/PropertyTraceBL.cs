using API.PropertiesUS.DAL.Dominio;
using API.PropertiesUS.DAL.Repo;
using API.PropertiesUS.DTO;
using System;
using System.Linq.Expressions;

namespace API.PropertiesUS.BL
{
    /// <summary>
    /// Class for business validations of the entity PropertyTrace 
    /// </summary>
    public class PropertyTraceBL : IPropertyTraceBL
    {
        private readonly IBaseRepo<Properties> _propertiesRepo;
        private readonly IBaseRepo<PropertyTraces> _propertyTracesRepo;
        /// <summary>
        /// Class constructor
        /// </summary>
        public PropertyTraceBL(string connectionString = null)
        {
            _propertiesRepo = new BaseRepo<Properties>(connectionString);
            _propertyTracesRepo = new BaseRepo<PropertyTraces>(connectionString);
        }

        /// <summary>
        /// Method to create a record of sale or trace of a property
        /// </summary>
        /// <param name="propertyTrace">PropertyTraceDTO with property query data and, sale or trace to record</param>
        /// <returns>New record identifier</returns>
        public long CreateTrace(PropertyTraceDTO propertyTrace)
        {
            try
            {
                Expression<Func<Properties, bool>> where = null;
                decimal newValue = decimal.Parse(propertyTrace.ValueTrace);
                if (newValue == 0)
                {
                    throw new Exception("The sale or trace value of the property must be greater than zero.");
                }
                decimal newTax = decimal.Parse(propertyTrace.TaxTrace);
                if (newTax == 0)
                {
                    throw new Exception("The sales or trace tax of the property must be greater than zero.");
                }

                if (string.IsNullOrWhiteSpace(propertyTrace.CodeInternalProperty)
                    && string.IsNullOrWhiteSpace(propertyTrace.NameProperty) && (!propertyTrace.IdProperty.HasValue || propertyTrace.IdProperty.Value == 0))
                {
                    throw new Exception("Property query data error.");
                }
                else
                {
                    if (propertyTrace.IdProperty.HasValue)
                    {
                        where = x => x.IdProperty == propertyTrace.IdProperty.Value;
                    }
                    else if (!string.IsNullOrWhiteSpace(propertyTrace.CodeInternalProperty))
                    {
                        where = x => x.CodeInternal.Contains(propertyTrace.CodeInternalProperty);
                    }
                    else if (!string.IsNullOrWhiteSpace(propertyTrace.NameProperty))
                    {
                        where = x => x.Name.Contains(propertyTrace.NameProperty);
                    }
                }
                var property = _propertiesRepo.GetByFilter(where.Compile());
                if (property != null && property.IdProperty > 0)
                {
                    DateTime newDateSale = DateTime.Now;
                    if (!string.IsNullOrWhiteSpace(propertyTrace.DateSale))
                    {
                        bool isValidDate = DateTime.TryParse(propertyTrace.DateSale, out newDateSale);
                        if (!isValidDate)
                        {
                            newDateSale = DateTime.Now;
                        }
                    }
                    PropertyTraces newPropertyTraces = new PropertyTraces()
                    {
                        DateSale = newDateSale,
                        IdProperty = property.IdProperty,
                        Name = propertyTrace.NameTrace,
                        Value = newValue,
                        Tax = newTax
                    };
                    newPropertyTraces = _propertyTracesRepo.Add(newPropertyTraces);
                    return newPropertyTraces.IdPropertyTrace;
                }
                else
                {
                    throw new Exception("Property query error.");
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Method to update a record of sale or ownership trace
        /// </summary>
        /// <param name="propertySimple">PropertySimpleDTO with property trace query data and values to update</param>
        /// <returns>Successful update indicator</returns>
        public bool UpdatePropertyTrace(PropertyTraceSimpleDTO propertySimple)
        {
            if (string.IsNullOrWhiteSpace(propertySimple.Name) && string.IsNullOrWhiteSpace(propertySimple.Tax)
                && string.IsNullOrWhiteSpace(propertySimple.Value) && string.IsNullOrWhiteSpace(propertySimple.DateSale))
            {
                throw new Exception("There is no data to update on the sale or trace of the property.");
            }
            else
            {
                if (propertySimple.IdPropertyTrace > 0)
                {
                    var propertyTraceExist = _propertyTracesRepo.GetByFilter(x => x.IdPropertyTrace == propertySimple.IdPropertyTrace);
                    if (propertyTraceExist != null && propertyTraceExist.IdPropertyTrace > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(propertySimple.Value))
                        {
                            propertyTraceExist.Value = decimal.TryParse(propertySimple.Value, out decimal newValue) ? newValue : propertyTraceExist.Value;
                        }
                        if (!string.IsNullOrWhiteSpace(propertySimple.Tax))
                        {
                            propertyTraceExist.Tax = decimal.TryParse(propertySimple.Tax, out decimal newTax) ? newTax : propertyTraceExist.Tax;
                        }
                        if (!string.IsNullOrWhiteSpace(propertySimple.Name))
                        {
                            propertyTraceExist.Name = propertySimple.Name;
                        }
                        if (!string.IsNullOrWhiteSpace(propertySimple.DateSale))
                        {
                            propertyTraceExist.DateSale = DateTime.TryParse(propertySimple.DateSale, out DateTime newDateSale) ? newDateSale : propertyTraceExist.DateSale;
                        }

                        var traceChanged = _propertyTracesRepo.Update(propertyTraceExist);
                        return true;
                    }
                    else
                    {
                        throw new Exception("Error in the sale query or property trace.");
                    }
                }
                else
                {
                    throw new Exception("Error with property trace log query data.");
                }
            }
        }

    }
}
