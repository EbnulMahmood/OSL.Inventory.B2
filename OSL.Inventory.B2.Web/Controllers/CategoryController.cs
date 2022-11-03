using OSL.Inventory.B2.Service.DTOs;
using OSL.Inventory.B2.Service.Extensions;
using OSL.Inventory.B2.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using OSL.Inventory.B2.Service.DTOs.Enums;

namespace OSL.Inventory.B2.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpPost, ActionName("Index")]
        public async Task<JsonResult> ListCategoriesAsync(int draw, int start, int length,
            string filter_keywords, StatusDto filter_option = 0)
        {
            string order = Request.Form.GetValues("order[0][column]")[0];
            string orderDir = Request.Form.GetValues("order[0][dir]")[0];
            /*
            //get total count of data in table
            totalRecord = entities.Count();

            if (!string.IsNullOrEmpty(filter_keywords))
            {
                entities = entities.Where(d => d.Name.ToLower().Contains(filter_keywords.ToLower()))
                .Where(d => d.Status != StatusDto.Deleted);
            }
            if (filter_option != 0)
            {
                entities = entities.Where(d => d.Status == filter_option)
                .Where(d => d.Status != StatusDto.Deleted);
            }

            // Sorting.   
            entities = SortByColumnWithOrder(order, orderDir, entities);

            // get total count of records after search 
            filterRecord = entities.Count();

            //pagination
            IEnumerable<CategoryDto> paginatdEntities = entities.Skip(start).Take(length)
                .OrderByDescending(d => d.CreatedAt).ToList()
                .Where(d => d.Status != StatusDto.Deleted);
            */
            var tuple = await _service.ListCategoriesWithSortingFilteringPagingServiceAsync(start, length, order, orderDir,
                filter_keywords, filter_option);

            var paginatdEntities = tuple.Item1;
            /*
            var entities = await _service.ListCategoriesServiceAsync();
            */
            int totalRecord = tuple.Item2;
            int filterRecord = tuple.Item3;

            List<object> entitiesList = new List<object>();
            foreach (var item in paginatdEntities)
            {
                string actionLink = $"<div class='btn-toolbar w-30' role='toolbar'>" +
                    $"<a href='Category/Edit/{item.Id}' class='btn btn-primary btn-sm mx-auto'><i class='bi bi-pencil-square'></i>Edit</a>" +
                    $"<button type='button' data-bs-target='#deleteCategory' data-bs-toggle='ajax-modal' class='btn btn-danger btn-sm mx-auto btn-category-delete'" +
                    $"data-category-id='{item.Id}'><i class='bi bi-trash-fill'></i>Delete</button><a href='Category/Details/{item.Id}'" +
                    $"class='btn btn-secondary btn-sm mx-auto'><i class='bi bi-ticket-detailed-fill'></i>Details</a></div>";

                string statusConditionClass = item.Status == StatusDto.Active ? "text-success" : "text-danger";
                string statusConditionText = item.Status == StatusDto.Active ? "Active" : "Inactive";
                string status = $"<span class='{statusConditionClass}'>{statusConditionText}</span>";

                List<string> dataItems = new List<string>
                {
                    item.Name,
                    status,
                    actionLink
                };

                entitiesList.Add(dataItems);
            }

            return Json(new
            {
                draw,
                recordsTotal = totalRecord,
                recordsFiltered = filterRecord,
                data = entitiesList
            });
        }

        private IEnumerable<CategoryDto> SortByColumnWithOrder(string order, string orderDir, IEnumerable<CategoryDto> data)
        {
            // Initialization.   
            IEnumerable<CategoryDto> sortedEntities = Enumerable.Empty<CategoryDto>();
            try
            {
                // Sorting   
                switch (order)
                {
                    case "0":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.Name).ToList() : data.OrderBy(p => p.Name).ToList();
                        break;
                    case "1":
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? 
                            data.OrderByDescending(p => p.Status).ToList() : 
                            data.OrderBy(p => p.Status).ToList();
                        break;
                    default:
                        // Setting.   
                        sortedEntities = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? 
                            data.OrderByDescending(p => p.Name).ToList() : 
                            data.OrderBy(p => p.Name).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                // info.   
                Console.Write(ex);
            }
            // info.   
            return sortedEntities;
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

        // GET: Category
        public ActionResult Index()
        {
            return View();
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
                IDictionary<string, string> errors = _service.ValidateCategoryDtoService(categoryDto);
                
                if (errors.Count > 0) ModelState.MergeError(errors);
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
                IDictionary<string, string> errors = _service.ValidateCategoryDtoService(categoryDto);

                if (errors.Count > 0) ModelState.MergeError(errors);
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