using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using KinoAfisha.Models;
using WebAppAspNetMvcCodeFirst.Extensions;



namespace KinoAfisha.Controllers
{
    public class FilmsController : Controller
    {
      //  private readonly string _key = "123456Qq";
        [HttpGet]
        public ActionResult Index()
        {
            var db = new KinoAfishaContext();
            var films = db.Films.ToList();

            return View(films);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var film = new Film();
            return View(film);
        }

        [HttpPost]
        public ActionResult Create(Film model)
        {
            if (!ModelState.IsValid)
                return View(model);
   
            var db = new KinoAfishaContext();

            if (model.FormatIds != null && model.FormatIds.Any())
            {
                var nationality = db.Formats.Where(s => model.FormatIds.Contains(s.Id)).ToList();
                model.Formats = nationality;
            }

            if (model.FilmCoverFile != null)
            {
                var data = new byte[model.FilmCoverFile.ContentLength];
                model.FilmCoverFile.InputStream.Read(data, 0, model.FilmCoverFile.ContentLength);

                model.FilmCover = new FilmCover()
                {
                    Guid = Guid.NewGuid(),
                    DateChanged = DateTime.Now,
                    Data = data,
                    ContentType = model.FilmCoverFile.ContentType,
                    FileName = model.FilmCoverFile.FileName
                };
            }

            //if (model.Key != _key)
            //    ModelState.AddModelError("Key", "Ключ для создания/изменения записи указан не верно");
            
            if (!ModelState.IsValid)
            {
                var films = db.Films.ToList();
                ViewBag.Create = model;
                return View("Index", films);

            }


            db.Films.Add(model);
            db.SaveChanges();

            return RedirectPermanent("/Films/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new KinoAfishaContext();
            var film = db.Films.FirstOrDefault(x => x.Id == id);
            if (film == null)
                return RedirectPermanent("/Films/Index");

            db.Films.Remove(film);
            db.SaveChanges();

            return RedirectPermanent("/Films/Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new KinoAfishaContext();
            var film = db.Films.FirstOrDefault(x => x.Id == id);
            if (film == null)
                return RedirectPermanent("/Films/Index");

            return View(film);
        }

        [HttpPost]
        public ActionResult Edit(Film model)
        {
            var db = new KinoAfishaContext();
            var film = db.Films.FirstOrDefault(x => x.Id == model.Id);
            if (film == null)
                ModelState.AddModelError("Id", "Фильм не найден");


            //if (model.Key != _key)
            //    ModelState.AddModelError("Key", "Ключ для создания/изменения записи указан не верно");

            
            if (!ModelState.IsValid)
                return View(model);

            if (!ModelState.IsValid)
            {
                var films = db.Films.ToList();
                ViewBag.Create = model;
                return View("Index", films);

            }
            MappingFilm(model, film, db);

            db.Entry(film).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/Films/Index");
        }

        private void MappingFilm(Film sourse, Film destination, KinoAfishaContext db)
        {
          
            destination.NameFilm = sourse.NameFilm;
            destination.FilmYears = sourse.FilmYears;
            destination.FilmAllActors = sourse.FilmAllActors;
            destination.FilmDescription = sourse.FilmDescription;
            destination.FilmDop = sourse.FilmDop;
            destination.Key = sourse.Key;

            if (destination.Formats != null)
                destination.Formats.Clear();

            if (sourse.FormatIds != null && sourse.FormatIds.Any())
                destination.Formats = db.Formats.Where(s => sourse.FormatIds.Contains(s.Id)).ToList();


            if (sourse.FilmCoverFile != null)
            {
                var image = db.FilmCovers.FirstOrDefault(x => x.Id == sourse.Id);
                if (image != null)
                    db.FilmCovers.Remove(image);

                var data = new byte[sourse.FilmCoverFile.ContentLength];
                sourse.FilmCoverFile.InputStream.Read(data, 0, sourse.FilmCoverFile.ContentLength);

                destination.FilmCover = new FilmCover()
                {
                    Guid = Guid.NewGuid(),
                    DateChanged = DateTime.Now,
                    Data = data,
                    ContentType = sourse.FilmCoverFile.ContentType,
                    FileName = sourse.FilmCoverFile.FileName
                };
            }
        }
        [HttpGet]
        public ActionResult GetImage(int id)
        {
            var db = new KinoAfishaContext();
            var image = db.FilmCovers.FirstOrDefault(x => x.Id == id);
            if (image == null)
            {
                FileStream fs = System.IO.File.OpenRead(Server.MapPath(@"~/Content/Images/not-foto.png"));
                byte[] fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int)fs.Length);
                fs.Close();

                return File(new MemoryStream(fileData), "image/jpeg");
            }

            return File(new MemoryStream(image.Data), image.ContentType);
        }


        [HttpPost]
        public void Upload()  //Here just store 'Image' in a folder in Project Directory 
                              //  name 'UplodedFiles'
        {
            foreach (string file in Request.Files)
            {
                var postedFile = Request.Files[file];
                postedFile.SaveAs(Server.MapPath("~/UploadedFiles/") + Path.GetFileName(postedFile.FileName));
            }
        }
        public ActionResult List() //I retrive Images List by using this Controller
        {
            var uploadedFiles = new List<FilmCover>();

            var files = Directory.GetFiles(Server.MapPath("~/UploadedFiles"));

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);

                var picture = new FilmCover() { FileName = Path.GetFileName(file) };
                picture.Size = fileInfo.Length;

                picture.Path = ("~/UploadedFiles/") + Path.GetFileName(file);
                uploadedFiles.Add(picture);
            }

            return View(uploadedFiles);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var db = new KinoAfishaContext();
            var kino = db.Films.FirstOrDefault(x => x.Id == id);
            if (kino == null)
                return RedirectPermanent("/Films/Index");

            return View(kino);
        }

        [HttpGet]
        public ActionResult GetXlsx()
        {
            var db = new KinoAfishaContext();
            var xlsx = db.Films.ToXlsx();

            return File(xlsx.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Films.xlsx");
        }
    }

}