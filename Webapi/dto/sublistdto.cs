using System.ComponentModel.DataAnnotations.Schema;

namespace Webapi.dto
{
    public class sublistdto
    {
        public DateTime sb_date { get; set; }
        public DateTime re_date { get; set; }

        public string plan { get; set; }

        public string user { get; set; }
    }
}
