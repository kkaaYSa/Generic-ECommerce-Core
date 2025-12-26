# 🛋️ Mobilya Dünyası - Modern E-Ticaret Otomasyonu

**Mobilya Dünyası**, ASP.NET MVC mimarisi kullanılarak geliştirilmiş, stok takibi, dinamik sepet yönetimi ve gelişmiş admin paneli özelliklerine sahip, modern bir B2C e-ticaret web projesidir.

Proje, klasik bir ürün kataloğu olmanın ötesinde; sipariş anında stok düşme, rol bazlı yetkilendirme ve kullanıcı dostu arayüzü ile tam kapsamlı bir alışveriş deneyimi sunar.

## 🚀 Projenin Amacı
Kullanıcıların mobilya ürünlerini detaylı inceleyebileceği, stok durumuna göre sepete ekleyebileceği ve güvenle sipariş verebileceği bir platform oluşturmaktır. Yönetici paneli sayesinde ürün, stok, fiyat ve sipariş süreçleri tek bir merkezden yönetilir.

## 🛠 Kullanılan Teknolojiler
* **Backend:** C# / ASP.NET MVC 5
* **Veritabanı:** MS SQL Server / Entity Framework (DB First)
* **Frontend:** HTML5, CSS3, Bootstrap 4, JavaScript (jQuery)
* **UI Kütüphaneleri:** SwiperJS (Mobil Uyumlu Slider), FontAwesome
* **Editör:** CKEditor (Admin panelinde zengin metin düzenleme)

## ✨ Öne Çıkan Özellikler

### 1. 🛒 Gelişmiş Sepet ve Sipariş Yönetimi
* **Akıllı Stok Kontrolü:** Kullanıcı sepete ürün eklerken veritabanındaki anlık stok kontrol edilir. Stoktan fazla ürün eklenmesi engellenir.
* **Otomatik Stok Düşme:** Sipariş tamamlandığında, satılan ürün adedi veritabanından otomatik olarak düşülür.
* **Adres Otomasyonu:** Üye girişi yapan kullanıcıların kayıtlı adres ve telefon bilgileri sipariş ekranına otomatik gelir.
* **Misafir/Üye Ayrımı:** İster üye olarak ister misafir olarak alışveriş yapma imkanı.

### 2. 🎨 Modern ve Responsive Tasarım
* **Özel Tema:** Mobilya sektörüne uygun "Koyu Lacivert & Ahşap Turuncusu" renk paleti ile profesyonel görünüm.
* **Mobil Uyumlu:** Telefon, tablet ve masaüstü cihazlarda kusursuz çalışan `Bootstrap` altyapısı.
* **Dinamik Slider:** Ana sayfada ve menüde `SwiperJS` ile çalışan dokunmatik uyumlu ürün vitrini.

### 3. 🔐 Güvenlik ve Yetkilendirme
* **Rol Bazlı Yönetim:**
    * **Admin (Personel):** Ürün ekleyebilir, siparişleri görebilir ancak silemez.
    * **Yönetici (Patron):** Tüm yetkilere sahiptir, ürün/personel silebilir.
* **Session Yönetimi:** Sepet verileri ve kullanıcı oturumları güvenli Session yapısıyla yönetilir.

### 4. ⚙️ Yönetim Paneli (Admin Dashboard)
* **Ürün Yönetimi:** Ürün ekleme, güncelleme, pasife alma, Fiyat ve Stok belirleme.
* **Sipariş Takibi:** Gelen siparişleri listeleme ve durum güncelleme (Bekliyor, Kargolandı vb.).
* **Görsel Editör:** Ürün açıklamaları için HTML destekli metin editörü.

## 📸 Ekran Görüntüleri

| Ana Sayfa & Slider | Ürün Detay & Stok Uyarısı |
| :---: | :---: |
| <img src="https://via.placeholder.com/400x200?text=Ana+Sayfa" width="400"> | <img src="https://via.placeholder.com/400x200?text=Urun+Detay" width="400"> |

| Sepet & Ödeme | Admin Paneli |
| :---: | :---: |
| <img src="https://via.placeholder.com/400x200?text=Sepet" width="400"> | <img src="https://via.placeholder.com/400x200?text=Admin" width="400"> |

*(Not: Ekran görüntüleri projenin son haline aittir.)*

## 💾 Kurulum

1. Projeyi bilgisayarınıza indirin veya klonlayın.
2. **SQL Server** üzerinde yeni bir veritabanı oluşturun.
3. Proje içindeki `.sql` dosyasını (varsa) çalıştırın veya `Web.config` dosyasındaki bağlantı dizesini (Connection String) kendi sunucunuza göre güncelleyin.
4. Visual Studio üzerinden `Update-Database` komutunu çalıştırın (Code First ise) veya scripti execute edin.
5. Projeyi `Ctrl + F5` ile çalıştırın.

---
**Geliştirici:** Volkan Ekici
