using Demo.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using Demo.Context;


namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext;
        public StudentController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        
        }

        [HttpGet]
        public IEnumerable<Student> GetStudent( )
        {
            return dbContext.Student.ToList();
        }

    }

}


