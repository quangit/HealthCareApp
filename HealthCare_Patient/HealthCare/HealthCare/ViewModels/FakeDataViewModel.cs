using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Services.Interfaces;

namespace HealthCare.ViewModels
{
    public class FakeDataViewModel : BaseViewModel<FakeDataViewModel>
    {

        #region Properties

        public ObservableCollection<FakeModel> ListData { get; set; }

        #endregion

        public FakeDataViewModel(INavigationService navigationService) : base(navigationService)
        {
            GetFakeData();
        }

        public void GetFakeData()
        {
            ListData = new ObservableCollection<FakeModel>
            {
                new FakeModel {Date1 = DateTime.Today, Date2 = DateTime.Today,
                    ImageUrl1 = "https://i1.sndcdn.com/artworks-000116176528-jp7ho1-t200x200.jpg",
                ImageUrl2 = "https://i1.sndcdn.com/artworks-000136026902-gj2b1n-t200x200.jpg",
                Name1 = "Le Thanh Quang",
                Name2 = "Nguyen Thanh Quang"},
                 new FakeModel {Date1 = DateTime.Today, Date2 = DateTime.Today,
                    ImageUrl1 = "https://i1.sndcdn.com/artworks-000116176528-jp7ho1-t200x200.jpg",
                ImageUrl2 = "https://i1.sndcdn.com/artworks-000136026902-gj2b1n-t200x200.jpg",
                Name1 = "Le Thanh Quang",
                Name2 = "Nguyen Thanh Quang"}, new FakeModel {Date1 = DateTime.Today, Date2 = DateTime.Today,
                    ImageUrl1 = "https://i1.sndcdn.com/artworks-000116176528-jp7ho1-t200x200.jpg",
                ImageUrl2 = "https://i1.sndcdn.com/artworks-000136026902-gj2b1n-t200x200.jpg",
                Name1 = "Le Thanh Quang",
                Name2 = "Nguyen Thanh Quang"}
            };
        }
    }

    public class FakeModel
    {
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
        public DateTime Date1 { get; set; }
        public DateTime Date2 { get; set; }
    }
}
