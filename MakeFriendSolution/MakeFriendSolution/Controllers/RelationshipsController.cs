using MakeFriendSolution.Application;
using MakeFriendSolution.Models.ViewModels;
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
    public class RelationshipsController : ControllerBase
    {
        private readonly IRelationshipApplication _relationshipApp;
        public RelationshipsController(IRelationshipApplication relationshipApp)
        {
            _relationshipApp = relationshipApp;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(RelationshipRequest request)
        {
            try
            {
                var relationship = await _relationshipApp.Create(request);
                return Ok(relationship);
            }
            catch (Exception e)
            {

                return BadRequest(new
                {
                    Message = e.Message
                });
            }
        }

        [HttpDelete("{userId}")]
        [Authorize]
        public async Task<IActionResult> DeleteRelationship(Guid userId)
        {
            try
            {
                await _relationshipApp.Delete(userId);
                return Ok();
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
