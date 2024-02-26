using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.VisualBasic;

namespace WebApiWithDapper.Entities
{
    public class Employees
    {

        public int EmpNo { get; set; }

        public string Name { get; set; } = null!;

        public int Basic { get; set; }

        public int DeptNo { get; set; }

        public string Email { get; set; }
        public string pass { get; set; }
        public DateTime CDate { get; set; }
       

    }
}
