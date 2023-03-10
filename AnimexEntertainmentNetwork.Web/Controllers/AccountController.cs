using AnimexEntertainmentNetwork.Aut;
using AnimexEntertainmentNetwork.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


using System.Drawing;
using System.IO;
using System.Web.Helpers;


namespace AnimexEntertainmentNetwork.Web.Controllers
{
    public class AccountController : BaseController
    {

        public AccountController(AnimexService udb)
            : base(udb)
        {

        }
        //
        // GET: /Account/
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult Index()
        {
            return View();
        }


        /* Login - Log Out - Sing Up */

        public ActionResult girisyap(string username,string password)
        {

            var kontrol = _udb.User_TB_Select().Where(m => m.USER_NAME == username && m.PASSWORD == password).Count();
            if (kontrol==0)
            {
                return Json("Kullanıcı Adı veya Şifresi Geçersiz");

            }
            else
	        {

                var ticket = new FormsAuthenticationTicket(
                              version: 1,
                              name: username,
                              issueDate: DateTime.Now,
                              expiration: DateTime.Now.AddMinutes(15),
                              isPersistent: true,
                              userData: String.Join("|", username));

                var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                cookie.HttpOnly = true;
                cookie.Expires = DateTime.Now.AddMinutes(15);
                HttpContext.Response.Cookies.Add(cookie);

                return Json("ok");
	        }
        }


        

        public ActionResult logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");

        }

        //public ActionResult kayitol(string username, string password, string mail)
        //{
        //    int usersay = _udb.User_TB_Select().Where(m => m.USER_NAME == username && m.PASSWORD == password && m.E_MAIL == mail).Count();
        //    if (usersay == 0)
        //    {
        //        if (username == "" || username == null || password == "" || password == null || mail == "" || mail == null)
        //        {
        //            return Json("nok");
        //        }
        //        else
        //        {
        //            _udb.USER_insert(username, password, mail);
        //            return Json("ok");
        //        }
        //    }
        //    else
        //    {
        //        return Json("aynikayit");
        //    }

        //}


        /* Login - Log Out - Sing Up */




        //Haber İşlemleri
       
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult HaberListele()
        {


            if (User.Identity.Name == "" || User.Identity.Name == null)
            {


                ViewData["EkibimizFotoFile"] = "../upload/kullanici_img/2020657311139412_513211862162706_2416717772970694998_n.jpg";
            }
            else
            {
                var USER = _udb.User_TB_Select().Where(m => m.USER_NAME == User.Identity.Name).First();

                ViewData["EkibimizFotoFile"] = USER.USER_IMG;
            }




            ViewBag.haberlistele = _udb.NewsTB_Select();
            ViewBag.DATAKATEGORI = _udb.CategoryTB_Select();

            return View();
        }

        //public ActionResult BlogKaydet(int selectkategori, string HaberBaslik, string HaberDetay)
        //{
        //    _udb.NewsTB_insert(selectkategori,HaberBaslik, HaberDetay);
        //    return RedirectToAction("HaberListele", "Account");
        //}


        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult haberekle(int ID,string News_Title, string News_Description)
        {
            var kontrol = _udb.CategoryTB_Select().Where(m => m.ID == ID);
            ViewBag.kategorilihaber = kontrol;

            ViewData["kategoriid"] = ID;

            if (News_Title == "" || News_Title == null && News_Description == "" || News_Description == null)
            {
                return Json("Boş Kayıt");
            }
            else
            {
                _udb.NewsTB_insert(ID,News_Title, News_Description);
                return Json("ok");
            }


           
        }


