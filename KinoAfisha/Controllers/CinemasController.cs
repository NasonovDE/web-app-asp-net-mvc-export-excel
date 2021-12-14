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
    public class CinemasController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new KinoAfishaContext();
            var cinemas = db.Cinemas.ToList();

            return View(cinemas);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var cinema = new Cinema();
            return View(cinema);
        }

        [HttpPost]
        public ActionResult Create(Cinema model)
        {
            var db = new KinoAfishaContext();

            if (!ModelState.IsValid)
            {
                var cinemas = db.Cinemas.ToList();
                ViewBag.Create = model;
                return View("Index", cinemas);

            }
            db.Cinemas.Add(model);
            db.SaveChanges();

            return RedirectPermanent("/Cinemas/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new KinoAfishaContext();
            var cinema = db.Cinemas.FirstOrDefault(x => x.Id == id);
            if (cinema == null)
                return RedirectPermanent("/Cinemas/Index");

            db.Cinemas.Remove(cinema);
            db.SaveChanges();

            return RedirectPermanent("/Cinemas/Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new KinoAfishaContext();
            var cinema = db.Cinemas.FirstOrDefault(x => x.Id == id);
            if (cinema == null)
                return RedirectPermanent("/Cinemas/Index");

            return View(cinema);
        }

        [HttpPost]
        public ActionResult Edit(Cinema model)
        {
            var db = new KinoAfishaContext();
            var cinema = db.Cinemas.FirstOrDefault(x => x.Id == model.Id);
            if (cinema == null)
                ModelState.AddModelError("Id", "Кинотеатр не найден");

            if (!ModelState.IsValid)
                return View(model);

            if (!ModelState.IsValid)
            {
                var cinemas = db.Cinemas.ToList();
                ViewBag.Create = model;
                return View("Index", cinemas);
            }
            MappingFilm(model, cinema, db);

            db.Entry(cinema).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/Cinemas/Index");
        }

        private void MappingFilm(Cinema sourse, Cinema destination, KinoAfishaContext db)
        {
          
            destination.CinemaPlace = sourse.CinemaPlace;
            destination.NumberOfBilets = sourse.NumberOfBilets;
            destination.QRcode = sourse.QRcode;

        }

        [HttpGet]
        public ActionResult GetXlsx()
        {
            var db = new KinoAfishaContext();
            var xlsx = db.Cinemas.ToXlsx();

            return File(xlsx.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Cinemas.xlsx");
        }
    }
    }

