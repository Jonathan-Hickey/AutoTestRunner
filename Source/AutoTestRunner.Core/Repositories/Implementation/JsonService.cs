﻿using System.Text.Json;
using AutoTestRunner.Core.Repositories.Interfaces;

namespace AutoTestRunner.Core.Repositories.Implementation
{
    public class JsonService : IJsonService
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public JsonService()
        {
            _jsonSerializerOptions = new JsonSerializerOptions { IgnoreNullValues = true };
        }
        
        public string Serialize<T>(T input)
        {
            return JsonSerializer.Serialize(input, _jsonSerializerOptions);
        }

        public T Deserialize<T>(string input)
        {
            return JsonSerializer.Deserialize<T>(input, _jsonSerializerOptions);
        }
    }
}
