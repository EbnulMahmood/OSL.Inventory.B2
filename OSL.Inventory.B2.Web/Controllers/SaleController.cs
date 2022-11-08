using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using OSL.Inventory.B2.Repository.Data;
using OSL.Inventory.B2.Service.DTOs;

namespace OSL.Inventory.B2.Web.Controllers
{
    public class SaleController : Controller
    {
        private InventoryDbContext db = new InventoryDbContext();

        // GET: Sale
        public async Task<ActionResult> Index()
        {
            var saleDtoes = db.SaleDtoes.Include(s => s.Customer);
            return View(await saleDtoes.ToListAsync());
        }

        // GET: Sale/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleDto saleDto = await db.SaleDtoes.FindAsync(id);
            if (saleDto == null)
            {
                return HttpNotFound();
            }
            return View(saleDto);
        }

        // GET: Sale/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.CustomerDtoes, "Id", "FirstName");
            return View();
        }

        // POST: Sale/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,SaleCode,SaleAmount,SaleDate,SaleAmountPaid,AmountPaidTime,ActionLinkHtml,StatusHtml,CustomerId,Status,CreatedAt,ModifiedAt,CreatedBy,ModifiedBy")] SaleDto saleDto)
        {
            if (ModelState.IsValid)
            {
                db.SaleDtoes.Add(saleDto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.CustomerDtoes, "Id", "FirstName", saleDto.CustomerId);
            return View(saleDto);
        }

        // GET: Sale/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleDto saleDto = await db.SaleDtoes.FindAsync(id);
            if (saleDto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.CustomerDtoes, "Id", "FirstName", saleDto.CustomerId);
            return View(saleDto);
        }

        // POST: Sale/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SaleCode,SaleAmount,SaleDate,SaleAmountPaid,AmountPaidTime,ActionLinkHtml,StatusHtml,CustomerId,Status,CreatedAt,ModifiedAt,CreatedBy,ModifiedBy")] SaleDto saleDto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saleDto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.CustomerDtoes, "Id", "FirstName", saleDto.CustomerId);
            return View(saleDto);
        }

        // GET: Sale/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleDto saleDto = await db.SaleDtoes.FindAsync(id);
            if (saleDto == null)
            {
                return HttpNotFound();
            }
            return View(saleDto);
        }

        // POST: Sale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            SaleDto saleDto = await db.SaleDtoes.FindAsync(id);
            db.SaleDtoes.Remove(saleDto);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
