using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRUD_IN__MVC_Employee .Models;
using System;
using Microsoft.AspNetCore.Http;

namespace CRUD_IN__MVC_Employee.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeDAL context = new EmployeeDAL();
        public IActionResult EmployeeList()
        {
            ViewBag.EmployeeList = context.GetAllDetails();
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Create(IFormCollection fc)
        {
            Employee e = new Employee();
            e.empName = fc["empName"];
            e.empDept = fc["empDept"];
            int res = context.Save(e);
            if (res == 1)
                return RedirectToAction("EmployeeList");

            return View();

        }
        [HttpGet]
        public IActionResult Edit(int empId)
        {
            Employee e = context.GetEmployeeByid(empId);
            ViewBag.empName = e.empName;
            ViewBag.empDept = e.empDept;
            ViewBag.empId = e.empId;
            return View();
        }
        [HttpPost]
        public IActionResult Edit(IFormCollection form)
        {
            Employee e = new Employee();
            e.empName = form["empName"];
            e.empDept= form["empDept"];
            e.empId = Convert.ToInt32(form["empId"]);
            int res = context.Upate(e);
            if (res == 1)
                return RedirectToAction("EmployeeList");

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int empId)
        {
            Employee e = context.GetEmployeeByid(empId);
            ViewBag.empName = e.empName;
            ViewBag.empDept = e.empDept;
            ViewBag.empId = e.empId;
            return View();
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int empId)
        {
            int res = context.Delete(empId);
            if (res == 1)
                return RedirectToAction("EmployeeList");

            return View();
        }
    }

}
