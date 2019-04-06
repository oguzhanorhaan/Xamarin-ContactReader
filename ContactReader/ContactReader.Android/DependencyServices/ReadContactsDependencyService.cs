using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ContactReader.DependencyServices;
using ContactReader.Droid.DependencyServices;
using ContactReader.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReadContactsDependencyService))]
namespace ContactReader.Droid.DependencyServices
{
    public class ReadContactsDependencyService : IReadContactsDependecyService
    {
        Task<List<PersonDTO>> ReadContactsTask = Task<List<PersonDTO>>.Factory.StartNew(() =>
        {
            var context = Forms.Context;
            // var uri = ContactsContract.Contacts.ContentUri;
            var uri = ContactsContract.CommonDataKinds.Phone.ContentUri;
            string[] projection = {
            ContactsContract.Contacts.InterfaceConsts.Id,
            ContactsContract.Contacts.InterfaceConsts.DisplayName,
            ContactsContract.CommonDataKinds.Phone.Number

        };
            var cursor = context.ContentResolver.Query(uri, projection, null, null, null);
            var contactList = new List<PersonDTO>();

            if (cursor.MoveToFirst())
            {
                do
                {
                    PersonDTO personCO = new PersonDTO
                    {
                        ContactName = cursor.GetString(cursor.GetColumnIndex(projection[1])),
                        ContactPhone = cursor.GetString(cursor.GetColumnIndex(projection[2])),
                    };
                    contactList.Add(personCO);
                }
                while (cursor.MoveToNext());
            }
            return contactList;
        });

        Task<List<PersonDTO>> IReadContactsDependecyService.ReadContacts()
        {
            return ReadContactsTask;
        }
    }
}