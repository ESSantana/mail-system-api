using MailSystem.Core.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MailSystem.Repository.Context;
using MailSystem.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace MailSystem.Repository.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly MailSystemDbContext _context;
        private readonly ILogger<AddressRepository> _logger;

        public AddressRepository(MailSystemDbContext context, ILogger<AddressRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<AddressModel> Get(string street, string number, string neighborhood, string zipcode)
        {
            _logger.LogDebug("Get all");
            try
            {
                var addresses = _context.Set<AddressModel>()
                    .AsNoTracking()
                    .Where(x => x.DeletedAt == DateTime.MinValue)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(street))
                {
                    addresses = addresses.Where(x => x.Street == street);
                }
                if (!string.IsNullOrEmpty(number))
                {
                    addresses = addresses.Where(x => x.Number == number);
                }
                if (!string.IsNullOrEmpty(neighborhood))
                {
                    addresses = addresses.Where(x => x.Neighborhood == neighborhood);
                }
                if (!string.IsNullOrEmpty(zipcode))
                {
                    addresses = addresses.Where(x => x.ZipCode == zipcode);
                }

                return addresses.ToList();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"Get error: {ex.Message}");
                throw;
            }
        }

        public AddressModel Get(long id)
        {
            _logger.LogDebug("Get by id");
            try
            {
                var address = _context.Set<AddressModel>()
                    .AsNoTracking()
                    .Where(x => x.DeletedAt == DateTime.MinValue)
                    .FirstOrDefault(x => x.Id == id);

                return address;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"Get by id error: {ex.Message}");
                throw;
            }
        }

        public int Create(List<AddressModel> addresses)
        {
            _logger.LogDebug("Create");
            try
            {
                _context.Set<AddressModel>().AddRange(addresses);

                var addressesCreated = _context.SaveChanges();

                if (addressesCreated < 1)
                {
                    return 0;
                }

                return addressesCreated;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"Create error: {ex.Message}");
                throw;
            }
        }

        public AddressModel Modify(AddressModel address)
        {
            _logger.LogDebug("Modify");
            try
            {
                var currentAddress = Get(address.Id);

                if (currentAddress == null)
                {
                    throw new Exception("Address was not found");
                }

                currentAddress.Street = address.Street;
                currentAddress.Number = address.Number;
                currentAddress.Neighborhood = address.Neighborhood;
                currentAddress.ZipCode = address.ZipCode;
                currentAddress.UpdatedAt = DateTime.Now;

                _context.Set<AddressModel>().Update(currentAddress);

                _context.SaveChanges();

                return currentAddress;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Modify error: {ex.Message}");
                throw;
            }
        }

        public int Delete(long id)
        {
            _logger.LogDebug("Delete");
            try
            {
                var address = Get(id);

                address.DeletedAt = DateTime.Now;

                var addressesRemoved = _context.SaveChanges();

                return addressesRemoved;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Delete error: {ex.Message}");
                throw;
            }
        }

    }
}
