using HouseRentalAPI.Models;
using HouseRentalAPI.Models.Interface;
using HouseRentalAPI.Models.Repository;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using static HouseRentalAPI.Models.ReqeustBody;

namespace HouseRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseRentalController : ControllerBase
    {
        private readonly IHouseRentalRepository _houseRentalRepository;
        public HouseRentalController(IHouseRentalRepository houseRentalRepository)
        {
            _houseRentalRepository = houseRentalRepository;
        }
        // GET: api/<HouseRentalController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var posts = _houseRentalRepository.HouseRentalPosts();

                if (posts == null || !posts.Any())
                {
                    return NotFound();
                }

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"取得資料時發生錯誤: {ex.Message}");
            }
        }

        // GET api/<HouseRentalController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _houseRentalRepository.GetHouseRentalById(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"取得資料時發生錯誤: {ex.Message}");
            }
        }

        [HttpGet("city/{cityName}")]
        public IActionResult GetByCity(string cityName)
        {
            try
            {
                var posts = _houseRentalRepository.GetHouseRentalsByCity(cityName);

                if (posts == null || !posts.Any())
                {
                    return NotFound();
                }

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"取得資料時發生錯誤: {ex.Message}");
            }
        }

        [HttpGet("price/{minPrice}/{maxPrice}")]
        public IActionResult GetByPrice(int minPrice, int maxPrice)
        {
            try
            {
                var posts = _houseRentalRepository.GetHouseRentalsByPrice(minPrice, maxPrice);

                if (posts == null || !posts.Any())
                {
                    return NotFound();
                }

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"取得資料時發生錯誤: {ex.Message}");
            }
        }

        // POST api/<HouseRentalController>
        [HttpPost]
        public IActionResult Post([FromBody] HouseRentalPostModel model)
        {
            try
            {
                _houseRentalRepository.AddHouseRentalPost(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"新增資料時發生錯誤: {ex.Message}");
            }
        }

        // PUT api/<HouseRentalController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, HouseRentalPostModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            try
            {
                _houseRentalRepository.EditHouseRentalPost(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"更新資料時發生錯誤: {ex.Message}");
            }
        }

        // DELETE api/<HouseRentalController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _houseRentalRepository.GetHouseRentalById(id);
            if (result == null)
            {
                return NotFound("找不到租屋資料");
            }
            try
            {
                _houseRentalRepository.DeleteHouseRentalPost(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"刪除資料時發生錯誤: {ex.Message}");
            }
        }
    }
}
