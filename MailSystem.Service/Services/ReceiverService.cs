using AutoMapper;
using MailSystem.Core.Entities;
using MailSystem.Core.Entities.Models;
using MailSystem.Repository.Repositories.Interfaces;
using MailSystem.Service.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace MailSystem.Service.Services
{
    public class ReceiverService : IReceiverService
    {
        private readonly ILogger<ReceiverService> _logger;
        private readonly IMapper _mapper;
        private readonly IReceiverRepository _repository;

        public ReceiverService(IReceiverRepository repository, ILogger<ReceiverService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public List<Receiver> Get(string name, string document)
        {
            _logger.LogDebug("Get all");
            var receivers = _repository.Get(name, document);

            _logger.LogDebug($"Get all result: {receivers.Count} receivers");
            return receivers.Select(r => _mapper.Map<Receiver>(r)).ToList();
        }

        public Receiver Get(long id)
        {
            _logger.LogDebug("Get by id");
            if (id < 1)
            {
                _logger.LogWarning("Invalid ID");
                return null;
            }

            var receiver = _repository.Get(id);

            _logger.LogDebug($"Get by id result? {receiver != null}");
            return _mapper.Map<Receiver>(receiver);
        }

        public int Create(List<Receiver> receivers)
        {
            _logger.LogDebug("Create");

            var filteredReceivers = receivers.Where(x => x.Id == 0).ToList();
            var receiversToCreate = filteredReceivers.Select(e => _mapper.Map<ReceiverModel>(e)).ToList();

            var receiversCreated = _repository.Create(receiversToCreate);

            _logger.LogDebug($"Create: {receiversCreated} receivers created");

            return receiversCreated;
        }

        public Receiver Modify(Receiver receiver)
        {
            _logger.LogDebug("Modify");
            if (receiver.Id < 1)
            {
                _logger.LogWarning("Invalid ID");
                return null;
            }

            var actualReceiver = Get(receiver.Id);

            if (actualReceiver == null)
            {
                _logger.LogWarning("Invalid ID");
                return null;
            }

            var receiverModified = _repository.Modify(_mapper.Map<ReceiverModel>(receiver));
            _logger.LogDebug($"Modify success? {!string.IsNullOrEmpty(receiverModified.Name)}");

            return _mapper.Map<Receiver>(receiverModified);
        }

        public int Delete(long id)
        {
            _logger.LogDebug("Delete");
            var receiverToDelete = Get(id);

            if (receiverToDelete == null)
            {
                _logger.LogWarning("Invalid ID");
                return 0;
            }

            var result = _repository.Delete(id);
            _logger.LogDebug($"Delete: receivers with id({id}) deleted");

            return result;
        }
    }
}
