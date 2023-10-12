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
    public class OrdineController : Controller
    {
        readonly ModelDbContext db = new ModelDbContext();

        public ActionResult ListaOrdine()
        {
            var ordine = db.Ordine.Include(o => o.Utente);
            return View(ordine.ToList());
        }

        public ActionResult ModificaOrdine(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordine ordine = db.Ordine.Find(id);
            if (ordine == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUtente = new SelectList(db.Utente, "IdUtente", "Nome", ordine.IdUtente);
            return View(ordine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificaOrdine([Bind(Include = "IdOrdine,IdUtente,DataOrdine,Importo,IndirizzoConsegna,NoteSpeciali,Evaso")] Ordine ordine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUtente = new SelectList(db.Utente, "IdUtente", "Nome", ordine.IdUtente);
            return View(ordine);
        }

        public ActionResult EliminaOrdine(int id)
        {
            Ordine ordine = db.Ordine.Find(id);
            db.Ordine.Remove(ordine);
            db.SaveChanges();
            return RedirectToAction("ListaProdotto");
        }
    }
}
