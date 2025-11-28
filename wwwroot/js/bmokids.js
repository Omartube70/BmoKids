// BmoKids - JavaScript للتعامل مع Web Speech API
window.BmoKids = {
    // متغيرات عامة
    recognition: null,
    synthesis: window.speechSynthesis,
    currentUtterance: null,
    isSpeaking: false,
    parentListener: null,
    dotNetHelper: null,

    // تهيئة النظام
    init: function () {
        // التحقق من دعم المتصفح
        if (!('webkitSpeechRecognition' in window) && !('SpeechRecognition' in window)) {
            console.error('Speech Recognition not supported');
            return false;
        }

        // إنشاء مثيل للتعرف على الصوت
        const SpeechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition;
        this.recognition = new SpeechRecognition();

        return true;

    },

    // النطق (Text-to-Speech)
    speak: function (text, lang = 'ar-EG', rate = 0.95, pitch = 1.0) {
        return new Promise((resolve, reject) => {
            // إلغاء أي نطق جاري
            if (this.isSpeaking) {
                this.synthesis.cancel();
            }

            const utterance = new SpeechSynthesisUtterance(text);
            utterance.lang = lang;
            utterance.rate = rate;
            utterance.pitch = pitch;
            utterance.volume = 1.0;

            // محاولة اختيار صوت مناسب
            const voices = this.synthesis.getVoices();
            const voice = voices.find(v => v.lang.startsWith(lang.split('-')[0])) || voices[0];
            if (voice) {
                utterance.voice = voice;
            }

            utterance.onstart = () => {
                this.isSpeaking = true;
                this.animateMouth(true);
            };

            utterance.onend = () => {
                this.isSpeaking = false;
                this.animateMouth(false);
                resolve();
            };

            utterance.onerror = (error) => {
                this.isSpeaking = false;
                this.animateMouth(false);
                reject(error);
            };

            this.currentUtterance = utterance;
            this.synthesis.speak(utterance);
        });
    },

    // تحريك الفم أثناء النطق
    animateMouth: function (isActive) {
        // يمكن إضافة تحريك الفم هنا
        // سيتم التعامل معه عبر Blazor Component
    },

    // التعرف على الصوت (Speech Recognition)
    recognizeSpeech: function (lang = 'ar-EG', timeout = 5000) {
        return new Promise((resolve, reject) => {
            if (!this.recognition) {
                if (!this.init()) {
                    reject('Speech Recognition not supported');
                    return;
                }
            }

            const recognition = this.recognition;
            recognition.lang = lang;
            recognition.interimResults = false;
            recognition.continuous = false;
            recognition.maxAlternatives = 5;

            let timeoutId = null;
            let hasResult = false;

            recognition.onstart = () => {
                console.log('Speech recognition started');
                // إيقاف التعرف تلقائياً بعد timeout
                timeoutId = setTimeout(() => {
                    if (!hasResult) {
                        recognition.stop();
                        reject('timeout');
                    }
                }, timeout);
            };

            recognition.onresult = (event) => {
                hasResult = true;
                clearTimeout(timeoutId);

                const result = event.results[0];
                const transcript = result[0].transcript.trim();
                const confidence = result[0].confidence || 0.5;

                console.log('Recognized:', transcript, 'Confidence:', confidence);

                // يمكن إرجاع كائن يحتوي على البدائل أيضاً
                const alternatives = [];
                for (let i = 0; i < result.length && i < 5; i++) {
                    alternatives.push({
                        transcript: result[i].transcript.trim(),
                        confidence: result[i].confidence || 0.5
                    });
                }

                resolve(transcript);
            };

            recognition.onerror = (event) => {
                clearTimeout(timeoutId);
                console.error('Speech recognition error:', event.error);
                reject(event.error);
            };

            recognition.onend = () => {
                clearTimeout(timeoutId);
                if (!hasResult) {
                    reject('no-speech');
                }
            };

            try {
                recognition.start();
            } catch (error) {
                console.error('Error starting recognition:', error);
                reject(error);
            }
        });
    },

    // مستمع خلفي لأوامر الوالد
    initParentListener: function (dotNetHelper) {
        this.dotNetHelper = dotNetHelper;

        const SpeechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition;
        if (!SpeechRecognition) return;

        this.parentListener = new SpeechRecognition();
        this.parentListener.lang = 'ar-EG';
        this.parentListener.continuous = true;
        this.parentListener.interimResults = false;

        this.parentListener.onresult = (event) => {
            const transcript = event.results[event.results.length - 1][0].transcript.toLowerCase();
            console.log('Parent command detected:', transcript);

            // التحقق من الكلمات المفتاحية
            if (transcript.includes('بيانات') || transcript.includes('عايزة') ||
                transcript.includes('إيقاف') || transcript.includes('وقف')) {

                if (this.dotNetHelper) {
                    this.dotNetHelper.invokeMethodAsync('OnParentCommand', transcript);
                }
            }
        };

        this.parentListener.onend = () => {
            // إعادة التشغيل تلقائياً بعد 500ms
            setTimeout(() => {
                try {
                    if (this.parentListener) {
                        this.parentListener.start();
                    }
                } catch (e) {
                    console.log('Parent listener restart error:', e);
                }
            }, 500);
        };

        try {
            this.parentListener.start();
        } catch (e) {
            console.log('Parent listener start error:', e);
        }
    },

    // صوت النجاح
    playSuccessSound: function () {
        // يمكن إضافة ملف صوتي أو استخدام Web Audio API
        const audioContext = new (window.AudioContext || window.webkitAudioContext)();
        const oscillator = audioContext.createOscillator();
        const gainNode = audioContext.createGain();

        oscillator.connect(gainNode);
        gainNode.connect(audioContext.destination);

        oscillator.frequency.value = 523.25; // C5
        oscillator.type = 'sine';

        gainNode.gain.setValueAtTime(0.3, audioContext.currentTime);
        gainNode.gain.exponentialRampToValueAtTime(0.01, audioContext.currentTime + 0.5);

        oscillator.start(audioContext.currentTime);
        oscillator.stop(audioContext.currentTime + 0.5);
    },

    // تحميل ملف
    downloadFile: function (filename, content) {
        const blob = new Blob([content], { type: 'application/json' });
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = filename;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        URL.revokeObjectURL(url);
    },

    // تنظيف
    dispose: function () {
        if (this.recognition) {
            this.recognition.stop();
        }
        if (this.parentListener) {
            this.parentListener.stop();
        }
        if (this.synthesis) {
            this.synthesis.cancel();
        }
    }
};

// تهيئة عند تحميل الصفحة
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', () => BmoKids.init());
} else {
    BmoKids.init();
}

// تحميل الأصوات
window.speechSynthesis.onvoiceschanged = () => {
    console.log('Voices loaded:', window.speechSynthesis.getVoices().length);
};