using System.Text.Json;
using Application.Helpers;
using Application.Interfaces;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Application.Services;

public class CachingService : ICachingService
{
    private readonly IDatabase _redis;
    private readonly RedisConfig _redisConfig;
    private readonly ConnectionMultiplexer _connectionMultiplexer;

    public CachingService(ConnectionMultiplexer connectionMultiplexer, IOptions<RedisConfig> redisConfig)
    {
        _connectionMultiplexer = connectionMultiplexer;
        _redisConfig = redisConfig.Value;
        _redis = _connectionMultiplexer.GetDatabase();
    }

    public T GetData<T>(string key)
    {
        var value = _redis.StringGet(key);
        if(!string.IsNullOrEmpty(value)) return JsonSerializer.Deserialize<T>(value);
        return default;
    }

    public bool SetData<T>(string key, T value)
    {
        var timeToExpire = TimeSpan.FromDays(_redisConfig.ExpirationTimeInDays);
        var recordIsSet = _redis.StringSet(key, JsonSerializer.Serialize(value), timeToExpire);

        return recordIsSet;
    }
    public object RemoveData(string key)
    {
        var keyExisted = _redis.KeyExists(key);
        if(keyExisted) return _redis.KeyDelete(key);
        return false;
    }

}
