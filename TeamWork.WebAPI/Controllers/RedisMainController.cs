using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TeamWork.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisMainController : ControllerBase
    {
        static readonly ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("redis-16532.c251.east-us-mz.azure.redns.redis-cloud.com:16532,password=kuLu8SrX5mvhtwYaSxf8TZNsl4fJFW96");
        private readonly IDatabase db;

        public RedisMainController()
        {
            db = redis.GetDatabase();
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            var userData = await db.StringGetAsync(id);
            if (!userData.IsNullOrEmpty)
            {
                var user = JsonConvert.DeserializeObject<User>(userData);
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User user)
        {
            try
            {
                string userJson = JsonConvert.SerializeObject(user);

                await db.StringSetAsync(user.Id, userJson);

                return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] User user)
        {
            try
            {
                var existingUser = await db.StringGetAsync(id);
                if (existingUser.IsNullOrEmpty)
                {
                    return NotFound();
                }

                string userJson = JsonConvert.SerializeObject(user);
                await db.StringSetAsync(id, userJson);

                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
