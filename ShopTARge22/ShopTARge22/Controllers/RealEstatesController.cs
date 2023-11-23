using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARge22.ApplicationServices.Services;
using ShopTARge22.Core.Domain;
using ShopTARge22.Core.Dto;
using ShopTARge22.Core.ServiceInterface;
using ShopTARge22.Data;
using ShopTARge22.Models.RealEstates;
using ShopTARge22.Models.Spaceships;

namespace ShopTARge22.Controllers
{
    public class RealEstatesController : Controller
    {
        private readonly ShopTARge22Context _context;
        private readonly IRealEstates _realEstates;
        private readonly IFileServices _fileServices;

        public RealEstatesController
        (
                ShopTARge22Context context,
                IRealEstates realEstates
            )
        {
            _context = context;
            _realEstates = realEstates;
            _fileServices = fileServices;
        }

        public IActionResult Index()
        {
            var result = _context.RealEstates
                .Select(x => new RealEstatesIndexViewModel
                {
                    Id = x.Id,
                    Address = x.Address,
                    SizeSqrM = x.SizeSqrM,
                    RoomCount = x.RoomCount,
                    Floor = x.Floor,
                    BuildingType = x.BuildingType,
                    BuiltInYear = x.BuiltInYear,
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RealEstatesCreateUpdateViewModel result = new RealEstatesCreateUpdateViewModel();

            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealEstatesCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Address = vm.Address,
                SizeSqrM = vm.SizeSqrM,
                RoomCount = vm.RoomCount,
                Floor = vm.Floor,
                BuildingType = vm.BuildingType,
                BuiltInYear = vm.BuiltInYear,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt
            };


            var result = await _realEstates.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var realEstate = await _realEstates.DetailsAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            var vm = new RealEstatesCreateUpdateViewModel();

            vm.Id = realEstate.Id;
            vm.Address = realEstate.Address;
            vm.SizeSqrM = realEstate.SizeSqrM;
            vm.RoomCount = realEstate.RoomCount;
            vm.Floor = realEstate.Floor;
            vm.BuildingType = realEstate.BuildingType;
            vm.BuiltInYear = realEstate.BuiltInYear;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.UpdatedAt = realEstate.UpdatedAt;

            return View("CreateUpdate", vm);

        }

        [HttpPost]
        public async Task<IActionResult> Update(RealEstatesCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Address = vm.Address,
                SizeSqrM = vm.SizeSqrM,
                RoomCount = vm.RoomCount,
                Floor = vm.Floor,
                BuildingType = vm.BuildingType,
                BuiltInYear = vm.BuiltInYear,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
            };

            var result = await _realEstates.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var realEstate = await _realEstates.DetailsAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            var vm = new RealEstatesDetailsViewModel();

            vm.Id = realEstate.Id;
            vm.Address = realEstate.Address;
            vm.SizeSqrM = realEstate.SizeSqrM;
            vm.RoomCount = realEstate.RoomCount;
            vm.Floor = realEstate.Floor;
            vm.BuildingType = realEstate.BuildingType;
            vm.BuiltInYear = realEstate.BuiltInYear;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.UpdatedAt = realEstate.UpdatedAt;

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var realEstate = await _realEstates.DetailsAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }


            var vm = new RealEstatesDeleteViewModel();

            vm.Id = realEstate.Id;
            vm.Address = realEstate.Address;
            vm.SizeSqrM = realEstate.SizeSqrM;
            vm.RoomCount = realEstate.RoomCount;
            vm.Floor = realEstate.Floor;
            vm.BuildingType = realEstate.BuildingType;
            vm.BuiltInYear = realEstate.BuiltInYear;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.UpdatedAt = realEstate.UpdatedAt;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var realEstate = await _realEstates.Delete(id);

            if (realEstate == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

    }

}
