using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IsisTravelCore.Data;
using IsisTravelCore.Models.Domains;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace IsisTravelCore.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        public FeaturesController(ApplicationDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Features
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Features.Include(f => f.Tour);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Features/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feature = await _context.Features
                .Include(f => f.Tour)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feature == null)
            {
                return NotFound();
            }

            return View(feature);
        }

        // GET: Features/Create
        public IActionResult Create()
        {
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "TourName");
            return View();
        }

        // POST: Features/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TourId,Icon,Title,Description")] Feature feature)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        //There is an error here
                        var FilePath = Path.Combine(_appEnvironment.WebRootPath, "uploads\\Features");
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
                                feature.Icon = fileName;
                            }
                        }
                    }
                }
                _context.Add(feature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "TourName", feature.TourId);
            return View(feature);
        }

        // GET: Features/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feature = await _context.Features.FindAsync(id);
            if (feature == null)
            {
                return NotFound();
            }
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "TourName", feature.TourId);
            return View(feature);
        }

        // POST: Features/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TourId,Icon,Title,Description")] Feature feature)
        {
            if (id != feature.Id)
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
                            var FilePath = Path.Combine(_appEnvironment.WebRootPath, "uploads\\Features");
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
                                    feature.Icon = fileName;
                                }
                            }
                        }
                    }
                    _context.Update(feature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeatureExists(feature.Id))
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
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "TourName", feature.TourId);
            return View(feature);
        }

        // GET: Features/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feature = await _context.Features
                .Include(f => f.Tour)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feature == null)
            {
                return NotFound();
            }

            return View(feature);
        }

        // POST: Features/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feature = await _context.Features.FindAsync(id);
            _context.Features.Remove(feature);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeatureExists(int id)
        {
            return _context.Features.Any(e => e.Id == id);
        }
    }
}
