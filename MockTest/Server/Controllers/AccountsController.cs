using MockTest.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using MockTest.Server.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
namespace MockTest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        UserManager<ApplicationUser> _userManager;
        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }
        //public AccountsController(ApplicationDbContext context,
        
        //UserManager<ApplicationUser> userManager)
        //{
        //    _context = context;
        //    _userManager = userManager;
        //}
        /// <summary>
        /// Retrieve all the users
        /// </summary>
        /// <returns>ClientApplicationUser Objects in a List</returns>
        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var usersList = await _context.ApplicationUsers.ToListAsync();
            return Ok(usersList);
            //var appUserList = await _context.ApplicationUsers.ToListAsync();
            //List<ClientApplicationUser> clientAppUserList = new List<ClientApplicationUser>();
            //foreach (var appUser in appUserList)
            //{
            //    ClientApplicationUser clientAppUser = new ClientApplicationUser(appUser);
            //    clientAppUserList.Add(clientAppUser);
            //}
            //return Ok(clientAppUserList);
        }
        /// <summary>
        /// Retrieve the role information based on user id
        /// </summary>
        /// <returns>Role in a string</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(string id)
        {
            var appUserList = await _context.ApplicationUsers.ToListAsync();
            foreach (var appUser in appUserList)
            {
                if (appUser.Id == id)
                {
                    var UserResult = await _userManager.IsInRoleAsync(appUser,
                    
                    "Administrator");
                    if (UserResult)
                    {
                        return Ok("Administrator");
                    }
                    UserResult = await _userManager.IsInRoleAsync(appUser, "User");
                    if (UserResult)
                    {
                        return Ok("User");
                    }
                }
            }
            return NotFound();
        }
    }
}