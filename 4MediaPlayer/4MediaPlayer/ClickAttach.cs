using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _4MediaPlayer
{
    public class ClickAttach : FrameworkElement
    {
        public static int GetClicks(FrameworkElement ctrl)
        {
            return (int)ctrl.GetValue(ClicksProperty);
        }
        public static void SetClicks(FrameworkElement ctrl, int value)
        {
            ctrl.SetValue(ClicksProperty, value);
        }

        public static readonly DependencyProperty ClicksProperty = DependencyProperty.RegisterAttached(
                          "Clicks",
                          typeof(int),
                          typeof(FrameworkElement));
    }
}
