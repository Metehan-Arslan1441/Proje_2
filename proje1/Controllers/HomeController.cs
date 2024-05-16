using Microsoft.AspNetCore.Mvc;
using proje1.Models;
using proje1.Models.Domain;
using System.Diagnostics;

namespace proje1.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _ctx;

        public HomeController(DatabaseContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ekleSayfa()
        {
            return View();
        }

        public IActionResult listele()
        {

            var kisiler = (from i in _ctx.persons
                           orderby i.Soyad ascending
                           select i).ToList();
            return View("listele", kisiler);

        }


        public IActionResult Deletekayýt(int ID)
        {
            try
            {
                var person = _ctx.persons.Find(ID);
                if (person != null)
                {
                    _ctx.persons.Remove(person);
                    _ctx.SaveChanges();

                }
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("listele");
        }
        string? eski_resim;
        [HttpPost]
        
        public IActionResult Guncelle(Person person,IFormFile formfile)
        {
            ModelState.Remove("formfile");

            if (formfile != null)
            {
                string eski_resim = person.resim;
                var path_sil = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\resim", eski_resim);
                if (System.IO.File.Exists(path_sil))
                {
                    System.IO.File.Delete(path_sil);    
                }
            }
        

      
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _ctx.persons.Update(person);
                _ctx.SaveChanges();
                return RedirectToAction("listele");
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could not update!!!";
                return View();
            }
        }

        public IActionResult arama()
        {
            return View();
        }

        [HttpPost]
        public IActionResult arama(String Ad)
        {
            if (Ad != null)
            {
                var ogrenciarama = (from i in _ctx.persons
                                    where i.Ad.StartsWith(Ad)
                                    select i).ToList();
                if (ogrenciarama.Count > 0)
                {
                    return View("listele", ogrenciarama);
                }
                TempData["msg"] = "Baþarýsýz!!!";
                return View();
            }
            else
            {
                TempData["msg"] = "lütfen isim yazýnýz!!!";
                return View();
            }

        }

        public IActionResult listelecard()
        {
            var listelenecekler = _ctx.persons.ToList();
            return View(listelenecekler);
        }

        public IActionResult zdenaya()
        {
            var kisiler = (from i in _ctx.persons
                           orderby i.Ad descending
                           select i).ToList();
            return View("listele", kisiler);
        }

        public IActionResult yaþagore()
        {
            var kisiler = (from i in _ctx.persons
                           orderby i.yaþ descending
                           select i).ToList();
            return View("listele", kisiler);
        }



        public IActionResult þikayet()
        {
            return View();
        }


        public IActionResult adem()
        {
            return View();
        }



        string? randomName;
        [HttpPost]
        public IActionResult ekleSayfa(Person person, IFormFile formFile)
        {
            ModelState.Remove("formfile");
            if (formFile != null)
            {
                var extent = Path.GetExtension(formFile.FileName);
                randomName = ($"{Guid.NewGuid()}{extent}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\resim", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }
            }
            else
            {
                TempData["msg"] = "resim gir";
                return View(person);
            }
            try
            {
                person.resim = randomName;
                _ctx.persons.Add(person);
                _ctx.SaveChanges();
                TempData["msg"] = "kayýt baþarýlý";
                return RedirectToAction("ekleSayfa");
            }
            catch (Exception ex)
            {
                TempData["msg"] = "kayýt baþarýsýz";
                return View(person);
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
