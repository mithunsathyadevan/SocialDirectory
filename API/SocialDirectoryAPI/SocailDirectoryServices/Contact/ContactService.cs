using SocailDirectoryModels.Models;
using SocialDirectoryContracts.Contact;
using SocialDirectoryDataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SocailDirectoryServices.Contact
{
    public class ContactService : IContacts
    {
        SocialDirectoryContext _context;
        public ContactService(
            SocialDirectoryContext context)
        {
            _context = context;
        }

        public async Task<List<ContactsModel>> GetContacts(int userId)
        {
            var data = from contact in _context.ContactLists
                       join user in _context.UserDetails
on contact.ContactId equals user.UserId
                       where contact.UserId == userId
                       select new ContactsModel
                       {
                           FirstName=user.FirstName,
                           LastName=user.LastName,
                           UserId=user.UserId,
                           Email=user.Email
                       };
            return await data.ToListAsync();
        }
        public async Task<ResponseViewModel> DeleteContact(int userId, int contactId)
        {
            var returnData = new ResponseViewModel();
            if (!_context.ContactLists.Any(x => x.UserId.Value == userId && x.ContactId == contactId))
            {
                return new ResponseViewModel
                {
                    IsSuccess = false,
                    Message = SocailDirectoryConstants.ContactNotExist

                };
            }
            else
            {
                var contact = _context.ContactLists.Where(x => x.ContactId == contactId && x.UserId == userId).FirstOrDefault();
                _context.ContactLists.Remove(contact);
                await _context.SaveChangesAsync();
                return new ResponseViewModel
                {
                    IsSuccess = true,
                    Message = SocailDirectoryConstants.ContactDeleted

                };
            }
        }
        public async Task<ResponseViewModel> SaveContact(int userId, int contactId)
        {
            var returnData = new ResponseViewModel();
            if(_context.ContactLists.Any(x=>x.UserId.Value==userId&& x.ContactId==contactId))
            {
                return new ResponseViewModel
                {
                    IsSuccess = false,
                    Message = SocailDirectoryConstants.ContactExist

                };
            }
            else
            {
                var ContactLists = new ContactList
                {
                    Active = true,
                    CreatedBy = userId.ToString(),
                    ContactId = contactId,
                    CreatedOn = DateTime.UtcNow,
                    UserId = userId,

                };
                _context.ContactLists.Add(ContactLists);
                await _context.SaveChangesAsync();
                return new ResponseViewModel
                {
                    IsSuccess = true,
                    Message = SocailDirectoryConstants.ContactAdded

                };
            }
        }
    }
}
