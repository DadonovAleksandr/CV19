using CV19.Models.Interfaces;
using CV19.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV19.Services.Base
{
    abstract class RepositoryInMemory<T> : IRepository<T> where T : IEntity
    {
        private List<T> _items = new List<T>();
        private int _lastId;

        protected RepositoryInMemory() { }
        protected RepositoryInMemory(IEnumerable<T> items)
        {
            foreach (var item in items) 
                Add(item);
        }
 
        public void Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (_items.Contains(item)) return;
            
            item.Id = ++ _lastId;
            _items.Add(item);


        }

        public IEnumerable<T> GetAll() => _items;
        
        public bool Remove(T item) => _items.Remove(item);
        
        public void Update(int id, T item)
        {
            if(item is null) throw new ArgumentNullException(nameof(item));
            if(id <= 0) throw new ArgumentOutOfRangeException(nameof(id), id, "Индекс не может быть меньше 1");
            if(_items.Contains(item)) return;
            var db_item = ((IRepository<T>)this).Get(id);
            if (db_item is null) throw new InvalidOperationException("Редактируемый элемент не найден в репозитории");
            Update(item, db_item);
        }

        protected abstract void Update(T source, T destination);
    }
}
