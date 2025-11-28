# دليل التشغيل السريع - BmoKids 🚀

## التشغيل في 5 دقائق

### الخطوة 1: التأكد من المتطلبات ✅
```bash
# تحقق من وجود .NET 10
dotnet --version
# يجب أن يكون 10.0.0 أو أحدث
```

### الخطوة 2: إنشاء المشروع 📦
```bash
# إنشاء مجلد المشروع
mkdir BmoKids
cd BmoKids

# إنشاء مشروع Blazor Server جديد
dotnet new blazorserver -n BmoKids -o .
```

### الخطوة 3: نسخ الملفات 📋

#### 3.1 هيكل المجلدات
```bash
mkdir -p Components/Pages
mkdir -p Services
mkdir -p Models
mkdir -p wwwroot/css
mkdir -p wwwroot/js
```

#### 3.2 الملفات المطلوبة
انسخ الملفات التالية إلى المشروع:

**الخدمات (Services/):**
- `DataStorageService.cs`
- `SpeechService.cs`
- `AssessmentService.cs`

**النماذج (Models/):**
- `ChildData.cs`

**المكونات (Components/):**
- `App.razor`
- `Routes.razor`
- `BMOFace.razor`
- `WelcomeScreen.razor`
- `NameInput.razor`
- `SectionSelector.razor`
- `LearningSession.razor`
- `ParentDashboard.razor`

**الصفحات (Components/Pages/):**
- `Home.razor`

**الموارد (wwwroot/):**
- `css/app.css`
- `js/bmokids.js`

**الإعدادات:**
- `Program.cs`
- `appsettings.json`

### الخطوة 4: تحديث Program.cs 🔧
تأكد من أن `Program.cs` يحتوي على:
```csharp
builder.Services.AddScoped<DataStorageService>();
builder.Services.AddScoped<SpeechService>();
builder.Services.AddScoped<AssessmentService>();
```

### الخطوة 5: التشغيل 🎉
```bash
# تشغيل التطبيق
dotnet run

# افتح المتصفح على
# https://localhost:5001
```

## نصائح للاختبار السريع 🧪

### اختبار الصوت
1. **امنح إذن المايكروفون** عند الطلب
2. **تكلم بوضوح** عند سؤال الاسم
3. **استخدم Chrome** للحصول على أفضل نتائج

### اختبار الدروس
1. اضغط على "إنجليزي"
2. انطق الحرف عندما يطلب منك BMO
3. شاهد التغذية الراجعة

### اختبار لوحة الوالد
1. اضغط زر "👤 ولي الأمر" أسفل اليسار
2. أو قل "عايزة بيانات الطفل"

## استكشاف الأخطاء السريع 🔍

### المشكلة: المايكروفون لا يعمل
```
الحل: تأكد من HTTPS وامنح الإذن في المتصفح
```

### المشكلة: الصوت لا يعمل
```
الحل: تأكد من تشغيل الصوت وأن المتصفح ليس في وضع صامت
```

### المشكلة: خطأ في التعرف على الصوت
```
الحل: تأكد من استخدام Chrome أو Edge
Safari لديه دعم محدود
```

## الإعدادات السريعة ⚙️

### تغيير العمر الافتراضي
في `appsettings.json`:
```json
"Assessment": {
  "DefaultAge": 6  // غيّر هذا الرقم (3-12)
}
```

### تغيير سرعة النطق
في `bmokids.js`، الدالة `speak()`:
```javascript
utterance.rate = 0.95; // أبطأ: 0.8, أسرع: 1.2
```

## الخطوات التالية 📚

1. **اقرأ الوثائق الكاملة** في README.md
2. **جرب جميع الأقسام** (إنجليزي، عربي، رياضة)
3. **اختبر مع أطفال حقيقيين** للحصول على تغذية راجعة
4. **عدّل الإعدادات** حسب احتياجاتك

## موارد إضافية 🔗

- [Blazor Documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [Web Speech API](https://developer.mozilla.org/en-US/docs/Web/API/Web_Speech_API)
- [.NET 10 Release Notes](https://github.com/dotnet/core/tree/main/release-notes/10.0)

---

**هل واجهت مشكلة؟** افتح issue على GitHub أو راسلنا على support@bmokids.com