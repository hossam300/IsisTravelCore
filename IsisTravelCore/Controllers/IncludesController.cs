using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IsisTravelCore.Data;
using IsisTravelCore.Models.Domains;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace IsisTravelCore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class IncludesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        public IncludesController(ApplicationDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Includes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Includes.Include(i => i.Creator);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Includes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var include = await _context.Includes
                .Include(i => i.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (include == null)
            {
                return NotFound();
            }

            return View(include);
        }

        // GET: Includes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Includes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Icon,Price,Active,CreatorId,CreatedDate,LastModifiedDate")] Include include)
        {
            include.CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            include.Active = true;
            include.CreatedDate = DateTime.Now;
            include.LastModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(include);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(include);
        }

        // GET: Includes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var include = await _context.Includes.FindAsync(id);
            if (include == null)
            {
                return NotFound();
            }
            return View(include);
        }

        // POST: Includes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Icon,Price,Active,CreatorId,CreatedDate,LastModifiedDate")] Include include)
        {
            include.LastModifiedDate = DateTime.Now;
            if (id != include.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(include);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncludeExists(include.Id))
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
            ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "Id", include.CreatorId);
            return View(include);
        }

        public async Task<IActionResult> Activation(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Include include = await _context.Includes.FindAsync(id);
            if (include == null)
            {
                return NotFound();
            }
            if (include.Active)
                include.Active = false;
            else
                include.Active = true;
            _context.Entry(include).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private bool IncludeExists(int id)
        {
            return _context.Includes.Any(e => e.Id == id);
        }
    }
}
