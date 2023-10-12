using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Epicode_S7_L5_BackEnd_Project.Models;

namespace Epicode_S7_L5_BackEnd_Project.Controllers
{
    public class UtenteController : Controller
    {
        readonly ModelDbContext db = new ModelDbContext();

        public ActionResult ListaUtente()
        {
            var utente = db.Utente.Include(u => u.Ruolo);
            return View(utente.ToList());
        }

        public ActionResult DettaglioUtente(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utente = db.Utente.Find(id);
            if (utente == null)
            {
                return HttpNotFound();
            }
            return View(utente);
        }

        public ActionResult ModificaUtente(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utente = db.Utente.Find(id);
            if (utente == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRuolo = new SelectList(db.Ruolo, "IdRuolo", "Role", utente.IdRuolo);
            return View(utente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificaUtente([Bind(Include = "IdUtente,Nome,Cognome,Provincia,Citta,Indirizzo,Email,Password,IdRuolo")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaUtente");
            }
            ViewBag.IdRuolo = new SelectList(db.Ruolo, "IdRuolo", "Role", utente.IdRuolo);
            return View(utente);
        }

        public ActionResult EliminaUtente(int id)
        {
            Utente utente = db.Utente.Find(id);
            db.Utente.Remove(utente);
            db.SaveChanges();
            return RedirectToAction("ListaUtente");
        }
    }
}
