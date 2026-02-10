<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 8" />
  <img src="https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C# 12" />
  <img src="https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" alt="SQL Server" />
  <img src="https://img.shields.io/badge/EF%20Core-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="EF Core 8" />
  <img src="https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white" alt="Docker" />
  <img src="https://img.shields.io/badge/Serilog-Logging-3E3E3E?style=for-the-badge" alt="Serilog" />
</p>

# ??? Restaurant.Management

**Onion Architecture** ile katmanlý olarak tasarlanmýþ, **.NET 8** üzerinde çalýþan kapsamlý bir **Restoran Yönetim Sistemi**. Proje; stok takip algoritmasý, reçete yönetimi, sipariþ/fatura akýþlarý, rol bazlý yönetim panelleri ve RESTful API katmaný içerir.

> ?? **Geliþtirme Süresi:** ~15 gün  
> ?? **BilgeAdam Akademi** kurs bitirme projesi

---

## ?? Ekran Görüntüleri

---

## ?? Tech Stack

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
| **Container** | Docker (Dockerfile per project) |
| **Mail** | SMTP (`IMailSender` abstraction) |

---

## ?? Mimari — Onion Architecture

```
???????????????????????????????????????????????????????????????
?                      PRESENTATION                           ?
?   Project.UI (MVC + Areas)  ?  Project.API (Web API)        ?
???????????????????????????????????????????????????????????????
?                      INFRASTRUCTURE                         ?
?  Persistance  ?  InnerInfra  ?  OuterInfra  ?  Validator    ?
???????????????????????????????????????????????????????????????
?                          CORE                               ?
?       Domain       ?      Application      ?    Contract    ?
???????????????????????????????????????????????????????????????
```

