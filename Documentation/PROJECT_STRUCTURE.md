# هيكل مشروع BmoKids 🏗️

## نظرة عامة على المجلدات والملفات

```
BmoKids/
│
├── 📁 Components/               # مكونات Blazor
│   │
│   ├── 📁 Pages/                # الصفحات
│   │   └── 📄 Home.razor        # الصفحة الرئيسية - المنطق الأساسي للتطبيق
│   │
│   ├── 📄 App.razor             # التطبيق الرئيسي والتخطيط
│   ├── 📄 Routes.razor          # المسارات وإعدادات التوجيه
│   ├── 📄 BMOFace.razor         # وجه BMO المتحرك والتعبيرات
│   ├── 📄 WelcomeScreen.razor   # شاشة الترحيب الأولى
│   ├── 📄 NameInput.razor       # إدخال اسم الطفل صوتياً
│   ├── 📄 SectionSelector.razor # اختيار القسم التعليمي
│   ├── 📄 LearningSession.razor # جلسة التعلم الصوتية
│   └── 📄 ParentDashboard.razor # لوحة تحكم ولي الأمر
│
├── 📁 Services/                 # الخدمات والمنطق
│   ├── 📁 LocalStorage/         # خدمات التخزين المحلي
│   │   ├── 📄 IStorageService.cs          # الواجهة الأساسية
│   │   ├── 📄 LocalStorageService.cs      # التنفيذ الأساسي
│   │   ├── 📄 DataStorageService.cs       # تخزين بيانات الطفل
│   │   ├── 📄 SettingsStorageService.cs   # تخزين الإعدادات
│   │   ├── 📄 CacheStorageService.cs      # التخزين المؤقت
│   │   └── 📄 README.md                   # توثيق الخدمات
│   ├── 📄 SpeechService.cs      # تقييم النطق والتعرف على الصوت
│   └── 📄 AssessmentService.cs  # المحتوى التعليمي (حروف، أرقام)
│
├── 📁 Models/                   # نماذج البيانات
│   └── 📄 ChildData.cs          # نماذج بيانات الطفل والأحداث
│
├── 📁 wwwroot/                  # الموارد الثابتة
│   ├── 📁 css/
│   │   └── 📄 app.css           # التصميم الكامل والمتجاوب
│   ├── 📁 js/
│   │   └── 📄 bmokids.js        # Web Speech API Integration
│   └── 📄 favicon.png           # أيقونة التطبيق
│
├── 📄 Program.cs                # نقطة البداية وتسجيل الخدمات
├── 📄 appsettings.json          # الإعدادات القابلة للتخصيص
├── 📄 BmoKids.csproj            # ملف المشروع
│
└── 📁 Documentation/            # التوثيق
    ├── 📄 README.md             # الدليل الشامل
    ├── 📄 QUICKSTART.md         # دليل التشغيل السريع
    └── 📄 PROJECT_STRUCTURE.md  # هذا الملف
```

## شرح تفصيلي للملفات الرئيسية

### 🎯 Core Files (الملفات الأساسية)

#### `Program.cs`
- نقطة بدء التطبيق
- تسجيل الخدمات في DI Container
- إعداد Blazor Server Pipeline

**الخدمات المُسجلة:**
```csharp
// Register LocalStorage services
builder.Services.AddScoped<IStorageService, LocalStorageService>();
builder.Services.AddScoped<DataStorageService>();
builder.Services.AddScoped<SettingsStorageService>();
builder.Services.AddScoped<CacheStorageService>();

// Register BmoKids services
builder.Services.AddScoped<SpeechService>();
builder.Services.AddScoped<AssessmentService>();
```

#### `BmoKids.csproj`
- ملف تعريف المشروع
- Target Framework: .NET 10
- Dependencies والحزم المطلوبة

---

### 🎨 Components (المكونات)

#### `Components/App.razor`
- التطبيق الرئيسي
- HTML الأساسي والـ Head
- تحميل CSS و JavaScript

#### `Components/Routes.razor`
- إعداد Router
- معالجة الصفحات غير الموجودة (404)

