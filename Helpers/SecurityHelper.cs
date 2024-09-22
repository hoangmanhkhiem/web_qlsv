using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace qlsv.Helpers
{
    public class SecurityHelper
    {
        // Variables
        private readonly IConfiguration _configuration;  
        private string _encryptionKey; 
        private string _hashKey;
        private string _algorithmEncryption;
        private string _algorithmHashing;
        // Constructors
        public SecurityHelper(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("Security");
            this.GetValue();
        }

        // Init get value
        private void GetValue() {
            _encryptionKey = _configuration["EncryptionKey"];
            _hashKey = _configuration["HashKey"];
            _algorithmEncryption = _configuration["AlgorithmEncryption"];
            _algorithmHashing = _configuration["AlgorithmHashing"];
        }

        // Method to encrypt a string
        public string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(_encryptionKey);
                aesAlg.IV = new byte[16]; // For simplicity, assuming IV is zeroed, but it should be dynamic in real-world usage

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        // Method to decrypt a string
        public string Decrypt(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(_encryptionKey);
                aesAlg.IV = new byte[16]; // Should be the same IV used during encryption

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        // Method to hash a string using HMACSHA256
        public string Hash(string input)
        {
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_hashKey)))
            {
                byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToBase64String(hashValue);
            }
        }

        // Method to validate a hash
        public bool ValidateHash(string input, string hash)
        {
            string computedHash = Hash(input);
            return computedHash == hash;
        }
    }
}