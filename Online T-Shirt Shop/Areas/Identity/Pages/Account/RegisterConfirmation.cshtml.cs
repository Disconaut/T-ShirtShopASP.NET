using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Online_T_Shirt_Shop.Areas.Identity.Data;
using Online_T_Shirt_Shop.Areas.Identity.Services.EmailSender;

namespace Online_T_Shirt_Shop.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<Consumer> _userManager;
        private readonly IEmailSender _sender;
        private readonly MessageTemplateService _messageService;

        public RegisterConfirmationModel(UserManager<Consumer> userManager, IEmailSender sender, MessageTemplateService messageService)
        {
            _userManager = userManager;
            _sender = sender;
            _messageService = messageService;
        }

        public string Email { get; set; }

        public string ReturnUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string email)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            ReturnUrl = Url.RouteUrl("default", new {Controller = "Shop", Action = "Index"});
            Email = email;
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var url = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = HttpUtility.UrlEncode(code) },
                protocol: Request.Scheme);
            await _sender.SendEmailAsync(
                email,
                "Confirm your email",
                _messageService.GetMessageHtml(url, user?.UserName));

            return Page();
        }
    }
}
