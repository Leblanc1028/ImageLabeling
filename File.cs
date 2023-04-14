using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Net.WebRequestMethods;


namespace Label_Monster
{
    class File
    {
        public string[] files;
        public string[] Mask_Files;
        public string Data_Path;
        private int File_Length = 0;
        private string Mask_Path;
        private Bitmap Current_Label;
        private Bitmap Current_Mask;
        public int Current_Index = 0;
        public List<Bitmap> Mask_Contains = new List<Bitmap>();
        public Dictionary<string, int> myDictionary = new Dictionary<string, int>();
        public string OutputPath_;
        public string[] Files { get => files; }
        public int Length { get => File_Length; }
        public string ImgFormat { get => ImgFormat_; set { ImgFormat_ = value; } }
        string ImgFormat_ = "jpg";

        public Bitmap Label_Bitmap { get => Current_Label; }
        public Bitmap Mask_Bitmap { get => Current_Mask; set { Current_Mask = value; } }
        public File(string DP)
        {
            Data_Path = DP;
            files = Directory.GetFiles(Data_Path + @"\P_image", "*");
            Mask_Files = Directory.GetFiles(Data_Path + @"\P_mask", "*");
            Set_MaskPath();
            Mask_Path = MaskPath;
            File_Length = files.Length;
            read_state_todict();           
        }

        public string Path { get => Data_Path; }
        public string MaskPath { get => Mask_Path; }

        public string OutputPath { get => OutputPath_; set { OutputPath_ = value; } }
        public bool SelectSavePath()
        {
            System.Windows.Forms.FolderBrowserDialog Loop = new FolderBrowserDialog();
            Loop.SelectedPath = OutputPath;
            if (Loop.ShowDialog() != System.Windows.Forms.DialogResult.OK) return false;
            OutputPath = Loop.SelectedPath;
            //System.Windows.MessageBox.Show("變更成功:" + OutputPath);
            return true;
        }
        public void SetImgFormat(int idx)
        {
            switch (idx)
            {
                case 0:
                    ImgFormat = "jpg";
                    break;
                case 1:
                    ImgFormat = "png";
                    break;
                default:
                    break;
            }
        }
        public void Set_MaskPath()
        {
            SetImgFormat(GetSourceFormat());
            Mask_Path = Data_Path + @"\P_mask\" + GetFileName() + "_mask" +"." + ImgFormat;
            OutputPath = Data_Path + @"\out\";
        }

        public void Set_MaskPath(int index, string PNG)
        {

            Mask_Path = PNG;
        }
        
        public int GetIndex { get => Current_Index; }
        public int SetIndex 
        {
            set 
            {
                try
                {
                    if (Current_Index >= 0 && Current_Index < Length - 1)
                    {
                        Current_Index = value;
                    }
                    else
                        System.Windows.MessageBox.Show("請輸入正確號碼");
                }
                catch
                {
                    System.Windows.MessageBox.Show("請輸入正確號碼");
                }
                
                
            } 
        }
        //public int SetIndex {  }
        public void LastIndex()
        {
            if (Current_Index == 0) Current_Index = Length - 1;
            else
                Current_Index--;
            Set_MaskPath();
        }
        public void NextIndex()
        {
            if (Current_Index == Length-1) Current_Index = 0;
            else
                Current_Index++;
            Set_MaskPath();
        }
        public Bitmap BeforeStep()
        {
            if (Mask_Contains.Count == 1) return (Bitmap)Mask_Contains[0].Clone();

            Mask_Contains.RemoveAt(Mask_Contains.Count - 1);
            return (Bitmap)Mask_Contains[Mask_Contains.Count - 1].Clone();//重點 針對記憶體位置需要用clone 
        }
        public Bitmap GetMask()
        {
            try
            {
                Set_MaskPath();
                Current_Mask = new Bitmap(Mask_Path);
            }
            catch
            {              
            }

            int ImgFormat = Image.GetPixelFormatSize(Current_Mask.PixelFormat);
            if (ImgFormat == 8) Current_Mask = TransformMask_To_24(Current_Mask);
            return Current_Mask;
        }
        public Bitmap TransformMask_To_24(Bitmap Image_8bpp)
        {
            Bitmap Current_Mask_24 = new Bitmap(Image_8bpp.Width, Image_8bpp.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            for (int H = 0; H < Image_8bpp.Height; H++)
            {
                for (int W = 0; W < Image_8bpp.Width; W++)
                {

                    Current_Mask_24.SetPixel(W, H, Image_8bpp.GetPixel(W, H));
                }
            }
            return Current_Mask_24;
        }
        public void SaveImage(Bitmap FinishMask)
        {
            FinishMask.Save(OutputPath + GetFileName() + "_mask"+"."+ ImgFormat);
            //System.Windows.MessageBox.Show("輸出成功:" + OutputPath + GetFileName() + "_mask" + "." + ImgFormat);
            using (StreamWriter file = new StreamWriter(Path + @"\dict.txt"))
            {
                foreach (var entry in myDictionary)
                    file.WriteLine("{0} {1}", entry.Key, entry.Value);
            }
        }
        public BitmapImage Mask_Image(Bitmap ImgRegister)
        {
            return GetBitmapImageBybitmap(ImgRegister);
        }       
        public BitmapImage Label()
        {
            BitmapImage Image = GetBitmapImageBybitmap(new Bitmap(Files[Current_Index]));

            return Image;
        }
        public BitmapImage Label(Bitmap ImgRegister)
        {
            BitmapImage Image = GetBitmapImageBybitmap(new Bitmap(Files[Current_Index]));

            return Image;
        }
        public int GetSourceFormat()
        {
            string[] Subs = Mask_Files[Current_Index].Split('.');
            int Total = Subs.Length;
            string Format = Subs[Total - 1];
            if (Format.Equals("jpg")) return 0;
            if (Format.Equals("png")) return 1;
            else
                return -1;
        }
        public string GetFileName()
        {
            
            return System.IO.Path.GetFileNameWithoutExtension(files[Current_Index]);
        }
        private void read_state_todict()
        {
            try
            {
                StreamReader sr = new StreamReader(Data_Path + @"\dict.txt");
                String line;
                while ((line = sr.ReadLine()) != null)
                {

                    string[] words = line.Split(' ');

                    myDictionary.Add(words[0], Int32.Parse(words[1]));
                }
                sr.Close();
            }
            catch
            {

            }
        }

        private static BitmapImage GetBitmapImageBybitmap(Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = ms;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze();
                }
            }
            catch
            {
            }
            return bitmapImage;
        }
        public void Add_History(Bitmap Img)
        {
            Mask_Contains.Add(new Bitmap(Img));

        }
        public Bitmap Reset_History(int index)
        {
            Bitmap Img = GetMask();
            Mask_Contains.RemoveRange(1, Mask_Contains.Count - 1);
            Mask_Contains[0] = (Bitmap)Img.Clone();
            return (Bitmap)Mask_Contains[0].Clone();
        }
    }
}
