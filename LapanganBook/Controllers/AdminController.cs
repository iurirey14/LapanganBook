// ===== ORANG 4: Dashboard admin + Home =====
using Microsoft.AspNetCore.Authorization; using Microsoft.EntityFrameworkCore;
[Authorize(Roles="Admin")] public class AdminController(AppDbContext db) : Controller {
  public IActionResult Index(){ var all = db.Bookings.Include(b=>b.User).Include(b=>b.Lapangan).OrderByDescending(b=>b.Id).ToList();
    ViewBag.Total=all.Count; ViewBag.Menunggu=all.Count(b=>b.Status=="Menunggu"); ViewBag.Omzet=all.Where(b=>b.Status=="Disetujui").Sum(b=>b.Total); ViewBag.User=db.Users.Count(u=>u.Role=="User"); return View(all); }
  [HttpPost] public IActionResult Status(int id,string status){ var b=db.Bookings.Find(id); if(b!=null){ b.Status=status; db.SaveChanges(); } return RedirectToAction("Index"); }
}
public class HomeController : Controller { public IActionResult Index() => View(); }
