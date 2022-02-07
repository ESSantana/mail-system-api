using AutoMapper;
using MailSystem.API.DTO;
using MailSystem.Core.Entities;
using MailSystem.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace MailSystem.API.Controllers
{
    /// <summary>
    /// Address endpoints
    /// </summary>
    [Route("api/v1/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private readonly IAddressService _service;
        private readonly IMapper _mapper;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="service">Address Service instance</param>
        /// <param name="logger">Logger instance</param>
        /// <param name="mapper">AutoMapper instance</param>
        public AddressController(IAddressService service, ILogger<AddressController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all addresses
        /// </summary>
        /// <returns>List of addresses</returns>
        [HttpGet]
        [Route("get")]
        [Authorize]
        public ActionResult<List<AddressDTO>> Get(
            [FromQuery] string street,
            [FromQuery] string number,
            [FromQuery] string neighborhood,
            [FromQuery] string zipcode
        )
        {
            _logger.LogDebug("Get all");
            var addresses = _service.Get(street, number, neighborhood, zipcode);

            _logger.LogDebug($"Get all result: {addresses.Count}");

            return addresses.Count > 0
                ? (ActionResult)Ok(addresses.Select(r => _mapper.Map<AddressDTO>(r)).ToList())
                : NoContent();
        }

        /// <summary>
        /// Get address by id
        /// </summary>
        /// <param name="id">Address id</param>
        /// <returns>Address with id requested</returns>
        [HttpGet]
        [Route("get/{id}")]
        [Authorize]
        public ActionResult<AddressDTO> Get(long id)
        {
            _logger.LogDebug("Get by id");
            var address = _service.Get(id);

            _logger.LogDebug($"Get result success? {address != null}");

            return address != null
                ? (ActionResult)Ok(_mapper.Map<AddressDTO>(address))
                : NoContent();
        }

        /// <summary>
        /// Create a new address
        /// </summary>
        /// <param name="addressesDTO">Address format</param>
        /// <returns>Number of addresses created</returns>
        [HttpPost]
        [Route("create")]
        [Authorize]
        public ActionResult<object> Create(List<AddressDTO> addressesDTO)
        {
            var addressesEntities = addressesDTO.Select(e => _mapper.Map<Address>(e)).ToList();

            _logger.LogDebug("Create");
            var result = _service.Create(addressesEntities);

            _logger.LogDebug($"Create: {result} addresses created");

            return result > 0
                ? (ActionResult)Ok(new { TotalResult = result })
                : NoContent();
        }

        /// <summary>
        /// Modify specific address
        /// </summary>
        /// <param name="addressDto">Address to modify</param>
        /// <returns>Address modified</returns>
        [HttpPost]
        [Route("modify")]
        [Authorize]
        public ActionResult<AddressDTO> Modify(AddressDTO addressDto)
        {
            var address = _mapper.Map<Address>(addressDto);

            _logger.LogDebug("Modify");
            var addressModified = _service.Modify(address);

            _logger.LogDebug($"Modify success? {addressModified != null}");

            return addressModified != null
                ? (ActionResult)Ok(_mapper.Map<AddressDTO>(addressModified))
                : NoContent();
        }

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="id">Id of the address that should be deleted</param>
        /// <returns>Number of address deleted</returns>
        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize]
        public ActionResult<object> Delete(long id)
        {
            _logger.LogDebug("Delete");
            var result = _service.Delete(id);

            _logger.LogDebug($"Delete: {result} addresses deleted");

            return result > 0
                ? (ActionResult)Ok(new { TotalDeleted = result })
                : NoContent();
        }
    }
}
