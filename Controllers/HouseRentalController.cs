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
        public IEnumerable<HouseRentalPost> Get()
        {
            return _houseRentalRepository.HouseRentalPosts();
        }

        // GET api/<HouseRentalController>/5
        [HttpGet("{id}")]
        public ActionResult<HouseRentalPost> Get(int id)
        {
            var result = _houseRentalRepository.SingleHouseRentalPost(id);
            if (result == null)
            {
                return NotFound("找不到租屋資料");
            }
            return result;
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
                return StatusCode(500, $"新增資料時發生錯誤: {ex.Message}");
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
                return StatusCode(500, $"更新資料時發生錯誤: {ex.Message}");
            }
        }

        // DELETE api/<HouseRentalController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _houseRentalRepository.SingleHouseRentalPost(id);
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
                return StatusCode(500, $"刪除資料時發生錯誤: {ex.Message}");
            }
        }
    }
}
