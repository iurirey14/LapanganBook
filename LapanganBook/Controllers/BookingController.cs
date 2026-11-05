// ===== ORANG 3: Booking + cek jadwal bentrok =====
using System.Security.Claims; using Microsoft.AspNetCore.Authorization; using Microsoft.EntityFrameworkCore;
[Authorize] public class BookingController(AppDbContext db) : Controller {
  int Uid => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
  public IActionResult Create(int id){ var l=db.Lapangans.Find(id); return l==null ? NotFound() : View(l); }
  [HttpPost] public IActionResult Create(int id, DateTime tanggal, int jam, int durasi){
    var l = db.Lapangans.Find(id); if(l==null) return NotFound(); string? err=null; var t=tanggal.Date;
    if(t < DateTime.Today) err="Tanggal tidak boleh sudah lewat.";
    else if(jam+durasi > 23) err="Booking maksimal sampai jam 23:00.";
    else if(db.Bookings.Any(b => b.LapanganId==id && b.Tanggal==t && b.Status!="Dibatalkan" && b.Status!="Ditolak" && b.JamMulai < jam+durasi && jam < b.JamMulai+b.Durasi)) err="Jadwal bentrok, pilih jam lain.";
    if(err!=null){ ViewBag.Err=err; return View(l); }
    db.Bookings.Add(new Booking{UserId=Uid,LapanganId=id,Tanggal=t,JamMulai=jam,Durasi=durasi,Total=l.Harga*durasi}); db.SaveChanges();
    return RedirectToAction("Saya"); }
  public IActionResult Saya() => View(db.Bookings.Include(b=>b.Lapangan).Where(b=>b.UserId==Uid).OrderByDescending(b=>b.Id).ToList());
  [HttpPost] public IActionResult Batal(int id){ var b=db.Bookings.FirstOrDefault(x=>x.Id==id&&x.UserId==Uid); if(b!=null&&b.Status=="Menunggu"){ b.Status="Dibatalkan"; db.SaveChanges(); } return RedirectToAction("Saya"); }
}
