using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using RestSharp;
using WebApiWithDapper.Contract;
using WebApiWithDapper.Entities;

namespace WebApiWithDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmpRepository _emprepository;
        public EmployeesController(IEmpRepository emprepository)
        {
            _emprepository = emprepository;
        }
        [HttpGet("geir", Name = "GetEmployeesinRange")]
        public async Task<IActionResult> GetEmployeesinRange(string FDate, string TDate)
        {
           
            var employees = await _emprepository.GetEmployeesinRange(FDate, TDate);
            return Ok(employees);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees() {
   
            var employees = await _emprepository.GetEmployees();
             return Ok(employees); 
            

        }


        [HttpGet("{id}/GetEmployee", Name = "CompanyById")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            try
            {
                var company = await _emprepository.GetEmployee(id);
                if (company == null)
                {
                    return NotFound();
                }
                return Ok(company);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employees company)
        {
            try
            {
                var d = await _emprepository.checkEmail(company.Email);
                if (d == true) { return BadRequest(); }
                else
                { 
                    var employees = await _emprepository.Create(company);
                    return CreatedAtAction("CompanyById", new {id= employees.EmpNo }, employees);
                }
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, Employees emp)
        {
            try
            {
                var dbemp = await _emprepository.GetEmployee(id);
                if (dbemp == null)
                {
                    return NotFound();
                }
                else
                {
                    await _emprepository.Update(id, emp);
                }
              return Ok();
            
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            
            try
            {
                var dbemp = await _emprepository.GetEmployee(id);
                
                if (dbemp == null)
                    return NotFound();
                await _emprepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
      
        [HttpGet("{Email},{pass}/Login", Name = "CompanyByEmail")]
        public async Task<IActionResult> Login(string Email,string pass)
        {
            try
            {
                var company = await _emprepository.Login(Email,pass);
                if (company == null)
                    return NotFound();
                else
                {
                    return Ok(company);
                }
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{Email}", Name = "CheckByEmail")]
        public async Task<IActionResult> checkEmail(string Email)
        {
            try
            {
                var company = await _emprepository.checkEmail(Email);
                if (company == true) { return Ok(company); }

                else
                    return NotFound();
                
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
