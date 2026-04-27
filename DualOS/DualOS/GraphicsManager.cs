using System;
using System.Collections.Generic;
using System.Drawing;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;

namespace DualOS
{
    public class GraphicsManager
    {
        private Canvas canvas;
        private Cosmos.System.Graphics.Fonts.Font font = PCScreenFont.Default;

        private List<string> outputLines = new List<string>();

        private const int Width = 800;
        private const int Height = 600;

        private const int HeaderHeight = 58;
        private const int FooterHeight = 42;
        private const int SidebarWidth = 180;

        public void Initialize()
        {
            canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(Width, Height, ColorDepth.ColorDepth32));
            canvas.Clear(Color.Black);
            canvas.Display();
        }

        public void DrawWelcomeScreen()
        {
            canvas.Clear(Color.FromArgb(10, 18, 30));

            canvas.DrawFilledRectangle(Color.FromArgb(12, 24, 42), 0, 0, Width, Height);
            canvas.DrawFilledRectangle(Color.FromArgb(20, 42, 70), 0, 0, Width, 80);
            canvas.DrawFilledRectangle(Color.FromArgb(7, 12, 22), 0, 500, Width, 100);

            int cx = 400;
            int cy = 230;

            canvas.DrawFilledRectangle(Color.FromArgb(0, 120, 255), cx - 90, cy - 90, 180, 14);
            canvas.DrawFilledRectangle(Color.FromArgb(0, 180, 220), cx - 90, cy + 76, 180, 14);
            canvas.DrawFilledRectangle(Color.FromArgb(0, 120, 255), cx - 90, cy - 90, 14, 180);
            canvas.DrawFilledRectangle(Color.FromArgb(0, 180, 120), cx + 76, cy - 90, 14, 180);

            canvas.DrawRectangle(Color.Cyan, cx - 105, cy - 105, 210, 210);
            canvas.DrawRectangle(Color.FromArgb(0, 180, 120), cx - 125, cy - 125, 250, 250);

            canvas.DrawFilledRectangle(Color.White, cx - 26, cy - 26, 52, 52);
            canvas.DrawRectangle(Color.FromArgb(0, 255, 180), cx - 38, cy - 38, 76, 76);

            DrawText("DualOS", 345, 360, Color.White);
            DrawText("Cosmos Graphic Subsystem Edition", 285, 385, Color.Cyan);
            DrawText("Press any key to continue...", 300, 470, Color.LightGray);

            canvas.Display();
        }

        public void DrawShell(string currentPath, string input)
        {
            DrawBaseLayout(currentPath);

            int y = HeaderHeight + 18;
            int maxLines = 23;

            int start = 0;
            if (outputLines.Count > maxLines)
            {
                start = outputLines.Count - maxLines;
            }

            for (int i = start; i < outputLines.Count; i++)
            {
                DrawText(outputLines[i], SidebarWidth + 24, y, Color.White);
                y += 18;
            }

            DrawText(currentPath + "> " + input, 20, Height - 28, Color.White);

            canvas.Display();
        }

        public void AddOutput(string text)
        {
            if (text == null || text.Trim() == "")
            {
                return;
            }

            string[] lines = text.Replace("\r", "").Split(new char[] { '\n' });

            for (int i = 0; i < lines.Length; i++)
            {
                outputLines.Add(lines[i]);
            }

            while (outputLines.Count > 80)
            {
                outputLines.RemoveAt(0);
            }
        }

        public void ClearOutput()
        {
            outputLines.Clear();
        }

        private void DrawBaseLayout(string currentPath)
        {
            canvas.Clear(Color.FromArgb(8, 12, 20));

            canvas.DrawFilledRectangle(Color.FromArgb(14, 35, 58), 0, 0, Width, HeaderHeight);
            canvas.DrawFilledRectangle(Color.FromArgb(0, 130, 190), 0, HeaderHeight - 4, Width, 4);

            DrawText("DualOS", 18, 18, Color.White);
            DrawText("Graphic Shell", 100, 18, Color.Cyan);
            DrawText("Path: " + currentPath, 520, 18, Color.LightGray);

            canvas.DrawFilledRectangle(Color.FromArgb(10, 22, 36), 0, HeaderHeight, SidebarWidth, Height - HeaderHeight - FooterHeight);
            canvas.DrawRectangle(Color.FromArgb(0, 150, 200), 10, HeaderHeight + 12, SidebarWidth - 20, 135);

            DrawText("COMMANDS", 24, HeaderHeight + 26, Color.Cyan);
            DrawText("guide", 24, HeaderHeight + 52, Color.White);
            DrawText("peek", 24, HeaderHeight + 70, Color.White);
            DrawText("jump", 24, HeaderHeight + 88, Color.White);
            DrawText("history", 24, HeaderHeight + 106, Color.White);
            DrawText("clear", 24, HeaderHeight + 124, Color.White);

            canvas.DrawRectangle(Color.FromArgb(0, 180, 110), 10, HeaderHeight + 165, SidebarWidth - 20, 92);
            DrawText("SYSTEM", 24, HeaderHeight + 180, Color.FromArgb(0, 255, 170));
            DrawText("VFS enabled", 24, HeaderHeight + 204, Color.White);
            DrawText("Keyboard ES", 24, HeaderHeight + 222, Color.White);

            canvas.DrawFilledRectangle(Color.FromArgb(5, 8, 14), SidebarWidth + 12, HeaderHeight + 12, Width - SidebarWidth - 24, Height - HeaderHeight - FooterHeight - 24);
            canvas.DrawRectangle(Color.FromArgb(40, 90, 125), SidebarWidth + 12, HeaderHeight + 12, Width - SidebarWidth - 24, Height - HeaderHeight - FooterHeight - 24);

            canvas.DrawFilledRectangle(Color.FromArgb(14, 35, 58), 0, Height - FooterHeight, Width, FooterHeight);
            canvas.DrawFilledRectangle(Color.FromArgb(0, 130, 190), 0, Height - FooterHeight, Width, 3);
        }

        private void DrawText(string text, int x, int y, Color color)
        {
            if (text == null)
            {
                return;
            }

            canvas.DrawString(text, font, color, x, y);
        }
    }
}
