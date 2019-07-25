using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace IconLib
{
    public static  class IconExtensions
    {
        public static Icon[] Split(this Icon icon)
        {
            return IconUtil.Split(icon);
        }
        public static BitmapSource ToBitmapSource(this Icon icon)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                          icon.Handle,
                          System.Windows.Int32Rect.Empty,
                          System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
        }
    }
}
