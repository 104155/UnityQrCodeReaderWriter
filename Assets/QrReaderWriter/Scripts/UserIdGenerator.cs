using System;

public class UserIdGenerator
{
    public string GenerateUserId() {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[8]; //string length
        var random = new System.Random();

        for (int i = 0; i < stringChars.Length; i++) {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        string finalString = new String(stringChars);
        return finalString;
    }
}
