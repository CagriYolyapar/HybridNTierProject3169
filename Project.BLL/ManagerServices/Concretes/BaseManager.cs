using Project.BLL.ManagerServices.Abstracts;
using Project.DAL.Repositories.Abstracts;
using Project.ENTITIES.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Concretes
{
    //Abstract olmamasının ciddi bir nedeni var : Cünkü BaseManager'dan instance alınmasının mümkün olmasını istiyoruz...

    // Onemli : Normal şartlarda Bir Manager yapısı Domain Entity almaz...Buradaki metotlar DTO almalıdır (Data Transfer Object). Siz bir Domain Entity üzerinden iş akışı işlemleri yapmazsınız...


    //Todo : Buradaki Domain Entity'leri DTO'ya cevirerek Manager sınıflarının DTO ile calısmasını saglayın...


    public  class BaseManager<T> : IManager<T> where T : class, IEntity
    {
        //Bu class Repository ile birlikte calısmak istiyor...



        protected IRepository<T> _iRep;

        public BaseManager(IRepository<T> iRep) //BUrada dikkat ederseniz BaseManager constructor'i bir Parametre alıyor(IRepository<T> tipinde) IOC(Inversion of Controls) paternine göre burada belirtilen tip Middleware'de görülürse bize instance'i alınabilecek bir şey göndermek zorunda...Bizim istegimiz IRepository<T> generic tipi algılandıgı anda BaseRepository instance'inin gonderilmesi...Bu yüzdendir ki BaseRepository'i Abstract yapmadık...
        {
            _iRep = iRep;

        }


        public string Add(T item)
        {
            //SaatEkle(DTO)
            SaatEkle(item);

            //Mapping => DTO
            _iRep.Add(item);
            return "Ekleme basarılıdır";



        }

        private static void SaatEkle(T item)
        {
            item.CreatedDate = item.CreatedDate.AddHours(3);
        }

        public async Task AddAsync(T item)
        {
            SaatEkle(item);
            await _iRep.AddAsync(item);


        }

        bool ElemanKontrol(List<T> list)
        {
            if (list.Count > 5) return false;
            return true;

        }


        public string AddRange(List<T> list)
        {

            if (ElemanKontrol(list)) return "Maksimum 5 veri ekleyebileceginiz icin islem gercekleştirelemedi";
            _iRep.AddRange(list);
            return "Ekleme basarılı bir şekilde gercekleşti";
        }

        public async Task<string> AddRangeAsync(List<T> list)
        {
            if (ElemanKontrol(list)) return "Maksimum 5 veri ekleyebileceginiz icin islem gercekleştirelemedi";
            await _iRep.AddRangeAsync(list);
            return "Ekleme basarılıdır";
        }

        public bool Any(Expression<Func<T, bool>> exp)
        {

            //Exp kontrol islemleri
            return _iRep.Any(exp);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> exp)
        {
            return await _iRep.AnyAsync(exp);
        }

        public void Delete(T item)
        {
            if (item.CreatedDate == default)
            {
                return;
            }

            _iRep.Delete(item);
        }

        public void DeleteRange(List<T> list)
        {
            _iRep.DeleteRange(list);
        }

        public string Destroy(T item)
        {
            //İş akısı önemlidir
            if (item.Status == ENTITIES.Enums.DataStatus.Deleted)
            {
                _iRep.Destroy(item);
                return "Veri basarıyla yok edildi";
            }
           
            return $"Veriyi silemezsiniz cünkü {item.ID} {item.Status}   pasif degil";

        }

        public string DestroyRange(List<T> list)
        {

            //foreach (T item in list)
            //{
            //    if (item.Status != ENTITIES.Enums.DataStatus.Deleted)

            //        return "Listede sorun var hicbir işlem yapılamaz";
            //}




            foreach (T item in list) return Destroy(item);

            return "Silme işleminde sorunla karsılasıldı lütfen veri durumunun pasif oldugundan emin olunuz";

        }

        public async Task<T> FindAsync(int id)
        {
            return await _iRep.FindAsync(id);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> exp)
        {
           return _iRep.FirstOrDefault(exp);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> exp)
        {
            return await _iRep.FirstOrDefaultAsync(exp);
        }

        public List<T> GetActives()
        {
            return _iRep.GetActives();
        }

        public async Task<List<T>> GetActivesAsync()
        {
            return await _iRep.GetActivesAsync();
        }

        public List<T> GetAll()
        {
            return _iRep.GetAll();
        }

        public List<T> GetFirstDatas(int count)
        {
           return _iRep.GetFirstDatas(count);
        }

        public List<T> GetLastDatas(int count)
        {
           return _iRep.GetLastDatas(count);
        }

        public List<T> GetModifieds()
        {
            return _iRep.GetModifieds();
        }

        public List<T> GetPassives()
        {
            return _iRep.GetPassives();
        }

        public object Select(Expression<Func<T, object>> exp)
        {
            return _iRep.Select(exp);
        }

        public IQueryable<X> Select<X>(Expression<Func<T, X>> exp)
        {
            return _iRep.Select(exp);

        }

        public async Task UpdateAsync(T item)
        {
             await _iRep.UpdateAsync(item);
        }

        public async Task UpdateRangeAsync(List<T> list)
        {
            await _iRep.UpdateRangeAsync(list);
        }

        public List<T> Where(Expression<Func<T, bool>> exp)
        {
            return _iRep.Where(exp);
        }
    }
}
