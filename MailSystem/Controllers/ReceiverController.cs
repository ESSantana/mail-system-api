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
    /// Receiver endpoints
    /// </summary>
    [Route("api/v1/receiver")]
    [ApiController]
    public class ReceiverController : ControllerBase
    {
        private readonly ILogger<ReceiverController> _logger;
        private readonly IReceiverService _service;
        private readonly IMapper _mapper;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="service">Receiver Service instance</param>
        /// <param name="logger">Logger instance</param>
        /// <param name="mapper">AutoMapper instance</param>
        public ReceiverController(IReceiverService service, ILogger<ReceiverController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all receivers
        /// </summary>
        /// <returns>List of receivers</returns>
        [HttpGet]
        [Route("get")]
        [Authorize]
        public ActionResult<List<ReceiverDTO>> Get(
            [FromQuery] string name,
            [FromQuery] string document
        )
        {
            _logger.LogDebug("Get all receivers");
            var receivers = _service.Get(name, document);

            _logger.LogDebug($"Get all result: {receivers.Count}");

            return receivers.Count > 0
                ? (ActionResult)Ok(receivers.Select(r => _mapper.Map<ReceiverDTO>(r)).ToList())
                : NoContent();
        }

        /// <summary>
        /// Get Receiver by id
        /// </summary>
        /// <param name="id">Receiver id</param>
        /// <returns>Receiver with id requested</returns>
        [HttpGet]
        [Route("get/{id}")]
        [Authorize]
        public ActionResult<ReceiverDTO> Get([FromRoute] long id)
        {
            _logger.LogDebug("Get by id");
            var receiver = _service.Get(id);

            _logger.LogDebug($"Get result success? {receiver != null}");

            return receiver != null
                ? (ActionResult)Ok(_mapper.Map<ReceiverDTO>(receiver))
                : NoContent();
        }

        /// <summary>
        /// Create a new receiver
        /// </summary>
        /// <param name="receiversDto">Receiver format</param>
        /// <returns>Number of receivers created</returns>
        [HttpPost]
        [Route("create")]
        [Authorize]
        public ActionResult<object> Create(List<ReceiverDTO> receiversDto)
        {
            var receiverEntities = receiversDto.Select(e => _mapper.Map<Receiver>(e)).ToList();

            _logger.LogDebug("Create");
            var result = _service.Create(receiverEntities);

            _logger.LogDebug($"Create: {result} receivers created");

            return result > 0
                ? (ActionResult)Ok(new { TotalResult = result })
                : NoContent();
        }

        /// <summary>
        /// Modify specific receiver
        /// </summary>
        /// <param name="receiverDto">Receiver to modify</param>
        /// <returns>Receiver modified</returns>
        [HttpPost]
        [Route("modify")]
        [Authorize]
        public ActionResult<ReceiverDTO> Modify(ReceiverDTO receiverDto)
        {
            var receiver = _mapper.Map<Receiver>(receiverDto);

            _logger.LogDebug("Modify");
            var receiverModified = _service.Modify(receiver);

            _logger.LogDebug($"Modify success? {receiverModified != null}");

            return receiverModified != null
                ? (ActionResult)Ok(_mapper.Map<ReceiverDTO>(receiverModified))
                : NoContent();
        }

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="id">Id of the receiver that should be deleted</param>
        /// <returns>Number of receiver deleted</returns>
        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize]
        public ActionResult<object> Delete(long id)
        {
            _logger.LogDebug("Delete");
            var result = _service.Delete(id);

            _logger.LogDebug($"Delete: {result} receivers deleted");

            return result > 0
                ? (ActionResult)Ok(new { TotalDeleted = result })
                : NoContent();
        }

    }
}
