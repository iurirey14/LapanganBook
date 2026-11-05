// ===== Iuri: Login / Register =====
using System.Security.Claims; using Microsoft.AspNetCore.Authentication; using Microsoft.AspNetCore.Identity;
public class AccountController(AppDbContext db, IPasswordHasher<User> h) : Controller {
  public IActionResult Login() => View();
  [HttpPost] public async Task<IActionResult> Login(string email, string password) {
    var u = db.Users.FirstOrDefault(x => x.Email == email);
    if(u == null || h.VerifyHashedPassword(u,u.PasswordHash,password) == PasswordVerificationResult.Failed){ ViewBag.Err="Email atau password salah"; return View(); }
    await Masuk(u); return u.Role=="Admin" ? RedirectToAction("Index","Admin") : RedirectToAction("Index","Lapangan"); }
  public IActionResult Register() => View();
  [HttpPost] public async Task<IActionResult> Register(string nama, string email, string password) {
    if(db.Users.Any(x => x.Email == email)){ ViewBag.Err="Email sudah terdaftar"; return View(); }
    var u = new User{Nama=nama,Email=email}; u.PasswordHash = h.HashPassword(u,password); db.Users.Add(u); db.SaveChanges();
    await Masuk(u); return RedirectToAction("Index","Lapangan"); }
  async Task Masuk(User u){ var id = new ClaimsIdentity(new[]{ new Claim(ClaimTypes.NameIdentifier,u.Id.ToString()), new Claim(ClaimTypes.Name,u.Nama), new Claim(ClaimTypes.Role,u.Role) },"Cookies");
    await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(id)); }
  public async Task<IActionResult> Logout(){ await HttpContext.SignOutAsync("Cookies"); return RedirectToAction("Index","Home"); }
}
