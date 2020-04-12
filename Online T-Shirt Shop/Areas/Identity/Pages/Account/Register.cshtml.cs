using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Online_T_Shirt_Shop.Areas.Identity.Data;

namespace Online_T_Shirt_Shop.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Consumer> _signInManager;
        private readonly UserManager<Consumer> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        private readonly string _msgHtml = @"<center class=""wrapper"" data-link-color=""#1188E6"" data-body-style=""font-size:14px; font-family:arial,helvetica,sans-serif; color:#000000; background-color:#FFFFFF;"">
        <div class=""webkit"">
          <table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"" class=""wrapper"" bgcolor=""#FFFFFF"">
            <tr>
              <td valign=""top"" bgcolor=""#FFFFFF"" width=""100%"">
                <table width=""100%"" role=""content-container"" class=""outer"" align=""center"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                  <tr>
                    <td width=""100%"">
                      <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                        <tr>
                          <td>
                            <!--[if mso]>
    <center>
    <table><tr><td width=""600"">
  <![endif]-->
                                    <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""width:100%; max-width:600px;"" align=""center"">
                                      <tr>
                                        <td role=""modules-container"" style=""padding:0px 0px 0px 0px; color:#000000; text-align:left;"" bgcolor=""#FFFFFF"" width=""100%"" align=""left""><table class=""module preheader preheader-hide"" role=""module"" data-type=""preheader"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""display: none !important; mso-hide: all; visibility: hidden; opacity: 0; color: transparent; height: 0; width: 0;"">
    <tr>
      <td role=""module-content"">
        <p></p>
      </td>
    </tr>
  </table><table class=""module"" role=""module"" data-type=""text"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""table-layout: fixed;"" data-muid=""b88cf84f-2918-404f-bd74-619036c16ccf"" data-mc-module-version=""2019-10-22"">
    <tbody>
      <tr>
        <td style=""padding:18px 0px 18px 0px; line-height:28px; text-align:inherit; background-color:#ff6800;"" height=""100%"" valign=""top"" bgcolor=""#ff6800"" role=""module-content""><div><div style=""font-family: inherit; text-align: center""><br></div>
