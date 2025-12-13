using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Data;
using MyWebApp.Models;

namespace MyWebApp.Controllers
{
    public class LearnerController : Controller
    {
        private SchoolContext db;

        public LearnerController(SchoolContext context)
        {
            db = context;
        }

        public IActionResult Index(int? mid)
        {
            if (mid == null)
            {
                var learners = db.Learners.Include(m => m.Major).ToList();
                return View(learners);
            }
            else
            {
                var learners = db.Learners
                    .Where(l => l.MajorID == mid)
                    .Include(m => m.Major).ToList();
                return View(learners);
            }
        }

        public IActionResult LearnerByMajorID(int mid)
        {
            var learners = db.Learners
                .Where(l => l.MajorID == mid)
                .Include(m => m.Major).ToList();
            return PartialView("LearnerTable", learners);
        }

        public IActionResult Create()
        {
            var majors = new List<SelectListItem>();

            foreach (var m in db.Majors)
            {
                majors.Add(new SelectListItem
                {
                    Text = m.MajorName,
                    Value = m.MajorID.ToString()
                });
            }

            ViewBag.MajorID = majors;
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View();
        }

        public IActionResult Edit(int id)
        {
            if (id == null || db.Learners == null)
            {
                return NotFound();
            }

            var learner = db.Learners.Find(id);
            if (learner == null)
            {
                return NotFound();
            }

            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (id != learner.LearnerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(learner);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearnerExists(learner.LearnerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        private bool LearnerExists(int id)
        {
            return (db.Learners?.Any(e => e.LearnerID == id)).GetValueOrDefault();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || db.Learners == null)
            {
                return NotFound();
            }

            var learner = db.Learners
                .Include(l => l.Major)
                .Include(l => l.Enrollments)
                .FirstOrDefault(m => m.LearnerID == id);

            if (learner == null)
            {
                return NotFound();
            }

            if (learner.Enrollments != null && learner.Enrollments.Count() > 0)
            {
                return Content("This learner has some enrollments, can't delete!");
            }

            return View(learner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (db.Learners == null)
            {
                return Problem("Entity set 'Learners' is null.");
            }

            var learner = db.Learners.Find(id);
            if (learner != null)
            {
                db.Learners.Remove(learner);
                db.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
