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
    /// FileList_Bubble.xaml 的互動邏輯
    /// </summary>
    public partial class FileList_Bubble : UserControl
    {
        public string FileName { get => FileName_Container.Text;set { FileName_Container.Text = value; } }

        public int Index{ get => Index_Bubble;set { Index_Bubble = value; } }
        public int Index_Bubble;
        public Brush Back_Color { get => B0.Background;set { B0.Background = value; } }

        public Brush State_Color { get => State_Ball.Background;set { State_Ball.Background = value; } }
        public FileList_Bubble(int index)
        {
            InitializeComponent();
            Index = index;

        }
        public enum Color_For_State
        {
            White,
            Green,
            Yellow,
            Black=7
            
        }
        public Color_For_State State_ { get=> State_now;set { State_now = value;Set_State(State_now); } }
        public Color_For_State State_now; 
        public void Set_State(Color_For_State State)
        {
            switch(State)
            {
                case Color_For_State.White:
                    State_Color = new SolidColorBrush(Colors.White);
                    break;
                case Color_For_State.Green:
                    State_Color = new SolidColorBrush(Colors.Green);//Color => ARGB   ; Colors => White,Black...
                    break;
                case Color_For_State.Yellow:
                    State_Color = new SolidColorBrush(Colors.Yellow);
                    break;              
                case Color_For_State.Black:
                    State_Color = new SolidColorBrush(Colors.Black);
                    break;
                default:
                    break;
            }
            return;
        }
        private void On_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Back_Color = new SolidColorBrush(Color.FromArgb(255, 70, 70, 70));
            if(State_now== Color_For_State.Black)
            {
                State_ = Color_For_State.White;
                
            }
        }

        private void On_MouseLeave(object sender, MouseEventArgs e)
        {
            Back_Color = new SolidColorBrush(Color.FromArgb(255, 83, 83, 83));
        }

        private void On_MouseMove(object sender, MouseEventArgs e)
        {
            Back_Color = new SolidColorBrush(Color.FromArgb(255, 70, 70, 70));
        }

        private void On_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Back_Color = new SolidColorBrush(Color.FromArgb(255, 50, 50, 50));
        }
    }
}
