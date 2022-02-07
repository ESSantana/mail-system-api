using AutoMapper;
using MailSystem.Core.Entities;
using MailSystem.Core.Entities.Models;
using MailSystem.Repository.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailSystem.Service.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly ILogger<DeliveryService> _logger;
        private readonly IMapper _mapper;
        private readonly IDeliveryRepository _repository;

        public DeliveryService(IDeliveryRepository repository, ILogger<DeliveryService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public List<Delivery> Get(string trackCode, string type, DateTime? arrivedDate)
        {
            _logger.LogDebug("Get all");
            var deliveries = _repository.Get(trackCode, type, arrivedDate);

            _logger.LogDebug($"Get all result: {deliveries.Count} entities");
            return deliveries.Select(r => _mapper.Map<Delivery>(r)).ToList();
        }

        public Delivery Get(long id)
        {
            _logger.LogDebug("Get by id");
            if (id < 1)
            {
                _logger.LogWarning("Invalid ID");
                return null;
            }

            var delivery = _repository.Get(id);

            _logger.LogDebug($"Get result by id? {delivery != null}");
            return _mapper.Map<Delivery>(delivery);
        }

        public int Create(List<Delivery> deliveries)
        {
            _logger.LogDebug("Create");

            var filteredDeliveries = deliveries.Where(x => x.Id == 0).ToList();
            var deliveriesToCreate = filteredDeliveries.Select(e => _mapper.Map<DeliveryModel>(e)).ToList();

            var deliveriesCreated = _repository.Create(deliveriesToCreate);

            _logger.LogDebug($"Create: {deliveriesCreated} deliveries created");

            return deliveriesCreated;
        }

        public Delivery Modify(Delivery delivery)
        {
            _logger.LogDebug("Modify");
            if (delivery.Id < 1)
            {
                _logger.LogWarning("Invalid ID");
                return null;
            }

            var actualDelivery = Get(delivery.Id);

            if (actualDelivery == null)
            {
                _logger.LogWarning("Invalid ID");
                return null;
            }

            var deliveryModified = _repository.Modify(_mapper.Map<DeliveryModel>(delivery));
            _logger.LogDebug($"Modify success? {!string.IsNullOrEmpty(deliveryModified.Description)}");

            return _mapper.Map<Delivery>(deliveryModified);
        }

        public int Delete(long id)
        {
            _logger.LogDebug("Delete");
            var deliveryToDelete = Get(id);

            if (deliveryToDelete == null)
            {
                _logger.LogWarning("Invalid ID");
                return 0;
            }

            var result = _repository.Delete(id);
            _logger.LogDebug($"Delete: delivery with id({id}) deleted");

            return result;
        }
    }
}
