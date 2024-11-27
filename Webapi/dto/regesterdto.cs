using System.ComponentModel.DataAnnotations;

namespace Webapi.dto
{
    public class regesterdto
    {
        public string name {  get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }
        public string phone {  get; set; }
        
        [DataType(DataType.EmailAddress )]
        public string email { get; set; }

    }
}
