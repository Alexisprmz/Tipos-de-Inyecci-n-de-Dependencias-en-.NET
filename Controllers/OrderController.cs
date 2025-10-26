using Microsoft.AspNetCore.Mvc;
using OrdersManager.Services; 

namespace OrdersManager.Controllers
{
    [ApiController]
    [Route("api")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _transientService;
        private readonly IOrderService _scopedService;
        private readonly IOrderService _singletonService;

        public OrdersController(
            [FromKeyedServices("transient")] IOrderService transientService,
            [FromKeyedServices("scoped")] IOrderService scopedService,
            [FromKeyedServices("singleton")] IOrderService singletonService)
        {
            _transientService = transientService;
            _scopedService = scopedService;
            _singletonService = singletonService;
        }

        private IActionResult GetServiceStatus(IOrderService service, string serviceType)
        {
            return Ok(new
            {
                ServiceType = serviceType,
                InstanceId = service.GetInstanceId(),
                OrdersCount = service.GetOrdersCount(),
                Orders = service.GetOrders()
            });
        }

        [HttpGet("transient/status")]
        public IActionResult GetTransientStatus() => GetServiceStatus(_transientService, "Transient");

        [HttpPost("transient/add")]
        public IActionResult AddTransientOrder([FromBody] Order order)
        {
            _transientService.AddOrder(order);
            return GetTransientStatus();
        }

        [HttpGet("scoped/status")]
        public IActionResult GetScopedStatus() => GetServiceStatus(_scopedService, "Scoped");

        [HttpPost("scoped/add")]
        public IActionResult AddScopedOrder([FromBody] Order order)
        {
            _scopedService.AddOrder(order);
            return GetScopedStatus();
        }

        [HttpGet("singleton/status")]
        public IActionResult GetSingletonStatus() => GetServiceStatus(_singletonService, "Singleton");

        [HttpPost("singleton/add")]
        public IActionResult AddSingletonOrder([FromBody] Order order)
        {
            _singletonService.AddOrder(order);
            return GetSingletonStatus();
        }
    }
}
