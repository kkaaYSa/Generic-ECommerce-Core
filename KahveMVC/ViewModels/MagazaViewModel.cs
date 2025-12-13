using KahveMVC.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KahveMVC.ViewModels
{
    public class MagazaViewModel
    {
        //liste olanlar IEnumerable<magazasaat> formatta yazılır
        public IEnumerable<magazasaat> MagazaSaatler { get; set; }

        public magaza Magaza { get; set; }
        public hakkimizda Hakkimizda { get; set; }

    }
}