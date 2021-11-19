using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocailDirectoryModels.Models;
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
        public ContactController(IContacts contacts)
        {
            _contacts = contacts;
        }
        [HttpGet("Save")]
        [Authorize]
        public async Task<ResponseViewModel> RegisterUser(int contactId)
        {
            int userId = Convert.ToInt32(HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);


            return await _contacts.SaveContact(userId, contactId);
        }
        [HttpGet("Delete")]
        [Authorize]
        public async Task<ResponseViewModel> DeleteContact(int contactId)
        {
            int userId = Convert.ToInt32(HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);


            return await _contacts.DeleteContact(userId, contactId);
        }
        [HttpGet("ListContacts")]
        [Authorize]
        public async Task<List<ContactResponse>> GetContacts()
        {

            int userId= Convert.ToInt32(HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value); 
            var data= await _contacts.GetContacts(userId);
          
            return data.Select(x => new ContactResponse
            {
                UserId = x.UserId,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToList();
        }
    }
}
