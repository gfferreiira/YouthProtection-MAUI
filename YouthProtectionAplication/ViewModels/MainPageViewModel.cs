﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using YouthProtectionAplication.Views;

namespace YouthProtectionAplication.ViewModels
{
    public partial class MainPageViewModel
    {

        [RelayCommand]
        public async Task CreateAccount()
        {
            await Shell.Current.GoToAsync(nameof(CreateAccountPage));
        }
    }
}