        [AutControl]
        [AutControl(Roles = "Admin")]
        public ActionResult habersil(int ID)
        {
            _udb.NewsTB_delete(ID);

            return Json("ok");

        }



        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult HaberDetay(int ID)
        {


            if (User.Identity.Name == "" || User.Identity.Name == null)
            {


                ViewData["EkibimizFotoFile"] = "../upload/kullanici_img/2020657311139412_513211862162706_2416717772970694998_n.jpg";
            }
            else
            {
                var USER = _udb.User_TB_Select().Where(m => m.USER_NAME == User.Identity.Name).First();

                ViewData["EkibimizFotoFile"] = USER.USER_IMG;
            }



            ViewBag.etiket = _udb.Etiketler_TB_select(ID);
            var kontrol = _udb.NewsTB_Select().Where(m => m.ID == ID).First();
            ViewData["ID"] = kontrol.ID;
            ViewData["News_Title"] = kontrol.News_Title;
            ViewData["News_Description"] = kontrol.News_Description;


            ViewBag.DataHaberResim = _udb.Blog_FotoTB_Select().Where(m => m.PostID == ID);
            ViewBag.DataSidebarHaberResim = _udb.Sidebar_News_FotoTB_Select().Where(m => m.PostID == ID);
            ViewBag.DataOtherHaberResim = _udb.Other_News_FotoTB_Select().Where(m => m.PostID == ID);

            return View();

        }

        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult haberguncelle(int ID, string News_Title, string PostEtiketi, string News_Description)
        {

            //_udb.NewsTB_update(ID, News_Title, News_Description, PostEtiketi, User.Identity.Name);

            

            var kontrol = _udb.SliderTB_Select().Where(m => m.News_ID == ID).Count();

            if (kontrol==0)
            {
                _udb.NewsTB_update(ID, News_Title, News_Description, PostEtiketi, User.Identity.Name);
                
            }
            else
            {
                _udb.NewsTB_update(ID, News_Title, News_Description, PostEtiketi, User.Identity.Name);
                _udb.SliderTB_update_haber(ID, News_Title, News_Description);
            }

           

            string[] etiketdizi = PostEtiketi.Split(',');
            int say = _udb.Etiketler_TB_select(ID).Count();

            if (say == 0)
            {
                foreach (var item in etiketdizi)
                {
                    if (PostEtiketi == "" || PostEtiketi == null)
                    {
                        return Json("etiketbos");
                    }
                    else
                    {
                        _udb.Etiketler_TB_insert(ID, item.ToString());
                    }
                    
                }
            }

            else
            {
                var etiketler = _udb.Etiketler_TB_select(ID);

                foreach (var item in etiketler)
                {
                    _udb.Etiketler_TB_delete(item.EtiketID);
                }
                foreach (var item2 in etiketdizi)
                {
                    _udb.Etiketler_TB_insert(ID, item2.ToString());
                }
            }

            return Json("ok");

        }

       



        [AutControl]
        [AutControl(Roles = "Admin")]
        public ActionResult HaberAnasayfaStatu(int ID, int Statu)
        {
            _udb.NewsTB_AnasayfaStatuUpdate(ID, Statu);
            return Json("ok");
        }



        //Haber İşlemleri




        //Post Resim İşlemleri

        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult PostresimEkle(AnimexEntertainmentNetwork.Web.Models.RESIMEKLE model)
        {
            var r = new Random();
            var randomNumber = r.Next();
            var randomNumberLessThan1000 = r.Next(99999999);

            var fileName = this.Server.MapPath("../upload/News_img/" + System.IO.Path.GetFileName(randomNumberLessThan1000 + model.resim.FileName));
            model.resim.SaveAs(fileName);

            var kontrol = _udb.UrunFotoTB_list(model.ID).Where(m => m.Default_Haber_Foto == true).Count();
            if (kontrol == 0)
            {
                _udb.Blog_FotoTB_insert(model.ID, true, "../upload/News_img/" + randomNumberLessThan1000 + model.resim.FileName, model.resim.FileName);
            }
            else
            {
                _udb.Blog_FotoTB_insert(model.ID, false, "../upload/News_img/" + randomNumberLessThan1000 + model.resim.FileName, model.resim.FileName);
            }
            return Redirect("HaberDetay?ID=" + model.ID);


        }


        //Default Haber FOTO
        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult Urundefaultfoto(int ID = 0)
        {
            var cekdefaulvarmi = _udb.UrunFotoTB_list_fotoid(ID).First();//haberfoto tablonda haberfoto id ye göre listeleme yap

            int urunid = cekdefaulvarmi.PostID.Value;//buradahaberid yi çektik

            var getir = _udb.UrunFotoTB_list(urunid);//haberfoto tablonda haberid ye göre listeleme yap
            foreach (var item in getir)
            {
                //_udb.UrunFotoTB_update(item.FotoID,item.PostID,false);//haberfoto tablonda hem haber id ye hem de haberfoto id ye göre listele first yap. default fotoyu güncelle
                _udb.UrunFotoTB_update(item.FotoID, item.PostID.Value, false);
            }

            _udb.UrunFotoTB_update(ID, urunid, true);



            return Json("ok");


        }






        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult HaberFotoSil(int ID)
        {
            

            _udb.Blog_FotoTB_Delete(ID);
          
            return Json("ok");
        }


        //Post Resim İşlemleri







