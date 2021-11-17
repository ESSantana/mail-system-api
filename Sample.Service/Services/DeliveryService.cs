using AutoMapper;
using MailSystem.Core.Entities;
using MailSystem.Core.Entities.Models;
using MailSystem.Repository.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Sample.Core.Entities;
using Sample.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Service.Services
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
            _logger.LogDebug("Get All");
            var result = _repository.Get(trackCode, type, arrivedDate);

            _logger.LogDebug($"Get All Result: {result.Count} entities");
            return result.Select(r => _mapper.Map<Delivery>(r)).ToList();
        }

        public Delivery Get(long Id)
        {
            _logger.LogDebug("Get");
            if (Id < 1)
            {
                _logger.LogWarning("Invalid ID");
                return null;
            }

            var result = _repository.Get(Id);

            _logger.LogDebug($"Get result? {result != null}");
            return _mapper.Map<Delivery>(result);
        }

        public int Create(List<Delivery> entities)
        {
            _logger.LogDebug("Create");

            var shouldCreate = entities.Any(x => string.IsNullOrEmpty(x.TrackCode) || string.IsNullOrEmpty(x.Type) || x.ReceiverId < 1);
            if (shouldCreate)
            {
                _logger.LogWarning("Missing required property 'Name'");
                return 0;
            }

            var filteredEntities = entities.Where(x => x.Id == 0).ToList();
            var entitiesToCreate = filteredEntities.Select(e => _mapper.Map<DeliveryModel>(e)).ToList();

            var result = _repository.Create(entitiesToCreate);

            _logger.LogDebug($"Create: {result} entities created");

            return result;
        }

        public int Delete(long Id)
        {
            _logger.LogDebug("Delete");
            var entityToDelete = Get(Id);

            if (entityToDelete == null)
            {
                _logger.LogWarning("Invalid ID");
                return 0;
            }

            var result = _repository.Delete(Id);
            _logger.LogDebug($"Delete: entity with id({Id}) deleted");

            return result;
        }

        public Delivery Modify(Delivery entity)
        {
            _logger.LogDebug("Modify");
            if (entity.Id < 1)
            {
                _logger.LogWarning("Invalid ID");
                return null;
            }

            var actualEntity = Get(entity.Id);

            if (actualEntity == null)
            {
                _logger.LogWarning("Invalid ID");
                return null;
            }

            var result = _repository.Modify(_mapper.Map<ExampleModel>(entity));
            _logger.LogDebug($"Modify success? {!string.IsNullOrEmpty(result.Name)}");

            return _mapper.Map<Delivery>(result);
        }
    }
}