#### `Components/Pages/Home.razor`
**المسؤوليات:**
- إدارة حالة التطبيق (AppState Enum)
- التنقل بين الشاشات
- استقبال الأوامر الصوتية من الوالد
- تحميل وحفظ بيانات الطفل

**الحالات الممكنة:**
- `Welcome` - شاشة الترحيب
- `AskingName` - سؤال الاسم
- `SelectingSection` - اختيار القسم
- `Learning` - جلسة التعلم
- `ParentDashboard` - لوحة الوالد

#### `Components/BMOFace.razor`
**المسؤوليات:**
- رسم وجه BMO باستخدام SVG
- التبديل بين التعبيرات (سعيد، حزين، يتكلم)
- حركة العيون والفم

**التعبيرات المتاحة:**
- `Neutral` - محايد
- `Closed` - مغلق الفم
- `Half` - نصف مفتوح
- `Open` - مفتوح
- `Wide` - واسع (نجاح)
- `Sad` - حزين (خطأ)

#### `Components/WelcomeScreen.razor`
**المسؤوليات:**
- عرض شاشة البداية
- تشغيل الترحيب الصوتي التلقائي
- الانتقال التلقائي لسؤال الاسم

**التوقيت:**
- 600ms: ظهور الوجه
- 1000ms: بدء التحية الصوتية
- ~5000ms: الانتقال للاسم

#### `Components/NameInput.razor`
**المسؤوليات:**
- سؤال الطفل عن اسمه صوتياً
- استقبال وتقييم النطق
- طلب التأكيد إذا لزم الأمر
- معالجة الأخطاء والمحاولات المتكررة

**آلية العمل:**
1. نطق السؤال
2. الاستماع للإجابة (5 ثوانٍ)
3. استخراج الاسم من النص
4. تقييم الثقة (Confidence)
5. قبول أو طلب تأكيد

#### `Components/SectionSelector.razor`
**المسؤوليات:**
- عرض 4 أقسام تعليمية
- استقبال اختيار الطفل (لمس)
- تأكيد الاختيار صوتياً

**الأقسام:**
- 🔤 إنجليزي
- أ عربي
- 🏃 رياضة (أرقام)
- 💬 نتكلم (قريباً)

#### `Components/LearningSession.razor`
**المسؤوليات:**
- تحميل المحتوى التعليمي
- عرض العنصر (حرف/رقم)
- نطق التعليم
- الاستماع للإجابة
- التقييم والتغذية الراجعة
- تتبع التقدم

**دورة الدرس:**
1. عرض العنصر (3 ثوانٍ)
2. نطق التعليم
3. الاستماع للإجابة (3.5 ثوانٍ)
4. تقييم الإجابة
5. إعطاء تغذية راجعة
6. الانتقال للعنصر التالي

**المحاولات:**
- 2 محاولات لكل عنصر
- بعد الفشل: إعطاء تلميح والانتقال

#### `Components/ParentDashboard.razor`
**المسؤوليات:**
- عرض إحصائيات شاملة
- قائمة آخر الأنشطة
- تصدير البيانات (JSON)
- مسح البيانات

**البيانات المعروضة:**
- عدد الجلسات
- الإجابات الصحيحة
- الإجابات الخاطئة
- نسبة النجاح
- آخر 10 أنشطة

---

### ⚙️ Services (الخدمات)

#### Services/LocalStorage/ (خدمات التخزين المحلي)

##### `IStorageService.cs`
**الواجهة الأساسية:**
- تعريف العمليات الأساسية للتخزين
- دعم Generic Types
- عمليات CRUD كاملة

##### `LocalStorageService.cs`
**التنفيذ الأساسي:**
- تنفيذ IStorageService
- JSON Serialization تلقائي
- معالجة الأخطاء
- دعم localStorage API

**العمليات المتاحة:**
- `SetItemAsync<T>` - حفظ
- `GetItemAsync<T>` - قراءة
- `RemoveItemAsync` - حذف
- `ClearAsync` - مسح الكل
- `ContainKeyAsync` - التحقق من وجود
- `LengthAsync` - عدد العناصر
- `KeyAsync` - الحصول على مفتاح

##### `DataStorageService.cs`
**المسؤوليات:**
- حفظ البيانات في localStorage
- تحميل البيانات المحفوظة
- حذف البيانات
- إضافة الأحداث مع timestamps

