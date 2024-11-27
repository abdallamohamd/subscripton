using System.ComponentModel.DataAnnotations;

namespace Webapi.dto
{
    public class logindto
    {
        public string Name { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("password")]
        [DataType(DataType.Password)]
        public string confirmpassword { get; set; }
         
    }
}
