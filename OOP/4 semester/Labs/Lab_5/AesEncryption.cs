using System.Security.Cryptography;

namespace Lab_5;

public class AesEncryption
{
    public static (string encrypt, byte[] iv) Encrypt(string text, byte[] key)
    {
        using Aes aesAlg = Aes.Create();
        aesAlg.Key = key;
        byte[] iv = aesAlg.IV;

        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using MemoryStream msEncrypt = new MemoryStream();
        using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        using StreamWriter swEncrypt = new StreamWriter(csEncrypt);
        swEncrypt.Write(text);
        csEncrypt.FlushFinalBlock();
        var array = msEncrypt.ToArray();
        return (Convert.ToBase64String(array), iv);
    }

    public static string Decrypt(string encryptText, byte[] key, byte[] iv)
    {
        using Aes aesAlg = Aes.Create();
        aesAlg.Key = key;
        aesAlg.IV = iv;

        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptText));
        using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using StreamReader srDecrypt = new StreamReader(csDecrypt);
        return srDecrypt.ReadToEnd();
    }
}
