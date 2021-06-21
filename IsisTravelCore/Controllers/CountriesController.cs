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
using Microsoft.AspNetCore.Authorization;

namespace IsisTravelCore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        public CountriesController(ApplicationDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Countries.Include(c => c.Creator).Include(c => c.Hotels).Include(c => c.Tours);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .Include(c => c.CountryImages).Include(c=>c.Tours).Include(c=>c.Hotels)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CountryMainImage,Description,Active,CreatorId,CreatedDate,LastModifiedDate")] Country country)
        {
           
            country.CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            country.Active = true;
            country.CreatedDate = DateTime.Now;
            country.LastModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        //There is an error here
                        var FilePath = Path.Combine(_appEnvironment.WebRootPath, "uploads\\" + country.Name);
                        bool exists = System.IO.Directory.Exists(FilePath);
                        if (!exists)
                        {
                            Directory.CreateDirectory(FilePath);

                        }
                        if (file.Length > 0)
                        {
                            var fileName = file.FileName;
                            using (var fileStream = new FileStream(Path.Combine(FilePath, "MainImage" + fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                country.CountryMainImage = "MainImage" + fileName;
                            }
                        }
                    }
                }
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "Id", country.CreatorId);
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CountryMainImage,Description,Active,CreatorId,CreatedDate,LastModifiedDate")] Country country)
        {
          
            if (id != country.Id)
            {
                return NotFound();
            }
            country.LastModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    var original_Country = _context.Countries.AsNoTracking().Where(P => P.Id == country.Id).FirstOrDefault();
                    if (original_Country.Name != country.Name)
                    {
                        var path = Path.Combine(_appEnvironment.WebRootPath, "uploads\\" + original_Country.Name);
                        var target = Path.Combine(_appEnvironment.WebRootPath, "uploads\\" + country.Name);
                        Directory.Move(path, target);
                    }
                    var files = HttpContext.Request.Form.Files;
                    foreach (var Image in files)
                    {
                        if (Image != null && Image.Length > 0)
                        {
                            var file = Image;
                            //There is an error here
                            var FilePath = Path.Combine(_appEnvironment.WebRootPath, "uploads\\" + country.Name);
                            bool exists = System.IO.Directory.Exists(FilePath);
                            if (!exists)
                            {
                                Directory.CreateDirectory(FilePath);

                            }
                            if (file.Length > 0)
                            {
                                var fileName = file.FileName;
                                using (var fileStream = new FileStream(Path.Combine(FilePath, "MainImage" + fileName), FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);
                                    country.CountryMainImage = "MainImage" + fileName;
                                }
                            }
                        }
                    }
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
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
            return View(country);
        }

        public async Task<IActionResult> ImageActivation(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            CountryImage countryImage = await _context.CountryImages.FindAsync(id);
            if (countryImage == null)
            {
                return NotFound();
            }
            if (countryImage.Active)
                countryImage.Active = false;
            else
                countryImage.Active = true;
            _context.Entry(countryImage).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Countries", new { id = countryImage.CountryId });
        }
        public async Task<IActionResult> Activation(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Country country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            if (country.Active)
                country.Active = false;
            else
                country.Active = true;
            _context.Entry(country).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ImageSlideShow(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            CountryImage countryImage = await _context.CountryImages.FindAsync(id);
            if (countryImage == null)
            {
                return NotFound();
            }
            if (countryImage.SlideShow)
                countryImage.SlideShow = false;
            else
                countryImage.SlideShow = true;
            _context.Entry(countryImage).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Countries", new { id = countryImage.CountryId });
        }
        public ActionResult UploadCountryImages(int? id)
        {
            Country country = _context.Countries.Find(id);
            var files = HttpContext.Request.Form.Files;
            foreach (var Image in files)
            {
                if (Image != null && Image.Length > 0)
                {
                    var file = Image;
                    //There is an error here
                    string subPath = country.Name + "//Images";
                    var FilePath = Path.Combine(_appEnvironment.WebRootPath, "uploads\\" + subPath);
                    bool exists = System.IO.Directory.Exists(FilePath);
                    if (!exists)
                    {
                        Directory.CreateDirectory(FilePath);

                    }
                    if (file.Length > 0)
                    {
                        var fileName = file.FileName;
                        CountryImage countryImage = new CountryImage
                        {
                            Active = true,
                            CountryId = (int)id,
                            CreatedDate = DateTime.Now,
                            Image = file.FileName,
                            LastModifiedDate = DateTime.Now,
                            SlideShow = true
                        };
                        _context.CountryImages.Add(countryImage);
                        string ImageName = System.IO.Path.GetFileName(file.FileName);
                        using (var fileStream = new FileStream(Path.Combine(FilePath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            countryImage.Image = fileName;
                        }
                    }
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Details", "Countries", new { id });
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
