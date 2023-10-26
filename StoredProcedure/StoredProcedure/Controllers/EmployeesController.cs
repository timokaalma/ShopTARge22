using Microsoft.AspNetCore.Mvc;
using StoredProcedure.Data;
using StoredProcedure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace StoredProcedure.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly StoredProcedureDbContext _context;
        private readonly IConfiguration _config;

        public EmployeesController
            (
                StoredProcedureDbContext context,
                IConfiguration config
            )
        {
            _context = context;
            _config = config;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IEnumerable<EmployeeViewModel> SearchResult()
        {
            var result = _context.Employee
                .FromSqlRaw<EmployeeViewModel>("spSearchEmployees")
                .ToList();
             
            return result;
        }

        [HttpGet]
        public IActionResult DynamicSQL()
        {
            string connectionStr = _config.GetConnectionString("DefaultConnection");

            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "dbo.spSearchEmployees";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                List<EmployeeViewModel> model = new List<EmployeeViewModel>();
                while (sdr.Read())
                {
                    var details = new EmployeeViewModel();
                    details.FirstName = sdr["FirstName"].ToString();
                    details.LastName = sdr["LastName"].ToString();
                    details.Gender = sdr["Gender"].ToString();
                    details.Salary = Convert.ToInt32(sdr["Salary"]);
                    model.Add(details);
                }
                return View(model);
            }
        }
    }
}
