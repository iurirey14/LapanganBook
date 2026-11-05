// ===== ORANG 2: Katalog + CRUD Lapangan (admin) =====
using Microsoft.AspNetCore.Authorization;
public class LapanganController(AppDbContext db) : Controller {
  public IActionResult Index(string? jenis){ var q = db.Lapangans.AsQueryable(); if(!string.IsNullOrEmpty(jenis)) q = q.Where(x => x.Jenis==jenis); ViewBag.Jenis=jenis; return View(q.ToList()); }
  [Authorize(Roles="Admin")] public IActionResult Kelola() => View(db.Lapangans.ToList());
  [Authorize(Roles="Admin")] public IActionResult Form(int? id) => View(id==null ? new Lapangan() : db.Lapangans.Find(id));
  [Authorize(Roles="Admin"), HttpPost] public IActionResult Form(Lapangan m){ if(m.Id==0) db.Lapangans.Add(m); else db.Lapangans.Update(m); db.SaveChanges(); return RedirectToAction("Kelola"); }
  [Authorize(Roles="Admin"), HttpPost] public IActionResult Hapus(int id){ var l=db.Lapangans.Find(id); if(l!=null){ db.Lapangans.Remove(l); db.SaveChanges(); } return RedirectToAction("Kelola"); }
}