        //Sidebar Resim İşlemleri




        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SidebarNewsFoto(AnimexEntertainmentNetwork.Web.Models.RESIMEKLE model)
        {
            var r = new Random();
            var randomNumber = r.Next();
            var randomNumberLessThan1000 = r.Next(99999999);



            var fileName = this.Server.MapPath("../upload/News_min_img/" + System.IO.Path.GetFileName(randomNumberLessThan1000 + model.resim.FileName));
            model.resim.SaveAs(fileName);


            var kontrol = _udb.SidebarFotoTB_list(model.ID).Where(m => m.Default_Haber_Foto == true).Count();
            if (kontrol == 0)
            {

                _udb.Sidebar_News_FotoTB_insert(model.ID, true, "../upload/News_min_img/" + randomNumberLessThan1000 + model.resim.FileName, model.resim.FileName);
            }
            else
            {

                _udb.Sidebar_News_FotoTB_insert(model.ID, false, "../upload/News_min_img/" + randomNumberLessThan1000 + model.resim.FileName, model.resim.FileName);
            }
            return Redirect("HaberDetay?ID=" + model.ID);


        }




        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult SidebarHaberResimDefault(int ID = 0)
        {
            var cekdefaulvarmi = _udb.SidebarFotoTB_list_fotoid(ID).First();//haberfoto tablonda haberfoto id ye göre listeleme yap

            int urunid = cekdefaulvarmi.PostID.Value;//buradahaberid yi çektik

            var getir = _udb.SidebarFotoTB_list(urunid);//haberfoto tablonda haberid ye göre listeleme yap
            foreach (var item in getir)
            {
                //_udb.UrunFotoTB_update(item.FotoID,item.PostID,false);//haberfoto tablonda hem haber id ye hem de haberfoto id ye göre listele first yap. default fotoyu güncelle
                _udb.Sidebar_News_Foto_TB_update(item.FotoID, item.PostID.Value, false);
            }

            _udb.Sidebar_News_Foto_TB_update(ID, urunid, true);



            return Json("ok");


        }


        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult SidebarHaberFotoSil(int ID)
        {
          

            _udb.Sidebar_News_FotoTB_delete(ID);

            return Json("ok");
        }


        //Sidebar Resim İşlemleri

        //Kategori İşlemleri

        [AutControl]
        [AutControl(Roles = "Admin")]
        public ActionResult kategoridoldurgetir()
        {
            var kontrol = _udb.CategoryTB_Select();
            string tablo = "";
            tablo += "<option >Seçiniz</option>";
            foreach (var item in kontrol)
            {
                tablo += "<option value=" + item.ID + " >" + item.Category_Name + "</option>";

            }
            return Json(tablo);
        }



        [AutControl]
        [AutControl(Roles = "Admin")]
        public ActionResult kategoriekle(string Category_Name)
        {
            if (Category_Name == "" || Category_Name == null)
            {
                return Json("Boş Kayıt");
            }
            else
            {
                _udb.CategoryTB_insert(Category_Name);
                return Json("ok");
            }

        }



        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult kategoriguncelle(int ID, string Category_Name)
        {

            _udb.CategoryTB_update(ID, Category_Name);

            return Json("ok");

        }




        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult KategoriListele()
        {


            if (User.Identity.Name == "" || User.Identity.Name == null)
            {


                ViewData["EkibimizFotoFile"] = "../upload/kullanici_img/2020657311139412_513211862162706_2416717772970694998_n.jpg";
            }
            else
            {
                var USER = _udb.User_TB_Select().Where(m => m.USER_NAME == User.Identity.Name).First();

                ViewData["EkibimizFotoFile"] = USER.USER_IMG;
            }

            ViewBag.kategorilistele = _udb.CategoryTB_Select();

            return View();
        }
        //Kategori İşlemleri





        //Sidebar Banner
      
        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult SidebarBannerListele()
        {

            if (User.Identity.Name == "" || User.Identity.Name == null)
            {


                ViewData["EkibimizFotoFile"] = "../upload/kullanici_img/2020657311139412_513211862162706_2416717772970694998_n.jpg";
            }
            else
            {
                var USER = _udb.User_TB_Select().Where(m => m.USER_NAME == User.Identity.Name).First();

                ViewData["EkibimizFotoFile"] = USER.USER_IMG;
            }
            ViewBag.DataSidebarBannerResim = _udb.Sidebar_Banner_img_TB_Select();
            return View();
        }

        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SidebarBannerFoto(AnimexEntertainmentNetwork.Web.Models.RESIMEKLE model)
        {
            var r = new Random();
            var randomNumber = r.Next();
            var randomNumberLessThan1000 = r.Next(99999999);



            var fileName = this.Server.MapPath("../upload/banner_img/" + System.IO.Path.GetFileName(randomNumberLessThan1000 + model.resim.FileName));
            model.resim.SaveAs(fileName);


            var kontrol = _udb.SidebarBannerFotoTB_list(model.ID).Where(m => m.Default_Foto == true).Count();
            if (kontrol == 0)
            {

                _udb.Sidebar_Banner_insert(true, "../upload/banner_img/" + randomNumberLessThan1000 + model.resim.FileName, model.resim.FileName);
            }
            else
            {

                _udb.Sidebar_Banner_insert(false, "../upload/banner_img/" + randomNumberLessThan1000 + model.resim.FileName, model.resim.FileName);
            }
            return Redirect("SidebarBannerListele");


        }


        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult SidebarBannerResimDefault(int ID = 0)
        {
            var cekdefaulvarmi = _udb.SidebarBannerFotoTB_list_fotoid(ID).First();//haberfoto tablonda haberfoto id ye göre listeleme yap

            int urunid = cekdefaulvarmi.FotoID;//buradahaberid yi çektik

            var getir = _udb.SidebarBannerFotoTB_list(urunid);//haberfoto tablonda haberid ye göre listeleme yap
            foreach (var item in getir)
            {
                //_udb.UrunFotoTB_update(item.FotoID,item.PostID,false);//haberfoto tablonda hem haber id ye hem de haberfoto id ye göre listele first yap. default fotoyu güncelle
                _udb.Sidebar_Banner_Foto_TB_update(item.FotoID, false);
            }

            _udb.Sidebar_Banner_Foto_TB_update(ID, true);



            return Json("ok");


        }


        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult SidebarBannerFotoSil(int ID)
        {
            

            _udb.Sidebar_Banner_delete(ID);

            return Json("ok");
        }




