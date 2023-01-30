using BynetApi.Models;
using BynetApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BynetApi.Controllers
{
    [ApiController]
    [Route("api")]

    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpGet]
        [Route("Get/{idNumber}")]
        public JsonResult Get(string idNumber)
        {
            var employee = _employeesService.Get(idNumber);
            if (employee == null)
            {
                return new JsonResult(false);
            }

            var managers = _employeesService.GetManagers();
            if (managers == null)
            {
                return new JsonResult(false);
            }

            return new JsonResult(employee);
        }

        [HttpGet]
        [Route("GetManagers")]
        public JsonResult GetManagers()
        {
            var managers = _employeesService.GetManagers();
            if (managers == null)
            {
                return new JsonResult(false);
            }

            return new JsonResult(managers);
        }

        [HttpGet]
        [Route("GetAll")]
        public JsonResult GetAll([FromQuery] bool isFilter = false)
        {
            var employees = _employeesService.GetAll(isFilter);

            return new JsonResult(employees);
        }

        [HttpPost]
        public JsonResult Add([FromBody] EmployeeDto employee)
        {
            if (employee is null || !ModelState.IsValid)
            {
                return new JsonResult(false);
            }

            if (_employeesService.Insert(employee))
            {
                return new JsonResult(true);
            }
            else
            {
                return new JsonResult(false);
            }
        }

        [HttpPut("{idNumber}")]
        public JsonResult Edit(string idNumber, [FromBody] EmployeeDto employee)
        {
            if (employee == null || !ModelState.IsValid)
            {
                return new JsonResult(false);
            }

            var update = _employeesService.Get(idNumber);
            if (update == null)
            {
                return new JsonResult(false);
            }

            if (_employeesService.Update(idNumber, employee))
            {
                return new JsonResult(true);
            }
            else
            {
                return new JsonResult(false);
            }
        }

        [HttpDelete("{idNumber}")]
        public JsonResult Delete(string idNumber)
        {
            if (idNumber.IsNullOrEmpty())
            {
                return new JsonResult(false);
            }

            if (_employeesService.Delete(idNumber))
            {
                return new JsonResult(true);
            }
            else
            {
                return new JsonResult(false);
            }
        }
    }
}