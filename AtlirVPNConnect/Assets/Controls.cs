using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

internal sealed class Header : Panel
{
    public Header()
    {
        DoubleBuffered = true;
        Dock = DockStyle.Top;
        Font = new Font(Drawer.FontLoader.FontCollection.Families[0], 12);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Height = 40;
        Graphics g = e.Graphics;
        BackColor = Drawer.HtmlColor("f8f8f8");
        g.Clear(Drawer.HtmlColor("f8f8f8"));
        g.TextRenderingHint = TextRenderingHint.AntiAlias;
        g.DrawString(Text, Font, Drawer.Brush("333333"), 12, 10);
        g.DrawLine(Drawer.Pen("e7e7e7", 4), new Point(0, Height - 1), new Point(Width, Height - 1));
    }
}

internal sealed class StatusBar : Control
{
    public StatusBar()
    {
        DoubleBuffered = true;
        Dock = DockStyle.Bottom;
        Font = new Font(Drawer.FontLoader.FontCollection.Families[0], 9);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Height = 24;
        Graphics g = e.Graphics;
        g.TextRenderingHint = TextRenderingHint.AntiAlias;
        g.Clear(Drawer.HtmlColor("158cba"));
        g.DrawString(Text, Font, Drawer.Brush("fff"), 4, 4);
        g.DrawString(ActiveTasks, Font, Drawer.Brush("fff"), ClientRectangle.Width - ((int)g.MeasureString(ActiveTasks, Font).Width + 4), 4);
    }

    private string TextStore;

    public override string Text
    {
        get { return TextStore; }
        set
        {
            Invalidate();
            TextStore = value;
        }
    }

    private string ActiveTasks;
    public string Tasks
    {
        get { return ActiveTasks; }
        set
        {
            ActiveTasks = value;
            Invalidate();
        }
    }
}

internal sealed class Tabs : TabControl
{
    public Tabs()
    {
        SetStyle(
            ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
            ControlStyles.DoubleBuffer, true);
        DoubleBuffered = true;
        Alignment = TabAlignment.Top;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        if(Disposing || IsDisposed) return;
        Graphics g = e.Graphics;
        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        var usingFont = new Font(FontFamily.GenericSansSerif, 8F, FontStyle.Regular);
        g.Clear(Parent.BackColor);
        g.DrawRectangle(Drawer.Pen("e7e7e7", 1),
            new Rectangle(3, 24, ClientRectangle.Width - 7, ClientRectangle.Height - 28));
        if (TabCount < 0)
            return;
        for (int i = 0; i <= TabCount - 1; i++)
        {
            try
            {
                TabPages[i].BackColor = Drawer.HtmlColor("FFF");
            }
            catch (Exception)
            {
            }
            var baserec = new Rectangle(GetTabRect(i).X + 1, 1, GetTabRect(i).Width - 2, GetTabRect(i).Height + 2);
            if (i == SelectedIndex)
            {
                g.DrawRectangle(Drawer.Pen("e7e7e7", 1), baserec);
                g.FillRectangle(Drawer.Brush("fff"),
                    new Rectangle(baserec.X + 1, baserec.Y + 1, baserec.Width - 1, baserec.Height + 10));

                int center =
                    Convert.ToInt32((baserec.Width - (int) g.MeasureString(TabPages[i].Text, usingFont).Width)/2);
                g.DrawString(TabPages[i].Text, usingFont, Drawer.Brush("303030"),
                    new Point(baserec.X + center,
                        baserec.Y + 1 + (baserec.Height - (int) g.MeasureString(TabPages[i].Text, usingFont).Height)/2));
            }
            else
            {
                g.DrawRectangle(Drawer.Pen("e7e7e7", 1),
                    new Rectangle(baserec.X, baserec.Y + 2, baserec.Width, baserec.Height - 2));
                g.DrawString(TabPages[i].Text, usingFont, new SolidBrush(ColorTranslator.FromHtml("#A3A3A3")),
                    new Point(baserec.X + (baserec.Width - (int) g.MeasureString(TabPages[i].Text, usingFont).Width)/2,
                        baserec.Y + 1 + (baserec.Height - (int) g.MeasureString(TabPages[i].Text, usingFont).Height)/2));
            }
        }
    }

