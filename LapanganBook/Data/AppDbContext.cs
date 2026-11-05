// ===== Iuri: Database + data awal =====
using Microsoft.AspNetCore.Identity; using Microsoft.EntityFrameworkCore;
public class AppDbContext(DbContextOptions<AppDbContext> o) : DbContext(o) {
  public DbSet<User> Users => Set<User>(); public DbSet<Lapangan> Lapangans => Set<Lapangan>(); public DbSet<Booking> Bookings => Set<Booking>(); }
public static class Seed { public static void Run(AppDbContext db, IPasswordHasher<User> h) {
  if(db.Users.Any()) return;
  var a = new User{Nama="Admin",Email="admin@gmail.com",Role="Admin"}; a.PasswordHash = h.HashPassword(a,"admin123"); db.Users.Add(a);
  db.Lapangans.AddRange(
   new Lapangan{Nama="Arena Futsal A",Jenis="Futsal",Harga=120000,Deskripsi="Rumput sintetis, indoor, AC & ruang ganti."},
   new Lapangan{Nama="Arena Futsal B",Jenis="Futsal",Harga=100000,Deskripsi="Lantai vinyl, cocok untuk turnamen kecil."},
   new Lapangan{Nama="Court Badminton 1",Jenis="Badminton",Harga=60000,Deskripsi="Lantai karpet standar BWF, pencahayaan terang."},
   new Lapangan{Nama="Court Badminton 2",Jenis="Badminton",Harga=55000,Deskripsi="Court ekonomis, nyaman untuk latihan harian."});
  db.SaveChanges(); } }
