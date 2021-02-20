using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelephoneBook.Models;
using TelephoneBook.Models.BasicModels;
using TelephoneBook.Repository;

namespace TelephoneBook.Controllers
{
    public class DictController : Controller
    {
        private static TelephoneRepository repository = new TelephoneRepository();

        public async Task<ActionResult> Index()
        {
            return View(await repository.GetTelephoneNotes());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TelephoneNote telephoneNote)
        {
            try
            {
                await repository.AddTelephoneNote(telephoneNote);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Update(int id)
        {
            return View((await repository.GetTelephoneNotes()).FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(TelephoneNote telephoneNote)
        {
            try
            {
                await repository.UpdateTelephoneNote(telephoneNote);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            return View((await repository.GetTelephoneNotes()).FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(TelephoneNote telephoneNote)
        {
            try
            {
                await repository.RemoveTelephoneNote(telephoneNote);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ProcessError404()
        {
            return View("Error404",new Error404Model() { });
        }
    }
}
