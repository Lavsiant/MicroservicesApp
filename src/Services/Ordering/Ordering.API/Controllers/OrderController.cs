using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.ViewModels;
using Ordering.Application.Interfaces;
using Ordering.Domain.DTO;
using System.Net;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderWriter _orderWriter;
        private readonly IOrderReader _orderReader;

        public OrderController(IOrderWriter orderWriter, IMapper mapper, IOrderReader orderReader)
        {
            _orderWriter = orderWriter;
            _mapper = mapper;
            _orderReader = orderReader;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(OrderViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderViewModel>> GetOrder(string id)
        {
            var orderDTO =  await _orderReader.GetOrder(id);
            if(orderDTO == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OrderViewModel>(orderDTO));
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> AddOrder(AddOrderViewModel order)
        {
            var orderDTO = _mapper.Map<OrderDTO>(order);
            var id = await _orderWriter.CreateOrder(orderDTO);

            return Ok(id);
        }

        [HttpPost]
        [Route ("GetProductByCategory")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> CancelOrder(string id)
        {
            var result = await _orderWriter.CancelOrder(id);
            
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateOrderInfo(UpdateOrderViewModel order)
        {
            var orderDTO = _mapper.Map<OrderDTO>(order);
            var result = await _orderWriter.UpdateOrder(orderDTO);

            return Ok(result);
        }
    }
}
