using ImplementConsumeAPI.Models;
using ImplementConsumeAPI.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImplementConsumeAPI.Controllers
{
    [Authorize]
    public class UniversityController : Controller
    {
        private readonly IUniversityRepository repository;
        public UniversityController(IUniversityRepository repository)
        {
            this.repository = repository;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var universities = new List<University>();

            if (result.Data != null)
            {
                universities = result.Data?.Select(e => new University
                {
                    Guid = e.Guid,
                    Code = e.Code,
                    Name = e.Name,
                    CreatedDate = e.CreatedDate,
                    ModifiedDate = e.ModifiedDate
                }).ToList();
            }

            return View(universities);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Creates()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Creates(University university)
        {
           /* if (ModelState.IsValid)
            {*/
                var result = await repository.Post(university);
                if (result.StatusCode == "200")
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (result.StatusCode == "409")
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
            /*}*/
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(University university)
        {
           /* if (ModelState.IsValid)
            {*/
                var result = await repository.Put(university);
                if (result.StatusCode == "200")
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (result.StatusCode == "409")
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
           /* }*/
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Guid)
        {
            var result = await repository.Get(Guid);
            var university = new University();
            if (result.Data?.Guid is null)
            {
                return View(university);
            }
            else
            {
                university.Guid = result.Data.Guid;
                university.Code = result.Data.Code;
                university.Name = result.Data.Name;
                university.CreatedDate = result.Data.CreatedDate;
                university.ModifiedDate = result.Data.ModifiedDate;
            }

            return View(university);
        }

        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> Deletes(Guid Guid)
        {
            var result = await repository.Get(Guid);
            var university = new University();
            if (result.Data?.Code is null)
            {
                return View(university);
            }
            else
            {
                university.Guid = result.Data.Guid;
                university.Code = result.Data.Code;
                university.Name = result.Data.Name;
            }
            return View(university);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        public async Task<IActionResult> Remove(Guid Guid)
        {
            var result = await repository.Deletes(Guid);
            if (result.StatusCode == "200")
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
