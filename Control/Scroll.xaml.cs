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
    /// Scroll.xaml 的互動邏輯
    /// </summary>
    public partial class Scroll : UserControl
    {
        bool Press = false;
        public double Initial_X;
        public double Param { get => Param_;set { Param_ = value; } }
        public double Param_=80;
        System.Windows.Media.BrushConverter converter = new System.Windows.Media.BrushConverter();
        public string BackColor { get => BackColor_; set { BackColor_ = value; Grid0.Background = (Brush)converter.ConvertFromString(BackColor_); } }
        public string BackColor_ = "#FF202225";

        public string RowColor { get => RowColor_  ; set { RowColor_ = value; Row.Background = (Brush)converter.ConvertFromString(RowColor_); } }
        public string RowColor_ = "#FFFFFFFF";
        public Scroll()
        {
            InitializeComponent();
            Bar.HorizontalAlignment = HorizontalAlignment.Left;
            Grid0.MouseDown += (ss, ee) =>{ Press = true; Initial_X = ee.GetPosition(Bar).X; };
            Grid0.MouseMove += (s, e) =>
            {
                if (Press)
                {
                    if (Bar.Margin.Left > 0 || Bar.Margin.Left <200)
                    {
                        int Left = (int)(Bar.Margin.Left + e.GetPosition(Bar).X - Initial_X);
                        Bar.Margin = new Thickness(Left, 0, 0, 0);
                        Row.Content =(10 + Bar.Margin.Left).ToString();//10為Grid到竿子最左邊差
                        Param = Bar.Margin.Left-10;
                    }
                                                 
                }
            };
            Grid0.MouseUp += (s, e) =>
            {
                               
                Press = false;
                
            };
            //Row.MouseLeave += (s, e) => { Press = false; };
            //Grid0.MouseLeave += (s, e) => { Press = false; };
        }
        public void Light_Theme()
        {
            BackColor = "#FFD2D2D2";

            RowColor = "#FF464646";
           
        }
        public void Dark_Theme()
        {
            BackColor = "#FF202225";

            RowColor = "#FFFFFFFF";

        }
    }
}
     
     