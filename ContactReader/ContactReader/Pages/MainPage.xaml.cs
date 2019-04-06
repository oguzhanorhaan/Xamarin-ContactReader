using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactReader.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();
		}

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await CrossPermissions.Current.RequestPermissionsAsync(Permission.Contacts);
            PermissionStatus ContactsPermission = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Contacts);
            if (ContactsPermission != PermissionStatus.Granted)
            {
                await CrossPermissions.Current.RequestPermissionsAsync(Permission.Contacts);
              
                Device.BeginInvokeOnMainThread(async () => { await DisplayAlert("", "Give permission to access contacts", "Ok"); });
                return;

            }

            await Navigation.PushAsync(new ContactsListPage());
        }
    }
}