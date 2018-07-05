﻿using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Content;
using Astropix.Services;
using System.Threading;
using Astropix.Factories;

namespace Astropix
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity
	{
        ImageView image;
        TextView title, explanation, copyright;

        protected override void OnCreate(Bundle savedInstanceState)
		{
            //TODO: Fill a View that will show the Current image of the day information
			

			SetContentView(Resource.Layout.activity_main);

			Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

			FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            image = FindViewById<ImageView>(Resource.Id.ivImageOfTheDay);
             title = FindViewById<TextView>(Resource.Id.tvTitle);
             explanation = FindViewById<TextView>(Resource.Id.tvExplanation);
             copyright = FindViewById<TextView>(Resource.Id.tvCopyright);
           
            
           
            fab.Click += FabOnClick;
            base.OnCreate(savedInstanceState);
        }
        protected override void OnResume()
        {
            try
            {
                if (AstropixRetrieverService.isInfoAvailable == true)
                {
                    FillImageOfTheDayInformation();
                }

            }
            catch
            {
                Console.Write("failed");
            }
            base.OnResume();
        }

        private void FillImageOfTheDayInformation()
        {
           ImageOfTheDay imageOfTheDay= ImageOfTheDay.ImageOfTheDayInstance();
            title.Text = imageOfTheDay.Title;
            copyright.Text = imageOfTheDay.Copyright;
            explanation.Text = imageOfTheDay.Explanation;
           image.SetImageBitmap(ImageComposer.RetrieveImageInStandardQuality(imageOfTheDay.Url));
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                //TODO: GO to settings screen
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            //View view = (View) sender;
            //Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
            //    .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
            Intent intent = new Intent(this, typeof(AstropixRetrieverService));
            StartService(intent);
            
        }
	}
}
