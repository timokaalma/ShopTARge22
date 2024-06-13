using Microsoft.AspNetCore.Mvc;
using ShopTARge22.Core.ServiceInterface;
using ShopTARge22.Data;
using ShopTARge22.Models.RealEstates;
using ShopTARge22.Core.Dto;
using Microsoft.EntityFrameworkCore;

namespace ShopTARge22.Controllers
{
    public class RealEstatesController : Controller
    {
        private readonly ShopTARge22Context _context;
        private readonly IRealEstatesServices _realEstateServices;
        private readonly IFileServices _fileServices;

        public RealEstatesController
        (
                ShopTARge22Context context,
                IRealEstatesServices realEstateServices,
                IFileServices fileServices
            )
        {
            _context = context;
            _realEstateServices = realEstateServices;
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
                    BuildingType = x.BuildingType,
                    BuiltInYear = (DateTime)x.BuiltInYear
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel result = new RealEstateCreateUpdateViewModel();

            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
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
                Files = vm.Files,
                Image = vm.Image
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.ImageId,
                        ImageData = x.ImageData,
                        ImageTitle = x.ImageTitle,
                        RealEstateId = x.RealEstateId
                    }).ToArray()
            };

            var result = await _realEstateServices.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var realEstate = await _realEstateServices.DetailsAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            var images = await _context.FileToDatabases
               .Where(x => x.RealEstateId == id)
               .Select(y => new RealEstateImageViewModel
               {
                   RealEstateId = y.Id,
                   ImageId = y.Id,
                   ImageData = y.ImageData,
                   ImageTitle = y.ImageTitle,
                   Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
               }).ToArrayAsync();

            var vm = new RealEstateCreateUpdateViewModel();

            vm.Id = realEstate.Id;
            vm.Address = realEstate.Address;
            vm.SizeSqrM = realEstate.SizeSqrM;
            vm.RoomCount = realEstate.RoomCount;
            vm.Floor = realEstate.Floor;
            vm.BuildingType = realEstate.BuildingType;
            vm.BuiltInYear = (DateTime)realEstate.BuiltInYear;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.UpdatedAt = realEstate.UpdatedAt;
            vm.Image.AddRange(images);

            return View("CreateUpdate", vm);

        }

        [HttpPost]
        public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
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
                Files = vm.Files,
                Image = vm.Image
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.ImageId,
                        ImageData = x.ImageData,
                        ImageTitle = x.ImageTitle,
                        RealEstateId = x.RealEstateId,
                    }).ToArray()
            };

            var result = await _realEstateServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var realEstate = await _realEstateServices.DetailsAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            var images = await _context.FileToDatabases
                .Where(x => x.RealEstateId == id)
                .Select(y => new RealEstateImageViewModel
                {
                    RealEstateId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new RealEstateDetailsViewModel();

            vm.Id = realEstate.Id;
            vm.Address = realEstate.Address;
            vm.SizeSqrM = realEstate.SizeSqrM;
            vm.RoomCount = realEstate.RoomCount;
            vm.Floor = realEstate.Floor;
            vm.BuildingType = realEstate.BuildingType;
            vm.BuiltInYear = (DateTime)realEstate.BuiltInYear;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.UpdatedAt = realEstate.UpdatedAt;
            vm.Image.AddRange(images);

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var realEstate = await _realEstateServices.DetailsAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            var images = await _context.FileToDatabases
                .Where(x => x.RealEstateId == id)
                .Select(y => new RealEstateImageViewModel
                {
                    RealEstateId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new RealEstateDeleteViewModel();

            vm.Id = realEstate.Id;
            vm.Address = realEstate.Address;
            vm.SizeSqrM = realEstate.SizeSqrM;
            vm.RoomCount = realEstate.RoomCount;
            vm.Floor = realEstate.Floor;
            vm.BuildingType = realEstate.BuildingType;
            vm.BuiltInYear = (DateTime)realEstate.BuiltInYear;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.UpdatedAt = realEstate.UpdatedAt;
            vm.Image.AddRange(images);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var realEstate = await _realEstateServices.Delete(id);

            if (realEstate == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImage(RealEstateImageViewModel vm)
        {
            var dto = new FileToDatabaseDto()
            {
                Id = vm.ImageId
            };

            var image = await _fileServices.RemoveFilesFromDatabase(dto);

            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

    }
}