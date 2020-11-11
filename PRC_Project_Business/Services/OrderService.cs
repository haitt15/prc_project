using AutoMapper;
using PRC_Project.Data.Models;
using PRC_Project.Data.UnitOfWork;
using PRC_Project.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRC_Project_Business.Services
{
    public class OrderService : IOrderService
    {
        private IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Orders> OrderProducts(OrderModel orderModel)
        {
            Orders order = null;
            if (orderModel != null)
            {
                order = new Orders
                {
                    OrderId = Guid.NewGuid().ToString(),
                    Username = orderModel.Username,
                    Address = orderModel.Address,
                    Phone = orderModel.Phone,
                    Total = orderModel.Total,
                    InsBy = orderModel.Username,
                    UpdBy = orderModel.Username,
                    InsDatetime = DateTime.Now,
                    UpdDatetime = DateTime.Now,
                    DelFlg = false,
                };

                _unitOfWork.OrdersRepository.Add(order);

                foreach (ProductModel product in orderModel.ListProductModels)
                {
                    var orderDetailModel = new OrderDetailModel()
                    {
                        OrderId = order.OrderId,
                        ProductId = product.ProductId,
                        Quantity = product.Quantity,
                        Price = product.Price
                    };
                    
                    _unitOfWork.OrderDetailRepository.Add(_mapper.Map<OrderDetail>(orderDetailModel));
                }

                await _unitOfWork.SaveAsync();
            }
            return _mapper.Map<Orders>(order);
        }
    }
}
