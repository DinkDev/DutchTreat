namespace DutchTreat.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ViewModels;

    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly UserManager<StoreUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(
            IDutchRepository repository, 
            UserManager<StoreUser> userManager,
            IMapper mapper, 
            ILogger<OrdersController> logger)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                var username = User.Identity.Name;

                var results = _repository.GetAllOrdersByUser(username, includeItems);
                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get failed: {ex}");
                return BadRequest("Failed to get orders"); // sets status code 400
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var username = User.Identity.Name;

                var order = _repository.GetOrderById(username, id);
                if (order != null) return Ok(_mapper.Map<Order, OrderViewModel>(order));
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get failed: {ex}");
                return BadRequest("Failed to get orders"); // sets status code 400
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderViewModel model)
        {
            // add item to the database
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);

                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    newOrder.User = currentUser;

                    _repository.AddEntity(newOrder);
                    if (_repository.SaveAll())
                    {
                        return Created($"/api/orders/{newOrder.Id}", _mapper.Map<Order, OrderViewModel>(newOrder));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save new order {model}: {ex}");
            }

            return BadRequest($"Failed to save new order {model}");
        }
    }
}
