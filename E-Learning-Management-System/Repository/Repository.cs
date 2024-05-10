using E_Learning_Management_System.Models;

namespace E_Learning_Management_System.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IDeletable
    {
        public readonly Context context;
        public Repository(Context context)
        {
            this.context = context;
        }
        public void insert(T obj)
        {
            context.Set<T>().Add(obj);
        }
        public void delete(T obj)
        {
            obj.IsDeleted = true;
            update(obj);
        }
        public void update(T obj)
        {
            context.Set<T>().Update(obj);
        }
        public List<T> GetAll()
        {

            //return context.Set<T>().ToList();
            return context.Set<T>().Where(e => !((IDeletable)e).IsDeleted).ToList();
        }
        public T Get(Func<T, bool> predicate) //find object by name ,id anything
        {
            //return context.Set<T>().FirstOrDefault(predicate);
            var entities = context.Set<T>().Where(e => !((IDeletable)e).IsDeleted).ToList();
            return entities.FirstOrDefault(predicate);
        }
        public int save()
        {
            return context.SaveChanges();
        }
    }
}
