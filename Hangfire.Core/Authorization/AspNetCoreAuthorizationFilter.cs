using System;
using System.Linq;
using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace Hangfire.Authorization
{
    public class AspNetCoreAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private static readonly string[] EmptyArray = new string[0];

        private string _users;
        private string[] _usersSplit = EmptyArray;
        private string _roles;
        private string[] _rolesSplit = EmptyArray;

        public string Roles
        {
            get { return _roles ?? String.Empty; }
            set
            {
                _roles = value;
                _rolesSplit = SplitString(value);
            }
        }

        public string Users
        {
            get { return _users ?? String.Empty; }
            set
            {
                _users = value;
                _usersSplit = SplitString(value);
            }
        }

        public bool Authorize([NotNull] DashboardContext context)
        {
            var aspNetCoreContext = context as AspNetCoreDashboardContext;

            if (aspNetCoreContext == null)
            {
                throw new NotSupportedException("This class only support ASP.NET Core.");
            }
            var user = aspNetCoreContext.HttpContext.User;

            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return false;
            }

            if (_usersSplit.Length > 0 && !_usersSplit.Contains(user.Identity.Name, StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }

            if (_rolesSplit.Length > 0 && !_rolesSplit.Any(user.IsInRole))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Splits the string on commas and removes any leading/trailing whitespace from each result item.
        /// </summary>
        /// <param name="original">The input string.</param>
        /// <returns>An array of strings parsed from the input <paramref name="original"/> string.</returns>
        private static string[] SplitString(string original)
        {
            if (String.IsNullOrEmpty(original))
            {
                return EmptyArray;
            }

            var split = from piece in original.Split(',')
                        let trimmed = piece.Trim()
                        where !String.IsNullOrEmpty(trimmed)
                        select trimmed;
            return split.ToArray();
        }
    }
}
