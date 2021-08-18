using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Nursery.Models;
using System.Data.Entity;
using System.Collections;
using PagedList.Mvc;
using PagedList;

namespace E_Nursery.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            using ( OurDbContext db = new OurDbContext())
            {
                return View(db.userAccount.ToList());
            }


        }
        public ActionResult Index2()
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.nurseryAccount.ToList());
            }
        }



        public ActionResult LoginAs()
        {
            return View();
        }
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    db.userAccount.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = account.FirstName + " " + account.LastName + " successfully registered";
            }
            return View();
        }
        public ActionResult NRegister()
        {

            return View();
        }
        [HttpPost]
        public ActionResult NRegister(NurseryAccount account)
        {
            if (ModelState.IsValid)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    db.nurseryAccount.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = "Nursery has been successfully registered";
            }
            return View();
        }
        public ActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult login(UserAccount user)
        {
            using (OurDbContext db = new OurDbContext())
            {
                var usr = db.userAccount.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
                if (usr != null)
                {
                    if (usr.Status.Equals("Approved"))
                    {
                        Session["UserID"] = usr.UserID.ToString();
                        Session["Username"] = usr.Username.ToString();
                        return RedirectToAction("LoggedIn");
                    }
                    else if (usr.Status.Equals("Applied"))
                    {
                        return View("loginWait");
                    }
                    else
                    {

                      
                        return View("loginDenied");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is wrong");
                }
            }
            return View();
        }
        public ActionResult NLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Nlogin(NurseryAccount user)
        {
            using (OurDbContext db = new OurDbContext())
            {
                var usr = db.nurseryAccount.FirstOrDefault(u => u.LoginID == user.LoginID && u.Password == user.Password);
                if (usr != null)
                {
                    if (usr.Status.Equals("Approved"))
                    {
                        Session["NurseryID"] = usr.NurseryID.ToString();
                        Session["LoginID"] = usr.LoginID.ToString();
                        return RedirectToAction("NLoggedIn");
                    }
                    else if (usr.Status.Equals("Applied"))
                    {
                        return View("loginWait");
                    }
                    else
                    {

                        return View("loginDenied");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "LoginID or Password is wrong");
                }
            }
            return View();
        }
        public ActionResult LoggedIn()
        {
            if (Session["UserID"] != null)
            {
                OurDbContext db = new OurDbContext();
                int p = Int32.Parse(Session["UserID"].ToString());
                UserAccount a = db.userAccount.Where(x => x.UserID == p).FirstOrDefault();
                List<NurseryInventory> inventories = db.NurseryInventories.AsEnumerable().ToList();
                NurseryAccount n = new NurseryAccount();
                ViewBag.Description = "";
                foreach (var item in inventories)
                {
                    if (item.discount > 0)
                    {
                        n = db.nurseryAccount.Where(x => x.NurseryID == item.NurseryID).FirstOrDefault();
                        string s = n.NurseryName + " Is Giving " + item.discount + "% Discount On " + item.PlantName + " Plants,";
                        ViewBag.Description = ViewBag.Description + " " + s;
                    }

                }
                ViewBag.Data = a.FirstName;
                return View();
            }

            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult NLoggedIn()
        {
            if (Session["LoginID"] != null)
            {
                OurDbContext db = new OurDbContext();
                int p = Int32.Parse(Session["NurseryID"].ToString());
                NurseryAccount a = db.nurseryAccount.Where(x => x.NurseryID == p ).FirstOrDefault();
                ViewBag.Data = a.NurseryName;
                return View();
            }

            else
            {
                return RedirectToAction("NLogin");
            }
        }


        public ActionResult ALogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Alogin(AdminAccount user)
        {
            using (OurDbContext db = new OurDbContext())
            {
                var usr = db.adminAccount.FirstOrDefault(u => u.LoginID == user.LoginID && u.Password == user.Password);
                if (usr != null)
                {
                    Session["AdminID"] = usr.AdminID.ToString();
                    Session["LoginID"] = usr.LoginID.ToString();
                    //return View("ALoogedIn");
                    return RedirectToAction("ALoogedIn");
                }
                else
                {
                    ModelState.AddModelError("", "LoginID or Password is wrong");
                }
            }
            return View();
        }


        public ActionResult Delete(int id)
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.nurseryAccount.Where(x => x.NurseryID == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            using (OurDbContext db = new OurDbContext())
            {
                NurseryAccount nursery = db.nurseryAccount.Where(x => x.NurseryID == id).FirstOrDefault();
                db.nurseryAccount.Remove(nursery);
                db.SaveChanges();
            }
            return View();
        }
        public ActionResult Deny(int id)
        {
            OurDbContext db = new OurDbContext();

            UserAccount user = db.userAccount.Where(x => x.UserID == id).FirstOrDefault();
            user.Status = "Rejected";
            db.SaveChanges();

            return View("Index", db.userAccount.ToList());
        }

        public ActionResult ApproveN(int id)
        {
            OurDbContext db = new OurDbContext();

            NurseryAccount user = db.nurseryAccount.Where(x => x.NurseryID == id).FirstOrDefault();
            user.Status = "Approved";
            db.SaveChanges();

            return View("Index2", db.nurseryAccount.ToList());
        }

        public ActionResult DenyN(int id)
        {
            OurDbContext db = new OurDbContext();

            NurseryAccount user = db.nurseryAccount.Where(x => x.NurseryID == id).FirstOrDefault();
            user.Status = "Rejected";
            db.SaveChanges();

            return View("Index2", db.nurseryAccount.ToList());
        }
        public ActionResult Approve(int id)
        {
            OurDbContext db = new OurDbContext();
            UserAccount user = db.userAccount.Where(x => x.UserID == id).FirstOrDefault();
            user.Status = "Approved";
            db.SaveChanges();
            return View("Index", db.userAccount.ToList());
        }

        public ActionResult UserPass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserPass(UserAccount user)
        {
            OurDbContext db = new OurDbContext();

            var usr = db.userAccount.FirstOrDefault(u => u.PasswordRecoveryQuestion == user.PasswordRecoveryQuestion && u.Username == user.Username);
            if (usr != null)
            {
                return RedirectToAction("ChangeUserPassword", new { id = usr.UserID });
            }

            else
            {
                ViewBag.Message = "User Not Found, Please Try Again";
                return View("loginError");
            }
        }
        public ActionResult ChangeUserPassword(int id)
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.userAccount.Where(x => x.UserID == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult ChangeUserPassword(int id,UserAccount user)
        {
            OurDbContext db = new OurDbContext();
            UserAccount usr = db.userAccount.Where(x => x.UserID == id).FirstOrDefault();
            usr.Password = user.Password;
            usr.ConfirmPassword = user.ConfirmPassword;
            db.SaveChanges();

            ViewBag.Message = "Your Password Has Been Changed, Login Again";
            return View("Login");
        }


        public ActionResult NurseryPass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NurseryPass(NurseryAccount user)
        {
            OurDbContext db = new OurDbContext();

            var usr = db.nurseryAccount.FirstOrDefault(u => u.PasswordRecoveryQuestion == user.PasswordRecoveryQuestion && u.LoginID == user.LoginID);
            if (usr != null)
            {
                return RedirectToAction("ChangeNurseryPassword", new { id = usr.NurseryID });
            }

            else
            {
                ViewBag.Message = "Nursery Not Found,Please Try Again";
                return View("loginError");
            }
        }
        public ActionResult ChangeNurseryPassword(int id)
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.nurseryAccount.Where(x => x.NurseryID == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult ChangeNurseryPassword(int id, NurseryAccount user)
        {
            OurDbContext db = new OurDbContext();
            NurseryAccount usr = db.nurseryAccount.Where(x => x.NurseryID == id).FirstOrDefault();
            usr.Password = user.Password;
            usr.ConfirmPassword = user.ConfirmPassword;
            db.SaveChanges();

            ViewBag.Message = "Your Password Has Been Changed, Login Again";
            return View("NLogin");
        }

        public ActionResult loginWait()
        {
            return View();
        }

        public ActionResult loginDenied()
        {
            return View();
        }

        public ActionResult Spring(string s,int? i)
        {
            OurDbContext db = new OurDbContext();

            List<NurseryInventory> inventories = db.NurseryInventories.Where(x => x.season == s).AsEnumerable().ToList();


            return View(inventories.ToPagedList(i ?? 1, 2));

        }

        public ActionResult SpringAvailableNursery(string s)
        {
            ViewBag.Data = s;
            OurDbContext db = new OurDbContext();
            var list1 = db.nurseryAccount.ToList();
            var list2 = db.NurseryInventories.ToList();
            ArrayList arl = new ArrayList();
            arl.Add(list1);
            arl.Add(list2);
            return View(arl);
        }


        public ActionResult Nearby(int id)
        {
            OurDbContext db = new OurDbContext();
            UserAccount usr = db.userAccount.Where(x => x.UserID == id).FirstOrDefault();
            return View(db.nurseryAccount.Where(x => x.Pincode == usr.Pincode).ToList());
        }

        public ActionResult Winter(string s, int? i)
        {
            OurDbContext db = new OurDbContext();

            List<NurseryInventory> inventories = db.NurseryInventories.Where(x => x.season == s).AsEnumerable().ToList();


            return View(inventories.ToPagedList(i ?? 1, 2));
        }
        public ActionResult WinterAvailableNursery(string s)
        {
            ViewBag.Data = s;
            OurDbContext db = new OurDbContext();
            var list1 = db.nurseryAccount.ToList();
            var list2 = db.NurseryInventories.ToList();
            ArrayList arl = new ArrayList();
            arl.Add(list1);
            arl.Add(list2);
            return View(arl);
        }
        public ActionResult Summer(string s, int? i)
        {
            OurDbContext db = new OurDbContext();

            List<NurseryInventory> inventories = db.NurseryInventories.Where(x => x.season == s).AsEnumerable().ToList();


            return View(inventories.ToPagedList(i ?? 1,2));
        }
        public ActionResult SummerAvailableNursery(string s)
        {
            ViewBag.Data = s;
            OurDbContext db = new OurDbContext();
            //var res = (from t1 in db.nurseryAccount join t2 in db.NurseryInventories on t1.NurseryID equals t2.NurseryID where( t2.season == "Summer" && t2.PlantName == s) select t1);
            var list1 = db.nurseryAccount.ToList();
            var list2 = db.NurseryInventories.ToList();
            ArrayList arl = new ArrayList();
            arl.Add(list1);
            arl.Add(list2);
            return View(arl);
        }

        public ActionResult Fall(string s,int? i)
        {
            OurDbContext db = new OurDbContext();

            List<NurseryInventory> inventories = db.NurseryInventories.Where(x => x.season == s).AsEnumerable().ToList();
            return View(inventories.ToPagedList(i ?? 1, 2));
        }

        public ActionResult FallAvailableNursery(string s)
        {
            ViewBag.Data = s;
            OurDbContext db = new OurDbContext();
            var list1 = db.nurseryAccount.ToList();
            var list2 = db.NurseryInventories.ToList();
            ArrayList arl = new ArrayList();
            arl.Add(list1);
            arl.Add(list2);
            return View(arl);
        }


        public ActionResult Nonseasonal(string s, int? i)
        {
            OurDbContext db = new OurDbContext();

            List<NurseryInventory> inventories = db.NurseryInventories.Where(x => x.season == s).AsEnumerable().ToList();
            return View(inventories.ToPagedList(i ?? 1, 2));
        }

        public ActionResult NonseasonalAvailableNursery(string s)
        {
            ViewBag.Data = s;
            OurDbContext db = new OurDbContext();
            var list1 = db.nurseryAccount.ToList();
            var list2 = db.NurseryInventories.ToList();
            ArrayList arl = new ArrayList();
            arl.Add(list1);
            arl.Add(list2);
            return View(arl);
        }





























        public ActionResult ManageUserProfile(int id)
        {
            using (OurDbContext db = new OurDbContext())
            {
                
                return View(db.userAccount.Where(x => x.UserID == id).FirstOrDefault());
            }

        }
        [HttpPost]
        public ActionResult ManageUserProfile(UserAccount user)
        {
            try
            {
                using (OurDbContext db = new OurDbContext())
                {


                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("LoggedIn");
            }
            catch
            {
                return View();
            }
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
        public ActionResult changed()
        {

            return View();
        }



        public ActionResult A_variety(string s)
        {
            OurDbContext db = new OurDbContext();

            List<NurseryInventory> inventories = db.NurseryInventories.Where(x => x.variety == s).AsEnumerable().ToList();


            return View(inventories);
        }

        public ActionResult B_variety(string s)
        {
            OurDbContext db = new OurDbContext();

            List<NurseryInventory> inventories = db.NurseryInventories.Where(x => x.variety == s).AsEnumerable().ToList();


            return View(inventories);
        }

        public ActionResult C_variety(string s)
        {
            OurDbContext db = new OurDbContext();

            List<NurseryInventory> inventories = db.NurseryInventories.Where(x => x.variety == s).AsEnumerable().ToList();


            return View(inventories);
        }

        public ActionResult D_variety(string s)
        {
            OurDbContext db = new OurDbContext();

            List<NurseryInventory> inventories = db.NurseryInventories.Where(x => x.variety == s).AsEnumerable().ToList();


            return View(inventories);
        }

        public ActionResult A_varietyAvailableNursery(string s)
        {
            ViewBag.Data = s;
            OurDbContext db = new OurDbContext();
            var list1 = db.nurseryAccount.ToList();
            var list2 = db.NurseryInventories.ToList();
            ArrayList arl = new ArrayList();
            arl.Add(list1);
            arl.Add(list2);
            return View(arl);
        }


        public ActionResult B_varietyAvailableNursery(string s)
        {
            ViewBag.Data = s;
            OurDbContext db = new OurDbContext();
            var list1 = db.nurseryAccount.ToList();
            var list2 = db.NurseryInventories.ToList();
            ArrayList arl = new ArrayList();
            arl.Add(list1);
            arl.Add(list2);
            return View(arl);
        }


        public ActionResult C_varietyAvailableNursery(string s)
        {
            ViewBag.Data = s;
            OurDbContext db = new OurDbContext();
            var list1 = db.nurseryAccount.ToList();
            var list2 = db.NurseryInventories.ToList();
            ArrayList arl = new ArrayList();
            arl.Add(list1);
            arl.Add(list2);
            return View(arl);
        }

        public ActionResult D_varietyAvailableNursery(string s)
        {
            ViewBag.Data = s;
            OurDbContext db = new OurDbContext();
            var list1 = db.nurseryAccount.ToList();
            var list2 = db.NurseryInventories.ToList();
            ArrayList arl = new ArrayList();
            arl.Add(list1);
            arl.Add(list2);
            return View(arl);
        }
















        public ActionResult Buy(int id)
        {
            OurDbContext db = new OurDbContext();
            NurseryInventory inventory = db.NurseryInventories.Where(x => x.InventoryID == id).FirstOrDefault();
            if (inventory.stock != 0)
            {
                inventory.stock = inventory.stock - 1;
                db.SaveChanges();
                ViewBag.Message = "Your Order Has Been Placed,Keep Shopping!";
                return View("LoggedIn");
            }
            else
            {
                ViewBag.Message = "The Plant You Are Looking For Is Currently Out Of Stock";
                return View("LoggedIn");
            }

        }



        public ActionResult Logout()
        {
            Session["UserID"] = null;
            Session.Abandon();
            ViewBag.Message = ("Successfully Logged out");
            return View("Login");
        }
        public ActionResult NLogout()
        {
            Session["NurseryID"] = null;
            Session.Abandon();
            ViewBag.Message = ("Successfully Logged out");
            return View("NLogin");
        }
        public ActionResult ALogout()
        {
            Session["AdminID"] = null;
            Session.Abandon();
            ViewBag.Message = ("Successfully Logged out");
            return View("ALogin");
        }


        public ActionResult ViewFeed()
        {
            OurDbContext db = new OurDbContext();
            return View(db.feedback.ToList());
        }

        public ActionResult GiveFeed()
        {
            Feedback f = new Feedback();
            //ViewBag.NurseryId = Int32.Parse(Session["NurseryID"].ToString());
            return View(f);
        }
        [HttpPost]
        public ActionResult GiveFeed(Feedback f)
        {
            try
            {

                OurDbContext db = new OurDbContext();
                if (ModelState.IsValid)
                {
                    db.feedback.Add(f);
                    db.SaveChanges();
                    List<Feedback> feedbacks = db.feedback.AsEnumerable().ToList();
                    //return View("InventoryDetails", inventories);
                    ViewBag.Message = "Your Feedback Has Been Submitted";
                    return View("LoggedIn");
                }
                return View(f);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("InventoryError");
            }
        }


        public ActionResult AViewFeed()
        {
            OurDbContext db = new OurDbContext();
            return View(db.feedback.ToList());
        }


        public ActionResult ADeleteFeed(int id)
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.feedback.Where(x => x.FeedbackID == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult ADeleteFeed(int id, FormCollection collection)
        {
            try
            {
                using (OurDbContext db = new OurDbContext())
                {
                    Feedback inventory = db.feedback.Where(x => x.FeedbackID == id).FirstOrDefault();
                    db.feedback.Remove(inventory);
                    db.SaveChanges();
                }
                ViewBag.Message = "Feedback Has Been Successfully Deleted";
                return View("ALoogedIn");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult CreateArticle()
        {
            Article inventory = new Article();
            //ViewBag.NurseryId = Int32.Parse(Session["NurseryID"].ToString());
            return View(inventory);
        }
        [HttpPost]
        public ActionResult CreateArticle(Article inventory)
        {
            try
            {

                OurDbContext db = new OurDbContext();
                if (ModelState.IsValid)
                {
                    db.article.Add(inventory);
                    db.SaveChanges();
                    List<Article> articlels = db.article.AsEnumerable().ToList();
                    //return View("InventoryDetails", inventories);
                    ViewBag.Message = "New Article Has Been Successfully Added";
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




        public ActionResult NCreateArticle()
        {
            Article inventory = new Article();
            //ViewBag.NurseryId = Int32.Parse(Session["NurseryID"].ToString());
            return View(inventory);
        }
        [HttpPost]
        public ActionResult NCreateArticle(Article inventory)
        {
            try
            {

                OurDbContext db = new OurDbContext();
                if (ModelState.IsValid)
                {
                    db.article.Add(inventory);
                    db.SaveChanges();
                    List<Article> articlels = db.article.AsEnumerable().ToList();
                    //return View("InventoryDetails", inventories);
                    ViewBag.Message = "New Article Has Been Successfully Added";
                    return View("NLoggedIn");
                }
                return View(inventory);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("InventoryError");
            }
        }


        public ActionResult ViewArticle()
        {
            OurDbContext db = new OurDbContext();
            return View(db.article.ToList());
        }





        //  public ActionResult ANotification()
        //{
        //    OurDbContext db = new OurDbContext();
        //    NurseryAccount n = new NurseryAccount(); 
        //    List<NurseryInventory> inventories = db.NurseryInventories.AsEnumerable().ToList();
        //    ViewBag.Description = "";
        //    foreach (var item in inventories)
        //    {
        //        if(item.stock < 5)
        //        {
        //            n = db.nurseryAccount.Where(x => x.NurseryID == item.NurseryID).FirstOrDefault();
        //            string s = n.NurseryName + " Is Running Out of " + item.PlantName+" Plants,";
                    
        //            ViewBag.Description = ViewBag.Description + " " + s;
                    
        //        }
               
        //    }
            
           
        //    return View("ALoogedIn");

        //}





        //public ActionResult UNotification()
        //{
        //    OurDbContext db = new OurDbContext();
        //    List<NurseryInventory> inventories = db.NurseryInventories.AsEnumerable().ToList();
        //    NurseryAccount n = new NurseryAccount();
        //    ViewBag.Description = "";
        //    foreach (var item in inventories)
        //    {
        //        if (item.discount > 0)
        //        {
        //            n = db.nurseryAccount.Where(x => x.NurseryID == item.NurseryID).FirstOrDefault();
        //            string s = n.NurseryName + " Is Giving " + item.discount + "% Discount On " + item.PlantName + " Plants,";
        //            ViewBag.Description = ViewBag.Description + " " + s;
        //        }

        //    }


        //    return View("LoggedIn");

        //}

        public ActionResult HPD()
        {
            return View();
        }

        public ActionResult AManageNurseryProfile(int id)
        {
            using (OurDbContext db = new OurDbContext())
            {

                return View(db.nurseryAccount.Where(x => x.NurseryID == id).FirstOrDefault());
            }

        }
        [HttpPost]
        public ActionResult AManageNurseryProfile(NurseryAccount user)
        {
            try
            {
                using (OurDbContext db = new OurDbContext())
                {

                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
                ViewBag.Message = "Nursery Profile Updated Succesfully";
                return View("ALoogedIn");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult AManageUserProfile(int id)
        {
            using (OurDbContext db = new OurDbContext())
            {

                return View(db.userAccount.Where(x => x.UserID == id).FirstOrDefault());
            }

        }
        [HttpPost]
        public ActionResult AManageUserProfile(UserAccount user)
        {
            try
            {
                using (OurDbContext db = new OurDbContext())
                {

                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
                ViewBag.Message = "User Profile Updated Succesfully";
                return View("ALoogedIn");
            }
            catch
            {
                return View();
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


        public ActionResult SummerAvailable()
        {
            return View();
        }















    }
}