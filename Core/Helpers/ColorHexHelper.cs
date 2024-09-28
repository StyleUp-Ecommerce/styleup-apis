using Core.Constants;
using System;

namespace Core.Helpers
{
    public class ColorHexHelper
    {
        public static string GetColorHex(ColorEnum color)
        {
            return color switch
            {
                ColorEnum.Red => "#FF0000",   
                ColorEnum.Blue => "#0000FF",   
                ColorEnum.Green => "#00FF00",  
                ColorEnum.Orange => "#FFA500", 
                _ => throw new ArgumentOutOfRangeException(nameof(color), $"Color not support: {color}")
            };
        }
    }
}