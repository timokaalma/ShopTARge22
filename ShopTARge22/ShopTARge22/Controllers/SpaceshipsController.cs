using Microsoft.AspNetCore.Mvc;
using ShopTARge22.Core.Dto;
using ShopTARge22.Core.ServiceInterface;
using ShopTARge22.Data;
using ShopTARge22.Models.Spaceships;

namespace ShopTARge22.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly ShopTARge22Context _context;
        private readonly ISpaceshipsServices _spaceshipServices;

        public SpaceshipsController
            (
                ShopTARge22Context context,
                ISpaceshipsServices spaceshipsServices
            )
        {
            _context = context;
            _spaceshipServices = spaceshipsServices;
        }


        public IActionResult Index()
        {
            var result = _context.Spaceships
                .Select(x => new SpaceshipsIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type,
                    BuiltDate = x.BuiltDate,
                    Passengers = x.Passengers
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            SpaceshipCreateViewModel result = new SpaceshipCreateViewModel();

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpaceshipCreateViewModel vm)
        {
            var dto = new SpaceshipDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Type = vm.Type,
                Passengers = vm.Passengers,
                BuiltDate = vm.BuiltDate,
                CargoWeight = vm.CargoWeight,
                Crew = vm.Crew,
                EnginePower = vm.EnginePower,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };

            var result = await _spaceshipServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }
    }
}
