USE master;
GO

-- Eğer veritabanı zaten varsa silip yeniden oluşturmak yerine kontrol ediyoruz.
-- Mevcut verileri korumak istersen bu bloğu atlayabilirsin.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'kahve')
BEGIN
    CREATE DATABASE kahve;
END
GO

USE kahve;
GO

-- --------------------------------------------------------
-- Tablo: anasayfa
-- --------------------------------------------------------
IF OBJECT_ID('dbo.anasayfa', 'U') IS NOT NULL DROP TABLE dbo.anasayfa;

CREATE TABLE anasayfa (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    foto NVARCHAR(50) NOT NULL,
    ustBaslik NVARCHAR(250) NOT NULL,
    ustIcerik NVARCHAR(MAX) NOT NULL, -- MySQL'deki varchar(6000) için NVARCHAR(MAX) uygundur
    link NVARCHAR(50) NOT NULL,
    altBaslik NVARCHAR(250) NOT NULL,
    altIcerik NVARCHAR(MAX) NOT NULL
);

-- ID değerlerini korumak için IDENTITY_INSERT açıyoruz
SET IDENTITY_INSERT anasayfa ON;

INSERT INTO anasayfa (id, foto, ustBaslik, ustIcerik, link, altBaslik, altIcerik) VALUES
(1, 'intro.jpg', 'Değerli İçecekleri', '<p>Falan filan Every cup of our quality artisan coffee starts with locally sourced, hand picked ingredients. Once you try it, our coffee will be a blissful addition to your everyday morning routine - we guarantee it!</p>', 'Visit Us Today!', 'To You', '<p>When you walk into our shop to start your day, we are dedicated to providing you with friendly service, a welcoming atmosphere, and above all else, excellent products made with the highest quality ingredients. If you are not satisfied, please let us know and we will do whatever we can to make things right!</p>');

SET IDENTITY_INSERT anasayfa OFF;
GO

-- --------------------------------------------------------
-- Tablo: hakkimizda
-- --------------------------------------------------------
IF OBJECT_ID('dbo.hakkimizda', 'U') IS NOT NULL DROP TABLE dbo.hakkimizda;

CREATE TABLE hakkimizda (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    foto NVARCHAR(50) NOT NULL,
    ustBaslik NVARCHAR(250) NOT NULL,
    baslik NVARCHAR(250) NOT NULL,
    icerik NVARCHAR(MAX) NOT NULL -- Text tipi MSSQL'de deprecated olduğu için NVARCHAR(MAX)
);

SET IDENTITY_INSERT hakkimizda ON;

INSERT INTO hakkimizda (id, foto, ustBaslik, baslik, icerik) VALUES
(1, 'about.jpg', 'Strong Coffee, Strong Roots', 'Kafemiz Hakkında', '<p>Founded in 1987 by the Hernandez brothers, our establishment has been serving up rich coffee sourced from artisan farmers in various regions of South and Central America. We are dedicated to travelling the world, finding the best coffee, and bringing back to you here in our cafe.</p><p>We guarantee that you will fall in <i>lust</i> with our decadent blends the moment you walk inside until you finish your last sip. Join us for your daily routine, an outing with friends, or simply just to enjoy some alone time.</p>');

SET IDENTITY_INSERT hakkimizda OFF;
GO

-- --------------------------------------------------------
-- Tablo: iletisimformu
-- --------------------------------------------------------
IF OBJECT_ID('dbo.iletisimformu', 'U') IS NOT NULL DROP TABLE dbo.iletisimformu;

CREATE TABLE iletisimformu (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    adSoyad NVARCHAR(100) NOT NULL,
    eposta NVARCHAR(50) NOT NULL,
    mesaj NVARCHAR(MAX) NOT NULL,
    cinsiyet BIT NOT NULL, -- MySQL bit(1) -> MSSQL BIT
    izin BIT NOT NULL,
    tarih DATETIME DEFAULT GETDATE()
);

SET IDENTITY_INSERT iletisimformu ON;

-- Not: MySQL'deki b'0' ve b'1' değerleri MSSQL için 0 ve 1 olarak düzeltildi.
INSERT INTO iletisimformu (id, adSoyad, eposta, mesaj, cinsiyet, izin, tarih) VALUES
(1, 'Ali Veli', 'aaaa@aaa.com', 'mesajjjksjakl jalksd jklasjd klasjd', 0, 1, '2019-04-05 11:02:12'),
(3, 'Ali Veli', 'aaaa@aa.com', 'wwww', 0, 1, '2019-04-05 11:02:12'),
(4, 'Ali Veli', 'aaaa@aa.com', 'wwww', 0, 1, '2019-04-05 11:02:12'),
(9, 'Ali Veli', 'aaaa@aa.com', 'sss', 0, 1, '2019-04-05 11:02:12'),
(13, 'Mahmut Kaya', 'sdsdsds@aaaa.com', 'dsddsds', 0, 1, '2019-04-05 11:02:12'),
(14, 'Mahmut Kaya', 'mahmut@aaa.com', 'rrr', 0, 1, '2019-04-05 11:02:12'),
(15, 'Mahmut Kaya', 'mahmut@aaa.com', 'uuuuuuu', 0, 1, '2019-04-05 11:02:12'),
(16, 'Mahmut Kaya', 'sdsdsds@aaaa.com', 'ddd', 0, 1, '2019-04-05 11:02:12'),
(20, 'ali veli', 'aaa@aaa.com', 's kljdkjfdsklj klsj lkjsdkl jlkjskdlf jlksd sdklj flksdjf ', 0, 1, '2019-04-05 11:02:12'),
(25, 'aaa', 'aaa@aaa.com', 'ssss', 1, 1, '2019-04-05 11:04:57');

