using Asparagus.Domain.Models;
using Asparagus.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asparagus
{

    public class FeedController : Controller
    {

        public FeedService FeedService;

        public FeedController(FeedService service)
        {
            this.FeedService = service;
        }

        // GET: HomeController
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MessageDto message)
        {
            try
            {
                FeedService.Add(message);
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult List() {

            return View(FeedService.GetFeed()); 
        }
    }
}