        [AutControl]
        [AutControl(Roles = "Admin")]
        public ActionResult BannerAnasayfaStatu(int FotoID, int Statu)
        {
            _udb.SidebarBanner_AnasayfaStatuUpdate(FotoID, Statu);
            return Json("ok");
        }



        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult sidebarfotonoteguncelle(int FotoID, string fotonote)
        {

            _udb.Sidebar_Banner_Note_Update(FotoID, fotonote);

            return Json("ok");

        }


        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult sidebarfotolinkguncelle(int FotoID, string fotolink)
        {

            _udb.Sidebar_Banner_Link_Update(FotoID, fotolink);

            return Json("ok");

        }

        //Sidebar Banner


        //Yorum İşlemleri
        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult OnaysizYorumListele()
        {


            if (User.Identity.Name == "" || User.Identity.Name == null)
            {


                ViewData["EkibimizFotoFile"] = "../upload/kullanici_img/2020657311139412_513211862162706_2416717772970694998_n.jpg";
            }
            else
            {
                var USER = _udb.User_TB_Select().Where(m => m.USER_NAME == User.Identity.Name).First();

                ViewData["EkibimizFotoFile"] = USER.USER_IMG;
            }

            ViewBag.yorumlar = _udb.CommentTB_Select().Where(m => m.Status == 0);
            return View();
        }



        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult OnayliYorumListele()
        {
            ViewBag.onayliyorum = _udb.CommentTB_Select().Where(m => m.Status == 1);
            return Json("ok");

        }

        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult YorumListele()
        {


            if (User.Identity.Name == "" || User.Identity.Name == null)
            {


                ViewData["EkibimizFotoFile"] = "../upload/kullanici_img/2020657311139412_513211862162706_2416717772970694998_n.jpg";
            }
            else
            {
                var USER = _udb.User_TB_Select().Where(m => m.USER_NAME == User.Identity.Name).First();

                ViewData["EkibimizFotoFile"] = USER.USER_IMG;
            }

            ViewBag.yorumlistele = _udb.CommentTB_Select().ToList();
            return View();

        }

        public ActionResult Yorumekle(int newsid,string kullaniciadi,string yorum)
        {

            _udb.CommentTB_insert(newsid, kullaniciadi,yorum, Request.UserHostAddress.ToString(), Request.Browser.Browser);

            _udb.YorumOkumaEkle(newsid);
            return Json("ok");
        }




        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult Yorumsil(int ID)
        {
            _udb.CommentTB_delete(ID);
                return Json("ok");

        }

        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult YorumDetay(int ID)
        {

            if (User.Identity.Name == "" || User.Identity.Name == null)
            {


                ViewData["EkibimizFotoFile"] = "../upload/kullanici_img/2020657311139412_513211862162706_2416717772970694998_n.jpg";
            }
            else
            {
                var USER = _udb.User_TB_Select().Where(m => m.USER_NAME == User.Identity.Name).First();

                ViewData["EkibimizFotoFile"] = USER.USER_IMG;
            }



            var kontrol = _udb.CommentTB_Select().Where(m => m.CommentID == ID).First();
            ViewData["ID"] = kontrol.CommentID;
            ViewData["YorumDetay"] = kontrol.User_Comment;

            return View();

        }


        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult YorumDetayGuncelle(int ID, string YorumDetay)
        {
            _udb.CommentTB_update(ID, YorumDetay);
            return Redirect("YorumDetay?ID=" + ID);

        }


        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult YorumOnay(int ID)
        {
            _udb.CommentTB_apporval(ID);
            return Json("ok");

        }
        //Yorum İşlemleri


