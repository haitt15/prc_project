using AutoMapper;
using PRC_Project.Data.Models;
using PRC_Project.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRC_Project.Data.AutoMapper
{
    public class ViewModelEntityCommonMapper : Profile
    {
        public ViewModelEntityCommonMapper()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();

            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>();

            CreateMap<OrderDetail, OrderDetailModel>();
            CreateMap<OrderDetailModel, OrderDetail>();

            CreateMap<Users, UserModel>();
            CreateMap<UserModel, Users>();
            CreateMap<RegisterModel, Users>();

            CreateMap<Role, RoleModel>();
            CreateMap<RoleModel, Role>();
        }
    }
}
