using Microsoft.AspNetCore.Mvc;
public class CardController : Controller
{
    private readonly ApplicationDbContext _context;

    public CardController(ApplicationDbContext context)
    {
        _context = context;
    }

    // READ: View all cards (Accessible by both Admins and Users)
    public IActionResult Index()
    {
        var cards = _context.Cards.ToList();
        return View(cards);
    }

    // CREATE: Admins only
    [HttpGet]
    public IActionResult Create()
    {
        if (HttpContext.Session.GetString("Role") != "Admin")
        {
            return RedirectToAction("Index"); // Redirect non-admin users
        }
        return View();
    }

    [HttpPost]
public IActionResult Create(Card card, IFormFile ImageFile)
{
    if (ImageFile != null && ImageFile.Length > 0)
    {
        // Define the path where the file will be saved
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        // Ensure the folder exists
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        // Save the file to the server
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            ImageFile.CopyTo(stream);
        }

        // Save the relative path in the database
        card.ImageUrl = $"/images/{uniqueFileName}";
    }
    else
    {
        ModelState.AddModelError("ImageFile", "Image upload is required.");
        return View(card);
    }

    _context.Cards.Add(card);
    _context.SaveChanges();

    return RedirectToAction("Index");
}

    // EDIT: Admins only
    [HttpGet]
    public IActionResult Edit(int id)
    {
        if (HttpContext.Session.GetString("Role") != "Admin")
        {
            return RedirectToAction("Index");
        }

        var card = _context.Cards.Find(id);
        if (card == null)
        {
            return NotFound();
        }
        return View(card);
    }

    [HttpPost]
    public IActionResult Edit(Card card)
    {
        if (HttpContext.Session.GetString("Role") != "Admin")
        {
            return RedirectToAction("Index");
        }

        if (ModelState.IsValid)
        {
            _context.Cards.Update(card);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(card);
    }

    // DELETE: Admins only
    [HttpGet]
    public IActionResult Delete(int id)
    {
        if (HttpContext.Session.GetString("Role") != "Admin")
        {
            return RedirectToAction("Index");
        }

        var card = _context.Cards.Find(id);
        if (card == null)
        {
            return NotFound();
        }
        return View(card);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        if (HttpContext.Session.GetString("Role") != "Admin")
        {
            return RedirectToAction("Index");
        }

        var card = _context.Cards.Find(id);
        if (card != null)
        {
            _context.Cards.Remove(card);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
