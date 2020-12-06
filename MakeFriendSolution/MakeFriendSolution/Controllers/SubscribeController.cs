using MakeFriendSolution.Models.ViewModels;
using MakeFriendSolution.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeController : ControllerBase
    {
        private readonly IMailchimpService _mailchimpService;
        public SubscribeController(IMailchimpService mailchimp)
        {
            _mailchimpService = mailchimp;
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Subscribe([FromForm] MailChimpModel mailChimp)
        {
            if (string.IsNullOrEmpty(mailChimp.Email))
                return BadRequest(new
                {
                    Message = "Email is required"
                });

            try
            {
                await _mailchimpService.Subscribe(mailChimp);
                return Ok(new
                {
                    Message = "Thanks for your Subscribe!"
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Message = e.Message
                });
            }
        }
    }
}
