using CarLog.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLog.ViewModels
{
    public class MainViewModel
    {

        public MainViewModel() {

            LoadDataFromRepository();

        }

        private async void LoadDataFromRepository()
        {

            /////[TEMP] await CLRepository.LoadDataAsync();

        }

    }
}
