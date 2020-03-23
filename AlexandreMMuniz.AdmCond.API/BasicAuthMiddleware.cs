﻿using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AlexandreMMuniz.AdmCond.API
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _realm;

        public BasicAuthMiddleware(RequestDelegate next, string realm)
        {
            _next = next;
            _realm = realm;
        }

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                // Get the encoded username and password
                var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                // Decode from Base64 to string
                var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
                // Split username and password
                var username = decodedUsernamePassword.Split(':', 2)[0];
                var password = decodedUsernamePassword.Split(':', 2)[1];
                // Check if login is correct
                if (IsAuthorized(username, password))
                {
                    await _next.Invoke(context).ConfigureAwait(false);
                    return;
                }
            }
            // Return authentication type (causes browser to show login dialog)
            context.Response.Headers["WWW-Authenticate"] = "Basic";
            // Add realm if it is not null
            if (!string.IsNullOrWhiteSpace(_realm))
            {
                context.Response.Headers["WWW-Authenticate"] += $" realm=\"{_realm}\"";
            }
            // Return unauthorized
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }

        // Make your own implementation of this
        private static bool IsAuthorized(string username, string password)
        {
            if(username.ToLower() == "amunizamigos" && password.ToLower() == "amuniz2019")
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
