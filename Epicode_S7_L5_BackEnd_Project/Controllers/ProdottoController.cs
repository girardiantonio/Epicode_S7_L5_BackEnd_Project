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
    public class ProdottoController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        public ActionResult ListaProdotto()
        {
            return View(db.Prodotto.ToList());
        }

        public ActionResult DettagliProdotto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotto prodotto = db.Prodotto.Find(id);
            if (prodotto == null)
            {
                return HttpNotFound();
            }
            return View(prodotto);
        }

        public ActionResult CreaProdotto()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreaProdotto([Bind(Include = "IdProdotto,Nome,FotoUrl,Prezzo,TempoConsegna,Ingredienti")] Prodotto prodotto)
        {
            if (ModelState.IsValid)
            {
                db.Prodotto.Add(prodotto);
                db.SaveChanges();
                return RedirectToAction("ListaProdotto");
            }

            return View(prodotto);
        }

        public ActionResult ModificaProdotto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotto prodotto = db.Prodotto.Find(id);
            if (prodotto == null)
            {
                return HttpNotFound();
            }
            return View(prodotto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificaProdotto([Bind(Include = "IdProdotto,Nome,FotoUrl,Prezzo,TempoConsegna,Ingredienti")] Prodotto prodotto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prodotto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaProdotto");
            }
            return View(prodotto);
        }

        public ActionResult EliminaProdotto(int id)
        {
            Prodotto prodotto = db.Prodotto.Find(id);
            db.Prodotto.Remove(prodotto);
            db.SaveChanges();
            return RedirectToAction("ListaProdotto");
        }
    }
}
