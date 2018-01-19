using System;
using System.Collections.Generic;
using System.Text;
using MSharp;

class RoleManagement
{
    internal static ProjectRole[] GetAuthorizedRoles() => new[] { AppRole.Director };
}
