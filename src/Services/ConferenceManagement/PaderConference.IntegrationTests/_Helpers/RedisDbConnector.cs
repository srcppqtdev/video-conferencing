﻿using System;
using System.Threading.Tasks;
using PaderConference.Infrastructure.Redis.Extensions;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Core.Implementations;

namespace PaderConference.IntegrationTests._Helpers
{
    public class RedisDbConnector : IAsyncDisposable
    {
        private readonly RedisCacheConnectionPoolManager _connectionPool;
        private readonly string _instanceId = "IntegrationTest:" + Guid.NewGuid().ToString("N");

        public RedisDbConnector()
        {
            var config = new RedisConfiguration {Hosts = new[] {new RedisHost {Host = "localhost", Port = 6379}}};
            _connectionPool = new RedisCacheConnectionPoolManager(config);
        }

        public IDatabase CreateConnection()
        {
            return new RedisDatabase(_connectionPool, new CamelCaseNewtonSerializer(), new ServerEnumerationStrategy(),
                0, 200 /*, _instanceId + ":" + Guid.NewGuid().ToString("N")*/).Database;
        }

        public async ValueTask DisposeAsync()
        {
            var connection = CreateConnection();
            await connection.KeyDeleteAsync(_instanceId + "*");

            _connectionPool?.Dispose();
        }
    }
}
