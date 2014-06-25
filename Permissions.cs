using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;

namespace Associativy.Frontends
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ViewGraphs = new Permission { Category = "Associativy", Description = "View Associativy graphs on the frontend.", Name = "ViewGraphs" };

        public virtual Feature Feature { get; set; }


        public IEnumerable<Permission> GetPermissions()
        {
            return new[]
            {
                ViewGraphs
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Anonymous",
                    Permissions = new[] { ViewGraphs }
                }
            };
        }
    }
}