using System;
using System.Linq;

namespace Common.Web.Api.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class CustomAuthorizeAttribute : Attribute
    {
        private string _roles;
        private string[] _rolesSplit = new string[0];

        public string Roles
        {
            get { return _roles ?? string.Empty; }
            set
            {
                _roles = value;
                _rolesSplit = SplitString(value);
            }
        }

        internal string[] RolesSplit
        {
            get { return _rolesSplit; }
        }

        private static string[] SplitString(string original)
        {
            if (string.IsNullOrEmpty(original))
            {
                return new string[0];
            }

            var split = from piece in original.Split(',')
                        let trimmed = piece.Trim()
                        where !string.IsNullOrEmpty(trimmed)
                        select trimmed;
            return split.ToArray();
        }
    }
}
