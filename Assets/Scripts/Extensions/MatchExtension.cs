using System;
using System.Security.Cryptography;
using System.Text;

public static class MatchExtension
{
    public static Guid ToGuid(this string id)
    {
        MD5 md5 = MD5.Create();

        byte[] inputBytes = Encoding.Default.GetBytes(id);
        byte[] hasBytes = md5.ComputeHash(inputBytes);

        return new Guid(hasBytes);
    }
}
