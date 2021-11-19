using SocailDirectoryModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialDirectoryContracts.Contact
{
    public interface IContacts
    {
        Task<ResponseViewModel> SaveContact(int userId, int contactId);
        Task<List<ContactsModel>> GetContacts(int userId);
        Task<ResponseViewModel> DeleteContact(int userId, int contactId);
    }
}
