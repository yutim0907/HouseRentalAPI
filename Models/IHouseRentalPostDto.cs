namespace HouseRentalAPI.Models
{
    public interface IHouseRentalPostDto
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Address { get; set; }

        public decimal Price { get; set; }

        public int LeaseDuration { get; set; }

    }
}
