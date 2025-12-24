# Deployment Guide - ZOMAGE

## უფასო Hosting ოფციები

### 1. Railway (რეკომენდებული - ყველაზე მარტივი)

**ნაბიჯები:**

1. გადადით [railway.app](https://railway.app) და შექმენით ანგარიში (GitHub-ით)
2. დააჭირეთ "New Project"
3. აირჩიეთ "Deploy from GitHub repo"
4. აირჩიეთ თქვენი repository
5. Railway ავტომატურად გააკეთებს deployment-ს

**პორტი:**
Railway ავტომატურად აყენებს PORT environment variable-ს. თუ საჭიროა, შეგიძლიათ დაამატოთ `Program.cs`-ში:

```csharp
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://0.0.0.0:{port}");
```

---

### 2. Render

**ნაბიჯები:**

1. გადადით [render.com](https://render.com) და შექმენით ანგარიში
2. დააჭირეთ "New +" → "Web Service"
3. დააკავშირეთ თქვენი GitHub repository
4. აირჩიეთ:
   - **Name:** zomage
   - **Environment:** .NET Core
   - **Build Command:** `dotnet publish -c Release -o ./publish`
   - **Start Command:** `cd ./publish && dotnet zomage.dll`
5. დააჭირეთ "Create Web Service"

---

### 3. Fly.io

**ნაბიჯები:**

1. დააინსტალირეთ Fly CLI: `iwr https://fly.io/install.ps1 -useb | iex`
2. შექმენით ანგარიში: `fly auth signup`
3. Login: `fly auth login`
4. Launch: `fly launch`
5. Deploy: `fly deploy`

---

### 4. Azure App Service (Free Tier)

**ნაბიჯები:**

1. გადადით [portal.azure.com](https://portal.azure.com)
2. შექმენით "App Service"
3. აირჩიეთ "Free" tier
4. Deploy GitHub-იდან

---

## მნიშვნელოვანი შენიშვნები

1. **Database:** SQLite ფაილი ბაზაში იქმნება ავტომატურად `EnsureCreated()`-ით
2. **Static Files:** `wwwroot` folder ავტომატურად serve-დება
3. **Environment:** Production mode-ში ბაზა არ წაიშლება (მხოლოდ Development-ში)

## Port Configuration

თუ hosting provider არ აყენებს PORT variable-ს, შეგიძლიათ დაამატოთ `Program.cs`-ში:

```csharp
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://0.0.0.0:{port}");
```

---

## რეკომენდაცია

**Railway** არის ყველაზე მარტივი და სწრაფი ოფცია deployment-ისთვის. ის ავტომატურად ამოიცნობს .NET პროექტს და აკეთებს deployment-ს.

