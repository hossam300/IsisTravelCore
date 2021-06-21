using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IsisTravelCore.Data;
using IsisTravelCore.Models.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace IsisTravelCore.Areas.Identity.Pages.Account
{
    public class CompanyUserModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
      //  private readonly IEmailSender _emailSender;
        private RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        public CompanyUserModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger, RoleManager<IdentityRole> roleManager, ApplicationDbContext context
            //,
            //IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
           // _emailSender = emailSender;
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
                    IsActive = false,
                    LastModifiedDate = DateTime.Now,
                    LockoutEnabled = false
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync("CompanyUser").Result)
                    {
                        var identityResult = await _roleManager.CreateAsync(new IdentityRole("CompanyUser"));
                    }
                    await _userManager.AddToRoleAsync(user, "CompanyUser");
                   
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