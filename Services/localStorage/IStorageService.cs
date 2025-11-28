namespace BmoKids.Services.LocalStorage;

/// <summary>
/// واجهة عامة لخدمات التخزين
/// </summary>
public interface IStorageService
{
    /// <summary>
    /// حفظ قيمة في التخزين
    /// </summary>
    Task SetItemAsync<T>(string key, T value);

    /// <summary>
    /// قراءة قيمة من التخزين
    /// </summary>
    Task<T?> GetItemAsync<T>(string key);

    /// <summary>
    /// حذف قيمة من التخزين
    /// </summary>
    Task RemoveItemAsync(string key);

    /// <summary>
    /// مسح كل البيانات
    /// </summary>
    Task ClearAsync();

    /// <summary>
    /// التحقق من وجود مفتاح
    /// </summary>
    Task<bool> ContainKeyAsync(string key);

    /// <summary>
    /// الحصول على عدد العناصر
    /// </summary>
    Task<int> LengthAsync();

    /// <summary>
    /// الحصول على مفتاح بناءً على الفهرس
    /// </summary>
    Task<string?> KeyAsync(int index);
}