<div style=""font-family: inherit; text-align: center""><span style=""font-size: 70px; font-family: arial, helvetica, sans-serif; color: #ffffff; background-color: rgb(0, 0, 0)"">&nbsp;&nbsp;VETOL &nbsp;</span></div><div></div></div></td>
      </tr>
    </tbody>
  </table><table class=""module"" role=""module"" data-type=""spacer"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""table-layout: fixed;"" data-muid=""badcde9d-3139-4325-8a68-731c7aba4368"">
    <tbody>
      <tr>
        <td style=""padding:0px 0px 50px 0px;"" role=""module-content"" bgcolor="""">
        </td>
      </tr>
    </tbody>
  </table><table class=""module"" role=""module"" data-type=""code"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""table-layout: fixed;"" data-muid=""919cd44c-f4e7-42a2-bf0a-7201e07eee77"">
    <tbody>
      <tr>
        <td height=""100%"" valign=""top"" role=""module-content""><div style=""font-size: 48px; font-family: inherit; display: flex; flex-direction: column; align-items: center""><span style=""font-size: 1em; z-index: 99""><strong>Email Confirmation</strong></span><div style=""position: absolute; width: 80%; height: 0.13em; background-color: #ff6800; bottom: 0.41em; z-index: 0""></div></div></td>
      </tr>
    </tbody>
  </table><table class=""module"" role=""module"" data-type=""spacer"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""table-layout: fixed;"" data-muid=""1905cf46-0542-43b7-9fe3-e6766c8a52a9"">
    <tbody>
      <tr>
        <td style=""padding:0px 0px 25px 0px;"" role=""module-content"" bgcolor="""">
        </td>
      </tr>
    </tbody>
  </table><table class=""module"" role=""module"" data-type=""text"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""table-layout: fixed;"" data-muid=""fceb2ff5-372b-4491-ac6b-c20ef845b783"" data-mc-module-version=""2019-10-22"">
    <tbody>
      <tr>
        <td style=""padding:18px 0px 18px 0px; line-height:22px; text-align:inherit;"" height=""100%"" valign=""top"" bgcolor="""" role=""module-content""><div><div style=""font-family: inherit; text-align: inherit""><span style=""font-size: 24px; font-family: arial, helvetica, sans-serif"">Hello, {0}!</span></div>
<div style=""font-family: inherit; text-align: inherit""><br></div>
<div style=""font-family: inherit; text-align: inherit""><span style=""font-size: 24px; font-family: arial, helvetica, sans-serif"">To comfirm your registration on our site you must click button below!</span></div><div></div></div></td>
      </tr>
    </tbody>
  </table><table class=""module"" role=""module"" data-type=""spacer"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""table-layout: fixed;"" data-muid=""1a94fdf6-7517-41c6-bd46-c3a713131251"">
    <tbody>
      <tr>
        <td style=""padding:0px 0px 35px 0px;"" role=""module-content"" bgcolor="""">
        </td>
      </tr>
    </tbody>
  </table><table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""module"" data-role=""module-button"" data-type=""button"" role=""module"" style=""table-layout:fixed;"" width=""100%"" data-muid=""f3888944-5a6a-4e34-831d-7c8ab3764f5b"">
      <tbody>
        <tr>
          <td align=""center"" bgcolor="""" class=""outer-td"" style=""padding:0px 0px 0px 0px;"">
            <table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""wrapper-mobile"" style=""text-align:center;"">
              <tbody>
                <tr>
                <td align=""center"" bgcolor=""#ff6800"" class=""inner-td"" style=""border-radius:6px; font-size:16px; text-align:center; background-color:inherit;"">
                  <a href=""{1}"" style=""background-color:#ff6800; border:0px solid #ff6800; border-color:#ff6800; border-radius:0px; border-width:0px; color:#ffffff; display:inline-block; font-weight:normal; letter-spacing:0px; line-height:normal; padding:12px 18px 12px 18px; text-align:center; text-decoration:none; border-style:solid; font-family:arial,helvetica,sans-serif; font-size:20px;"" target=""_blank"">Confirm Email</a>
                </td>
                </tr>
              </tbody>
            </table>
          </td>
        </tr>
      </tbody>
    </table><table class=""module"" role=""module"" data-type=""spacer"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""table-layout: fixed;"" data-muid=""676c57e4-92d4-410d-a633-0d3e0f0ff289"">
    <tbody>
      <tr>
        <td style=""padding:0px 0px 30px 0px;"" role=""module-content"" bgcolor="""">
        </td>
      </tr>
    </tbody>
  </table><table class=""module"" role=""module"" data-type=""divider"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""table-layout: fixed;"" data-muid=""d70b672d-37fb-4dc5-bd2a-ea688f489e13"">
    <tbody>
      <tr>
        <td style=""padding:0px 0px 0px 0px;"" role=""module-content"" height=""100%"" valign=""top"" bgcolor="""">
          <table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" width=""100%"" height=""10px"" style=""line-height:10px; font-size:10px;"">
            <tbody>
              <tr>
                <td style=""padding:0px 0px 10px 0px;"" bgcolor=""#000000""></td>
              </tr>
            </tbody>
          </table>
        </td>
      </tr>
    </tbody>
  </table><div data-role=""module-unsubscribe"" class=""module"" role=""module"" data-type=""unsubscribe"" style=""color:#444444; font-size:12px; line-height:20px; padding:16px 16px 16px 16px; text-align:Center;"" data-muid=""4e838cf3-9892-4a6d-94d6-170e474d21e5""><div class=""Unsubscribe--addressLine""><p class=""Unsubscribe--senderName"" style=""font-size:12px; line-height:20px;"">Sender_Name</p><p style=""font-size:12px; line-height:20px;""><span class=""Unsubscribe--senderAddress"">Sender_Address</span>, <span class=""Unsubscribe--senderCity"">Sender_City</span>, <span class=""Unsubscribe--senderState"">Sender_State</span> <span class=""Unsubscribe--senderZip"">Sender_Zip</span></p></div><p style=""font-size:12px; line-height:20px;""><a class=""Unsubscribe--unsubscribeLink"" href=""unsubscribe"" target=""_blank"" style="""">Unsubscribe</a> - <a href=""unsubscribe_preferences"" target=""_blank"" class=""Unsubscribe--unsubscribePreferences"" style="""">Unsubscribe Preferences</a></p></div></td>
                                      </tr>
                                    </table>
                                    <!--[if mso]>
                                  </td>
                                </tr>
                              </table>
                            </center>
                            <![endif]-->
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </div>
      </center>";

        public RegisterModel(
            UserManager<Consumer> userManager,
            SignInManager<Consumer> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new Consumer { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Email Confirmation",string.Format(_msgHtml, user.UserName, callbackUrl));

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
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
