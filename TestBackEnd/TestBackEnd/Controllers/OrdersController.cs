using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TestBackEnd.Models;

namespace TestBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/orders (Chỉ Admin mới xem được tất cả đơn)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }

        // 2. GET: api/orders/user/{userId} (Xem lịch sử đơn hàng của User)
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUser(int userId)
        {
            var currentUserId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (currentUserId != userId && currentUserRole != "Admin")
            {
                return Forbid();
            }
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
            return Ok(orders);
        }

        // 3. POST: api/orders (Tạo đơn hàng mới)
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            var currentUserId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            if (order.UserId != currentUserId)
            {
                return Forbid();
            }
            order.OrderDate = DateTime.Now;
            order.Status = "Pending";
            order.CreatedAt = DateTime.Now;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrdersByUser), new { userId = order.UserId }, order);
        }
    }
}
