using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using IconLib;
using ShellLink;

namespace dock
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Items = new ObservableCollection<ShortcutIcon>();

            AddIcon(@"C:\Program Files\Blender Foundation\Blender\blender.exe");
        }

        public ObservableCollection<ShortcutIcon> Items { get; set; }

        private void AddIcon(string path, int index = 0)
        {
            if (path.ToLower().EndsWith(".ico"))
            {
                var sysicon = new System.Drawing.Icon(path);
                var icon = sysicon.Split().OrderByDescending(x => x.Width).FirstOrDefault().ToBitmapSource();
                Items.Add(new ShortcutIcon() { Icon = icon });
            }
            else
            {
                var ie = new IconExtractor(path);
                var p = ie.GetIcon(index).Split().OrderByDescending(x => x.Width).FirstOrDefault();
                Items.Add(new ShortcutIcon() { Icon = p.ToBitmapSource() });
            }
        }

        public Tuple<string,int> GetShortcutTargetFile(string shortcutFilename)
        {
            var r = Shortcut.ReadFromFile(shortcutFilename);
            if (r.StringData.IconLocation != null)
            {
                if(!File.GetAccessControl(r.StringData.IconLocation).AreAccessRulesProtected)
                    return new Tuple<string, int>(r.StringData.IconLocation, r.IconIndex);
            }
            return new Tuple<string, int>(r.LinkTargetIDList.Path, r.IconIndex);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePosition();
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach(var p in files)
                {
                    try
                    {
                        if (p.ToLower().EndsWith("lnk"))
                        {
                            var r = GetShortcutTargetFile(p);
                            AddIcon(r.Item1,r.Item2);
                        }
                        else
                            AddIcon(p);
                    }
                    catch { }
                }

            }
            
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdatePosition();
        }

        double maxWidth = 0;
        private void UpdatePosition()
        {
            var w = System.Windows.SystemParameters.PrimaryScreenWidth;
            var h = System.Windows.SystemParameters.PrimaryScreenHeight;

            if (this.Width > maxWidth)
            {
                maxWidth = Width + 30;
                MinWidth = maxWidth - 20;

                this.Left = (w / 2) - (Width / 2);
                this.Top = h - Height - 50;
            }
        }
    }

    public class ShortcutIcon
    {
        public BitmapSource Icon { get; set; }
        public string Name { get; set; }
        public Uri Uri { get; set; }
    }
}
