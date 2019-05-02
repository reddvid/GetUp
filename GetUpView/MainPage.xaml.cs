using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GetUpView
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Parameter as string))
            {
                Debug.WriteLine(e.Parameter.ToString());

                WwwFormUrlDecoder decoder = new WwwFormUrlDecoder(e.Parameter.ToString());

                int fontIndex = Convert.ToInt32(decoder[0].Value);
                string sysUptime = decoder[1].Value;

                ChangeFont(fontIndex);

                upTxt.Text = "System uptime: " + sysUptime;
            }

            base.OnNavigatedTo(e);
        }

        private void ChangeFont(int fontIndex)
        {
            Debug.WriteLine("Changing font: " + fontIndex);

            switch (fontIndex)
            {
                case 0:
                    msgTxt.FontFamily = upTxt.FontFamily = new FontFamily("/Assets/Segoe UI Bold.ttf#Segoe UI");
                    break;

                case 1:
                    msgTxt.FontFamily = upTxt.FontFamily = new FontFamily("/Assets/Montserrat-Bold.ttf#Montserrat");
                    break;

                case 2:
                    msgTxt.FontFamily = upTxt.FontFamily = new FontFamily("/Assets/Raleway-Bold.ttf#Raleway");
                    break;
            }
        }
    }
}
