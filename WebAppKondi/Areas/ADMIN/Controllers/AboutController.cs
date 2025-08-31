using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using WebAppCoreMVC.Models;
using WebAppCoreMVC.Services;

namespace WebAppCoreMVC.Areas.ADMIN.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }


        // GET: AboutController
        public ActionResult Index()
        {
            IList<About> abouts = _aboutService.ObtenirListeElement();
            return View(abouts);
        }

        // GET: AboutController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AboutController/Create
        public ActionResult Create()
        {
            return View("create");
        }

        // POST: AboutController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View("create");
            }

            try
            {
                // Accéder aux champs du formulaire
                string libelle = collection["Libelle"];
                string description = collection["Description"];
                string icone = collection["Icone"];

                _aboutService.EnregistrerElement(new About { Libelle = libelle, Description = description, Icone = icone});
                TempData["Message"] = "Nouvel enrégistrement réussie avec succès !";
            }
            catch
            {
                throw;
            }
            return RedirectToAction("Index");
        }

        // GET: AboutController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            About about = _aboutService.ObtenirElement(id);
            if (about == null)
            {
                return NotFound();
            }

            return View("edit", about);
        }

        // POST: AboutController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            try
            {
                var abt = _aboutService.ObtenirElement(id);

                abt.Libelle = collection["Libelle"];
                abt.Description = collection["Description"];
                abt.Icone = collection["Icone"];
                _aboutService.MiseAjoutElement(abt);

                TempData["Message"] = "Mise à jour réussie avec succès !";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }
        }

        // GET: AboutController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            About about = _aboutService.ObtenirElement(id);
            if (about == null)
            {
                return NotFound();
            }

            return View("delete", about);
        }

        // POST: AboutController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _aboutService.SupprimerElement(_aboutService.ObtenirElement(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
