<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 8" />
  <img src="https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C# 12" />
  <img src="https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" alt="SQL Server" />
  <img src="https://img.shields.io/badge/EF%20Core-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="EF Core 8" />
  <img src="https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white" alt="Docker" />
  <img src="https://img.shields.io/badge/Serilog-Logging-3E3E3E?style=for-the-badge" alt="Serilog" />
</p>

# Restaurant.Management

**Onion Architecture** ile katmanli olarak tasarlanmis, **.NET 8** uzerinde calisan kapsamli bir **Restoran Yonetim Sistemi**. Proje; stok takip algoritmasi, recete yonetimi, siparis/fatura akislari, rol bazli yonetim panelleri ve RESTful API katmani icerir.

> **Gelistirme Suresi:** ~15 gun
>
> **BilgeAdam Akademi** kurs bitirme projesi

---

## Ekran Goruntuleri



---

## Tech Stack

| Katman | Teknoloji |
|--------|-----------|
| **Backend** | `.NET 8`, `C# 12`, `ASP.NET Core` |
| **Web UI** | ASP.NET Core MVC + Areas, Razor Views, HTML / CSS / JavaScript |
| **REST API** | ASP.NET Core Web API, Swagger / OpenAPI |
| **Database** | SQL Server, Entity Framework Core 8 (Code First) |
| **Auth** | ASP.NET Core Identity (cookie-based) |
| **Validation** | FluentValidation 12 |
| **Mapping** | AutoMapper 13 |
| **Logging** | Serilog (Console + Rolling File) |
| **Caching** | In-Memory Cache (`IMemoryCache`) |
| **Testing** | xUnit, Moq, FluentAssertions, EF Core InMemory |
| **Container** | Docker, Docker Compose |
| **Mail** | SMTP (`IMailSender` abstraction) |

---

## Mimari -- Onion Architecture

```
+-------------------------------------------------------------+
|                      PRESENTATION                           |
|   Project.UI (MVC + Areas)  |  Project.API (Web API)        |
+-------------------------------------------------------------+
|                      INFRASTRUCTURE                         |
|  Persistance  |  InnerInfra  |  OuterInfra  |  Validator    |
+-------------------------------------------------------------+
|                          CORE                               |
|       Domain       |      Application      |    Contract    |
+-------------------------------------------------------------+
```

### Core Katmani

| Proje | Sorumluluk |
|-------|------------|
| `Project.Domain` | Entity siniflari (AppUser, Product, Order, Recipe, StockTransAction vb.), enumlar (DataStatus, OrderDetailStatus, TransActionType) |
| `Project.Application` | DTOlar, AutoMapper profilleri, servis arayuzleri (`IManager<T,TDto>`), Result / PagedResult pattern, mail sozlesmesi |
| `Project.Contract` | Repository arayuzleri (`IRepository<T>`, `IUnitOfWork`, domain-specific repo interfaceleri) |

### Infrastructure Katmani

| Proje | Sorumluluk |
|-------|------------|
| `Project.Persistance` | EF Core MyContext, repository implementasyonlari, entity konfigurasyonlari, seed data, migrationlar |
| `Project.InnerInfrastructure` | Manager / Service implementasyonlari (is kurallari), `BaseManager<TEntity,TDto>` generic yapisi |
| `Project.OuterInfrastructure` | Dis servis entegrasyonlari (SMTP mail sender) |
| `Project.Validator` | FluentValidation validatorlari (16 entity icin ayri validator) |

### Presentation Katmani

| Proje | Sorumluluk |
|-------|------------|
| `Project.UI` | Web arayuzu -- Areas ile moduler paneller (HR, Bar, Kitchen, Administrative) |
| `Project.API` | RESTful API -- Products, Orders, Tables, Suppliers, Categories, Recipes, Menus, Units, StockTransActions |

### Test Katmani

