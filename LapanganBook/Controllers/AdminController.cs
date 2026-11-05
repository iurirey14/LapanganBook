// ===== ORANG 4: Dashboard admin (grafik + filter) + Home =====
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
[Authorize(Roles = "Admin")]
public class AdminController(AppDbContext db) : Controller
{
    public IActionResult Index(string? status)
    {
        var all = db.Bookings.Include(b => b.User).Include(b => b.Lapangan).OrderByDescending(b => b.Id).ToList();
        ViewBag.Total = all.Count;
        ViewBag.Menunggu = all.Count(b => b.Status == "Menunggu");
        ViewBag.Omzet = all.Where(b => b.Status == "Disetujui").Sum(b => b.Total);
        ViewBag.User = db.Users.Count(u => u.Role == "User");
        ViewBag.Status = status;
        ViewBag.Harian = Enumerable.Range(0, 7).Select(i => DateTime.Today.AddDays(i - 6))
          .Select(d => (Tgl: d.ToString("dd/MM"), N: all.Count(b => b.Tanggal == d && b.Status != "Dibatalkan" && b.Status != "Ditolak"))).ToList();
        var tampil = string.IsNullOrEmpty(status) ? all : all.Where(b => b.Status == status).ToList();
        return View(tampil);
    }
    [HttpPost] public IActionResult Status(int id, string status) { var b = db.Bookings.Find(id); if (b != null) { b.Status = status; db.SaveChanges(); } return RedirectToAction("Index"); }
}
public class HomeController : Controller { public IActionResult Index() => View(); }