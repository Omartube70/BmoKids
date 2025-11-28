using BmoKids.Models;
using System.Text.Json;
using Microsoft.JSInterop;

namespace BmoKids.Services.LocalStorage;

public class DataStorageService
{
    private readonly IJSRuntime _jsRuntime;
    private const string STORAGE_KEY = "bmokids_v1";

    public DataStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<ChildData?> LoadDataAsync()
    {
        try
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", STORAGE_KEY);
            if (string.IsNullOrEmpty(json))
                return null;

            return JsonSerializer.Deserialize<ChildData>(json);
        }
        catch
        {
            return null;
        }
    }

    public async Task SaveDataAsync(ChildData data)
    {
        try
        {
            var json = JsonSerializer.Serialize(data);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", STORAGE_KEY, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data: {ex.Message}");
        }
    }

    public async Task<bool> ClearDataAsync()
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", STORAGE_KEY);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task AddEventAsync(ChildData data, LearningEvent learningEvent)
    {
        learningEvent.Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        data.Events.Add(learningEvent);
        data.Interactions++;
        await SaveDataAsync(data);
    }
}