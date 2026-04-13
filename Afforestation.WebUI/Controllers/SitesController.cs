using Afforestation.WebUI.Models.ViewModels;
using Afforestation.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Afforestation.WebUI.Controllers
{
    public class SitesController : Controller
    {
        private readonly ISiteService _siteService;

        public SitesController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        public async Task<IActionResult> Index()
        {
            var sites = await _siteService.GetAllAsync();
            return View(sites);
        }

        public async Task<IActionResult> Details(int id)
        {
            var site = await _siteService.GetByIdAsync(id);
            if (site == null) return NotFound();

            // get observations separately to ensure latest
            var observations = await _siteService.GetObservationsAsync(id);
            site.Observations = observations;

            return View(site);
        }

        public IActionResult Create()
        {
            return View(new SiteEditViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SiteEditViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var success = await _siteService.CreateAsync(model);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Unable to create site.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var site = await _siteService.GetByIdAsync(id);
            if (site == null) return NotFound();

            var edit = new SiteEditViewModel
            {
                Name = site.Name,
                Latitude = site.Latitude,
                Longitude = site.Longitude,
                City = site.City,
                District = site.District,
                PlantingData = site.PlantingData,
                Status = site.Status
            };

            return View(edit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SiteEditViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var success = await _siteService.UpdateAsync(id, model);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Unable to update site.");
                return View(model);
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _siteService.DeleteAsync(id);
            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Map()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> MapData()
        {
            var data = await _siteService.GetMapDataAsync();
            return Json(data);
        }
    }
}
