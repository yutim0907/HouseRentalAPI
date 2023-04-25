using HouseRentalAPI.Models;
using HouseRentalAPI.Models.Repository;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using HouseRentalAPI.Controllers;
using HouseRentalAPI.Models.Interface;
using Microsoft.AspNetCore.Http;
using static HouseRentalAPI.Models.ReqeustBody;

namespace HouseRentalAPI.Tests
{
    [TestFixture]
    public class HouseRentalControllerTests
    {
        private HouseRentalController _houseRentalController;
        private Mock<IHouseRentalRepository> _mockHouseRentalRepository;
        [SetUp]
        public void Setup()
        {
            _mockHouseRentalRepository = new Mock<IHouseRentalRepository>();
            _houseRentalController = new HouseRentalController(_mockHouseRentalRepository.Object);
        }

        [Test]
        public void GetById_ExistingId_ReturnsCorrectHouseRentalPost()
        {
            // Arrange
            var postId = 1;
            var expectedPost = new HouseRentalPost
            {
                Id = postId,
                Title = "Test1",
                Description = "test description",
                City = "台北市",
                District = "南港區",
                Address = "三重路19-10號",
                Price = 180000,
                LeaseDuration = 5
            };
            _mockHouseRentalRepository.Setup(repo => repo.GetHouseRentalById(postId)).Returns(expectedPost);

            // Act
            var result = _houseRentalController.Get(postId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(expectedPost));
        }
        [Test]
        public void GetAllHouseRentalPosts_ReturnsOkResult()
        {
            // Arrange
            var expectedPosts = new List<HouseRentalPost>
            {
                new HouseRentalPost 
                { 
                    Id = 1, 
                    Title = "Test Post 1",
                    Description = "test description", 
                    Address = "Address 1",
                    City = "台北市", 
                    District ="信義區",
                    Price = 200000,
                    LeaseDuration = 5
                },
                new HouseRentalPost
                {
                    Id = 2,
                    Title = "Test Post 2",
                    Description = "test description 2",
                    Address = "Address 2",
                    City = "新北市",
                    District ="板橋區",
                    Price = 100000,
                    LeaseDuration = 2
                },
                new HouseRentalPost 
                {
                    Id = 3,
                    Title = "Test Post 3",
                    Description = "test description 3",
                    Address = "Address 3",
                    City = "台南市",
                    District ="安平區",
                    Price = 50000,
                    LeaseDuration = 1
                }
            };
            _mockHouseRentalRepository.Setup(repo => repo.HouseRentalPosts()).Returns(expectedPosts);

            // Act
            var result = _houseRentalController.Get();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(expectedPosts));
        }
        [Test]
        public void Get_ReturnsNotFound_WhenPostsNotExist()
        {
            // Arrange
            _mockHouseRentalRepository.Setup(repo => repo.HouseRentalPosts()).Returns(new List<HouseRentalPost>());

            // Act
            var result = _houseRentalController.Get();

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
        [Test]
        public void Get_ReturnsInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            _mockHouseRentalRepository.Setup(repo => repo.HouseRentalPosts()).Throws(new Exception("Test Exception"));

            // Act
            var result = _houseRentalController.Get();

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var statusResult = result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusResult.StatusCode);
        }
        [Test]
        public void GetByCity_WithValidCityName_ReturnsOkResult()
        {
            // Arrange
            var cityName = "台北市";
            var expectedPosts = new List<HouseRentalPost>
            {
                new HouseRentalPost
                {
                    Id = 1,
                    Title = "Test Post 1",
                    Description = "test description",
                    Address = "Address 1",
                    City = "台北市",
                    District ="信義區",
                    Price = 200000,
                    LeaseDuration = 5
                },
                new HouseRentalPost
                {
                    Id = 2,
                    Title = "Test Post 2",
                    Description = "test description 2",
                    Address = "Address 2",
                    City = "新北市",
                    District ="板橋區",
                    Price = 100000,
                    LeaseDuration = 2
                },
                new HouseRentalPost
                {
                    Id = 3,
                    Title = "Test Post 3",
                    Description = "test description 3",
                    Address = "Address 3",
                    City = "台南市",
                    District ="安平區",
                    Price = 50000,
                    LeaseDuration = 1
                }
            };
            _mockHouseRentalRepository.Setup(repo => repo.GetHouseRentalsByCity(cityName)).Returns(expectedPosts);

            // Act
            var result = _houseRentalController.GetByCity(cityName);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(expectedPosts, okResult.Value);
        }
        [Test]
        public void GetByPrice_WithValidPriceRange_ReturnsOkResult()
        {
            // Arrange
            var minPrice = 100000;
            var maxPrice = 160000;
            var expectedPosts = new List<HouseRentalPost>
            {
                new HouseRentalPost
                {
                    Id = 1,
                    Title = "Test Post 1",
                    Description = "test description",
                    Address = "Address 1",
                    City = "台北市",
                    District ="信義區",
                    Price = 200000,
                    LeaseDuration = 5
                },
                new HouseRentalPost
                {
                    Id = 2,
                    Title = "Test Post 2",
                    Description = "test description 2",
                    Address = "Address 2",
                    City = "新北市",
                    District ="板橋區",
                    Price = 100000,
                    LeaseDuration = 2
                },
                new HouseRentalPost
                {
                    Id = 3,
                    Title = "Test Post 3",
                    Description = "test description 3",
                    Address = "Address 3",
                    City = "台南市",
                    District ="安平區",
                    Price = 50000,
                    LeaseDuration = 1
                }
            };
            _mockHouseRentalRepository.Setup(repo => repo.GetHouseRentalsByPrice(minPrice, maxPrice)).Returns(expectedPosts);

            // Act
            var result = _houseRentalController.GetByPrice(minPrice, maxPrice);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(expectedPosts, okResult.Value);
        }
        [Test]
        public void Post_WithValidModel_ReturnsOk()
        {
            // Arrange
            var model = new HouseRentalPostModel
            {
                Title = "Test Post",
                Description = "Test Description",
                Address = "Test Address",
                City = "台北市",
                District = "信義區",
                Price = 10000,
                LeaseDuration = 12
            };

            // Act
            var result = _houseRentalController.Post(model) as OkResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }
        [Test]
        public void Put_WithValidModel_ReturnsOk()
        {
            var id = 1;
            // Arrange
            var post = new HouseRentalPostModel
            {
                Id = id,
                Title = "測試貼文",
                Description = "測試敘述",
                Address = "地址",
                City = "台北市",
                District = "信義區",
                Price = 10000,
                LeaseDuration = 12
            };
            _mockHouseRentalRepository.Setup(repo => repo.AddHouseRentalPost(post));

            var editPost = new HouseRentalPostModel
            {
                Id = id,
                Title = "測試編輯",
                Description = "測試編輯",
                Address = "測試地址",
                City = "台北市",
                District = "大安區",
                Price = 20000,
                LeaseDuration = 24
            };
            _mockHouseRentalRepository.Setup(repo => repo.EditHouseRentalPost(editPost));
            // Act
            var result = _houseRentalController.Put(id, editPost) as OkResult;

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
        [Test]
        public void Put_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            var id = 1;
            var postModel = new HouseRentalPostModel { Id = 2 };
            _mockHouseRentalRepository.Setup(repo => repo.EditHouseRentalPost(It.IsAny<HouseRentalPostModel>()));

            // Act
            var result = _houseRentalController.Put(id, postModel);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }
        [Test]
        public void Delete_NonExistingPost_ReturnsNotFound()
        {
            // Arrange
            int postId = 4;
            _mockHouseRentalRepository.Setup(repo => repo.GetHouseRentalById(postId))
                           .Returns((HouseRentalPost)null);

            // Act
            var result = _houseRentalController.Delete(postId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
        [Test]
        public void Delete_ExistingPost_ReturnsOk()
        {
            var expectedPosts = new List<HouseRentalPost>
            {
                new HouseRentalPost
                {
                    Id = 1,
                    Title = "Test Post 1",
                    Description = "test description",
                    Address = "Address 1",
                    City = "台北市",
                    District ="信義區",
                    Price = 200000,
                    LeaseDuration = 5
                },
                new HouseRentalPost
                {
                    Id = 2,
                    Title = "Test Post 2",
                    Description = "test description 2",
                    Address = "Address 2",
                    City = "新北市",
                    District ="板橋區",
                    Price = 100000,
                    LeaseDuration = 2
                },
                new HouseRentalPost
                {
                    Id = 3,
                    Title = "Test Post 3",
                    Description = "test description 3",
                    Address = "Address 3",
                    City = "台南市",
                    District ="安平區",
                    Price = 50000,
                    LeaseDuration = 1
                }
            };
            // Arrange
            int postId = 1;
            _mockHouseRentalRepository.Setup(repo => repo.GetHouseRentalById(postId))
                           .Returns(expectedPosts.First(p => p.Id == postId));

            // Act
            var result = _houseRentalController.Delete(postId);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}