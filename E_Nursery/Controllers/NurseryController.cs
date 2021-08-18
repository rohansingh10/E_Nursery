using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Nursery.Models;
using System.Data.Entity;
using System.Collections;
namespace E_Nursery.Controllers
{
    public class NurseryController : Controller
    {
        // GET: Nursery


        public ActionResult InventoryDetails()
        {
            
            OurDbContext db = new OurDbContext();
            int NID = (Session["NurseryID"].ToString() != null) ? Int32.Parse(Session["NurseryID"].ToString()) : 0;
            List<NurseryInventory> inventories = db.NurseryInventories.Where(x=> x.NurseryID == NID ).AsEnumerable().ToList();
            return View(inventories);
        }

        public ActionResult AdminInventoryDetails()
        {
            OurDbContext db = new OurDbContext();
            List<NurseryInventory> inventories = db.NurseryInventories.AsEnumerable().ToList();
            return View(inventories);
        }
        public ActionResult ACreateInventory()
        {
            NurseryInventory inventory = new NurseryInventory();
            //ViewBag.NurseryId = Int32.Parse(Session["NurseryID"].ToString());
            return View(inventory);
        }
        [HttpPost]
        public ActionResult ACreateInventory(NurseryInventory inventory)
        {
            try
            {

                OurDbContext db = new OurDbContext();
                if (ModelState.IsValid)
                {
                    db.NurseryInventories.Add(inventory);
                    db.SaveChanges();
                    List<NurseryInventory> inventories = db.NurseryInventories.AsEnumerable().ToList();
                    //return View("InventoryDetails", inventories);
                    ViewBag.Message = "New Item Has Been Successfully Added";
                    return View("ALoogedIn");
                }
                return View(inventory);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("InventoryError");
            }
        }
        public ActionResult ALoogedIn()
        {
            OurDbContext db = new OurDbContext();
            NurseryAccount n = new NurseryAccount();
            List<NurseryInventory> inventories = db.NurseryInventories.AsEnumerable().ToList();
            ViewBag.Description = "";
            foreach (var item in inventories)
            {
                if (item.stock < 5)
                {
                    n = db.nurseryAccount.Where(x => x.NurseryID == item.NurseryID).FirstOrDefault();
                    string s = n.NurseryName + " Is Running Out of " + item.PlantName + " Plants,";

                    ViewBag.Description = ViewBag.Description + " " + s;

                }

            }
            return View();
        }
        public ActionResult AEditInventory(int id)
        {
            using (OurDbContext db = new OurDbContext())
            {
                NurseryInventory inventory = new NurseryInventory();
                //ViewBag.NurseryId = Int32.Parse(Session["NurseryID"].ToString());
                return View(db.NurseryInventories.Where(x => x.InventoryID == id).FirstOrDefault());
            }

        }
        [HttpPost]
        public ActionResult AEditInventory(NurseryInventory inventory)
        {
            try
            {
                using (OurDbContext db = new OurDbContext())
                {


                    // NurseryInventory inventory2 = db.NurseryInventories.Find(inventory.InventoryID);
                    db.Entry(inventory).State = EntityState.Modified;
                    db.SaveChanges();
                }
                ViewBag.Message = "Item Has Been Successfully Updated";
                return View("ALoogedIn");

            }
            catch
            {
                return View();
            }
        }

        public ActionResult ADeleteInventory(int id)
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.NurseryInventories.Where(x => x.InventoryID == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult ADeleteInventory(int id, FormCollection collection)
        {
            try
            {
                using (OurDbContext db = new OurDbContext())
                {
                    NurseryInventory inventory = db.NurseryInventories.Where(x => x.InventoryID == id).FirstOrDefault();
                    db.NurseryInventories.Remove(inventory);
                    db.SaveChanges();
                }
                ViewBag.Message = "Item Has Been Successfully Deleted";
                return View("ALoogedIn");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CreateInventory()
        {
            NurseryInventory inventory = new NurseryInventory();
            ViewBag.NurseryId = Int32.Parse(Session["NurseryID"].ToString());
            return View(inventory);
        }
        [HttpPost]
        public ActionResult CreateInventory(NurseryInventory inventory)
        {
            try
            {

                OurDbContext db = new OurDbContext();
                if (ModelState.IsValid)
                {
                    db.NurseryInventories.Add(inventory);
                    db.SaveChanges();
                    List<NurseryInventory> inventories = db.NurseryInventories.AsEnumerable().ToList();
                    //return View("InventoryDetails", inventories);
                    ViewBag.Message = "New Item Has Been Successfully Added";
                    return RedirectToRoute(new { controller = "Account", action = "NLoggedIn" });
                }
                return View(inventory);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("InventoryError");
            }
        }
        public ActionResult EditInventory(int id)
        {
            using (OurDbContext db = new OurDbContext())
            {
                NurseryInventory inventory = new NurseryInventory();
                ViewBag.NurseryId = Int32.Parse(Session["NurseryID"].ToString());
                return View(db.NurseryInventories.Where(x => x.InventoryID == id).FirstOrDefault());
            }
              
        }
        [HttpPost]
        public ActionResult EditInventory(NurseryInventory inventory)
        {
            try
            {
                using (OurDbContext db = new OurDbContext())
                {


                   // NurseryInventory inventory2 = db.NurseryInventories.Find(inventory.InventoryID);
                    db.Entry(inventory).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("InventoryDetails");

            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteInventory(int id)
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.NurseryInventories.Where(x => x.InventoryID == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult DeleteInventory(int id,FormCollection collection)
        {
            try
            {
                using (OurDbContext db = new OurDbContext())
                {
                    NurseryInventory inventory= db.NurseryInventories.Where(x=> x.InventoryID == id).FirstOrDefault();
                    db.NurseryInventories.Remove(inventory);
                    db.SaveChanges();
                }
                return RedirectToAction("InventoryDetails");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ANotification()
        {
            OurDbContext db = new OurDbContext();
            NurseryAccount n = new NurseryAccount();
            List<NurseryInventory> inventories = db.NurseryInventories.AsEnumerable().ToList();
            ViewBag.Description = "";
            foreach (var item in inventories)
            {
                if (item.stock < 5)
                {
                    n = db.nurseryAccount.Where(x => x.NurseryID == item.NurseryID).FirstOrDefault();
                    string s = n.NurseryName + " Is Running Out of " + item.PlantName + " Plants,";

                    ViewBag.Description = ViewBag.Description + " " + s;

                }

            }


            return View("ALoogedIn");

        }


        public ActionResult ManageNurseryProfile(int id)
        {
            using (OurDbContext db = new OurDbContext())
            {

                return View(db.nurseryAccount.Where(x => x.NurseryID == id).FirstOrDefault());
            }

        }
        [HttpPost]
        public ActionResult ManageNurseryProfile(NurseryAccount user)
        {
            try
            {
                using (OurDbContext db = new OurDbContext())
                {

                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
                ViewBag.Message = "Profile Updated Succesfully";
                return View("NLoggedIn");
            }
            catch
            {
                return View();
            }
        }














    }
}