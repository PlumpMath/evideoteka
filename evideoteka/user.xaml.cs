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
using AxAXVLC;
using AXVLC;
using Microsoft.Win32;
namespace evideoteka
{
   
    public partial class user : Window
    {
        AxVLCPlugin vlc;
        AxVLCPlugin2 vlc2;
        public user()
        {
            InitializeComponent();
            vlc = new AxVLCPlugin();
            vlc2 = new AxVLCPlugin2();
            windowsFormHostMain.Child = vlc;
            windowsFormHost2.Child = vlc2;
             
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            
            //vlc.addTarget("file:///‪E:\\anime\\Liar Game [Complete]\\Liar Game - 02.avi", null, AXVLC.VLCPlaylistMode.VLCPlayListReplaceAndGo, 0);
               // vlc.play();
                vlc2.playlist.add("file:///‪E:\\anime\\Liar Game [Complete]\\Liar Game - 02.avi");
                vlc2.playlist.play();
            /*OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            if (ofd.FileName != " ")
            {
                
                vlc.addTarget("file:///‪E:\anime\Liar Game [Complete]\Liar Game - 02.avi", null, AXVLC.VLCPlaylistMode.VLCPlayListReplaceAndGo, 0);
                vlc.play();
                vlc2.playlist.add("file:///‪E:\anime\Liar Game [Complete]\Liar Game - 02.avi");
                vlc2.playlist.play();
            }*/
            
        }
    }
}