        //Slider Kategori İşlemleri
        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult SliderYerListele()
        {

            if (User.Identity.Name == "" || User.Identity.Name == null)
            {


                ViewData["EkibimizFotoFile"] = "../upload/kullanici_img/2020657311139412_513211862162706_2416717772970694998_n.jpg";
            }
            else
            {
                var USER = _udb.User_TB_Select().Where(m => m.USER_NAME == User.Identity.Name).First();

                ViewData["EkibimizFotoFile"] = USER.USER_IMG;
            }




            ViewBag.sliderkategorilistele = _udb.SliderCategoryTB_Select();

        

            return View();

        }


        [AutControl]
        [AutControl(Roles = "Admin")]
        public ActionResult SliderCategoryEkle(string SliderCategoryName, string SliderCategoryNote)
        {
            if (SliderCategoryName == "" || SliderCategoryName == null)
            {
                return Json("Boş Kayıt");
            }
            else
            {
                _udb.SliderCategoryTB_insert(SliderCategoryName,SliderCategoryNote);
                return Json("ok");
            }

        }

        [AutControl]
        [AutControl(Roles = "Admin")]
        public ActionResult SliderCategorySil(int ID)
        {

            _udb.SliderCategoryTB_delete(ID);
            return Json("ok");
        }
        [AutControl]
        [AutControl(Roles = "Admin")]
        public ActionResult SliderCategoryGuncelle(int ID, String SliderCategoryName, String SliderCategoryNote)
        {
            _udb.SliderCategoryTB_update(ID, SliderCategoryName, SliderCategoryNote);
            return Json("ok");
        }


        [AutControl]
        [AutControl(Roles = "Admin")]
        public ActionResult SliderNotGuncelle(int ID){

            var kontrol = _udb.SliderCategoryTB_Select().Where(m => m.SliderCategoryID == ID).First();
            ViewData["ID"] = kontrol.SliderCategoryID;
            ViewData["SliderCategoryNote"] = kontrol.SliderCategoryNote;

            return View();
        }

        [AutControl]
        [AutControl(Roles = "Admin")]
        public ActionResult SliderNotGuncelleFonsksiyon(int ID, string Yer_Notu)
        {
            _udb.SliderYerNotu_update(ID, Yer_Notu);

            return Json("ok");
        }



        [AutControl]
        [AutControl(Roles = "Admin")]
        public ActionResult SliderCategoryDetay(int ID)
        {


            if (User.Identity.Name == "" || User.Identity.Name == null)
            {


                ViewData["EkibimizFotoFile"] = "../upload/kullanici_img/2020657311139412_513211862162706_2416717772970694998_n.jpg";
            }
            else
            {
                var USER = _udb.User_TB_Select().Where(m => m.USER_NAME == User.Identity.Name).First();

                ViewData["EkibimizFotoFile"] = USER.USER_IMG;
            }




            var kontrol = _udb.SliderCategoryTB_Select().Where(m => m.SliderCategoryID == ID).First();
            ViewData["ID"] = kontrol.SliderCategoryID;


            ViewBag.slider = _udb.SliderTB_Select().Where(m => m.SliderCategoryID == ID);

            //using (var context = new BloggingContext())
            //{
            //    var blogs = context.Blogs.SqlQuery("SELECT ID FROM News_TB where ID not in ( select News_ID From Slider_TB)").ToList();
            //}


           
            //ViewBag.sliderhabercek = "SELECT ID FROM News_TB where ID not in ( select News_ID From Slider_TB)";

            ViewBag.sliderhabercek = _udb.NewsTB_Select();
          
          

            
            return View();

        }
        //Slider Kategori İşlemleri

        //Slider İşlemleri
        [AutControl]
        [AutControl(Roles = "Admin")]
        public ActionResult SliderEkle(int SliderhaberbaslikID, int yerID)
        {

            _udb.SliderTB_insert(SliderhaberbaslikID, yerID);
                return Json("ok");
          


        }
        [AutControl]
        [AutControl(Roles = "Admin")]
        public ActionResult SliderSil(int ID)
        {

            _udb.SliderTB_delete(ID);
            return Json("ok");
        }

