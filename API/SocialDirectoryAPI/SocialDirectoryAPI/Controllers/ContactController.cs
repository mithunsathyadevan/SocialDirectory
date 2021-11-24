using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocailDirectoryModels.Models;
using SocialDirectoryAPI.Contract;
using SocialDirectoryAPI.Models;
using SocialDirectoryContracts.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialDirectoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        IContacts _contacts;
        IUserDetailsContract _userDetails;
        public ContactController(IContacts contacts, IUserDetailsContract userDetails)
        {
            _contacts = contacts;
            _userDetails = userDetails;
        }

        [HttpGet("Save")]
        [Authorize]
        public async Task<ResponseViewModel> RegisterUser(int contactId)
        {
            var user = _userDetails.GetUserDetails(HttpContext);
            return await _contacts.SaveContact(user.UserId, contactId);
        }

        [HttpGet("Delete")]
        [Authorize]
        public async Task<ResponseViewModel> DeleteContact(int contactId)
        {
            var user = _userDetails.GetUserDetails(HttpContext);
            return await _contacts.DeleteContact(user.UserId, contactId);
        }

        [HttpGet("ListContacts")]
        [Authorize]
        public async Task<List<ContactResponse>> GetContacts()
        {
            var user = _userDetails.GetUserDetails(HttpContext);
            var data = await _contacts.GetContacts(user.UserId);

            return data.Select(x => new ContactResponse
            {
                UserId = x.UserId,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToList();
        }
    }
}
