using Microsoft.AspNetCore.Mvc;
using ShopTARge22.Core.ServiceInterface;
using ShopTARge22.Models.Forecast;

namespace ShopTARge22.Controllers
{
    public class WeatherForecastsController : Controller
    {
        private readonly IWeatherForecastServices _weatherForecastServices;

        public WeatherForecastsController
            (
            IWeatherForecastServices weatherForecastServices
            )
        {
            _weatherForecastServices = weatherForecastServices;
        }


        [HttpGet]
        public IActionResult SearchCity()
        {
            SearchCityViewModel model = new();

            return View(model);
        }

        [HttpPost]
        public IActionResult SearchCity(SearchCityViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "WeatherForecasts", new { city = model.CityName });
            }

            return View(model);
        }

        public IActionResult City(string city)
        {



            return View();
        }
    }
}
