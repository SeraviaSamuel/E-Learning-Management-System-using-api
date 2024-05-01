using E_Learning_Management_System.Models;

namespace E_Learning_Management_System.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IDeletable
    {
        private readonly Context context;
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
        public ICollection<T> GetAll()
        {

            return context.Set<T>().ToList();
        }
        public T Get(Func<T, bool> predicate) //find object by name ,id anything
        {
            return context.Set<T>().FirstOrDefault(predicate);
        }
        public int save()
        {
            return context.SaveChanges();
        }
    }
}
