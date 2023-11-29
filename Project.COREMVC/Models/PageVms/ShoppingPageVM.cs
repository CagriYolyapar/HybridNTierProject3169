using Project.ENTITIES.Models;
using X.PagedList;

namespace Project.COREMVC.Models.PageVms
{
    public class ShoppingPageVM
    {
        //Todo: Refactor yaparken Domain Entity'leri VM'lere cevireceksiniz 


        //PageVM
        //PageVM'ler iclerinde veri kapsülleyen ve sayfalara model olarak gönderilecek VM'lerdir...(Request ve Response icin aynı kullanılacaksa direkt PageVM denilebilir..
        //RequestPageVM: KUllanıcının gönderecegi verileri enkapsülleyen PageVM'dir
        //ResponsePageVM: Server'dan kullanıcıya gönderilecek verileri enkapsülleyen PageVM'dir

        //*************************************************************************

        //PureVM : (3'e ayrılır...Bir Shared normal (request ve response icin bir calısacak)...ResponseModel  , RequestModel

        //PureVM : Request ve Response icin bir calısacak  ve PageVM'lerin iclerinde enkapsüllenecek olan VM'lerdir...(Bunlar   DTO ile calısmaktan ziyade o yapıları temsil ederler...)
        //ResponseModel : Server'dan kullanıcıya cevap olarak gönderilecek VM'lerdir...Bunlar da PageVM icerisinde enkapsüllenir
        //RequestModel: Kullanıcıdan server'a gönderilecek VM'lerdir...
        public IPagedList<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}
