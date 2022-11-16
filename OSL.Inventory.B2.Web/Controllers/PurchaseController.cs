using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using OSL.Inventory.B2.Service.DTOs;
using OSL.Inventory.B2.Service;
using OSL.Inventory.B2.Service.DTOs.Enums;
using System.Collections.Generic;
using System;

namespace OSL.Inventory.B2.Web.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _service;

        public PurchaseController(IPurchaseService service)
        {
            _service = service;
        }

        // GET: Purchase
        public async Task<ActionResult> Index()
        {
            try
            {
                var entities = await _service.ListSuppliersIdNameServiceAsync();
                ViewBag.Suppliers = entities;
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost, ActionName("Index")]
        public async Task<JsonResult> ListPurchasesAsync(int draw, int start, int length,
            string searchByPurchaseCode, Nullable<DateTime> dateFrom, Nullable<DateTime> dateTo,
            string filterBySupplier, StatusDto filterByStatus = 0)
        {
            try
            {
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];

                var listPurchasesTuple = await _service
                    .ListPurchasesWithSortingFilteringPagingServiceAsync(start, length,
                    order, orderDir, searchByPurchaseCode, dateFrom, dateTo, filterBySupplier, filterByStatus);

                int totalRecord = listPurchasesTuple.Item2;
                int filterRecord = listPurchasesTuple.Item3;
                IEnumerable<PurchaseDatatableViewDto> listPurchases = listPurchasesTuple.Item1;

                return Json(new
                {
                    draw,
                    recordsTotal = totalRecord,
                    recordsFiltered = filterRecord,
                    data = listPurchases
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Purchase/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var entityDto = await _service.GetPurchaseByIdServiceAsync(id);
                if (entityDto == null)
                {
                    return HttpNotFound();
                }
                return View(entityDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Purchase/Create
        public async Task<ActionResult> Create()
        {
            try
            {

                ViewBag.Suppliers = await _service.SelectListSuppliersServiceAsync();
                ViewBag.Products = await _service.SelectListProductsServiceAsync();

                PurchaseDto purchaseDto = new PurchaseDto()
                {
                    PurchaseDetails = new List<PurchaseDetailDto>() { new PurchaseDetailDto() },
                };
                return View(purchaseDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Purchase/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PurchaseDto purchaseDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.CreatePurchaseServiceAsync(purchaseDto);
                    return RedirectToAction("Index");
                }

                // ViewBag.SupplierId = new SelectList(db.SupplierDtoes, "Id", "FirstName", purchaseDto.SupplierId);
                return View(purchaseDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Purchase/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var entityDto = await _service.GetPurchaseByIdServiceAsync(id);
                if (entityDto == null)
                {
                    return HttpNotFound();
                }
                // ViewBag.SupplierId = new SelectList(db.SupplierDtoes, "Id", "FirstName", purchaseDto.SupplierId);
                return View(entityDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Purchase/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,PurchaseCode,PurchaseAmount,PurchaseDate,PurchaseAmountPaid,AmountPaidTime,SupplierId")] PurchaseDto purchaseDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.UpdatePurchaseServiceAsync(purchaseDto);
                    return RedirectToAction("Index");
                }
                // ViewBag.SupplierId = new SelectList(db.SupplierDtoes, "Id", "FirstName", purchaseDto.SupplierId);
                return View(purchaseDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
