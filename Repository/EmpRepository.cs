
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using WebApiWithDapper.Context;
using WebApiWithDapper.Contract;
using WebApiWithDapper.Entities;

namespace WebApiWithDapper.Repository
{
    public class EmpRepository:IEmpRepository
    {
        private readonly DapperContext _context;
        public EmpRepository(DapperContext context)
        {
            _context = context; 
        }

        public async Task<Employees> Create(Employees emp)
        {



           
            DateTime currentDateTime = DateTime.Now.Date;

          
            var parameters = new DynamicParameters();
            parameters.Add("Name", emp.Name, DbType.String);
            parameters.Add("Basic", emp.Basic, DbType.Int32);
            parameters.Add("DeptNo", emp.DeptNo, DbType.Int32);
            parameters.Add("Email", emp.Email, DbType.String);
            parameters.Add("pass", emp.pass, DbType.String);
         parameters.Add("CDate", emp.CDate = currentDateTime , DbType.Date);
            
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>("DapperInsert", parameters, commandType: CommandType.StoredProcedure);
                var employees = new Employees
                {
                   
                    Name = emp.Name,
                    Basic = emp.Basic,
                    DeptNo = emp.DeptNo,
                    Email = emp.Email,
                    pass = emp.pass,
                   CDate = currentDateTime
                };
                     return employees;
               
            }
        }

        

        public async Task<Employees> GetEmployee(int EmpNo)
        {
           
            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Employees>("GetSingleEmployee", new { EmpNo }, commandType: CommandType.StoredProcedure);
                return company;
            }
        }

        public async Task<IEnumerable<Employees>> GetEmployees()
        {
            
            using (var connection = _context.CreateConnection())
            {

                    var employees = await connection.QueryAsync<Employees>("AllEmployees", commandType: CommandType.StoredProcedure);
                return employees;
               
            }
        }

        public async Task Update(int id, Employees emp)
        {
           
            var parameters = new DynamicParameters();
            parameters.Add("EmpNo", id, DbType.Int32);
            parameters.Add("Name", emp.Name, DbType.String);
            parameters.Add("Basic", emp.Basic, DbType.Int32);
            parameters.Add("DeptNo", emp.DeptNo, DbType.Int32);
            parameters.Add("Email", emp.Email, DbType.String);
        


            using (var connection = _context.CreateConnection())
            {
               await connection.ExecuteAsync("DapperUpdate", parameters, commandType: CommandType.StoredProcedure);
                
            }
        }
        public async Task Delete(int EmpNo)
        {
            
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync("DapperDelete",new { EmpNo }, commandType: CommandType.StoredProcedure);
               
            }
        }

        public async Task<Employees> Login(string Email,string pass)
        {
            
            using (var connection = _context.CreateConnection())
            {
             
                var company = await connection.QuerySingleOrDefaultAsync<Employees>("GetbyEmail", new { Email,pass }, commandType: CommandType.StoredProcedure);
                return company;
            }
        }

        public async Task <bool> checkEmail(string Email)
        {
            using (var connection = _context.CreateConnection())
            {

                var company = await connection.QuerySingleOrDefaultAsync<Employees>("checkEmail", new { Email}, commandType: CommandType.StoredProcedure);
                if (company != null)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public async Task<IEnumerable<Employees>> GetEmployeesinRange(string FDate, string TDate)
        {
           
            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<Employees>("GetEmployeesinRange",new { FDate,TDate}, commandType: CommandType.StoredProcedure);
                return employees;
            }
        }
    }
}
