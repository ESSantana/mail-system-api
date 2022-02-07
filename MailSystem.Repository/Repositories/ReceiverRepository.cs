using MailSystem.Core.Entities.Models;
using MailSystem.Repository.Context;
using MailSystem.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailSystem.Repository.Repositories
{
    public class ReceiverRepository : IReceiverRepository
    {
        private readonly MailSystemDbContext _context;
        private readonly ILogger<ReceiverRepository> _logger;

        public ReceiverRepository(MailSystemDbContext context, ILogger<ReceiverRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<ReceiverModel> Get(string name, string document)
        {
            _logger.LogDebug("Get all");
            try
            {
                var receivers = _context.Set<ReceiverModel>()
                    .AsNoTracking()
                    .Include(x => x.Documents)
                    .Where(x => x.DeletedAt == DateTime.MinValue)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    receivers = receivers.Where(rec => rec.Name.ToUpper().Contains(name.ToUpper()));
                }

                if (!string.IsNullOrEmpty(document))
                {
                    receivers = receivers.Where(rec => rec.Documents.Any(doc => doc.Value.Contains(document)));
                }

                return receivers.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Get all error: {ex.Message}");
                throw;
            }
        }

        public ReceiverModel Get(long id)
        {
            _logger.LogDebug("Get by id");
            try
            {
                var receiver = _context.Set<ReceiverModel>()
                    .Include(x => x.Documents)
                    .Include(x => x.Address)
                    .AsNoTracking()
                    .Where(x => x.DeletedAt == DateTime.MinValue)
                    .FirstOrDefault(x => x.Id == id);

                return receiver;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Get by id ${id} error: {ex.Message}");
                throw;
            }
        }

        public int Create(List<ReceiverModel> receivers)
        {
            _logger.LogDebug("Create");
            try
            {
                var registeredSuccessfully = 0;
                _context.Set<ReceiverModel>().AddRange(receivers);
                registeredSuccessfully = _context.SaveChanges();

                return registeredSuccessfully;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Create error: {ex.Message}");
                throw;
            }
        }

        public ReceiverModel Modify(ReceiverModel receiver)
        {
            _logger.LogDebug("Modify");
            try
            {
                var currentReceiver = Get(receiver.Id);

                if (currentReceiver == null)
                {
                    throw new Exception("Receiver was not found");
                }

                currentReceiver.Name = receiver.Name;
                currentReceiver.AddressId = receiver.AddressId;

                _context.Set<ReceiverModel>().Update(currentReceiver);

                _context.SaveChanges();

                return currentReceiver;
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
                var receiver = Get(id);

                receiver.DeletedAt = DateTime.Now;

                var receiversRemoved = _context.SaveChanges();

                return receiversRemoved;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Delete error: {ex.Message}");
                throw;
            }
        }
    }
}
