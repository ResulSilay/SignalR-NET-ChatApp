using System;
using System.Drawing;

namespace Core
{
    public static class Generator
    {
        public static string SystemColor = "#FFFF4040";
        private static Color[] colors = { Color.Blue, Color.Green, Color.Yellow, Color.LightPink, Color.Orange };

        public static string ColorRand => "#" + Guid.NewGuid().ToString().Substring(0, 6);
        public static string LabelColor()
        {
            var color = colors[new Random().Next(colors.Length)];
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }
    }
}