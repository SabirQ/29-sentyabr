using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserRegistrationMvc.DataContext;
using UserRegistrationMvc.Filters;
using UserRegistrationMvc.Models;

namespace UserRegistrationMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly Context _context;
        private const string LOGIN_SESSION_KEY = "login";

        public UsersController(Context context)
        {
            _context = context;
        }

        // GET: Admin/Users
        [Custom(RoleEnum.Moderator)]
        public async Task<IActionResult> Index()
        {
          
            var users =await _context.Users.Include(u => u.Role).ToListAsync();

            return View(users);
        }
        [Custom(RoleEnum.Admin)]
        public async Task<IActionResult> Edit(int? id)
        {
           
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var existed = await _context.Users.FindAsync(id);
            if (existed == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", existed.RoleId);
            return View(existed);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Username,Password,RoleId")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

          
                try
                {
                var existed = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                  existed.RoleId=user.RoleId;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", user.RoleId);
            return View(user);
        }

        private bool UserExists(int id)
        {
          return _context.Users.Any(e => e.Id == id);
        }
    }
}
