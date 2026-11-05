// ===== iuri: Models =====
public class User { public int Id{get;set;} public string Nama{get;set;}=""; public string Email{get;set;}=""; public string PasswordHash{get;set;}=""; public string Role{get;set;}="User"; }
public class Lapangan { public int Id{get;set;} public string Nama{get;set;}=""; public string Jenis{get;set;}="Futsal"; public decimal Harga{get;set;} public string Deskripsi{get;set;}=""; }
public class Booking { public int Id{get;set;} public int UserId{get;set;} public User? User{get;set;} public int LapanganId{get;set;} public Lapangan? Lapangan{get;set;}
  public DateTime Tanggal{get;set;} public int JamMulai{get;set;} public int Durasi{get;set;} public decimal Total{get;set;} public string Status{get;set;}="Menunggu"; }
