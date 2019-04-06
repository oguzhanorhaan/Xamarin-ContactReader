using ContactReader.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactReader.DependencyServices
{
    public interface IReadContactsDependecyService
    {
        Task<List<PersonDTO>> ReadContacts();
    }
}
