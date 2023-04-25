using static HouseRentalAPI.Models.ReqeustBody;

namespace HouseRentalAPI.Models.Interface
{
    public interface IHouseRentalRepository
    {
        List<HouseRentalPost> HouseRentalPosts();
        HouseRentalPost? GetHouseRentalById(int id);
        void AddHouseRentalPost(HouseRentalPostModel model);
        void EditHouseRentalPost(HouseRentalPostModel model);
        void DeleteHouseRentalPost(int id);
        List<HouseRentalPost> GetHouseRentalsByCity(string cityName);
        List<HouseRentalPost> GetHouseRentalsByPrice(int minPrice, int maxPrice);
    }
}
