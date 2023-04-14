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
using static System.Resources.ResXFileRef;

namespace Label_Monster.Control
{
    /// <summary>
    /// Label_Area.xaml 的互動邏輯
    /// </summary>
    ///    
    public partial class Label_Area : UserControl
    {
        public PointF LastCor = new PointF();
        public PointF StartCor = new PointF();
        public List<PointF> All_Points = new List<PointF>();
        public List<PointF[]> All_Points_Outline = new List<PointF[]>();

        public List<PointF> Fill_Points = new List<PointF>();
        public List<PointF[]> Fill_Points_Outline = new List<PointF[]>();
        public PointF Fill = new PointF();
        public List<Bitmap> Label_History = new List<Bitmap>();
        public List<int> Color_History = new List<int>();
        bool Draw = false;
        bool Edit_ = false;
        public Bitmap Img_Register;
        public TransformGroup TransformGroup;
        System.Windows.Media.BrushConverter converter = new System.Windows.Media.BrushConverter();
        public string TransparencyColor { get => TransparencyColor_; set { TransparencyColor_ = value; Image_Label_Mask.OpacityMask = (System.Windows.Media.Brush)converter.ConvertFromString(TransparencyColor_); } }
        public string TransparencyColor_ = "#FF212138";

        public string BackColor { get => BackColor_; set { BackColor_ = value; Grid0.Background = (System.Windows.Media.Brush)converter.ConvertFromString(BackColor_); } }
        public string BackColor_ = "#FF454950";
        Graphics imgGraph;
        //**********************************//
        //縮放XAML IMAGE跟MASK要給出固定HW才不會跑掉//
        //
        //**********************************//
        //**********************************//
        //CANVAS影響畫圖後的準確//
        ////
        //**********************************//
        System.Windows.Point MousePosition = new System.Windows.Point();
        System.Windows.Point ContentCtlPosition = new System.Windows.Point();

        private object downSender;
        private DateTime downTime;
        TimeSpan timeSinceDown = new TimeSpan();

        public Label_Area()
        {
            InitializeComponent();
            TransformGroup = new TransformGroup();
            TransformGroup.Children.Add(new ScaleTransform());
            TransformGroup.Children.Add(new TranslateTransform());
            Label_Image.RenderTransform = TransformGroup;
            Image_Label_Mask.RenderTransform = TransformGroup;
            Canvas_Label.RenderTransform = TransformGroup;
        }
        public bool Edit
        {
            get => Edit_;
            set
            {
                Edit_ = value;
            }
        }
        public bool Draw_
        {
            get => Draw;
        }
        public PointF LastPoint
        {
            get => LastCor;
            set { LastCor = value; }
        }
        public List<PointF> FillPoint
        {

            get => Fill_Points;
            set { Fill_Points = value; }
        }
        public List<PointF[]> Fill_Area
        {

            get => Fill_Points_Outline;
            set { Fill_Points_Outline = value; }
        }

        public void History_Clear()
        {
            Label_History.Clear();
            Color_History.Clear();
            return;
        }

        public Bitmap DrawArea(System.Drawing.Brush brush , Bitmap DrawRegister)
        {
            imgGraph = Graphics.FromImage(DrawRegister);
            for (int i = 0; i < Fill_Area.Count; i++)
            {
                imgGraph.FillPolygon(brush, Fill_Area[i]);
            }
            return (Bitmap)DrawRegister.Clone();
        }
        private void On_Left_Mouse_Up(object sender, MouseButtonEventArgs e)
        {
            if (!Edit) return;
            BitmapImage ImgSource = (BitmapImage)Image_Label_Mask.Source;
            timeSinceDown = DateTime.Now - downTime;
            if (timeSinceDown.TotalMilliseconds > 150) return;

            System.Windows.Point p = e.GetPosition(Canvas_Label);
            if (Draw)
            {
                StartCor.X = LastCor.X;
                StartCor.Y = LastCor.Y;
                LastCor.X = (float)p.X;
                LastCor.Y = (float)p.Y;
                All_Points.Add(LastPoint);

                Fill = TransformCoordinate(LastCor);
                FillPoint.Add(Fill);

            }
            else
            {
                All_Points.Clear();
                All_Points_Outline.Clear();
                FillPoint.Clear();
                Fill_Points_Outline.Clear();
                StartCor.X = (float)p.X;
                StartCor.Y = (float)p.Y;
                LastCor.X = (float)p.X;
                LastCor.Y = (float)p.Y;
                All_Points.Add(LastPoint);

                Fill = TransformCoordinate(LastCor);
                FillPoint.Add(Fill);

                Canvas_Label.Children.Add(new Line
                {
                    X1 = StartCor.X,
                    Y1 = StartCor.Y,
                    X2 = LastCor.X,
                    Y2 = LastCor.Y,
                    Stroke = new SolidColorBrush(Colors.Red),
                    StrokeThickness = 1,
                });

                Draw = true;

            }
            Canvas_Label.Children.Add(new Line
            {

                X1 = StartCor.X,
                Y1 = StartCor.Y,
                X2 = LastCor.X,
                Y2 = LastCor.Y,
                Stroke = new SolidColorBrush(Colors.Orange),
                StrokeThickness = 1,
            });

        }

