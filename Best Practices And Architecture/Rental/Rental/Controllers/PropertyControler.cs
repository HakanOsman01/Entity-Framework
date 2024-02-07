using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Rental.Core.Contracts;

namespace Rental.Controllers
{
    public class PropertyControler : Controller
    {
        private readonly IProperyService properyService;
        public PropertyControler(IProperyService properyService)
        {
            this.properyService = properyService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new Core.Models.PropertyModel();

            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Create(Core.Models.PropertyModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            await properyService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

    }
}
