using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using urfurniture.DAL.Entities;
using urfurniture.Models; 
using urfurniture.Models.Dto.ResponseDto;

namespace urfurniture.Helper
{
    public class AutoMapping: Profile
    {
        public AutoMapping()  
        {
            CreateMap<TblUser,UserResDTOs>();
            CreateMap<UserModel, TblUser>();
            CreateMap<ProductModel, TblProduct>().ForMember(x => x.ProductId, opt => opt.UseDestinationValue())
                                                 .ForMember(x => x.AddedDate, opt => opt.UseDestinationValue())
                                                 .ForMember(x => x.IsActive, opt => opt.UseDestinationValue())
                                                 .ForMember(x => x.UpdateTime, opt => opt.UseDestinationValue())
                                                 .ForMember(x => x.IsDeleted, opt => opt.UseDestinationValue());


            CreateMap<UserAddressModel, TblUserAddress>().ForMember(x => x.IsActive, opt => opt.UseDestinationValue())
                                                         .ForMember(x => x.UpdatedTime, opt => opt.UseDestinationValue())
                                                         .ForMember(x => x.UserRefId, opt => opt.UseDestinationValue());


            CreateMap<TblUserAddress,UserAddressModel>();
            CreateMap<TblUser, UserDetailsModel>();
            CreateMap<UserDetailsModel, TblUser>().ForMember(x => x.AddedDate, opt => opt.UseDestinationValue())
                                                 .ForMember(x => x.IsActive, opt => opt.UseDestinationValue())
                                                 .ForMember(x => x.RoleId, opt => opt.UseDestinationValue())
                                                 .ForMember(x => x.UpdatedTime, opt => opt.UseDestinationValue());

            CreateMap<ProductModel, ProductOptionModel>();
            CreateMap<ProductOptionModel, TblProductOption>().ForMember(x => x.ProductOptionId, opt => opt.UseDestinationValue())
                                                 .ForMember(x => x.CreateDate, opt => opt.UseDestinationValue())
                                                 .ForMember(x => x.IsActive, opt => opt.UseDestinationValue())
                                                 .ForMember(x => x.IsDelete, opt => opt.UseDestinationValue())
                                                 .ForMember(x => x.UpdatedTime, opt => opt.UseDestinationValue());
                                                        
            CreateMap<TblProductOption,ProductOptionModel>();
        }
    }
    
}
