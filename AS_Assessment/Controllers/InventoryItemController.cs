using AS_Assessment.DTOs;
using AS_Assessment.Enums;
using AS_Assessment.Models;
using AS_Assessment.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace AS_Assessment.Controllers
{
    public class InventoryItemController : Controller
    {
        private readonly IInventoryItemService _inventoryService;
        private readonly ICategoryService _categoryService;

        public InventoryItemController(IInventoryItemService inventoryService, ICategoryService categoryService)
        {
            _inventoryService = inventoryService;
            _categoryService = categoryService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string? searchName, string? categoryId, StockStatus? stockStatus, string? viewMode)
        {
            bool isAuthenticated = User.Identity?.IsAuthenticated ?? false;
            bool isAdmin = User.IsInRole("Admin");

            IEnumerable<InventoryItemReadDto> items;

            if (isAdmin)
            {
                // Admin: show all items
                items = await _inventoryService.GetAllWithCategoryAsync();
            }
            else if (isAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (viewMode == "MyItems")
                {
                    items = await _inventoryService.GetByUserAsync(userId);
                }
                else
                {
                    items = await _inventoryService.GetAllWithCategoryAsync();
                }
            }
            else
            {
                // Anonymous: show all items, no "My Items" filter
                items = await _inventoryService.GetAllWithCategoryAsync();
            }

            // Filter by searchName
            if (!string.IsNullOrEmpty(searchName))
            {
                items = items.Where(i => i.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase));
            }

            // Filter by categoryId
            if (!string.IsNullOrEmpty(categoryId))
            {
                items = items.Where(i => i.CategoryId == categoryId);
            }

            // Filter by stockStatus
            if (stockStatus.HasValue)
            {
                items = items.Where(i => i.StockStatus == stockStatus.Value);
            }

            ViewData["IsAdmin"] = isAdmin;
            ViewData["IsAuthenticated"] = isAuthenticated;
            ViewData["CurrentUserId"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["SearchName"] = searchName ?? "";
            ViewData["CategoryId"] = categoryId ?? "";
            ViewData["StockStatus"] = stockStatus;
            ViewData["ViewMode"] = viewMode ?? "AllItems";

            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", categoryId);

            return View(items);
        }

        [Authorize]
        public async Task<IActionResult> Add()
        {
            var categories = await _categoryService.GetAllAsync();  // Await async call

            // Use SelectList with Name as value and text
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(InventoryItemCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync();  // Await here too
                ViewBag.Categories = new SelectList(categories, "Name", "Name");
                return View(dto);
            }

            if (string.IsNullOrEmpty(dto.UserId))
                dto.UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var createdItem = await _inventoryService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }



        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var item = await _inventoryService.GetByIdAsync(id);
            if (item == null) return NotFound();

            // Manual mapping from ReadDto to UpdateDto
            var updateDto = new InventoryItemUpdateDto
            {
                Id = item.Id,
                Name = item.Name,
                Quantity = item.Quantity,
                WhatNeeded = item.WhatNeeded,
                CategoryId = item.CategoryId,
                UserId = item.UserId
                // Add any other properties that exist in both DTOs
            };

            return View(updateDto);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(InventoryItemUpdateDto dto)
        {
            dto.UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!ModelState.IsValid) return View(dto);

            var updated = await _inventoryService.UpdateAsync(dto);
            if (updated == null) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            var item = await _inventoryService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _inventoryService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent(); // For API call, return 204 No Content
        }
    }
}
