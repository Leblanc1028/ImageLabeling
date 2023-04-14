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
    /// FileList.xaml 的互動邏輯
    /// </summary>
    public partial class FileList : UserControl
    {
        public FileList()
        {
            InitializeComponent();
            DockPanel P0 = new DockPanel();
            G0.Children.Add(P0);
            //Stack_Panel.Children.Add(BB0);
            //Stack_Panel.Children.Add(BB1);
        }
    }
}
