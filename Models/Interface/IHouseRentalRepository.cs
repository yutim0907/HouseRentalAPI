using static HouseRentalAPI.Models.ReqeustBody;

namespace HouseRentalAPI.Models.Interface
{
    public interface IHouseRentalRepository
    {
        public List<HouseRentalPost> HouseRentalPosts();
        HouseRentalPost? SingleHouseRentalPost(int id);
        void AddHouseRentalPost(HouseRentalPostModel model);
        void EditHouseRentalPost(HouseRentalPostModel model);
        void DeleteHouseRentalPost(int id);
    }
}
