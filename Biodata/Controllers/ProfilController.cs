using Biodata.Models;
using Biodata.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace Biodata.Controllers
{
    public class ProfilController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<ProfilController> _logger;

        public ProfilController(ILogger<ProfilController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string search = "", int currentPage = 1)
        {
            search = string.IsNullOrEmpty(search) ? "" : search.ToLower();
            var ProfilData = new ProfilViewModel();

            var profil = (from pro in _context.Profil
                            where search == "" || pro.NIK.ToLower().Contains(search) || pro.Nama.ToLower().Contains(search)
                            || pro.Alamat.ToLower().Contains(search)
                          select new Profil
                             {
                                 Id = pro.Id,
                                 NIK = pro.NIK,
                                 Nama = pro.Nama,
                                 Alamat = pro.Alamat,
                                 StatusPerkawinan = pro.StatusPerkawinan
                             }
                            );
            int totalRecords = profil.Count();
            int pageSize = 5;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            profil = profil.Skip((currentPage - 1) * pageSize).Take(pageSize);
            ProfilData.Profil = profil;
            ProfilData.CurrentPage = currentPage;
            ProfilData.TotalPages = totalPages;
            ProfilData.Search = search;
            ProfilData.PageSize = pageSize;
            ProfilData.OrderBy = "Id";
            return View(ProfilData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AddProfil()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProfil(Profil profil)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _context.Profil.Add(profil);
                _context.SaveChanges();
                TempData["msg"] = "Berhasil di tambahkan";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["msg"] = "Gagal di tambahkan!!!";
                return View();
            }

        }

        public IActionResult EditProfil(int id)
        {
            var profil = _context.Profil.Find(id);
            return View(profil);
        }

        [HttpPost]
        public IActionResult EditProfil(Profil profil)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _context.Profil.Update(profil);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["msg"] = "Gagal di update!!!";
                return View();
            }

        }

        public IActionResult DeleteProfil(int id)
        {
            try
            {
                var profil = _context.Profil.Find(id);
                if (profil != null)
                {
                    _context.Profil.Remove(profil);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {


            }
            return RedirectToAction("Index");

        }
    }


}