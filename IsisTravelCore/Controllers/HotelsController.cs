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
    public class HotelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        public HotelsController(ApplicationDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Hotels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Hotels.Include(h => h.Category).Include(h => h.Country).Include(h => h.Creator);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Hotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels
                .Include(h => h.Category)
                .Include(h => h.Country)
                .Include(h => h.HotelImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", hotel.CategoryId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", hotel.CountryId);
            return View(hotel);
        }

        // GET: Hotels/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,HotelMainImage,Address,Phone,Description,ExtelnalLink,FacebookLink,TwitterLink,Lat,Lang,CountryId,CategoryId,Active,CreatorId,CreatedDate,LastModifiedDate")] Hotel hotel)
        {

            hotel.CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            hotel.Active = true;
            hotel.CreatedDate = DateTime.Now;
            hotel.LastModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        Country country = _context.Countries.Find(hotel.CountryId);
                        var file = Image;
                        string ImageName = System.IO.Path.GetFileName(file.FileName);
                        //There is an error here
                        var FilePath = Path.Combine(_appEnvironment.WebRootPath, "uploads\\" + country.Name + "/Hotels");
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
                                hotel.HotelMainImage = "MainImage" + fileName;
                            }
                        }
                    }
                }
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", hotel.CategoryId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", hotel.CountryId);
            return View(hotel);
        }

        // GET: Hotels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels.Include(h => h.Country).FirstOrDefaultAsync(h => h.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", hotel.CategoryId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", hotel.CountryId);
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,HotelMainImage,Address,Phone,Description,ExtelnalLink,FacebookLink,TwitterLink,Lat,Lang,CountryId,CategoryId,Active,CreatorId,CreatedDate,LastModifiedDate")] Hotel hotel)
        {

            hotel.LastModifiedDate = DateTime.Now;
            if (id != hotel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var country = _context.Countries.AsNoTracking().FirstOrDefault(c => c.Id == hotel.CountryId);
                    var original_Hotel = _context.Hotels.AsNoTracking().Where(P => P.Id == hotel.Id).FirstOrDefault();
                    if (country.Id != original_Hotel.CountryId)
                    {
                        Directory.CreateDirectory(Path.Combine(_appEnvironment.WebRootPath, "uploads\\" + country.Name + "/Hotels/"));

                    }
                    var files = HttpContext.Request.Form.Files;
                    foreach (var Image in files)
                    {
                        if (Image != null && Image.Length > 0)
                        {
                            var file = Image;
                            string ImageName = System.IO.Path.GetFileName(file.FileName);
                            //There is an error here
                            var FilePath = Path.Combine(_appEnvironment.WebRootPath, "uploads\\" + country.Name + "/Hotels");
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
                                    hotel.HotelMainImage = "MainImage" + fileName;
                                }
                            }
                        }
                        else
                        {
                            hotel.HotelMainImage = original_Hotel.HotelMainImage;
                        }

                    }
                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", hotel.CategoryId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", hotel.CountryId);
            return View(hotel);
        }

        public async Task<ActionResult> Activation(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Hotel hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            if (hotel.Active)
                hotel.Active = false;
            else
                hotel.Active = true;
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Hotels");
        }

        public async Task<ActionResult> ImageActivation(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            HotelImage hotelImage = await _context.HotelImage.FindAsync(id);
            if (hotelImage == null)
            {
                return NotFound();
            }
            if (hotelImage.Active)
                hotelImage.Active = false;
            else
                hotelImage.Active = true;
            _context.Entry(hotelImage).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Hotels", new { id = hotelImage.HotelId });
        }
        public async Task<ActionResult> ImageHome(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            HotelImage hotelImage = await _context.HotelImage.FindAsync(id);
            if (hotelImage == null)
            {
                return NotFound();
            }
            if (hotelImage.IsHome)
                hotelImage.IsHome = false;
            else
                hotelImage.IsHome = true;
            _context.Entry(hotelImage).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Hotels", new { id = hotelImage.HotelId });
        }

        public ActionResult UploadHotelImages(int? id)
        {
            string fName = "";
            Hotel hotel = _context.Hotels.Find(id);
            Country country = _context.Countries.Find(hotel.CountryId);
            foreach (var file in HttpContext.Request.Form.Files)
            {
                //Save file content goes here
                fName = file.FileName;
                if (file != null && file.Length > 0)
                {

                    HotelImage hotelImage = new HotelImage
                    {
                        Active = true,
                        HotelId = (int)id,
                        CreatedDate = DateTime.Now,
                        CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                        Image = file.FileName,
                        LastModifiedDate = DateTime.Now,
                        IsHome = true
                    };
                    _context.HotelImage.Add(hotelImage);
                    string FilePath = Path.Combine(_appEnvironment.WebRootPath, "uploads\\" + country.Name + "\\Hotels");
                    bool isExists = Directory.Exists(FilePath);
                    if (!isExists)
                    {
                        Directory.CreateDirectory(FilePath);
                    }
                    var path = string.Format("{0}\\{1}", FilePath, file.FileName);
                    using (var fileStream = new FileStream(Path.Combine(FilePath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                        hotelImage.Image = file.FileName;
                    }
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Details", "Hotels", new { id });
        }
        private bool HotelExists(int id)
        {
            return _context.Hotels.Any(e => e.Id == id);
        }
    }
}
