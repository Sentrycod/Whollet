﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Whollet.Model.Helpers;
using Xamarin.Forms;
using Whollet.Views.KYC;
using Whollet.Views.FirstTimeInApp;

using Whollet.Enums;
using System.Threading.Tasks;
using Whollet.Views.Wallet;
using Whollet.Model;

namespace Whollet.ViewModel
{
    public class KycTabModel2 : BaseViewModel
    {
        // These position keep stock of which Content View to show at any given moment, within the 3 tabs we're working with
        private TabViewManager TransactionViewPosition, DepositViewPosition, PortfolioViewPosition;

        //These dictionaries store Content views, with their positions as keys for the three tabs
        private Dictionary<TabViewManager, ContentView> TransactionsDictionary = new Dictionary<TabViewManager, ContentView>();
        private Dictionary<TabViewManager, ContentView> DepositDictionary = new Dictionary<TabViewManager, ContentView>();
        private Dictionary<TabViewManager, ContentView> PortfolioDictionary = new Dictionary<TabViewManager, ContentView>();

        //These ContentView connects to the Xaml via data binding for dynamic changes via INotifyPropertyChanged
        ContentView firstview, secondview, thirdview;

        //The index keeps track of which tab is on screen at any given moment via the Tab Changed Event
        private int _index;

        

        public KycTabModel2(TabViewManager position, int index)
        {

            // Content view dictionaries populated
            //First view dictionary
            TransactionsDictionary.Add(TabViewManager.FirstView, Startup.Resolve<TransactionsView>());

            //Third view dictionary
            switch (position)
            {
                case TabViewManager.FirstView:
                    if (!PortfolioDictionary.ContainsKey(TabViewManager.FirstView))
                    {
                        PortfolioDictionary.Add(TabViewManager.FirstView, Startup.Resolve<EmptyStateView>());
                    }                    
                    break;
                case TabViewManager.SecondView:
                    if (!PortfolioDictionary.ContainsKey(TabViewManager.SecondView))
                        PortfolioDictionary.Add(TabViewManager.SecondView, Startup.Resolve<EmptyStatePending>());
                    break;
                case TabViewManager.ThirdView:
                    if (!PortfolioDictionary.ContainsKey(TabViewManager.ThirdView))
                        PortfolioDictionary.Add(TabViewManager.ThirdView, Startup.Resolve<EmptyStateFinished>());
                    break;
                case TabViewManager.FourthView:
                    if (!PortfolioDictionary.ContainsKey(TabViewManager.FourthView))
                        PortfolioDictionary.Add(TabViewManager.FourthView, Startup.Resolve<PortfolioView>());
                    break;
                case TabViewManager.FifthView:
                    if(!PortfolioDictionary.ContainsKey(TabViewManager.FifthView))
                        PortfolioDictionary.Add(TabViewManager.FifthView, Startup.Resolve<WalletOverview>());
                    break;
                case TabViewManager.SixthView:
                    break;
                case TabViewManager.SeventhView:
                    break;
                default:
                    break;
            }

            //TabPage assignment
            _index = index;
            ViewSwitcher(_index, position);
            LoadOtherViews();
            MyMenu = GetMenus();


            //if (ThirdView == PortfolioDictionary[portfolioViewPosition])
            //{
            //    SecondView = null;
            //}

        }

        private void LoadOtherViews()
        {
            var t = new List<int> { 0, 1 };
            foreach (var indx in t)
            {
                if (indx == _index)
                {

                }
                else
                {
                    ViewSwitcher(indx, TabViewManager.FirstView);
                }
            }
        }

        private void ViewSwitcher(int index, TabViewManager position)
        {
            switch (index)
            {
                case 0:
                    TransactionViewPosition = position;
                    FirstView = TransactionsDictionary[TransactionViewPosition];
                    break;
                case 1:
                    //removing the deopsit view will cause alot of bugs fml
                    PortfolioViewPosition = position;
                    ThirdView = PortfolioDictionary[PortfolioViewPosition];
                    break;
                default:
                    break;
            }
        }

