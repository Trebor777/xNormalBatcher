﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace XnormalBatcher.ViewModels
{
    internal class SettingsDerivative
    {
        internal Color BackgroundColor;
        internal SettingsDerivative()
        {
            BackgroundColor = Color.FromRgb(127, 127, 0);
        }
    }
    internal class VMSettingsDerivative : BaseViewModel
    {
        public VMSettingsDerivative()
        {
            Data = new SettingsDerivative();
        }

        private SettingsDerivative Data { get; set; }

        public Color BackgroundColor
        {
            get => Data.BackgroundColor;
            set
            {
                Data.BackgroundColor = value;
                NotifyPropertyChanged();
            }
        }

    }
}