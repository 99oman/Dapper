using WebApiWithDapper.Entities;

namespace WebApiWithDapper.Contract
{
    public interface IEmpRepository
    {
        public Task<IEnumerable<Employees>> GetEmployees();
        public Task<Employees> GetEmployee(int EmpNo);
        public Task<Employees> Create(Employees emp);
        public Task Update(int id, Employees emp);
        public Task Delete(int EmpNo);
        public Task<Employees> Login(string Email,string pass);
        public Task<bool> checkEmail(string Email);
        public Task<IEnumerable<Employees>> GetEmployeesinRange (string FDate , string TDate);
    }
}