    protected override void OnGotFocus(EventArgs e)
    {
        Invalidate();
    }

    protected override void OnLostFocus(EventArgs e)
    {
        Invalidate();
    }
}

internal sealed class Button : Control
{
    private bool Clicked;
    private bool Ena = true;
    private bool Hover;

    public Button()
    {
        Width = 80;
        Height = 25;
        DoubleBuffered = true;
    }

    [Category("Behavior")]
    public new bool Enabled
    {
        get { return Ena; }
        set
        {
            Ena = value;
            Invalidate();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Font usingFont = new Font(Drawer.FontLoader.FontCollection.Families[0], 10, FontStyle.Regular);
        Graphics g = e.Graphics;
        g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        g.Clear(Drawer.HtmlColor("158cba"));
        g.DrawLine(Drawer.Pen("127BA3", 3), 0, Height - 2, Width, Height - 2);
        if (!Enabled)
            g.FillRectangle(Drawer.Brush("525252"), ClientRectangle);
        else if (Clicked)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(80, 0, 0, 0)), ClientRectangle);
            Clicked = false;
        }
        else if (Hover)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(30, 0, 0, 0)), ClientRectangle);
        }
        g.DrawString(Text, usingFont, Drawer.Brush("fff"),
            new Point((((ClientRectangle.Width - (int)g.MeasureString(Text, usingFont).Width)) / 2) + 1, ((ClientRectangle.Height - (int)g.MeasureString(Text, usingFont).Height)) / 2));
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        Hover = true;
        Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        Hover = false;
        Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        Clicked = true;
        Invalidate();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        Invalidate();
    }
}

internal sealed class TextArea : Control
{
    private bool focus;
    public TextBox Inner = new TextBox();

    public TextArea()
    {
        Height = 25;
        Width = 140;
        Inner.Location = new Point(4, 4);
        Inner.Size = new Size(60, 22);
        Inner.BorderStyle = BorderStyle.None;
        Inner.Font = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular);
        Controls.Add(Inner);
        Inner.GotFocus += inner_GotFocus;
        Inner.LostFocus += inner_LostFocus;
        DoubleBuffered = true;
        Inner.TextChanged += (sender, args) => OnTextChanged(new EventArgs());
    }

    public override string Text
    {
        get { return Inner.Text; }
        set { Inner.Text = value;
            OnTextChanged(new EventArgs());
        }
    }

    public bool Multiline
    {
        get { return Inner.Multiline; }
        set
        {
            Inner.Multiline = value;
            Invalidate();
        }
    }

    public bool ReadOnly
    {
        get { return Inner.ReadOnly; }
        set
        {
            Inner.ReadOnly = value;
            Invalidate();
        }
    }

    public bool Password
    {
        get { return Inner.UseSystemPasswordChar; }
        set
        {
            Inner.UseSystemPasswordChar = value;
            Invalidate();
        }
    }

    public new bool Enabled
    {
        get { return Inner.Enabled; }
        set
        {
            Inner.Enabled = value;
            Invalidate();
        }
    }

    private void inner_LostFocus(object sender, EventArgs e)
    {
        focus = false;
        Invalidate();
    }

    private void inner_GotFocus(object sender, EventArgs e)
    {
        focus = true;
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        Inner.Size = new Size(Width - 8, Height - 8);
        Inner.ForeColor = ColorTranslator.FromHtml("#000000");
        if (!Enabled)
        {
            g.Clear(ColorTranslator.FromHtml("#E6E6E6"));
            Inner.BackColor = ColorTranslator.FromHtml("#E6E6E6");
            g.DrawRectangle(new Pen(ColorTranslator.FromHtml("#E6E6E6")),
                new Rectangle(0, 0, ClientRectangle.Width - 1, ClientRectangle.Height - 1));
        }
        else if (focus)
        {
            g.Clear(ColorTranslator.FromHtml("#FFFFFF"));
            Inner.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            g.DrawRectangle(new Pen(ColorTranslator.FromHtml("#E0E0E0")),
                new Rectangle(0, 0, ClientRectangle.Width - 1, ClientRectangle.Height - 1));
        }
        else
        {
            g.Clear(ColorTranslator.FromHtml("#FFFFFF"));
            Inner.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            g.DrawRectangle(new Pen(ColorTranslator.FromHtml("#E6E6E6")),
                new Rectangle(0, 0, ClientRectangle.Width - 1, ClientRectangle.Height - 1));
        }
        if (!Multiline)
        {
            Height = Inner.Height + 9;
        }
        else
        {
            Inner.ScrollBars = ScrollBars.Vertical;
        }
        base.OnPaint(e);
    }

    protected override void OnGotFocus(EventArgs e)
    {
        Inner.Focus();
        base.OnGotFocus(e);
    }
}