### Core Katmaný
| Proje | Sorumluluk |
|-------|------------|
| `Project.Domain` | Entity sýnýflarý (`AppUser`, `Product`, `Order`, `Recipe`, `StockTransAction` vb.), enum'lar (`DataStatus`, `OrderDetailStatus`, `TransActionType`) |
| `Project.Application` | DTO'lar, AutoMapper profilleri, servis arayüzleri (`IManager<T,TDto>`), `Result` / `PagedResult` pattern, mail sözleþmesi |
| `Project.Contract` | Repository arayüzleri (`IRepository<T>`, `IUnitOfWork`, domain-specific repo interface'leri) |

### Infrastructure Katmaný
| Proje | Sorumluluk |
|-------|------------|
| `Project.Persistance` | EF Core `MyContext`, repository implementasyonlarý, entity konfigürasyonlarý, seed data, migration'lar |
| `Project.InnerInfrastructure` | Manager / Service implementasyonlarý (iþ kurallarý), `BaseManager<TEntity,TDto>` generic yapýsý |
| `Project.OuterInfrastructure` | Dýþ servis entegrasyonlarý (SMTP mail sender) |
| `Project.Validator` | FluentValidation validator'larý (16 entity için ayrý validator) |

### Presentation Katmaný
| Proje | Sorumluluk |
|-------|------------|
| `Project.UI` | Web arayüzü — Areas ile modüler paneller (HR, Bar, Kitchen, Administrative) |
| `Project.API` | RESTful API — Products, Orders, Tables, Suppliers, Categories, Recipes, Menus, Units, StockTransActions |

### Test Katmaný
| Proje | Sorumluluk |
|-------|------------|
| `Project.UnitTests` | xUnit + Moq ile birim testleri |
| `Project.IntegrationTests` | EF Core InMemory ile entegrasyon testleri |

---

## ??? Design Pattern'lar

### Unit of Work
Birden fazla repository iþlemini tek bir transaction olarak ele alýr. `_unitOfWork.CommitAsync()` ile atomik veri tutarlýlýðý saðlanýr.

> **Örnek:** Sipariþ oluþturulurken eþ zamanlý stok düþümü — biri baþarýsýz olursa tamamý geri alýnýr (rollback).

### Repository Pattern
Generic `BaseRepository<T>` üzerinden CRUD, Where ve Paged sorgularý standartlaþtýrýlýr. Domain-specific repository'ler (`RecipeRepository`, `OrderRepository` vb.) ek ihtiyaçlar için override edilir.

### Result Pattern
Tüm iþ katmaný operasyonlarý `Result` / `Result<T>` döner. `OperationStatus` enum'u ile (`Success`, `NotFound`, `ValidationError`, `AlreadyExists`, `Failed`) hata yönetimi tutarlý hale getirilir.

### Generic Manager Pattern
`BaseManager<TEntity,TDto>` sýnýfý; CRUD, soft/hard delete, pagination, validation, mapping ve structured logging iþlemlerini tek merkezden yönetir. Alt manager'lar domain-specific iþ kurallarýný override eder.

---

## ?? Core Feature: Stok Takip Algoritmasý

Projenin kalbi olan stok takip sistemi, **"her deðiþiklik bir muhasebe hareketidir"** prensibini uygular.

### Akýþ

```
OrderDetail oluþturuldu  ???  StockTransAction kaydý (Initial)
OrderDetail güncellendi  ???  StockTransAction kaydý (Update — fark kadar)
OrderDetail silindi      ???  StockTransAction kaydý (Deletion — iptal/return)
```

### Reçete Desteði
Satýþ sonrasý stok düþümünde ürünün reçetesi kontrol edilir:
- **Reçetesi var ?** bileþen ürünlerinin stoklarýndan düþülür
- **Reçetesi yok ?** doðrudan ürün stoðundan düþülür

Her durumda `StockTransAction` kaydý ile hareket log'lanýr ? tam izlenebilirlik (auditability).

**Ýlgili sýnýflar:**

| Bileþen | Dosya |
|---------|-------|
| Entity | `Domain/Entities/StockTransAction.cs` |
| DTO | `Application/DTOs/StockTransActionDTO.cs` |
| Manager | `InnerInfrastructure/ManagerConcretes/StockTransActionManager.cs` |
| Stok düþümü | `ProductManager.ReduceStockAfterSaleAsync(...)` |

---

## ????? Roller ve Paneller

Uygulama `Areas/Manager` üzerinden rol bazlý paneller sunar:

| Panel | Controller | Yetkiler |
|-------|-----------|----------|
| **Ýnsan Kaynaklarý** | `HrController` | Personel listesi, profil tamamlama, rol yönetimi, raporlar |
| **Bar Þefi** | `BarController` | Aktif sipariþler, reçete yönetimi, menü ürünleri, raporlar |
| **Mutfak Þefi** | `KitchenController` | Aktif sipariþler, reçete yönetimi, menü ürünleri, raporlar |
| **Ýdari Panel** | `AdministrativeController` | Ürün / kategori / birim / tedarikçi / masa / menü yönetimi, alým-satýþ faturalarý, personel |

---

## ?? Cross-Cutting Concerns

### ??? Global Exception Handler
Her iki presentation katmanýnda ayrý middleware:
- **UI ?** AJAX istekleri JSON response, normal istekler `/Home/Error` redirect
- **API ?** Standart JSON error response (`traceId` dahil)

### ?? Structured Logging — Serilog
- Console + dosya sink'i (`Logs/ui-log-{date}.txt`, `Logs/api-log-{date}.txt`)
- Rolling daily, 30 gün retention
- Microsoft / EF Core log seviyesi `Warning`'a çekilmiþ
- `UseSerilogRequestLogging()` ile HTTP request loglama
- Manager katmanýnda structured logging (`{EntityType}`, `{Id}` parametreleri)

### ?? Pagination
- **Repository:** `GetPagedAsync(page, pageSize, filter)` — EF Core `Skip` / `Take`
- **Manager:** `PagedResult<TDto>` — sayfa validasyonu, soft-delete filtresi
- **API:** `/api/{resource}/paged?page=1&pageSize=10` endpoint'leri

### ? In-Memory Caching
- `IMemoryCache` ile API katmanýnda response caching
- Sýk sorgulanan endpoint'lerde aktif (ör. Products paged)

### ?? Rate Limiting — API
- Fixed window limiter: **100 istek / dakika**
- `429 Too Many Requests` response
- Queue desteði (10 bekleme kapasitesi)

---

## ?? Kurulum ve Çalýþtýrma

### Ön Gereksinimler

| Araç | Versiyon |
|------|----------|
| .NET SDK | 8.0+ |
| SQL Server | LocalDB veya tam sürüm |
| IDE | Visual Studio 2022+ (önerilen) |

### 1) Repository'yi Klonlayýn

```bash
git clone https://github.com/Kutlayyctrk/Restaurant.Management.git
cd Restaurant.Management
```

### 2) Connection String

`Presentation/Project.UI/appsettings.json` ve `Presentation/Project.API/appsettings.json` içindeki `ConnectionStrings:OnionDb` deðerini güncelleyin:

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
# Startup Project: Presentation/Project.UI
# Default Project: Infrastructure/Project.Persistance

Add-Migration InitialCreate -Project Infrastructure\Project.Persistance -StartupProject Presentation\Project.UI
Update-Database -Project Infrastructure\Project.Persistance -StartupProject Presentation\Project.UI
```

**veya .NET CLI:**
```bash
dotnet ef database update --project Infrastructure/Project.Persistance --startup-project Presentation/Project.UI
```

### 4) Çalýþtýrma

```bash
# UI
dotnet run --project Presentation/Project.UI

# API (ayrý terminal)
dotnet run --project Presentation/Project.API
```

### 5) Docker

```bash
# UI
docker build -f Presentation/Project.UI/Dockerfile -t restaurant-ui .
docker run -p 8080:8080 restaurant-ui

# API
docker build -f Presentation/Project.API/Dockerfile -t restaurant-api .
docker run -p 8081:8080 restaurant-api
```

---

## ?? Mail Servisi

E-posta aktivasyon akýþý için SMTP yapýlandýrmasý:

| Bileþen | Dosya |
|---------|-------|
| SMTP ayarlarý | `appsettings.json` ? `Smtp` bölümü |
| DI kaydý | `OuterInfrastructure/DependencyResolvers/MailServiceInjection.cs` |
| Implementasyon | `OuterInfrastructure/Tools/MailSender.cs` |
| Sözleþme | `Application/MailService/IMailSender.cs` |

> ?? **Güvenlik:** Production'da SMTP credential'larý `User Secrets` veya environment variable ile yönetilmelidir. Mevcut yapý demo amaçlýdýr.

---

## ?? Teknik Borçlar

Proje öðrenme odaklý geliþtirildiði için tespit edilen teknik borçlar:

### ?? Yüksek Öncelik

| # | Borç | Açýklama | Etki |
|---|------|----------|------|
| 1 | **Domain katmanýnda Identity baðýmlýlýðý** | `Project.Domain` içinde `Microsoft.AspNetCore.Identity.EntityFrameworkCore` paketi var. Onion Architecture'da Domain katmaný framework-agnostic olmalýdýr. | Mimari ihlal |
| 2 | **SMTP credential'larý source code'da** | `appsettings.json` içinde açýk SMTP þifresi. | Güvenlik riski |
| 3 | **API'de Authorization eksikliði** | API controller'larýnda `[Authorize]` attribute yok. Tüm endpoint'ler anonim eriþime açýk. | Güvenlik riski |

### ?? Orta Öncelik

| # | Borç | Açýklama | Etki |
|---|------|----------|------|
| 4 | **Düþük test coverage** | Sadece `OrderManagerTests` ve `OrderRepositoryIntegrationTests` mevcut. 16+ manager/repo için test eksik. | Kalite güvencesi |
| 5 | **Caching tutarsýzlýðý** | `ProductsController`'da cache var, diðer controller'larda yok. Cache invalidation stratejisi tanýmsýz. | Performans tutarsýzlýðý |
| 6 | **CategoriesController boþ** | API'de dosya var ama içeriði boþ. | Eksik endpoint |
| 7 | **AutoMapper Core katmanýnda** | Mapping konfigürasyonu Application'da. Bazý mimarilerde sýnýr katmanlarýnda olmasý tercih edilir. | Tartýþmalý mimari karar |

### ?? Düþük Öncelik

| # | Borç | Açýklama | Etki |
|---|------|----------|------|
| 8 | **docker-compose.yml eksik** | Dockerfile'lar var ama orchestration yok. | DevOps |
| 9 | **API versioning yok** | `/api/v1/...` yapýsý tanýmsýz. | Ýleriye uyumluluk |
| 10 | **Health check endpoint eksik** | Container senaryolarý için `/health` yok. | Monitoring |
| 11 | **Response compression yok** | Compression middleware aktif deðil. | Bant geniþliði |
| 12 | **Pagination UI'da kullanýlmýyor** | Backend'de pagination altyapýsý hazýr ama UI controller'larý tüm veriyi çekiyor. | Performans |

---

## ??? Solution Yapýsý

```
Restaurant.Management/
?
??? Core/
?   ??? Project.Domain/                 # Entity'ler, enum'lar, abstract sýnýflar
?   ??? Project.Application/            # DTO, mapping, manager interface, Result pattern
?   ??? Project.Contract/               # Repository interface'leri, IUnitOfWork
?
??? Infrastructure/
?   ??? Project.Persistance/            # DbContext, repository impl, migration, seed, config
?   ??? Project.InnerInfrastructure/    # Manager implementasyonlarý (iþ kurallarý)
?   ??? Project.OuterInfrastructure/    # Mail servisi (SMTP)
?   ??? Project.Validator/             # FluentValidation validator'larý (16 adet)
?
??? Presentation/
?   ??? Project.UI/                    # MVC + Areas (HR, Bar, Kitchen, Administrative)
?   ?   ??? Areas/Manager/             # Rol bazlý controller & view'lar
?   ?   ??? Controllers/              # LoginAndRegister, Sales
?   ?   ??? Middleware/               # GlobalExceptionHandlerMiddleware
?   ?   ??? wwwroot/                  # Static assets (FlatAdmin)
?   ?
?   ??? Project.API/                   # RESTful Web API (9 controller)
?       ??? Controllers/              # Products, Orders, Tables, Suppliers vb.
?       ??? Middleware/               # GlobalExceptionHandlerMiddleware
?       ??? Models/                   # ApiResponse<T>
?
??? Tests/
?   ??? Project.UnitTests/            # xUnit + Moq
?   ??? Project.IntegrationTests/     # EF Core InMemory
?
??? README.md
```

---

## ?? AI–Human Collaboration

Frontend tasarýmýnda ve dinamik AJAX kullaným senaryolarýnda geliþtirme sürecinde **AI tabanlý araçlardan** destek alýnmýþtýr. Amaç; üretkenliði artýrmak ve farklý çözüm alternatiflerini hýzlý deðerlendirmektir.

---

## ?? Kazanýmlar

- **Onion Architecture** teorisi gerçek bir proje üzerinde pratiðe döküldü
- **Unit of Work** + **Repository Pattern** ile veri bütünlüðü deneyimlendi
- **Global Exception Handling** ve **Serilog** ile production-ready hata yönetimi kuruldu
- **Pagination** ve **Caching** altyapýsý ile performans optimizasyonu uygulandý
- **Rate Limiting** ile API güvenliði saðlandý
- **FluentValidation**, **AutoMapper**, **Identity** kütüphanelerinin sürdürülebilir koda katkýlarý deneyimlendi
- **Docker** ile containerization altyapýsý hazýrlandý
- **xUnit** + **Moq** ile test yazma pratiði kazanýldý

---

## ?? Lisans / Üçüncü Taraf Varlýklar

UI tarafýnda kullanýlan vendor asset'ler (`wwwroot/FlatAdmin/...`) üçüncü taraf içerikler barýndýrabilir. Ýlgili lisanslar kendi dizinlerinde mevcuttur.

---

<p align="center">
  <sub>Built with ?? using .NET 8 & Onion Architecture</sub>
</p>
