# Bulut Tabanlı Ağaçlandırma Sahası Takip ve Görselleştirme Sistemi

Bu proje, ağaçlandırma sahalarının dijital ortamda izlenmesi, yönetilmesi ve harita üzerinde görselleştirilmesi amacıyla geliştirilmiş bulut tabanlı bir web uygulamasıdır. Sistem; saha bilgileri, konum verileri ve gözlem kayıtlarının merkezi bir yapı üzerinden yönetilmesini sağlar.

## Proje Amacı

Ağaçlandırma sahalarında yalnızca dikim işlemi değil, dikim sonrası bakım ve gözlem süreçleri de büyük önem taşır. Bu proje ile saha verilerinin düzenli şekilde tutulması, gözlem kayıtlarının takip edilmesi ve sahaların güncel durumlarının harita tabanlı olarak değerlendirilmesi amaçlanmıştır.

## Özellikler

* Ağaçlandırma sahalarını listeleme
* Yeni saha kaydı ekleme
* Saha bilgilerini güncelleme
* Saha detaylarını görüntüleme
* Gözlem kayıtları ekleme ve takip etme
* Verimlilik skoruna göre saha durumu değerlendirme
* Harita üzerinde saha konumlarını görüntüleme
* Riskli, orta ve verimli sahaları renk bazlı gösterme
* REST API üzerinden veri iletişimi
* Swagger ile API endpointlerini test etme
* Azure üzerinde canlı çalıştırılabilir yapı

## Kullanılan Teknolojiler

* ASP.NET Core MVC
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server / Azure SQL Database
* PostgreSQL
* Leaflet
* OpenStreetMap
* Swagger
* Azure App Service
* DBeaver

## Sistem Mimarisi

Proje katmanlı mimari yapısına uygun olarak geliştirilmiştir.

```text
Afforestation.Core
Afforestation.API
Afforestation.WebUI
```

### Core Katmanı

Core katmanında sistemin temel model yapıları, entity sınıfları, DTO yapıları, DbContext ve Entity Framework Core veri erişim altyapısı yer almaktadır.

### API Katmanı

API katmanı, istemci uygulamalar ile veri katmanı arasındaki veri iletişimini sağlar. REST tabanlı endpointler aracılığıyla saha ve gözlem verileri yönetilir.

### WebUI Katmanı

WebUI katmanı, kullanıcıların sistemle etkileşim kurduğu arayüz kısmıdır. Kullanıcılar bu katman üzerinden saha kayıtlarını görüntüleyebilir, düzenleyebilir, gözlem ekleyebilir ve harita ekranı üzerinden sahaları takip edebilir.

## Harita Modülü

Harita modülünde OpenStreetMap harita altlığı olarak kullanılmıştır. Leaflet kütüphanesi ile sahalar enlem ve boylam bilgilerine göre harita üzerinde marker olarak gösterilmektedir.

Sahalar, en güncel gözlem verisindeki verimlilik skoruna göre sınıflandırılır:

```text
0 - 40    : Riskli
41 - 70   : Orta
71 - 100  : Verimli
```

Marker renkleri bu sınıflandırmaya göre belirlenir. Marker üzerine tıklandığında saha adı, gözlem notu, verimlilik skoru ve gözlem tarihi görüntülenebilir.

## Veritabanı Yapısı

Sistemde temel olarak iki ana tablo bulunmaktadır:

### Sites

Ağaçlandırma sahalarına ait temel bilgileri tutar.

* Id
* Name
* Latitude
* Longitude
* City
* District
* PlantingData
* Status

### Observations

Sahalara ait gözlem kayıtlarını tutar.

* Id
* SiteId
* Date
* ProductivityScore
* Note

Bir saha için birden fazla gözlem kaydı tutulabilir. Bu nedenle `Sites` ve `Observations` tabloları arasında bire-çok ilişki bulunmaktadır.

## Kurulum

Projeyi çalıştırmadan önce veritabanı bağlantı ayarlarının yapılması gerekir.

### 1. Projeyi klonla

```bash
git clone https://github.com/kullanici-adi/proje-adi.git
cd proje-adi
```

### 2. Veritabanı bağlantısını ayarla

API projesindeki `appsettings.json` dosyasında connection string bilgisini kendi veritabanı ortamına göre düzenle.

SQL Server örneği:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=AfforestationDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

PostgreSQL örneği:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=AfforestationDb;Username=postgres;Password=your_password"
}
```

### 3. Migration işlemlerini çalıştır

```bash
dotnet ef database update
```

### 4. API projesini çalıştır

```bash
dotnet run --project Afforestation.API
```

### 5. WebUI projesini çalıştır

```bash
dotnet run --project Afforestation.WebUI
```

## API Testi

API servisleri Swagger arayüzü üzerinden test edilebilir.

```text
https://localhost:xxxx/swagger
```

Swagger üzerinden saha listeleme, saha ekleme, gözlem ekleme ve harita verisi getirme gibi işlemler test edilebilir.

## Bulut Yayını

Proje Azure üzerinde canlı çalışacak şekilde yapılandırılmıştır.

* WebUI: Azure App Service
* API: Azure App Service
* Veritabanı: Azure SQL Database

Bu yapı sayesinde sistem yerel ortam dışında web üzerinden erişilebilir hale getirilmiştir.

## Gelecek Geliştirmeler

* Kullanıcı doğrulama ve yetkilendirme sistemi
* Rol bazlı kullanıcı yönetimi
* Mobil uygulama desteği
* Sensör verileri entegrasyonu
* Drone görüntüleri ile saha analizi
* Yapay zekâ tabanlı verimlilik tahmini
* Gelişmiş raporlama modülü

## Proje Durumu

Proje, lisans bitirme projesi kapsamında geliştirilmiştir. Temel saha yönetimi, gözlem takibi, REST API, harita görselleştirme ve bulut ortamında çalıştırma özellikleri tamamlanmıştır.

## Geliştirici

**Anılcan Bostancı**
Karabük Üniversitesi
Bilgisayar Mühendisliği
