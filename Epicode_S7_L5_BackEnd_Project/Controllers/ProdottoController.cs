using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Epicode_S7_L5_BackEnd_Project.Models;

namespace Epicode_S7_L5_BackEnd_Project.Controllers
{
    [Authorize]
    public class ProdottoController : Controller
    {
        readonly ModelDbContext db = new ModelDbContext();

        public ActionResult ListaProdotto()
        {
            return View(db.Prodotto.ToList());
        }

        [AllowAnonymous]
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
        public ActionResult CreaProdotto(Prodotto prodotto, HttpPostedFileBase FotoUrl)
        {

            string nomeFile = FotoUrl.FileName;
            string pathToSave = Path.Combine(Server.MapPath("~/Content/imgs"), nomeFile);
            FotoUrl.SaveAs(pathToSave);

            prodotto.FotoUrl = nomeFile;

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
            TempData["fotoNome"] = prodotto.FotoUrl;
            return View(prodotto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificaProdotto(Prodotto prodotto, HttpPostedFileBase FotoUrl)
        {
            if (FotoUrl != null)
            {
                string nomeFile = FotoUrl.FileName;
                string pathToSave = Path.Combine(Server.MapPath("~/Content/imgs"), nomeFile);
                FotoUrl.SaveAs(pathToSave);

                prodotto.FotoUrl = nomeFile;
            }
            else
            {
                prodotto.FotoUrl = TempData["fotoNome"].ToString();
            }

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
