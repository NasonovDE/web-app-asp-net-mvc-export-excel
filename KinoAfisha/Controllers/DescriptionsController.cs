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
    public class DescriptionsController : Controller
    {
     //   private readonly string _key = "123456Qq";

        [HttpGet]
        public ActionResult Index()
        {
            var db = new KinoAfishaContext();
            var descriptions = db.Descriptions.ToList();

            return View(descriptions);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var description = new Description();
            return View(description);
        }

        [HttpPost]
        public ActionResult Create(Description model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db = new KinoAfishaContext();
            //if (model.Key != _key)
            //    ModelState.AddModelError("Key", "Ключ для создания/изменения записи указан не верно");
            if (!ModelState.IsValid)
                return View(model);
            db.Descriptions.Add(model);
            db.SaveChanges();

            return RedirectPermanent("/Descriptions/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new KinoAfishaContext();
            var description = db.Descriptions.FirstOrDefault(x => x.Id == id);
            if (description == null)
                return RedirectPermanent("/Descriptions/Index");

            db.Descriptions.Remove(description);
            db.SaveChanges();

            return RedirectPermanent("/Descriptions/Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new KinoAfishaContext();
            var description = db.Descriptions.FirstOrDefault(x => x.Id == id);
            if (description == null)
                return RedirectPermanent("/Descriptions/Index");

            return View(description);
        }

        [HttpPost]
        public ActionResult Edit(Description model)
        {
            var db = new KinoAfishaContext();
            var description = db.Descriptions.FirstOrDefault(x => x.Id == model.Id);
            if (description == null)
                ModelState.AddModelError("Id", "Дисциплина не найдена");
            //if (model.Key != _key)
            //    ModelState.AddModelError("Key", "Ключ для создания/изменения записи указан не верно");
            if (!ModelState.IsValid)
                return View(model);

            MappingDiscipline(model, description, db);

            db.Entry(description).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/Descriptions/Index");
        }

        private void MappingDiscipline(Description sourse, Description destination, KinoAfishaContext db)
        {
            destination.Name = sourse.Name;
            destination.DescriptionCinemaCorporation = sourse.DescriptionCinemaCorporation;
            destination.DescriptionAllFilms = sourse.DescriptionAllFilms;
            destination.DescriptionAllActors = sourse.DescriptionAllActors;
          //  destination.Key = sourse.Key;


        }
        [HttpGet]
        public ActionResult GetXlsx()
        {
            var db = new KinoAfishaContext();
            var xlsx = db.Descriptions.ToXlsx();

            return File(xlsx.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Description.xlsx");
        }
    }
}