using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Webapi.dto;
using Webapi.models;
using Webapi.repo;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class planController : ControllerBase
    {
        private readonly Iplanrepo iplanrepo;

        public planController(Iplanrepo iplanrepo)
        {
            this.iplanrepo = iplanrepo;
        }

        [HttpGet("all")]         
        public IActionResult show_all()
        {
            List<plan> plans = iplanrepo.all();
            List<plandto> plandtolist=new List<plandto>();
            foreach (var item in plans)
            {
               plandto plandto = new plandto();

                plandto.name = item.name;
                plandto.description = item.description;
                plandto.price = item.price;
                plandto.count = item.subscriptions.Count(); 

                plandtolist.Add(plandto);
            }
            return Ok(plandtolist);
        }

        [HttpGet]
        public IActionResult show(int id)
        {
             plan plan = iplanrepo.Get(id);
            if(plan != null)
            {
               return Ok(plan);
            }
            return BadRequest("id not found");
        }

        [HttpPost]
        public IActionResult add(plandto plandto)
        {
            if(ModelState.IsValid)
            {
                plan plan = new plan();

                plan.name = plandto.name;
                plan.price = plandto.price;
                plan.description = plandto.description;

               iplanrepo.add(plan);
               iplanrepo.save();
                return Created();
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult edite(plandto plandto ,int id )
        {

            plan plan = iplanrepo.Get(id);
             if(plan != null)
            {
                plan.name = plandto.name;
                plan.price = plandto.price;
                plan.description = plandto.description;
                iplanrepo.save();
                return Ok(plan);
            } 
            return BadRequest(" id not found");
        }

        [HttpDelete]
        public IActionResult delete(int id)
        {
            plan plan = iplanrepo.Get(id);
            if(plan != null)
            {
                iplanrepo.remove(id);
                iplanrepo.save();
                return Ok();
            }
            return BadRequest("id not found");
        } 
            
          
    }
}
