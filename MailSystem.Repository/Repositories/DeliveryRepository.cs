using MailSystem.Core.Entities.Models;
using MailSystem.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MailSystem.Repository.Context;
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
            _logger.LogDebug("Get all");
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
                _logger.LogError(ex, $"Get all error: {ex.Message}");
                throw;
            }
        }

        public DeliveryModel Get(long id)
        {
            _logger.LogDebug("Get by id");
            try
            {
                var delivery = _context.Set<DeliveryModel>()
                    .AsNoTracking()
                    .Where(x => x.DeletedAt == DateTime.MinValue)
                    .FirstOrDefault(x => x.Id == id);

                return delivery;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Get by id ${id} error: {ex.Message}");
                throw;
            }
        }

        public int Create(List<DeliveryModel> deliveries)
        {
            _logger.LogDebug("Create");
            try
            {
                _context.Set<DeliveryModel>().AddRange(deliveries);

                var deliveriesCreated = _context.SaveChanges();

                return deliveriesCreated;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Create error: {ex.Message}");
                throw;
            }
        }

        public DeliveryModel Modify(DeliveryModel delivery)
        {
            _logger.LogDebug("Modify");
            try
            {
                var currentDelivery = Get(delivery.Id);

                if (currentDelivery == null)
                {
                    throw new Exception("Delivery was not found");
                }

                currentDelivery.Description = delivery.Description;
                currentDelivery.ReceiverId = delivery.ReceiverId;
                currentDelivery.TrackCode = delivery.TrackCode;
                currentDelivery.Type = delivery.Type;
                currentDelivery.DeliveredTo = delivery.DeliveredTo;
                currentDelivery.ArrivedAt = delivery.ArrivedAt;
                currentDelivery.DeliveredAt = delivery.DeliveredAt;

                _context.Set<DeliveryModel>().Update(currentDelivery);

                _context.SaveChanges();

                return currentDelivery;
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
                var delivery = Get(id);

                delivery.DeletedAt = DateTime.Now;

                var deliveriesRemoved = _context.SaveChanges();

                return deliveriesRemoved;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Delete error: {ex.Message}");
                throw;
            }
        }
    }
}
