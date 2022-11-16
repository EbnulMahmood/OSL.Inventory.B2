using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using OSL.Inventory.B2.Service.DTOs;
using OSL.Inventory.B2.Service.DTOs.Enums;
using OSL.Inventory.B2.Service;
using OSL.Inventory.B2.Service.Extensions;

namespace OSL.Inventory.B2.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        // GET: Customer
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
                    .ListCustomersWithSortingFilteringPagingServiceAsync(start, length,
                    order, orderDir, searchByName, filterByStatus);

                int totalRecord = listProductsTuple.Item2;
                int filterRecord = listProductsTuple.Item3;
                IEnumerable<CustomerDatatableViewDto> listProducts = listProductsTuple.Item1;

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

        // GET: Customer/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entityDto = await _service.GetCustomerByIdServiceAsync(id);
            if (entityDto == null) return HttpNotFound();

            return View(entityDto);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FirstName,LastName,EmailAddress,PhoneNumber,Country,City,State,ZipCode")] CustomerDto entityDto)
        {
            try
            {
                if (!ModelState.IsValid) return View(entityDto);

                await _service.CreateCustomerServiceAsync(entityDto);
                TempData["message"] = $"'{entityDto.FirstName} {entityDto.LastName}' has been created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Customer/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entitiyDto = await _service.GetCustomerByIdServiceAsync(id);
            if (entitiyDto == null)
            {
                return HttpNotFound();
            }
            return View(entitiyDto);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,PhoneNumber,Country,City,State,ZipCode,CreatedAt,CreatedBy")] CustomerDto entityDto)
        {
            try
            {
                if (!ModelState.IsValid) return View(entityDto);

                await _service.UpdateCustomerServiceAsync(entityDto);
                TempData["message"] = $"'{entityDto.FirstName} {entityDto.LastName}' has been updated successfully!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Customer/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                string productDeletePartial = "_CustomerDeletePartial";
                var entityDto = await _service.GetCustomerByIdServiceAsync(id);
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

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            try
            {
                var entityDto = await _service.GetCustomerByIdServiceAsync(id);
                if (entityDto == null)
                {
                    return HttpNotFound();
                }

                await _service.DeleteCustomerByIdServiceAsync(id);

                return Json(new { message = $"'{entityDto.FirstName} {entityDto.LastName}' has been deleted successfully!" });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
