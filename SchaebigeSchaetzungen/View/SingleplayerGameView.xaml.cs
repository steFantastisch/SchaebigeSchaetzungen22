﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Net;
using Amazon.DynamoDBv2.Model.Internal.MarshallTransformations;
using SchaebigeSchaetzungen.Command;
using SchaebigeSchaetzungen.ViewModel;

namespace SchaebigeSchaetzungen.View
{

    /// <summary>
    /// Interaction logic for SingleplayerGameView.xaml
    /// </summary>
    /// 
    public partial class SingleplayerGameView : UserControl
    {
        int viewCount;
        int commentCount;
        int likeCount;
        string? language;
        int round;
        string[] VideoIDs;
        int guess;


        public SingleplayerGameView()
        {
            
            round=0;
            InitializeComponent();
            Init();
            
        }

        public async void Init()
        {
            TextBox1.Text="";
            Model.Video video = new Model.Video();
            await video.GetDetailsAsync(video.VideoID);
            webBrowser1.NavigateToString(video.Dispstr);

            viewCount=video.Views;
            likeCount=video.Likes;
            commentCount=video.Comments;
            language=video.Language;
            //game.PlayerOne.guess =1;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SubmitBtn.Content.ToString() == "Submit")
            {
                if (!Int32.TryParse(TextBox1.Text, out guess))
                {
                    //TODO
                    //Fehlermessage wegen falscheingabe
                    return;
                }
                BindingExpression binding3 = TextBox1.GetBindingExpression(TextBox.TextProperty);
                binding3.UpdateSource();
                //TODO change to 3
                if (round > 1) // Maximum 5 Runden
                {
                    SubmitBtn.Content="Result";
                    SubmitBtn.Visibility= Visibility.Collapsed;
                    ResultBtn.Visibility= Visibility.Visible;
                }
                else
                {
                    SubmitBtn.Content="Next Round";
                }
                SubmitBtn.Content="Next Round";
                TextBox1.Visibility= Visibility.Collapsed;
                GuessLabel.Visibility= Visibility.Collapsed;
                HintCheckBox.Visibility= Visibility.Collapsed;
                HintLikes.Visibility = Visibility.Collapsed;
                HintComments.Visibility = Visibility.Collapsed;



                ViewTextBox.Text= viewCount.ToString();
                BindingExpression binding = ViewTextBox.GetBindingExpression(TextBox.TextProperty);
                binding.UpdateSource();

                PointsTextBox.Text=SingleplayerPts(guess, viewCount);
                BindingExpression binding2 = PointsTextBox.GetBindingExpression(TextBox.TextProperty);
                binding2.UpdateSource();



                HintLabel.Content= "Entfernung " +Math.Abs(viewCount - guess);


                ViewTextBox.Visibility = Visibility.Visible;
                PointsTextBox.Visibility= Visibility.Visible;
                ViewLabel.Visibility= Visibility.Visible;
                PointsLabel.Visibility= Visibility.Visible;
                //HintLabel.Visibility= Visibility.Collapsed;
                LanguageLabel.Content="Language: "+language;
                LanguageLabel.Visibility = Visibility.Visible;
                return;

            }

            else if (SubmitBtn.Content.ToString() == "Next Round")
            {
                Init();
                TextBox1.Visibility= Visibility.Visible;
                GuessLabel.Visibility= Visibility.Visible;
                HintCheckBox.Visibility= Visibility.Visible;
                if (HintCheckBox.IsChecked==true)
                {
                    HintLikes.Content = "Likes: " + likeCount.ToString();
                    HintLikes.Visibility = Visibility.Visible;
                    HintComments.Content = "Comments: "+ commentCount.ToString();
                    HintComments.Visibility = Visibility.Visible;
                }

                HintLabel.Content= "Hints";
                ViewTextBox.Visibility = Visibility.Collapsed;
                PointsTextBox.Visibility= Visibility.Collapsed;
                PointsLabel.Visibility = Visibility.Collapsed;
                ViewLabel.Visibility= Visibility.Collapsed;
                LanguageLabel.Visibility = Visibility.Collapsed;
                SubmitBtn.Content="Submit";

                round++;

            }

        }
        public string SingleplayerPts(int Playerguess, int Views)
        {
            int pts = 120;

            return pts.ToString();
        }

        private void HintCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            HintLikes.Content = "Likes: " + likeCount.ToString();
            HintLikes.Visibility = Visibility.Visible;
            HintComments.Content = "Comments: "+ commentCount.ToString();
            HintComments.Visibility = Visibility.Visible;
        }
        private void HintCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            HintLikes.Visibility = Visibility.Collapsed;
            HintComments.Visibility = Visibility.Collapsed;
        }
    }

}

