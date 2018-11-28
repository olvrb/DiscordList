using System;
using System.Text;

namespace FallProject.Utilities {
    public static class Base64Utilities {
        // Credits to https://stackoverflow.com/a/11743162/8611114.

        // Extension method, just in case I need it later.
        public static string Base64Encode(this string plainText) {
            // Convert the string to a byte array.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            // Then convert it to a base64 string.
            return Convert.ToBase64String(plainTextBytes);
        }

        // Extension method, just in case I need it later.
        public static string Base64Decode(this string base64EncodedData) {
            // Convert the string to a byte array.
            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            // Then convert it to a UTF8 string.
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}