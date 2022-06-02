//Reference
//Website: C# Corner https://www.c-sharpcorner.com/article/compute-sha256-hash-in-c-sharp/
//Title: Compute SHA256 Hash in C#
//Author: Mahesh Chand
//Publish Date : 16 April 2020

using System.Security.Cryptography;
using System.Text;

namespace SmartInventoryAPI.Models
{
    public static class Encrypt
    {
        public static string HashString(string data)
        {
            
            //Create a SHA256
            using (SHA256 hash = SHA256.Create())
            {
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(data));


                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length;i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
            
        }
        
    }
}