        private void On_Right_Mouse_Up(object sender, MouseButtonEventArgs e)
        {
            //Image_Label_Mask.OpacityMask = new SolidBrush(System.Drawing.Color.FromArgb(0, 0, 0, 0));
            if (!Edit) return;
            Draw = false;
            System.Windows.Point p = e.GetPosition(Canvas_Label);
            LastCor.X = (float)p.X;
            LastCor.Y = (float)p.Y;

            All_Points.Add(All_Points[0]);
            All_Points_Outline.Add(All_Points.ToArray());
            Canvas_Label.Children.Clear();


            FillPoint.Add(FillPoint[0]);
            Fill_Area.Add(FillPoint.ToArray());

        }

        private void On_Mouse_Move(object sender, MouseEventArgs e)
        {
            System.Windows.Point p = e.GetPosition(Canvas_Label);
            if (Draw)
            {
                LastCor.X = (float)p.X;
                LastCor.Y = (float)p.Y;
            }
            else
            {
                //if (!Edit) return;
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    UserControl ctl = sender as UserControl;
                    System.Windows.Point desPosition = e.GetPosition(ctl);
                    TranslateTransform Transform = TransformGroup.Children[1] as TranslateTransform;
                    Transform.X = ContentCtlPosition.X + (desPosition.X - MousePosition.X);
                    Transform.Y = ContentCtlPosition.Y + (desPosition.Y - MousePosition.Y);

                }
                

            }



        }
        //***********************************************************
        //Canvas上座標轉換成Image上filling座標
        //***********************************************************
        private PointF TransformCoordinate(PointF P)
        {
            BitmapImage ImgSource = (BitmapImage)Image_Label_Mask.Source;
            PointF FF = new PointF();
            FF.X = (float)((P.X)* ImgSource.PixelWidth / Image_Label_Mask.ActualWidth);//Mask用Center會移動過頭 故用調整margin方式 再去扣掉滑鼠座標及margin位移量
            FF.Y = (float)((P.Y) * ImgSource.PixelHeight / Image_Label_Mask.ActualHeight);
            return FF;
        }

        public void UpdateColor(object sender, EventArgs e)
        {

            try
            {

                var Line_Current = (Line)Canvas_Label.Children[Canvas_Label.Children.Count - 1];
                Line_Current.Stroke = new SolidColorBrush(Colors.Orange);
                Line_Current.X2 = LastPoint.X;
                Line_Current.Y2 = LastPoint.Y;

                var Line_To_Origin = (Line)Canvas_Label.Children[0];
                Line_To_Origin.Stroke = new SolidColorBrush(Colors.Red);
                Line_To_Origin.X2 = LastPoint.X;
                Line_To_Origin.Y2 = LastPoint.Y;

            }
            catch
            {
            }

        }

        private void Grid0_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            UserControl ctl = sender as UserControl;
            System.Windows.Point Point_ctl = e.GetPosition(ctl);
            double scale;
            if (e.Delta>0) scale = e.Delta * 0.005;//滾輪靈敏度
            else
            {
                scale = e.Delta * 0.005;
            }

            Zoom_Image(TransformGroup, Point_ctl, scale);
        }

        private void Zoom_Image(TransformGroup Group, System.Windows.Point Point, double scale)
        {
            System.Windows.Point PointToContent = Group.Inverse.Transform(Point);
            ScaleTransform ScaleT = Group.Children[0] as ScaleTransform;
            TranslateTransform translateT = Group.Children[1] as TranslateTransform;
            if (ScaleT.ScaleX + scale < 1)
            {
                ScaleT.ScaleX  =1;
                ScaleT.ScaleY = 1;
                translateT.X = 1;
                translateT.Y = 1;
                return;
            }
            ScaleT.ScaleX += scale;
            ScaleT.ScaleY += scale;
            
            translateT.X = -1 * ((PointToContent.X * ScaleT.ScaleX) - Point.X);
            translateT.Y = -1 * ((PointToContent.Y * ScaleT.ScaleY) - Point.Y);
        }

        private void Grid0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (Edit) return;
            downSender = sender;
            downTime = DateTime.Now;
            UserControl ctl = sender as UserControl;
            MousePosition = e.GetPosition(ctl);
            TranslateTransform tt = TransformGroup.Children[1] as TranslateTransform;
            ContentCtlPosition = new System.Windows.Point(tt.X, tt.Y);
        }

        public void Light_Theme()
        {
            BackColor = "#FFFBFBFB";

            

        }
        public void Dark_Theme()
        {
            BackColor = "#FF454950";

            

        }
    }
}