internal sealed class Labeler : Control
{
    public Labeler()
    {
        Font = new Font(Drawer.FontLoader.FontCollection.Families[0], 9, FontStyle.Regular);
        DoubleBuffered = true;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        Width = (int)g.MeasureString(Text, Font).Width + 6;
        Height = (int)g.MeasureString(Text, Font).Height + 6;
        g.TextRenderingHint = TextRenderingHint.AntiAlias;
        g.DrawString(Text, Font, Drawer.Brush("333333"),
            new Point((((ClientRectangle.Width - (int)g.MeasureString(Text, Font).Width)) / 2) + 1,
                ((ClientRectangle.Height - (int)g.MeasureString(Text, Font).Height)) / 2));
    }
}

public class Drawer
{
    public static SolidBrush Brush(string color)
    {
        return new SolidBrush(HtmlColor(color));
    }

    public static SolidBrush Brush(string color, int alpha)
    {
        return new SolidBrush(HtmlColor(color, alpha));
    }

    public static Pen Pen(string color, int size)
    {
        return new Pen(HtmlColor(color), size);
    }

    public static Pen Pen(string color, int alpha, int size)
    {
        return new Pen(HtmlColor(color, alpha), size);
    }

    public static Color HtmlColor(string color)
    {
        return ColorTranslator.FromHtml("#" + color.Replace("#", ""));
    }

    public static Color HtmlColor(string color, int alpha)
    {
        return Color.FromArgb(alpha, ColorTranslator.FromHtml("#" + color.Replace("#", "")));
    }

    public static class FontLoader
    {
        private static PrivateFontCollection FontCollectionPvt;
        public static PrivateFontCollection FontCollection
        {
            get
            {
                if (FontCollectionPvt == null)
                {
                    FontCollectionPvt = new PrivateFontCollection();
                    LoadFonts();
                }
                return FontCollectionPvt;
            }
        }
        private static void LoadFonts()
        {
            string[] resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach (var name in resourceNames)
            {
                if (name.Contains(".ttf"))
                {
                    using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
                    {
                        byte[] resourceBytes = new byte[resourceStream.Length];
                        resourceStream.Read(resourceBytes, 0, (int)resourceStream.Length);
                        resourceStream.Close();
                        //Credit sky-dev@stackoverflow
                        IntPtr fontData = Marshal.AllocCoTaskMem(resourceBytes.Length);
                        Marshal.Copy(resourceBytes, 0, fontData, resourceBytes.Length);
                        FontCollectionPvt.AddMemoryFont(fontData, resourceBytes.Length);
                        Marshal.FreeCoTaskMem(fontData);
                    }
                }
            }
        }
    }
}