**المفتاح المستخدم:**
```csharp
private const string STORAGE_KEY = "bmokids_v1";
```

**العمليات المتاحة:**
- `LoadDataAsync()` - تحميل البيانات
- `SaveDataAsync(ChildData)` - حفظ البيانات
- `ClearDataAsync()` - حذف البيانات
- `AddEventAsync(ChildData, LearningEvent)` - إضافة حدث

##### `DataStorageService.cs`
**المسؤوليات:**
- حفظ بيانات الطفل في localStorage
- تحميل البيانات المحفوظة
- حذف البيانات
- إضافة الأحداث مع timestamps

**المفتاح المستخدم:**
```csharp
private const string STORAGE_KEY = "bmokids_v1";
```

**العمليات المتاحة:**
- `LoadDataAsync()` - تحميل البيانات
- `SaveDataAsync(ChildData)` - حفظ البيانات
- `ClearDataAsync()` - حذف البيانات
- `AddEventAsync(ChildData, LearningEvent)` - إضافة حدث

##### `SettingsStorageService.cs`
**المسؤوليات:**
- حفظ إعدادات الطفل
- إعدادات التطبيق (Theme, Language)
- تفعيل/تعطيل الصوت
- سرعة النطق

**المفاتيح المستخدمة:**
- `bmokids_settings` - إعدادات الطفل
- `bmokids_theme` - السمة
- `bmokids_language` - اللغة
- `bmokids_sound_enabled` - الصوت
- `bmokids_voice_speed` - السرعة

**العمليات:**
- `SaveChildSettingsAsync` / `LoadChildSettingsAsync`
- `SetThemeAsync` / `GetThemeAsync`
- `SetLanguageAsync` / `GetLanguageAsync`
- `SetSoundEnabledAsync` / `IsSoundEnabledAsync`
- `SetVoiceSpeedAsync` / `GetVoiceSpeedAsync`

##### `CacheStorageService.cs`
**المسؤوليات:**
- التخزين المؤقت مع انتهاء صلاحية
- مسح Cache المنتهي تلقائياً
- تحسين الأداء

**المميزات:**
- ✅ انتهاء صلاحية تلقائي
- ✅ مسح Cache المنتهي
- ✅ بادئة تلقائية (`bmokids_cache_`)

**العمليات:**
- `SetCacheAsync<T>(key, value, expiration)`
- `GetCacheAsync<T>(key)`
- `RemoveCacheAsync(key)`
- `ClearExpiredCacheAsync()`
- `ClearAllCacheAsync()`

#### `Services/SpeechService.cs`
**المسؤوليات:**
- تقييم إجابات الطفل
- مقارنة النطق مع المتوقع
- حساب Levenshtein Distance
- استخراج الأسماء من النصوص

**خوارزمية التقييم:**
1. فحص المطابقة المباشرة
2. فحص النطق المقبول
3. حساب Levenshtein Distance
4. مقارنة مع Thresholds حسب العمر

**Thresholds حسب العمر:**
- 3-5 سنوات: 0.28
- 6-8 سنوات: 0.35
- 9-12 سنة: 0.45

#### `Services/AssessmentService.cs`
**المسؤوليات:**
- توفير المحتوى التعليمي
- الحروف الإنجليزية (A-Z)
- الحروف العربية (أ-ي)
- الأرقام (1-20)

**بنية العنصر التعليمي:**
```csharp
public class LessonItem {
    string Value;              // القيمة (A, ب, 5)
    string DisplayText;        // النص المعروض
    List<string> AcceptedPronunciations; // النطق المقبول
    string TeachingText;       // نص التعليم
    string Language;           // اللغة (en-US, ar-EG)
}
```

---

### 📊 Models (النماذج)

#### `Models/ChildData.cs`
**الفئات المُعرَّفة:**

1. **ChildData** - بيانات الطفل الأساسية
2. **ChildSettings** - إعدادات الطفل
3. **LearningEvent** - حدث تعليمي
4. **RecognitionResult** - نتيجة التعرف على الصوت
5. **RecognitionAlternative** - بديل للنطق
6. **MouthState** - حالة فم BMO
7. **LearningSection** - القسم التعليمي
8. **LessonItem** - عنصر تعليمي

