using Epicode_S7_L5_BackEnd_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static Epicode_S7_L5_BackEnd_Project.Models.Prodotto;

namespace Epicode_S7_L5_BackEnd_Project.Controllers
{
    public class HomeController : Controller
    {
        ModelDbContext db = new ModelDbContext();

        public ActionResult Home()
        {
            return View(db.Prodotto.ToList());
        }

        public ActionResult Login()
        {
            return View();
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

        [HttpPost]
        public ActionResult Login([Bind(Include = "Username,Password")] Utente utente)
        {
            Utente Utente = db.Utente.Where(u => u.Email == utente.Email && u.Password == utente.Password).FirstOrDefault();
            if (Utente != null)
            {
                Session["IdUtente"] = Utente.IdUtente;
                FormsAuthentication.SetAuthCookie(utente.Email, true);
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Exclude = "IdRuolo")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                utente.IdRuolo = 1;
                db.Utente.Add(utente);
                db.SaveChanges();

                Session["IdUtente"] = utente.IdUtente;
                FormsAuthentication.SetAuthCookie(utente.Email, true);

                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult VisualizzaCarrello()
        {
            List<CarrelloItem> carrelloItems = Session["Carrello"] as List<CarrelloItem>;

            if (carrelloItems != null && carrelloItems.Any())
            {
                return View(carrelloItems);
            }
            else
            {
                return View("Home");
            }
        }

        [HttpPost]
        public ActionResult AggiungiAlCarrello(int id, string nome, decimal prezzo, int quantita)
        {
            List<CarrelloItem> carrello = Session["Carrello"] as List<CarrelloItem>;

            if (carrello == null)
            {
                carrello = new List<CarrelloItem>();
            }

            var carrelloItem = carrello.FirstOrDefault(item => item.Prodotto.IdProdotto == id);

            if (carrelloItem != null)
            {
                carrelloItem.Quantita += quantita;
                carrelloItem.Totale = carrelloItem.Prodotto.Prezzo * carrelloItem.Quantita; // Calcola il subtotale
            }
            else
            {
                var nuovoCarrelloItem = new CarrelloItem
                {
                    Prodotto = new Prodotto { IdProdotto = id, Nome = nome, Prezzo = prezzo },
                    Quantita = quantita
                };
                nuovoCarrelloItem.Totale = nuovoCarrelloItem.Prodotto.Prezzo * nuovoCarrelloItem.Quantita; // Calcola il subtotale
                carrello.Add(nuovoCarrelloItem);
            }

            // Calcola il totale sommando i subtotale di tutti gli elementi nel carrello
            decimal totale = carrello.Sum(item => item.Totale);

            Session["Carrello"] = carrello;
            Session["TotaleCarrello"] = totale; // Salva il totale in Session

            return RedirectToAction("Home");
        }

    }
}
