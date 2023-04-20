using Microsoft.AspNetCore.Mvc;

namespace HouseRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseRentalController : ControllerBase
    {
        // GET: api/<HouseRentalController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<HouseRentalController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HouseRentalController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HouseRentalController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HouseRentalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
