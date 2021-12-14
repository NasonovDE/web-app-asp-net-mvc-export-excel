using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using KinoAfisha.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ClosedXML.Excel;
using WebAppAspNetMvcCodeFirst.Extensions;


namespace KinoAfisha.Controllers
{
    public class KinosController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new KinoAfishaContext();
            var kino = db.Kinos.ToList();


            return View(kino);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var kino = new Kino();
            return View(kino);

        }

        [HttpPost]
        public ActionResult Create(Kino model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db = new KinoAfishaContext();
            model.CreateAt = DateTime.Now;
            model.NextArrivalDate = DateTime.Now;
            if (model.FilmIds != null && model.FilmIds.Any())
            {
                var film = db.Films.Where(s => model.FilmIds.Contains(s.Id)).ToList();
                model.Films = film;
            }
            if (model.CinemaIds != null && model.CinemaIds.Any())
            {
                var cinema = db.Cinemas.Where(s => model.CinemaIds.Contains(s.Id)).ToList();
                model.Cinemas = cinema;
            }

            if (!ModelState.IsValid)
            {
                var kinos = db.Kinos.ToList();
                ViewBag.Create = model;
                return View("Index", kinos);

            }


            db.Kinos.Add(model);
            db.SaveChanges();


            return RedirectPermanent("/Kinos/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new KinoAfishaContext();
            var kino = db.Kinos.FirstOrDefault(x => x.Id == id);
            if (kino == null)
                return RedirectPermanent("/Kinos/Index");

            db.Kinos.Remove(kino);
            db.SaveChanges();

            return RedirectPermanent("/Kinos/Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new KinoAfishaContext();
            var kino = db.Kinos.FirstOrDefault(x => x.Id == id);

            if (kino == null)
                return RedirectPermanent("/Kinos/Index");

            return View(kino);
        }

        [HttpPost]
        public ActionResult Edit(Kino model)
        {

            var db = new KinoAfishaContext();
            var kino = db.Kinos.FirstOrDefault(x => x.Id == model.Id);

            

            if (kino == null)
            {
                ModelState.AddModelError("Id", "кино не найдено");
            }
            if (!ModelState.IsValid)
            {
                var kinos = db.Kinos.ToList();
                ViewBag.Create = model;
                return View("Index", kinos);

            }
            if (!ModelState.IsValid)
                return View(model);
            
            MappingKino(model, kino, db);
            
            db.Entry(kino).State = EntityState.Modified;
            db.SaveChanges();


            return RedirectPermanent("/Kinos/Index");
        }


        private void MappingKino(Kino sourse, Kino destination, KinoAfishaContext db)
        {

            destination.Price = sourse.Price;
            destination.NextArrivalDate = sourse.NextArrivalDate;
            destination.KinoTime = sourse.KinoTime;
            destination.FilmIds = sourse.FilmIds;




            if (destination.Films != null)
                destination.Films.Clear();

            if (sourse.FilmIds != null && sourse.FilmIds.Any())
                destination.Films = db.Films.Where(s => sourse.FilmIds.Contains(s.Id)).ToList();

            if (destination.Cinemas != null)
                destination.Cinemas.Clear();

            if (sourse.CinemaIds != null && sourse.CinemaIds.Any())
                destination.Cinemas = db.Cinemas.Where(s => sourse.CinemaIds.Contains(s.Id)).ToList();


        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var db = new KinoAfishaContext();
            var kino = db.Kinos.FirstOrDefault(x => x.Id == id);
            if (kino == null)
                return RedirectPermanent("/Kinos/Index");

            return View(kino);
        }

        [HttpGet]
        public FileResult GetXlsx(Kino model)
        {
            var db = new KinoAfishaContext();
            var values = db.Kinos.ToList();
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Data");


            ws.Cell("A" + 1).Value = "Id";
            ws.Cell("B" + 1).Value = "Название фильма";
            ws.Cell("C" + 1).Value = "Цена";
            ws.Cell("D" + 1).Value = "Место показа";
            ws.Cell("E" + 1).Value = "Дата сеанса";
            ws.Cell("F" + 1).Value = "Время сеанса";

            int row = 2;
            foreach (var value in values)
            {
                ws.Cell("A" + row).Value = value.Id;
                ws.Cell("B" + row).Value = string.Join(", ", value.Films.Select(x => $"{x.NameFilm}"));
             
                ws.Cell("C" + row).Value = value.Price;
                ws.Cell("D" + row).Value = string.Join(", ", value.Cinemas.Select(x => $"{x.CinemaPlace}"));
                    
                ws.Cell("E" + row).Value = value.NextArrivalDate;
                ws.Cell("F" + row).Value = value.KinoTime;
                row++;
            };
            var rngHead = ws.Range("A1:F" + 1);
            rngHead.Style.Fill.BackgroundColor = XLColor.AshGrey;

            var rngTable = ws.Range("A1:F" + 10);
            rngTable.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            rngTable.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

            ws.Columns().AdjustToContents();



            using (MemoryStream stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Afisha.xlsx");
            }
        }
    }
}
