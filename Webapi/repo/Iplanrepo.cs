using Webapi.models;

namespace Webapi.repo
{
    public interface Iplanrepo
    {
        public List<plan> all();
        public plan Get(int id);
        public void remove (int id);
        public void add(plan plan);
        public void save();
    }
}
