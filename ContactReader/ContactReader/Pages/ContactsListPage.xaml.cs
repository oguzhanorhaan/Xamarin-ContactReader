using ContactReader.DependencyServices;
using ContactReader.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactReader.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactsListPage : ContentPage
	{
        ObservableCollection<PersonDTO> contactList;

        public ContactsListPage ()
		{
			InitializeComponent ();
            GetContacts();
            SearchBar.TextChanged += (sender2, e2) => FilterContacts(SearchBar.Text);
            if (Device.RuntimePlatform == Device.Android)
            {
                LoadingIndicator.IsRunning = true;
                SearchBar.HeightRequest = 50.0;
            }
        }

        private async void GetContacts()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                await Task.Delay(100);

                List<PersonDTO> personList = new List<PersonDTO>();

                await Task.Run(async () =>
                {
                    var result = await DependencyService.Get<IReadContactsDependecyService>().ReadContacts();
                    personList = result;

                });
                if (personList.Count != 0)
                {
                    MainListView.ItemsSource = personList;
                    contactList = new ObservableCollection<PersonDTO>();
                    foreach (var person in personList)
                    {
                        contactList.Add(person);
                    }  
                }
                else
                {
                    await DisplayAlert("", "No contact found", "Ok");
                }
                LoadingIndicator.IsRunning = false;
            }
        }

        private void FilterContacts(string filterText)
        {
            if (!string.IsNullOrEmpty(filterText))
                MainListView.ItemsSource = contactList.Where(x => x.ContactName.ToLower().Contains(filterText.ToLower()));
            else
                MainListView.ItemsSource = contactList;
        }
    }
}