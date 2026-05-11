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

            int cx = Width / 2;
            int cy = 230;

            canvas.DrawFilledRectangle(Color.FromArgb(0, 120, 255), cx - 90, cy - 90, 180, 14);
            canvas.DrawFilledRectangle(Color.FromArgb(0, 180, 220), cx - 90, cy + 76, 180, 14);
            canvas.DrawFilledRectangle(Color.FromArgb(0, 120, 255), cx - 90, cy - 90, 14, 180);
            canvas.DrawFilledRectangle(Color.FromArgb(0, 180, 120), cx + 76, cy - 90, 14, 180);

            canvas.DrawRectangle(Color.Cyan, cx - 105, cy - 105, 210, 210);
            canvas.DrawRectangle(Color.FromArgb(0, 180, 120), cx - 125, cy - 125, 250, 250);

            canvas.DrawFilledRectangle(Color.White, cx - 26, cy - 26, 52, 52);
            canvas.DrawRectangle(Color.FromArgb(0, 255, 180), cx - 38, cy - 38, 76, 76);

            DrawCenteredText("DualOS", 360, Color.White);
            DrawCenteredText("Cosmos Graphic Subsystem Edition", 385, Color.Cyan);
            DrawCenteredText("Press any key to continue...", 470, Color.LightGray);

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

            text = SanitizeText(text);

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

            DrawSidebar();

            canvas.DrawFilledRectangle(
                Color.FromArgb(5, 8, 14),
                SidebarWidth + 12,
                HeaderHeight + 12,
                Width - SidebarWidth - 24,
                Height - HeaderHeight - FooterHeight - 24
            );

            canvas.DrawRectangle(
                Color.FromArgb(40, 90, 125),
                SidebarWidth + 12,
                HeaderHeight + 12,
                Width - SidebarWidth - 24,
                Height - HeaderHeight - FooterHeight - 24
            );

            canvas.DrawFilledRectangle(Color.FromArgb(14, 35, 58), 0, Height - FooterHeight, Width, FooterHeight);
            canvas.DrawFilledRectangle(Color.FromArgb(0, 130, 190), 0, Height - FooterHeight, Width, 3);

            DrawText("Use 'guide' to show all commands.", 240, Height - 28, Color.LightGray);
        }

        private void DrawSidebar()
        {
            canvas.DrawFilledRectangle(
                Color.FromArgb(10, 22, 36),
                0,
                HeaderHeight,
                SidebarWidth,
                Height - HeaderHeight - FooterHeight
            );

            canvas.DrawRectangle(Color.FromArgb(0, 150, 200), 10, HeaderHeight + 10, SidebarWidth - 20, 110);

            DrawText("MAIN", 24, HeaderHeight + 22, Color.Cyan);
            DrawText("guide", 24, HeaderHeight + 42, Color.White);
            DrawText("history", 24, HeaderHeight + 58, Color.White);
            DrawText("clear", 24, HeaderHeight + 74, Color.White);
            DrawText("origin", 24, HeaderHeight + 90, Color.White);

            canvas.DrawRectangle(Color.FromArgb(0, 180, 110), 10, HeaderHeight + 130, SidebarWidth - 20, 110);

            DrawText("FILES", 24, HeaderHeight + 142, Color.FromArgb(0, 255, 170));
            DrawText("disks", 24, HeaderHeight + 162, Color.White);
            DrawText("peek", 24, HeaderHeight + 178, Color.White);
            DrawText("jump", 24, HeaderHeight + 194, Color.White);
            DrawText("forge/wipe", 24, HeaderHeight + 210, Color.White);
            DrawText("read/write", 24, HeaderHeight + 226, Color.White);

            canvas.DrawRectangle(Color.FromArgb(170, 120, 0), 10, HeaderHeight + 250, SidebarWidth - 20, 95);

            DrawText("NETWORK", 24, HeaderHeight + 262, Color.Yellow);
            DrawText("netconfig", 24, HeaderHeight + 282, Color.White);
            DrawText("ip", 24, HeaderHeight + 298, Color.White);
            DrawText("ftpstart", 24, HeaderHeight + 314, Color.White);
            DrawText("ftpstatus", 24, HeaderHeight + 330, Color.White);

            canvas.DrawRectangle(Color.FromArgb(110, 80, 170), 10, HeaderHeight + 355, SidebarWidth - 20, 65);

            DrawText("TOOLS", 24, HeaderHeight + 367, Color.FromArgb(190, 160, 255));
            DrawText("calc", 24, HeaderHeight + 387, Color.White);

            canvas.DrawRectangle(Color.FromArgb(50, 70, 90), 10, HeaderHeight + 430, SidebarWidth - 20, 70);

            DrawText("SYSTEM", 24, HeaderHeight + 442, Color.LightGray);
            DrawText("VFS enabled", 24, HeaderHeight + 462, Color.White);
            DrawText("Keyboard ES", 24, HeaderHeight + 478, Color.White);
        }

        private void DrawCenteredText(string text, int y, Color color)
        {
            if (text == null)
            {
                return;
            }

            text = SanitizeText(text);

            int charWidth = 8;
            int textWidth = text.Length * charWidth;
            int x = (Width - textWidth) / 2;

            DrawText(text, x, y, color);
        }

        private string SanitizeText(string text)
        {
            string clean = "";

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if ((c >= 32 && c <= 126) || c == '\n' || c == '\r')
                {
                    clean += c;
                }
                else
                {
                    clean += "?";
                }
            }

            return clean;
        }

        private void DrawText(string text, int x, int y, Color color)
        {
            if (text == null)
            {
                return;
            }

            text = SanitizeText(text);

            canvas.DrawString(text, font, color, x, y);
        }
    }
}