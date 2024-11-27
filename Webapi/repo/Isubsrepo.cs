using Webapi.models;

namespace Webapi.repo
{
    public interface Isubsrepo
    {
        public List<subscription> all();
        public subscription get(int id);
        public void add(subscription subscription);
        public void remove (int  id);
        public void save();
    }
}
