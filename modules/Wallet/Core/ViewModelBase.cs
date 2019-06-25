/*
 * Copyright 2018 NAXAM CO.,LTD.
 *
 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at
 *
 *       http://www.apache.org/licenses/LICENSE-2.0
 *
 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 */ 
using Prism.Navigation;
namespace Wallet.Core
{
    public abstract class ViewModelBase : ModelBase, INavigationAware
    {
        bool _ShouldShowNavigationBar = true;
        public bool ShouldShowNavigationBar
        {
            get => _ShouldShowNavigationBar;
            set => SetProperty(ref _ShouldShowNavigationBar, value);
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}