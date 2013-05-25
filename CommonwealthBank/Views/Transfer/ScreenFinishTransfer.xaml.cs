﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CMcG.CommonwealthBank.ViewModels.Transfer;

namespace CMcG.CommonwealthBank.Views.Transfer
{
    public partial class ScreenFinishTransfer : PhoneApplicationPage
    {
        public ScreenFinishTransfer()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var argLookup = NavigationContext.QueryString;
            int fromId    = int.Parse(argLookup["fromid"]);
            int toId      = int.Parse(argLookup["toid"]);
            this.CheckPermissions(e, () => new FinishTransferViewModel(fromId, toId));
        }

        void MakeTransaction(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to transfer this money?", "Confirmation", MessageBoxButton.OKCancel);
            if (result != MessageBoxResult.OK)
                return;

            var vm = (FinishTransferViewModel)DataContext;
            //vm.MakeTransaction();
        }

        void Cancel(object sender, EventArgs e)
        {
            NavigationService.RemoveBackEntry();
            NavigationService.RemoveBackEntry();
            NavigationService.GoBack();
        }

    }
}