using GymLedger.Helpers.CookieAuth;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

public class CustomAuthorizeAttribute : AuthorizeAttribute
{
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        var authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName]; // Use your custom cookie name
        if (authCookie == null)
        {
            return false; // No cookie means unauthorized
        }

        try
        {
            // Use AuthCookieHelper to validate the cookie
            var authData = AuthCookieHelper.DecryptAuthCookie(authCookie.Value);
            if (authData == null)
            {
                return false; // Invalid or tampered cookie
            }

            var username = authData.Username;
            var password = authData.Password;

            // Validate user from the database
            return AuthCookieHelper.ValidateUser(username, password);
        }
        catch
        {
            return false; // Decryption or validation failed
        }
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        // Redirect to the login page for unauthorized access
        filterContext.Result = new RedirectResult("~/Account/Login");
    }
}
