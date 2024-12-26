using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Login Page
    public IActionResult Login() => View();

    // GET: Registration Page
    public IActionResult Register() => View();

    // POST: Login User
    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ViewBag.Error = "Username and Password cannot be empty.";
            return View();
        }

        var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

        if (user != null)
        {
            // Set session data
            HttpContext.Session.SetString("Role", user.Role);
            HttpContext.Session.SetString("Username", user.Username);
            return RedirectToAction("Index", "Card");
        }

        ViewBag.Error = "Invalid credentials";
        return View();
    }

    // POST: Register New User (Admin-Only)
    [HttpPost]
    public IActionResult Register(User user)
    {
        // Validate Username
        if (string.IsNullOrEmpty(user.Username) || user.Username.Length < 3 || user.Username.Length > 20 || !Regex.IsMatch(user.Username, @"^[a-zA-Z0-9]+$"))
        {
            ViewBag.UsernameError = "Username must be between 3 and 20 characters and contain only alphanumeric characters.";
            return View(user);
        }

        // Validate Password
        if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 6 || !Regex.IsMatch(user.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$"))
        {
            ViewBag.PasswordError = "Password must be at least 6 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.";
            return View(user);
        }

        if (_context.Users.Any(u => u.Username == user.Username))
        {
            ViewBag.Error = "Username already exists.";
            return View(user);
        }

        // Set role based on session (only Admin can assign roles)
        if (HttpContext.Session.GetString("Role") != "Admin")
        {
            user.Role = "User"; // Default to "User" for self-registration
        }

        _context.Users.Add(user);
        _context.SaveChanges();

        return RedirectToAction("Login");
    }
}
