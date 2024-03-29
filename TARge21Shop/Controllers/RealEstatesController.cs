﻿using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using TARge21Shop.ApplicationServices.Services;
using TARge21Shop.Core.Dto;
using TARge21Shop.Core.ServiceInterface;
using TARge21Shop.Data;
using TARge21Shop.Models.RealEstate;


namespace TARge21Shop.Controllers
{
    public class RealEstatesController : Controller
    {
        private readonly IRealEstatesServices _realEstatesServices;
        private readonly TARge21ShopContext _context;


        public RealEstatesController
            (
            TARge21ShopContext context,
            IRealEstatesServices realEstates
            )
        {
            _realEstatesServices = realEstates;
            _context = context;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.RealEstates
                .OrderByDescending(y => y.CreatedAt)
                .Select(x => new RealEstateIndexViewModel
                {
                    Id = x.Id,
                    Address = x.Address,
                    City = x.City,
                    Country = x.Country,
                    Size = x.Size,
                    Price = x.Price
                });
            

            

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel vm = new();

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Address = vm.Address,
                City = vm.City,
                Country = vm.Country,
                Size = vm.Size,
                Price = vm.Price,
                Floor = vm.Floor,
                Region = vm.Region,
                Phone = vm.Phone,
                Fax = vm.Fax,
                PostalCode = vm.PostalCode,
                RoomCount = vm.RoomCount,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };

            var result = await _realEstatesServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index", vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var realEstate = await _realEstatesServices.GetAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            //var photos = await _context.FileToDatabase
            //    .Where(x => x.SpaceshipId == id)
            //    .Select(y => new ImageViewModel
            //    {
            //        SpaceshipId = y.Id,
            //        ImageId = y.Id,
            //        ImageData = y.ImageData,
            //        ImageTitle = y.ImageTitle,
            //        Image = string.Format("data:image/gif;base64, {0}", Convert.ToBase64String(y.ImageData))
            //    }).ToArrayAsync();

            var vm = new RealEstateCreateUpdateViewModel();

            vm.Id = realEstate.Id;
            vm.Address = realEstate.Address;
            vm.City = realEstate.City;
            vm.Country = realEstate.Country;
            vm.Size = realEstate.Size;
            vm.Price = realEstate.Price;
            vm.Floor = realEstate.Floor;
            vm.Region = realEstate.Region;
            vm.Phone = realEstate.Phone;
            vm.Fax = realEstate.Fax;
            vm.PostalCode = realEstate.PostalCode;
            vm.RoomCount = realEstate.RoomCount;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.ModifiedAt = realEstate.ModifiedAt;
            //vm.Image.AddRange(photos);

            return View("CreateUpdate", vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var realEstate = await _realEstatesServices.GetAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            //var photos = await _context.FileToDatabase
            //    .Where(x => x.SpaceshipId == id)
            //    .Select(y => new ImageViewModel
            //    {
            //        SpaceshipId = y.Id,
            //        ImageId = y.Id,
            //        ImageData = y.ImageData,
            //        ImageTitle = y.ImageTitle,
            //        Image = string.Format("data:image/gif;base64, {0}", Convert.ToBase64String(y.ImageData))
            //    }).ToArrayAsync();

            var vm = new RealEstateDeleteViewModel();


            vm.Id = realEstate.Id;
            vm.Address = realEstate.Address;
            vm.City = realEstate.City;
            vm.Country = realEstate.Country;
            vm.Size = realEstate.Size;
            vm.Price = realEstate.Price;
            vm.Floor = realEstate.Floor;
            vm.Region = realEstate.Region;
            vm.Phone = realEstate.Phone;
            vm.Fax = realEstate.Fax;
            vm.PostalCode = realEstate.PostalCode;
            vm.RoomCount = realEstate.RoomCount;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.ModifiedAt = realEstate.ModifiedAt;
            //vm.Image.AddRange(photos);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var realEstateId = await _realEstatesServices.Delete(id);

            if (realEstateId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
