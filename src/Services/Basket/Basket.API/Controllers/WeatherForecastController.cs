using AutoMapper;
using Basket.API.ViewModels;
using Basket.Services.Interfaces;
using Basket.Services.Models.ServiceDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public BasketController(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCartViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartViewModel>> GetBasket(string userName)
        {
            var result = await _basketService.GetBasket(userName);

            if (result.Succeeded)
            {
                return Ok(result.Model);
            }
            
            return BadRequest(result.Error);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCartViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartViewModel>> UpdateBasket([FromBody] ShoppingCartViewModel basketViewModel)
        {
            var basket = _mapper.Map<ShoppingCartDTO>(basketViewModel);
            var result = await _basketService.UpdateBasket(basket);

            if (result.Succeeded)
            {
                return Ok(result.Model);
            }

            return BadRequest(result.Error);
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            var result = await _basketService.DeleteBasket(userName);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result.Error);
        }
    }
}