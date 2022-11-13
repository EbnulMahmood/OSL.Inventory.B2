using System;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using OSL.Inventory.B2.Service.DTOs;
using OSL.Inventory.B2.Service.DTOs.Enums;
using System.Collections.Generic;
using System.Linq;
using OSL.Inventory.B2.Service.Extensions;
using OSL.Inventory.B2.Service;

namespace OSL.Inventory.B2.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        // GET: Product
        public ActionResult Index()
        {
            var entities = _service.SelectCategoriesListItems();
            ViewBag.Categories = entities;
            return View();
        }

        [HttpGet, ActionName("SearchCategoriesSelect")]
        public async Task<JsonResult> SearchCategoriesSelectAsync(string term, int page)
        {
            int resultCount = 5;
            var entities = await _service.ListCategoriesServiceAsync(term, page, resultCount);

            return Json(entities, JsonRequestBehavior.AllowGet );
        }

        [HttpPost, ActionName("Index")]
        public async Task<JsonResult> ListProductsAsync(int draw, int start, int length,
            string searchByName, string filterByCategory, StatusDto filterByStatus = 0)
        {
            try
            {
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];

                var listProductsTuple = await _service
                    .ListProductsWithSortingFilteringPagingServiceAsync(start, length,
                    order, orderDir, searchByName, filterByCategory, filterByStatus);

                int totalRecord = listProductsTuple.Item2;
                int filterRecord = listProductsTuple.Item3;
                List<object> listProducts = listProductsTuple.Item1;

                return Json(new
                {
                    draw,
                    recordsTotal = totalRecord,
                    recordsFiltered = filterRecord,
                    data = listProducts
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet, ActionName("CategoriesDropdown")]
        public async Task<JsonResult> ListCategoriesDropdownAsync(string term)
        {
            Console.WriteLine("controller search key -> ", term);
            var entities = await _service.ListCategoriesByNameServiceAsync(term);
            var entitiesSelectList = new SelectList(entities, "Id", "Name");

            return Json(new { data = entitiesSelectList }, JsonRequestBehavior.AllowGet);
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

        private IEnumerable<SelectListItem> FilterBasicUnitDto()
        {
            return Enum.GetValues(typeof(BasicUnitDto))
                       .Cast<BasicUnitDto>()
                       .Select(e => new SelectListItem
                       {
                           Value = ((int)e).ToString(),
                           Text = e.ToString()
                       });
        }

        private async Task<IEnumerable<SelectListItem>> FilterCategoriesDto()
        {
            var categories = await _service.ListCategoriesAsync();
            return categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            });
        }

        private async Task<SelectList> SelectListCategories()
        {
            var categories = await _service.ListCategoriesAsync();
            return new SelectList(categories, "Id", "Name");
        }

        // GET: Product/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            try
            {
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                var entityDto = await _service.GetProductByIdServiceAsync(id);
                if (entityDto == null) return HttpNotFound();

                return View(entityDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Product/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Categories = await SelectListCategories();
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Description,ImageUrl,Limited,InStock,PricePerUnit,BasicUnit,CategoryId")] ProductDto entityDto)
        {
            try
            {
                if (!ModelState.IsValid) return View(entityDto);

                await _service.CreateProductServiceAsync(entityDto);
                TempData["message"] = $"'{entityDto.Name}' has been created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var entitiyDto = await _service.GetProductByIdServiceAsync(id);
                if (entitiyDto == null)
                {
                    return HttpNotFound();
                }
                ViewBag.SelectStatusList = FilterStatusDto();
                ViewBag.SelectBasicUnitList = FilterBasicUnitDto();
                ViewBag.SelectCategoriesList = await FilterCategoriesDto();
                return View(entitiyDto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,ImageUrl,Limited,InStock,PricePerUnit,Status,BasicUnit,CategoryId,CreatedAt,CreatedBy")] ProductDto entityDto)
        {
            try
            {
                if (!ModelState.IsValid) return View(entityDto);

                await _service.UpdateProductServiceAsync(entityDto);
                TempData["message"] = $"'{entityDto.Name}' has been updated successfully!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Product/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                string productDeletePartial = "_ProductDeletePartial";
                var entityDto = await _service.GetProductByIdServiceAsync(id);
                if (entityDto == null)
                {
                    return HttpNotFound();
                }
                return PartialView(productDeletePartial, entityDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            try
            {
                var entityDto = await _service.GetProductByIdServiceAsync(id);
                if (entityDto == null)
                {
                    return HttpNotFound();
                }

                await _service.DeleteProductByIdServiceAsync(id);

                return Json(new { message = $"'{entityDto.Name}' has been deleted successfully!" });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