**مثال على البيانات:**
```json
{
  "name": "رواء",
  "sessions": 5,
  "correct": 23,
  "wrong": 7,
  "interactions": 30,
  "events": [
    {
      "type": "attempt",
      "item": "A",
      "correct": true,
      "confidence": 0.85,
      "timestamp": 1699564800000
    }
  ]
}
```

---

### 🎨 wwwroot (الموارد الثابتة)

#### `wwwroot/css/app.css`
**الأقسام:**
- Reset و Variables
- Global Styles
- Welcome Screen
- Name Input
- Section Selector
- Learning Session
- Parent Dashboard
- BMO Face (SVG)
- Animations
- Responsive Design

**المتغيرات الرئيسية:**
```css
--primary-color: #90EE90;
--success-color: #4CAF50;
--error-color: #f44336;
```

#### `wwwroot/js/bmokids.js`
**الوظائف الرئيسية:**

1. **init()** - تهيئة Web Speech API
2. **speak(text, lang, rate, pitch)** - النطق الصوتي
3. **recognizeSpeech(lang, timeout)** - التعرف على الصوت
4. **initParentListener(dotNetHelper)** - المستمع الخلفي للوالد
5. **playSuccessSound()** - صوت النجاح
6. **downloadFile(filename, content)** - تحميل الملفات

**Web Speech APIs المستخدمة:**
- `SpeechSynthesis` - للنطق
- `SpeechRecognition` - للتعرف
- `SpeechSynthesisUtterance` - للجمل المنطوقة

---

### ⚙️ Configuration (الإعدادات)

#### `appsettings.json`
**الأقسام:**

1. **Logging** - إعدادات السجلات
2. **BmoKids** - إعدادات التطبيق
   - SpeechRecognition
   - TextToSpeech
   - Assessment (Thresholds)

**مثال:**
```json
{
  "BmoKids": {
    "SpeechRecognition": {
      "DefaultLanguage": "ar-EG",
      "DefaultTimeout": 5000
    }
  }
}
```

---

## 🔄 تدفق البيانات (Data Flow)

### مسار البيانات الأساسي:

```
User Input (Voice/Touch)
    ↓
JavaScript (bmokids.js)
    ↓
Blazor Components
    ↓
Services (SpeechService, DataStorageService)
    ↓
Models (ChildData)
    ↓
localStorage
```

### مسار الصوت:

```
Microphone → SpeechRecognition API
    ↓
bmokids.js (recognizeSpeech)
    ↓
Blazor Component (NameInput/LearningSession)
    ↓
SpeechService (EvaluateAnswer)
    ↓
Feedback to User (TTS + Visual)
```

---

## 📦 الحزم والتبعيات

### Required NuGet Packages:
- `Microsoft.AspNetCore.Components.Web` (10.0.0)

### Browser APIs المستخدمة:
- Web Speech API (SpeechRecognition)
- Web Speech API (SpeechSynthesis)
- localStorage
- Web Audio API (للأصوات)

---

## 🔐 الأمان والخصوصية

### البيانات المُخزنة محلياً:
- اسم الطفل
- الإحصائيات (جلسات، صح، خطأ)
- الأحداث التعليمية

### لا يتم تخزين:
- التسجيلات الصوتية
- معلومات شخصية حساسة
- بيانات الوالدين

### الصلاحيات المطلوبة:
- Microphone Access (للتعرف على الصوت)
- HTTPS (مطلوب للـ Speech Recognition)

---

## 🚀 نقاط التوسع المستقبلية

### ملفات جديدة مقترحة:

1. **Services/FirebaseService.cs** - للمزامنة السحابية
2. **Services/MLService.cs** - للتعلم الآلي
3. **Components/GameSession.razor** - للألعاب التعليمية
4. **Models/Achievement.cs** - نظام الإنجازات

### أقسام جديدة في CSS:
- Achievements UI
- Progress Charts
- Multiplayer UI

---

هذا الهيكل قابل للتوسع ومُصمم بطريقة Modular لسهولة الصيانة والتطوير! 🎉