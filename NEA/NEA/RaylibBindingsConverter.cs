using Microsoft.Toolkit.HighPerformance.Buffers;
using Raylib_CsLo;
using Raylib_CsLo.InternalHelpers;
using System.Numerics;
using System.Runtime.InteropServices;

internal static class RaylibBindingsConverter
{

    internal static Raylib_CsLo.Rectangle ConvertRectangle(Rectangle rectangle)
    {
        return new Raylib_CsLo.Rectangle(rectangle.x, rectangle.y, rectangle.width, rectangle.height);
    }
  
    

    internal unsafe static string ConvertSbyte(sbyte* sbytes, int maxLength)
    {
        if (sbytes is null)
        {
            return string.Empty;
        }

        int length = 0;

        // Find the length of the string, up to maxLength
        while (length < maxLength && sbytes[length] != 0)
        {
            length++;
        }

        // Create a byte array and copy the sbytes into it
        byte[] bytes = new byte[length];
        Marshal.Copy((IntPtr)sbytes, bytes, 0, length);

        // Convert the byte array to a string
        string str = System.Text.Encoding.UTF8.GetString(bytes);

        return str;
    }

   

}

