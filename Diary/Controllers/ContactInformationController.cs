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
using Diary.Common;

namespace Diary.Controllers
{
    public class ContactInformationController : Controller
    {
        private DiaryContext db = new DiaryContext();

        /// <summary>Return enum representation as List {value:name}</summary>
        public string GetContactInformationTypes()
        {
            return EnumExport.ExportEnumToJSON<ContactInformationType>();
        }


        // POST: ContactInformation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public void Create([Bind(Include = "ContactInformationType,Value")] ContactInformation contactInformation, int contactRecordId)
        {
            if (ModelState.IsValid)
            {
                db.ContactInformations.Add(contactInformation);
                AddToContactRecord(contactRecordId, contactInformation);
                db.SaveChanges();
            }
        }

        // POST: ContactInformation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public void Edit([Bind(Include = "ID,ContactInformationType,Value")] ContactInformation contactInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactInformation).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        private void AddToContactRecord(int id, ContactInformation contactInformation)
        {
            var contactRecord = db.Contacts.Find(id);
            contactRecord.ContactInformation.Add(contactInformation);
            db.Entry(contactRecord).State = EntityState.Modified;
        }

        // GET: ContactInformation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactInformation contactInformation = db.ContactInformations.Find(id);
            if (contactInformation == null)
            {
                return HttpNotFound();
            }
            return View(contactInformation);
        }

        // POST: ContactInformation/Delete/5
        [HttpPost]
        public void Delete(int id)
        {
            ContactInformation contactInformation = db.ContactInformations.Find(id);
            db.ContactInformations.Remove(contactInformation);
            db.SaveChanges();
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
