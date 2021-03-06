﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using YoutubeApp.Model;
using System.Windows.Media.Imaging;
using GoogleAds;

namespace YoutubeApp.View
{
    public partial class PlaylistChannelPage : PhoneApplicationPage
    {
        
        public PlaylistChannelPage()
        {
            
            InitializeComponent();
            ad_banner.Format = AdFormats.Banner;
            ad_banner.AdUnitID = "ca-app-pub-9878130893369780/3779606154";
            AdRequest adRequest = new AdRequest();
            adRequest.ForceTesting = true;
            ad_banner.LoadAd(adRequest);
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            //ScreenResolution screen = new ScreenResolution();
            
            

            base.OnNavigatedTo(e);

            string msg = "";
            string playlist_id="";
            if (NavigationContext.QueryString.TryGetValue("msg", out msg)) playlist_id = msg;
            
            YoutubeMethod ytb = new YoutubeMethod();
            gridlist_gridview.ItemsSource = await ytb.get_video_Playlist(playlist_id);
            await ytb.channelInfo();
            avatar_image.Source = new BitmapImage(new Uri(ytb.Channel_Avatar, UriKind.RelativeOrAbsolute));
            //gridlist_gridview.Visibility = Visibility.Visible;
            
            
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void gridlist_gridview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VideoInfo myobject = (sender as LongListSelector).SelectedItem as VideoInfo;
            System.Diagnostics.Debug.WriteLine(myobject.Id);

            if (myobject != null)
            {

                NavigationService.Navigate(new Uri("/View/VideoPage.xaml?msg=" + myobject.Id, UriKind.Relative));

            }
        }
    }
}