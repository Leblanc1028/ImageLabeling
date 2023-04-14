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
using static System.Resources.ResXFileRef;

namespace Label_Monster.Control
{
    /// <summary>
    /// JustaCircle.xaml 的互動邏輯
    /// </summary>
    public partial class JustaCircle : UserControl
    {
        System.Windows.Media.BrushConverter converter = new System.Windows.Media.BrushConverter();
        public string BorderColor { get => BorderColor_; set { BorderColor_ = value; Bord.Background = (Brush)converter.ConvertFromString(BorderColor_); } }
        public string BorderColor_ = "#FFFFFFFF";
        public double Border_H{ get => Bord.Height;set { Bord.Height = value; } }
        public double Border_W { get => Bord.Width; set { Bord.Width = value; } }
        public JustaCircle()
        {
            InitializeComponent();
        }
    }
}
