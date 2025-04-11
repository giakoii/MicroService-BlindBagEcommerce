using System.Security.Cryptography;
using System.Text;
using AuthService.Models.Helpers;
using Microsoft.EntityFrameworkCore;
using OpenIdConnect.Utils.Consts;

namespace AuthService.Logics.Commons;

/// <summary>
/// Common logic
/// </summary>
public static class CommonLogic
{
    /// <summary>
    /// Encrypt the text
    /// </summary>
    /// <param name="beforeEncrypt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string EncryptText(string beforeEncrypt, AppDbContext context)
    {
        // Check for null or empty
        ArgumentException.ThrowIfNullOrEmpty(beforeEncrypt);
        
        // Get the system config
        var key = context.SystemConfigs.AsNoTracking().FirstOrDefault(x => x.Id == SystemConfig.EncryptKey)!.Value;
        var iv = context.SystemConfigs.AsNoTracking().FirstOrDefault(x => x.Id == SystemConfig.EncryptIv)!.Value;
        // Check for null
        if (key == null)
        {
            throw new ArgumentException();
        }
        // Encrypt the text
        using (Aes aes = Aes.Create())
        {
            // Set the key and IV
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = Encoding.UTF8.GetBytes(iv);
            
            // Encrypt
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(beforeEncrypt);
                    }
                }
                return Convert.ToBase64String(ms.ToArray());
            }   
        }
    }
    
    /// <summary>
    /// Decrypt the text
    /// </summary>
    /// <param name="beforeDecrypt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string DecryptText(string beforeDecrypt, AppDbContext context)
    {
        // Check for null or empty
        ArgumentException.ThrowIfNullOrEmpty(beforeDecrypt);
        // Get the system config
        var key = context.SystemConfigs.AsNoTracking().FirstOrDefault(x => x.Id == SystemConfig.EncryptKey)!.Value;
        var iv = context.SystemConfigs.AsNoTracking().FirstOrDefault(x => x.Id == SystemConfig.EncryptIv)!.Value;
        // Check for null
        if (key == null)
        {
            throw new ArgumentException();
        }
        // Decrypt the text
        using (Aes aes = Aes.Create())
        {
            // Set the key and IV
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = Encoding.UTF8.GetBytes(iv);
            // Decrypt
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(beforeDecrypt)))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
}