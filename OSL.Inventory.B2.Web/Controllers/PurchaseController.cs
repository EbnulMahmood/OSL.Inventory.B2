using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OSL.Inventory.B2.Repository.Data;
using OSL.Inventory.B2.Service.DTOs;

namespace OSL.Inventory.B2.Web.Controllers
{
    public class PurchaseController : Controller
    {
        private InventoryDbContext db = new InventoryDbContext();

        // GET: Purchase
        public async Task<ActionResult> Index()
        {
            var purchaseDtoes = db.PurchaseDtoes.Include(p => p.Supplier);
            return View(await purchaseDtoes.ToListAsync());
        }

        // GET: Purchase/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseDto purchaseDto = await db.PurchaseDtoes.FindAsync(id);
            if (purchaseDto == null)
            {
                return HttpNotFound();
            }
            return View(purchaseDto);
        }

        // GET: Purchase/Create
        public ActionResult Create()
        {
            ViewBag.SupplierId = new SelectList(db.SupplierDtoes, "Id", "FirstName");
            return View();
        }

        // POST: Purchase/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,PurchaseCode,PurchaseAmount,PurchaseDate,PurchaseAmountPaid,AmountPaidTime,SupplierId")] PurchaseDto purchaseDto)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseDtoes.Add(purchaseDto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierId = new SelectList(db.SupplierDtoes, "Id", "FirstName", purchaseDto.SupplierId);
            return View(purchaseDto);
        }

        // GET: Purchase/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseDto purchaseDto = await db.PurchaseDtoes.FindAsync(id);
            if (purchaseDto == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierId = new SelectList(db.SupplierDtoes, "Id", "FirstName", purchaseDto.SupplierId);
            return View(purchaseDto);
        }

        // POST: Purchase/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,PurchaseCode,PurchaseAmount,PurchaseDate,PurchaseAmountPaid,AmountPaidTime,SupplierId")] PurchaseDto purchaseDto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseDto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierId = new SelectList(db.SupplierDtoes, "Id", "FirstName", purchaseDto.SupplierId);
            return View(purchaseDto);
        }

        // GET: Purchase/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseDto purchaseDto = await db.PurchaseDtoes.FindAsync(id);
            if (purchaseDto == null)
            {
                return HttpNotFound();
            }
            return View(purchaseDto);
        }

        // POST: Purchase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            PurchaseDto purchaseDto = await db.PurchaseDtoes.FindAsync(id);
            db.PurchaseDtoes.Remove(purchaseDto);
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
