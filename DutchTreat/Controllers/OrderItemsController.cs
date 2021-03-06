﻿namespace DutchTreat.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ViewModels;

    [Route("/api/orders/{orderId}/items")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemsController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderItemsController> _logger;

        public OrderItemsController(
            IDutchRepository repository,
            IMapper mapper,
            ILogger<OrderItemsController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var username = User.Identity.Name;
            var order = _repository.GetOrderById(username, orderId);
            if (order != null)
            {
                return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var username = User.Identity.Name;

            var order = _repository.GetOrderById(username, orderId);
            var item = order?.Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
            }

            return NotFound();
        }
    }
}
