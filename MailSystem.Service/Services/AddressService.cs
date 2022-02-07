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
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repository;
        private readonly ILogger<AddressService> _logger;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository repository, ILogger<AddressService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public List<Address> Get(string street, string number, string neighborhood, string zipcode)
        {
            _logger.LogDebug("Get all");
            var addresses = _repository.Get(street, number, neighborhood, zipcode);

            _logger.LogDebug($"Get all Result: {addresses.Count} entities");
            return addresses.Select(r => _mapper.Map<Address>(r)).ToList();
        }

        public Address Get(long id)
        {
            _logger.LogDebug("Get by id");
            if (id < 1)
            {
                _logger.LogWarning("Invalid ID");
                return null;
            }

            var address = _repository.Get(id);

            _logger.LogDebug($"Get by id result? {address != null}");
            return _mapper.Map<Address>(address);
        }

        public int Create(List<Address> addresses)
        {
            _logger.LogDebug("Create");

            var filteredAddresses = addresses.Where(x => x.Id == 0).ToList();
            var addressesToCreate = filteredAddresses.Select(e => _mapper.Map<AddressModel>(e)).ToList();

             var addressesCreated = _repository.Create(addressesToCreate);

            _logger.LogDebug($"Create: {addressesCreated} entities created");

            return addressesCreated;
        }

        public Address Modify(Address address)
        {
            _logger.LogDebug("Modify");
            if (address.Id < 1)
            {
                _logger.LogWarning("Invalid ID");
                return null;
            }

            var actualAddress = Get(address.Id);

            if (actualAddress == null)
            {
                _logger.LogWarning("Invalid ID");
                return null;
            }

            var addressModified = _repository.Modify(_mapper.Map<AddressModel>(address));
            _logger.LogDebug($"Modify success? {!string.IsNullOrEmpty(addressModified.Street)}");

            return _mapper.Map<Address>(addressModified);
        }

        public int Delete(long id)
        {
            _logger.LogDebug("Delete");
            var addressToDelete = Get(id);

            if (addressToDelete == null)
            {
                _logger.LogWarning("Invalid ID");
                return 0;
            }

            var result = _repository.Delete(id);
            _logger.LogDebug($"Delete: address with id({id}) deleted");

            return result;
        }
    }
}
