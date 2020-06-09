using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTestRunner.Api.Services.Interfaces;

namespace AutoTestRunner.Api.Services.Implementation
{
    public class HashService : IHashService
    {
        public string GetHash(string input)
        {
            //https://stackoverflow.com/a/39131803
            //Source code
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}
