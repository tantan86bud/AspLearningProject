using AspLearningProject.API.Controllers.Models;
using AspLearningProject.API.Models;
using AspLearningProject.Models.DataLayer;
using AutoMapper;
using AutoMapper.Execution;

namespace AspLearningProject.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductResource>();
            CreateMap<ProductEditResource, Product>();
        }
    }

}