SET IDENTITY_INSERT iletisimformu OFF;
GO

-- --------------------------------------------------------
-- Tablo: kullanici
-- --------------------------------------------------------
IF OBJECT_ID('dbo.kullanici', 'U') IS NOT NULL DROP TABLE dbo.kullanici;

CREATE TABLE kullanici (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ad NVARCHAR(50) NOT NULL DEFAULT '0',
    sifre NVARCHAR(50) NOT NULL DEFAULT '0'
);

SET IDENTITY_INSERT kullanici ON;

INSERT INTO kullanici (id, ad, sifre) VALUES
(3, 'admin', '81dc9bdb52d04dc20036dbd8313ed055');

SET IDENTITY_INSERT kullanici OFF;
GO

-- --------------------------------------------------------
-- Tablo: magaza
-- --------------------------------------------------------
IF OBJECT_ID('dbo.magaza', 'U') IS NOT NULL DROP TABLE dbo.magaza;

CREATE TABLE magaza (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ustBaslik NVARCHAR(50) NOT NULL DEFAULT '0',
    anaBaslik NVARCHAR(500) NOT NULL DEFAULT '0',
    adres NVARCHAR(250) NOT NULL DEFAULT '0',
    telefon NVARCHAR(20) NOT NULL DEFAULT '0'
);

SET IDENTITY_INSERT magaza ON;

INSERT INTO magaza (id, ustBaslik, anaBaslik, adres, telefon) VALUES
(1, 'COME ON IN', 'Çalışma Saatleri', '<p><i><strong>1116 Orchard Street</strong></i><br><i>Golden Valley, Minnesota</i></p>', '(317) 585-8468');

SET IDENTITY_INSERT magaza OFF;
GO

-- --------------------------------------------------------
-- Tablo: magazasaat
-- --------------------------------------------------------
IF OBJECT_ID('dbo.magazasaat', 'U') IS NOT NULL DROP TABLE dbo.magazasaat;

CREATE TABLE magazasaat (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    gun NVARCHAR(50) NOT NULL,
    saat NVARCHAR(50) NOT NULL
);

SET IDENTITY_INSERT magazasaat ON;

INSERT INTO magazasaat (id, gun, saat) VALUES
(1, 'Pazartesi', '08:00 - 20:00'),
(2, 'Salı', '08:00 - 20:00'),
(3, 'Çarşamba', '08:00 - 20:00'),
(4, 'Perşembe', '08:00 - 20:00'),
(5, 'Cuma', '08:00 - 20:00'),
(6, 'Cumartesi', 'Kapali'),
(7, 'Pazar', 'Kapalı');

SET IDENTITY_INSERT magazasaat OFF;
GO

-- --------------------------------------------------------
-- Tablo: urunler
-- --------------------------------------------------------
IF OBJECT_ID('dbo.urunler', 'U') IS NOT NULL DROP TABLE dbo.urunler;

CREATE TABLE urunler (
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    foto NVARCHAR(50) NOT NULL,
    baslik NVARCHAR(250) NOT NULL,
    ustBaslik NVARCHAR(250) NOT NULL,
    icerik NVARCHAR(MAX) NOT NULL,
    aktif TINYINT NOT NULL,
    sira INT NOT NULL
);

SET IDENTITY_INSERT urunler ON;

-- Not: MySQL string escape (\') karakterleri MSSQL formatına ('') çevrilmiştir.
INSERT INTO urunler (id, foto, baslik, ustBaslik, icerik, aktif, sira) VALUES
(1, 'products-01.jpg', 'Coffees & Teas', 'Blended to Perfection', '<p class="mb-0">We take pride in our work, and it shows. Every time you order a beverage from us, we guarantee that it will be an experience worth having. Whether it''s our world famous Venezuelan Cappuccino, a refreshing iced herbal tea, or something as simple as a cup of speciality sourced black coffee, you will be coming back for more.</p>', 0, 100),
(2, 'products-02.jpg', 'Bakery & Kitchen', 'Delicious Treats, Good Eats', '<p class="mb-0">Our seasonal menu features delicious snacks, baked goods, and even full meals perfect for breakfast or lunchtime. We source our ingredients from local, oragnic farms whenever possible, alongside premium vendors for specialty goods.</p>', 1, 350),
(4, 'products-01.jpg', 'Coffees & Teas', 'Blended to Perfection', '<p class="mb-0">We take pride in our work, and it shows. Every time you order a beverage from us, we guarantee that it will be an experience worth having. Whether it''s our world famous Venezuelan Cappuccino, a refreshing iced herbal tea, or something as simple as a cup of speciality sourced black coffee, you will be coming back for more.</p>', 0, 300),
(3, 'products-03.jpg', 'Bulk Speciality Blends', 'From Around the World', '<p class="mb-0">Travelling the world for the very best quality coffee is something take pride in. When you visit us, you''ll always find new blends from around the world, mainly from regions in Central and South America. We sell our blends in smaller to large bulk quantities. Please visit us in person for more details.</p>', 1, 400),
(5, 'products-03.jpg', 'Bulk Speciality Blends', 'From Around the World', '<p class="mb-0">Travelling the world for the very best quality coffee is something take pride in. When you visit us, you''ll always find new blends from around the world, mainly from regions in Central and South America. We sell our blends in smaller to large bulk quantities. Please visit us in person for more details.</p>', 0, 500),
(6, 'products-02.jpg', 'Bakery & Kitchen', 'Delicious Treats, Good Eats', '<p class="mb-0">Our seasonal menu features delicious snacks, baked goods, and even full meals perfect for breakfast or lunchtime. We source our ingredients from local, oragnic farms whenever possible, alongside premium vendors for specialty goods.</p>', 1, 150);

SET IDENTITY_INSERT urunler OFF;
GO