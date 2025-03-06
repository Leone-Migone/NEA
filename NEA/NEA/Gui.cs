using Microsoft.Toolkit.HighPerformance.Buffers;
using Raylib_CsLo;
using Raylib_CsLo.InternalHelpers;
using System.Numerics;
using System.Text;
using static RaylibBindingsConverter;

namespace NEA
{
    public static class Gui
    {
        

       
        public static int GuiTextInputBox(Rectangle bounds, string? title, string? message, string? buttons, ref string? text, int textMaxSize = 255)
        {
            title ??= string.Empty;
            message ??= string.Empty;
            buttons ??= string.Empty;
            text ??= string.Empty;
            unsafe
            {
                SpanOwner<sbyte> spanOwner = title.MarshalUtf8();
                try
                {
                    SpanOwner<sbyte> spanOwner2 = message.MarshalUtf8();
                    try
                    {
                        SpanOwner<sbyte> spanOwner3 = buttons.MarshalUtf8();
                        try
                        {
                            SpanOwner<sbyte> spanOwner4 = text.MarshalUtf8();
                            try
                            {
                                var textBytes = spanOwner4.AsPtr();
                                int buttonClicked = Raylib_CsLo.RayGui.GuiTextInputBox(ConvertRectangle(bounds), spanOwner.AsPtr(), spanOwner2.AsPtr(), spanOwner3.AsPtr(), textBytes, textMaxSize, null);
                                text = ConvertSbyte(textBytes, textMaxSize);
                                return buttonClicked;
                            }
                            finally
                            {
                                spanOwner4.Dispose();
                            }
                        }
                        finally
                        {
                            spanOwner3.Dispose();
                        }
                    }
                    finally
                    {
                        spanOwner2.Dispose();
                    }
                }
                finally
                {
                    spanOwner.Dispose();
                }
            }
        }

    }
}