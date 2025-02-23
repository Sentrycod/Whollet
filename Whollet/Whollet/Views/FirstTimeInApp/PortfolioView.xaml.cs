﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whollet.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whollet.Views.FirstTimeInApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortfolioView : ContentView
    {
        private PortfolioViewModel viewModel;
        public PortfolioView(PortfolioViewModel vm)
        {
            viewModel = vm;
            InitializeComponent();
            BindingContext = viewModel;
        }
        public PortfolioView()
        {
            InitializeComponent();
        }

        //protected virtual void OnAppearing()
        //{
        //    BindingContext = viewModel;
        //}
    }
}