//Source: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-8.0
//Edited by: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Security.Cryptography;

public static class EncryptionManager
{
    /// <summary>
    /// using the AES class, this encrypts the original string using the key and IV this has been called with.
    /// </summary>
    /// <param name="original">The original string</param>
    /// <param name="key"> The encryption key </param>
    /// <param name="initVector"> The initialization vector (IV) </param>
    /// <returns>encrypted message in an array of bytes</returns>
    public static byte[] EncryptToBytes(string original, byte[] key, byte[] initVector)
    {
        //the encrypted message
        byte[] encrypted;

        // create a new AES instance, which creates its own key and IV
        using (Aes aesAlg = Aes.Create())
        {
            //Overwrite the key and IV with our predetermined ones
            aesAlg.Key = key;
            aesAlg.IV = initVector;

            // Create an instance of an encryptor, using AES with our key and IV
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            //Crate the streams necessary for encryption
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (
                    CryptoStream csEncrypt = new CryptoStream(
                        msEncrypt,
                        encryptor,
                        CryptoStreamMode.Write
                    )
                )
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Enter the original data to the stream
                        swEncrypt.Write(original);
                    }
                    //Return the encrypted messages as bytes
                    encrypted = msEncrypt.ToArray();
                }
            }

            return encrypted;
        }
    }

    /// <summary>
    /// Decrypts an array of bytes and returns a readable string, using the AES class and a predetermined key and IV
    /// </summary>
    /// <param name="cipher">the encrypted message in bytes</param>
    /// <param name="key">the encryption key</param>
    /// <param name="initVector">The initialization vector (IV)</param>
    /// <returns>the decrypted, readable string</returns>
    public static string DecryptFromBytes(byte[] cipher, byte[] key, byte[] initVector)
    {
        //the decrypted message
        string plaintext = null;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = initVector;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipher))
            {
                using (
                    CryptoStream csDecrypt = new CryptoStream(
                        msDecrypt,
                        decryptor,
                        CryptoStreamMode.Read
                    )
                )
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }
}
