using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MockTest.Server.Data;
using MockTest.Shared.Domain;

using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MockTest.Server.Models;

namespace MockTest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        //Refactored

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public TestsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //public TestsController(IUnitOfWork unitofWork)
        //{
        //    //Refactored
        //    //_context = context;
        //    _unitofWork = unitofWork;
        //}

        // GET: api/Tests
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Test>>> GetTests()
        {
            //return NotFound();
            return await _context.Tests.ToListAsync();
            //var Tests = await _unitofWork.Tests.GetAll();

            //return Ok(Tests);
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<Test>> GetTest(int id)
        {

            var Test = await _context.Tests.FindAsync(id);

            if (Test == null)
            {
                return NotFound();
            }

            return Ok(Test);
        }
        //public async Task<IActionResult> GetTest(int id)
        //{

        //    var Test = await _unitofWork.Tests.Get(q => q.Id == id);

        //    if (Test == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(Test);
        //}


        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTest(int id, Test Test)
        {
            if (id != Test.Id)
            {
                return BadRequest();
            }

            _context.Entry(Test).State = EntityState.Modified;
            //_unitofWork.Tests.Update(Test);

            try
            {
                await _context.SaveChangesAsync();
                //await _unitofWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!TestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Test>> PostTest(Test Test)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);


            Test.CreatedDate = DateTime.Now;
            Test.CreatedBy = user.Email;
             _context.Tests.Add(Test);
             await _context.SaveChangesAsync();

            //await _unitofWork.Tests.Insert(Test);
            //await _unitofWork.Save(HttpContext);


            return CreatedAtAction("GetTest", new { id = Test.Id }, Test);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(int id)
        {
            var Test = await _context.Tests.FindAsync(id);
            //var Test = await _unitofWork.Tests.Get(q => q.Id == id);
            if (Test == null)
            {
                return NotFound();
            }

            _context.Tests.Remove(Test);
             await _context.SaveChangesAsync();
            //await _unitofWork.Tests.Delete(id);
            //await _unitofWork.Save(HttpContext);

            return NoContent();
        }
        //private async Task<bool> TestExists(int id)
        private bool TestExists(int id)
        {
            return _context.Tests.Any(q => q.Id == id);
            //var Test = await _unitofWork.Tests.Get(q => q.Id == id);
            //return Test != null;
        }
    }
}
