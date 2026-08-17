using BulkyBook.Business.Services.IServices;
using BulkyBook.Models;
using BulkyBook.Utiltiy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;
        private const int PageSize = 8;

        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) page = 1;
            var pagedProducts = await _productService.GetPagedProductsAsync(page, PageSize, includeCategory: true);
            return View(pagedProducts);
        }

        public HomeController(IProductService productService, IShoppingCartService shoppingCartService) 
        {
            _productService = productService;
            _shoppingCartService = shoppingCartService;
        }
        public async Task<IActionResult> Details(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId , includeCategory:true);
            if (product == null)
            {
                return NotFound();
            }

            ShoppingCart cart = new()
            {
                Product = product,
                Count = 1,
                ProductId = productId
            };
            return View(cart);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            shoppingCart.ApplicationUserId = userId;
            await _shoppingCartService.AddToCartAsync(shoppingCart);
            var count = await _shoppingCartService.GetCartCountAsync(userId);
            HttpContext.Session.SetInt32(SD.SessionCart, count);
            TempData["success"] = "Item added to cart";
            return RedirectToAction("Index");
        }


    }
}