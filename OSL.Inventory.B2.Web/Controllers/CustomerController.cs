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
    public class CustomerController : Controller
    {
        private InventoryDbContext db = new InventoryDbContext();

        // GET: Customer
        public async Task<ActionResult> Index()
        {
            return View(await db.CustomerDtoes.ToListAsync());
        }

        // GET: Customer/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerDto customerDto = await db.CustomerDtoes.FindAsync(id);
            if (customerDto == null)
            {
                return HttpNotFound();
            }
            return View(customerDto);
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
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,PhoneNumber,Country,City,State,ZipCode")] CustomerDto customerDto)
        {
            if (ModelState.IsValid)
            {
                db.CustomerDtoes.Add(customerDto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(customerDto);
        }

        // GET: Customer/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerDto customerDto = await db.CustomerDtoes.FindAsync(id);
            if (customerDto == null)
            {
                return HttpNotFound();
            }
            return View(customerDto);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,PhoneNumber,Country,City,State,ZipCode")] CustomerDto customerDto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerDto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customerDto);
        }

        // GET: Customer/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerDto customerDto = await db.CustomerDtoes.FindAsync(id);
            if (customerDto == null)
            {
                return HttpNotFound();
            }
            return View(customerDto);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            CustomerDto customerDto = await db.CustomerDtoes.FindAsync(id);
            db.CustomerDtoes.Remove(customerDto);
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
