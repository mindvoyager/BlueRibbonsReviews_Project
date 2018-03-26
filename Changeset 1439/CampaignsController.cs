using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlueRibbonsReview.Models;

namespace BlueRibbonsReview.Controllers
{
    public class CampaignsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Campaigns
        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.StartDateDescSortParm = "startDate_desc";
            ViewBag.StartDateSortParm = "startDate";
            
            ViewBag.CloseDateSortParm = "closeDate";
           
            ViewBag.NameSortParm = "name";
            ViewBag.NameDescSortParm = "name_desc";
            
            ViewBag.SalePriceSortParm = "salePrice";
            ViewBag.SalePriceDescSortParm = "salePrice_desc";
            var campaigns = from c in db.Campaigns.Include(c => c.Seller)
                            select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                campaigns = campaigns.Where(c => c.Name.Contains(searchString)
                    || c.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "startDate_desc":
                    campaigns = campaigns.OrderByDescending(c => c.StartDate);
                    break;
                case "startDate":
                    campaigns = campaigns.OrderBy(c => c.StartDate);
                    break;
                case "closeDate":
                    campaigns = campaigns.OrderBy(c => c.CloseDate);
                    break;
                case "name":
                    campaigns = campaigns.OrderBy(c => c.Name);
                    break;
                case "name_desc":
                    campaigns = campaigns.OrderByDescending(c => c.Name);
                    break;
                case "salePrice":
                    campaigns = campaigns.OrderBy(c => c.SalePrice);
                    break;
                case "salePrice_desc":
                    campaigns = campaigns.OrderByDescending(c => c.SalePrice);
                    break;
                default:
                    campaigns = campaigns.OrderByDescending(c => c.StartDate);
                    break;
            }

            return View(campaigns.ToList());
        }

        // GET: Campaigns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = db.Campaigns.Find(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }
        

        // GET: Campaigns/Create
        public ActionResult Create()
        {
            ViewBag.SellerID = new SelectList(db.Sellers, "SellerID", "FirstName");
            return View();
        }

        // POST: Campaigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CampaignID,ASIN,Name,Category,ImageUrL,Description,RetailPrice,SalePrice,StartDate,CloseDate,ExpireDate,CalculatedDiscount,VendorsPurchaseInstructions,SellerID")] Campaign campaign)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    campaign.VendorsPurchaseURL = String.Format("https://www.amazon.com/dp/{0}",campaign.ASIN);
                    campaign.OpenCampaign = false;
                    db.Campaigns.Add(campaign);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem perists, see your system administrator.");
            }

            ViewBag.SellerID = new SelectList(db.Sellers, "SellerID", "FirstName", campaign.SellerID);
            return View(campaign);
        }

        // GET: Campaigns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = db.Campaigns.Find(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            ViewBag.SellerID = new SelectList(db.Sellers, "SellerID", "FirstName", campaign.SellerID);
            return View(campaign);
        }

        // POST: Campaigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var campaignToUpdate = db.Campaigns.Find(id);
            if (TryUpdateModel(campaignToUpdate, "",
                new string[] { "ASIN", "Name", "OpenCampaign", "Category", "ImageURL",
                "Description", "RetailPrice", "SalePrice", "StartDate", "CloseDate",
                "ExpireDate", "CalculatedDiscount", "VendorsPurchaseInstructions",
                "VendorsPurchaseURL", "SellerID"}))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(campaignToUpdate);
        }

        // I commented out the below code so I could use it as a reference as I 
        // built out the new method- Tyler B.

        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CampaignID,ASIN,Name,OpenCampaign,Category,ImageUrL,Description,RetailPrice,SalePrice,StartDate,CloseDate,ExpireDate,CalculatedDiscount,VendorsPurchaseInstructions,VendorsPurchaseURL,SellerID")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campaign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SellerID = new SelectList(db.Sellers, "SellerID", "FirstName", campaign.SellerID);
            return View(campaign);
        }
        */

        // GET: Campaigns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = db.Campaigns.Find(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // POST: Campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Campaign campaign = db.Campaigns.Find(id);
            db.Campaigns.Remove(campaign);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
