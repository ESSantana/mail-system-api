using AutoMapper;
using MailSystem.API.Controllers;
using MailSystem.API.DTO;
using MailSystem.Core.Entities;
using MailSystem.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Sample.Test.Configuration.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Test.ControllerTests
{
    public class DeliveryControllerTest
    {
        private DeliveryController controller;
        private Mock<IDeliveryService> service;

        [SetUp]
        public void Setup()
        {
            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILogger<DeliveryController>>();
            service = new Mock<IDeliveryService>();

            service.Setup(r => r.Get(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(DeliveryMockResult.Get());
            service.Setup(r => r.Get(It.IsAny<long>())).Returns(DeliveryMockResult.Get().First());
            service.Setup(r => r.Create(It.IsAny<List<Delivery>>())).Returns(1);
            service.Setup(r => r.Modify(It.IsAny<Delivery>())).Returns(DeliveryMockResult.Get().First());
            service.Setup(r => r.Delete(It.IsAny<long>())).Returns(1);

            controller = new DeliveryController(service.Object, logger.Object, mapper.Object);
        }

        [Test]
        public void Get_ShouldReturn_ListOfDeliveryDTO()
        {
            var result = controller.Get("Mock trackCode","Mock Type", null);

            Assert.Multiple(() =>
            {
                service.Verify(s => s.Get("Mock trackCode", "Mock Type", null), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
            });
        }

        [Test]
        public void Get_ShouldReturn_NoContentResult()
        {
            service.Setup(r => r.Get("Mock trackCode", "Mock Type", null)).Returns(new List<Delivery>());

            var result = controller.Get("Mock trackCode", "Mock Type", null);

            Assert.Multiple(() =>
            {
                service.Verify(s => s.Get("Mock trackCode", "Mock Type", null), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(NoContentResult), result.Result);
            });
        }

        [Test]
        public void GetById_ShouldReturn_DeliveryDTO()
        {
            var result = controller.Get(1);

            Assert.Multiple(() =>
            {
                service.Verify(s => s.Get(It.IsAny<long>()), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
            });
        }

        [Test]
        public void GetByID_ShouldReturn_NoContentResult()
        {
            service.Setup(r => r.Get(It.IsAny<long>())).Returns((Delivery)null);

            var result = controller.Get(1);

            Assert.Multiple(() =>
            {
                service.Verify(s => s.Get(It.IsAny<long>()), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(NoContentResult), result.Result);
            });
        }

        [Test]
        public void Create_ShouldReturn_TotalResult()
        {
            var entity = new List<DeliveryDTO>
            {
                new DeliveryDTO
                {
                    Description = "Mock Description"
                }
            };

            var result = controller.Create(entity);

            Assert.Multiple(() =>
            {
                service.Verify(s => s.Create(It.IsAny<List<Delivery>>()), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
            });
        }

        [Test]
        public void Create_ShouldReturn_NoContentResult()
        {
            service.Setup(r => r.Create(It.IsAny<List<Delivery>>())).Returns(0);

            var entity = new List<DeliveryDTO>
            {
                new DeliveryDTO
                {
                    Description = "Mock Description"
                }
            };

            var result = controller.Create(entity);

            Assert.Multiple(() =>
            {
                service.Verify(s => s.Create(It.IsAny<List<Delivery>>()), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(NoContentResult), result.Result);
            });
        }

        [Test]
        public void Modify_ShouldReturn_DeliveryDTO()
        {
            var entity = new DeliveryDTO
            {
                Id = 1,
                Description = "Mock Description"
            };

            var result = controller.Modify(entity);

            Assert.Multiple(() =>
            {
                service.Verify(s => s.Modify(It.IsAny<Delivery>()), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
            });
        }

        [Test]
        public void Modify_ShouldReturn_NoContentResult()
        {
            service.Setup(r => r.Modify(It.IsAny<Delivery>())).Returns((Delivery) null);

            var entity = new DeliveryDTO
            {
                Id = 1,
                Description = "Mock Description"
            };

            var result = controller.Modify(entity);

            Assert.Multiple(() =>
            {
                service.Verify(s => s.Modify(It.IsAny<Delivery>()), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(NoContentResult), result.Result);
            });
        }

        [Test]
        public void Delete_ShouldReturn_TotalDeleted()
        {
            var result = controller.Delete(1);

            Assert.Multiple(() =>
            {
                service.Verify(s => s.Delete(It.IsAny<long>()), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
            });
        }

        [Test]
        public void Delete_ShouldReturn_NoContentResult()
        {
            service.Setup(r => r.Delete(It.IsAny<long>())).Returns(0);

            var result = controller.Delete(1);

            Assert.Multiple(() =>
            {
                service.Verify(s => s.Delete(It.IsAny<long>()), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(NoContentResult), result.Result);
            });
        }
    }
}
