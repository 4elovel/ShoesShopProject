using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ShoesShopProject.Models;
public class ApplicationUser : IdentityUser
{
    public int Id { get; set; }
    public string? Adress { get; set; }
    [MaxLength(255)]
    public string? FirstName { get; set; }
    [MaxLength(255)]
    public string? LastName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<Good> WishList { get; set; } = [];
    public List<Order> Orders { get; set; } = [];

}
