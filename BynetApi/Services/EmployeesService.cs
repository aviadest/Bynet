using BynetApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BynetApi.Services
{
    public interface IEmployeesService
    {
        V_Employee? Get(string idNumber);
        List<V_Employee> GetAll(bool filterManagers);
        bool Insert(EmployeeDto employee);
        bool Update(string idNumber, EmployeeDto employee);
        bool Delete(string idNumber);
        List<Manager> GetManagers();
    }

    public class EmployeesService : IEmployeesService
    {

        public V_Employee? Get(string idNumber)
        {
            using (var db = new BynetContext())
            {
                return db.V_Employees.SingleOrDefault(e => e.IdNumber == idNumber);
            }
        }

        public List<V_Employee> GetAll(bool filterManagers)
        {
            using (var db = new BynetContext())
            {
                return filterManagers ?
                    (from manager in db.V_Employees
                     join emp in db.Employees on manager.IdNumber equals emp.ManagerId
                     select manager
                    ).Distinct().ToList()
                     :
                    db.V_Employees.ToList();
            }
        }

        public bool Insert(EmployeeDto employee)
        {
            using (var db = new BynetContext())
            {
                try
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool Update(string idNumber, EmployeeDto employee)
        {
            using (var db = new BynetContext())
            {
                try
                {
                    employee.IdNumber = idNumber;
                    db.Employees.Update(employee);
                    db.SaveChanges();
                    return true;
                }
                catch(Exception e)
                {
                    return false;
                }
            }
        }

        public bool Delete(string idNumber)
        {
            using (var db = new BynetContext())
            {
                var employee = db.Employees.SingleOrDefault(e => e.IdNumber == idNumber);
                if (employee != null)
                {
                    try
                    {
                        db.Remove(employee);
                        db.SaveChanges();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                return false;
            }

        }

        public List<Manager> GetManagers()
        {
            using (var db = new BynetContext())
            {
                var managers = (from emp in db.V_Employees
                     select new Manager(){ Id = emp.IdNumber, Name = $"{emp.FirstName} {emp.LastName}"}
                    ).ToList();
                
                return managers;
            }
        }
    }
}