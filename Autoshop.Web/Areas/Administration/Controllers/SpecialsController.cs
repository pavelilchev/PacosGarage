namespace Autoshop.Web.Areas.Admin.Controllers
{
    using Autoshop.Services;
    using Autoshop.Web.Areas.Administration.Controllers;
    using Autoshop.Web.Areas.Administration.Models.Specials;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class SpecialsController : BaseAdminController
    {
        ISpecialsService specials;

        public SpecialsController(ISpecialsService specials)
        {
            this.specials = specials;
        }

        public async Task<IActionResult> Index()
        {
            var specials = await this.specials.All();          

            return View(specials);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpecialFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var id = await this.specials.Add(model.Title, model.Description, model.DaysValid);

            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> Details(int id)
        {
            var special = await this.specials.Find(id);
            if (special == null)
            {
                return NotFound();
            }

            return View(special);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var special = await this.specials.Find(id);
            if (special == null)
            {
                return NotFound();
            }
            
            return View(new SpecialFormViewModel
            {
                Title = special.Title,
                Description = special.Description,
                DaysValid = special.DaysValid
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SpecialFormViewModel model)
        {
            var special = await this.specials.Find(id);
            if (special == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.specials.Update(id, model.Title, model.Description, model.DaysValid);

            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var special = await this.specials.Find(id);
            if (special == null)
            {
                return NotFound();
            }

            return View(special);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var special = await this.specials.Find(id);
            if (special == null)
            {
                return NotFound();
            }

            await this.specials.Remove(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
