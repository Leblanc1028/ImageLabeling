using Label_Monster.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Security.RightsManagement;
using System.Net.Http.Headers;
using MaterialDesignThemes.Wpf;
using static System.Resources.ResXFileRef;
using System.Data;

namespace Label_Monster
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Media.BrushConverter converter = new System.Windows.Media.BrushConverter();
        File _File;
        int Current_Img_Idx = 0;
        Graphics imgGraph;
        bool If_Masked = true;
        bool If_White = true;
        Bitmap Mask_Register;
        string Save_Mask_Path = null;
        SolidBrush brush;
        string Initial_Path = @"D:\碩二計畫\pred_reg\last144\test_for monster";
        System.Windows.Forms.FolderBrowserDialog Loop = new FolderBrowserDialog();
        bool If_Theme_Light = false;
        class EndTextFocus
        {
            public delegate void To_End_Focus(bool x);
            public event To_End_Focus End_Focus;
            public void End()
            {
                End_Focus.Invoke(true);
            }
        }
        public string Top_SP_Color { get => Top_SP_Color_; set { Top_SP_Color_ = value; Top_SP.Background = (System.Windows.Media.Brush)converter.ConvertFromString(Top_SP_Color_); } }
        public string Top_SP_Color_ = "#FFF2F2F2";

        public string SP2_Color { get => SP2_Color_; set { SP2_Color_ = value; SP2.Background = (System.Windows.Media.Brush)converter.ConvertFromString(SP2_Color_); } }
        public string SP2_Color_ = "#FF202225";
        public string ZZ_Color { get => ZZ_Color_; set { ZZ_Color_ = value; ZZ.Background = (System.Windows.Media.Brush)converter.ConvertFromString(ZZ_Color_); } }
        public string ZZ_Color_ = "#FF393E44";
        public string Label_Transparency_Color { get => Label_Transparency_Color_; set { Label_Transparency_Color_ = value; _Transparancy.Foreground = (System.Windows.Media.Brush)converter.ConvertFromString(Label_Transparency_Color_); } }
        public string Label_Transparency_Color_ = "#FFFFFFFF";
        public string TransP_Num_Color { get => TransP_Num_Color_; set { TransP_Num_Color_ = value; _TransP_Num.Foreground = (System.Windows.Media.Brush)converter.ConvertFromString(TransP_Num_Color_); } }
        public string TransP_Num_Color_ = "#FFFFFFFF";

        public string Main_Back_Color { get => Main_Back_Color_; set { Main_Back_Color_ = value; Container.Background = (System.Windows.Media.Brush)converter.ConvertFromString(Main_Back_Color_); } }
        public string Main_Back_Color_ = "#FF36393F";
        public MainWindow()
        {
            InitializeComponent();
            double WindowH = Window_Main.Height;
            double WindowW = Window_Main.Width;
            //ST_top.Height = WindowH;
            //ST_top.Width= WindowW;
            CompositionTarget.Rendering += LabelArea.UpdateColor;

            File_Open.MouseUp += (ss, ee) =>
              {
                  if (SelectPath())
                      Set_JPG();
                  RefreshUI();
              };

            File_Open.MouseEnter += (ss, ee) =>
            {
                FileOpen_Button_Tooltip.Visibility = Visibility.Visible;
            };
            File_Open.MouseLeave += (ss, ee) =>
            {
                FileOpen_Button_Tooltip.Visibility = Visibility.Collapsed;
            };

            Change_Path_Button.MouseDown += (ss, ee) =>
              {

                  _File.SelectSavePath();
                  T3.Text = _File.OutputPath;

                  T3.TextSize = 12;
              };
            TBox_of_ToTal_Data.MouseEnter += (ss, ee) =>
            {
                TotalFile_Tooltip.Visibility = Visibility.Visible;
            };
            TBox_of_ToTal_Data.MouseLeave += (ss, ee) =>
            {
                TotalFile_Tooltip.Visibility = Visibility.Collapsed;
            };
            TextBox_of_FileName.MouseEnter += (ss, ee) =>
            {
                CurrentFileName_Tooltip.Visibility = Visibility.Visible;
            };
            TextBox_of_FileName.MouseLeave += (ss, ee) =>
            {
                CurrentFileName_Tooltip.Visibility = Visibility.Collapsed;
            };
            T3.MouseEnter += (ss, ee) =>
            {
                SavePath_Tooltip.Visibility = Visibility.Visible;
            };
            T3.MouseLeave += (ss, ee) =>
            {
                SavePath_Tooltip.Visibility = Visibility.Collapsed;
            };
            Change_Path_Button.MouseEnter += (ss, ee) =>
            {
                ChangeSavePath_Tooltip.Visibility = Visibility.Visible;
            };
            Change_Path_Button.MouseLeave += (ss, ee) =>
            {
                ChangeSavePath_Tooltip.Visibility = Visibility.Collapsed;
            };

            Scroll_0.MouseMove += (ss, ee) =>
            {
                int Data = (int)((Scroll_0.Param / 200) * 255);
                int Data_For_Show = (int)((float)Data * 100) / 255;
                if (Data_For_Show < 0 || Data_For_Show > 100) return;
                _TransP_Num.Content = Data_For_Show.ToString() + "%";
                LabelArea.Image_Label_Mask.OpacityMask = new SolidColorBrush(System.Windows.Media.Color.FromArgb((byte)Data, (byte)2F, 29, 29));
            };

            Edit_Button.MouseUp += (ss, ee) =>
              {

                  StateBall.BorderColor = "#FFF2C301";
                  Edit_Button.OriginalBackColor();
                  try
                  {
                      if (!LabelArea.Edit) Editing();
                      else
                          No_Editing();
                      _File.myDictionary.Add(_File.GetFileName(), 2);
                  }
                  catch
                  {
                      Upload_State(_File.GetFileName(), 2);
                  }
                  Refresh_State();
              };
            Edit_Button.MouseEnter += (ss, ee) =>
            {
                Edit_Button_Tooltip.Visibility = Visibility.Visible;
            };
            Edit_Button.MouseLeave += (ss, ee) =>
            {
                Edit_Button_Tooltip.Visibility = Visibility.Collapsed;
            };




            Search_Button.MouseUp += (ss, ee) =>
              {
                  _File.SetIndex = Int32.Parse(TBox_of_Current_Data.T0.Text);
                  RefreshUI();
              };
            Last_Button.MouseUp += (ss, ee) =>
              {
                  _File.LastIndex();
                  RefreshUI();
              };
            Next_Button.MouseUp += (ss, ee) =>
            {
                _File.NextIndex();
                RefreshUI();
            };
            Save_Button.MouseUp += (ss, ee) =>
              {
                  try
                  {
                      _File.SaveImage(Mask_Register);
                  }
                  catch { }
              };

            Save_Button.MouseEnter += (ss, ee) =>
            {
                Save_Button_Tooltip.Visibility = Visibility.Visible;
            };
            Save_Button.MouseLeave += (ss, ee) =>
            {
                Save_Button_Tooltip.Visibility = Visibility.Collapsed;
            };
            LabelArea.MouseRightButtonUp += (ss, ee) =>
              {
                  _File.Mask_Contains.Add(LabelArea.DrawArea(brush, Mask_Register));
                  ShowImage();
              };
            Before_Button.MouseUp += (ss, ee) =>
              {
                  Mask_Register = _File.BeforeStep();
                  ShowImage();
              };
            Before_Button.MouseEnter += (ss, ee) => { Before_Button_Tooltip.Visibility = Visibility.Visible; };
            Before_Button.MouseLeave += (ss, ee) => { Before_Button_Tooltip.Visibility = Visibility.Collapsed; };
            Reset_Button.MouseUp += (ss, ee) =>
              {
                  Mask_Register = _File.Reset_History(Current_Img_Idx);
                  ShowImage();
              };
            Reset_Button.MouseEnter += (ss, ee) => { Reset_Button_Tooltip.Visibility = Visibility.Visible; };
            Reset_Button.MouseLeave += (ss, ee) => { Reset_Button_Tooltip.Visibility = Visibility.Collapsed; };
            White_Button.MouseUp += (ss, ee) =>
            {
                Editing();
                White_Using();

            };
            White_Button.MouseEnter += (ss, ee) => { White_Button_Tooltip.Visibility = Visibility.Visible; };
            White_Button.MouseLeave += (ss, ee) => { White_Button_Tooltip.Visibility = Visibility.Collapsed; };
            Black_Button.MouseUp += (ss, ee) =>
            {
                Editing();
                Black_Using();
            };
            Black_Button.MouseEnter += (ss, ee) => { Black_Button_Tooltip.Visibility = Visibility.Visible; };
            Black_Button.MouseLeave += (ss, ee) => { Black_Button_Tooltip.Visibility = Visibility.Collapsed; };
            Window_Main.KeyDown += (ss, ee) =>
              {
                  if (ee.Key == System.Windows.Input.Key.D)
                  {
                      if (If_Masked)
                      {
                          If_Masked = false;
                          LabelArea.Image_Label_Mask.Visibility = Visibility.Hidden;
                      }
                      else
                      {
                          If_Masked = true;
                          LabelArea.Image_Label_Mask.Visibility = Visibility.Visible;
                      }
                  }
                  if (ee.Key == System.Windows.Input.Key.E)
                  {

                      StateBall.BorderColor = "#FFF2C301";
                      try
                      {
                          if (!LabelArea.Edit)
                              Editing();
                          else
                              No_Editing();
                          _File.myDictionary.Add(_File.GetFileName(), 2);
                      }
                      catch
                      {
                          Upload_State(_File.GetFileName(), 2);
                      }
                      Refresh_State();
                  }
              };
            JPG_Button.MouseUp += (ss, ee) => { Set_JPG(); };
            PNG_Button.MouseUp += (ss, ee) => { Set_PNG(); };
            Mask_Button.MouseUp += (ss, ee) =>
              {
                  if (If_Masked)
                  {
                      If_Masked = false;
                      Mask_Button.MousePressIconColor();
                      LabelArea.Image_Label_Mask.Visibility = Visibility.Hidden;
                      Mask_Button.Icon_Type = PackIconKind.EyeCheck;
                  }
                  else
                  {
                      If_Masked = true;
                      Mask_Button.OriginalIconColor();
                      LabelArea.Image_Label_Mask.Visibility = Visibility.Visible;
                      Mask_Button.Icon_Type = PackIconKind.Eye;
                  }

              };
            Mask_Button.MouseEnter += (ss, ee) => { Mask_Button_Tooltip.Visibility = Visibility.Visible; };
            Mask_Button.MouseLeave += (ss, ee) =>
            {
                if (!If_Masked) Mask_Button.MousePressIconColor();
                Mask_Button_Tooltip.Visibility = Visibility.Collapsed;

            };
            Theme_Button.MouseUp += (ss, ee) =>
            {
                if (!If_Theme_Light)
                {

                    If_Theme_Light = true;
                    Theme_Light();

                    if (LabelArea.Edit) Editing();
                    else
                        No_Editing();
                }
                else
                {
                    If_Theme_Light = false;
                    Theme_Dark();
                    if (LabelArea.Edit) Editing();
                    else
                        No_Editing();
                }

            };
            Theme_Button.MouseEnter += (ss, ee) => { Theme_Tooltip.Visibility = Visibility.Visible; };
            Theme_Button.MouseLeave += (ss, ee) => { Theme_Tooltip.Visibility = Visibility.Collapsed; };
        }

        //FileOpen

        public void Refresh_Current_Mark()
        {
            /*
            foreach (FileList_Bubble FB in FileList_Unit)
            {
                if (FB.Index == Current_Img_Idx)
                    FB.Current_Mark.Visibility = Visibility;
                else
                    FB.Current_Mark.Visibility = Visibility.Collapsed;
            }*/
        }
        public void PressSearch(FileList_Bubble[] controls)
        {/*
            foreach(FileList_Bubble FB in controls)
            {
                FB.MouseUp += (ss, ee) =>
                 {
                     Current_Img_Idx = FB.Index;
                     if (FileList_Unit[Current_Img_Idx].State_ == FileList_Bubble.Color_For_State.Black)
                     {
                         FileList_Unit[Current_Img_Idx].State_ = FileList_Bubble.Color_For_State.White;

                     }
                     T0.Text = Current_Img_Idx.ToString();
                     T2.Text = _File.FileName(Current_Img_Idx);
                     Mask_Register = _File.Reset_History(Current_Img_Idx);

                     ShowImage();
                     Edit_No_Use();
                     Refresh_State();
                 };
            }*/
        }
        private void Refresh_State()
        {
            Refresh_Current_Mark();
            int StateRegister = 7;
            try
            {
                StateRegister = _File.myDictionary[_File.GetFileName()];
            }
            catch
            {
                _File.myDictionary.Add(_File.GetFileName(), 0);

            }
            if (StateRegister == 1)
            {
                StateBall.BorderColor = "#FF50B140";
                //FileList_Unit[Current_Img_Idx].State_Color = new SolidColorBrush(Colors.Green);
            }
            else if (StateRegister == 2)
            {


                StateBall.BorderColor = "#FFF2C301";
                //FileList_Unit[Current_Img_Idx].State_Color = new SolidColorBrush(Colors.Yellow);
            }
            else if (StateRegister == -1)
            {
                StateBall.BorderColor = "#FFE83A18";
                //FileList_Unit[Current_Img_Idx].State_Color = new SolidColorBrush(Colors.Red);
            }

            else if (StateRegister == 0)
            {
                StateBall.BorderColor = "#FFFFFFFF";
                //FileList_Unit[Current_Img_Idx].State_Color = new SolidColorBrush(Colors.White);
            }

            else
            {
                StateBall.BorderColor = "#FF000000";
                //FileList_Unit[Current_Img_Idx].State_Color = new SolidColorBrush(Colors.Black);
            }

            // TBox5.Text = "State:" + staa;
        }
        public void RefreshUI()
        {
            TBox_of_Current_Data.T0.Text = _File.GetIndex.ToString();
            TBox_of_ToTal_Data.T0.Text = _File.Length.ToString();
            TextBox_of_FileName.Text = _File.GetFileName();
            T3.T0.Text = _File.OutputPath;
            T3.TextSize = 12;

            Save_Part.Text_Path.Text = Save_Mask_Path;

            Mask_Register = _File.GetMask();
            _File.Mask_Contains.Add((Bitmap)Mask_Register.Clone());
            LoadMaskFormat();
            ShowImage();
            Refresh_State();
            No_Editing();
        }
        public bool SelectPath()
        {
            Loop.SelectedPath = Initial_Path;
            if (Loop.ShowDialog() != System.Windows.Forms.DialogResult.OK) return false;
            _File = new File(Loop.SelectedPath);
            return true;
        }
        public bool SelectSavePath()
        {
            Loop.SelectedPath = _File.OutputPath;
            if (Loop.ShowDialog() != System.Windows.Forms.DialogResult.OK) return false;
            _File.OutputPath = Loop.SelectedPath;
            return true;
        }
        private void Upload_State(string filename, int state)
        {
            _File.myDictionary[filename] = state;
        }
        public void White_Using()
        {
            If_White = true;
            Edit_Button.EditBackColor();
            Black_Button.OriginalBackColor();
            White_Button.EditBackColor();
            brush = new SolidBrush(System.Drawing.Color.White);
        }
        public void Black_Using()
        {
            If_White = false;
            Edit_Button.EditBackColor();
            Black_Button.EditBackColor();
            White_Button.OriginalBackColor();
            brush = new SolidBrush(System.Drawing.Color.Black);
        }
        public void LoadMaskFormat()
        {
            switch (_File.ImgFormat)
            {
                case "jpg":
                    Set_JPG();
                    break;
                case "png":
                    Set_PNG();
                    break;
                default:
                    break;
            }
        }
        public void Set_JPG()
        {
            _File.SetImgFormat(0);
            JPG_Button.ImageFormatBackColor();
            PNG_Button.ImageFormatOriginalBackColor();
        }
        public void Set_PNG()
        {
            _File.SetImgFormat(1);
            JPG_Button.ImageFormatOriginalBackColor();
            PNG_Button.ImageFormatBackColor();
        }
        public void Editing()
        {
            LabelArea.Edit = true;

            White_Using();
        }
        public void No_Editing()
        {
            LabelArea.Edit = false;
            Edit_Button.OriginalBackColor();
            Black_Button.OriginalBackColor();
            White_Button.OriginalBackColor();
        }
        public void ShowImage()
        {
            LabelArea.Label_Image.Source = _File.Label();
            LabelArea.Image_Label_Mask.Source = _File.Mask_Image(Mask_Register);
            Mask_Image.Source = _File.Mask_Image(Mask_Register);
            return;
        }

        public void Theme_Light()
        {
            Top_SP_Color = "#FFF2F2F2";
            File_Open.Light_Theme();
            Save_Button.Light_Theme();

            JPG_Button.Light_Theme();
            PNG_Button.Light_Theme();

            LoadMaskFormat();

            Search_Button.Light_Theme();
            Change_Path_Button.Light_Theme();
            TBox_of_Current_Data.Light_Theme();
            TBox_of_ToTal_Data.Light_Theme();
            TextBox_of_FileName.Light_Theme();
            T3.Light_Theme();
            SP2_Color = "#FFD2D2D2";
            Edit_Button.Light_Theme();
            White_Button.Light_Theme();
            Black_Button.Light_Theme();
            Mask_Button.Light_Theme();
            Before_Button.Light_Theme();
            Reset_Button.Light_Theme();
            Last_Button.Light_Theme();
            Next_Button.Light_Theme();
            ZZ_Color = "#FF9C9C9C";
            Label_Transparency_Color = "#FF585858";
            TransP_Num_Color = "#FF585858";
            Theme_Button.Light_Theme();
            Scroll_0.Light_Theme();
            LabelArea.Light_Theme();
            Main_Back_Color = "#FFE1E1E1";

            Theme_Tooltip.Text = "主題:深色";
        }
        public void Theme_Dark()
        {
            Top_SP_Color = "#FF2F3136";
            File_Open.Dark_Theme();
            Save_Button.Dark_Theme();

            JPG_Button.Dark_Theme();
            PNG_Button.Dark_Theme();
            LoadMaskFormat();

            Search_Button.Dark_Theme();
            Change_Path_Button.Dark_Theme();
            TBox_of_Current_Data.Dark_Theme();
            TBox_of_ToTal_Data.Dark_Theme();
            TextBox_of_FileName.Dark_Theme();
            T3.Dark_Theme();
            SP2_Color = "#FF202225";
            Edit_Button.Dark_Theme();
            White_Button.Dark_Theme();
            Black_Button.Dark_Theme();
            Mask_Button.Dark_Theme();
            Before_Button.Dark_Theme();
            Reset_Button.Dark_Theme();
            Last_Button.Dark_Theme();
            Next_Button.Dark_Theme();
            ZZ_Color = "#FF393E44";
            Label_Transparency_Color = "#FFFFFFFF";
            TransP_Num_Color = "#FFFFFFFF";
            Theme_Button.Dark_Theme();
            Scroll_0.Dark_Theme();
            LabelArea.Dark_Theme();
            Main_Back_Color = "#FF36393F";

            Theme_Tooltip.Text = "主題:淺色";
        }
    }

}
