namespace BmoKids.Services.LocalStorage;

/// <summary>
/// خدمة التخزين المؤقت (Cache)
/// </summary>
public class CacheStorageService
{
    private readonly IStorageService _storage;
    private const string CACHE_PREFIX = "bmokids_cache_";

    public CacheStorageService(IStorageService storage)
    {
        _storage = storage;
    }

    /// <summary>
    /// حفظ قيمة في الـ Cache مع مدة انتهاء
    /// </summary>
    public async Task SetCacheAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var cacheKey = CACHE_PREFIX + key;
        var cacheItem = new CacheItem<T>
        {
            Value = value,
            Expiration = expiration.HasValue
                ? DateTimeOffset.UtcNow.Add(expiration.Value)
                : null
        };

        await _storage.SetItemAsync(cacheKey, cacheItem);
    }

    /// <summary>
    /// قراءة قيمة من الـ Cache
    /// </summary>
    public async Task<T?> GetCacheAsync<T>(string key)
    {
        var cacheKey = CACHE_PREFIX + key;
        var cacheItem = await _storage.GetItemAsync<CacheItem<T>>(cacheKey);

        if (cacheItem == null)
            return default;

        // التحقق من انتهاء الصلاحية
        if (cacheItem.Expiration.HasValue && cacheItem.Expiration.Value < DateTimeOffset.UtcNow)
        {
            await RemoveCacheAsync(key);
            return default;
        }

        return cacheItem.Value;
    }

    /// <summary>
    /// حذف قيمة من الـ Cache
    /// </summary>
    public async Task RemoveCacheAsync(string key)
    {
        var cacheKey = CACHE_PREFIX + key;
        await _storage.RemoveItemAsync(cacheKey);
    }

    /// <summary>
    /// مسح كل الـ Cache المنتهي
    /// </summary>
    public async Task ClearExpiredCacheAsync()
    {
        var length = await _storage.LengthAsync();
        var keysToRemove = new List<string>();

        for (int i = 0; i < length; i++)
        {
            var key = await _storage.KeyAsync(i);
            if (key != null && key.StartsWith(CACHE_PREFIX))
            {
                try
                {
                    var cacheItem = await _storage.GetItemAsync<CacheItem<object>>(key);
                    if (cacheItem?.Expiration.HasValue == true &&
                        cacheItem.Expiration.Value < DateTimeOffset.UtcNow)
                    {
                        keysToRemove.Add(key);
                    }
                }
                catch
                {
                    // في حالة فشل قراءة العنصر، احذفه
                    keysToRemove.Add(key);
                }
            }
        }

        foreach (var key in keysToRemove)
        {
            await _storage.RemoveItemAsync(key);
        }
    }

    /// <summary>
    /// مسح كل الـ Cache
    /// </summary>
    public async Task ClearAllCacheAsync()
    {
        var length = await _storage.LengthAsync();
        var keysToRemove = new List<string>();

        for (int i = 0; i < length; i++)
        {
            var key = await _storage.KeyAsync(i);
            if (key != null && key.StartsWith(CACHE_PREFIX))
            {
                keysToRemove.Add(key);
            }
        }

        foreach (var key in keysToRemove)
        {
            await _storage.RemoveItemAsync(key);
        }
    }

    /// <summary>
    /// فئة مساعدة لتخزين العناصر مع مدة الانتهاء
    /// </summary>
    private class CacheItem<T>
    {
        public T? Value { get; set; }
        public DateTimeOffset? Expiration { get; set; }
    }
}