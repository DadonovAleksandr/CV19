using CV19.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV19.Services.Interfaces;

interface IRepository<T> where T : IEntity
{
    void Add(T item);
    bool Remove(T item);
    void Update(int id, T item);

    T Get(int id) => GetAll().FirstOrDefault(item => item.Id == id);
    IEnumerable<T> GetAll();   
}
