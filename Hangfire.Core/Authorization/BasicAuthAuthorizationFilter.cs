using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;

namespace Hangfire.Authorization
{
    /// <summary>
    /// Represents Hangfire authorization filter for basic authentication.
    /// </summary>
    /// <remarks>If you are using this together with Asp.net core security, configure Hangfire BEFORE Asp.net core security configuration.</remarks>
    public class BasicAuthAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly BasicAuthAuthorizationFilterOptions _options;

        public BasicAuthAuthorizationFilter()
            : this(new BasicAuthAuthorizationFilterOptions())
        {
        }

        public BasicAuthAuthorizationFilter(BasicAuthAuthorizationFilterOptions options)
        {
            _options = options;
        }


        public bool Authorize([NotNull] DashboardContext context)
        {
            var aspNetCoreContext = context as AspNetCoreDashboardContext;

            if (aspNetCoreContext == null)
            {
                throw new NotSupportedException("This class only support ASP.NET Core.");
            }

            if ((_options.SslRedirect == true) && (aspNetCoreContext.HttpContext.Request.Scheme != "https"))
            {
                aspNetCoreContext.HttpContext.Response.OnStarting(async (state) =>
                {
                    string redirectUri = new UriBuilder("https", aspNetCoreContext.HttpContext.Request.Host.Host, 443, aspNetCoreContext.HttpContext.Request.Path.Value).ToString();
                    aspNetCoreContext.HttpContext.Response.StatusCode = 301;
                    aspNetCoreContext.HttpContext.Response.Redirect(redirectUri);
                }, null);
                return false;
            }

            if ((_options.RequireSsl == true) && (aspNetCoreContext.HttpContext.Request.IsHttps == false))
            {
                context.Response.WriteAsync("Secure connection is required to access Hangfire Dashboard.");
                return false;
            }

            string header = aspNetCoreContext.HttpContext.Request.Headers["Authorization"];

            if (String.IsNullOrWhiteSpace(header) == false)
            {
                AuthenticationHeaderValue authValues = AuthenticationHeaderValue.Parse(header);

                if ("Basic".Equals(authValues.Scheme, StringComparison.InvariantCultureIgnoreCase))
                {
                    string parameter = Encoding.UTF8.GetString(Convert.FromBase64String(authValues.Parameter));
                    var parts = parameter.Split(':');

                    if (parts.Length > 1)
                    {
                        string login = parts[0];
                        string password = parts[1];

                        if ((String.IsNullOrWhiteSpace(login) == false) && (String.IsNullOrWhiteSpace(password) == false))
                        {
                            return _options
                                .Users
                                .Any(user => user.Validate(login, password, _options.LoginCaseSensitive))
                                   || Challenge(aspNetCoreContext);
                        }
                    }
                }
            }

            return Challenge(aspNetCoreContext);
        }

        private bool Challenge(AspNetCoreDashboardContext context)
        {
            context.HttpContext.Response.OnStarting(async (state) =>
            {
                context.HttpContext.Response.StatusCode = 401;
                context.HttpContext.Response.Headers.Append("WWW-Authenticate", "Basic realm=\"Hangfire Dashboard\"");
                var buffer = Encoding.UTF8.GetBytes("Authentication is required.");
                await context.Response.Body.WriteAsync(buffer, 0, (int)buffer.Length);
            }, false);
            return false;
        }
    }
}