﻿namespace MVC.Controllers;
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }
    
    [Authorize]
    public IActionResult Logout()
    {
        try
        {
            var idTokenHint = User.FindFirst("id_token")?.Value;

            return SignOut(new AuthenticationProperties
            {
                RedirectUri = $"{SD.AuthAPIBase}/connect/endsession?id_token_hint={idTokenHint}&post_logout_redirect_uri={Url.Action("Index", "Home", null, Request.Scheme)}"
            }, "Cookies", "oidc");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during logout process");
            throw;
        }
    }

    public IActionResult Login(string returnUrl = "/")
    {
        try
        {
            return Challenge(new AuthenticationProperties { RedirectUri = returnUrl });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during login process");
            throw;
        }
    }
}
