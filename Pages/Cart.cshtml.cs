using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Infrastructure;
using SportsStore.Models;
namespace SportsStore.Pages {
    public class CartModel : PageModel {
        private IStoreRepository repository;
        public CartModel(IStoreRepository repo, Cart cartService) {
            repository = repo;
            Cart = cartService;
        }
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";
        public void OnGet(string returnUrl) {
            ReturnUrl = returnUrl ?? "/";
        }
        public IActionResult OnPost(long productId, string returnUrl) {
            Product? product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null) {
                Cart.AddItem(product, 1);
            }
            return RedirectToPage(new { returnUrl = returnUrl });
        }
        public IActionResult OnPostRemove(long productId, string returnUrl) {
            // 1. Find the product in the current cart lines
            var line = Cart.Lines.First(cl => cl.Product.ProductID == productId);
    
            // 2. Pass that product to the RemoveLine method in your Cart class
            Cart.RemoveLine(line.Product);
    
            // 3. Refresh the page to show the updated cart
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}