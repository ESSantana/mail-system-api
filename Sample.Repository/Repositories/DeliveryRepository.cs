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
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly MailSystemDbContext _context;
        private readonly ILogger<DeliveryRepository> _logger;

        public DeliveryRepository(MailSystemDbContext context, ILogger<DeliveryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<DeliveryModel> Get(string trackCode, string type, DateTime? arrivedDate)
        {
            _logger.LogDebug("Get");
            try
            {
                var deliveries = _context.Set<DeliveryModel>()
                    .AsNoTracking()
                    .Where(x => x.DeletedAt == DateTime.MinValue)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(trackCode))
                {
                    deliveries = deliveries.Where(x => x.TrackCode == trackCode);
                }

                if (!string.IsNullOrEmpty(type))
                {
                    deliveries = deliveries.Where(x => x.Type == type);
                }

                if (arrivedDate != null && arrivedDate != DateTime.MinValue)
                {
                    deliveries = deliveries.Where(x => x.ArrivedAt >= arrivedDate);
                }

                return deliveries.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get error: {ex.Message}", ex);
                throw;
            }
        }

        public DeliveryModel Get(long Id)
        {
            _logger.LogDebug("Get");
            try
            {
                var delivery = _context.Set<DeliveryModel>()
                    .AsNoTracking()
                    .Where(x => x.DeletedAt == DateTime.MinValue)
                    .FirstOrDefault(x => x.Id == Id);

                return delivery;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get by id ${Id} error: {ex.Message}", ex);
                throw;
            }
        }

        public int Create(List<DeliveryModel> entities)
        {
            _logger.LogDebug("Create");
            try
            {
                var deliveryContext = _context.Set<DeliveryModel>();

                foreach (var entity in entities)
                {
                    deliveryContext.Add(entity);
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

        public DeliveryModel Modify(DeliveryModel entity)
        {
            _logger.LogDebug("Modify");
            try
            {
                var currentDelivery = Get(entity.Id);

                if (currentDelivery == null)
                {
                    throw new Exception("Delivery was not found");
                }

                currentDelivery.Description = entity.Description;
                currentDelivery.ReceiverId = entity.ReceiverId;
                currentDelivery.TrackCode = entity.TrackCode;
                currentDelivery.Type = entity.Type;
                currentDelivery.DeliveredTo = entity.DeliveredTo;
                currentDelivery.ArrivedAt = entity.ArrivedAt;
                currentDelivery.DeliveredAt = entity.DeliveredAt;

                _context.Set<DeliveryModel>().Update(currentDelivery);

                _context.SaveChanges();

                return currentDelivery;
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
                var delivery = Get(Id);

                delivery.DeletedAt = DateTime.Now;

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
