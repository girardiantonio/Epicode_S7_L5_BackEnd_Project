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
    [Authorize]
    public class OrdineController : Controller
    {
        readonly ModelDbContext db = new ModelDbContext();

        public ActionResult ListaOrdine()
        {
            int? userId = (int?)Session["UserId"];

            if (userId.HasValue)
            {
                if(User.IsInRole("Admin"))
                {
                    var ordineAdmin = db.Ordine.Include(o => o.Utente);
                    return View(ordineAdmin.ToList());
                }
                else
                {
                    var ordineUtente = db.Ordine.Include(o => o.Utente).Where(o => o.IdUtente == userId.Value).ToList();
                    return View(ordineUtente);
                }
            }
            return View(new List<Ordine>());
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
        public ActionResult ModificaOrdine(Ordine ordine)
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


        public ActionResult ControlloOrdine() 
        { 
            return View(); 
        }

        [HttpPost]
        public JsonResult OrdiniEvasi()
        {
            {
                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(1);

                List<Ordine> ordine = db.Ordine.Where(a => a.Evaso == true && a.DataOrdine >= today && a.DataOrdine < tomorrow).ToList();
                List<OrdineEvaso> ordineEvaso = new List<OrdineEvaso>();

                foreach (var o in ordine)
                {
                    ordineEvaso.Add(new OrdineEvaso
                    {
                        Id = o.IdUtente,
                        Nome = o.Utente.Nome,
                        TotaleOrdiniOggi = ordine.Count()
                    });
                }

                return Json(ordineEvaso);
            }
        }

        [HttpPost]
        public JsonResult OrdineByData(DateTime inputVal)
        {
            DateTime tomorrow = inputVal.AddDays(1);

            List<Ordine> ordine = db.Ordine.Where(a => a.Evaso == true && a.DataOrdine >= inputVal && a.DataOrdine < tomorrow).ToList();

            decimal TotaleIncasso = ordine.Sum(o => o.Importo);

            return Json(TotaleIncasso);
        }

    }
}
