using Microsoft.AspNetCore.Authorization;

public class LapanganController(AppDbContext db) : Controller
{
    public IActionResult Index(string? jenis)
    {
        var lapangans = db.Lapangans.AsQueryable();

        if (!string.IsNullOrEmpty(jenis))
        {
            lapangans = lapangans.Where(lapangan => lapangan.Jenis == jenis);
        }

        ViewBag.Jenis = jenis;
        return View(lapangans.ToList());
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Kelola()
    {
        return View(db.Lapangans.ToList());
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Form(int? id)
    {
        if (id is null)
        {
            return View(new Lapangan());
        }

        var lapangan = db.Lapangans.Find(id);
        return lapangan is null ? NotFound() : View(lapangan);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Form(Lapangan model)
    {
        if (model.Id == 0)
        {
            db.Lapangans.Add(model);
        }
        else
        {
            db.Lapangans.Update(model);
        }

        db.SaveChanges();
        return RedirectToAction(nameof(Kelola));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Hapus(int id)
    {
        var lapangan = db.Lapangans.Find(id);
        if (lapangan is not null)
        {
            db.Lapangans.Remove(lapangan);
            db.SaveChanges();
        }

        return RedirectToAction(nameof(Kelola));
    }
}
