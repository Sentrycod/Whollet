﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Whollet.Views.KYC;
using Xamarin.Forms;

namespace Whollet.ViewModel
{
    public class DocumentVerificationViewModel : BaseViewModel
    {
        public DocumentVerificationViewModel()
        {
            Progress = 50;
            bool v = App.LoggedInUser.NationalID == null || App.LoggedInUser.NationalID.Length == 0 ? NationalIDCompleted = false : NationalIDCompleted = true;
            var t = App.LoggedInUser.Passport == null || App.LoggedInUser.Passport.Length == 0 ? PassportCompleted = false : PassportCompleted = true;
            var u = App.LoggedInUser.Drivers_license == null || App.LoggedInUser.Drivers_license.Length == 0 ? DriversLicenseCompleted = false : DriversLicenseCompleted = true;

            if (v)
            {
                Progress = Progress + 20;
            }
            if (t)
            {
                Progress = Progress + 20;
            }
            if (u)
            {
                Progress = Progress + 20;
            }
            if (NationalIDCompleted && PassportCompleted && DriversLicenseCompleted)
            {
                IsRegistered = true;
                Progress = 100;
            }
        }

        private int progress;

        public int Progress
        {
            get { return progress; }
            set { progress = value; OnPropertyChanged(); }
        }


        private bool nationalIDCompleted;

        public bool NationalIDCompleted
        {
            get { return nationalIDCompleted; }
            set { nationalIDCompleted = value; OnPropertyChanged(); }
        }

        private bool passportCompleted;

        public bool PassportCompleted
        {
            get { return passportCompleted; }
            set { passportCompleted = value; OnPropertyChanged(); }
        }

        private bool driverslicenseCompleted;

        public bool DriversLicenseCompleted
        {
            get { return driverslicenseCompleted; }
            set { driverslicenseCompleted = value; OnPropertyChanged(); }
        }

        private bool isRegistered;

        public bool IsRegistered
        {
            get { return isRegistered; }
            set { isRegistered = value; OnPropertyChanged(); }
        }


        public Command FinishedRegisteringCommand => new Command(() =>
        {
            GoToPageAsync(Startup.Resolve<KycLastConfirmed>());
        });

        public Command GoToScan => new Command((x) => 
        {
            var parameter = (ImageForm)Enum.Parse(typeof(ImageForm), x.ToString());
            switch (parameter)
            {
                case ImageForm.NationalID:
                    var nextpage = ActivatorUtilities.CreateInstance<FrontDocumentScan>(Startup.serviceprovider, parameter);
                   
                    GoToPageAsync(nextpage);
                    break;
                case ImageForm.Passport:
                    var vm2 = ActivatorUtilities.CreateInstance<FrontDocumentScan>(Startup.serviceprovider, parameter);
                    
                    GoToPageAsync(vm2);
                    break;
                case ImageForm.Drivers_License:
                    var vm3 = ActivatorUtilities.CreateInstance<FrontDocumentScan>(Startup.serviceprovider, parameter);
                    
                    GoToPageAsync(vm3);
                    break;
                default:
                    break;
            }

            
        });
    }
}
