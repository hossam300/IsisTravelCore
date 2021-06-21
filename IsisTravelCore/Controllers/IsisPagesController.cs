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
    public class IsisPagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        public IsisPagesController(ApplicationDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: IsisPages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.IsisPages.Include(i => i.Creator);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: IsisPages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isisPage = await _context.IsisPages
                .Include(i => i.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (isisPage == null)
            {
                return NotFound();
            }

            return View(isisPage);
        }

        // GET: IsisPages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IsisPages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,PageCatgory,Active,CreatorId,CreatedDate,LastModifiedDate")] IsisPage isisPage)
        {
            isisPage.Active = true;
            isisPage.CreatedDate = DateTime.Now;
            isisPage.LastModifiedDate = DateTime.Now;
            isisPage.CreatorId= this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                _context.Add(isisPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(isisPage);
        }
        [HttpPost]
        public async Task<string> UploadImages()
        {
            var files = HttpContext.Request.Form.Files;
            var url = "";
            foreach (var Image in files)
            {
                if (Image != null && Image.Length > 0)
                {
                    var file = Image;
                    //There is an error here
                    var FilePath = Path.Combine(_appEnvironment.WebRootPath, "uploads\\IsisPages");
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
                            url = fileName;
                        }
                    }
                }
            }
            return url;
        }
        // GET: IsisPages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isisPage = await _context.IsisPages.FindAsync(id);
            if (isisPage == null)
            {
                return NotFound();
            }
            return View(isisPage);
        }

        // POST: IsisPages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,PageCatgory,Active,CreatorId,CreatedDate,LastModifiedDate")] IsisPage isisPage)
        {
            if (id != isisPage.Id)
            {
                return NotFound();
            }
            isisPage.LastModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(isisPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IsisPageExists(isisPage.Id))
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
            return View(isisPage);
        }

        // GET: IsisPages/Delete/5
        public async Task<IActionResult> Activation(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            IsisPage isisPage = await _context.IsisPages.FindAsync(id);
            if (isisPage == null)
            {
                return NotFound();
            }
            if (isisPage.Active)
                isisPage.Active = false;
            else
                isisPage.Active = true;
            _context.Entry(isisPage).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool IsisPageExists(int id)
        {
            return _context.IsisPages.Any(e => e.Id == id);
        }
    }
}