        //Slider İşlemleri
        [AutControl]
        [AutControl(Roles = "Admin")]
        public ActionResult SliderDetay(int ID)
        {


            if (User.Identity.Name == "" || User.Identity.Name == null)
            {


                ViewData["EkibimizFotoFile"] = "../upload/kullanici_img/2020657311139412_513211862162706_2416717772970694998_n.jpg";
            }
            else
            {
                var USER = _udb.User_TB_Select().Where(m => m.USER_NAME == User.Identity.Name).First();

                ViewData["EkibimizFotoFile"] = USER.USER_IMG;
            }



            ViewBag.slider = _udb.SliderTB_Foto_Select().Where(m => m.SliderID == ID);
            return View();
        }


        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SliderFotoEkle(AnimexEntertainmentNetwork.Web.Models.RESIMEKLE model)
        {
            var r = new Random();
            var randomNumber = r.Next();
            var randomNumberLessThan1000 = r.Next(99999999);



            var fileName = this.Server.MapPath("../upload/slider_img/" + System.IO.Path.GetFileName(randomNumberLessThan1000 + model.resim.FileName));
            model.resim.SaveAs(fileName);


            var kontrol = _udb.SliderFotoTB_list(model.ID).Where(m => m.Default_Haber_Foto == true).Count();
            if (kontrol == 0)
            {

                _udb.Slider_Foto_insert(model.ID, true, "../upload/slider_img/" + randomNumberLessThan1000 + model.resim.FileName, model.resim.FileName);
            }
            else
            {

                _udb.Slider_Foto_insert(model.ID, false, "../upload/slider_img/" + randomNumberLessThan1000 + model.resim.FileName, model.resim.FileName);
            }
            return Redirect("SliderDetay?ID=" + model.ID);


        }


 



        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult SliderFotoResimDefault(int ID = 0)
        {
            var cekdefaulvarmi = _udb.SliderFotoTB_list_fotoid(ID).First();//haberfoto tablonda haberfoto id ye göre listeleme yap

            int urunid = cekdefaulvarmi.SliderID.Value;//buradahaberid yi çektik

            var getir = _udb.SliderFotoTB_list(urunid);//haberfoto tablonda haberid ye göre listeleme yap
            foreach (var item in getir)
            {
                //_udb.UrunFotoTB_update(item.FotoID,item.PostID,false);//haberfoto tablonda hem haber id ye hem de haberfoto id ye göre listele first yap. default fotoyu güncelle
                _udb.SliderFotoTB_update(item.FotoID, item.SliderID.Value, false);
            }

            _udb.SliderFotoTB_update(ID, urunid, true);



            return Json("ok");


        }




        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult SliderDefaultYap(int ID = 0)
        {
            var cekdefaulvarmi = _udb.Slider_list_SELECT(ID).First();//haberfoto tablonda haberfoto id ye göre listeleme yap

            int urunid = cekdefaulvarmi.SliderCategoryID.Value;//burada haberid yi çektik

            var getir = _udb.Slider_list_SliderID(urunid);//haberfoto tablonda haberid ye göre listeleme yap
            foreach (var item in getir)
            {

                _udb.Slider_default_update(item.ID, item.SliderCategoryID.Value, 0);//haberfoto tablonda hem haber id ye hem de haberfoto id ye göre listele first yap. default fotoyu güncelle
            }

            _udb.Slider_default_update(ID, urunid, 1);



            return Json("ok");


        }


        //Slider İşlemleri


        //Etiket İşlemleri


        public ActionResult Etiketekle(string TagName)
        {

            int etiketsay = _udb.TagTB_Select().Where(m => m.TagName == TagName).Count();

            if (etiketsay != 0)
            {
                return Json("aynikayit");
            }
            else
            {
                if (TagName =="" || TagName == null)
                {
                    return Json("boskayit");
                }
                else
                {
                    _udb.TagTB_insert(TagName);
                    return Json("ok");
                }
            }
        }


        public ActionResult CekFormEtiket(int gelenNewsID)
        {

            var tag = _udb.TagTB_Select();

            string gonder = " ";



            foreach (var item in tag)
            {
                var etiketicinsay = _udb.Etiketler_TB_select(gelenNewsID).Where(m => m.EtiketAdi == Convert.ToString(item.TagID)).Count();
                if (etiketicinsay != 0)
                {
                    gonder += "<option selected='selected' value='" + item.TagID + "'>" + item.TagName + "</option>";
                }
                else
                {
                    gonder += "<option value='" + item.TagID + "'>" + item.TagName + "</option>";
                }

            }


            return Json(gonder);
        }



        //public ActionResult EtiketeGoreAraGetir(string PostEtiketi)
        //{
        //    string[] etiketdizi = PostEtiketi.Split(',');
        //    string query = "";
        //    if (PostEtiketi == "")
        //    {
        //        query = "select *from Blog_TB Where Status=0";




        //    }


        //    else
        //    {
        //        string aa = "";
        //        int i = 0;
        //        foreach (var item in etiketdizi)
        //        {
        //            if (i == 0)
        //            {
        //                aa += " like '%" + item + "%'";
        //            }
        //            else
        //            {
        //                aa += "or PostTags like '%" + item + "%'";
        //            }
        //            i += 1;
        //        }

        //        query = "select *from Blog_TB where Status=0 and PostTags " + aa;



        //    }
        //    int a = 0;
        //    var liste = _udb.BlogTB_Selectwithquery(query);
        //    string table = "";
        //    foreach (var item in liste)
        //    {

        //        a = a + 1;
        //        table += " <tr>"

