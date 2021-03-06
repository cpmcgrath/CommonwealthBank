﻿using System;
using System.Linq;
using Microsoft.Phone.Controls;
using System.Collections.Generic;
using CMcG.Bankr.ViewModels.Options;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace CMcG.Bankr.Views.Options
{
    public partial class LoginEditView : PhoneApplicationPage
    {
        public LoginEditView()
        {
            InitializeComponent();
            DataContext = new LoginEditViewModel();
        }

        public Action OnSave { get; set; }

        void Save(object sender, RoutedEventArgs e)
        {
            var vm = (LoginEditViewModel)DataContext;
            vm.Save();

            var popup = (Popup)Parent;
            popup.IsOpen = false;
            OnSave();
        }
    }
}