| Proje | Sorumluluk |
|-------|------------|
| `Project.UnitTests` | xUnit + Moq ile birim testleri |
| `Project.IntegrationTests` | EF Core InMemory ile entegrasyon testleri |

---

## Design Patternlar

### Unit of Work

Birden fazla repository islemini tek bir transaction olarak ele alir. `_unitOfWork.CommitAsync()` ile atomik veri tutarliligi saglanir.

> **Ornek:** Siparis olusturulurken es zamanli stok dusumu -- biri basarisiz olursa tamami geri alinir (rollback).

### Repository Pattern

Generic `BaseRepository<T>` uzerinden CRUD, Where ve Paged sorgulari standartlastirilir. Domain-specific repositoryler (RecipeRepository, OrderRepository vb.) ek ihtiyaclar icin override edilir.

### Result Pattern

Tum is katmani operasyonlari `Result` / `Result<T>` doner. `OperationStatus` enumu ile (Success, NotFound, ValidationError, AlreadyExists, Failed) hata yonetimi tutarli hale getirilir.

### Generic Manager Pattern

`BaseManager<TEntity,TDto>` sinifi; CRUD, soft/hard delete, pagination, validation, mapping ve structured logging islemlerini tek merkezden yonetir. Alt managerlar domain-specific is kurallarini override eder.

---

## Core Feature: Stok Takip Algoritmasi

Projenin kalbi olan stok takip sistemi, **"her degisiklik bir muhasebe hareketidir"** prensibini uygular.

### Akis

```
OrderDetail olusturuldu  -->  StockTransAction kaydi (Initial)
OrderDetail guncellendi  -->  StockTransAction kaydi (Update -- fark kadar)
OrderDetail silindi      -->  StockTransAction kaydi (Deletion -- iptal/return)
```

### Recete Destegi

Satis sonrasi stok dusumunde urunun recetesi kontrol edilir:

- **Recetesi var -->** bilesen urunlerinin stoklarindan dusulur
- **Recetesi yok -->** dogrudan urun stogundan dusulur

Her durumda `StockTransAction` kaydi ile hareket loglanir -- tam izlenebilirlik (auditability).

**Ilgili siniflar:**

| Bilesen | Dosya |
|---------|-------|
| Entity | `Domain/Entities/StockTransAction.cs` |
| DTO | `Application/DTOs/StockTransActionDTO.cs` |
| Manager | `InnerInfrastructure/ManagerConcretes/StockTransActionManager.cs` |
| Stok dusumu | `ProductManager.ReduceStockAfterSaleAsync(...)` |

---

## Roller ve Paneller

Uygulama `Areas/Manager` uzerinden rol bazli paneller sunar:

| Panel | Controller | Yetkiler |
|-------|-----------|----------|
| **Insan Kaynaklari** | `HrController` | Personel listesi, profil tamamlama, rol yonetimi, raporlar |
| **Bar Sefi** | `BarController` | Aktif siparisler, recete yonetimi, menu urunleri, raporlar |
| **Mutfak Sefi** | `KitchenController` | Aktif siparisler, recete yonetimi, menu urunleri, raporlar |
| **Idari Panel** | `AdministrativeController` | Urun / kategori / birim / tedarikci / masa / menu yonetimi, alim-satis faturalari, personel |

---

## Cross-Cutting Concerns

### Global Exception Handler

Her iki presentation katmaninda ayri middleware:

- **UI -->** AJAX istekleri JSON response, normal istekler `/Home/Error` redirect
- **API -->** Standart JSON error response (traceId dahil)

### Structured Logging -- Serilog

- Console + dosya sinki (`Logs/ui-log-{date}.txt`, `Logs/api-log-{date}.txt`)
- Rolling daily, 30 gun retention
- Microsoft / EF Core log seviyesi Warning olarak ayarli
- `UseSerilogRequestLogging()` ile HTTP request loglama
- Manager katmaninda structured logging ({EntityType}, {Id} parametreleri)

