using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Order:BaseEntity
    {
        public string ShippingAddress { get; set; } //Kullanıcının siparişi talep ettigi (siparişin gönderilecegi) adres
        public string Email { get; set; } //Üye olmayan bir kullanıcının email'i özel olarak burada tutulur(null gecildiyse anlayın ki kullanıcı üye olarak alısveriş yapmıstır)
        public string NameDescription { get; set; }//Üye olmayan bir kullanıcının isim acıklaması  burada tutulur(null gecildiyse anlayın ki kullanıcı üye olarak alısveriş yapmıstır)
        public int? AppUserID { get; set; } //null gecilebiliyorsa anlayın ki kullanıcı üye degildir 
        //TOdo: buradaki AppUser iş akısını ödev olarak ver
        public decimal PriceOfOrder { get; set; } //Siparişin toplam fiyatı(Sepetin onaylanan fiyatı)

        //Relational Properties
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual AppUser AppUser { get; set; }


    }
}
