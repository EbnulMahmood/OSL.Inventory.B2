using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using OSL.Inventory.B2.Repository.Data;
using OSL.Inventory.B2.Service.DTOs;

namespace OSL.Inventory.B2.Web.Controllers
{
    public class SupplierController : Controller
    {
        private InventoryDbContext db = new InventoryDbContext();

        // GET: Supplier
        public async Task<ActionResult> Index()
        {
            return View(await db.SupplierDtoes.ToListAsync());
        }

        // GET: Supplier/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierDto supplierDto = await db.SupplierDtoes.FindAsync(id);
            if (supplierDto == null)
            {
                return HttpNotFound();
            }
            return View(supplierDto);
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
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,PhoneNumber,Country,City,State,ZipCode")] SupplierDto supplierDto)
        {
            if (ModelState.IsValid)
            {
                db.SupplierDtoes.Add(supplierDto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(supplierDto);
        }

        // GET: Supplier/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierDto supplierDto = await db.SupplierDtoes.FindAsync(id);
            if (supplierDto == null)
            {
                return HttpNotFound();
            }
            return View(supplierDto);
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,PhoneNumber,Country,City,State,ZipCode")] SupplierDto supplierDto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplierDto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(supplierDto);
        }

        // GET: Supplier/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierDto supplierDto = await db.SupplierDtoes.FindAsync(id);
            if (supplierDto == null)
            {
                return HttpNotFound();
            }
            return View(supplierDto);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            SupplierDto supplierDto = await db.SupplierDtoes.FindAsync(id);
            db.SupplierDtoes.Remove(supplierDto);
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
