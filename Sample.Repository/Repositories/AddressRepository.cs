using MailSystem.Core.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sample.Repository.Context;
using Sample.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Sample.Repository.Repositories
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
            _logger.LogDebug("Get");
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
                _logger.LogError(ex, $"Get error: {ex.Message}", ex);
                throw;
            }
        }

        public AddressModel Get(long Id)
        {
            _logger.LogDebug("Get by Id");
            try
            {
                var address = _context.Set<AddressModel>()
                    .AsNoTracking()
                    .Where(x => x.DeletedAt == DateTime.MinValue)
                    .FirstOrDefault(x => x.Id == Id);

                return address;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"Get by Id error: {ex.Message}");
                throw;
            }
        }

        public int Create(List<AddressModel> entities)
        {
            _logger.LogDebug("Create");
            try
            {
                _context.Set<AddressModel>().AddRange(entities);

                var result = _context.SaveChanges();

                if (result < 1)
                {
                    return 0;
                }

                return result;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"Create error: {ex.Message}");
                throw;
            }
        }

        public AddressModel Modify(AddressModel entity)
        {
            _logger.LogDebug("Modify");
            try
            {
                var currentAddress = Get(entity.Id);

                if (currentAddress == null)
                {
                    throw new Exception("Address was not found");
                }

                currentAddress.Street = entity.Street;
                currentAddress.Number = entity.Number;
                currentAddress.Neighborhood = entity.Neighborhood;
                currentAddress.ZipCode = entity.ZipCode;
                currentAddress.UpdatedAt = DateTime.Now;

                _context.Set<AddressModel>().Update(currentAddress);

                _context.SaveChanges();

                return currentAddress;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Modify error: {ex.Message}", ex);
                throw;
            }
        }

        public int Delete(long Id)
        {
            _logger.LogDebug("Delete");
            try
            {
                var address = Get(Id);

                address.DeletedAt = DateTime.Now;

                var entitiesRemoved = _context.SaveChanges();

                return entitiesRemoved;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete error: {ex.Message}", ex);
                throw;
            }
        }

    }
}
