using Microsoft.AspNetCore.Mvc;
using ShopTARge22.Core.ServiceInterface;

namespace ShopTARge22.Controllers
{
    public class AccuWeatherForecastsController : Controller
    {
        private readonly IAccuWeatherForecastsServices _accuWeatherForecastsServices;

        public AccuWeatherForecastsController
            (IAccuWeatherForecastsServices accuWeatherForecastsServices)
        {
            _accuWeatherForecastsServices = accuWeatherForecastsServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchCity(AccuWeatherSearchCityViewModel vm)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "AccuWeatherForecasts", new { city = model.City });
            }
        }

    }
}