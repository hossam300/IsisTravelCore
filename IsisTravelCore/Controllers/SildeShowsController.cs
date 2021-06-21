using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IsisTravelCore.Data;
using IsisTravelCore.Models.Domains;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;

namespace IsisTravelCore.Controllers
{
    public class SildeShowsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        public SildeShowsController(ApplicationDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: SildeShows
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SildeShows.Include(s => s.Creator).Include(s => s.Tour);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SildeShows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sildeShow = await _context.SildeShows
                .Include(s => s.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sildeShow == null)
            {
                return NotFound();
            }
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "TourName", sildeShow.TourId);
            return View(sildeShow);
        }

        // GET: SildeShows/Create
        public IActionResult Create()
        {
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "TourName");
            return View();
        }

        // POST: SildeShows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,TourId,Active,CreatorId,CreatedDate,LastModifiedDate")] SildeShow sildeShow)
        {
            
            sildeShow.CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            sildeShow.Active = true;
            sildeShow.CreatedDate = DateTime.Now;
            sildeShow.LastModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        //There is an error here
                        var FilePath = Path.Combine(_appEnvironment.WebRootPath, "uploads\\SildeShows");
                        bool exists = System.IO.Directory.Exists(FilePath);
                        if (!exists)
                        {
                            Directory.CreateDirectory(FilePath);

                        }
                        if (file.Length > 0)
                        {
                            var fileName = file.FileName;
                            using (var fileStream = new FileStream(Path.Combine(FilePath, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                sildeShow.Image = fileName;
                            }
                        }
                    }
                }
                _context.Add(sildeShow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "TourName", sildeShow.TourId);
            return View(sildeShow);
        }

        // GET: SildeShows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var sildeShow = await _context.SildeShows.FindAsync(id);
            if (sildeShow == null)
            {
                return NotFound();
            }
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "TourName", sildeShow.TourId);
            return View(sildeShow);
        }

        // POST: SildeShows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,TourId,Active,CreatorId,CreatedDate,LastModifiedDate")] SildeShow sildeShow)
        {
           
            sildeShow.LastModifiedDate = DateTime.Now;
            if (id != sildeShow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var files = HttpContext.Request.Form.Files;
                    foreach (var Image in files)
                    {
                        if (Image != null && Image.Length > 0)
                        {
                            var file = Image;
                            //There is an error here
                            var FilePath = Path.Combine(_appEnvironment.WebRootPath, "uploads\\SildeShows");
                            bool exists = System.IO.Directory.Exists(FilePath);
                            if (!exists)
                            {
                                Directory.CreateDirectory(FilePath);

                            }
                            if (file.Length > 0)
                            {
                                var fileName = file.FileName;
                                using (var fileStream = new FileStream(Path.Combine(FilePath, fileName), FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);
                                    sildeShow.Image = fileName;
                                }
                            }
                        }
                    }
                    _context.Update(sildeShow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SildeShowExists(sildeShow.Id))
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
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "TourName", sildeShow.TourId);
            return View(sildeShow);
        }

        public async Task<IActionResult> Activation(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            SildeShow sildeShow = await _context.SildeShows.FindAsync(id);
            if (sildeShow == null)
            {
                return NotFound();
            }
            if (sildeShow.Active)
                sildeShow.Active = false;
            else
                sildeShow.Active = true;
            _context.Entry(sildeShow).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool SildeShowExists(int id)
        {
            return _context.SildeShows.Any(e => e.Id == id);
        }
    }
}
