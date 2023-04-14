using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Image_Show.xaml 的互動邏輯
    /// </summary>
    public partial class Image_Show : UserControl
    {
        public PointF CurrentCor = new PointF();
        public List<PointF> All_Points = new List<PointF>();
        public List<PointF[]> All_Points_Outline = new List<PointF[]>();
        bool Draw = false;
        public Image_Show()
        {
            InitializeComponent();
        }
        public List<PointF[]> GetFillPosition
        {
            get => All_Points_Outline;
        }
        public bool _Draw
        {
            get => Draw;
        }
        private void On_Left_Mouse_Up(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("123");
            if (!Draw)
            {
                Draw = true;
                All_Points.Clear();
                All_Points_Outline.Clear();
            }            
                BitmapImage ImgSource = (BitmapImage)Label_Image.Source;
                CurrentCor.X = (float)(e.GetPosition(Label_Image).Y * ImgSource.PixelHeight / Label_Image.Height);
                CurrentCor.Y = (float)(e.GetPosition(Label_Image).X * ImgSource.PixelWidth / Label_Image.Width);
                All_Points.Add(CurrentCor);
            
        }

        private void On_Right_Mouse_Up(object sender, MouseButtonEventArgs e)
        {
            All_Points.Add(All_Points[0]);
            All_Points_Outline.Add(All_Points.ToArray());
        }
    }
}
