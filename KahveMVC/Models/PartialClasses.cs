using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

// BURASI ÇOK ÖNEMLİ: Senin Controller'ındaki using ile aynı olmalı
namespace KahveMVC.Models.EntityFramework
{
    public partial class anasayfa
    {
        [NotMapped]
        public HttpPostedFileBase fotoFile { get; set; }
    }

    public partial class urunler
    {
        [NotMapped]
        public HttpPostedFileBase fotoFile { get; set; }
    }

    public partial class hakkimizda
    {
        [NotMapped]
        public HttpPostedFileBase fotoFile { get; set; }
    }

    public partial class kullanici
    {
        [NotMapped]
        public bool BeniHatirla { get; set; }
    }
}