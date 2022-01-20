using System;
using API.PropertiesUS.BL;
using API.PropertiesUS.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace API.PropertiesUS.Controllers
{
    /// <summary>
    /// Controller actions for Property Images data management
    /// </summary>
    [SwaggerTag("PropertyImages API - Controller actions for Property Images data management")]
    [ApiController]
    [Route("[controller]")]
    public class PropertyImageController : ControllerBase
    {
        /// <summary>
        /// Object of type IPropertyImagesBL
        /// </summary>
        private readonly IPropertyImagesBL _propertyImageBL;
        /// <summary>
        /// Object of type ILogger(PropertyImageController)
        /// </summary>
        private readonly ILogger<PropertyImageController> _logger;

        /// <summary>
        /// Class constructor of controller
        /// </summary>
        /// <param name="logger">Object to record the log</param>
        public PropertyImageController(ILogger<PropertyImageController> logger)
        {
            _logger = logger;
            _propertyImageBL = new PropertyImagesBL();
        }

        /// <summary>
        /// Create a new Property Image record, the property is filtered by internal code, name or ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Add
        ///     {
        ///       "fileImage": null,
        ///       "imageBase64": "/9j/4AAQSkZJRgABAQEAZABkAAD/7QNqUGhvdG9zaG9wIDMuMAA4QklNBAQAAA...",
        ///       "codeInternalProperty": "",
        ///       "nameProperty": "",
        ///       "idProperty": 1
        ///     }
        ///      
        /// </remarks>
        /// <param name="propertyImage">PropertyImageDTO with data to identify the property and the image to add</param>
        /// <returns>Record Identifier</returns>
        /// <response code="200">Returns the ID of the new record</response>
        /// <response code="400">Notify error in business validations</response>  
        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Add(PropertyImageDTO propertyImage)
        {
            try
            {
                var resp = _propertyImageBL.AddImageByProperty(propertyImage);
                return Ok(new { idPropertyImage = resp });
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.InnerException != null ? exc.InnerException.Message : exc.Message);
                return BadRequest(exc.InnerException != null ? exc.InnerException.Message : exc.Message);                
            }
        }

        /// <summary>
        /// Update a Property Image record, the property image record is filtered by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Update
        ///     {
        ///       "fileImage": null,
        ///       "imageBase64": "sdfghj0000g4j/7255555h3g45/jh3fg...",
        ///       "idPropertyImage": 1
        ///     }
        ///      
        /// </remarks>
        /// <param name="propertyImage">PropertyImageSimpleDTO with data to identify the Property Image record and the image to change</param>
        /// <returns>Successful update indicator</returns>
        /// <response code="200">Returns successful update indicator</response>
        /// <response code="400">Notify error in business validations</response>  
        [HttpPost]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(PropertyImageSimpleDTO propertyImage)
        {
            try
            {
                var resp = _propertyImageBL.UpdateImageById(propertyImage);
                return Ok(new { imageChanged = resp });
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.InnerException != null ? exc.InnerException.Message : exc.Message);
                return BadRequest(exc.InnerException != null ? exc.InnerException.Message : exc.Message);
            }
        }
    }
}
