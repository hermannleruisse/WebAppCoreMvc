using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCoreMVC.Areas.ADMIN.Interfaces;
using WebAppCoreMVC.Entities;
using WebAppCoreMVC.Models;
using WebAppCoreMVC.Helpers;

namespace WebAppCoreMVC.Areas.ADMIN.Controllers
{
    [Area("ADMIN")]
    public class RolesController : Controller
    {
        private readonly IRepository<Role> _roleRepo;
        private readonly IRepository<Permission> _permissionRepo;
        private readonly ApplicationDbContext _context;

        public RolesController(IRepository<Permission> permissionRepo, IRepository<Role> roleRepo, ApplicationDbContext context)
        {
            _roleRepo = roleRepo;
            _permissionRepo = permissionRepo;
            _context = context;
        }

        // GET: ADMIN/Roles
        public async Task<IActionResult> Index()
        {
            var roles = await _context.Roles
            .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
            .ToListAsync();
            return View(roles);
        }

        // GET: ADMIN/Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: ADMIN/Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ADMIN/Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                _context.Add(role);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: ADMIN/Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var role = await _context.Roles
                .Include (r => r.RolePermissions)
                .FirstOrDefaultAsync (r => r.Id == id);

            if (role == null)
            {
                return NotFound();
            }

            var allPermissions = await _context.Permissions.ToListAsync();
            var viewModel = new EditRoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
                AvailablePermissions = allPermissions.Select(p => new PermissionCheckboxItem
                {
                    Id = p.Id,
                    Name = p.Name,
                    IsSelected = role.RolePermissions.Any(rp => rp.PermissionId == p.Id)
                }).ToList()
            };
            return View(viewModel);
        }

        // POST: ADMIN/Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var role = await _context.Roles
                .Include (r => r.RolePermissions)
                .FirstOrDefaultAsync(r => r.Id == model.Id);

            if (role == null)
            {
                return NotFound();
            }

            // Mise à jour du nom
            role.Name = model.Name;

            // Mise à jour des permissions
            // Supprimer celles qui ne sont plus sélectionnées
            var existingPermissionIds = role.RolePermissions.Select(rp => rp.PermissionId);
            var toRemove = role.RolePermissions.Where(rp => !model.SelectedPermissionIds.Contains(rp.PermissionId)).ToList();
            _context.RolePermissions.RemoveRange(toRemove);

            // Ajouter les nouvelles permissions sélectionnées
            var toAdd = model.SelectedPermissionIds
                .Where(pid => !existingPermissionIds.Contains(pid))
                .Select(pid => new RolePermission { RoleId = role.Id, PermissionId = pid });

            await _context.RolePermissions.AddRangeAsync(toAdd);
            
            await _context.SaveChangesAsync();
                
            return RedirectToAction(nameof(Index));
        }

        // GET: ADMIN/Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: ADMIN/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await _context.Roles
                .Include(r => r.RolePermissions)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (role == null)
                return NotFound();

            // Supprimer les relations avec les permissions
            _context.RolePermissions.RemoveRange(role.RolePermissions);
            // Supprimer le rôle
            _context.Roles.Remove(role);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
