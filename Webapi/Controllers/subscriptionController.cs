using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Webapi.dto;
using Webapi.models;
using Webapi.repo;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class subscriptionController : ControllerBase
    {
        private readonly Isubsrepo isubsrepo;

        public subscriptionController(Isubsrepo isubsrepo)
        {
            this.isubsrepo = isubsrepo;
        }

        [HttpGet("all")]
        public IActionResult show_all()
        {
            List<subscription> subscriptions = isubsrepo.all();
            List<sublistdto> sublistdtos = new List<sublistdto>();
            foreach(var item in subscriptions)
            {
                sublistdto sub = new sublistdto();

                sub.sb_date=item.sb_date;
                sub.re_date = item.re_date;
                sub.user = item.users.UserName;
                sub.plan = item.plan.name;
                sublistdtos.Add(sub);
            }

            return Ok(sublistdtos);
        }


        [HttpGet]
        public IActionResult get(int id)
        {
            subscription subscription = isubsrepo.get(id); 
            if(subscription != null)
            {
                return Ok(subscription);
            }
            return BadRequest("id not found");
        }


        [HttpPost]
        public IActionResult add(subdto subdto)
        {
            if(ModelState.IsValid)
            {
                subscription subscription = new subscription();
                if (User.Identity.IsAuthenticated == true)
                {
                    Claim id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                    string u_id = id.Value;

                    subscription.sb_date = DateTime.Now;
                    subscription.re_date = DateTime.Now.AddMonths(1);
                    subscription.plan_id = subdto.plan_id;
                    subscription.user_id = u_id;

                    isubsrepo.add(subscription);
                    isubsrepo.save();
                    return Created();
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public IActionResult delete(int id)
        {
            subscription subscription = isubsrepo.get(id);
            if(subscription != null)
            {
                isubsrepo.remove(id);
                isubsrepo.save();
                return Ok();
            }
            return BadRequest("id not found ");
        }


    }
}
