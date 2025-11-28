# خدمات التخزين المحلي (LocalStorage Services) 💾

## نظرة عامة

هذا المجلد يحتوي على جميع خدمات التخزين المحلي للتطبيق، مبنية بطريقة منظمة وقابلة للتوسع.

## هيكل الملفات 📁

```
Services/LocalStorage/
├── IStorageService.cs              # الواجهة الأساسية
├── LocalStorageService.cs          # التنفيذ الأساسي
├── DataStorageService.cs           # تخزين بيانات الطفل
├── SettingsStorageService.cs       # تخزين الإعدادات
├── CacheStorageService.cs          # التخزين المؤقت
└── README.md                       # هذا الملف
```

---

## الخدمات المتاحة 🛠️

### 1. IStorageService (الواجهة الأساسية)

الواجهة العامة لجميع خدمات التخزين.

**الدوال المتاحة:**
```csharp
Task SetItemAsync<T>(string key, T value);
Task<T?> GetItemAsync<T>(string key);
Task RemoveItemAsync(string key);
Task ClearAsync();
Task<bool> ContainKeyAsync(string key);
Task<int> LengthAsync();
Task<string?> KeyAsync(int index);
```

---

### 2. LocalStorageService (التنفيذ الأساسي)

تنفيذ عام لعمليات localStorage مع دعم JSON Serialization.

#### الاستخدام:

```csharp
@inject IStorageService Storage

// حفظ بيانات
await Storage.SetItemAsync("myKey", myData);

// قراءة بيانات
var data = await Storage.GetItemAsync<MyType>("myKey");

// حذف
await Storage.RemoveItemAsync("myKey");

// التحقق من وجود مفتاح
bool exists = await Storage.ContainKeyAsync("myKey");

// مسح كل شيء
await Storage.ClearAsync();
```

#### مميزات:
- ✅ JSON Serialization تلقائي
- ✅ معالجة الأخطاء
- ✅ دعم Generic Types
- ✅ Case-insensitive property names

---

### 3. DataStorageService (بيانات الطفل)

خدمة متخصصة لتخزين بيانات الطفل والأحداث التعليمية.

#### الاستخدام:

```csharp
@inject DataStorageService DataStorage

// تحميل البيانات
var childData = await DataStorage.LoadDataAsync();

// حفظ البيانات
await DataStorage.SaveDataAsync(childData);

// إضافة حدث
await DataStorage.AddEventAsync(childData, new LearningEvent 
{
    Type = "attempt",
    Item = "A",
    Correct = true
});

// مسح البيانات
await DataStorage.ClearDataAsync();
```

#### المفتاح المستخدم:
```csharp
"bmokids_v1"
```

#### البيانات المخزنة:
- اسم الطفل
- عدد الجلسات
- الإجابات الصحيحة والخاطئة
- الأحداث التعليمية مع timestamps
- الإعدادات

---

### 4. SettingsStorageService (الإعدادات)

خدمة متخصصة لحفظ إعدادات التطبيق والتفضيلات.

#### الاستخدام:

```csharp
@inject SettingsStorageService SettingsStorage

// إعدادات الطفل
var settings = new ChildSettings { Age = 6, LanguagePreference = "ar-EG" };
await SettingsStorage.SaveChildSettingsAsync(settings);
var loadedSettings = await SettingsStorage.LoadChildSettingsAsync();

// السمة (Theme)
await SettingsStorage.SetThemeAsync("dark");
var theme = await SettingsStorage.GetThemeAsync();

// اللغة
await SettingsStorage.SetLanguageAsync("ar-EG");
var language = await SettingsStorage.GetLanguageAsync();

// الصوت
await SettingsStorage.SetSoundEnabledAsync(true);
bool soundEnabled = await SettingsStorage.IsSoundEnabledAsync();

// سرعة الصوت
await SettingsStorage.SetVoiceSpeedAsync(0.95);
double speed = await SettingsStorage.GetVoiceSpeedAsync();

// مسح كل الإعدادات
await SettingsStorage.ClearAllSettingsAsync();
```

