using System;
using API.PropertiesUS.BL;
using API.PropertiesUS.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace API.PropertiesUS.Controllers
{
    /// <summary>
    /// Controller actions for Owner data management
    /// </summary>
    [SwaggerTag("Owners API - Controller actions for Owner data management")]
    [ApiController]
    [Route("[controller]")]
    public class OwnerController : ControllerBase
    {
        /// <summary>
        /// Object of type IConfiguration
        /// </summary>
        public IConfiguration _configuration { get; }
        /// <summary>
        /// Object of type IOwnerBL
        /// </summary>
        private readonly IOwnerBL _ownerBL;
        /// <summary>
        /// Object of type ILogger(OwnerController)
        /// </summary>
        private readonly ILogger<OwnerController> _logger;

        /// <summary>
        /// Class constructor of controller
        /// </summary>
        /// <param name="configuration">Object of type IConfiguration</param>
        /// <param name="logger">Object to record the log</param>
        public OwnerController(IConfiguration configuration, ILogger<OwnerController> logger)
        {
            _logger = logger;
            _configuration = configuration;
            string connection = configuration.GetSection("ConnectionStrings")["APIConnection"];
            _ownerBL = new OwnerBL(connection);
        }

        /// <summary>
        /// Create a new Owner record
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Create
        ///     {
        ///       "name": "UsuarioEmpresarial",
        ///       "address": "Calle 1 # 1 - 1",
        ///       "phone": "3005003050",
        ///       "photo": null,
        ///       "photoBase64": null,
        ///       "birthday": "1990-01-01"
        ///     }
        ///      
        /// </remarks>
        /// <param name="owner">OwnerDTO with basic data to register the owner</param>
        /// <returns>Record Identifier</returns>
        /// <response code="200">Returns the ID of the new record</response>
        /// <response code="400">Notify error in business validations</response>  
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create(OwnerDTO owner)
        {
            try
            {
                var resp = _ownerBL.CreateOwner(owner);
                return Ok(new { idOwner = resp });
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.InnerException != null ? exc.InnerException.Message : exc.Message);
                return BadRequest(exc.InnerException != null ? exc.InnerException.Message : exc.Message);                
            }
        }

        /// <summary>
        /// Get the list of all registered Owners
        /// </summary>
        /// <returns>List of records consulted</returns>
        /// <response code="200">Returns the List of records consulted successfully</response>
        /// <response code="400">Notify error in business validations</response>  
        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GetAll()
        {
            try
            {
                var resp = _ownerBL.GetAll();
                return Ok(new { ownersList = resp });
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.InnerException != null ? exc.InnerException.Message : exc.Message);
                return BadRequest(exc.InnerException != null ? exc.InnerException.Message : exc.Message);
            }
        }
    }
}
