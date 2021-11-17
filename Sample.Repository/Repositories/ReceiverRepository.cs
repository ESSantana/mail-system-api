using MailSystem.Core.Entities.Models;
using MailSystem.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sample.Repository.Context;
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
            _logger.LogDebug("Get");
            try
            {
                var receivers = _context.Set<ReceiverModel>()
                    .AsNoTracking()
                    .Where(x => x.DeletedAt == DateTime.MinValue)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    receivers = receivers.Where(x => x.Name == name);
                }

                if (!string.IsNullOrEmpty(document))
                {
                    receivers = receivers.Where(x => x.Document == document);
                }

                return receivers.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get error: {ex.Message}", ex);
                throw;
            }
        }

        public ReceiverModel Get(long Id)
        {
            _logger.LogDebug("Get");
            try
            {
                var receiver = _context.Set<ReceiverModel>()
                    .AsNoTracking()
                    .Where(x => x.DeletedAt == DateTime.MinValue)
                    .FirstOrDefault(x => x.Id == Id);

                return receiver;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get by id ${Id} error: {ex.Message}", ex);
                throw;
            }
        }

        public int Create(List<ReceiverModel> entities)
        {
            _logger.LogDebug("Create");
            try
            {
                var receiverContext = _context.Set<ReceiverModel>();

                foreach (var entity in entities)
                {
                    receiverContext.Add(entity);
                }

                var entitiesCreated = _context.SaveChanges();

                return entitiesCreated;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create error: {ex.Message}", ex);
                throw;
            }
        }

        public ReceiverModel Modify(ReceiverModel entity)
        {
            _logger.LogDebug("Modify");
            try
            {
                var currentReceiver = Get(entity.Id);

                if (currentReceiver == null)
                {
                    throw new Exception("Receiver was not found");
                }

                currentReceiver.Name = entity.Name;
                currentReceiver.Document = entity.Document;
                currentReceiver.AddressId = entity.AddressId;
                currentReceiver.UpdatedAt = DateTime.Now;

                _context.Set<ReceiverModel>().Update(currentReceiver);

                _context.SaveChanges();

                return currentReceiver;
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
                var receiver = Get(Id);

                receiver.DeletedAt = DateTime.Now;

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
