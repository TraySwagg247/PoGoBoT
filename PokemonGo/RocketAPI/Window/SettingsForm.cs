﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.MapProviders;
using GMap.NET;
using System.Configuration;
using System.Globalization;

namespace PokemonGo.RocketAPI.Window
{
    partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        public class Loc
        {
            public string name { get; set; }
            public double lat { get; set; }
            public double lng { get; set; }

            public override string ToString()
            {
                return name;
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

            authTypeCb.Text = Settings.Instance.AuthType.ToString();
            if (authTypeCb.Text == "google")
            {
                UserLoginBox.Text = Settings.Instance.Email.ToString();
                UserPasswordBox.Text = Settings.Instance.Password.ToString();
            } else
            {
                UserLoginBox.Text = Settings.Instance.PtcUsername.ToString();
                UserPasswordBox.Text = Settings.Instance.PtcPassword.ToString();
            }
            latitudeText.Text = Settings.Instance.DefaultLatitude.ToString();
            longitudeText.Text = Settings.Instance.DefaultLongitude.ToString();
            razzmodeCb.Text = Settings.Instance.RazzBerryMode;
            razzSettingText.Text = Settings.Instance.RazzBerrySetting.ToString();
            transferTypeCb.Text = Settings.Instance.TransferType;
            transferCpThresText.Text = Settings.Instance.TransferCPThreshold.ToString();
            transferIVThresText.Text = Settings.Instance.TransferIVThreshold.ToString();
            evolveAllChk.Checked = Settings.Instance.EvolveAllGivenPokemons;
            CatchPokemonBox.Checked = Settings.Instance.CatchPokemon;
            TravelSpeedBox.Text = Settings.Instance.TravelSpeed.ToString();
           // ImageSizeBox.Text = Settings.Instance.ImageSize.ToString();
            // Initialize map:
            //use google provider
            gMapControl1.MapProvider = GoogleMapProvider.Instance;
            //get tiles from server only
            gMapControl1.Manager.Mode = AccessMode.ServerOnly;
            //not use proxy
            GMapProvider.WebProxy = null;
            //center map on moscow
            string lat = ConfigurationManager.AppSettings["DefaultLatitude"];
            string longit = ConfigurationManager.AppSettings["DefaultLongitude"];
            lat.Replace(',', '.');
            longit.Replace(',', '.');
            gMapControl1.Position = new PointLatLng(double.Parse(lat.Replace(",", "."), CultureInfo.InvariantCulture), double.Parse(longit.Replace(",", "."), CultureInfo.InvariantCulture));



            //zoom min/max; default both = 2
            gMapControl1.DragButton = MouseButtons.Left;

            gMapControl1.CenterPen = new Pen(Color.Red, 2);
            gMapControl1.MinZoom = trackBar.Maximum = 1;
            gMapControl1.MaxZoom = trackBar.Maximum = 20;
            trackBar.Value = 10;

            //set zoom
            gMapControl1.Zoom = trackBar.Value;

            //Add Options
            addfarminglocations();
        }

        private void addfarminglocations()
        {
            comboLocations.Items.Add(new Loc() { name = "London, England", lat = 51.501663, lng = -0.14102 });
            comboLocations.Items.Add(new Loc() { name = "Myrtle Beach, SC, USA", lat = 33.714451, lng = -78.877194 });
            comboLocations.Items.Add(new Loc() { name = "Santa Monica Pier, LA, USA", lat = 34.00873594425199, lng = -118.49761247634888 });
            comboLocations.Items.Add(new Loc() { name = "Long Beach, CA, USA", lat = 34.00873594425199, lng = -118.49761247634888 });
            comboLocations.Items.Add(new Loc() { name = "Seattle, WA, USA", lat = 47.626680, lng = -122.335884 });
            comboLocations.Items.Add(new Loc() { name = "Bryant Park, NY, USA", lat = 40.75320648472645, lng = -73.98390769958496 });
            comboLocations.Items.Add(new Loc() { name = "Vancouver, BC, Canada", lat = 45.6298963412979, lng = -122.67196521162987 });
            comboLocations.Items.Add(new Loc() { name = "Sydney, Australia", lat = -33.870273020353416, lng = 151.20878219604492 });
            comboLocations.Items.Add(new Loc() { name = "Achen, Germany", lat = 50.776309, lng = 6.083505 });
            comboLocations.Items.Add(new Loc() { name = "Ottawa, Canada", lat = 45.42158812329091, lng = -75.6877326965332 });
            comboLocations.Items.Add(new Loc() { name = "Hamburg, Germany", lat = 53.5588061, lng = 10.057689399999958 });
            comboLocations.Items.Add(new Loc() { name = "Dusseldorf, Germany", lat = 51.224382, lng = 6.778896 });
            comboLocations.Items.Add(new Loc() { name = "Tokyo, Japan", lat = 35.69051125265253, lng = 139.68954205513 });
            comboLocations.Items.Add(new Loc() { name = "Disneyland Park", lat = 33.8120962, lng = -117.9189742 });
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Settings.Instance.SetSetting(authTypeCb.Text, "AuthType");
            if (authTypeCb.Text == "google")
            {
                Settings.Instance.SetSetting(UserLoginBox.Text, "Email");
                Settings.Instance.SetSetting(UserPasswordBox.Text, "Password");
            } else
            {
                Settings.Instance.SetSetting(UserLoginBox.Text, "PtcUsername");
                Settings.Instance.SetSetting(UserPasswordBox.Text, "PtcPassword");
            }
            Settings.Instance.SetSetting(latitudeText.Text.Replace(',', '.'), "DefaultLatitude");
            Settings.Instance.SetSetting(longitudeText.Text.Replace(',', '.'), "DefaultLongitude");

            string lat = ConfigurationManager.AppSettings["DefaultLatitude"];
            string longit = ConfigurationManager.AppSettings["DefaultLongitude"];
            lat.Replace(',', '.');
            longit.Replace(',', '.');


            Settings.Instance.SetSetting(razzmodeCb.Text, "RazzBerryMode");
            Settings.Instance.SetSetting(razzSettingText.Text, "RazzBerrySetting");
            Settings.Instance.SetSetting(transferTypeCb.Text, "TransferType");
            Settings.Instance.SetSetting(transferCpThresText.Text, "TransferCPThreshold");
            Settings.Instance.SetSetting(transferIVThresText.Text, "TransferIVThreshold");
            Settings.Instance.SetSetting(TravelSpeedBox.Text, "TravelSpeed");
            //Settings.Instance.SetSetting(ImageSizeBox.Text, "ImageSize");
            Settings.Instance.SetSetting(evolveAllChk.Checked ? "true" : "false", "EvolveAllGivenPokemons");
            Settings.Instance.SetSetting(CatchPokemonBox.Checked ? "true" : "false", "CatchPokemon");
            Settings.Instance.Reload();

            Close();
        }

        private void authTypeCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (authTypeCb.Text == "google")
            {
                UserLabel.Text = "Email:";
            }
            else
            {
                UserLabel.Text = "Username:";
            }
        }

