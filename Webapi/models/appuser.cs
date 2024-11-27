using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Webapi.models
{
    public class appuser : IdentityUser
    {
        public string? address { get; set; }
    }
}
