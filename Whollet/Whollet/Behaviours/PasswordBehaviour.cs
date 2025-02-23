﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.CommunityToolkit.Behaviors.Internals;
using Xamarin.Forms;

namespace Whollet.Behaviours
{
    public class PasswordBehaviour : BaseBehavior<VisualElement>
    {
        const string passwordRegex = @"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\w\d\s:])([^\s]){8,16}$";
        public Entry AssociatedObject { get; private set; }
        // public static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(PasswordBehaviour), false);

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create("IsValid", typeof(bool),
            typeof(PasswordBehaviour), false, propertyChanged: (b, o, n) =>
            {
                var spv = (PasswordBehaviour)b;
                spv.IsValid = (bool)n;
                
                //do something with spv
                //o is the old value of the property
                //n is the new value
            });
        Color defaultColor;


        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            set { base.SetValue(IsValidProperty, value);}
        }

        protected override void OnAttachedTo(VisualElement bindable)
        {
            AssociatedObject = bindable as Entry;
            if (!(AssociatedObject == null))
            {
                
                defaultColor = AssociatedObject.TextColor;

                AssociatedObject.TextChanged += HandleTextChanged;
            }           
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            var EntryText = ((Entry)sender).Text;
            IsValid = (Regex.IsMatch(e.NewTextValue, passwordRegex, RegexOptions.None, TimeSpan.FromMilliseconds(250)));
            var col = ((Entry)sender).TextColor;
            ((Entry)sender).TextColor = IsValid ? defaultColor : Color.Red;
            if (EntryText.Length != 0)
            {
                if (EntryText.Contains(" "))
                {
                    EntryText = ((Entry)sender).Text.Remove(EntryText.Length - 1);
                    ((Entry)sender).Text = EntryText;

                }

            }

        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            AssociatedObject = bindable as Entry;
            AssociatedObject.TextChanged -= HandleTextChanged;

        }

        //protected override void OnPropertyChanged([CallerMemberName] string temp = null)
        //{
        //    base.OnPropertyChanged(temp);
        //    if (IsValidProperty.PropertyName == temp)
        //    {
        //        LabelControl.Text = LabelText;
        //    }
            
        //    //if (AttachedEntryBehaviorProperty.PropertyName == temp)
        //    //{
        //    //    EntryControl.Visual = (IVisual)AttachedEntryBehavior;
        //    //}
        //}
    }
}
