using MaterialDesignThemes.Wpf;
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
    /// Tooltip.xaml 的互動邏輯
    /// </summary>
    public partial class Tooltip : UserControl
    {
        private string _Text = null;

        public Tooltip()
        {
            InitializeComponent();

        }        
        public string Text { get => _Text; set { _Text = value; Label.Content = value; } }
        public double TextSize { get => Label.FontSize; set { Label.FontSize = value; } }

        public double Outer_Height { get => Outer.Height; set { Outer.Height = value; } }
        public double Outer_Width { get => Outer.Width; set { Outer.Width = value; } }

        public double Label_Width { get => Label.Width; set { Label.Width = value; } }

        public double Label_Height { get => Label.Height; set { Label.Height = value; } }

        public Visibility IconVisibility { get => packIcon.Visibility; set { packIcon.Visibility = value; } }

        public Thickness LabelMargin { get => Label.Margin;set { Label.Margin = value; } }
        public Thickness IconMargin { get => packIcon.Margin; set { packIcon.Margin = value; } }
        
    }
}
