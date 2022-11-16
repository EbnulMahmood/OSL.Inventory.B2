using OSL.Inventory.B2.Service.DTOs;
using OSL.Inventory.B2.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using OSL.Inventory.B2.Service.DTOs.Enums;
using OSL.Inventory.B2.Service;

namespace OSL.Inventory.B2.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("Index")]
        public async Task<JsonResult> ListCategoriesAsync(int draw, int start, int length,
            string searchByName, StatusDto filterByStatus = 0)
        {
            try
            {
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];

                var listCategoriesTuple = await _service
                    .ListCategoriesWithSortingFilteringPagingServiceAsync(start, length,
                    order, orderDir, searchByName, filterByStatus);

                int totalRecord = listCategoriesTuple.Item2;
                int filterRecord = listCategoriesTuple.Item3;
                IEnumerable<CategoryDatatableViewDto> listCategories = listCategoriesTuple.Item1;

                return Json(new
                {
                    draw,
                    recordsTotal = totalRecord,
                    recordsFiltered = filterRecord,
                    data = listCategories
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        private IEnumerable<SelectListItem> FilterStatusDto()
        {
             return Enum.GetValues(typeof(StatusDto))
                        .Cast<StatusDto>()
                        .Where(e => e != StatusDto.Deleted)
                        .Select(e => new SelectListItem
                        {
                            Value = ((int)e).ToString(),
                            Text = e.ToString()
                        });
        }
         
        // GET: Category/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            try
            {
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                var entityDto = await _service.GetCategoryByIdServiceAsync(id);
                if (entityDto == null) return HttpNotFound();
                
                return View(entityDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }
         
        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description")]
            CategoryDto categoryDto)
        {
            try
            {
                if (!ModelState.IsValid) return View(categoryDto);

                await _service.CreateCategoryServiceAsync(categoryDto);

                TempData["message"] = $"'{categoryDto.Name}' has been created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }
         
        // GET: Category/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var categoryDto = await _service.GetCategoryByIdServiceAsync(id);
                if (categoryDto == null)
                {
                    return HttpNotFound();
                }
                ViewBag.SelectList = FilterStatusDto();
                return View(categoryDto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,Status,CreatedAt,CreatedBy")]
            CategoryDto categoryDto)
        {
            try
            {
                if (!ModelState.IsValid) return View(categoryDto);

                await _service.UpdateCategoryServiceAsync(categoryDto);
                TempData["message"] = $"'{categoryDto.Name}' has been updated successfully!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Category/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                string categoryDeletePartial = "_CategoryDeletePartial";
                var categoryDto = await _service.GetCategoryByIdServiceAsync(id);
                if (categoryDto == null)
                {
                    return HttpNotFound();
                }
                return PartialView(categoryDeletePartial, categoryDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            try
            {
                var categoryDto = await _service.GetCategoryByIdServiceAsync(id);
                if (categoryDto == null)
                {
                    return HttpNotFound();
                }

                await _service.DeleteCategoryByIdServiceAsync(id);

                return Json(new { message = $"'{categoryDto.Name}' has been deleted successfully!" });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}