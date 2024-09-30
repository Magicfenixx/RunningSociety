using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly ICloudService _cloudService;
        public ClubController(IClubRepository clubRepository, ICloudService cloudService)
        {
            _clubRepository = clubRepository;
            _cloudService = cloudService;
        }
        public async Task<IActionResult> Index()
        {
            var clubs = await _clubRepository.GetAll();
            return View(clubs);
        }
        public async Task<IActionResult> Details(int id)
        {
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        //CREATING
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _cloudService.AddPhotoAsync(clubVM.Image);

                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    ClubCategory = clubVM.ClubCategory,
                    Adress = new Adress
                    {
                        Street = clubVM.Adress.Street,
                        City = clubVM.Adress.City,
                        State = clubVM.Adress.State,
                    }
                };
                _clubRepository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(clubVM);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);
            if (club == null) return View("Error");
            var clubVM = new EditClubViewModel()
            {
                Title = club.Title,
                Description = club.Description,
                AdressId = club.AdressId,
                Adress = club.Adress,
                URL = club.Image,
                ClubCategory = club.ClubCategory
            };
            return View(clubVM);

        }

        [HttpPost]
        //EDITING
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", " Edit Error");
                return View("Edit", clubVM);
            }
            var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);
            if (userClub != null)
            {
                try
                {
                    var fi = new FileInfo(userClub.Image);
                    var publicId = Path.GetFileNameWithoutExtension(fi.Name);
                    await _cloudService.DeletePhotoAsync(publicId);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", " Counldn't delete photo");
                    return View(clubVM);
                }
                var PhotoResult = await _cloudService.AddPhotoAsync(clubVM.Image);

                var club = new Club()
                {
                    Id = id,
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = PhotoResult.Url.ToString(),
                    AdressId = clubVM.AdressId,
                    Adress = clubVM.Adress,
                    ClubCategory = clubVM.ClubCategory
                };

                _clubRepository.Update(club);

                return RedirectToAction("Index");
            }
            else
            {
                return View(clubVM);
            }
        }
    }
}
