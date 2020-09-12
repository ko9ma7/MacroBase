/*
 * キャプチャウィンドウ
 */
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
using System.Windows.Shapes;

namespace MacroBase.WINDOW
{
    public partial class CaptureWin : Window
    {
        public int mode;                            //動作モード 1=範囲キャプチャ   2=絶対位置クリック 3=相対位置クリック
                                                    //4=位置確認
        public System.Drawing.Rectangle rect;       //選択された範囲
        private double posx, posy;                  //最初に選択した座標
        public System.Drawing.Point pnt;            //マッチ位置(mode=3)
        public double score = 0;                    //マッチスコア(mode=3)
        public int DrawX = 0, DrawY = 0;            //×を描画する位置(mode=4)
        public System.Drawing.Rectangle matchrect;  //マッチ四角(mode=4)

        public CaptureWin(int mode)
        {
            InitializeComponent();

            this.mode = mode;
            this.Cursor = Cursors.Cross;

            Left = 0;
            Top = 0;
            Width = SystemParameters.PrimaryScreenWidth;
            Height = SystemParameters.PrimaryScreenHeight;

            if (mode == 1 || mode == 2)
            {
                label.Margin = new Thickness(Width / 2 - 50, Height / 2 - 50, 0.0, 0.0);
            }
            else if (mode == 3)
            {
                label.Content = "";
                label.Margin = new Thickness(Width / 2 - 200, Height / 2 - 50, 0.0, 0.0);
            }else if(mode == 4)
            {
                label.Content = "座標確認モード";
                label.Margin = new Thickness(Width / 2 - 50, Height / 2 - 50, 0.0, 0.0);
                CheckLabel.Visibility = Visibility.Visible;

            }
            MM.MainWin.Hide();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(mode == 4)
            {
                Canvas.SetLeft(CheckLabel, DrawX - (CheckLabel.ActualWidth /2));
                Canvas.SetTop(CheckLabel, DrawY - (CheckLabel.ActualHeight / 2));

                Canvas.SetLeft(rectangle, matchrect.X);
                Canvas.SetTop(rectangle, matchrect.Y);
                rectangle.Width = matchrect.Width;
                rectangle.Height = matchrect.Height;
                rectangle.Visibility = Visibility.Visible;
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                MM.MainWin.Show();
                this.DialogResult = false;
            }
            Point point = e.GetPosition(this);
            posx = point.X;
            posy = point.Y;

            if (mode == 1 || mode == 2)
            {
                Canvas.SetLeft(rectangle, posx);
                Canvas.SetTop(rectangle, posy);

                rectangle.Width = 2;
                rectangle.Height = 2;
                rectangle.Visibility = Visibility.Visible;
            }
            else if (mode == 3)
            {
                //this.MouseUp += Window_MouseUp;
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = e.GetPosition(this);

            if (mode == 1 || mode == 2)
            {
                label.Content = string.Format("({0},{1})", point.X, point.Y);

                double w = point.X - posx;
                double h = point.Y - posy;

                if (w < 0) { w = w * -1; }
                if (h < 0) { h = h * -1; }

                rectangle.Width = w;
                rectangle.Height = h;
            }
            else if (mode == 3)
            {
                label.Content = string.Format("マッチ座標({0},{1}) 相対座標({2},{3})score{4:0.00}",
                    pnt.X, pnt.Y, point.X - pnt.X, point.Y - pnt.Y, score);
            }
            else if(mode == 4)
            {
                label.Content = string.Format("絶対座標({0},{1})", point.X, point.Y);
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Cursor = Cursors.Arrow;

            Point point = e.GetPosition(this);
            if (mode == 1 || mode == 2)
            {
                rect = new System.Drawing.Rectangle((int)posx, (int)posy, (int)rectangle.Width, (int)rectangle.Height);
            }
            else if (mode == 3)
            {
                rect = new System.Drawing.Rectangle((int)point.X - pnt.X, (int)point.Y - pnt.Y, (int)rectangle.Width, (int)rectangle.Height);
            }
            MM.MainWin.Show();
            this.DialogResult = true;
        }

    }
}
