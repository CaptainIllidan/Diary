using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Diary.DataAccessLayer;
using Diary.Models.Contacts;
using Diary.Common.Extensions;
using Newtonsoft.Json;

namespace Diary.Controllers
{
    public class ContactRecordController : Controller
    {
        private DiaryContext db = new DiaryContext();

        // GET: ContactRecord
        public ActionResult Index(string sortOrder, string searchString)
        {
            var contacts = db.Contacts.ToList();
            var searchedContacts = Search(contacts, searchString);
            var contactsSorted = Sort(searchedContacts, sortOrder);
            return View(contactsSorted);
        }

        private IEnumerable<ContactRecord> Search (IEnumerable<ContactRecord> contacts, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return contacts.Where(s => s.FirstName.ContainsIgnoreCase(searchString)
                                       || s.LastName.ContainsIgnoreCase(searchString)
                                       || s.Patronymic.ContainsIgnoreCase(searchString)
                                       || s.BirthDate.ToString().ContainsIgnoreCase(searchString)
                                       || s.Company.ToString().ContainsIgnoreCase(searchString))
                                       .ToList();
            }
            return contacts;
        }

        private IOrderedEnumerable<ContactRecord> Sort(IEnumerable<ContactRecord> contacts, string sortOrder)
        {
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "FirstName_desc" : "";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            ViewBag.PatronymicSortParm = sortOrder == "Patronymic" ? "Patronymic_desc" : "Patronymic";
            ViewBag.BirthDateSortParm = sortOrder == "BirthDate" ? "BirthDate_desc" : "BirthDate";
            ViewBag.CompanySortParm = sortOrder == "Company" ? "Company_desc" : "Company";
            switch (sortOrder)
            {
                case "FirstName_desc":
                    return contacts.OrderByDescending(z => z.FirstName);
                case "LastName":
                    return contacts.OrderBy(z => z.LastName);
                case "LastName_desc":
                    return contacts.OrderByDescending(z => z.LastName);
                case "Patronymic":
                    return contacts.OrderBy(z => z.Patronymic == null).ThenBy(z => z.Patronymic);
                case "Patronymic_desc":
                    return contacts.OrderByDescending(z => z.Patronymic != null).ThenByDescending(z => z.Patronymic);
                case "BirthDate":
                    return contacts.OrderBy(z => z.BirthDate == null).ThenBy(z => z.BirthDate);
                case "BirthDate_desc":
                    return contacts.OrderByDescending(z => z.BirthDate != null).ThenByDescending(z => z.BirthDate);
                case "Company":
                    return contacts.OrderBy(z => z.Company == null).ThenBy(z => z.Company);
                case "Company_desc":
                    return contacts.OrderByDescending(z => z.Company != null).ThenByDescending(z => z.Company);
                default:
                    return contacts.OrderBy(z => z.FirstName);
            }
        }

        // GET: ContactRecord/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactRecord contactRecord = db.Contacts.Find(id);
            if (contactRecord == null)
            {
                return HttpNotFound();
            }
            return View(contactRecord);
        }

        // GET: ContactRecord/Create
        //Возвращается новый экземпляр, поскольку в представлении вызываются поля класса
        public ActionResult Create()
        {
            return View(new ContactRecord());
        }

        public string GetContactInformation(int? id, string sidx, string sord, int page, int rows, bool _search, string filters)
        {
            if (id==null)
            {
                return null;
            }
            var contact = db.Contacts.Find(id);
            if (contact==null)
            {
                return null;
            }
            var contactInformations = contact.ContactInformation.Select(
                z => new
                {
                    z.ID,
                    z.ContactInformationType,
                    z.Value
                })
                .ToList();
            return JsonConvert.SerializeObject(contactInformations);
            
        }

        // POST: ContactRecord/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Patronymic,BirthDate,Company")] ContactRecord contactRecord)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contactRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactRecord);
        }

        // GET: ContactRecord/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactRecord contactRecord = db.Contacts.Find(id);
            if (contactRecord == null)
            {
                return HttpNotFound();
            }
            return View(contactRecord);
        }

        // POST: ContactRecord/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Patronymic,BirthDate,Company,ContactInformation")] ContactRecord contactRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactRecord);
        }

        // GET: ContactRecord/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactRecord contactRecord = db.Contacts.Find(id);
            if (contactRecord == null)
            {
                return HttpNotFound();
            }
            return View(contactRecord);
        }

        // POST: ContactRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactRecord contactRecord = db.Contacts.Find(id);
            db.Contacts.Remove(contactRecord);
            db.SaveChanges();
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
