using MakeFriendSolution.Models;
using MakeFriendSolution.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeFriendSolution.Application
{
    public interface IFeatureApplication
    {
        Task UpdateGlobalVariable();
        Task<List<Feature>> GetFeatures();
        Task<List<FeatureDetail>> GetFeatureDetails();
        Task<Feature> GetFeatureById(int id);

        Task<Feature> AddFeature(Feature feature);
        Task<Feature> UpdateFeature(Feature updateFeature);
        Task<Boolean> DeleteFeature(int id);

        Task<FeatureDetail> AddFeatureDetail(FeatureDetail featureDetail);
        Task<FeatureDetail> UpdateFeatureDetail(FeatureDetail updateFeatureDetail);
        Task<bool> DeleteFeatureDetail(int id);

        Task<FeatureViewModel> GetFeatureViewModel(int featureDetailId);
        Task<FeatureResponse> GetFeatureResponse(int featureDetailId);
    }
}