### Pagination

- **Repository:** `GetPagedAsync(page, pageSize, filter)` -- EF Core Skip / Take
- **Manager:** `PagedResult<TDto>` -- sayfa validasyonu, soft-delete filtresi
- **API:** `/api/{resource}/paged?page=1&pageSize=10` endpointleri

### In-Memory Caching

- `IMemoryCache` ile API katmaninda response caching
- Sik sorgulanan endpointlerde aktif (orn. Products paged)

### Rate Limiting -- API

- Fixed window limiter: **100 istek / dakika**
- `429 Too Many Requests` response
- Queue destegi (10 bekleme kapasitesi)

---

## Kurulum ve Calistirma

### On Gereksinimler

| Arac | Versiyon |
|------|----------|
| .NET SDK | 8.0+ |
| SQL Server | LocalDB veya tam surum |
| IDE | Visual Studio 2022+ (onerilen) |

### 1) Repoyu Klonlayin

```bash
git clone https://github.com/Kutlayyctrk/Restaurant.Management.git
cd Restaurant.Management
```

### 2) Connection String

`Presentation/Project.UI/appsettings.json` ve `Presentation/Project.API/appsettings.json` icindeki `ConnectionStrings:OnionDb` degerini guncelleyin:

```json
{
  "ConnectionStrings": {
    "OnionDb": "Server=YOUR_SERVER;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 3) Database Migration

**Package Manager Console:**

```powershell
Add-Migration InitialCreate -Project Infrastructure\Project.Persistance -StartupProject Presentation\Project.UI
Update-Database -Project Infrastructure\Project.Persistance -StartupProject Presentation\Project.UI
```

**veya .NET CLI:**

```bash
dotnet ef database update --project Infrastructure/Project.Persistance --startup-project Presentation/Project.UI
```

### 4) Calistirma

```bash
# UI
dotnet run --project Presentation/Project.UI

# API (ayri terminal)
dotnet run --project Presentation/Project.API
```

### 5) Docker Compose

Tum servisleri (SQL Server + API + UI) tek komutla ayaga kaldirabilirsiniz:

```bash
docker compose up --build -d
```

| Servis | URL |
|--------|-----|
| **UI** | http://localhost:5001 |
| **API (Swagger)** | http://localhost:5000/swagger |
| **SQL Server** | localhost:1433 (sa / DockerSql.2024!) |

Durdurmak icin:

```bash
docker compose down
```

> **Not:** API container baslangicta veritabanini otomatik olusturur/gunceller (EF Core Migrate). Ilk calistirmada SQL Server'in hazir olmasini bekler (healthcheck).

#### Tek Tek Calistirma (Opsiyonel)

```bash
# UI
docker build -f Presentation/Project.UI/Dockerfile -t restaurant-ui .
docker run -p 8080:8080 restaurant-ui

