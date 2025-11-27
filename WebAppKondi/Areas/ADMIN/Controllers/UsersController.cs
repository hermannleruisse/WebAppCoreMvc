using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreMVC.Areas.ADMIN.Interfaces;
using WebAppCoreMVC.Entities;
using WebAppCoreMVC.Helpers;
using WebAppCoreMVC.Models;
using WebAppCoreMVC.Services;
using BCryptNet = BCrypt.Net.BCrypt;

namespace WebAppCoreMVC.Areas.ADMIN.Controllers
{
    [Area("ADMIN")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;

        public UsersController(IUserService userService, ApplicationDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        // GET: ADMIN/Users
        public async Task<IActionResult> Index()
        {
            var users = await _context
                .Users
                .Include(u => u.Role)
                .ToListAsync();
            return View(users);
        }

        // GET: ADMIN/Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.ObtenirElement(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: ADMIN/Users/Create
        public IActionResult Create()
        {
            var model = new UserViewModel
            {
                Roles = _context.Roles.Select(r => new SelectListItem { Text = r.Name, Value = r.Id.ToString() }).ToList()
            };
            return View(model);
        }

        // POST: ADMIN/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Username,SelectedRole,Password")] UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User();
                user.Username = userViewModel.Username;
                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;
                user.Password = BCryptNet.HashPassword(userViewModel.Password);
                user.Role = _context.Roles.Find(Convert.ToInt32(userViewModel.SelectedRole));

                await _userService.EnregistrerElement(user);
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }

        // GET: ADMIN/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Roles = _context.Roles.Select(r => new SelectListItem { Text = r.Name, Value = r.Id.ToString() }).ToList(),
                SelectedRole = user.Role == null ? "": user.Role.Id.ToString()
            };

            return View(userViewModel);
        }

        // POST: ADMIN/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Username,SelectedRole")] UserViewModel userViewModel)
        {
            if (id != userViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = _context.Users.FirstOrDefault(u => u.Id == id);
                    user.Username = userViewModel.Username;
                    user.FirstName = userViewModel.FirstName;
                    user.LastName = userViewModel.LastName;
                    //user.Password = BCryptNet.HashPassword(userViewModel.Password);
                    user.Role = _context.Roles.FirstOrDefault(r => r.Id == Convert.ToInt32(userViewModel.SelectedRole));

                    await _userService.MiseAjourElement(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }

        // GET: ADMIN/Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.ObtenirElement(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: ADMIN/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userService.ObtenirElement(id);
            if (user != null)
            {
                await _userService.SupprimerElement(user);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return (_userService.ObtenirElement(id).Result != null);
            //return _context.Users.Any(e => e.Id == id);
        }
    }
}
