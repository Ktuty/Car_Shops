using CarShops.ViewModels;
using CarShops.Data;
using CarShops.Interfaces;
using CarShops.Models;
using Microsoft.AspNetCore.Mvc;
using CarShops.Repository;
using CarShops.Services;

namespace CarShops.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IPhotoService _photoService;
        public CarController(ICarRepository carRepository, IPhotoService photoService)
        {
            _carRepository = carRepository;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Car> cars = await _carRepository.GetALL();
            return View(cars);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Car car = await _carRepository.GetByIdAsync(id);
            return View(car);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCarViewModel carVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(carVM.Image);

                var car = new Car
                {
                    Name = carVM.Name,
                    ShortDesc = carVM.ShortDesc,
                    LongDesc = carVM.LongDesc,
                    Price = carVM.Price,
                    categoryID = carVM.categoryID,
                    Available = carVM.Available,
                    Image = result.Url.ToString()
                };
                _carRepository.Add(car);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload faild");
            }
            return View(carVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car == null) return View("Error");
            var carVM = new EditCarViewModel
            {
                Name = car.Name,
                ShortDesc = car.ShortDesc,
                LongDesc = car.LongDesc,
                Price = car.Price,
                Available = car.Available,
                categoryID = car.categoryID
            };
            return View(carVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCarViewModel carVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Faild to edit car");
                return View("Edit", carVM);
            }

            var userCar = await _carRepository.GetByIdAsyncNoTracking(id);

            if (userCar != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userCar.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delet photo");
                    return View(carVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(carVM.Image);

                var car = new Car
                {
                    Id = carVM.Id,
                    Name = carVM.Name,
                    ShortDesc = carVM.ShortDesc,
                    LongDesc = carVM.LongDesc,
                    Price = carVM.Price,
                    Available = carVM.Available,
                    Image = photoResult.Url.ToString()
                };

                _carRepository.Update(car);

                return RedirectToAction("Index");
            }
            else
            {
                return View(carVM);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _carRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");
            return View(clubDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var clubDetails = await _carRepository.GetByIdAsync(id);

            if (clubDetails == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(clubDetails.Image))
            {
                _ = _photoService.DeletePhotoAsync(clubDetails.Image);
            }

            _carRepository.Delete(clubDetails);
            return RedirectToAction("Index");
        }
    }
}
