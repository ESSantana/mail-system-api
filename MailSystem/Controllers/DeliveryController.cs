using AutoMapper;
using MailSystem.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MailSystem.API.DTO;
using MailSystem.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailSystem.API.Controllers
{
    /// <summary>
    /// Delivery endpoints
    /// </summary>
    [Route("api/v1/delivery")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly ILogger<DeliveryController> _logger;
        private readonly IDeliveryService _service;
        private readonly IMapper _mapper;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="service">Delivery service instance</param>
        /// <param name="logger">Logger instance</param>
        /// <param name="mapper">AutoMapper instance</param>
        public DeliveryController(IDeliveryService service, ILogger<DeliveryController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all deliveries
        /// </summary>
        /// <returns>List of deliveries</returns>
        [HttpGet]
        [Route("get")]
        [Authorize]
        public ActionResult<List<DeliveryDTO>> Get(
            [FromQuery] string trackCode,
            [FromQuery] string type,
            [FromQuery] DateTime? arrivedDate
        )
        {
            _logger.LogDebug("Get all");
            var deliveries = _service.Get(trackCode, type, arrivedDate);

            _logger.LogDebug($"Get all result: {deliveries.Count}");

            return deliveries.Count > 0
                ? (ActionResult)Ok(deliveries.Select(r => _mapper.Map<DeliveryDTO>(r)).ToList())
                : NoContent();
        }

        /// <summary>
        /// Get delivery by id
        /// </summary>
        /// <param name="id">Delivery id</param>
        /// <returns>Devilery with id requested</returns>
        [HttpGet]
        [Route("get/{id}")]
        [Authorize]
        public ActionResult<DeliveryDTO> Get(long id)
        {
            _logger.LogDebug("Get by id");
            var delivery = _service.Get(id);

            _logger.LogDebug($"Get by id result success? {delivery != null}");

            return delivery != null
                ? (ActionResult)Ok(_mapper.Map<DeliveryDTO>(delivery))
                : NoContent();
        }

        /// <summary>
        /// Create a new delivery
        /// </summary>
        /// <param name="deliveriesDto">delivery format</param>
        /// <returns>Number of deliveries created</returns>
        [HttpPost]
        [Route("create")]
        [Authorize]
        public ActionResult<object> Create(List<DeliveryDTO> deliveriesDto)
        {
            var deliveriesEntities = deliveriesDto.Select(e => _mapper.Map<Delivery>(e)).ToList();

            _logger.LogDebug("Create");
            var result = _service.Create(deliveriesEntities);

            _logger.LogDebug($"Create: {result} deliveries created");

            return result > 0
                ? (ActionResult)Ok(new { TotalResult = result })
                : NoContent();
        }

        /// <summary>
        /// Modify specific delivery
        /// </summary>
        /// <param name="deliveryDto">Delivery to modify</param>
        /// <returns>Delivery modified</returns>
        [HttpPost]
        [Route("modify")]
        [Authorize]
        public ActionResult<DeliveryDTO> Modify(DeliveryDTO deliveryDto)
        {
            var delivery = _mapper.Map<Delivery>(deliveryDto);

            _logger.LogDebug("Modify");
            var deliveryModified = _service.Modify(delivery);

            _logger.LogDebug($"Modify success? {deliveryModified != null}");

            return deliveryModified != null
                ? (ActionResult)Ok(_mapper.Map<DeliveryDTO>(deliveryModified))
                : NoContent();
        }

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="id">Id of the delivery that should be deleted</param>
        /// <returns>Number of delivery deleted</returns>
        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize]
        public ActionResult<object> Delete(long id)
        {
            _logger.LogDebug("Delete");
            var result = _service.Delete(id);

            _logger.LogDebug($"Delete: {result} deliveries deleted");

            return result > 0
                ? (ActionResult)Ok(new { TotalDeleted = result })
                : NoContent();
        }
    }
}