        //          + "  <td>" + a + "</td>"
        //           + " <td><center><button type='button' class='btn' onclick='POSTKALDIR(" + item.PostID + ")'>Kaldır</button></center></td>"
        //           + " <td><center> <button type='button' class='btn' onclick='DETAY_POST(" + item.PostID + ")'>Detay</button></center></td>"


        //            + "<td>"
        //              + item.CategoryName +


        //           " </td>"
        //           + " <td>" +
        //                item.PostTitle

        //          + "  </td>"


        //        + "  </tr>";
        //    }

        //    return Json(table);


        // }


        //Etiket İşlemleri



        //Diğer Haberler Foto




        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult OtherNewsFoto(AnimexEntertainmentNetwork.Web.Models.RESIMEKLE model)
        {
            var r = new Random();
            var randomNumber = r.Next();
            var randomNumberLessThan1000 = r.Next(99999999);



            var fileName = this.Server.MapPath("../upload/News_other_img/" + System.IO.Path.GetFileName(randomNumberLessThan1000 + model.resim.FileName));
            model.resim.SaveAs(fileName);


            var kontrol = _udb.OtherFotoTB_list(model.ID).Where(m => m.Default_Haber_Foto == true).Count();
            if (kontrol == 0)
            {

                _udb.Other_News_FotoTB_insert(model.ID, true, "../upload/News_other_img/" + randomNumberLessThan1000 + model.resim.FileName, model.resim.FileName);
            }
            else
            {

                _udb.Other_News_FotoTB_insert(model.ID, false, "../upload/News_other_img/" + randomNumberLessThan1000 + model.resim.FileName, model.resim.FileName);
            }
            return Redirect("HaberDetay?ID=" + model.ID);


        }




        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult OtherHaberResimDefault(int ID = 0)
        {
            var cekdefaulvarmi = _udb.OtherFotoTB_list_fotoid(ID).First();//haberfoto tablonda haberfoto id ye göre listeleme yap

            int urunid = cekdefaulvarmi.PostID.Value;//buradahaberid yi çektik

            var getir = _udb.OtherFotoTB_list(urunid);//haberfoto tablonda haberid ye göre listeleme yap
            foreach (var item in getir)
            {
                //_udb.UrunFotoTB_update(item.FotoID,item.PostID,false);//haberfoto tablonda hem haber id ye hem de haberfoto id ye göre listele first yap. default fotoyu güncelle
                _udb.Other_News_Foto_TB_update(item.FotoID, item.PostID.Value, false);
            }

            _udb.Other_News_Foto_TB_update(ID, urunid, true);



            return Json("ok");


        }


        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult OtherHaberFotoSil(int ID)
        {
            
            _udb.Other_News_FotoTB_delete(ID);

            return Json("ok");
        }

        //Diğer Haberler Foto


        //Hakkımızda Animex Nedir ?


        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult HakkimizdaDetay(int ID)
        {

            if (User.Identity.Name == "" || User.Identity.Name == null)
            {


                ViewData["EkibimizFotoFile"] = "../upload/kullanici_img/2020657311139412_513211862162706_2416717772970694998_n.jpg";
            }
            else
            {
                var USER = _udb.User_TB_Select().Where(m => m.USER_NAME == User.Identity.Name).First();

                ViewData["EkibimizFotoFile"] = USER.USER_IMG;
            }



            var kontrol = _udb.AboutUs_TB_Select().Where(m => m.AboutUsID == ID).First();
            ViewData["HakkimizdaID"] = kontrol.AboutUsID;
            ViewData["HakkimizdaBaslik"] = kontrol.AboutUs_Title;
            ViewData["Hakkimizdaİcerik"] = kontrol.AboutUs_Detail;

            return View();

        }

        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult hakkimizdaguncelle(int AboutUsID, string AboutUs_Title, string AboutUs_Detail)
        {

            _udb.About_Us_update(AboutUsID, AboutUs_Title, AboutUs_Detail);

            return Json("ok");

        }


        //Hakkımızda Animex Nedir ?


        //Kullanım Şartları

        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult KullanimSartlariDetay(int ID)
        {

            if (User.Identity.Name == "" || User.Identity.Name == null)
            {


                ViewData["EkibimizFotoFile"] = "../upload/kullanici_img/2020657311139412_513211862162706_2416717772970694998_n.jpg";
            }
            else
            {
                var USER = _udb.User_TB_Select().Where(m => m.USER_NAME == User.Identity.Name).First();

                ViewData["EkibimizFotoFile"] = USER.USER_IMG;
            }



            var kontrol = _udb.TermsofUse_TB_Select().Where(m => m.SartID == ID).First();
            ViewData["SartID"] = kontrol.SartID;
            ViewData["Sartİcerik"] = kontrol.Kullanim_Sartlari;

            return View();

        }

        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult kullanimsartlariguncelle(int TermsID, string Terms_Detail)
        {

            _udb.TermofUse_update(TermsID, Terms_Detail);

            return Json("ok");

        }
        //Kullanım Şartları

