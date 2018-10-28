using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Diary.DataAccessLayer;
using Diary.Models.Diary;
using Diary.Common.Extensions;

namespace Diary.Controllers
{
    public class DiaryRecordController : Controller
    {
        private DiaryContext db = new DiaryContext();

        // GET: DiaryRecord
        public ActionResult Index(string sortOrder, string searchString, string period)
        {
            var diaryRecords = db.DiaryRecords.ToList();
            var searchedDiaryRecords = Search(diaryRecords, searchString).ToList();
            var diaryRecordsFilteredByPeriod = FilterByPeriod(searchedDiaryRecords, period);
            var diaryRecordsSorted = Sort(diaryRecordsFilteredByPeriod, sortOrder);
            return View(diaryRecordsSorted);
        }

        private IEnumerable<DiaryRecord> Search(IEnumerable<DiaryRecord> diaryRecords, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return diaryRecords.Where(s => s.DiaryRecordType.EnumDisplayNameFor().ContainsIgnoreCase(searchString)
                                       || s.Theme.ContainsIgnoreCase(searchString)
                                       || s is Meeting && (s as Meeting).Place.ContainsIgnoreCase(searchString));
            }
            return diaryRecords;
        }

        private IOrderedEnumerable<DiaryRecord> Sort(IEnumerable<DiaryRecord> diaryRecords, string sortOrder)
        {
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";
            ViewBag.DateStartSortParm = sortOrder == "DateStart" ? "DateStart_desc" : "DateStart";
            ViewBag.DateEndSortParm = sortOrder == "DateEnd" ? "DateEnd_desc" : "DateEnd";

            switch (sortOrder)
            {
                case "type_desc":
                    return diaryRecords.OrderByDescending(z => z.DiaryRecordType.EnumDisplayNameFor());
                case "DateStart":
                    return diaryRecords.OrderBy(z => z.StartDateTime);
                case "DateStart_desc":
                    return diaryRecords.OrderByDescending(z => z.StartDateTime);
                case "DateEnd":
                    return diaryRecords.OrderBy(z =>
                        (z is DiaryRecordFinishable)
                            ? (z as DiaryRecordFinishable).EndDateTime
                            : DateTime.MaxValue);
                case "DateEnd_desc":
                    return diaryRecords.OrderByDescending(z =>
                        (z is DiaryRecordFinishable)
                            ? (z as DiaryRecordFinishable).EndDateTime
                            : DateTime.MinValue);
                default:
                    return diaryRecords.OrderBy(z => z.DiaryRecordType.EnumDisplayNameFor());
            }
        }

        private IEnumerable<DiaryRecord> FilterByPeriod(IEnumerable<DiaryRecord> diaryRecords, string period)
        {
            switch (period)
            {
                case "Day":
                    return diaryRecords.Where(z => z.StartDateTime.Date == DateTime.Now.Date);
                case "Week":
                    return diaryRecords.Where(z => DateTime.Now.Date <= z.StartDateTime.Date && z.StartDateTime.Date <= DateTime.Now.Date.AddDays(7));
                case "Month":
                    return diaryRecords.Where(z => DateTime.Now.Date <= z.StartDateTime.Date && z.StartDateTime.Date <= DateTime.Now.Date.AddMonths(1));
                default:
                    return diaryRecords;
            }
        }

        // GET: DiaryRecord/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiaryRecord diaryRecord = db.DiaryRecords.Find(id);
            if (diaryRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.type = diaryRecord.DiaryRecordType.ToString();
            return View(diaryRecord);
        }

        // GET: DiaryRecord/Create
        public ActionResult Create(string type)
        {
            ViewBag.type = type;
            return View();
        }

        // POST: DiaryRecord/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMeeting([Bind(Include = "ID,Theme,StartDateTime,IsDone,EndDateTime,Place")] Meeting meeting)
        {
            return Create(meeting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNote([Bind(Include = "ID,Theme,StartDateTime,IsDone")] Note note)
        {
            return Create(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateThing([Bind(Include = "ID,Theme,StartDateTime,IsDone,EndDateTime")] Thing thing)
        {
            return Create(thing);
        }

        private ActionResult Create(DiaryRecord diaryRecord)
        {
            if (ModelState.IsValid)
            {
                db.DiaryRecords.Add(diaryRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create", diaryRecord);
        }

        // GET: DiaryRecord/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiaryRecord diaryRecord = db.DiaryRecords.Find(id);
            if (diaryRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.type = diaryRecord.DiaryRecordType.ToString();
            return View(diaryRecord);
        }

        // POST: DiaryRecord/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMeeting([Bind(Include = "ID,Theme,StartDateTime,IsDone,EndDateTime,Place")] Meeting meeting)
        {
            return Edit(meeting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNote([Bind(Include = "ID,Theme,StartDateTime,IsDone,EndDateTime,Place")] Note note)
        {
            return Edit(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditThing([Bind(Include = "ID,Theme,StartDateTime,IsDone,EndDateTime,Place")] Thing thing)
        {
            return Edit(thing);
        }

        private ActionResult Edit(DiaryRecord diaryRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diaryRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit",diaryRecord);
        }

        // GET: DiaryRecord/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiaryRecord diaryRecord = db.DiaryRecords.Find(id);
            if (diaryRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.type = diaryRecord.DiaryRecordType.ToString();
            return View(diaryRecord);
        }

        // POST: DiaryRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiaryRecord diaryRecord = db.DiaryRecords.Find(id);
            db.DiaryRecords.Remove(diaryRecord);
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
