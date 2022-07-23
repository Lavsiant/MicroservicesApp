using AutoMapper;
using Ordering.Application.Interfaces;
using Ordering.Domain.DTO;
using Ordering.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Services
{
    public class OrderReader : IOrderReader
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public OrderReader(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<OrderDTO?> GetOrder(string id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return order != null ? _mapper.Map<OrderDTO>(order) : null;
        }
    }
}