        //Gizlilik Politikası
        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult GizlilikPolitikasiDetay(int ID)
        {


            if (User.Identity.Name == "" || User.Identity.Name == null)
            {


                ViewData["EkibimizFotoFile"] = "../upload/kullanici_img/2020657311139412_513211862162706_2416717772970694998_n.jpg";
            }
            else
            {
                var USER = _udb.User_TB_Select().Where(m => m.USER_NAME == User.Identity.Name).First();

                ViewData["EkibimizFotoFile"] = USER.USER_IMG;
            }


            var kontrol = _udb.Privacy_Policy_TB_Select().Where(m => m.PolitikaID == ID).First();
            ViewData["PolitikaID"] = kontrol.PolitikaID;
            ViewData["Politikaİcerik"] = kontrol.Gizlilik_Politikasi_Detay;

            return View();

        }

        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult gizlilikpolitikasiguncelle(int PolitikaID, string Policy_Detail)
        {

            _udb.Privacy_Policy_uptade(PolitikaID, Policy_Detail);

            return Json("ok");

        }



       
        //Gizlilik Politikası


        //İkon ve Banner Sayfası

        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult İkonveBannerDetay(int ID)
        {


            if (User.Identity.Name == "" || User.Identity.Name == null)
            {


                ViewData["EkibimizFotoFile"] = "../upload/kullanici_img/2020657311139412_513211862162706_2416717772970694998_n.jpg";
            }
            else
            {
                var USER = _udb.User_TB_Select().Where(m => m.USER_NAME == User.Identity.Name).First();

                ViewData["EkibimizFotoFile"] = USER.USER_IMG;
            }


            var kontrol = _udb.iconbanner_TB_Select().Where(m => m.iconbannerID == ID).First();
            ViewData["iconbannerID"] = kontrol.iconbannerID;
            ViewData["iconbannerDetay"] = kontrol.iconbannerdetay;

            return View();

        }

        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult ikonvebannerguncelle(int iconbannerID, string iconvebannerdetay)
        {

            _udb.iconbanner_update(iconbannerID, iconvebannerdetay);

            return Json("ok");

        }

        //İkon ve Banner Sayfası



        //Developer Blog

        [AutControl]
        [AutControl(Roles="Admin")]
        [ValidateInput(false)]
        public ActionResult DeveloperBlog_ekle(string DevBlog_Description)
        {
            if (DevBlog_Description == "" || DevBlog_Description == null)
            {
                return Json("Boş Kayıt");
            }
            else
            {
                _udb.Developer_Blog_insert(DevBlog_Description, User.Identity.Name);
                return Json("ok");
            }

        }

        [AutControl]
        [AutControl(Roles="Admin")]
        [ValidateInput(false)]
        public ActionResult DeveloperBlog_sil(int ID)
        {

            _udb.Developer_Blog_delete(ID);
            return Json("ok");
        }

        [AutControl]
        [AutControl(Roles="Admin")]
        [ValidateInput(false)]
        public ActionResult DeveloperBlog_guncelle(int ID,string DevBlog_Description)
        {
            _udb.Developer_Blog_update(ID, DevBlog_Description, User.Identity.Name);

            return Json("ok");
        }

        [AutControl]
        [AutControl(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult DeveloperBlogListele()
        {

            if (User.Identity.Name == "" || User.Identity.Name == null)
            {


                ViewData["EkibimizFotoFile"] = "../upload/kullanici_img/2020657311139412_513211862162706_2416717772970694998_n.jpg";
            }
            else
            {
                var USER = _udb.User_TB_Select().Where(m => m.USER_NAME == User.Identity.Name).First();

                ViewData["EkibimizFotoFile"] = USER.USER_IMG;
            }

            ViewBag.bloglistele = _udb.Developer_Blog_Select();
            return View();
        }



        [AutControl]
        [AutControl(Roles="Admin")]
        [ValidateInput(false)]
        public ActionResult DeveloperBlogDetay(int ID)
        {

            if (User.Identity.Name == "" || User.Identity.Name == null)
            {


                ViewData["EkibimizFotoFile"] = "../upload/kullanici_img/2020657311139412_513211862162706_2416717772970694998_n.jpg";
            }
            else
            {
                var USER = _udb.User_TB_Select().Where(m => m.USER_NAME == User.Identity.Name).First();

                ViewData["EkibimizFotoFile"] = USER.USER_IMG;
            }


            var kontrol = _udb.Developer_Blog_Select().Where(m => m.DevBlogID == ID).First();
            ViewData["devblogID"] = kontrol.DevBlogID;
            ViewData["developerblogDetay"] = kontrol.DevBlog_Description;

            return View();
        }



        //Developer Blog
	}
}