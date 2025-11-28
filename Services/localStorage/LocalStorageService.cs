using System.Text.Json;
using Microsoft.JSInterop;

namespace BmoKids.Services.LocalStorage;

/// <summary>
/// خدمة التخزين المحلي العامة
/// </summary>
public class LocalStorageService : IStorageService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly JsonSerializerOptions _jsonOptions;

    public LocalStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        };
    }

    public async Task SetItemAsync<T>(string key, T value)
    {
        try
        {
            var json = JsonSerializer.Serialize(value, _jsonOptions);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting item in localStorage: {ex.Message}");
            throw;
        }
    }

    public async Task<T?> GetItemAsync<T>(string key)
    {
        try
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

            if (string.IsNullOrEmpty(json))
                return default;

            return JsonSerializer.Deserialize<T>(json, _jsonOptions);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting item from localStorage: {ex.Message}");
            return default;
        }
    }

    public async Task RemoveItemAsync(string key)
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error removing item from localStorage: {ex.Message}");
            throw;
        }
    }

    public async Task ClearAsync()
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.clear");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error clearing localStorage: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> ContainKeyAsync(string key)
    {
        try
        {
            var value = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
            return !string.IsNullOrEmpty(value);
        }
        catch
        {
            return false;
        }
    }

    public async Task<int> LengthAsync()
    {
        try
        {
            return await _jsRuntime.InvokeAsync<int>("eval", "localStorage.length");
        }
        catch
        {
            return 0;
        }
    }

    public async Task<string?> KeyAsync(int index)
    {
        try
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.key", index);
        }
        catch
        {
            return null;
        }
    }
}