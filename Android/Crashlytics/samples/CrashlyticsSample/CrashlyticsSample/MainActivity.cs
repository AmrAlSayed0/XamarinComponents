﻿using Android.App;
using Android.Widget;
using Android.OS;
using Java.Lang;
using Android.Views;
using System;

namespace CrashlyticsSample
{
    [Activity(Label = "Crashlytics Sample", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Fabric.Fabric.With(this, new Crashlytics.Crashlytics());

            Crashlytics.Crashlytics.HandleManagedExceptions();

            SetContentView(Resource.Layout.Main);
            
            var btnForceCrash = FindViewById<Button>(Resource.Id.btnCrashlytics);
            var btnLogin = FindViewById<Button>(Resource.Id.btnFabric);

            btnLogin.Click += (sender, args) =>
            {
                // TODO: Use the current user's information
                // You can call any combination of these three methods
                Crashlytics.Crashlytics.SetUserIdentifier("12345");
                Crashlytics.Crashlytics.SetUserIdentifier("user@fabric.io");
                Crashlytics.Crashlytics.SetUserName("Test User");
            };

            btnForceCrash.Click += (sender, args) =>
            {
				try
				{
					try
					{
						throw new ApplicationException("This is a nexted exception");
					}
					catch (System.Exception e1)
					{
						throw new InvalidCastException("Level 1", e1);
					}
				}
				catch (System.Exception e2)
				{
					throw new InvalidCastException("Level 2", e2);
				}
			};

        }
    }
}