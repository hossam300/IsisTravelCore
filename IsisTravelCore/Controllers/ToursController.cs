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
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using IsisTravelCore.Models;
using Microsoft.AspNetCore.Authorization;

namespace IsisTravelCore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ToursController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        public ToursController(ApplicationDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }
        // GET: Tours
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tours.Include(t => t.Country).Include(t => t.Creator);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: Tours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours
                .Include(t => t.Country)
                .Include(t => t.TourImages)
                .Include(t => t.TourQuotes)
                .Include(t => t.TourCategories).ThenInclude(c => c.Category)
                .Include(t => t.TourCategories).ThenInclude(c => c.City)
                .Include(t => t.TourCategories).ThenInclude(c => c.CategoryDayPrices)
                .Include(t => t.TourInclude).ThenInclude(c => c.Include)
                .Include(t => t.AdditionalServices).ThenInclude(c => c.Service)
                .Include(t => t.Activities).ThenInclude(c => c.Activity)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tour == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", tour.CountryId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["ActivityId"] = new SelectList(_context.Activities, "Id", "Name");
            ViewData["IncludeId"] = new SelectList(_context.Includes, "Id", "Name");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");
            return View(tour);
        }

        // GET: Tours/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            return View();
        }

        // POST: Tours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TourName,TourMainImage,Intro,Description,OldPrice,Price,HomePage,StartDate,EndDate,Duration,CountryId,Active,CreatorId,CreatedDate,LastModifiedDate")] Tour tour)
        {

            tour.CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            tour.Active = true;
            tour.HomePage = true;
            tour.CreatedDate = DateTime.Now;
            tour.LastModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        Country country = _context.Countries.Find(tour.CountryId);
                        var file = Image;
                        string ImageName = System.IO.Path.GetFileName(file.FileName);
                        //There is an error here
                        var FilePath = Path.Combine(_appEnvironment.WebRootPath, "uploads\\" + country.Name + "/Tours");
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
                                tour.TourMainImage = "MainImage" + fileName;
                            }
                        }
                    }
                }
                _context.Add(tour);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Tours", new { id = tour.Id });
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", tour.CountryId);
            return View(tour);
        }

        // GET: Tours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", tour.CountryId);
            return View(tour);
        }

        // POST: Tours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TourName,TourMainImage,Intro,Description,OldPrice,Price,HomePage,StartDate,EndDate,Duration,CountryId,Active,CreatorId,CreatedDate,LastModifiedDate")] Tour tour)
        {

            tour.LastModifiedDate = DateTime.Now;
            if (id != tour.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var country = _context.Countries.AsNoTracking().FirstOrDefault(c => c.Id == tour.CountryId);
                    var original_Tour = _context.Tours.Include(c => c.Country).AsNoTracking().Where(P => P.Id == tour.Id).FirstOrDefault();
                    if (country.Id != original_Tour.CountryId)
                    {
                        var path = Path.Combine(_appEnvironment.WebRootPath, "uploads\\" + country.Name + "/Tours");

                        var target = Path.Combine(_appEnvironment.WebRootPath, "uploads\\" + original_Tour.Country.Name + "/Tours");
                        bool existHotels = System.IO.Directory.Exists(target);
                        if (existHotels)
                        {
                            Directory.CreateDirectory(target);
                        }
                        Directory.Move(path, target);

                    }
                    var files = HttpContext.Request.Form.Files;
                    foreach (var Image in files)
                    {
                        if (Image != null && Image.Length > 0)
                        {
                            var file = Image;
                            string ImageName = System.IO.Path.GetFileName(file.FileName);
                            //There is an error here
                            var FilePath = Path.Combine(_appEnvironment.WebRootPath, "uploads\\" + country.Name + "/Tours");
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
                                    tour.TourMainImage = "MainImage" + fileName;
                                }
                            }
                        }
                        else
                        {
                            tour.TourMainImage = original_Tour.TourMainImage;
                        }

                    }
                    _context.Update(tour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourExists(tour.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Tours", new { id = tour.Id });
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", tour.CountryId);
            return View(tour);
        }
        [HttpPost]
        public ActionResult CreateTourCategory(TourCategoryVM tourCategoryVm)
        {
            if (tourCategoryVm.Id == 0)
            {
                Category cat = new Category
                {
                    Active = true,
                    AdultPrice = (tourCategoryVm.AdultPrice == null) ? 0 : (decimal)tourCategoryVm.AdultPrice,
                    BabyPrice = (tourCategoryVm.BabyPrice == null) ? 0 : (decimal)tourCategoryVm.BabyPrice,
                    ChildPrice = (tourCategoryVm.ChildPrice == null) ? 0 : (decimal)tourCategoryVm.ChildPrice,
                    CreatedDate = DateTime.Now,
                    CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    LastModifiedDate = DateTime.Now,
                    Name = tourCategoryVm.CategoryId.ToString()
                };
                _context.SaveChanges();
                TourCategory tourCategory = new TourCategory
                {
                    Active = true,
                    CreatedDate = DateTime.Now,
                    CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    LastModifiedDate = DateTime.Now,
                    TourId = tourCategoryVm.TourId,
                    Category = cat,
                    CityId = tourCategoryVm.CityId
                };
                var tour = _context.Tours.AsNoTracking().FirstOrDefault(t => t.Id == tourCategoryVm.TourId);
                //var c = Enum.GetValues(typeof(DaysEs)).GetValue(Convert.ToInt32(Days.ToString()));

                //  var category = _context.Categories.AsNoTracking().FirstOrDefault(t => t.Id == (int)CategoryId);
                if (tour.StartDate == null)
                {
                    tour.StartDate = DateTime.Now;
                }
                if (tour.EndDate == null)
                {
                    tour.EndDate = DateTime.Now.AddMonths(6);
                }
                var dates = Enumerable.Range(0, 1 + (Convert.ToDateTime(tour.EndDate)).Subtract((Convert.ToDateTime(tour.StartDate))).Days)
                  .Select(offset => (Convert.ToDateTime(tour.StartDate)).AddDays(offset)).ToList();
                foreach (var item in dates)
                {
                    var DayList = tourCategoryVm.Days.Split(',');
                    if (DayList.Contains(((int)item.DayOfWeek).ToString()))
                    {
                        tourCategory.CategoryDayPrices.Add(new CatrgoryDayPrice
                        {
                            Active = true,
                            AdultPrice = (decimal)tourCategoryVm.AdultPrice,
                            BabyPrice = (decimal)tourCategoryVm.BabyPrice,
                            ChildPrice = (decimal)tourCategoryVm.ChildPrice,
                            SingleRoomExtrafees = (decimal)tourCategoryVm.SingleRoomExtrafees,
                            CreatedDate = DateTime.Now,
                            CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                            Date = item,
                            LastModifiedDate = DateTime.Now
                        });
                    }

                }
                _context.TourCategories.Add(tourCategory);
                _context.SaveChanges();
                var category = new CategoryTourVM
                {
                    Id = tourCategory.Id,
                    CategoryName = cat.Name,
                    CityName = _context.Cities.Find(tourCategoryVm.CityId).Name,
                    AdultPrice = tourCategory.CategoryDayPrices[0].AdultPrice,
                    ChildPrice = tourCategory.CategoryDayPrices[0].ChildPrice,
                    BabyPrice = tourCategory.CategoryDayPrices[0].BabyPrice,
                    SingleRoomExtrafees = tourCategory.CategoryDayPrices[0].SingleRoomExtrafees,
                    Active = true
                };
                return Json(category);
            }
            else
            {
                var OldtourCategory = _context.TourCategories.Find(tourCategoryVm.Id);
                _context.TourCategories.Remove(OldtourCategory);
                _context.SaveChanges();
                TourCategory tourCategory = new TourCategory
                {
                    Active = true,
                    CreatedDate = DateTime.Now,
                    CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    LastModifiedDate = DateTime.Now,
                    TourId = tourCategoryVm.TourId,
                    CategoryId = tourCategoryVm.Category,
                    CityId = tourCategoryVm.CityId
                };
                var tour = _context.Tours.AsNoTracking().FirstOrDefault(t => t.Id == tourCategoryVm.TourId);
                //var c = Enum.GetValues(typeof(DaysEs)).GetValue(Convert.ToInt32(Days.ToString()));

                //  var category = _context.Categories.AsNoTracking().FirstOrDefault(t => t.Id == (int)CategoryId);
                if (tour.StartDate == null)
                {
                    tour.StartDate = DateTime.Now;
                }
                if (tour.EndDate == null)
                {
                    tour.EndDate = DateTime.Now.AddMonths(6);
                }
                var dates = Enumerable.Range(0, 1 + (Convert.ToDateTime(tour.EndDate)).Subtract((Convert.ToDateTime(tour.StartDate))).Days)
                  .Select(offset => (Convert.ToDateTime(tour.StartDate)).AddDays(offset)).ToList();
                foreach (var item in dates)
                {
                    var DayList = tourCategoryVm.Days.Split(',');
                    if (DayList.Contains(((int)item.DayOfWeek).ToString()))
                    {
                        tourCategory.CategoryDayPrices.Add(new CatrgoryDayPrice
                        {
                            Active = true,
                            AdultPrice = (decimal)tourCategoryVm.AdultPrice,
                            BabyPrice = (decimal)tourCategoryVm.BabyPrice,
                            ChildPrice = (decimal)tourCategoryVm.ChildPrice,
                            SingleRoomExtrafees = (decimal)tourCategoryVm.SingleRoomExtrafees,
                            CreatedDate = DateTime.Now,
                            CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                            Date = item,
                            LastModifiedDate = DateTime.Now
                        });
                    }

                }
                _context.TourCategories.Add(tourCategory);
                _context.SaveChanges();
                var category = new CategoryTourVM
                {
                    Id = tourCategory.Id,
                    CategoryName = tourCategoryVm.CategoryId,
                    CityName = _context.Cities.Find(tourCategoryVm.CityId).Name,
                    AdultPrice = tourCategory.CategoryDayPrices[0].AdultPrice,
                    ChildPrice = tourCategory.CategoryDayPrices[0].ChildPrice,
                    BabyPrice = tourCategory.CategoryDayPrices[0].BabyPrice,
                    SingleRoomExtrafees = tourCategory.CategoryDayPrices[0].SingleRoomExtrafees,
                    Active = true
                };
                return Json(category);
            }


        }
        public ActionResult CreateTourActivity(TourActivityVM tourActivityVM)
        {
            if (tourActivityVM.TourActivityId == 0)
            {
                Activity activity = new Activity
                {
                    Active = true,
                    Name = tourActivityVM.ActivityId,
                    CreatedDate = DateTime.Now,
                    CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Day = Day.domingo,
                    Description = tourActivityVM.Description,
                    LastModifiedDate = DateTime.Now
                };
                TourActivity tourActivity = new TourActivity()
                {
                    Active = true,
                    Activity = activity,
                    CreatedDate = DateTime.Now,
                    CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    LastModifiedDate = DateTime.Now,
                    TourId = tourActivityVM.TourId,
                };
                _context.TourActivities.Add(tourActivity);
                _context.SaveChanges();
                return Json(tourActivity.Id);
            }
            else
            {
                Activity activity = _context.Activities.Find(tourActivityVM.Activity);
                activity.Name = tourActivityVM.ActivityId;
                activity.Description = tourActivityVM.Description;
                _context.Update(activity);
                _context.SaveChanges();
                return Json(tourActivityVM.TourActivityId);
            }

        }
        public ActionResult CreateTourService(int? ServiceId, int TourId)
        {
            TourService tourService = new TourService()
            {
                Active = true,
                ServiceId = (int)ServiceId,
                CreatedDate = DateTime.Now,
                CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                LastModifiedDate = DateTime.Now,
                TourId = TourId
            };
            _context.TourServices.Add(tourService);
            _context.SaveChanges();
            return Json(true);
        }
        public IActionResult CategoryDayTour(int id)
        {
            var TourCategory = _context.TourCategories.Include(t => t.Tour).Include(t => t.Category).Include(t => t.CategoryDayPrices).FirstOrDefault(t => t.Id == id);
            ViewBag.Tour = TourCategory.Tour.TourName;
            ViewBag.Category = TourCategory.Category.Name;
            ViewBag.TourId = TourCategory.TourId;
            CategoryDayPricesVM dayPrices = new CategoryDayPricesVM
            {
                CategoryDayPrices = TourCategory.CategoryDayPrices.Select(x =>
               new CategoryDayPriceVM
               {
                   className = (x.Date >= DateTime.Now) ? "m-fc-event--primary m-fc-event--solid-info"
                                                        : "m-fc-event--danger m-fc-event--solid-warning",
                   description = "Adult Price : " + x.AdultPrice + "\n Baby Price : " + x.BabyPrice + "\n Child Price : "
                   + x.ChildPrice,
                   id = x.Id,
                   title = "Price : " + x.AdultPrice,
                   start = x.Date,
                   end = x.Date.AddHours(8)
               }

           ).ToList(),
                DayPrice = new CatrgoryDayPrice
                {
                    TourCategoryId = id,
                    Date = DateTime.Now
                }
            };
            return View(dayPrices);
        }
        public JsonResult GetDayDetails(int id)
        {
            var day = _context.CatrgoryDayPrices.Find(id);
            return Json(day);
        }
        public ActionResult CreateTourQuote(TourQuoteVM tourQuoteVM)
        {
            if (tourQuoteVM.TourQuoteId==0)
            {
                TourQuote tourQuote = new TourQuote
                {
                    CreatedDate = DateTime.Now,
                    CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    LastModifiedDate = DateTime.Now,
                    QuoteName = tourQuoteVM.QuoteName,
                    QuotePrice = tourQuoteVM.QuotePrice,
                    TourId = tourQuoteVM.TourId
                };
                _context.TourQuotes.Add(tourQuote);
                _context.SaveChanges();
                return Json(tourQuote);
            }
            else
            {
                TourQuote tourQuote = _context.TourQuotes.Find(tourQuoteVM.TourQuoteId);
                tourQuote.QuoteName = tourQuoteVM.QuoteName;
                tourQuote.QuotePrice = tourQuoteVM.QuotePrice;
                tourQuote.LastModifiedDate = DateTime.Now;
                _context.Update(tourQuote);
                _context.SaveChanges();
                return Json(tourQuote);
            }
          
        }
        public ActionResult UploadTourImages(int? id)
        {
            Tour tour = _context.Tours.Include(t => t.Country).FirstOrDefault(t => t.Id == id);
            var files = HttpContext.Request.Form.Files;
            foreach (var Image in files)
            {
                if (Image != null && Image.Length > 0)
                {
                    var file = Image;
                    //There is an error here
                    string subPath = tour.Country.Name + "/Tours";
                    var FilePath = Path.Combine(_appEnvironment.WebRootPath, "uploads\\" + subPath);
                    bool exists = System.IO.Directory.Exists(FilePath);
                    if (!exists)
                    {
                        Directory.CreateDirectory(FilePath);
                    }
                    if (file.Length > 0)
                    {
                        var fileName = file.FileName;
                        TourImage tourImage = new TourImage
                        {
                            Active = true,
                            TourId = (int)id,
                            CreatedDate = DateTime.Now,
                            Image = file.FileName,
                            LastModifiedDate = DateTime.Now,
                        };
                        string ImageName = System.IO.Path.GetFileName(file.FileName);
                        using (var fileStream = new FileStream(Path.Combine(FilePath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            tourImage.Image = fileName;
                        }
                        _context.TourImages.Add(tourImage);
                    }
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Details", "Tours", new { id });
        }
        [HttpPost]
        public JsonResult AddDayPrice(CatrgoryDayPrice DayPrice)
        {
            DayPrice.Date.AddHours(1);
            if (DayPrice.Id != 0)
            {
                var oldDay = _context.CatrgoryDayPrices.AsNoTracking().FirstOrDefault(d => d.Id == DayPrice.Id);
                DayPrice.Active = oldDay.Active;
                DayPrice.CreatedDate = oldDay.CreatedDate;
                DayPrice.CreatorId = oldDay.CreatorId;
                DayPrice.LastModifiedDate = DateTime.Now;
                _context.Entry(DayPrice).State = EntityState.Modified;
                try
                {
                    _context.SaveChanges();
                    return Json(true);
                }
                catch (Exception)
                {
                    return Json(false);
                }
            }
            else
            {
                DayPrice.Active = true;
                DayPrice.CreatedDate = DateTime.Now;
                DayPrice.CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                DayPrice.LastModifiedDate = DateTime.Now;
                _context.CatrgoryDayPrices.Add(DayPrice);
                try
                {
                    _context.SaveChanges();
                    return Json(true);
                }
                catch (Exception)
                {
                    return Json(false);
                }
            }
        }
        public IActionResult CreateTourInclude(TourIncludeVM tourIncludeVM)
        {
            if (tourIncludeVM.TourIncludeId == 0)
            {
                Include include = new Include
                {
                    Active = true,
                    CreatedDate = DateTime.Now,
                    CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Description = tourIncludeVM.IncludeDescription,
                    LastModifiedDate = DateTime.Now,
                    Name = tourIncludeVM.IncludeId,
                };
                TourInclude tourInclude = new TourInclude()
                {
                    Active = true,
                    Include = include,
                    CreatedDate = DateTime.Now,
                    CreatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    LastModifiedDate = DateTime.Now,
                    TourId = tourIncludeVM.TourId
                };
                _context.TourIncludes.Add(tourInclude);
                _context.SaveChanges();
                return Json(tourInclude.Id);
            }
            else
            {
                Include include = _context.Includes.Find(tourIncludeVM.Include);
                include.Name = tourIncludeVM.IncludeId;
                include.Description = tourIncludeVM.IncludeDescription;
                _context.Update(include);
                _context.SaveChanges();
                return Json(tourIncludeVM.TourIncludeId);
            }
        }
        public async Task<IActionResult> Activation(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Tour tour = await _context.Tours.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            if (tour.Active)
                tour.Active = false;
            else
                tour.Active = true;
            _context.Entry(tour).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Tours");
        }
        public async Task<IActionResult> CategoriesActivation(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            TourCategory tour = await _context.TourCategories.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            if (tour.Active)
                tour.Active = false;
            else
                tour.Active = true;
            _context.Entry(tour).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Tours", new { id = tour.TourId });
        }
        public async Task<IActionResult> ActivationActivity(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            TourActivity tour = await _context.TourActivities.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            if (tour.Active)
                tour.Active = false;
            else
                tour.Active = true;
            _context.Entry(tour).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Tours", new { id = tour.TourId });
        }
        public async Task<IActionResult> ActivationIncludes(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            TourInclude tour = await _context.TourIncludes.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            if (tour.Active)
                tour.Active = false;
            else
                tour.Active = true;
            _context.Entry(tour).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Tours", new { id = tour.TourId });
        }
        public async Task<IActionResult> ActivationServices(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            TourService tour = await _context.TourServices.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            if (tour.Active)
                tour.Active = false;
            else
                tour.Active = true;
            _context.Entry(tour).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Tours", new { id = tour.TourId });
        }
        public async Task<ActionResult> TourHomePage(int? id)
        {

            if (id == null)
            {
                return BadRequest();
            }
            Tour tour = await _context.Tours.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            if (tour.HomePage)
                tour.HomePage = false;
            else
                tour.HomePage = true;
            _context.Entry(tour).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Tours");
        }
        public IActionResult TourCategoryDayPrice()
        {
            CategoryDayPricesVM dayPrices = new CategoryDayPricesVM
            {
                DayPrice = new CatrgoryDayPrice(),
                CategoryDayPrices = new List<CategoryDayPriceVM>()
            };
            ViewBag.Tours = new SelectList(_context.Tours, "Id", "TourName");
            return View(dayPrices);
        }
        public JsonResult GetCategoryByTourId(int id)
        {
            var categories = _context.TourCategories.Where(t => t.TourId == id).Select(x => new
            {
                value = x.Id,
                text = x.Category.Name
            }).ToList();
            return Json(categories);
        }
        public JsonResult CategoryDays(int id)
        {
            var TourCategory = _context.TourCategories.Include(t => t.Tour).Include(t => t.Category).Include(t => t.CategoryDayPrices).FirstOrDefault(t => t.Id == id);
            ViewBag.Tour = TourCategory.Tour.TourName;
            ViewBag.Category = TourCategory.Category.Name;
            ViewBag.TourId = TourCategory.TourId;
            CategoryDayPricesVM dayPrices = new CategoryDayPricesVM
            {
                CategoryDayPrices = TourCategory.CategoryDayPrices.Select(x =>
               new CategoryDayPriceVM
               {
                   className = (x.Date >= DateTime.Now) ? "m-fc-event--primary m-fc-event--solid-info" :
                   "m-fc-event--danger m-fc-event--solid-warning",
                   description = "Adult Price : " + x.AdultPrice + "\n Baby Price : " + x.BabyPrice + "\n Child Price : " + x.ChildPrice,
                   id = x.Id,
                   title = "Price : " + x.AdultPrice,
                   start = x.Date.AddHours(8),
                   end = x.Date.AddHours(8)
               }
           ).ToList(),
                DayPrice = new CatrgoryDayPrice
                {
                    TourCategoryId = id,
                    Date = DateTime.Now
                }
            };
            return Json(dayPrices);
        }
        public JsonResult TCategoryDays(int id)
        {
            var TourCategory = _context.TourCategories.Include(t => t.Tour).Include(t => t.Category).Include(t => t.CategoryDayPrices).FirstOrDefault(t => t.Id == id);

            CategoryDayPricesVM dayPrices = new CategoryDayPricesVM
            {
                CategoryDayPrices = TourCategory.CategoryDayPrices.Select(x =>
               new CategoryDayPriceVM
               {
                   className = (x.Date >= DateTime.Now) ? "m-fc-event--primary m-fc-event--solid-info" :
                   "m-fc-event--danger m-fc-event--solid-warning",
                   description = "Adult Price : " + x.AdultPrice + "\n Baby Price : " + x.BabyPrice + "\n Child Price : " + x.ChildPrice,
                   id = x.Id,
                   title = "Price : " + x.AdultPrice,
                   start = x.Date.AddHours(8),
                   end = x.Date.AddHours(8)
               }
           ).ToList(),
                DayPrice = new CatrgoryDayPrice
                {
                    TourCategoryId = id,
                    Date = DateTime.Now
                }
            };
            return Json(dayPrices);
        }
        public JsonResult GetTourCategory(int id)
        {
            TourCategory tourCategory = _context.TourCategories.Include(c => c.Category).Include(t => t.CategoryDayPrices)
                .FirstOrDefault(c => c.Id == id);
            TourCategoryVM tourCategoryVM = new TourCategoryVM
            {

                Id = tourCategory.Id,
                AdultPrice = tourCategory.CategoryDayPrices.FirstOrDefault().AdultPrice,
                BabyPrice = tourCategory.CategoryDayPrices.FirstOrDefault().BabyPrice,
                ChildPrice = tourCategory.CategoryDayPrices.FirstOrDefault().ChildPrice,
                CategoryId = tourCategory.Category.Name,
                CityId = tourCategory.CityId,
                SingleRoomExtrafees = tourCategory.CategoryDayPrices.FirstOrDefault().SingleRoomExtrafees,
                TourId = tourCategory.TourId,
                Days = GetDays(tourCategory.CategoryDayPrices),
                Category = tourCategory.CategoryId
            };
            return Json(tourCategoryVM);
        }
        public JsonResult GetTourActivity(int id)
        {
            TourActivityVM tourActivity = _context.TourActivities.Include(x => x.Activity).Select(x => new TourActivityVM
            {
                Activity = x.ActivityId,
                ActivityId = x.Activity.Name,
                TourActivityId = x.Id,
                Description = x.Activity.Description,
                TourId = x.TourId
            }).FirstOrDefault(x => x.TourActivityId == id);
            return Json(tourActivity);
        }
        public JsonResult GetTourInclude(int id)
        {
            TourIncludeVM tourActivity = _context.TourIncludes.Include(x => x.Include).Select(x => new TourIncludeVM
            {
                Include = x.IncludeId,
                IncludeId = x.Include.Name,
                TourIncludeId = x.Id,
                IncludeDescription = x.Include.Description,
                TourId = x.TourId
            }).FirstOrDefault(x => x.TourIncludeId == id);
            return Json(tourActivity);
        }
        public JsonResult GetTourQuote(int id)
        {
            TourQuoteVM tourActivity = _context.TourQuotes.Select(x => new TourQuoteVM
            {
                QuoteName = x.QuoteName,
                QuotePrice = x.QuotePrice,
                TourQuoteId = x.Id,
                TourId = x.TourId
            }).FirstOrDefault(x => x.TourQuoteId == id);
            return Json(tourActivity);
        }
        private static string GetDays(List<CatrgoryDayPrice> categoryDayPrices)
        {
            var days = categoryDayPrices.Select(x => (int)x.Date.DayOfWeek).Distinct().ToList();
            string daystring = "";
            foreach (var item in days)
            {
                daystring = daystring + "," + item;
            }
            return daystring.Remove(0, 1);
        }

        private bool TourExists(int id)
        {
            return _context.Tours.Any(e => e.Id == id);
        }
    }
}
