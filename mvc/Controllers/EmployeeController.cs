using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Models.Repositories;
using System.Linq;

namespace mvc.Controllers
{
   
    public class EmployeeController : Controller
    {
        readonly IRepository<Employee> employeeRepository;

        public EmployeeController(IRepository<Employee> empRepository)
        {
            employeeRepository = empRepository;
        }

        // GET: EmployeeController
        public ActionResult Index()
        {
            var employees = employeeRepository.GetAll();
            ViewData["EmployeesCount"] = employees.Count();
            ViewData["SalaryAverage"] = employeeRepository.SalaryAverage();
            ViewData["MaxSalary"] = employeeRepository.MaxSalary();
            ViewData["HREmployeesCount"] = employeeRepository.HrEmployeesCount();
            return View(employees);
        }



        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            var emp =employeeRepository.FindByID(id);
            return View(emp);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee e)
        {
            try
            {
                employeeRepository.Add(e);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            var emp =employeeRepository.FindByID(id);
            return View(emp);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee newEmp)
        {
            try
            {
                employeeRepository.Update(id,newEmp);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            var emp = employeeRepository.FindByID(id);
            return View(emp);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Employee e)
        {
            try
            {
                employeeRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Search(string term)
        {
            var result = employeeRepository.Search(term);
            return View("Index", result);
        }

    }
}