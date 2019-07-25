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

namespace dock
{
    /// <summary>
    /// Interação lógica para ZoomControl.xam
    /// </summary>
    public partial class ZoomControl : UserControl
    {
        public ZoomControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SourceProperty =
         DependencyProperty.Register("Source", typeof(ImageSource), typeof(ZoomControl), new
            PropertyMetadata());

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
    }
}
