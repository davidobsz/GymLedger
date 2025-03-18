using GymLedger.Data;
using GymLedger.Models.Models;
using GymLedger.Views.Account.Login;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace GymLedger.Helpers.CookieAuth
{
    public static class AuthCookieHelper
    {
        public static void CreateAuthCookie(string username, string password, bool isPersistent)
        {
            // You can combine username and a hashed password as a ticket for extra security
            var authTicket = new FormsAuthenticationTicket(
                1, // Version
                username, // Username
                DateTime.Now, // Issue date
                DateTime.Now.AddHours(2), // Expiration date
                isPersistent,
                password // User-specific data
            );

            // Encrypt the ticket
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            // Create a cookie
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true, // Prevent access via JavaScript
                Secure = FormsAuthentication.RequireSSL, // Use HTTPS
                Expires = isPersistent ? authTicket.Expiration : DateTime.MinValue
            };

            // Add the cookie to the response
            HttpContext.Current.Response.Cookies.Add(authCookie);


        }

        public static LoginAccountView DecryptAuthCookie(string encryptedCookie)
        {
            try
            {
                var authTicket = FormsAuthentication.Decrypt(encryptedCookie);
                if (authTicket == null)
                {
                    return null;
                }

                return new LoginAccountView
                {
                    Username = authTicket.Name,
                    Password = authTicket.UserData
                };
            }
            catch
            {
                return null; // Decryption failed
            }
        }
        private static readonly object _lock = new object();
        public static bool ValidateUser(string username, string password)
        {
            // if needed to prevent concurrent users
            /*lock (_lock)
            {
                using (var db = new DataContext())
                {
                    return db.Users.Any(u => u.Username == username && u.Password == password);
                }
            }*/
            using (var db = new DataContext())
            {
                return db.Users.Any(u => u.Username == username && u.Password == password);
            }
        }

        public static User getUserIdentity()
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return null;  // No cookie = no logged-in user
            }

            var userInfo = DecryptAuthCookie(authCookie.Value);
            if (userInfo == null)
            {
                return null;  // Decryption failed
            }

            return new User { Username = userInfo.Username };
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}
