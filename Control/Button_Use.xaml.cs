using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Label_Monster.Control
{
    /// <summary>
    /// Button_Use.xaml 的互動邏輯
    /// </summary>
    public partial class Button_Use : UserControl
    {
        public Button_Use()
        {
            InitializeComponent();
        }
        System.Windows.Media.BrushConverter converter = new System.Windows.Media.BrushConverter();
        public bool Press = false;
        private string _Text = null;
        public Visibility IconVisibility { get => packIcon.Visibility; set { packIcon.Visibility = value; } }
        public Visibility TextVisibility { get => TextContent.Visibility; set { TextContent.Visibility = value; } }
        public double Grid_Height { get => G0.Height; set { G0.Height = value; } }
        public double Grid_Width { get => G0.Width; set { G0.Width = value; } }
        public double Outer_Height { get => Outer.Height; set { Outer.Height = value; } }
        public double Outer_Width { get => Outer.Width; set { Outer.Width = value; } }
        public string Text { get => _Text; set { _Text = value; TextContent.Text = value; } }
        public double TextContent_Width { get => TextContent.Width; set { TextContent.Width = value; } }
        public double TextContent_Height { get => TextContent.Height; set { TextContent.Height = value; } }
        public double Text_Size { get => TextContent.FontSize; set { TextContent.FontSize = value; } }
        public MaterialDesignThemes.Wpf.PackIconKind Icon_Type { get => packIcon.Kind; set { packIcon.Kind = value; } }
        public string BorderColor { get => BorderColor_; set { BorderColor_ = value; Border_.Background = (Brush)converter.ConvertFromString(BorderColor_); } }
        public string BorderColor_ = "#FF212138";
        public string IconColor { get => IconColor_; set { IconColor_ = value; packIcon.Foreground = (Brush)converter.ConvertFromString(IconColor_); } }
        public string IconColor_ = "#FFB9BBBE";

        public string TextColor { get => TextColor_; set { TextColor_ = value; TextContent.Foreground = (Brush)converter.ConvertFromString(TextColor_); } }
        public string TextColor_ = "#FFB9BBBE";
        bool Theme_Light = false;
        
        private void On_Mouse_Move(object sender, MouseEventArgs e)
        {
            MouseMoveIconColor();
            MouseMoveTextColor();
        }

        private void On_Mouse_Leave(object sender, MouseEventArgs e)
        {
            OriginalIconColor();
            OriginalTextColor();
        }
        private void On_Mouse_Down(object sender, MouseButtonEventArgs e)
        {
            MousePressTextColor();
            MousePressIconColor();
        }
        private void On_Mouse_Up(object sender, MouseButtonEventArgs e)
        {
            MouseMoveIconColor();
            MouseMoveTextColor();
        }
        public void OriginalBackColor()
        {
            if (Theme_Light) BorderColor = "#FFF2F2F2"; 
            else
                 BorderColor = "#FF202225";
        }
        public void EditBackColor()
        {
            if (Theme_Light) BorderColor = "#FF94C7DF";
            else
                BorderColor = "#FF42464D";
        }
        public void MousePressBackColor()
        {
            BorderColor = "#FF202225";
        }
        public void MouseMoveBackColor()
        {
            BorderColor = "#FFFFFFFF";
        }

        public void OriginalTextColor()
        {
            if (Theme_Light) TextColor = "#FF383C42";
            else
                TextColor = "#FFB9BBBE";
        }
        public void MousePressTextColor()
        {
            if (Theme_Light) TextColor = "#FF748298";
            else
                TextColor = "#FFB9BBBE";
        }
        public void MouseMoveTextColor()
        {
            if (Theme_Light)
                TextColor = "#FF748298";
            else
                TextColor = "#FFFFFFFF";
        }

        public void OriginalIconColor()
        {
            if (Theme_Light) IconColor = "#FF383C42";
            else
                IconColor = "#FFB9BBBE";
        }
        public void MousePressIconColor()
        {
            if (Theme_Light)
                IconColor = "#FF748298";
            else
                IconColor = "#FFFFFFFF";
        }
        public void MouseMoveIconColor()
        {
            if (Theme_Light)
                IconColor = "#FF748298";
            else
                IconColor = "#FFFFFFFF";
        }
        
        public void ImageFormatBackColor()
        {
            if (Theme_Light)
                BorderColor = "#FF94C7DF";//小藍色
            else
                BorderColor = "#FF4A4C52";

        }
        public void ImageFormatOriginalBackColor()
        {
            if (Theme_Light)
                BorderColor = "#FFF2F2F2";
            else
                BorderColor = "#FF2F3136";

        }
        public void Light_Theme()
        {
            Theme_Light = true;

            BorderColor = "#FFF2F2F2";

            IconColor = "#FF383C42";

            TextColor = "#FF383C42";
        }
        
        public void Dark_Theme()
        {
            Theme_Light = false;

            BorderColor = "#FF2F3136";
                
            IconColor = "#FFB9BBBE";

            TextColor = "#FFB9BBBE";

        }
}
}
