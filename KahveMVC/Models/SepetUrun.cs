using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KahveMVC.Models
{
    public class SepetUrun
    {

        public int UrunId { get; set; }
        public string UrunAd { get; set; }

        public string Resim { get; set; }
        public double fiyat { get; set; }
        public int adet { get; set; }
        public double ToplamTutar 
        {
            get
            {
                return adet * fiyat;
            }
        }
    }
}