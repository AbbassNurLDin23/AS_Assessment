using AS_Assessment.DTOs;
using AS_Assessment.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AS_Assessment.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: /Category
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        // GET: /Category/Add
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View(new CreateCategoryDTO());
        }

        // POST: /Category/Add
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CreateCategoryDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _categoryService.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Category/Edit/{id}
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();

            var dto = new UpdateCategoryDTO { Name = category.Name };
            ViewBag.CategoryId = id;
            return View(dto);
        }

        // POST: /Category/Edit/{id}
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UpdateCategoryDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = id;
                return View(dto);
            }

            await _categoryService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        // POST: /Category/Delete/{id}
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            await _categoryService.SoftDeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