# API
docker build -f Presentation/Project.API/Dockerfile -t restaurant-api .
docker run -p 8081:8080 restaurant-api
```

---

## Mail Servisi

E-posta aktivasyon akisi icin SMTP yapilandirmasi:

| Bilesen | Dosya |
|---------|-------|
| SMTP ayarlari | `appsettings.json` --> Smtp bolumu |
| DI kaydi | `OuterInfrastructure/DependencyResolvers/MailServiceInjection.cs` |
| Implementasyon | `OuterInfrastructure/Tools/MailSender.cs` |
| Sozlesme | `Application/MailService/IMailSender.cs` |

> **Guvenlik:** Productionda SMTP credentiallari User Secrets veya environment variable ile yonetilmelidir. Mevcut yapi demo amaclidir.

---

## Teknik Borclar

Proje ogrenme odakli gelistirildigi icin tespit edilen teknik borclar:

### Yuksek Oncelik

| # | Borc | Aciklama | Etki |
|---|------|----------|------|
| 1 | **Domain katmaninda Identity bagimliligi** | Project.Domain icinde Microsoft.AspNetCore.Identity.EntityFrameworkCore paketi var. Onion Architectureda Domain katmani framework-agnostic olmalidir. | Mimari ihlal |
| 2 | **SMTP credentiallari source codeda** | appsettings.json icinde acik SMTP sifresi. | Guvenlik riski |
| 3 | **APIde Authorization eksikligi** | API controllerlarinda Authorize attribute yok. Tum endpointler anonim erisime acik. | Guvenlik riski |

### Orta Oncelik

| # | Borc | Aciklama | Etki |
|---|------|----------|------|
| 4 | **Dusuk test coverage** | Sadece OrderManagerTests ve OrderRepositoryIntegrationTests mevcut. 16+ manager/repo icin test eksik. | Kalite guvencesi |
| 5 | **Caching tutarsizligi** | ProductsControllerda cache var, diger controllerlarda yok. Cache invalidation stratejisi tanimsiz. | Performans tutarsizligi |
| 6 | **AutoMapper Core katmaninda** | Mapping konfigurasyonu Applicationda. Bazi mimarilerde sinir katmanlarinda olmasi tercih edilir. | Tartismali mimari karar |


### Dusuk Oncelik

| # | Borc | Aciklama | Etki |
|---|------|----------|------|
| 7 | **API versioning yok** | /api/v1/... yapisi tanimsiz. | Ileriye uyumluluk |
| 8 | **Health check endpoint eksik** | Container senaryolari icin /health yok. | Monitoring |
| 9 | **Response compression yok** | Compression middleware aktif degil. | Bant genisligi |
| 10 | **Pagination UIda kullanilmiyor** | Backendde pagination altyapisi hazir ama UI controllerlari tum veriyi cekiyor. | Performans |

---

## Solution Yapisi

```
Restaurant.Management/
|
+-- Core/
|   +-- Project.Domain/
|   +-- Project.Application/
|   +-- Project.Contract/
|
+-- Infrastructure/
|   +-- Project.Persistance/
|   +-- Project.InnerInfrastructure/
|   +-- Project.OuterInfrastructure/
|   +-- Project.Validator/
|
+-- Presentation/
|   +-- Project.UI/
|   |   +-- Areas/Manager/
|   |   +-- Controllers/
|   |   +-- Middleware/
|   |   +-- wwwroot/
|   |
|   +-- Project.API/
|       +-- Controllers/
|       +-- Middleware/
|       +-- Models/
|
+-- Tests/
|   +-- Project.UnitTests/
|   +-- Project.IntegrationTests/
|
+-- docker-compose.yml
+-- README.md
```

---

## AI-Human Collaboration

Frontend tasariminda ve dinamik AJAX kullanim senaryolarinda gelistirme surecinde **AI tabanli araclardan** destek alinmistir. Amac; uretkenligi artirmak ve farkli cozum alternatiflerini hizli degerlendirmektir.

---

## Kazanimlar

- **Onion Architecture** teorisi gercek bir proje uzerinde pratige dokuldu
- **Unit of Work** + **Repository Pattern** ile veri butunlugu deneyimlendi
- **Global Exception Handling** ve **Serilog** ile production-ready hata yonetimi kuruldu
- **Pagination** ve **Caching** altyapisi ile performans optimizasyonu uygulandi
- **Rate Limiting** ile API guvenligi saglandi
- **FluentValidation**, **AutoMapper**, **Identity** kutuphanelerinin surdurulebilir kod yapisina katkilari deneyimlendi
- **Docker** ile containerization altyapisi hazirlandi
- **xUnit** + **Moq** ile test yazma pratigi kazanildi

---

## Lisans / Ucuncu Taraf Varliklar

UI tarafinda kullanilan vendor assetler (`wwwroot/FlatAdmin/...`) ucuncu taraf icerikler barindirir. Ilgili lisanslar kendi dizinlerinde mevcuttur.
