using System;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using OSL.Inventory.B2.Service.DTOs;
using OSL.Inventory.B2.Service.DTOs.Enums;
using System.Collections.Generic;
using OSL.Inventory.B2.Service;
using OSL.Inventory.B2.Service.Extensions;

namespace OSL.Inventory.B2.Web.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _service;

        public SupplierController(ISupplierService service)
        {
            _service = service;
        }

        // GET: Supplier
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("Index")]
        public async Task<JsonResult> ListCustomersAsync(int draw, int start, int length,
            string searchByName, StatusDto filterByStatus = 0)
        {
            try
            {
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];

                var listProductsTuple = await _service
                    .ListSuppliersWithSortingFilteringPagingServiceAsync(start, length,
                    order, orderDir, searchByName, filterByStatus);

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

        // GET: Supplier/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entityDto = await _service.GetSupplierByIdServiceAsync(id);
            if (entityDto == null) return HttpNotFound();

            return View(entityDto);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FirstName,LastName,EmailAddress,PhoneNumber,Country,City,State,ZipCode")] SupplierDto entityDto)
        {
            try
            {
                if (!ModelState.IsValid) return View(entityDto);

                await _service.CreateSupplierServiceAsync(entityDto);
                TempData["message"] = $"'{entityDto.FirstName} {entityDto.LastName}' has been created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Supplier/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entitiyDto = await _service.GetSupplierByIdServiceAsync(id);
            if (entitiyDto == null)
            {
                return HttpNotFound();
            }
            return View(entitiyDto);
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,PhoneNumber,Country,City,State,ZipCode,CreatedAt,CreatedBy")] SupplierDto entityDto)
        {
            try
            {
                if (!ModelState.IsValid) return View(entityDto);

                await _service.UpdateSupplierServiceAsync(entityDto);
                TempData["message"] = $"'{entityDto.FirstName} {entityDto.LastName}' has been updated successfully!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Supplier/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                string productDeletePartial = "_SupplierDeletePartial";
                var entityDto = await _service.GetSupplierByIdServiceAsync(id);
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

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            try
            {
                var entityDto = await _service.GetSupplierByIdServiceAsync(id);
                if (entityDto == null)
                {
                    return HttpNotFound();
                }

                await _service.DeleteSupplierByIdServiceAsync(id);

                return Json(new { message = $"'{entityDto.FirstName} {entityDto.LastName}' has been deleted successfully!" });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
