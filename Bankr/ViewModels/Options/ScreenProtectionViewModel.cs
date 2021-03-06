﻿using System;
using System.Linq;
using System.Collections.Generic;
using CMcG.Bankr.Data;
using Caliburn.Micro;

namespace CMcG.Bankr.ViewModels.Options
{
    public class ScreenProtectionViewModel : ViewModelBase
    {
        public ScreenProtectionViewModel(INavigationService navigationService) : base(navigationService) { }

        protected override void OnLoad()
        {
            Items = App.GetViewModelTypes().Select(GetScreenSecurity)
                       .OrderBy(y => y.Name).ToArray();

            OptionItems   = Items.Where(x => x.Group == "Options" ).ToArray();
            TransferItems = Items.Where(x => x.Group == "Transfer").ToArray();
            OtherItems    = Items.Except(OptionItems).Except(TransferItems).ToArray();
        }

        public ScreenSecurity GetScreenSecurity(Type vm)
        {
            return new ScreenSecurity().Load(vm, App.Current.Security.GetAccessLevel(vm));
        }

        public ScreenSecurity[] Items         { get; set; }
        public ScreenSecurity[] OptionItems   { get; set; }
        public ScreenSecurity[] TransferItems { get; set; }
        public ScreenSecurity[] OtherItems    { get; set; }

        public void Save()
        {
            using (var store = new DataStoreContext())
            {
                store.ScreenSecurity.DeleteAllOnSubmit(store.ScreenSecurity.ToArray());
                store.SubmitChanges();
                store.ScreenSecurity.InsertAllOnSubmit(Items.Where(x => x.AccessLevel != AccessLevel.None));
                store.SubmitChanges();
            }

            App.Current.Security.LoadFromDatabase();
            Navigator.GoBack();
        }
    }
}
