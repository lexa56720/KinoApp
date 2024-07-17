using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KinoApp.ViewModel
{
    internal class MainViewModel
    {
        public ICommand ItemInvokedCommand
        {
            get
            {
                if (itemInvokedCommand == null)
                    itemInvokedCommand = new RelayCommand<object>(ItemInvoked);
                return itemInvokedCommand;
            }
        }
        private ICommand itemInvokedCommand;

        private void ItemInvoked(object o)
        {
            Navigate
        }
    }
}
