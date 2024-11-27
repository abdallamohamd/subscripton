using Microsoft.EntityFrameworkCore;
using Webapi.models;

namespace Webapi.repo
{
    public class subsrepo : Isubsrepo
    {
        private readonly appcontext appcontext;

        public subsrepo(appcontext appcontext)
        {
            this.appcontext = appcontext;
        }

        public void add(subscription subscription)
        {
            appcontext.subscriptions.Add(subscription);
        }

        public List<subscription> all()
        {
           return appcontext.subscriptions.Include(x=>x.users).Include(p=>p.plan).ToList();
        }

        public subscription get(int id)
        {
            return appcontext.subscriptions.FirstOrDefault(x => x.Id == id);
        }

        public void remove(int id)
        {
            subscription subscription = get(id);
            appcontext.subscriptions.Remove(subscription);
        }

        public void save()
        {
            appcontext.SaveChanges();
        }
    }
}
