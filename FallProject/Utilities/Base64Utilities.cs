namespace FallProject.Utilities {
    public static class Base64Utilities {
        // Credits to https://stackoverflow.com/a/11743162/8611114.
        public static string Encode(string plainText) {
           // Convert the string to a byte array.
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            // Then convert it to a base64 string.
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Decode(string base64EncodedData) {
            // Convert the string to a byte array.
            byte[] base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            // Then convert it to a UTF8 string.
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}