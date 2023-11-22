using Project.ENTITIES.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Abstracts
{
    //Bu interface'in icerisindeki metotlar Repository'deki metotlarınıza denk düsmek isterler...Ancak imzaları aynı olmak zorunda degildir...
    public interface IManager<T> where T:IEntity
    {
        //List Commands

        List<T> GetAll();
        List<T> GetActives();
        Task<List<T>> GetActivesAsync();
        List<T> GetPassives();
        List<T> GetModifieds();

        //Modify Commands

        string Add(T item);
        Task AddAsync(T item);
        string AddRange(List<T> list);
        Task<string> AddRangeAsync(List<T> list);
        void Delete(T item);
        void DeleteRange(List<T> list);
        Task UpdateAsync(T item);
        Task UpdateRangeAsync(List<T> list);
        string Destroy(T item);
        string DestroyRange(List<T> list);

        //Linq Commands
        List<T> Where(Expression<Func<T, bool>> exp);
        bool Any(Expression<Func<T, bool>> exp);
        Task<bool> AnyAsync(Expression<Func<T, bool>> exp);
        T FirstOrDefault(Expression<Func<T, bool>> exp);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> exp);
        object Select(Expression<Func<T, object>> exp);
        IQueryable<X> Select<X>(Expression<Func<T, X>> exp);

        //Find Command
        Task<T> FindAsync(int id);
        //Last Datas
        List<T> GetLastDatas(int count);
        //First Datas
        List<T> GetFirstDatas(int count);
    }
}
