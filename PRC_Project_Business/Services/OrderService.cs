using AutoMapper;
using PRC_Project.Data.Models;
using PRC_Project.Data.Repository;
using PRC_Project.Data.UnitOfWork;
using PRC_Project.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRC_Project_Business.Services
{
    public class OrderService : IOrderService
    {
        private IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public OrderService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<bool> OrderProducts(IEnumerable<ProductModel> listProductModel, string username)
        {
            if (listProductModel != null && username != null)
            {
                // insert into Order
                _uow.OrdersRepository.Add(new Orders()
                {
                    Username = username,
                    InsBy = username,
                    UpdBy = username,
                    InsDatetime = DateTime.Now,
                    UpdDatetime = DateTime.Now,
                    DelFlg = false,
                });

                // if insert success
                if (await _uow.SaveAsync() > 0)
                {
                    // get orderID
                    var order = await _uow.OrdersRepository.GetLast(filter: order => order.Username == username);
                    // insert into OrderDetail
                    foreach (ProductModel product in listProductModel)
                    {
                        var orderDetailModel = new OrderDetailModel()
                        {
                            OrderId = order.OrderId,
                            ProductId = product.ProductId,
                            Quantity = product.Quantity,
                            Price = product.Price
                        };
                        ;
                        _uow.OrderDetailRepository.Add(_mapper.Map<OrderDetail>(orderDetailModel));
                    }
                    return await _uow.SaveAsync() > 0;
                }
                else
                {
                    throw new ArgumentException("missing listProduct or username");
                }
            }
            return false;
        }
    }
}
