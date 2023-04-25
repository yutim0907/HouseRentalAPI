using HouseRentalAPI.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static HouseRentalAPI.Models.ReqeustBody;

namespace HouseRentalAPI.Models.Repository
{
    public class HouseRentalRepository : IHouseRentalRepository
    {
        private readonly HouseRentalDbContext _houseRentalContext;
        public HouseRentalRepository(HouseRentalDbContext houseRentalContext)
        {
            _houseRentalContext = houseRentalContext;
        }

        public List<HouseRentalPost> HouseRentalPosts()
        {
            var result = _houseRentalContext.HouseRentalPosts.ToList();
            return result;
        }
        public List<HouseRentalPost> GetHouseRentalsByCity(string cityName)
        {
            return _houseRentalContext.HouseRentalPosts.Where(h => h.City == cityName).ToList();
        }
        public List<HouseRentalPost> GetHouseRentalsByPrice(int minPrice, int maxPrice)
        {
            return _houseRentalContext.HouseRentalPosts.Where(h => h.Price >= minPrice && h.Price <= maxPrice).ToList();
        }
        public HouseRentalPost? GetHouseRentalById(int id)
        {
            var result = _houseRentalContext.HouseRentalPosts.Where(x => x.Id == id).FirstOrDefault();
            if(result == null)
            {
                return null;
            }
            return result;
        }
        public void AddHouseRentalPost(HouseRentalPostModel model)
        {
            var dateTimeNow = DateTime.Now;
            var newPost = new HouseRentalPost
            {
                Title = model.Title,
                Description = model.Description,
                City = model.City,
                District = model.District,
                Address = model.Address,
                Price = model.Price,
                LeaseDuration = model.LeaseDuration,
                CreatedDate = dateTimeNow,
                ModifiedDate = dateTimeNow,
            };
            try
            {
                _houseRentalContext.Add(newPost);
                _houseRentalContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void EditHouseRentalPost(HouseRentalPostModel model)
        {
            var dateTimeNow = DateTime.Now;
            var selectHouseRentalPost = GetHouseRentalById(model.Id);
            if (selectHouseRentalPost != null)
            {
                selectHouseRentalPost.Title = model.Title;
                selectHouseRentalPost.Description = model.Description;
                selectHouseRentalPost.City = model.City;
                selectHouseRentalPost.District = model.District;
                selectHouseRentalPost.Address = model.Address;
                selectHouseRentalPost.Price = model.Price;
                selectHouseRentalPost.LeaseDuration = model.LeaseDuration;
                selectHouseRentalPost.ModifiedDate = dateTimeNow;
                try
                {
                    _houseRentalContext.Update(selectHouseRentalPost);
                    _houseRentalContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public void DeleteHouseRentalPost(int id)
        {
            var deletePost = _houseRentalContext.HouseRentalPosts.Find(id);
            if(deletePost != null)
            {
                try
                {
                    _houseRentalContext.HouseRentalPosts.Remove(deletePost);
                    _houseRentalContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }
    }
}