#### المفاتيح المستخدمة:
- `bmokids_settings` - إعدادات الطفل
- `bmokids_theme` - السمة
- `bmokids_language` - اللغة
- `bmokids_sound_enabled` - تفعيل الصوت
- `bmokids_voice_speed` - سرعة الصوت

---

### 5. CacheStorageService (التخزين المؤقت)

خدمة للتخزين المؤقت مع دعم انتهاء الصلاحية.

#### الاستخدام:

```csharp
@inject CacheStorageService CacheStorage

// حفظ في Cache لمدة ساعة
await CacheStorage.SetCacheAsync("apiData", data, TimeSpan.FromHours(1));

// حفظ بدون انتهاء
await CacheStorage.SetCacheAsync("permanentData", data);

// قراءة من Cache
var cachedData = await CacheStorage.GetCacheAsync<MyType>("apiData");

// حذف من Cache
await CacheStorage.RemoveCacheAsync("apiData");

// مسح Cache المنتهي فقط
await CacheStorage.ClearExpiredCacheAsync();

// مسح كل Cache
await CacheStorage.ClearAllCacheAsync();
```

#### المميزات:
- ✅ انتهاء صلاحية تلقائي
- ✅ مسح Cache المنتهي
- ✅ بادئة تلقائية للمفاتيح (`bmokids_cache_`)

#### حالات الاستخدام:
- تخزين نتائج API
- تخزين البيانات المحسوبة
- تحسين الأداء

---

## التسجيل في Program.cs 📝

```csharp
// Register LocalStorage services
builder.Services.AddScoped<IStorageService, LocalStorageService>();
builder.Services.AddScoped<DataStorageService>();
builder.Services.AddScoped<SettingsStorageService>();
builder.Services.AddScoped<CacheStorageService>();
```

---

## أمثلة عملية 💡

### مثال 1: حفظ وتحميل بيانات الطفل

```csharp
@page "/example1"
@inject DataStorageService DataStorage

<h3>بيانات الطفل</h3>

@code {
    private ChildData? childData;

    protected override async Task OnInitializedAsync()
    {
        // تحميل البيانات
        childData = await DataStorage.LoadDataAsync();
        
        if (childData == null)
        {
            // إنشاء بيانات جديدة
            childData = new ChildData
            {
                Name = "رواء",
                Sessions = 1
            };
            
            await DataStorage.SaveDataAsync(childData);
        }
    }

    private async Task AddCorrectAnswer()
    {
        if (childData != null)
        {
            childData.Correct++;
            
            await DataStorage.AddEventAsync(childData, new LearningEvent
            {
                Type = "attempt",
                Item = "A",
                Correct = true,
                Confidence = 0.85
            });
        }
    }
}
```

### مثال 2: استخدام الإعدادات

```csharp
@page "/example2"
@inject SettingsStorageService SettingsStorage

<h3>الإعدادات</h3>

<button @onclick="ToggleTheme">تبديل السمة</button>
<button @onclick="ToggleSound">تبديل الصوت</button>

@code {
    private string currentTheme = "light";
    private bool soundEnabled = true;

    protected override async Task OnInitializedAsync()
    {
        currentTheme = await SettingsStorage.GetThemeAsync();
        soundEnabled = await SettingsStorage.IsSoundEnabledAsync();
    }

    private async Task ToggleTheme()
    {
        currentTheme = currentTheme == "light" ? "dark" : "light";
        await SettingsStorage.SetThemeAsync(currentTheme);
    }

    private async Task ToggleSound()
    {
        soundEnabled = !soundEnabled;
        await SettingsStorage.SetSoundEnabledAsync(soundEnabled);
    }
}
```

### مثال 3: استخدام Cache

```csharp
@page "/example3"
@inject CacheStorageService CacheStorage
@inject HttpClient Http

<h3>بيانات من API</h3>

@code {
    private async Task<MyData?> GetDataWithCache()
    {
        // محاولة قراءة من Cache
        var cachedData = await CacheStorage.GetCacheAsync<MyData>("apiData");
        
        if (cachedData != null)
        {
            Console.WriteLine("Data from cache");
            return cachedData;
        }

        // إذا لم توجد، اجلب من API
        var data = await Http.GetFromJsonAsync<MyData>("api/data");
        
        // احفظ في Cache لمدة 10 دقائق
        await CacheStorage.SetCacheAsync("apiData", data, TimeSpan.FromMinutes(10));
        
        return data;
    }
}
```

