# Restaurant.Management — Restaurant Management System (Onion Architecture)

> **BilgeAdam Akademi** kurs bitirme projesi kapsamında geliştirilen, rol bazlı panellere sahip bir **Restaurant Yönetim Sistemi**.

---

## ✨ Overview

Bu repository; **Onion Architecture** yaklaşımıyla katmanlara ayrılmış, **.NET 8** üzerinde çalışan bir Restaurant Management uygulamasıdır. Proje; ürün/kategori/birim yönetimi, menü & reçete yönetimi, sipariş/fatura akışları ve en önemlisi **stok takip algoritması** gibi işlevleri kapsar.

---

## 🧰 Tech Stack

| Category | Tech |
|---|---|
| Backend | `.NET 8`, `C#` |
| UI | ASP.NET Core (`MVC + Areas`, Razor Views), `HTML`, `CSS`, `JavaScript` |
| Database | `SQL Server (SSMS)`, `Entity Framework Core 8 (Code First)` |
| AuthN/AuthZ | `ASP.NET Core Identity` |
| Libraries | `FluentValidation`, `AutoMapper` |

---

## 🧅 Architecture (Onion)

Solution yapısı Onion Architecture’a uygun olarak katmanlara ayrılmıştır:

- `Core`
  - `Project.Domain`: Domain entity’leri ve enum’lar
  - `Project.Application`: DTO’lar, mapping (`AutoMapper`) ve servis sözleşmeleri
  - `Project.Contract`: Repository arayüzleri
- `Infrastructure`
  - `Project.Persistance`: EF Core `DbContext` (`MyContext`), repository implementasyonları, konfigürasyonlar & seed’ler
  - `Project.InnerInfrastructure`: Manager/Service implementasyonları (iş kuralları)
  - `Project.OuterInfrastructure`: Dış servisler (örn. mail)
  - `Project.Validator`: `FluentValidation` validator’ları
- `Presentation`
  - `Project.UI`: Web arayüzü (Areas ile modüler paneller)

---

## 🚀 Key Feature: Stock Tracking Algorithm (Project Core)

Projenin “kalbi” stok takip yaklaşımıdır.

### 1) `OrderDetail` hareketleri stokta “iz” bırakır

Stok hareketleri `StockTransAction` entity’si ile takip edilir. Amaç; stok değişimlerini “güncel stok” yanında **hareket log’u** olarak da kalıcı hale getirmek ve geriye dönük izlenebilirlik sağlamaktır.

- Entity: `StockTransAction`
- DTO: `StockTransActionDTO`
- Manager: `StockTransActionManager`

### 2) Otomatik stok hareketi üretimi (`OrderDetail` bazlı)

`StockTransActionManager` üzerinden:

- `CreateInitialOrderActionAsync(...)` → satır ilk oluştuğunda
- `CreateUpdateOrderActionAsync(...)` → satır güncellendiğinde (fark kadar)
- `CreateDeletionOrderActionAsync(...)` → satır silindiğinde (iptal/return mantığı)

Bu yaklaşım stok yönetiminde “**her değişiklik bir muhasebe hareketidir**” prensibini uygular: veri bütünlüğü, auditability ve geriye dönük analiz imkanı sağlar.

> Ek not: `ProductManager.ReduceStockAfterSaleAsync(...)` içinde satış sonrası stok düşümü; ürünün reçetesi varsa **recipe item’lar üzerinden bileşen stoklarından**, yoksa direkt üründen düşülür. Her durumda `StockTransAction` kaydı ile hareket log’lanır.

---

## 🧑‍💼 Roles & Panels (UI)

Uygulama `Areas/Manager` üzerinden rol bazlı paneller sunar. Örnek controller’lar:

- `HrController` (İnsan Kaynakları)
- `BarController` (Bar Şefi / bar akışları)
- `KitchenController` (Mutfak Şefi / mutfak akışları)
- `AdministrativeController` (İdari panel)

Navigation örneği: `Presentation/Project.UI/Areas/Manager/Views/Shared/_SideBar.cshtml`

---

## ⚙️ Installation & Run

### Prerequisites

- `.NET SDK 8`
- `SQL Server` (SSMS)
- Visual Studio 2022

### 1) Connection String

`Presentation/Project.UI/appsettings.json` içindeki `ConnectionStrings:OnionDb` değerini kendi SQL Server bilgilerinize göre güncelleyin.

- File: `Presentation/Project.UI/appsettings.json`
- Key: `ConnectionStrings:OnionDb`

### 2) EF Core Migrations (**IMPORTANT**)

Migration işlemlerini **Persistence katmanı** üzerinden çalıştırın:

- Startup Project: `Presentation/Project.UI`
- Default Project (Migrations): `Infrastructure/Project.Persistance`

Örnek (Package Manager Console):

- `Add-Migration InitialCreate -Project Infrastructure\Project.Persistance -StartupProject Presentation\Project.UI`
- `Update-Database -Project Infrastructure\Project.Persistance -StartupProject Presentation\Project.UI`

### 3) Run

- Startup project: `Presentation/Project.UI`
- Çalıştır: `https` / `http`

---

## ✉️ Mail Service Notes

Mail aktivasyon akışı:

- SMTP ayarları: `Presentation/Project.UI/appsettings.json` içindeki `Smtp` bölümü
- DI: `Infrastructure/Project.OuterInfrastructure/DependencyResolvers/MailServiceInjection.cs`
- Sender: `Infrastructure/Project.OuterInfrastructure/Tools/MailSender.cs`

> Güvenlik notu: Repository içinde gerçek SMTP şifresi bulundurmak önerilmez. Prod senaryoda `User Secrets` veya environment variable kullanılmalıdır.

---

## 🧾 Technical Debt (Learning Notes)

Bu proje öğrenme odaklı geliştirildiği için tespit edilmiş teknik borçlar bulunmaktadır:

1. **Domain katmanında Identity bağımlılığı**
   - `Project.Domain` içinde `Microsoft.AspNetCore.Identity.EntityFrameworkCore` paketi bulunuyor.
   - Onion prensipleri açısından ideal olan; Domain katmanının framework bağımlılıklarını minimumda tutmasıdır.

2. **Application katmanında AutoMapper kullanımı**
   - Mapping pratiklik sağlar; ancak bazı senaryolarda mapping’in Application yerine sınır katmanlarında ele alınması tercih edilebilir.
   - Proje kapsamında öğrenme ve hız amaçlı tercih edilmiştir.

---

## 🤝 AI–Human Collaboration

Frontend tasarımında ve dinamik `AJAX` kullanım senaryolarında geliştirme sürecinde **AI tabanlı araçlardan** destek alınmıştır. Amaç; üretkenliği artırmak ve farklı çözüm alternatiflerini hızlı değerlendirmektir.

---

## 📈 Personal Development & Vision

- Onion Architecture teorisi gerçek bir proje üzerinde pratiğe döküldü.
- Hata yönetimi ve debugging süreçlerinde (özellikle breakpoint odaklı) daha sistematik bir yaklaşım kazanıldı.
- `FluentValidation`, `AutoMapper`, `Identity` gibi kütüphanelerin sürdürülebilir koda katkıları deneyimlendi.
- Proje; yeni teknolojiler ve iyileştirmelerle (ör. bağımlılıkların daha doğru katmanlara taşınması, test altyapısı, güvenli secrets yönetimi) geliştirilmeye devam edecektir.

---

## 🗂️ Solution Structure (Quick Map)

---

## 📌 License / Third‑Party Assets

UI tarafında kullanılan bazı vendor asset’ler (`wwwroot/FlatAdmin/...`) üçüncü taraf içerikler barındırabilir.