        private void gMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            Point localCoordinates = e.Location;
            gMapControl1.Position = gMapControl1.FromLocalToLatLng(localCoordinates.X, localCoordinates.Y);

            if (e.Clicks >= 2)
            {
                gMapControl1.Zoom += 5;
            }

            double X = Math.Round(gMapControl1.Position.Lng, 6);
            double Y = Math.Round(gMapControl1.Position.Lat, 6);
            string longitude = X.ToString();
            string latitude = Y.ToString();
            latitudeText.Text = latitude;
            longitudeText.Text = longitude;
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            gMapControl1.Zoom = trackBar.Value;
        }

        private void FindAdressButton_Click(object sender, EventArgs e)
        {
            gMapControl1.SetPositionByKeywords(AdressBox.Text);
            gMapControl1.Zoom = 15;
        }

        private void authTypeLabel_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void transferCpThresText_TextChanged(object sender, EventArgs e)
        {
        }

        private void transferTypeCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (transferTypeCb.Text == "CP")
            {
                label4.Visible = true;
                transferCpThresText.Visible = true;
            }
            else
            {
                label4.Visible = false;
                transferCpThresText.Visible = false;

            }

            if (transferTypeCb.Text == "IV")
            {
                label6.Visible = true;
                transferIVThresText.Visible = true;
            }
            else
            {
                label6.Visible = false;
                transferIVThresText.Visible = false;

            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {

        }

        private void FindAdressButton_Click_1(object sender, EventArgs e)
        {
            gMapControl1.SetPositionByKeywords(AdressBox.Text);
            gMapControl1.Zoom = 15;
            double X = Math.Round(gMapControl1.Position.Lng, 6);
            double Y = Math.Round(gMapControl1.Position.Lat, 6);
            string longitude = X.ToString();
            string latitude = Y.ToString();
            latitudeText.Text = latitude;
            longitudeText.Text = longitude;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void evolveAllChk_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            double lat;
            double lng;

            Loc location = (Loc)comboLocations.SelectedItem;
            latitudeText.Text = location.lat.ToString();
            longitudeText.Text = location.lng.ToString();

            lat = location.lat;
            lng = location.lng;


            gMapControl1.Position = new GMap.NET.PointLatLng(lat, lng);
        }
    }
}