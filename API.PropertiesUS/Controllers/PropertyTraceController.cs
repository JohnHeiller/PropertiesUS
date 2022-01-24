using System;
using API.PropertiesUS.BL;
using API.PropertiesUS.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace API.PropertiesUS.Controllers
{
    /// <summary>
    /// Controller actions for Sales data management or property traces
    /// </summary>
    [Authorize]
    [SwaggerTag("PropertyTraces API - Controller actions for Sales data management or property traces")]
    [ApiController]
    [Route("[controller]")]
    public class PropertyTraceController : ControllerBase
    {
        /// <summary>
        /// Object of type IConfiguration
        /// </summary>
        public IConfiguration _configuration { get; }
        /// <summary>
        /// Object of type IPropertyTraceBL
        /// </summary>
        private readonly IPropertyTraceBL _propertyTracesBL;
        /// <summary>
        /// Object of type ILogger(PropertyTraceController)
        /// </summary>
        private readonly ILogger<PropertyTraceController> _logger;

        /// <summary>
        /// Class constructor of controller
        /// </summary>
        /// <param name="configuration">Object of type IConfiguration</param>
        /// <param name="logger">Object to record the log</param>
        public PropertyTraceController(IConfiguration configuration, ILogger<PropertyTraceController> logger)
        {
            _logger = logger;
            _configuration = configuration;
            string connection = configuration.GetSection("ConnectionStrings")["APIConnection"];
            _propertyTracesBL = new PropertyTraceBL(connection);
        }

        /// <summary>
        /// Create a new Sale record or property trace
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Create
        ///     {
        ///       "idProperty": 1,
        ///       "nameProperty": "",
        ///       "codeInternalProperty": "",
        ///       "nameTrace": "Venta de prueba",
        ///       "valueTrace": "15000500",
        ///       "taxTrace": "520"
        ///     }
        ///      
        /// </remarks>
        /// <param name="propertyTrace">PropertyTraceDTO with data to identify the property and record the sale or property trace</param>
        /// <returns>Record Identifier</returns>
        /// <response code="200">Returns the ID of the new record</response>
        /// <response code="400">Notify error in business validations</response>  
        /// <response code="401">Report authentication error</response>  
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create(PropertyTraceDTO propertyTrace)
        {
            try
            {
                var resp = _propertyTracesBL.CreateTrace(propertyTrace);
                return Ok(new { idPropertyTrace = resp });
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.InnerException != null ? exc.InnerException.Message : exc.Message);
                return BadRequest(exc.InnerException != null ? exc.InnerException.Message : exc.Message);                
            }
        }

        /// <summary>
        /// Update a Sale or property trace
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Update
        ///     {
        ///       "idPropertyTrace": 1,
        ///       "name": "Prueba de venta",
        ///       "dateSale": "2021-07-05",
        ///       "value": "12540000",
        ///       "tax": "400"
        ///     }
        ///      
        /// </remarks>
        /// <param name="propertyTraceSimple">PropertyTraceSimpleDTO with data to identify the property</param>
        /// <returns>Successful update indicator</returns>
        /// <response code="200">Returns successful record update indicator</response>
        /// <response code="400">Notify error in business validations</response>  
        /// <response code="401">Report authentication error</response>  
        [HttpPost]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(PropertyTraceSimpleDTO propertyTraceSimple)
        {
            try
            {
                var resp = _propertyTracesBL.UpdatePropertyTrace(propertyTraceSimple);
                return Ok(new { traceChanged = resp });
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.InnerException != null ? exc.InnerException.Message : exc.Message);
                return BadRequest(exc.InnerException != null ? exc.InnerException.Message : exc.Message);
            }
        }

    }
}
