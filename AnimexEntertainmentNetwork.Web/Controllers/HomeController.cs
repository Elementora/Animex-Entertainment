using AnimexEntertainmentNetwork.Aut;
using AnimexEntertainmentNetwork.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AnimexEntertainmentNetwork.Web.Controllers
{
    public class HomeController : BaseController
    {
         public HomeController(AnimexService udb)
            : base(udb)
        {

        }

        //
        // GET: /Home/
        public ActionResult Index()
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

            return View();
        }

        public ActionResult Login()
        {


            return View();
        }

        public ActionResult SingUp()
        {

            return View();

        }


        public ActionResult Etiketler()
        {
            return View();
        }
        public ActionResult Galeri()
        {
            return View();
        }
        public ActionResult OzelYapimcilar()
        {
            return View();
        }
       

        [Authorize]
        public ActionResult ProfilAyarlari()
        {
            var USER = _udb.User_TB_Select().Where(m => m.USER_NAME == User.Identity.Name).First();
            ViewData["kullaniciadi"] = User.Identity.Name;
            ViewData["nickname"] = USER.RUMUZ;
            ViewData["email"] = USER.E_MAIL;
            ViewData["password"] = USER.PASSWORD;
            ViewData["dogumtarihi"] = String.Format("{0:yyyy-MM-dd}", USER.DATE_OF_BIRTH);
            ViewData["cinsiyet"] = USER.CINSIYET;
            ViewData["hakkimda"] = USER.ABOUT;
            ViewData["KAPAK"] = "";
            ViewData["EkibimizFotoFile"] = USER.USER_IMG;
            return View();
        }




        [HttpPost]
        public ActionResult EkibimizresimGuncelle(AnimexEntertainmentNetwork.Web.Models.RESIMEKLE model)
        {
            var r = new Random();
            var randomNumber = r.Next();
            var randomNumberLessThan1000 = r.Next(99999999);

            var fileName = this.Server.MapPath("../upload/kullanici_img/" + System.IO.Path.GetFileName(randomNumberLessThan1000 + model.resim.FileName));
            model.resim.SaveAs(fileName);
            _udb.USER_TB_FotoUpdate("../upload/kullanici_img/" + randomNumberLessThan1000 + model.resim.FileName);
            return Redirect("ProfilAyarlari");


        }


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



            

            var postdetay = _udb.NewsTB_Select().Where(m => m.ID == ID).First();

          //  Server.ClearError();
          //  Response.Status = "404 not found";
          //  Response.StatusCode = 404;

            ViewData["ID"] = postdetay.ID;
            ViewData["PostBaslik"] = postdetay.News_Title;
            ViewData["PostIcerik"] = postdetay.News_Description;
            ViewData["CategoryID"] = postdetay.CategoryID;


            var kategoriyegore = _udb.NewsTB_Select().Where(m => m.ID == ID).First();


            var kategori = _udb.NewsTB_Select().Where(m => m.CategoryID == kategoriyegore.CategoryID).Take(5);
            ViewBag.kategoricek = kategori;


            string[] etiketler = postdetay.News_Tag.Split(',');

            string TagNameler = _udb.Tag_adiListesiOlustur(postdetay.News_Tag);
            string[] etiketlerad = TagNameler.Split(',');
            ViewBag.Data = etiketlerad;

            //ViewBag.et = etiketler;

            var postresimdetay = _udb.Blog_FotoTB_Select().Where(m => m.PostID == ID).First();
            ViewData["PostResim"] = postresimdetay.FotoURL;

            return View();
        }


        public ActionResult HaberOkumaBildir(int ID)
        {
            _udb.NewsTB_Report(ID);
            return Json("ok");
        }

        public ActionResult YorumBildir(int ID)
        {
            _udb.CommentTB_report(ID);
           
            return Json("ok");
        }

        public ActionResult YorumLike(int ID)
        {

            _udb.YorumLike(ID);
            return Json("ok");
        }

        public ActionResult YorumDislike(int ID)
        {
            _udb.YorumDislike(ID);
            return Json("ok");
        }


        public ActionResult Hakkimizda(int ID)
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


            var hakkimizdadetay = _udb.
                AboutUs_TB_Select().Where(m=>m.AboutUsID == ID).First();

            ViewData["HakkimizdaBaslik"]=hakkimizdadetay.AboutUs_Title;
            ViewData["HakkimizdaDetay"] = hakkimizdadetay.AboutUs_Detail;
            return View();
        }


        public ActionResult KullanimSartlari(int ID)
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

            var kullanimsartlaridetay = _udb.TermsofUse_TB_Select().Where(m => m.SartID == ID).First();

            ViewData["Sartİcerik"] = kullanimsartlaridetay.Kullanim_Sartlari;
            return View();
        }

        public ActionResult GizlilikPolitikasi(int ID)
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

            var gizlilikpolitikasidetay = _udb.Privacy_Policy_TB_Select().Where(m => m.PolitikaID == ID).First();

            ViewData["Politikaİcerik"] = gizlilikpolitikasidetay.Gizlilik_Politikasi_Detay;
            return View();
        }



        public ActionResult İconveBanner(int ID)
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

            var ikovebannerdetay = _udb.iconbanner_TB_Select().Where(m => m.iconbannerID == ID).First();

            ViewData["İkonveBannerİcerik"] = ikovebannerdetay.iconbannerdetay;

            return View();

        }




        //Developer Blog
        public ActionResult DeveloperBlog()
        {

            return View();
        }

       

        //Developer Blog


        //İletişim İşlemleri

        public ActionResult ContactUs()
        {

            return View();
        }

        public ActionResult Iletisimgonder(string ad, string email, string konu, string mesaj)
        {
            if (ad == "" || ad == null || email == "" || email == null || konu == "" || konu == null || mesaj == "")
            {

                return Json("nok");
            }
            else
            {
                string mail = "<html lang='tr-TR'>"
    + "<head>"
    + "    <meta charset='utf-8'>"
    + "  <title>Animex Entertainment Network</title>"

     + "<meta name='viewport' content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no'>"

   

    
    +"<link rel='stylesheet' href='assets/css/foundation-emails.css'>"
    
    + "<link rel='stylesheet' href='http://fonts.googleapis.com/css?family=Open+Sans:400italic,700italic,300,400,700'>"

    + "</head>"
    + "<body class='container' style='min-height: 724px;'>"

     + "<div id='main' role='main'>"

            + "<div class='row'><div class='well'>"



             + "<form action='' class='smart-form'>"

                 + "<fieldset>"

                  + "<span id='logo'><img src='http://www.yoanu.com/wp-content/uploads/2015/03/anime-girls-wallpaper-latest-awesome-i0eok2jx.png' height='500' width='500'></span>"
                            + "<br/>"
                             + "<br/>"



                    
                + "  <div class='form-group'>"

                                                   + " 	<label class='small-12 large-6 first columns'><strong>Ad Soyad  : </strong></label>"


                                                       + " 	<label  style='color:red; font-size:50px;'>" + ad + "</label>"

                                                  + "  </div>"
                                                     + "<br/>"
                + "  <div class='form-group'>"

                                                   + " 	<label class='small-12 large-6 first columns'><strong>E-Posta  : </strong></label>"


                                                       + " 	<label '>" + email + "</label>"

                                                  + "  </div>"
                                                  + "<br/>"

                 + "  <div class='form-group'>"

                                                   + " 	<label class='small-12 large-6 first columns'><strong>Konu : </strong></label>"


                                                       + " 	<label'>" + konu + "</label>"

                                                  + "  </div>"
                                                  + "<br/>"

                                 + "  <div class='form-group'>"

                                                   + " 	<label class='small-12 large-6 first columns'><strong>Mesaj  : </strong></label>"


                                                       + " 	<label'>" + mesaj + "</label>"

                                                  + "  </div>"

                                ;



                mail += "</fieldset>"


              + "</form>"


      + "</div></div>"

      + "</body>"

      + "</html>";

                _udb.AEN_iletisim_insert(mail);
                return Json("ok");
            }
        }

        //İletişim İşlemleri




        //Profil İşlemleri
        public ActionResult UserProfile()
        {
            return View();
        }


        //Profil İşlemleri


    

     // Socialİndex İşlemleri


        public ActionResult Social()
        {
            return View();
        
        }

    
    // Socialİndex İşlemleri


        //Wiki İşlemleri


        public ActionResult Wiki()
        {
            return View();

        }


        //Wiki İşlemleri

	}
}