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

    public static byte[] GetEncryptKey(string keyPath)
    {
        using var stream = File.Open(keyPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
        using var byteStream = new StreamReader(stream);
        string stringKey = byteStream.ReadToEnd();
        byte[] key = new byte[stringKey.Length];

        if (stringKey.Length > 0)
        {
            for (int i = 0; i < key.Length; i++)
                key[i] = (byte)stringKey[i];
            return key;
        }
        else
        {
            using Aes aesAlg = Aes.Create();
            key = aesAlg.Key;
            stringKey = "";
            for (int i = 0; i < key.Length; i++)
                stringKey += (char)key[i];
            using var writer = new StreamWriter(stream);
            writer.Write(stringKey);
            return key;
        }
    }
}
