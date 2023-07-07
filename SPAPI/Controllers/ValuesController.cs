using System.Data;
using Microsoft.AspNetCore.Mvc;
using SPAPI.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        db dbop = new db();
        string msg = string.Empty;

        // GET: api/<EmployeesController>
        [HttpGet]

        public List<Employee> Get(string Id, string Name)
        {
            string result = " Id= " + Id + " Name= " + Name;


            Employee emp = new Employee();

            DataSet ds = dbop.EmployeesGet(emp, out msg);
            List<Employee> list = new List<Employee>();


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new Employee
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Age = Convert.ToInt32(dr["Age"]),
                    Active = Convert.ToInt32(dr["Active"]),
                    Name = dr["Name"].ToString(),


                });
            }

            return list;

        }


        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public List<Employee> Get(int id)
        {

            Employee emp = new Employee();
            emp.Id = id;

            DataSet ds = dbop.EmployeesGet(emp, out msg);
            List<Employee> list = new List<Employee>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new Employee
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Age = Convert.ToInt32(dr["Age"]),
                    Active = Convert.ToInt32(dr["Active"]),
                    Name = dr["Name"].ToString(),
                });
            }

            return list;
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public string Post([FromBody] Employee emp)
        {
            string msg = string.Empty;
            try
            {
                msg = dbop.EmployeesOpt(emp);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] Employee emp)
        {
            string msg = string.Empty;
            try
            {
                emp.Id = id;

                msg = dbop.EmployeesOpt(emp);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            string msg = string.Empty;
            try
            {
                Employee emp = new Employee();
                emp.Id = id;

                msg = dbop.EmployeesOpt(emp);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
    }
}