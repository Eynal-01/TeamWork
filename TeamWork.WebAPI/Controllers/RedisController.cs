using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace TeamWork.WebAPI.Controllers
{
    public class RedisController : Controller
    {
        static readonly ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("redis-16532.c251.east-us-mz.azure.redns.redis-cloud.com:16532,password=kuLu8SrX5mvhtwYaSxf8TZNsl4fJFW96");

        public RedisController()
        {
            var db = redis.GetDatabase();
        }

        // GET: RedisController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RedisController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RedisController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RedisController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                // Example data from form
                string key = collection["key"];
                string value = collection["value"];

                // Get the Redis database
                var db = redis.GetDatabase();

                // Set the key-value pair in Redis
                await db.StringSetAsync(key, value);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RedisController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RedisController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RedisController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RedisController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
