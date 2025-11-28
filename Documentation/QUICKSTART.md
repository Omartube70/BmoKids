<div align="center">

# ⚡ دليل البدء السريع - BMO Kids

<img src="../BmoImages/WhatsApp%20Image%202025-11-28%20at%2010.19.12%20AM%20(1).jpeg" width="150">

### 🚀 من الصفر إلى التشغيل في 5 دقائق!

</div>

---

## 📋 المحتويات السريعة

- [التحقق من المتطلبات](#-الخطوة-1-التحقق-من-المتطلبات)
- [التثبيت](#-الخطوة-2-التثبيت)
- [التشغيل](#-الخطوة-3-التشغيل)
- [الاختبار الأول](#-الخطوة-4-الاختبار-الأول)
- [استكشاف الأخطاء](#-استكشاف-الأخطاء)

---

## 🎯 الخطوة 1: التحقق من المتطلبات

### تحقق من .NET 10

افتح Terminal/PowerShell واكتب:

```bash
dotnet --version
```

**يجب أن يظهر:** `10.0.0` أو أحدث

### ❌ إذا لم يكن مثبتًا

<details>
<summary><b>تثبيت .NET 10 على Windows</b></summary>

1. اذهب إلى: https://dotnet.microsoft.com/download/dotnet/10.0
2. حمّل **SDK** (وليس Runtime)
3. شغّل الملف واتبع التعليمات
4. أعد فتح Terminal واختبر مرة أخرى

</details>

<details>
<summary><b>تثبيت .NET 10 على Mac</b></summary>

```bash
# باستخدام Homebrew
brew install dotnet-sdk
```

</details>

<details>
<summary><b>تثبيت .NET 10 على Linux</b></summary>

```bash
# Ubuntu/Debian
wget https://dot.net/v1/dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --channel 10.0
```

</details>

---

## 📦 الخطوة 2: التثبيت

### الطريقة الأولى: استنساخ من GitHub (موصى به)

```bash
# 1. استنساخ المشروع
git clone https://github.com/Omartube70/BmoKids.git

# 2. الانتقال للمجلد
cd BmoKids

# 3. استعادة الحزم
dotnet restore
```

### الطريقة الثانية: تحميل ZIP

1. اذهب إلى: https://github.com/Omartube70/BmoKids
2. اضغط `Code` ← `Download ZIP`
3. فك الضغط
4. افتح Terminal في المجلد
5. شغّل: `dotnet restore`

---

## 🎮 الخطوة 3: التشغيل

### تشغيل التطبيق

```bash
dotnet run
```

**انتظر حتى تظهر:**

```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7286
      Now listening on: http://localhost:5218
```

### فتح المتصفح

افتح أحد الروابط التالية:

- **HTTPS (موصى به):** https://localhost:7286
- **HTTP:** http://localhost:5218

> ⚠️ **مهم:** استخدم HTTPS لتشغيل Web Speech API

---

## ✅ الخطوة 4: الاختبار الأول

### 1️⃣ شاشة الترحيب

<img src="../BmoImages/WhatsApp%20Image%202025-11-28%20at%2010.19.12%20AM.jpeg" width="400">

- **ما يجب أن تراه:** وجه BMO يظهر
- **ما يجب أن تسمعه:** "أهلاً! أنا BMO..."
- **المدة:** ~5 ثوانٍ

### 2️⃣ إدخال الاسم

<img src="../BmoImages/WhatsApp%20Image%202025-11-28%20at%2010.19.13%20AM.jpeg" width="400">

**الخطوات:**
1. امنح إذن المايكروفون عندما يُطلب منك
2. انتظر حتى يسأل BMO: "ممكن أعرف اسمك؟"
3. قل اسمك بوضوح: مثلاً "أحمد"
4. انتظر التأكيد

**نصائح:**
- 🎤 تكلم في بيئة هادئة
- 🔊 استخدم صوت واضح
- ⏱️ لديك 5 ثوانٍ للإجابة

### 3️⃣ اختيار القسم

<img src="../BmoImages/WhatsApp%20Image%202025-11-28%20at%2010.19.14%20AM.jpeg" width="400">

اختر أحد الأقسام:
- 🔤 **إنجليزي** - تعلم A-Z
- أ **عربي** - تعلم الحروف العربية
- 🔢 **رياضة** - تعلم الأرقام
- 💬 **نتكلم** - محادثة حرة (قريبًا)

### 4️⃣ جلسة التعلم

**دورة التعلم:**
```
عرض الحرف (3 ثوانٍ)
      ↓
BMO ينطق الحرف
      ↓
انتظار إجابتك (3.5 ثانية)
      ↓
التقييم والتغذية الراجعة
      ↓
الانتقال للحرف التالي
```

**النتائج المتوقعة:**
- ✅ **صحيح:** "براڤو! ممتاز! 🎉"
- ❌ **خطأ:** "مش كده، نجرب تاني 💪"
- 🔁 **محاولة ثانية:** فرصة أخرى
- 💡 **بعد محاولتين:** BMO يعطيك التلميح

---

## 🔍 استكشاف الأخطاء

### ❌ المشكلة: المايكروفون لا يعمل

<details>
<summary><b>الحلول المقترحة</b></summary>

**1. تحقق من إذن المتصفح**
```
Chrome: Settings → Privacy and Security → Site Settings → Microphone
```

**2. تأكد من HTTPS**
- يجب استخدام `https://` وليس `http://`
- Web Speech API لا يعمل على HTTP

**3. جرب متصفح آخر**
- ✅ Chrome (موصى به)
- ✅ Edge
- ⚠️ Firefox (دعم محدود)
- ❌ Safari (لا يدعم Recognition)

**4. اختبر المايكروفون**
```javascript
// في Console
navigator.mediaDevices.getUserMedia({ audio: true })
  .then(() => console.log('✅ المايكروفون يعمل'))
  .catch(err => console.error('❌ خطأ:', err));
```

</details>

### ❌ المشكلة: الصوت لا يعمل

<details>
<summary><b>الحلول المقترحة</b></summary>

**1. تحقق من صوت النظام**
- تأكد أن الصوت ليس مكتومًا
- ارفع الصوت إلى مستوى مسموع

**2. تحقق من صوت المتصفح**
```
Chrome: انقر على أيقونة السماعة في tab
```

**3. جرب تشغيل صوت**
```javascript
// في Console
BmoKids.speak('مرحباً', 'ar-EG', 0.95, 1.0);
```

</details>

### ❌ المشكلة: خطأ في التعرف على الصوت

<details>
<summary><b>الحلول المقترحة</b></summary>

**1. تحقق من اللغة**
```javascript
// في Console
console.log(navigator.language); // يجب أن يكون ar أو en
```

**2. قلل الضوضاء الخلفية**
- استخدم بيئة هادئة
- أغلق التطبيقات الصوتية الأخرى

**3. تكلم بوضوح**
- استخدم نطق واضح
- لا تتكلم بسرعة
- اقترب من المايكروفون

**4. جرب إعادة تشغيل المتصفح**

</details>

### ❌ المشكلة: البيانات لا تُحفظ

<details>
<summary><b>الحلول المقترحة</b></summary>

**1. تحقق من localStorage**
```javascript
// في Console
console.log(localStorage.getItem('bmokids_v1'));
```

**2. تأكد من عدم استخدام Private/Incognito Mode**

**3. امسح Cache وأعد المحاولة**
```
Chrome: Settings → Privacy → Clear browsing data
```

</details>

---

## 🎓 خطوات متقدمة

### تشغيل في Production Mode

```bash
# Build المشروع
dotnet publish -c Release

# تشغيل النسخة المُنتجة
cd bin/Release/net10.0/publish
dotnet BmoKids.dll
```

### تخصيص المنفذ (Port)

```bash
# في launchSettings.json
"applicationUrl": "https://localhost:YOUR_PORT"
```

أو من Command Line:

```bash
dotnet run --urls "https://localhost:8080"
```

### تفعيل Hot Reload

```bash
dotnet watch run
```

الآن أي تغيير في الكود سيُحدّث تلقائيًا! 🔥

---

## 📝 ملاحظات مهمة

### متطلبات المتصفح

| المتصفح | التعرف الصوتي | النطق | التقييم |
|---------|---------------|-------|---------|
| Chrome | ✅ ممتاز | ✅ ممتاز | ⭐⭐⭐⭐⭐ |
| Edge | ✅ ممتاز | ✅ ممتاز | ⭐⭐⭐⭐⭐ |
| Firefox | ⚠️ محدود | ✅ جيد | ⭐⭐⭐ |
| Safari | ❌ لا يدعم | ✅ جيد | ⭐⭐ |

### أداء النظام

**الحد الأدنى:**
- RAM: 4GB
- CPU: Dual Core
- المتصفح: Chrome 90+

**موصى به:**
- RAM: 8GB+
- CPU: Quad Core
- المتصفح: Chrome Latest

### حجم البيانات

- **localStorage:** حتى 10MB
- **بيانات الطفل:** ~100KB
- **Cache:** ~1MB

---

## 🎯 الخطوات التالية

### 1. اقرأ التوثيق الكامل

```bash
cd Documentation
# README.md - الدليل الشامل
# PROJECT_STRUCTURE.md - هيكل المشروع
```

### 2. جرب جميع الأقسام

- [ ] الإنجليزية
- [ ] العربية
- [ ] الأرقام

### 3. افتح لوحة الوالدين

```
زر "👤 ولي الأمر" في أسفل اليسار
```

### 4. صدّر البيانات

```
لوحة الوالدين → "📤 تصدير البيانات"
```

### 5. ساهم في المشروع

```bash
git checkout -b feature/my-awesome-feature
# أضف ميزتك
git commit -m "إضافة ميزة رائعة"
git push origin feature/my-awesome-feature
```

---

## 💡 نصائح للاستخدام الأمثل

### للأطفال 👦👧

- 🎤 تكلم بوضوح
- 🔊 استخدم صوت مسموع
- 😊 لا تخف من الأخطاء
- 🔁 جرب مرة أخرى
- 🎉 استمتع بالتعلم!

### للآباء 👨‍👩‍👧‍👦

- 📊 تابع التقدم يوميًا
- ⏰ جلسات قصيرة (10-15 دقيقة)
- 🏆 شجع على المحاولة
- 📈 راقب نسبة النجاح
- 💬 ناقش ما تعلمه الطفل

### للمطورين 👨‍💻

- 🔍 استخدم Console للتشخيص
- 🐛 افتح Issues عند المشاكل
- 🤝 ساهم في التحسينات
- 📖 حدّث التوثيق
- ⭐ أعط النجمة على GitHub!

---

## 📞 هل تحتاج مساعدة؟

### الموارد

- 📖 [الوثائق الكاملة](README.md)
- 🐛 [GitHub Issues](https://github.com/Omartube70/BmoKids/issues)
- 💬 [Discussions](https://github.com/Omartube70/BmoKids/discussions)
- 📧 [البريد الإلكتروني](mailto:support@bmokids.com)

### المجتمع

انضم إلى مجتمعنا على:
- Discord (قريبًا)
- Telegram (قريبًا)
- Twitter: @BmoKids

---

<div align="center">

## 🎉 مبروك! أنت جاهز الآن!

<img src="../BmoImages/WhatsApp%20Image%202025-11-28%20at%2010.19.13%20AM%20(1).jpeg" width="200">

### استمتع بتجربة BMO Kids! 🚀

[⬆️ العودة للأعلى](#-دليل-البدء-السريع---bmo-kids)

</div>