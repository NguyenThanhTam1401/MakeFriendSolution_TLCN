﻿using MakeFriendSolution.EF;
using MakeFriendSolution.Models;
using MakeFriendSolution.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Application
{
    public class FeedbackApplication : IFeedbackApplication
    {
        private readonly MakeFriendDbContext _context;
        public FeedbackApplication(MakeFriendDbContext context)
        {
            _context = context;
        }
        public async Task<FeedbackResponse> Create(FeedbackRequest request)
        {
            if(request.Vote <= 0)
            {
                throw new Exception("Vote must be between 1 and 5");
            }

            var feedback = new Feedback(request);

            try
            {
                _context.Feedbacks.Add(feedback);
                await _context.SaveChangesAsync();
                return new FeedbackResponse(feedback);
            }
            catch (Exception)
            {
                throw new Exception("Can not create feedback");
            }
        }

        public async Task<List<FeedbackResponse>> Get(PagingRequest request)
        {
            var feedbacks = await _context.Feedbacks
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var responses = new List<FeedbackResponse>();

            foreach (var item in feedbacks)
            {
                var user = await _context.Users.FindAsync(item.UserId);
                var response = new FeedbackResponse(item, user);

                responses.Add(response);
            }
            return responses;
        }

        public async Task<FeedbackResponse> Update(FeedbackRequest request)
        {
            var feedback = await _context.Feedbacks.FindAsync(request.Id);

            if(feedback.UserId != request.UserId)
            {
                throw new Exception("UserId not match");
            }

            feedback.Title = request.Title;
            feedback.Content = request.Content;
            feedback.UpdatedAt = DateTime.Now;

            try
            {
                _context.Feedbacks.Update(feedback);
                await _context.SaveChangesAsync();
                return new FeedbackResponse(feedback);
            }
            catch (Exception)
            {
                throw new Exception("Can not update feedback");
            }

        }
    }
}
