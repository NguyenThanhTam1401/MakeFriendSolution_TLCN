using MakeFriendSolution.Common;
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
    public class FeatureApplication : IFeatureApplication
    {
        private readonly MakeFriendDbContext _context;
        public FeatureApplication(MakeFriendDbContext context)
        {
            _context = context;
        }
        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Feature> AddFeature(Feature feature)
        {
            _context.Features.Add(feature);
            await Save();
            return feature;
        }

        public async Task<FeatureDetail> AddFeatureDetail(FeatureDetail featureDetail)
        {
            _context.FeatureDetails.Add(featureDetail);
            await Save();
            return featureDetail;
        }

        public async Task<bool> DeleteFeature(int id)
        {
            var userFeatures = await _context.UserFeatures.Where(x => x.FeatureId == id).ToListAsync();
            var featureDetail = await _context.FeatureDetails.Where(x => x.FeatureId == id).ToListAsync();

            _context.UserFeatures.RemoveRange(userFeatures);
            _context.FeatureDetails.RemoveRange(featureDetail);
            try
            {
                await Save();
                await UpdateGlobalVariable();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteFeatureDetail(int id)
        {
            var userFeatures = await _context.UserFeatures.Where(x => x.FeatureDetailId == id).ToListAsync();
            var featureDetail = await _context.FeatureDetails.FindAsync(id);
            _context.UserFeatures.RemoveRange(userFeatures);
            _context.FeatureDetails.Remove(featureDetail);
            try
            {
                await Save();
                await UpdateGlobalVariable();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<Feature> GetFeatureById(int id)
        {
            if (!GlobalVariable.HaveData)
            {
                await UpdateGlobalVariable();
            }

            var feature = GlobalVariable.Features.Where(x => x.Id == id).FirstOrDefault();
            if (feature == null)
                return null;
            feature.FeatureDetails = GlobalVariable.FeatureDetails.Where(x => x.FeatureId == id).ToList();
            return feature;
        }
        public async Task UpdateGlobalVariable()
        {
            GlobalVariable.Features = await _context.Features.ToListAsync();
            GlobalVariable.FeatureDetails = await _context.FeatureDetails.ToListAsync();
            GlobalVariable.HaveData = true;
        }
        public async Task<List<FeatureDetail>> GetFeatureDetails()
        {
            if (!GlobalVariable.HaveData)
            {
               await UpdateGlobalVariable();
            }
            return GlobalVariable.FeatureDetails;
        }

        public async Task<FeatureResponse> GetFeatureResponse(int featureDetailId)
        {
            if (!GlobalVariable.HaveData)
            {
                await UpdateGlobalVariable();
            }
            var featureDetail = GlobalVariable.FeatureDetails.Where(x => x.Id == featureDetailId).FirstOrDefault();
            var feature = GlobalVariable.Features.Where(x => x.Id == featureDetail.FeatureId).FirstOrDefault();

            return new FeatureResponse()
            {
                FeatureId = feature.Id,
                FeatureDetailId = featureDetail.Id,
                Name = feature.Name,
                Content = featureDetail.Content
            };
        }

        public async Task<List<Feature>> GetFeatures()
        {
            if (!GlobalVariable.HaveData)
                await UpdateGlobalVariable();
            return GlobalVariable.Features;
        }

        public async Task<FeatureViewModel> GetFeatureViewModel(int featureDetailId)
        {
            if (!GlobalVariable.HaveData)
            {
                await UpdateGlobalVariable();
            }
            var featureDetail = GlobalVariable.FeatureDetails.Where(x => x.Id == featureDetailId).FirstOrDefault();
            var feature = GlobalVariable.Features.Where(x => x.Id == featureDetail.FeatureId).FirstOrDefault();

            return new FeatureViewModel()
            {
                FeatureId = feature.Id,
                FeatureDetailId = featureDetail.Id,
                Name = feature.Name,
                Content = featureDetail.Content,
                Rate = feature.WeightRate,
                weight = featureDetail.Weight
            };
        }

        public async Task<Feature> UpdateFeature(Feature updateFeature)
        {
            _context.Features.Update(updateFeature);
            await Save();
            await UpdateGlobalVariable();
            return updateFeature;
        }

        public async Task<FeatureDetail> UpdateFeatureDetail(FeatureDetail updateFeatureDetail)
        {
            _context.FeatureDetails.Update(updateFeatureDetail);
            await Save();
            await UpdateGlobalVariable();
            return updateFeatureDetail;
        }
    }
}
