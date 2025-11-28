using BmoKids.Models;

namespace BmoKids.Services.LocalStorage;

/// <summary>
/// خدمة تخزين الإعدادات والتفضيلات
/// </summary>
public class SettingsStorageService
{
    private readonly IStorageService _storage;
    private const string SETTINGS_KEY = "bmokids_settings";
    private const string THEME_KEY = "bmokids_theme";
    private const string LANGUAGE_KEY = "bmokids_language";
    private const string SOUND_ENABLED_KEY = "bmokids_sound_enabled";
    private const string VOICE_SPEED_KEY = "bmokids_voice_speed";

    public SettingsStorageService(IStorageService storage)
    {
        _storage = storage;
    }

    /// <summary>
    /// حفظ إعدادات الطفل
    /// </summary>
    public async Task SaveChildSettingsAsync(ChildSettings settings)
    {
        await _storage.SetItemAsync(SETTINGS_KEY, settings);
    }

    /// <summary>
    /// تحميل إعدادات الطفل
    /// </summary>
    public async Task<ChildSettings?> LoadChildSettingsAsync()
    {
        return await _storage.GetItemAsync<ChildSettings>(SETTINGS_KEY);
    }

    /// <summary>
    /// حفظ السمة (فاتح/غامق)
    /// </summary>
    public async Task SetThemeAsync(string theme)
    {
        await _storage.SetItemAsync(THEME_KEY, theme);
    }

    /// <summary>
    /// تحميل السمة
    /// </summary>
    public async Task<string> GetThemeAsync()
    {
        var theme = await _storage.GetItemAsync<string>(THEME_KEY);
        return theme ?? "light";
    }

    /// <summary>
    /// حفظ اللغة المفضلة
    /// </summary>
    public async Task SetLanguageAsync(string language)
    {
        await _storage.SetItemAsync(LANGUAGE_KEY, language);
    }

    /// <summary>
    /// تحميل اللغة المفضلة
    /// </summary>
    public async Task<string> GetLanguageAsync()
    {
        var lang = await _storage.GetItemAsync<string>(LANGUAGE_KEY);
        return lang ?? "ar-EG";
    }

    /// <summary>
    /// تفعيل/تعطيل الصوت
    /// </summary>
    public async Task SetSoundEnabledAsync(bool enabled)
    {
        await _storage.SetItemAsync(SOUND_ENABLED_KEY, enabled);
    }

    /// <summary>
    /// التحقق من تفعيل الصوت
    /// </summary>
    public async Task<bool> IsSoundEnabledAsync()
    {
        var enabled = await _storage.GetItemAsync<bool?>(SOUND_ENABLED_KEY);
        return enabled ?? true;
    }

    /// <summary>
    /// حفظ سرعة الصوت
    /// </summary>
    public async Task SetVoiceSpeedAsync(double speed)
    {
        await _storage.SetItemAsync(VOICE_SPEED_KEY, speed);
    }

    /// <summary>
    /// تحميل سرعة الصوت
    /// </summary>
    public async Task<double> GetVoiceSpeedAsync()
    {
        var speed = await _storage.GetItemAsync<double?>(VOICE_SPEED_KEY);
        return speed ?? 0.95;
    }

    /// <summary>
    /// مسح جميع الإعدادات
    /// </summary>
    public async Task ClearAllSettingsAsync()
    {
        await _storage.RemoveItemAsync(SETTINGS_KEY);
        await _storage.RemoveItemAsync(THEME_KEY);
        await _storage.RemoveItemAsync(LANGUAGE_KEY);
        await _storage.RemoveItemAsync(SOUND_ENABLED_KEY);
        await _storage.RemoveItemAsync(VOICE_SPEED_KEY);
    }
}