using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShoesShopProject.Models;
[Index(nameof(Login), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Phone]
    public string? PhoneNumber { get; set; }
    public string? Adress { get; set; }
    [MaxLength(255)]
    public string FirstName { get; set; } = null!;
    [MaxLength(255)]
    public string LastName { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<Good> WishList { get; set; } = [];
    public List<Order> Orders { get; set; } = [];


}
