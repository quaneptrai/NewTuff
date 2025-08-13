using Aris3._0.Infrastructure.Data.Context;
using Aris3._0Fe.Models;
using Aris3._0Fe.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MimeKit;
using System.Security.Claims;
public class UserController : Controller
{
    private readonly ArisDbContext dbContext;
    private readonly SendEmail sendEmail;

    public UserController(ArisDbContext dbContext,SendEmail _sendEmail)
    {
        this.dbContext = dbContext;
        sendEmail = _sendEmail;
    }
    public IActionResult UserProfile()
    {
        return View(dbContext.Accounts.ToList());
    }
    public IActionResult ForgotPassword()
    {
        return View();
    }
    public IActionResult VerifyMail()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> SendEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            ModelState.AddModelError("", "Email is required.");
            return View("ForgotPassword");
        }

        var result = await sendEmail.SendVerificationEmailAsync(email);
        TempData["EmailResult"] = result;
        return RedirectToAction("VerifyMail");
    }
    [HttpGet]
    public async Task<IActionResult> VerifyOtp(int code)
    {
        var person = await dbContext.Otps.FirstOrDefaultAsync(t=>t.Code == code&&t.ExpireAt > DateTime.Now);
        
        if(person != null) return RedirectToAction("Index", "Home");
        return RedirectToAction("ForgotPassword");
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(Account account)
    {
        var user = dbContext.Accounts
                    .FirstOrDefault(u => u.Email == account.Email && u.Password == account.Password);
        if (user == null)
        {
            ModelState.AddModelError("", "Invalid email or password");
            return View(account);
        }

        var claims = new List<Claim>
        {
        new Claim(ClaimTypes.Email, user.Email),
        new Claim("UserName", user.UserName)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("MyCookieAuth", principal);

        return RedirectToAction("Index", "Home");
    }

}


