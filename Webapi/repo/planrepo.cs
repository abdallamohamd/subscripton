using Microsoft.EntityFrameworkCore;
using Webapi.models;

namespace Webapi.repo
{
    public class planrepo : Iplanrepo
    {
        private readonly appcontext appcontext;

        public planrepo(appcontext appcontext)
        {
            this.appcontext = appcontext;
        }

        public void add(plan plan)
        {
            appcontext.plans.Add(plan);
        }

        public List<plan> all()
        {
            return appcontext.plans.Include(x=>x.subscriptions).ToList();
        }

        public plan Get(int id)
        {
           return appcontext.plans.FirstOrDefault(x=>x.id== id);  
        }

        public void remove(int id)
        {
            plan plan =Get(id);
            appcontext.plans.Remove(plan);
        }

        public void save()
        {
            appcontext.SaveChanges();
        }
    }
}
