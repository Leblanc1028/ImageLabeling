using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
using static System.Resources.ResXFileRef;

namespace Label_Monster.Control
{
    /// <summary>
    /// Input_Text.xaml 的互動邏輯
    /// </summary>
    public partial class Input_Text : UserControl
    {
        System.Windows.Media.BrushConverter converter = new System.Windows.Media.BrushConverter();
        public double T0_Width { get => T0.Width; set { T0.Width = value; } }
        
        public double T0_Height { get => T0.Height; set { T0.Height = value; } }
        public string  Text { get => T0.Text; set { T0.Text = value; } }

        public double TextSize { get => T0.FontSize; set { T0.FontSize = value; } }
        public double Outer_Height { get => Outer.Height; set { Outer.Height = value; } }
        public double Outer_Width { get => Outer.Width; set { Outer.Width = value; } }
        public string BorderColor { get => BorderColor_; set { BorderColor_ = value; Border_.Background = (Brush)converter.ConvertFromString(BorderColor_); } }
        public string BorderColor_ = "#FF202225";

        public string T0BackColor { get => T0BackColor_; set { T0BackColor_ = value; T0.Background = (Brush)converter.ConvertFromString(T0BackColor_); } }
        public string T0BackColor_ = "#FF202225";

        public string T0TextColor { get => T0TextColor_; set { T0TextColor_ = value; T0.Foreground = (Brush)converter.ConvertFromString(T0TextColor_); } }
        public string T0TextColor_ = "#FF969696";
        public bool OnlyRead { get => T0.IsReadOnly; set { T0.IsReadOnly = value; } }

        public void Test(byte Data)
        {
            Border_.Background = new SolidColorBrush(Color.FromArgb(Data, 32, 34, 37));
            T0.Background = new SolidColorBrush(Color.FromArgb(Data, 32, 34, 37));
            T0.Foreground = new SolidColorBrush(Color.FromArgb(Data, 150, 150, 150));
        }

        public bool IsReadOnly { set { T0.IsReadOnly = value; } }
        public Input_Text( )
        {
            InitializeComponent();

            T0.PreviewMouseDown += (ss, ee)=>
            {
                T0.IsReadOnly = false;
            };

        }
        public void EndFocus(bool End)
        {
            T0.IsReadOnly = true;           
        }

        private void T0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            T0.Text = "";
        }
        public void Light_Theme()
        {
            BorderColor = "#FFE8E8E8";

            T0BackColor = "#FFE8E8E8";

            T0TextColor = "#FF777777";
        }
        public void Dark_Theme()
        {
            BorderColor = "#FF202225";

            T0BackColor = "#FF202225";

            T0TextColor = "#FF969696";

        }
    }
}
