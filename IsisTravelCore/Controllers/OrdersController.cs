using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IsisTravelCore.Data;
using IsisTravelCore.Models.Domains;
using IsisTravelCore.Services;
using System.IO;
using Microsoft.AspNetCore.Hosting;


namespace IsisTravelCore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IHostingEnvironment _appEnvironment;
      //  public IJsReportMVCService JsReportMVCService { get; }
        public OrdersController(ApplicationDbContext context,/* IJsReportMVCService jsReportMVCService, */IHostingEnvironment appEnvironment)
        {
            _context = context;
         //   JsReportMVCService = jsReportMVCService;
            _appEnvironment = appEnvironment;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(o => o.Creator);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderNumber,Name,City,Address,CP,Country,NIE,Phone,Population,OrderDate,ItemName1,Quantity1,Price1,Discount1,ItemName2,Quantity2,Price2,Discount2,ItemName3,Quantity3,Price3,Discount3,ItemName4,Quantity4,Price4,Discount4,Active,CreatorId,CreatedDate,LastModifiedDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                //   CreatePDF(order);
            //    return RedirectToAction("Invoice", order);
            }
            ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "Id", order.CreatorId);
            return View(order);

        }
        //[MiddlewareFilter(typeof(JsReportPipeline))]
        //public IActionResult Invoice(Order order)
        //{
        //    HttpContext.JsReportFeature().Recipe(Recipe.ChromePdf).OnAfterRender((r) =>
        //    {
        //        using (var file = System.IO.File.Open(Path.Combine(_appEnvironment.WebRootPath, $"PDFCreator\\report{order.Id}.pdf"), FileMode.Create))
        //        {
        //            r.Content.CopyTo(file);
        //        }
        //        r.Content.Seek(0, SeekOrigin.Begin);
        //        EmailService emailService = new EmailService();
        //        var file2 = Path.Combine(_appEnvironment.WebRootPath, $"PDFCreator\\report{order.Id}.pdf");
        //        emailService.SendEmailAsync(order.Name, "semsem9000@gmail.com", "reserva completada", file2, "");
        //    });


        //    return View("Invoice", order);
        //}
        //[MiddlewareFilter(typeof(JsReportPipeline))]
        //public IActionResult InvoiceDownload(Order order)
        //{
        //    HttpContext.JsReportFeature().Recipe(Recipe.ChromePdf);
        //    HttpContext.JsReportFeature().OnAfterRender((r) =>
        //    {
        //        using (var file = System.IO.File.Open("report.pdf", FileMode.Create))
        //        {
        //            r.Content.CopyTo(file);
        //        }
        //        r.Content.Seek(0, SeekOrigin.Begin);
        //    });

        //    return View("Invoice", order);
        //}

        //[MiddlewareFilter(typeof(JsReportPipeline))]
        //public async Task<IActionResult> InvoiceWithHeader(Order order)
        //{
        //    var header = await JsReportMVCService.RenderViewToStringAsync(HttpContext, RouteData, "Header", new { });

        //    HttpContext.JsReportFeature()
        //        .Recipe(Recipe.ChromePdf)
        //        .Configure((r) => r.Template.Chrome = new Chrome
        //        {
        //            HeaderTemplate = header,
        //            DisplayHeaderFooter = true,
        //            MarginTop = "1cm",
        //            MarginLeft = "1cm",
        //            MarginBottom = "1cm",
        //            MarginRight = "1cm"
        //        });

        //    return View("Invoice", order);
        //}

        //[MiddlewareFilter(typeof(JsReportPipeline))]
        //public IActionResult Items(Order order)
        //{
        //    HttpContext.JsReportFeature()
        //        .Recipe(Recipe.HtmlToXlsx)
        //        .Configure((r) => r.Template.HtmlToXlsx = new HtmlToXlsx() { HtmlEngine = "chrome" });

        //    return View(order);
        //}

        //[MiddlewareFilter(typeof(JsReportPipeline))]
        //public IActionResult ItemsExcelOnline(Order order)
        //{
        //    HttpContext.JsReportFeature()
        //        .Configure(req => req.Options.Preview = true)
        //        .Recipe(Recipe.HtmlToXlsx)
        //        .Configure((r) => r.Template.HtmlToXlsx = new HtmlToXlsx() { HtmlEngine = "chrome" });

        //    return View("Items", order);
        //}

        //[MiddlewareFilter(typeof(JsReportPipeline))]
        //public IActionResult InvoiceDebugLogs(Order order)
        //{
        //    HttpContext.JsReportFeature()
        //        .DebugLogsToResponse()
        //        .Recipe(Recipe.ChromePdf);

        //    return View("Invoice", order);
        //}
        //public void CreatePDF(Order order)
        //{
        //    var globalSettings = new GlobalSettings
        //    {
        //        ColorMode = ColorMode.Color,
        //        Orientation = Orientation.Portrait,
        //        PaperSize = PaperKind.A4,
        //        Margins = new MarginSettings { Top = 10 },
        //        DocumentTitle = "PDF _ FACTURA",
        //        Out = Path.Combine(_appEnvironment.WebRootPath,string.Format(@"PDFCreator\{0}_FACTURA.pdf", DateTime.Now.Year + "_" + order.Id))
        //    };

        //    var objectSettings = new ObjectSettings
        //    {
        //        PagesCount = true,
        //        HtmlContent = TemplateGenerator.GetHTMLString(order),
        //        WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css"), LoadImages = true },
        //        HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
        //        FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
        //    };

        //    var pdf = new HtmlToPdfDocument()
        //    {
        //        GlobalSettings = globalSettings,
        //        Objects = { objectSettings }
        //    };

        //    _converter.Convert(pdf);
        //}
    }
}
