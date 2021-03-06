﻿using System.Drawing;
using System.Windows.Forms;
using SlimDX;
using SlimDX.Direct2D;
using SlimDX.DirectWrite;
using Brush = SlimDX.Direct2D.Brush;
using Factory = SlimDX.DirectWrite.Factory;

namespace Kazedan.Graphics
{
    class GFXResources
    {
        public static readonly Size Bounds = new Size(1600, 900);
        public static int KeyHeight = 100;
        public static int BlackKeyHeight = 50;
        public static int NoteOffset = 21;
        public static int NoteCount = 88;

        public static readonly bool[] IsBlack = { false, true, false, true, false, false, true, false, true, false, true, false };
        public static float KeyboardY => Bounds.Height - KeyHeight;
        public static float KeyWidth => 1.0f * Bounds.Width / NoteCount;

        public static readonly Color[] ChannelColors = {
            Color.Red,          Color.HotPink,      Color.Yellow,
            Color.Green,        Color.Blue,         Color.Indigo,
            Color.SteelBlue,    Color.Pink,         Color.OrangeRed,
            Color.GreenYellow,  Color.Lime,         Color.Cyan,
            Color.Purple,       Color.DarkViolet,   Color.DarkSalmon,
            Color.Brown
        };
        public static readonly Color[] DefaultColors = {
            Color.White,        Color.Black,        Color.FromArgb(30, 30, 30),
            Color.IndianRed,    Color.Red,          Color.FromArgb(100, 10, 200, 10),
            Color.FromArgb(70, 70, 70),
        };

        public static Brush[] ChannelBrushes;
        public static Brush[] DefaultBrushes;
        public static LinearGradientBrush[] ChannelGradientBrushes;

        public static LinearGradientBrush KeyboardGradient;
        public static LinearGradientBrush BackgroundGradient;

        public static TextFormat DebugFormat;
        public static TextFormat SmallFormat;
        public static TextFormat HugeFormat;

        public static readonly RectangleF DebugRectangle = new RectangleF(10, 10, 500, 0);
        public static RectangleF FullRectangle;
        public static RoundedRectangle NoteRoundRect = new RoundedRectangle
        {
            RadiusX = 4,
            RadiusY = 4
        };
        public static RectangleF NoteRect;
        public static PointF GradientPoint;
        public static RectangleF ProgressBarBounds;

        public static void Init(RenderTarget renderTarget)
        {
            FullRectangle = new RectangleF(0, 0, Bounds.Width, Bounds.Height);
            ProgressBarBounds =
            new RectangleF(20 + DebugRectangle.X + DebugRectangle.Width, 20,
                Bounds.Width - 40 - DebugRectangle.X - DebugRectangle.Width, 20);

            // Generate common brushes
            DefaultBrushes = new Brush[DefaultColors.Length];
            for (int i = 0; i < DefaultColors.Length; i++)
                DefaultBrushes[i] = new SolidColorBrush(renderTarget, DefaultColors[i]);
            ChannelBrushes = new Brush[ChannelColors.Length];
            for (int i = 0; i < ChannelColors.Length; i++)
                ChannelBrushes[i] = new SolidColorBrush(renderTarget, ChannelColors[i]);

            // Generate common gradients
            KeyboardGradient = new LinearGradientBrush(renderTarget,
                new GradientStopCollection(renderTarget, new[] {
                    new GradientStop()
                    { Color = new Color4(Color.White),Position = 0 },
                    new GradientStop()
                    { Color = new Color4(Color.DarkGray), Position = 1 }
                }),
                new LinearGradientBrushProperties()
                {
                    StartPoint = new PointF(0, renderTarget.Size.Height),
                    EndPoint = new PointF(0, renderTarget.Size.Height - KeyHeight)
                });
            BackgroundGradient = new LinearGradientBrush(renderTarget,
                new GradientStopCollection(renderTarget, new[] {
                    new GradientStop()
                    { Color = Color.Black, Position = 1f },
                    new GradientStop()
                    { Color = Color.FromArgb(30, 30, 30), Position = 0f }
                }),
                new LinearGradientBrushProperties()
                {
                    StartPoint = new PointF(0, renderTarget.Size.Height),
                    EndPoint = new PointF(0, 0)
                });
            ChannelGradientBrushes = new LinearGradientBrush[ChannelColors.Length];
            for (int i = 0; i < ChannelGradientBrushes.Length; i++)
            {
                ChannelGradientBrushes[i] = new LinearGradientBrush(renderTarget,
                new GradientStopCollection(renderTarget, new[] {
                    new GradientStop()
                    { Color = ChannelColors[i], Position = 1f },
                    new GradientStop()
                    { Color = ControlPaint.Light(ChannelColors[i], .8f), Position = 0f }
                }),
                new LinearGradientBrushProperties()
                {
                    StartPoint = new PointF(0, renderTarget.Size.Height),
                    EndPoint = new PointF(0, 0)
                });
            }
            // Generate common fonts
            using (var textFactory = new Factory())
            {
                DebugFormat = new TextFormat(textFactory, "Consolas", FontWeight.Bold,
                    SlimDX.DirectWrite.FontStyle.Normal, FontStretch.Normal, 14, "en-us");
                SmallFormat = new TextFormat(textFactory, "Century Gothic", FontWeight.UltraBold,
                    SlimDX.DirectWrite.FontStyle.Normal, FontStretch.Normal, 10, "en-us");
                HugeFormat = new TextFormat(textFactory, "Century Gothic", FontWeight.UltraBold,
                   SlimDX.DirectWrite.FontStyle.Normal, FontStretch.Normal, 50, "en-us")
                {
                    TextAlignment = TextAlignment.Center,
                    ParagraphAlignment = ParagraphAlignment.Center
                };
            }
        }
    }
}