---

## أفضل الممارسات 🎯

### 1. استخدام أسماء مفاتيح واضحة

```csharp
// ✅ جيد
const string USER_PROFILE_KEY = "bmokids_user_profile";

// ❌ سيء
const string KEY1 = "k1";
```

### 2. معالجة الأخطاء

```csharp
try
{
    await DataStorage.SaveDataAsync(childData);
}
catch (Exception ex)
{
    Console.WriteLine($"Error saving data: {ex.Message}");
    // عرض رسالة للمستخدم
}
```

### 3. التحقق من null

```csharp
var data = await Storage.GetItemAsync<ChildData>("key");
if (data != null)
{
    // استخدم البيانات
}
```

### 4. استخدام Cache للبيانات المكلفة

```csharp
// للبيانات التي تتطلب حسابات معقدة أو API calls
await CacheStorage.SetCacheAsync("expensiveData", result, TimeSpan.FromMinutes(30));
```

### 5. تنظيف Cache الدوري

```csharp
// في مكان مناسب (مثلاً عند بدء التطبيق)
await CacheStorage.ClearExpiredCacheAsync();
```

---

## الحدود والقيود ⚠️

### localStorage Limits:
- **الحجم**: عادة 5-10 MB لكل domain
- **النوع**: String فقط (نستخدم JSON Serialization)
- **المتصفح**: يجب دعم HTML5

### التعامل مع الحدود:

```csharp
try
{
    await Storage.SetItemAsync("largeData", hugeObject);
}
catch (Exception ex)
{
    if (ex.Message.Contains("QuotaExceededError"))
    {
        // مسح بيانات قديمة أو Cache
        await CacheStorage.ClearExpiredCacheAsync();
        
        // إعادة المحاولة
        await Storage.SetItemAsync("largeData", hugeObject);
    }
}
```

---

## الأمان والخصوصية 🔒

### ما يُخزن:
- ✅ بيانات غير حساسة (اسم الطفل، إحصائيات)
- ✅ إعدادات التطبيق
- ✅ Cache مؤقت

### ما لا يُخزن:
- ❌ كلمات مرور
- ❌ بيانات شخصية حساسة
- ❌ معلومات مالية
- ❌ تسجيلات صوتية

### نصائح الأمان:
1. لا تخزن معلومات حساسة في localStorage
2. استخدم HTTPS دائماً
3. تحقق من البيانات قبل الاستخدام
4. امسح البيانات عند الخروج إذا لزم الأمر

---

## الاختبار 🧪

### اختبار وحدة (Unit Test):

```csharp
[Fact]
public async Task DataStorageService_SaveAndLoad_ReturnsCorrectData()
{
    // Arrange
    var storage = new DataStorageService(jsRuntime);
    var testData = new ChildData { Name = "Test", Sessions = 5 };

    // Act
    await storage.SaveDataAsync(testData);
    var loadedData = await storage.LoadDataAsync();

    // Assert
    Assert.Equal("Test", loadedData?.Name);
    Assert.Equal(5, loadedData?.Sessions);
}
```

---

## التوسعات المستقبلية 🚀

### مقترحات:

1. **Sync Service** - مزامنة مع السحابة
2. **Compression Service** - ضغط البيانات الكبيرة
3. **Encryption Service** - تشفير البيانات الحساسة
4. **IndexedDB Service** - للبيانات الكبيرة جداً
5. **Backup Service** - نسخ احتياطي تلقائي

---

## المساهمة 🤝

عند إضافة خدمة جديدة:
1. اتبع نفس النمط
2. أضف documentation
3. أضف أمثلة استخدام
4. سجل الخدمة في Program.cs
5. حدّث هذا الملف

---

**تم تطوير هذه الخدمات بـ ❤️ لتطبيق BmoKids**