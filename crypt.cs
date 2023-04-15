using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System;
using System.IO;

public class AES256
{
    public static Encoding Enc = Encoding.UTF8;
    public static byte[] SaltBytes = { 7, 3, 6, 4, 1, 2, 9, 5, 8 };
    public static byte[] EncryptString(string aInput, string aPassword) {
        byte[] upBytes = Enc.GetBytes(aInput);
        byte[] pwBytes = SHA256.Create().ComputeHash(Enc.GetBytes(aPassword));
        byte[] bytesEncrypted = Encrypt(upBytes, pwBytes);
        return bytesEncrypted;
    }
    public static string DecryptBytes(byte[] aEncryptedBytes, string aPassword) {
        byte[] pwBytes = SHA256.Create().ComputeHash(Enc.GetBytes(aPassword));
        byte[] bytesDecrypted = Decrypt(aEncryptedBytes, pwBytes);
        string result = Enc.GetString(bytesDecrypted);
        return result;
    }
    public static byte[] Encrypt(byte[] aInputBytes, byte[] aPasswordBytes) {
        byte[] enBytes = null;
        using (var ms = new MemoryStream()) {
            using (var AES = new RijndaelManaged()) {
                AES.KeySize = 256;
                AES.BlockSize = 128;
                var key = new Rfc2898DeriveBytes(aPasswordBytes, SaltBytes, 1000);
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);
                AES.Mode = CipherMode.CBC;
                using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write)) {
                    cs.Write(aInputBytes, 0, aInputBytes.Length);
                    cs.Close();
                }
                enBytes = ms.ToArray();
            }
        }
        return enBytes;
    }
    public static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes) {
        byte[] deBytes = null;
        using (var ms = new MemoryStream()) {
            using (var AES = new RijndaelManaged()) {
                AES.KeySize = 256;
                AES.BlockSize = 128;
                var key = new Rfc2898DeriveBytes(passwordBytes, SaltBytes, 1000);
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);
                AES.Mode = CipherMode.CBC;
                using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write)) {
                    cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                    cs.Close();
                }
                deBytes = ms.ToArray();
            }
        }
        return deBytes;
    }
    
}