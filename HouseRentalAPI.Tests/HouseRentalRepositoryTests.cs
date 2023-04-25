
using HouseRentalAPI.Models.Repository;
using HouseRentalAPI.Models;
using Microsoft.EntityFrameworkCore;
using static HouseRentalAPI.Models.ReqeustBody;

namespace HouseRentalAPI.Tests
{
    [TestFixture]
    public class HouseRentalRepositoryTests
    {
        private HouseRentalDbContext _dbContext;
        private HouseRentalRepository _repository;
        [SetUp]
        public void Setup()
        {
            string dbConnectingString = "Server=127.0.0.1;Database=HouseRentalDb;Trusted_Connection=True;TrustServerCertificate=True;User ID=sa;Password=Fmcs@23145632";
            // 初始化測試用的資料庫
            var options = new DbContextOptionsBuilder<HouseRentalDbContext>()
                .UseSqlServer(dbConnectingString)
                .Options;
            _dbContext = new HouseRentalDbContext(options);

            // 初始化要測試的 Repository
            _repository = new HouseRentalRepository(_dbContext);
        }
        [Test]
        public void HouseRentalPosts_ShouldReturnAllPosts()
        {
            // Act
            var result = _repository.HouseRentalPosts();

            // Assert
            Assert.AreNotEqual(0, result.Count);
        }
        [Test]
        public void GetHouseRentalsByCity_ShouldReturnPostsInCity()
        {
            // Act
            var result = _repository.GetHouseRentalsByCity("台北市");

            // Assert
            Assert.AreNotEqual(0, result.Count);
        }
        [Test]
        public void GetHouseRentalsByPrice_ShouldReturnPostsInPriceRange()
        {
            // Act
            var result = _repository.GetHouseRentalsByPrice(0, 100000);
            // Assert
            Assert.AreNotEqual(0, result.Count);
        }
        [Test]
        public void GetHouseRentalById_ShouldReturnSinglePost()
        {
            // Act
            var result = _repository.GetHouseRentalById(6);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("桃園市中壢區雅房", result.Title);
        }
        [Test]
        public void AddHouseRentalPost_ShouldAddNewPost()
        {
            // Arrange
            var newPost = new HouseRentalPostModel
            {
                Title = "測試新增貼文 By Unit Test",
                Description = "用於測試貼文用 By Unit Test",
                City = "南投縣",
                Address = "測試地址",
                Price = 999999,
                District = "南投區",
                LeaseDuration = 999,
            };

            // Act
            _repository.AddHouseRentalPost(newPost);
            var result = _repository.HouseRentalPosts();

            // Assert
            Assert.GreaterOrEqual(result.Count, 1);
            Assert.AreEqual("測試新增貼文 By Unit Test", result.Last().Title);
        }
        [Test]
        public void EditHouseRentalPost_ShouldModifyPost()
        {
            var allPosts = _repository.HouseRentalPosts();
            var testPostToEdit = allPosts.Where(x => x.Title.Contains("測試新增")).FirstOrDefault();
            if(testPostToEdit == null)
            {
                // Arrange
                var newPost = new HouseRentalPostModel
                {
                    Title = "測試新增貼文 By Unit Test",
                    Description = "用於測試貼文用 By Unit Test",
                    City = "南投縣",
                    Address = "測試地址",
                    Price = 999999,
                    District = "南投區",
                    LeaseDuration = 999,
                };

                // Act
                _repository.AddHouseRentalPost(newPost);
                testPostToEdit = allPosts.Where(x => x.Title.Contains("測試新增")).FirstOrDefault();
            }
            // Arrange
            var editPost = new HouseRentalPostModel
            {
                Id = testPostToEdit.Id,
                Title = "測試編輯貼文 By Unit Test",
                Description = "用於測試編輯貼文用 By Unit Test",
                City = "桃園市",
                District = "桃園區",
                Address = "330桃園市桃園區文中路168號",
                Price = 123456,
                LeaseDuration = 72
            };

            // Act
            _repository.EditHouseRentalPost(editPost);
            var result = _repository.GetHouseRentalById(testPostToEdit.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("測試編輯貼文 By Unit Test", result.Title);
            Assert.AreEqual(123456, result.Price);
            Assert.AreEqual("桃園區", result.District);
        }
        [Test]
        public void DeleteHouseRentalPost_ShouldRemovePost()
        {
            var allPosts = _repository.HouseRentalPosts();
            var testPostToDelete = allPosts.Where(x => x.Title.Contains("測試")).FirstOrDefault();
            if(testPostToDelete == null)
            {
                // Arrange
                var newPost = new HouseRentalPostModel
                {
                    Title = "測試用刪除貼文 By Unit Test",
                    Description = "測試用刪除貼文 By Unit Test",
                    City = "南投縣",
                    District = "南投區",
                    Address = "測試地址",
                    Price = 999999,
                    LeaseDuration = 999,
                };

                // Act
                _repository.AddHouseRentalPost(newPost);
                testPostToDelete = allPosts.Where(x => x.Title.Contains("測試")).FirstOrDefault();
                allPosts = _repository.HouseRentalPosts();
            }
            // Act
            _repository.DeleteHouseRentalPost(testPostToDelete.Id);
            var resultPosts = _repository.HouseRentalPosts();
            Assert.Greater(allPosts.Count, resultPosts.Count);
        }
    }
}
