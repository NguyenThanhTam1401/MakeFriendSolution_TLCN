using MakeFriendSolution.EF;
using MakeFriendSolution.Models;
using MakeFriendSolution.Models.ViewModels;
using MakeFriendSolution.Services;
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
        private readonly IStorageService _storageService;

        public RelationshipApplication(MakeFriendDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task Accept(int id)
        {
            var relationship = await _context.Relationships
                .Where(x => x.Id == id && x.HasRelationship)
                .FirstOrDefaultAsync();

            if (relationship == null)
                throw new Exception("Can not find relationship");

            var existFrom = await _context.Relationships
                .Where(x => (x.FromId == relationship.FromId || x.ToId == relationship.ToId)
                    && x.HasRelationship && x.IsAccept)
                .FirstOrDefaultAsync();

            if (existFrom != null)
                throw new Exception("Người dùng đang trong mối quan hệ khác");

            if (relationship.IsAccept)
                return;

            relationship.IsAccept = true;
            relationship.UpdatedAt = DateTime.Now;

            try
            {
                _context.Relationships.Update(relationship);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Can not accept this relationship");
            }

        }

        public async Task<Relationship> Create(RelationshipRequest request)
        {

            var isExist = await _context.Relationships
                .AnyAsync(x => (x.FromId == request.ToId || x.ToId == request.ToId)
                && x.HasRelationship && x.IsAccept);

            if (isExist)
                throw new Exception("Người dùng đang trong mối quan hệ với người khác");

            var existFrom = await _context.Relationships
                .Where(x => (x.FromId == request.FromId || x.ToId == request.FromId) && x.HasRelationship && x.IsAccept)
                .FirstOrDefaultAsync();

            if (existFrom != null)
            {
                existFrom.HasRelationship = false;
                existFrom.UpdatedAt = DateTime.Now;
                _context.Relationships.Update(existFrom);
            }

            var relationship = await _context.Relationships
                .Where(x => x.FromId == request.FromId && x.ToId == request.ToId)
                .FirstOrDefaultAsync();

            if(relationship == null)
            {
                if (request.RelationShipType == Models.Enum.ERelationShip.Không_có_gì)
                    return null;

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

                var exist = await _context.Relationships
                    .Where(x => x.FromId == request.FromId && x.HasRelationship && x.IsAccept)
                    .ToListAsync();

                if(exist == null || exist.Count == 0)
                {
                    _context.Relationships.RemoveRange(exist);
                }
            }
            else
            {
                if (request.RelationShipType == relationship.RelationShipType)
                    return relationship;

                if(request.RelationShipType == Models.Enum.ERelationShip.Không_có_gì)
                {
                    relationship.HasRelationship = false;
                }
                else
                {
                    relationship.RelationShipType = request.RelationShipType;
                    relationship.UpdatedAt = DateTime.Now;
                }

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

        public async Task Decline(int id)
        {
            var relationship = await _context.Relationships
                .Where(x => x.Id == id && x.HasRelationship)
                .FirstOrDefaultAsync();

            if (relationship == null)
                throw new Exception("Can not find relationship");

            relationship.UpdatedAt = DateTime.Now;
            relationship.HasRelationship = false;

            try
            {
                _context.Relationships.Update(relationship);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Can not decline this relationship");
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

        public async Task<RelationshipResponse> GetById(Guid fromId, Guid toId)
        {
            var relationship = await _context.Relationships
                .Where(x => x.FromId == fromId && x.ToId == toId && x.HasRelationship)
                .FirstOrDefaultAsync();

            if (relationship == null)
                throw new Exception("Can not find relationship");

            var fromUser = await _context.Users.FindAsync(relationship.FromId);
            var toUser = await _context.Users.FindAsync(relationship.ToId);

            return new RelationshipResponse(relationship, fromUser, toUser, _storageService);
        }

        public async Task<RelationshipResponse> GetByUserId(Guid userId)
        {
            var relationship = await _context.Relationships
                .Where(x => (x.FromId == userId || x.ToId == userId) && x.HasRelationship && x.IsAccept)
                .FirstOrDefaultAsync();

            if (relationship == null)
                throw new Exception("Can not find relationship");

            var fromUser = await _context.Users.FindAsync(relationship.FromId);
            var toUser = await _context.Users.FindAsync(relationship.ToId);

            return new RelationshipResponse(relationship, fromUser, toUser, _storageService);
        }
    }
}
