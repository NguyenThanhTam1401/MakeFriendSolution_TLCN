using MakeFriendSolution.Application;
using MakeFriendSolution.EF;
using MakeFriendSolution.Models;
using MakeFriendSolution.Models.ViewModels;
using MakeFriendSolution.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly MakeFriendDbContext _context;
        private readonly IStorageService _storageService;
        private ISessionService _sessionService;
        private readonly IUserApplication _userApplication;
        private readonly IFeatureApplication _featureApplication;

        public FeaturesController(MakeFriendDbContext context, IStorageService storageService, ISessionService sessionService, IUserApplication userApplication, IFeatureApplication featureApplication)
        {
            _context = context;
            _storageService = storageService;
            _sessionService = sessionService;
            _userApplication = userApplication;
            _featureApplication = featureApplication;
        }

        [HttpGet]
        public async Task<IActionResult> GetFeatures()
        {
            var features = await _featureApplication.GetFeatures();
            return Ok(features);
        }
        [HttpPost]
        public async Task<IActionResult> AddFeature(CreateFeatureRequest request)
        {
            var feature = new Feature()
            {
                Name = request.Name,
                IsCalculated = request.IsCalculated,
                IsSearchFeature = request.IsSearchFeature,
                WeightRate = request.WeightRate,
            };

            feature = await _featureApplication.AddFeature(feature);
            return Ok(feature);
        }

        public async Task<IActionResult> AddFeatureDetail(CreateFeatureDetailRequest request)
        {
            var featureDetail = new FeatureDetail()
            {
                Content = request.Content,
                Weight = request.Weight,
                FeatureId = request.FeatureId
            };

            featureDetail = await _featureApplication.AddFeatureDetail(featureDetail);
            return Ok(featureDetail);
        }

        public async Task<IActionResult> UpdateFeature(UpdateFeatureRequest request)
        {
            var updateFeature = await _featureApplication.GetFeatureById(request.Id);
            if(updateFeature == null)
            {
                return NotFound(new
                {
                    Message = "Can not find feature with Id = " + request.Id
                });
            }

            updateFeature.IsCalculated = request.IsCalculated;
            updateFeature.Name = request.Name;
            updateFeature.WeightRate = request.WeightRate;
            updateFeature.IsSearchFeature = request.IsSearchFeature;
            updateFeature = await _featureApplication.UpdateFeature(updateFeature);

            return Ok(updateFeature);
        }

        public async Task<IActionResult> DeleteFeature(int id)
        {
            var userFeature = await _context.UserFeatures.Where(x => x.FeatureId == id).ToListAsync();
            var featureDetails = await _context.FeatureDetails.Where(x => x.FeatureId == id).ToListAsync();
            var feature = await _featureApplication.GetFeatureById(id);
            _context.UserFeatures.RemoveRange(userFeature);
            _context.FeatureDetails.RemoveRange(featureDetails);
            _context.Features.Remove(feature);

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        public async Task<IActionResult> DeleteFeatureDetail(int id)
        {
            var featureDetail = await _context.FeatureDetails.FindAsync(id);
            var userFeature = await _context.UserFeatures.Where(x => x.FeatureDetailId == id).ToListAsync();

            _context.UserFeatures.RemoveRange(userFeature);
            _context.FeatureDetails.Remove(featureDetail);

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
