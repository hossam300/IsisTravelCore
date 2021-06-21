using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using IsisTravelCore.Models.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using IsisTravelCore.Data;

namespace IsisTravelCore.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class UserRegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        public UserRegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger, RoleManager<IdentityRole> roleManager, ApplicationDbContext context,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        public int? RequestId { get; set; }
        public class InputModel
        {
            [Required]
            [Display(Name = "Nombre de usuario")]
            public string UserName { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} al menos debe ser {2} y al máximo {1} personajes largos.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar contraseña")]
            [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
            public string ConfirmPassword { get; set; }
            [Display(Name = "Nombre de pila")]
            [Required]
            public string FirstName { get; set; }
            [Display(Name = "Apellido")]
            public string LastName { get; set; }
            [Display(Name = "Género")]
            public string Gender { get; set; }
            [Display(Name = "Fecha de nacimiento")]
            public DateTime Birthdate { get; set; }
            [Display(Name = "Teléfono")]
            [Required]
            public string Phone { get; set; }
            [Display(Name = "Dirección")]
            public string Address { get; set; }
            public int? RequestId { get; set; }
        }

        public void OnGet(int? id, string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            RequestId = id;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.UserName,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    Address = Input.Address,
                    Birthdate = Input.Birthdate,
                    CreationDate = DateTime.Now,
                    Gender = Input.Gender,
                    LastName = Input.LastName,
                    Phone = Input.Phone,
                    IsActive = true,
                    LastModifiedDate=DateTime.Now,
                    LockoutEnabled = true
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync("User").Result)
                    {
                        var identityResult = await _roleManager.CreateAsync(new IdentityRole("User"));
                    }
                    await _userManager.AddToRoleAsync(user, "User");
                    if (Input.RequestId != null)
                    {
                        var request = _context.RequestOffers.Find((int)Input.RequestId);
                        request.CreatorId = user.Id;
                        _context.Update(request);
                        _context.SaveChanges();
                        await _signInManager.SignInAsync(user, true);
                        return RedirectToAction("SendMail", "Home", new { id = Input.RequestId, UserId = user.Id });
                    }
                    await _signInManager.SignInAsync(user, true);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
