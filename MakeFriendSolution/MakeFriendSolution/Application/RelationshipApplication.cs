using MakeFriendSolution.EF;
using MakeFriendSolution.Models;
using MakeFriendSolution.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Application
{
    public class RelationshipApplication : IRelationshipApplication
    {
        private readonly MakeFriendDbContext _context;

        public RelationshipApplication(MakeFriendDbContext context)
        {
            _context = context;
        }
        public async Task<Relationship> Create(RelationshipRequest request)
        {
            var relationship = await _context.Relationships
                .Where(x => x.FromId == request.FromId && x.ToId == request.ToId)
                .FirstOrDefaultAsync();

            if(relationship == null)
            {
                relationship = new Relationship()
                {
                    Id = 0,
                    FromId = request.FromId,
                    ToId = request.ToId,
                    HasRelationship = true,
                    RelationShipType = request.RelationShipType,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Relationships.Add(relationship);
            }
            else
            {
                relationship.RelationShipType = request.RelationShipType;
                relationship.UpdatedAt = DateTime.Now;
                _context.Relationships.Update(relationship);
            }

            try
            {
                await _context.SaveChangesAsync();
                return relationship;
            }
            catch (Exception)
            {
                throw new Exception("Can not save Relationship");
            }

        }

        public async Task Delete(Guid userId)
        {
            var relationship = await _context.Relationships
                .Where(x => (x.FromId == userId || x.ToId == userId) && x.HasRelationship)
                .FirstOrDefaultAsync();

            if(relationship != null)
            {
                relationship.HasRelationship = false;
            }

            try
            {
                _context.Relationships.Update(relationship);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Can not delete this relationship");
            }

        }

        public async Task<RelationshipResponse> GetByUserId(Guid userId)
        {
            var relationship = await _context.Relationships
                .Where(x => (x.FromId == userId || x.ToId == userId) && x.HasRelationship)
                .FirstOrDefaultAsync();

            if (relationship == null)
                throw new Exception("Can not find relationship");

            var fromUser = await _context.Users.FindAsync(relationship.FromId);
            var toUser = await _context.Users.FindAsync(relationship.ToId);

            return new RelationshipResponse(relationship, fromUser, toUser);
        }
    }
}
