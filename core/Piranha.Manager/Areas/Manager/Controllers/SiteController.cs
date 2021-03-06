/*
 * Copyright (c) 2017 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * https://github.com/piranhacms/piranha.core
 * 
 */

using Microsoft.AspNetCore.Mvc;
using Piranha.Areas.Manager.Models;

namespace Piranha.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class SiteController : ManagerAreaControllerBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="api">The current api</param>
        public SiteController(Api api) : base(api) { }
        
        /// <summary>
        /// Gets the list view for the current sites.
        /// </summary>
        [Route("manager/sites")]
        public IActionResult List() {
            return View(SiteListModel.Get(api));
        }

        [Route("manager/site/add")]
        public IActionResult Add() {
            return View("Edit", new SiteEditModel());
        }

        [Route("manager/site/{id}")]
        public IActionResult Edit(string id) {
            return View(SiteEditModel.GetById(api, id));
        }

        [Route("manager/site/save")]
        public IActionResult Save(SiteEditModel model) {
            if (model.Save(api)) {
                SuccessMessage("The site has been saved.");
                return RedirectToAction("Edit", new { id = model.Site.Id });
            } else {
                ErrorMessage("The site could not be saved.", false);
                return View("Edit", model);
            }
        }

        [Route("manager/site/delete/{id}")]
        public IActionResult Delete(string id) {
            var site = api.Sites.GetById(id);

            if (site != null) {
                if (!site.IsDefault) {
                    api.Sites.Delete(id);
                    SuccessMessage("The site has been deleted.");
                } else {
                    ErrorMessage("Can't delete the default site.");
                }
            } else {
                ErrorMessage("This site could not be found.");
            }
            return RedirectToAction("List");
        }
    }
}
