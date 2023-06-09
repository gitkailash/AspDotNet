using StudentCRUDOperation.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentCRUDOperation.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        TestEntities1 dbObject = new TestEntities1();
        public ActionResult Student(Student model)
        {
            if (model != null)
            {
                return View(model);
            }else

            return View();
        }

        // add student
        [HttpPost]
        public ActionResult AddStudent(Student model)
        {
            if (ModelState.IsValid)
            {
                Student obj = new Student();

                obj.ID = model.ID;
                obj.FirstName = model.FirstName;
                obj.LastName = model.LastName;
                obj.Email = model.Email;
                obj.Mobile = model.Mobile;
                obj.Address = model.Address;

                if (model.ID == 0)
                {
                    dbObject.Students.Add(obj);
                    dbObject.SaveChanges();
                    ModelState.Clear();
                }
                else
                {
                    dbObject.Entry(obj).State = EntityState.Modified;
                    dbObject.SaveChanges();
                    ModelState.Clear();

                }
                
            }
                return View("Student");

        }

        //for update student
        public ActionResult UpdateStudent(int id)
        {
            var stdDetails = dbObject.Students.Where(model => model.ID == id).FirstOrDefault();
            return View("Student", stdDetails);
        }


        //for delete student
        public ActionResult DeleteStudent(int id)
        {
            var res = dbObject.Students.Where(model=>model.ID==id).First();
            dbObject.Students.Remove(res);
            dbObject.SaveChanges();

            var list = dbObject.Students.ToList();
            return View("StudentList", list);
        }

        public ActionResult StudentList()
        {
            var studentlist = dbObject.Students.ToList();
            return View(studentlist);
        }
    }
}