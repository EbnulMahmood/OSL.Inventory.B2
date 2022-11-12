using System;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using OSL.Inventory.B2.Service.DTOs;
using OSL.Inventory.B2.Service;
using OSL.Inventory.B2.Service.DTOs.Enums;
using System.Collections.Generic;

namespace OSL.Inventory.B2.Web.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISaleService _service;

        public SaleController(ISaleService service)
        {
            _service = service;
        }

        // GET: Sale
        public async Task<ActionResult> Index()
        {
            var entities = await _service.SelectCustomerListItemsAsync();
            ViewBag.Customers = entities;
            return View();
        }

        [HttpPost, ActionName("Index")]
        public async Task<JsonResult> ListSalesAsync(int draw, int start, int length,
            string searchBySaleCode, Nullable<DateTime> dateFrom, Nullable<DateTime> dateTo,
            string filterByCustomer, StatusDto filterByStatus = 0)
        {
            try
            {
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];

                var listSalesTuple = await _service.ListSalesWithSortingFilteringPagingServiceAsync(start, length,
                    order, orderDir, searchBySaleCode, dateFrom, dateTo, filterByCustomer, filterByStatus);

                int totalRecord = listSalesTuple.Item2;
                int filterRecord = listSalesTuple.Item3;
                List<object> listSales = listSalesTuple.Item1;

                return Json(new
                {
                    draw,
                    recordsTotal = totalRecord,
                    recordsFiltered = filterRecord,
                    data = listSales
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Sale/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entityDto = await _service.GetSaleByIdServiceAsync(id);
            if (entityDto == null)
            {
                return HttpNotFound();
            }
            return View(entityDto);
        }

        [HttpGet]
        public JsonResult GetItemUnitPrice(long itemId)
        {
            try
            {
                var unitPrice = _service.GetProductUnitPriceService(itemId);
                return Json(unitPrice, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Sale/Create
        public async Task<ActionResult> Create()
        {
            // ViewBag.CustomerId = new SelectList(db.CustomerDtoes, "Id", "FirstName");

            ViewBag.Customers = await _service.SelectListCustomersServiceAsync();
            ViewBag.Products = await _service.SelectListProductsServiceAsync();
            return View();
        }

        // POST: Sale/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,SaleCode,SaleAmount,SaleDate,SaleAmountPaid,AmountPaidTime,CustomerId,Status,CreatedAt,ModifiedAt,CreatedBy,ModifiedBy")] SaleDto saleDto)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateSaleServiceAsync(saleDto);   
                return RedirectToAction("Index");
            }

            // ViewBag.CustomerId = new SelectList(db.CustomerDtoes, "Id", "FirstName", saleDto.CustomerId);
            return View(saleDto);
        }

        // GET: Sale/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entityDto = await _service.GetSaleByIdServiceAsync(id);
            if (entityDto == null)
            {
                return HttpNotFound();
            }
            // ViewBag.CustomerId = new SelectList(db.CustomerDtoes, "Id", "FirstName", saleDto.CustomerId);
            return View(entityDto);
        }

        // POST: Sale/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SaleCode,SaleAmount,SaleDate,SaleAmountPaid,AmountPaidTime,CustomerId,Status,CreatedAt,ModifiedAt,CreatedBy,ModifiedBy")] SaleDto saleDto)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateSaleServiceAsync(saleDto);
                return RedirectToAction("Index");
            }
            // ViewBag.CustomerId = new SelectList(db.CustomerDtoes, "Id", "FirstName", saleDto.CustomerId);
            return View(saleDto);
        }
    }
}
