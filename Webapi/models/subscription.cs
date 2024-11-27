using System.ComponentModel.DataAnnotations.Schema;

namespace Webapi.models
{
    public class subscription
    {
        public int Id { get; set; }
        public DateTime sb_date { get; set; }
        public DateTime re_date { get; set; }

        [ForeignKey("plan")]
        public int  plan_id {  get; set; }

        [ForeignKey("users")]
        public string user_id {  get; set; }
        public appuser users { get; set; }
        public plan? plan { get; set; }
    }
}