        public Command TabChanged => new Command((Object args) =>
        {
            var t = args as Xamarin.CommunityToolkit.UI.Views.TabSelectionChangedEventArgs;
            _index = t.NewPosition;
            switch (_index)
            {
                case 0:
                    ViewSwitcher(_index, TransactionViewPosition);
                    break;
                case 1:
                    ViewSwitcher(_index, PortfolioViewPosition);
                    break;
                default:
                    break;
            }

        });
        public Command ChangeViewCommand => new Command(ChangeView());

        private Action ChangeView()
        {
            return () =>
            {
                //use Index to determine which tab page is currently on screen. 
                switch (_index)
                {   case 0:
                        switch (TransactionViewPosition)
                        {
                            case TabViewManager.FirstView:
                                FirstView = TransactionsDictionary[TabViewManager.SecondView];
                                FirstView.BindingContext = this;
                                TransactionViewPosition = TabViewManager.SecondView;
                                // MessagingCenter.Send<object>(this, "Hi");
                                break;
                            case TabViewManager.SecondView:
                                FirstView = TransactionsDictionary[TabViewManager.SecondView];
                                FirstView.BindingContext = this;
                                TransactionViewPosition = TabViewManager.SecondView;
                                break;
                            case TabViewManager.ThirdView:
                                FirstView = TransactionsDictionary[TabViewManager.SecondView];
                                FirstView.BindingContext = this;
                                TransactionViewPosition = TabViewManager.SecondView;
                                //Currentview = ContentViewDictionary[TabViewManager.FourthView];
                                //Currentview.BindingContext = this;
                                //_position = TabViewManager.FourthView;
                                break;
                            case TabViewManager.FourthView:
                                SecondView = DepositDictionary[TabViewManager.FifthView];
                                //  Currentview.BindingContext = this;
                                //  _position = TabViewManager.FifthView;
                                break;
                            case TabViewManager.FifthView:
                                SecondView = DepositDictionary[TabViewManager.SixthView];
                                SecondView.BindingContext = this;
                                DepositViewPosition = TabViewManager.SixthView;
                                break;
                            case TabViewManager.SixthView:
                                SecondView = DepositDictionary[TabViewManager.SeventhView];
                                SecondView.BindingContext = this;
                                DepositViewPosition = TabViewManager.SeventhView;
                                break;
                            case TabViewManager.SeventhView:
                                break;
                            default:
                                break;
                        }
                        // Write code controlling view navigation within first tab
                        break;
                    case 1:
                        switch (PortfolioViewPosition)
                        {
                            case TabViewManager.FirstView:
                                if (!PortfolioDictionary.ContainsKey(TabViewManager.SecondView))
                                    PortfolioDictionary.Add(TabViewManager.SecondView, Startup.Resolve<EmptyStatePending>());
                                ThirdView = PortfolioDictionary[TabViewManager.SecondView];
                                ThirdView.BindingContext = this;
                                PortfolioViewPosition = TabViewManager.SecondView;
                                // MessagingCenter.Send<object>(this, "Hi");
                                break;
                            case TabViewManager.SecondView:
                                if (!PortfolioDictionary.ContainsKey(TabViewManager.ThirdView))
                                    PortfolioDictionary.Add(TabViewManager.ThirdView, Startup.Resolve<EmptyStateFinished>());
                                ThirdView = PortfolioDictionary[TabViewManager.ThirdView];                               
                                ThirdView.BindingContext = this;
                                PortfolioViewPosition = TabViewManager.ThirdView;
                                break;
                            case TabViewManager.ThirdView:
                                if (!PortfolioDictionary.ContainsKey(TabViewManager.FourthView))
                                    PortfolioDictionary.Add(TabViewManager.FourthView, Startup.Resolve<PortfolioView>());
                                ThirdView = PortfolioDictionary[TabViewManager.FourthView];
                                var model = Startup.Resolve<PortfolioViewModel>();
                                ThirdView.BindingContext = model;
                                PortfolioViewPosition = TabViewManager.FourthView;                  
                                break;
                            case TabViewManager.FourthView:
                                
                                //  Currentview.BindingContext = this;
                                //  _position = TabViewManager.FifthView;
                                break;
                            case TabViewManager.FifthView:
                                SecondView = DepositDictionary[TabViewManager.SixthView];
                                SecondView.BindingContext = this;
                                DepositViewPosition = TabViewManager.SixthView;
                                break;
                            case TabViewManager.SixthView:
                                SecondView = DepositDictionary[TabViewManager.SeventhView];
                                SecondView.BindingContext = this;
                                DepositViewPosition = TabViewManager.SeventhView;
                                break;
                            case TabViewManager.SeventhView:
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        // Write code controlling view navigation within first tab
                        break;
                    default:
                        break;
                }
                
            };
        }

        public Command MoveBackCommand => new Command(async () =>
        {
            switch (_index)
            {
                case 0:
                    switch (TransactionViewPosition)
                    {
                        case TabViewManager.FirstView:
                            await Application.Current.MainPage.Navigation.PopAsync();
                            break;
                        case TabViewManager.SecondView:
                            FirstView = TransactionsDictionary[TabViewManager.FirstView];
                            FirstView.BindingContext = this;
                            TransactionViewPosition = TabViewManager.FirstView;
                            break;
                        case TabViewManager.ThirdView:
                            break;
                        case TabViewManager.FourthView:
                            break;
                        case TabViewManager.FifthView:
                            break;
                        case TabViewManager.SixthView:
                            break;
                        case TabViewManager.SeventhView:
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:
                    switch (PortfolioViewPosition)
                    {
                        case TabViewManager.FirstView:
                            await RemoveCurrentPage();
                            break;
                        case TabViewManager.SecondView:
                            ThirdView = PortfolioDictionary[TabViewManager.FirstView];
                            ThirdView.BindingContext = this;
                            PortfolioViewPosition = TabViewManager.FirstView;
                            break;
                        case TabViewManager.ThirdView:
                            ThirdView = PortfolioDictionary[TabViewManager.SecondView];
                            ThirdView.BindingContext = this;
                            PortfolioViewPosition = TabViewManager.SecondView;
                            break;
                        case TabViewManager.FourthView:
                            ThirdView = PortfolioDictionary[TabViewManager.ThirdView];
                            ThirdView.BindingContext = this;
                            PortfolioViewPosition = TabViewManager.ThirdView;
                            break;
                        case TabViewManager.FifthView:
                            await RemoveCurrentPage();
                            break;
                        case TabViewManager.SixthView:
                            ThirdView = PortfolioDictionary[TabViewManager.FifthView];
                            ThirdView.BindingContext = this;
                            PortfolioViewPosition = TabViewManager.FifthView;
                            break;
                        case TabViewManager.SeventhView:
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            
        });

        public Command MyBackPressCommand => new Command(() =>
        {
            Application.Current.MainPage.DisplayAlert("It is working", "Nice", "ok");
        });

        public ContentView SecondView
        {
            get
            {
                return secondview;
            }

            set
            {
                secondview = value;
                OnPropertyChanged();
            }
        }
        public ContentView FirstView
        {
            get { return firstview; }
            set { firstview = value; OnPropertyChanged(); }
        }
        public ContentView ThirdView
        {
            get { return thirdview; }
            set { thirdview = value; OnPropertyChanged(); }
        }

        private List<SwipeMenuItems> mymenu;

        public List<SwipeMenuItems> MyMenu
        {
            get { return mymenu; }
            set { mymenu = value; }
        }

        private List<SwipeMenuItems> GetMenus()
        {
            return new List<SwipeMenuItems>
            {
                new SwipeMenuItems{ Name = "Wallet", Icon = "wallet.png"},
                new SwipeMenuItems{ Name = "Deposit", Icon = "menu_deposit.png"},
                new SwipeMenuItems{ Name = "Withdraw", Icon = "withdraw.png"},
                new SwipeMenuItems{ Name = "Send", Icon = "send.png"},
                new SwipeMenuItems{ Name = "Exchange", Icon = "exchange.png"},
                new SwipeMenuItems{ Name = "Profile", Icon = "profileavatar.png"},
            };
        }

    }
}
