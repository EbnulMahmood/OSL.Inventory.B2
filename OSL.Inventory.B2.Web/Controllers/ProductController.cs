using System;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using OSL.Inventory.B2.Service.DTOs;
using OSL.Inventory.B2.Service.Interfaces;
using OSL.Inventory.B2.Service.DTOs.Enums;
using System.Collections.Generic;
using System.Linq;
using OSL.Inventory.B2.Service.Extensions;

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
            return View();
        }

        [HttpPost, ActionName("Index")]
        public async Task<JsonResult> ListProductsAsync(int draw, int start, int length,
            string searchByName, StatusDto filterByStatus = 0)
        {
            try
            {
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];

                var listProductsTuple = await _service.ListProductsWithSortingFilteringPagingServiceAsync(start, length, order, orderDir,
                    searchByName, filterByStatus);

                IEnumerable<ProductDto> listProducts = listProductsTuple.Item1;

                int totalRecord = listProductsTuple.Item2;
                int filterRecord = listProductsTuple.Item3;

                List<object> entitiesList = new List<object>();
                foreach (var item in listProducts)
                {
                    string actionLink = $"<div class='btn-toolbar w-30' role='toolbar'>" +
                        $"<a href='Product/Edit/{item.Id}' class='btn btn-primary btn-sm mx-auto'><i class='bi bi-pencil-square'></i>Edit</a>" +
                        $"<button type='button' data-bs-target='#deleteProduct' data-bs-toggle='ajax-modal' class='btn btn-danger btn-sm mx-auto btn-product-delete'" +
                        $"data-product-id='{item.Id}'><i class='bi bi-trash-fill'></i>Delete</button><a href='Product/Details/{item.Id}'" +
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,ImageUrl,Limited,InStock,PricePerUnit,BasicUnit,CategoryName")] ProductDto entityDto)
        {
            try
            {
                IDictionary<string, string> errors = _service.ValidateProductDtoService(entityDto);

                if (errors.Count > 0) ModelState.MergeError(errors);
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
                ViewBag.SelectList = FilterStatusDto();
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
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,ImageUrl,Limited,InStock,PricePerUnit,BasicUnit,CategoryName,Status,CreatedAt,CreatedBy")] ProductDto entityDto)
        {
            try
            {
                IDictionary<string, string> errors = _service.ValidateProductDtoService(entityDto);

                if (errors.Count > 0) ModelState.MergeError(errors);
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
