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
    public IActionResult Create(Card card)
    {
        if (HttpContext.Session.GetString("Role") != "Admin")
        {
            return RedirectToAction("Index");
        }

        if (ModelState.IsValid)
        {
            _context.Cards.Add(card);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(card);
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
