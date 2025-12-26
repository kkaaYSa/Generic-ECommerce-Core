using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace KahveMVC
{
    public class Turkce
    {
        public static string DosyaAdiDuzenle(string veri)
        {
            string str = veri.Replace("ı", "i").Replace("ğ", "g").Replace("ü", "u").Replace("ş", "s").Replace("ö", "o").Replace("ç", "c").Replace("İ", "I").Replace("Ğ", "G").Replace("Ü", "U").Replace("Ş", "S").Replace("Ö", "O").Replace("Ç", "C").Replace(" ", "_");
            return str;
        }

        public static string AdresDuzenle(string veri)
        {
            if (string.IsNullOrEmpty(veri)) return "";
            string str = veri.ToLower();
            str = str.Replace("ı", "i").Replace("ğ", "g").Replace("ü", "u").Replace("ş", "s").Replace("ö", "o").Replace("ç", "c").Replace(" ", "-").Replace(".", "").Replace("/", "").Replace("?", "").Replace("'", "").Replace("\"", "");
            return str;
        }
    }
}