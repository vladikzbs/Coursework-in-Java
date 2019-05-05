using Coursework_in_Java.AppKernel.DatabaseConfigurations.Initializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coursework_in_Java.AppKernel.Roles
{
    public static class RoleInfo
    {
        public static string User => UserRoles.User.ToString();
        public static string Admin => UserRoles.Admin.ToString();
        public static string Inspector => UserRoles.Inspector.ToString();
        public static string Director => UserRoles.Director.ToString();
        public static string Guest => UserRoles.Guest.ToString();
    }
}