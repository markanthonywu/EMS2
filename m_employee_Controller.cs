using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EMS.Models;


namespace EMS.Controllers
{
    public class m_employeeController : Controller
    {
        private employeeEntities db = new employeeEntities();

        // GET: m_employee
        public ActionResult Index()
        {
            var m_employee = db.m_employee.Include(m => m.m_section).Include(m => m.m_railway_route).Include(m => m.m_skillsheet_comment);
            return View(m_employee.ToList());
        }
        public ActionResult End()
        {
            var m_employee = db.m_employee.Include(m => m.m_section).Include(m => m.m_railway_route).Include(m => m.m_skillsheet_comment);
            return View(m_employee.ToList());
        }

        // GET: m_employee/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_employee m_employee = db.m_employee.Find(id);
            if (m_employee == null)
            {
                return HttpNotFound();
            }
            return View(m_employee);
        }

        // GET: m_employee/Create
        public ActionResult Create()
        {
            ViewBag.section_id = new SelectList(db.m_section, "section_id", "section_name");
            ViewBag.railway_route_cd = new SelectList(db.m_railway_route, "railway_route_cd", "railway_route_nm");
            ViewBag.emp_cd = new SelectList(db.m_skillsheet_comment, "emp_cd", "skillsheet_id");
            return View();
        }

        // POST: m_employee/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "emp_cd,user_id,password,user_level,last_name,first_name,last_name_kn,first_name_kn,section_id,gender_cd,birth_date,emp_date,mail,mobile,zip_code,address_city,address_block,address_building,railway_route_cd,nearest_station,final_education,created,updated,deleted")] m_employee m_employee)
        {
            if (ModelState.IsValid)
            {
                db.m_employee.Add(m_employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.section_id = new SelectList(db.m_section, "section_id", "section_name", m_employee.section_id);
            ViewBag.railway_route_cd = new SelectList(db.m_railway_route, "railway_route_cd", "railway_route_nm", m_employee.railway_route_cd);
            ViewBag.emp_cd = new SelectList(db.m_skillsheet_comment, "emp_cd", "skillsheet_id", m_employee.emp_cd);
            return View(m_employee);
        }

        // GET: m_employee/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_employee m_employee = db.m_employee.Find(id);
            if (m_employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.section_id = new SelectList(db.m_section, "section_id", "section_name", m_employee.section_id);
            ViewBag.railway_route_cd = new SelectList(db.m_railway_route, "railway_route_cd", "railway_route_nm", m_employee.railway_route_cd);
            ViewBag.emp_cd = new SelectList(db.m_skillsheet_comment, "emp_cd", "skillsheet_id", m_employee.emp_cd);
            return View(m_employee);
        }

        // POST: m_employee/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "emp_cd,user_id,password,user_level,last_name,first_name,last_name_kn,first_name_kn,section_id,gender_cd,birth_date,emp_date,mail,mobile,zip_code,address_city,address_block,address_building,railway_route_cd,nearest_station,final_education,created,updated,deleted")] m_employee m_employee)
        {
            //m_employee.deleted = 0;
            //if (m_employee.deleted == 1)
            //{
                //この辺考え中★★★★★★★★
            //}
            if (ModelState.IsValid)
            {
                db.Entry(m_employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("End");
            }
            ViewBag.section_id = new SelectList(db.m_section, "section_id", "section_name", m_employee.section_id);
            ViewBag.railway_route_cd = new SelectList(db.m_railway_route, "railway_route_cd", "railway_route_nm", m_employee.railway_route_cd);
            ViewBag.emp_cd = new SelectList(db.m_skillsheet_comment, "emp_cd", "skillsheet_id", m_employee.emp_cd);
            return View(m_employee);
        }
        //public ActionResult BindSampleResult([Bind(Exclude = "emp_cd,user_id,password,user_level,last_name,first_name,last_name_kn,first_name_kn,section_id,gender_cd,birth_date,emp_date,mail,mobile,zip_code,address_city,address_block,address_building,railway_route_cd,nearest_station,final_education,created,updated,deleted")] m_employee m_employee)
        //{
        //    //考え中
        //    return ("Index");   //★★★★★★★★
        //}

        // GET: m_employee/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_employee m_employee = db.m_employee.Find(id);
            if (m_employee == null)
            {
                return HttpNotFound();
            }
            return View(m_employee);
        }

        // POST: m_employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            m_employee m_employee = db.m_employee.Find(id);
            db.m_employee.Remove(m_employee);
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
