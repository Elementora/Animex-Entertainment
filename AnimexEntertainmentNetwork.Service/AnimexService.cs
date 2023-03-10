using AnimexEntertainmentNetwork.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AnimexEntertainmentNetwork.Service
{
    public class AnimexService
    {
        Animex_EntertainmentEntities1 db = new Animex_EntertainmentEntities1();


//Kullanıcı Bölümü

        //Kullanıcı Listele
        public List<USER> User_TB_Select()
        {

            return (from m in db.USER select m).ToList();

        }

        //Kullanıcı Listele

        //Kullanıcı Ekle
        public void USER_insert(string USER_NAME,string PASSWORD,string E_MAIL)
        {
            USER ekle = new USER();
            ekle.USER_NAME = USER_NAME;
            ekle.PASSWORD = PASSWORD;
            ekle.E_MAIL = E_MAIL;
            ekle.CINSIYET = "";

            db.AddToUSER(ekle);
            db.SaveChanges();

        }
        //Kullanıcı Ekle

        //Kullanıcı Sil
        public void USER_TB_delete(int ID)
        {
            var kontrol = User_TB_Select().Where(m => m.ID == ID).First();
            db.DeleteObject(kontrol);
            db.SaveChanges();
        }


        //Kullanıcı Sil


        //Kullanıcı Güncelle

        public void USER_TB_update(string USER_NAME,string PASSWORD, string E_MAIL ,int ID)
        {
            var cek = (from m in db.USER where m.ID == ID select m).First();

            cek.USER_NAME = USER_NAME;
            cek.PASSWORD = PASSWORD;
            cek.E_MAIL = E_MAIL;

            db.SaveChanges();

        }

        //Kullanıcı Güncelle


        //Kullanıcı Profil Ayarları Güncelle

        public void USER_TB_profile_update(string USER_NAME , string Rumuz , string E_MAIL , string PASSWORD , string CINSIYET , string ABOUT , DateTime DATE_OF_BIRTH ,string USER)
        {
            var profilguncelleme = (from m in db.USER where USER_NAME == USER select m).First();

            profilguncelleme.RUMUZ = Rumuz;
            profilguncelleme.E_MAIL=E_MAIL;
            profilguncelleme.PASSWORD=PASSWORD;
            profilguncelleme.CINSIYET=CINSIYET;
            profilguncelleme.ABOUT=ABOUT;
            profilguncelleme.DATE_OF_BIRTH = DATE_OF_BIRTH;


            db.SaveChanges();

        }

        //Kullanıcı Profil Ayarları Güncelle


        //Kullanıcı Foto Güncelle

        public void USER_TB_FotoUpdate(string FotoFile)
        {

            var kontrol = User_TB_Select().First();
            kontrol.USER_IMG = FotoFile;
            db.SaveChanges();
             

        }

        //Kullanıcı Foto Güncelle



//Kullanıcı Bölümü

 //Haber Bölümü


        //Yeni
        public List<News_TB> NewsTB_Select()
        {

            return (from m in db.News_TB orderby m.ID descending select m).ToList();
        }

        public List<News_TB> HaberTB_selectAnasayfa()
        {
            return (from m in db.News_TB orderby m.ID descending select m).ToList();
        }


      
        public List<News_TB>RastgeleListele()
        {
           
            return (from m in db.News_TB select m).OrderByDescending(p => Guid.NewGuid()).ToList();

        }



        public Int64 NewsTB_Report(int ID)
        {
            var kontrol = NewsTB_Select().Where(m => m.ID == ID).First();
            kontrol.News_Reading++;
            db.SaveChanges();
            return (kontrol.ID);

        }



        public void NewsTB_insert(int ID, string HaberBaslik, string HaberDetay)
        {
            var kontol = CategoryTB_Select().Where(m => m.ID == ID).First();

            News_TB ekle = new News_TB();

            ekle.CategoryID = kontol.ID;
            ekle.Category_Name = kontol.Category_Name;

            ekle.News_Title = HaberBaslik;
            ekle.News_Description = HaberDetay;
            ekle.News_IMG = "";
            ekle.News_Create_Date = DateTime.Now;
            ekle.News_Reading = 0;
            ekle.News_Comment_Number = 0;
            ekle.Statu = 0;


            db.AddToNews_TB(ekle);
            db.SaveChanges();
        }

        public void NewsTB_delete(int ID)
        {
            var kontrol = NewsTB_Select().Where(m => m.ID == ID).First();
            db.DeleteObject(kontrol);
            db.SaveChanges();

        }
        public Int64 NewsTB_update(int ID, string HaberBaslik, string HaberDetay ,string PostEtiketi, string SonKullanici)
        {

            var kontrol = NewsTB_Select().Where(m => m.ID == ID).First();
            kontrol.News_Title = HaberBaslik;
            kontrol.News_Description = HaberDetay;
            kontrol.News_Tag = PostEtiketi;
            kontrol.LastUpdateUser = SonKullanici;

            db.SaveChanges();
            return kontrol.ID;

        }

        public Int64 SliderTB_update_haber(int ID,string HaberBaslik, string HaberDetay)
        {
            var kontrol = SliderTB_Select().Where(m => m.News_ID == ID).First();
            kontrol.News_Title=HaberBaslik;
            kontrol.News_Description = HaberDetay;

            db.SaveChanges();
            return kontrol.ID;
            
        }

         public void NewsTB_AnasayfaStatuUpdate(int Haberid, int Statu)
       {
           var kontrol = NewsTB_Select().Where(m => m.ID == Haberid).First();
           kontrol.Statu = Statu;
           db.SaveChanges();

       }





        

        //Kategori

        public List<Category_TB> CategoryTB_Select()
        {

            return (from m in db.Category_TB orderby m.ID descending select m).ToList();
        }


        public void CategoryTB_insert(string KategoriAdi)
        {

            Category_TB ekle = new Category_TB();

            ekle.Category_Name = KategoriAdi;




            db.AddToCategory_TB(ekle);
            db.SaveChanges();
        }

        public Int64 CategoryTB_update(int ID, string KategoriAdi)
        {
            var kontrol = CategoryTB_Select().Where(m => m.ID == ID).First();
            kontrol.Category_Name = KategoriAdi;

            db.SaveChanges();
            return kontrol.ID;

        }

        public void CategoryTB_delete(int ID)
        {

            var kontrol = CategoryTB_Select().Where(m => m.ID == ID).First();
            db.DeleteObject(kontrol);
            db.SaveChanges();
        }

        //Kategori

        //Yeni




        // Post Resim Ekleme İşlemleri

        public List<Blog_Foto_TB> Blog_FotoTB_Select()
        {

            return (from m in db.Blog_Foto_TB orderby m.FotoID descending select m).ToList();
        }

        public void Blog_FotoTB_insert(int ID, bool HaberDefaultFoto, string FotoFile, string haberResimAdi)
        {
            var kontrol = NewsTB_Select().Where(m => m.ID == ID).First();
            Blog_Foto_TB foto = new Blog_Foto_TB();
            foto.Default_Haber_Foto = HaberDefaultFoto;
            foto.FotoURL = FotoFile;
            foto.FotoName = haberResimAdi;
            foto.PostID = kontrol.ID;

            db.AddToBlog_Foto_TB(foto);
            db.SaveChanges();


        }

        public void Blog_FotoTB_Delete(int ID)
        {
            var kontrol = Blog_FotoTB_Select().Where(m => m.FotoID == ID).First();


            db.DeleteObject(kontrol);
            db.SaveChanges();

        }




        // Post Resim Ekleme İşlemleri


        //Default Haber FOTO



        public List<Blog_Foto_TB> UrunFotoTB_list_fotoid(int FotoID)
        {
            return (from m in db.Blog_Foto_TB where m.FotoID == FotoID select m).ToList();

        }


        public List<Blog_Foto_TB> UrunFotoTB_list(int PostID)
        {
            return (from m in db.Blog_Foto_TB where m.PostID == PostID select m).ToList();

        }

        public void UrunFotoTB_update(int FotoID, int PostID, bool Default_Haber_Foto)
        {

            var kontrol = Blog_FotoTB_Select().Where(m => m.PostID == PostID && m.FotoID == FotoID).First();



            kontrol.Default_Haber_Foto = Default_Haber_Foto;

            db.SaveChanges();

        }


        //Default Haber FOTO






        //<!- Sidebar Ufak Resim ->

        public List<Sidebar_News_Foto_TB> Sidebar_News_FotoTB_Select()
        {

            return (from m in db.Sidebar_News_Foto_TB orderby m.FotoID descending select m).ToList();
        }


        public void Sidebar_News_FotoTB_insert(int ID, bool HaberDefaultFoto, string FotoFile, string haberResimAdi)
        {

            var kontrol = NewsTB_Select().Where(m => m.ID == ID).First();

            Sidebar_News_Foto_TB sidebarnewsfoto = new Sidebar_News_Foto_TB();
            sidebarnewsfoto.Default_Haber_Foto = HaberDefaultFoto;
            sidebarnewsfoto.FotoURL = FotoFile;
            sidebarnewsfoto.FotoName = haberResimAdi;
            sidebarnewsfoto.PostID = kontrol.ID;

            db.AddToSidebar_News_Foto_TB(sidebarnewsfoto);
            db.SaveChanges();
        }


        public void Sidebar_News_FotoTB_delete(int ID)
        {

            var kontrol = Sidebar_News_FotoTB_Select().Where(m => m.FotoID == ID).First();


            db.DeleteObject(kontrol);
            db.SaveChanges();
        }

        //<!- Sidebar Ufak Resim ->

        //Default Haber FOTO Sidebar

        public List<Sidebar_News_Foto_TB> SidebarFotoTB_list_fotoid(int FotoID)
        {

            return (from m in db.Sidebar_News_Foto_TB where m.FotoID == FotoID select m).ToList();
        }

        public List<Sidebar_News_Foto_TB> SidebarFotoTB_list(int PostID)
        {
            return (from m in db.Sidebar_News_Foto_TB where m.PostID == PostID select m).ToList();

        }

        public void  Sidebar_News_Foto_TB_update(int FotoID,int PosID,bool Default_Haber_Foto)
        {
            var kontrol = Sidebar_News_FotoTB_Select().Where(m => m.PostID == PosID && m.FotoID == FotoID).First();

            kontrol.Default_Haber_Foto = Default_Haber_Foto;
            db.SaveChanges();

        }
        //Default Haber FOTO Sidebar



        //Sidebar Banner



        public List<Sidebar_Banner_img_TB> Sidebar_Banner_img_TB_Select()
        {

            return (from m in db.Sidebar_Banner_img_TB orderby m.FotoID descending select m).ToList();
        }




        public void Sidebar_Banner_insert(bool DefaultFoto, string FotoFile, string haberResimAdi)
        {



            Sidebar_Banner_img_TB sidebarbannerfoto = new Sidebar_Banner_img_TB();
            sidebarbannerfoto.Default_Foto = DefaultFoto;
            sidebarbannerfoto.FotoURL = FotoFile;
            sidebarbannerfoto.FotoName = haberResimAdi;


            db.AddToSidebar_Banner_img_TB(sidebarbannerfoto);
            db.SaveChanges();
        }

        public void Sidebar_Banner_delete(int ID)
        {

            var kontrol = Sidebar_Banner_img_TB_Select().Where(m => m.FotoID == ID).First();


            db.DeleteObject(kontrol);
            db.SaveChanges();
        }

        public void SidebarBanner_AnasayfaStatuUpdate(int Fotoid, int Statu)
        {
            var kontrol = Sidebar_Banner_img_TB_Select().Where(m => m.FotoID == Fotoid).First();
            kontrol.Statu = Statu;
            db.SaveChanges();

        }

        public Int64 Sidebar_Banner_Note_Update(int FotoID, string FotoNote)
        {
            var kontrol = Sidebar_Banner_img_TB_Select().Where(m => m.FotoID == FotoID).First();
            kontrol.FotoNote = FotoNote;

            db.SaveChanges();
            return kontrol.FotoID;

        }

        public Int64 Sidebar_Banner_Link_Update(int FotoID, string FotoLink)
        {
            var kontrol = Sidebar_Banner_img_TB_Select().Where(m => m.FotoID == FotoID).First();
            kontrol.Link = FotoLink;

            db.SaveChanges();
            return kontrol.FotoID;

        }
       

        //Default Banner FOTO Sidebar
        public List<Sidebar_Banner_img_TB> SidebarBannerFotoTB_list_fotoid(int FotoID)
        {

            return (from m in db.Sidebar_Banner_img_TB where m.FotoID == FotoID select m).ToList();
        }


        public List<Sidebar_Banner_img_TB> SidebarBannerFotoTB_list(int FotoID)
        {
            return (from m in db.Sidebar_Banner_img_TB where m.FotoID == FotoID select m).ToList();

        }



        public void Sidebar_Banner_Foto_TB_update(int FotoID, bool DefaultFoto)
        {
            var kontrol = Sidebar_Banner_img_TB_Select().Where(m => m.FotoID == FotoID).First();

            kontrol.Default_Foto = DefaultFoto;
            db.SaveChanges();

        }

        //Default Banner FOTO Sidebar


        //Sidebar Banner


        //Yorum İşlemleri
        public List<Comment_TB> CommentTB_Select()
        {

            return (from m in db.Comment_TB orderby m.CommentID descending select m).ToList();

        }

        public void CommentTB_insert(int NewsID,string UserName,string CommentDescription,string Commentip,string Browser)
        {

            var kontrol = NewsTB_Select().Where(m => m.ID == NewsID).First();
            var kontrol2 = User_TB_Select().Where(m => m.ID == m.ID).First();

            Comment_TB yorum = new Comment_TB();
            yorum.NewsID = NewsID;
            yorum.News_Title = kontrol.News_Title;
            yorum.User_Name = kontrol2.USER_NAME;
            yorum.User_Email = kontrol2.E_MAIL;
            yorum.User_IMG = kontrol2.USER_IMG;
            yorum.User_Comment = CommentDescription;
            yorum.Category_Name = kontrol.Category_Name;
            yorum.CategoryID = kontrol.CategoryID;
            yorum.Comment_Date = DateTime.Now;
            yorum.Comment_IP = Commentip;
            yorum.Browser = Browser;
            yorum.Status=1;
            yorum.Comment_Report = 0;
            yorum.Comment_Like = 0;
            yorum.Comment_Dislike = 0;


            db.AddToComment_TB(yorum);
            db.SaveChanges();

        }

        public Int64 YorumOkumaEkle(int ID)
        {
            var kontrol = NewsTB_Select().Where(m => m.ID == ID).First();
            kontrol.News_Comment_Number++;
            db.SaveChanges();
            return (kontrol.ID);

            
        }

        public Int64 CommentTB_update(int ID,string CommentDescription)
        {
            var kontrol = CommentTB_Select().Where(m => m.CommentID == ID).First();
            kontrol.User_Comment = CommentDescription;

            db.SaveChanges();
            return(kontrol.CommentID);

        }

        public Int64 CommentTB_apporval(int ID)
        {
            var kontrol = CommentTB_Select().Where(m => m.CommentID == ID).First();
            kontrol.Status = 1;

            db.SaveChanges();
            return (kontrol.CommentID);

        }

        public Int64 CommentTB_disapporval(int ID)
        {
            var kontrol = CommentTB_Select().Where(m => m.CommentID == ID).First();
            kontrol.Status = 0;

            db.SaveChanges();
            return (kontrol.CommentID);

        }

        public Int64 CommentTB_report(int ID)
        {
            var kontrol = CommentTB_Select().Where(m => m.CommentID == ID).First();
            kontrol.Comment_Report++;
            db.SaveChanges();
            return (kontrol.CommentID);

        }

        public Int64 YorumLike(int ID)
        {
            var kontrol = CommentTB_Select().Where(m => m.CommentID == ID).First();
            kontrol.Comment_Like++;
            db.SaveChanges();
            return (kontrol.CommentID);
        }

        public Int64 YorumDislike(int ID)
        {
            var kontrol = CommentTB_Select().Where(m => m.CommentID == ID).First();
            kontrol.Comment_Dislike--;
            db.SaveChanges();
            return (kontrol.CommentID);

        }
        

        public void CommentTB_delete(int ID)
        {
            var kontrol = CommentTB_Select().Where(m => m.CommentID == ID).First();
            db.DeleteObject(kontrol);
            db.SaveChanges();
        }
        //Yorum İşlemleri


        //Slider Kategori İşlemleri
        
            public List<Slider_Category_TB> SliderCategoryTB_Select()
        {
            return (from m in db.Slider_Category_TB orderby m.SliderCategoryID descending select m).ToList();

        }



            public void SliderCategoryTB_insert(string SliderCategoryName, string SliderCategoryNote)
            {

                Slider_Category_TB sliderkategori = new Slider_Category_TB();
                sliderkategori.SliderCategoryName = SliderCategoryName;
                sliderkategori.SliderCategoryNote = SliderCategoryNote;

                db.AddToSlider_Category_TB(sliderkategori);
                db.SaveChanges();
            }

            public void SliderCategoryTB_delete(int ID)
                {
                    var kontrol = SliderCategoryTB_Select().Where(m => m.SliderCategoryID == ID).First();
                    db.DeleteObject(kontrol);
                    db.SaveChanges();
                }

            public Int64 SliderCategoryTB_update(int ID, string SliderCategoryName, string SliderCategoryNote)
            {
                var kontrol = SliderCategoryTB_Select().Where(m => m.SliderCategoryID == ID).First();
                kontrol.SliderCategoryName = SliderCategoryName;
                kontrol.SliderCategoryNote = SliderCategoryNote;

                db.SaveChanges();
                return(kontrol.SliderCategoryID);
        }

            public Int64 SliderYerNotu_update(int ID, string SliderCategoryNote)
            {
                var kontrol = SliderCategoryTB_Select().Where(m => m.SliderCategoryID == ID).First();
                kontrol.SliderCategoryNote = SliderCategoryNote;

                db.SaveChanges();
                return (kontrol.SliderCategoryID);

            }
      //Slider Kategori İşlemleri



        //Slider İşlemleri
        public List<Slider_TB>SliderTB_Select()
            {

                return (from m in db.Slider_TB orderby m.ID descending select m).ToList();
            }

        public List<Slider_TB> Slider_list_SELECT(int ID)
        {
            return (from m in db.Slider_TB where m.ID == ID select m).ToList();

        }

        public List<Slider_TB> Slider_list_SliderID(int SliderID)
        {
            return (from m in db.Slider_TB where m.SliderCategoryID == SliderID select m).ToList();

        }

        public void Slider_default_update(int ID, int SliderID, int Status)
        {

            var kontrol = SliderTB_Select().Where(m => m.SliderCategoryID == SliderID && m.ID == ID).First();



            kontrol.Status = Status;

            db.SaveChanges();

        }



        //

        public void SliderTB_insert(int SliderhaberbaslikID, int yerID)
        {

            var kontrol = SliderCategoryTB_Select().Where(m => m.SliderCategoryID == yerID).First();
            var kontrol2 = NewsTB_Select().Where(m => m.ID == SliderhaberbaslikID).First();
            

            Slider_TB slider = new Slider_TB();
            slider.SliderCategoryID = kontrol.SliderCategoryID;
            slider.News_ID = kontrol2.ID;
            slider.News_Title = kontrol2.News_Title;
            slider.News_Description = kontrol2.News_Description;
            slider.Status = 0;
            

            db.AddToSlider_TB(slider);
            db.SaveChanges();

        }


        public void SliderTB_delete(int ID)
        {
            var kontrol = SliderTB_Select().Where(m => m.ID == ID).First();
            db.DeleteObject(kontrol);
            db.SaveChanges();

        }

        //Slider İşlemleri


        //Slider FotoEkleme

        public List<Slider_img_TB> SliderTB_Foto_Select()
        {

            return (from m in db.Slider_img_TB orderby m.FotoID descending select m).ToList();
        }


        public void Slider_Foto_insert(int ID,bool HaberDefaultFoto, string FotoFile, string haberResimAdi)
        {

            var kontrol = SliderTB_Select().Where(m => m.ID == ID).First();

            Slider_img_TB sliderfoto = new Slider_img_TB();
            sliderfoto.Default_Haber_Foto = HaberDefaultFoto;
            sliderfoto.FotoURL = FotoFile;
            sliderfoto.FotoName = haberResimAdi ;
            sliderfoto.SliderID = kontrol.ID;

            db.AddToSlider_img_TB(sliderfoto);
            db.SaveChanges();
        }


        public void SliderFoto_delete(int ID)
        {

            var kontrol = SliderTB_Foto_Select().Where(m => m.FotoID == ID).First();


            db.DeleteObject(kontrol);
            db.SaveChanges();
        }


        //Slider FotoEkleme


        //Slider Foto İşlemleri
     

        public List<Slider_img_TB> SliderFotoTB_list_fotoid(int FotoID)
        {
            return (from m in db.Slider_img_TB where m.FotoID == FotoID select m).ToList();

        }


        public List<Slider_img_TB> SliderFotoTB_list(int SliderID)
        {
            return (from m in db.Slider_img_TB where m.SliderID == SliderID select m).ToList();

        }

        public void SliderFotoTB_update(int FotoID, int SliderID, bool Default_Haber_Foto)
        {

            var kontrol = SliderTB_Foto_Select().Where(m => m.SliderID == SliderID && m.FotoID == FotoID).First();

          

            kontrol.Default_Haber_Foto = Default_Haber_Foto;

            db.SaveChanges();

        }


        //Slider Foto İşlemleri

        //Etiket İşlemleri
        public List<Tag_TB> TagTB_Select()
        {
            return (from m in db.Tag_TB orderby m.TagID descending select m).ToList();

        }


        public void TagTB_insert(string TagName)
        {
            Tag_TB ekle = new Tag_TB();
            ekle.TagName = TagName;

            db.AddToTag_TB(ekle);
            db.SaveChanges();

        }

        public List<Etiketler_TB> Etiket_Select()
        {
            return (from m in db.Etiketler_TB orderby m.EtiketID descending select m).ToList();

        }
        //yeni



        public List<Etiketler_TB> Etiketler_TB_select(int postid)
        {


            return (from m in db.Etiketler_TB where m.NewsID == postid select m).ToList();


        }


        public void Etiketler_TB_insert(int ID, string etiketid)
        {

            Etiketler_TB etb = new Etiketler_TB();
            var tagismi = TagTB_Select().Where(m => m.TagID == Convert.ToInt32(etiketid)).First();

            etb.EtiketAdi = etiketid;
            etb.Tag = tagismi.TagName;
            etb.NewsID = ID;



            db.AddToEtiketler_TB(etb);
            db.SaveChanges();



        }


        public void Etiketler_TB_delete(int eid)
        {

            var aa = (from m in db.Etiketler_TB where m.EtiketID == eid select m).First();

            db.DeleteObject(aa);
            db.SaveChanges();
        }

        public void Etiketlertoplu_TB_insert(int ID, string etiket)
        {
            string[] etiketdizi = etiket.Split(',');
            Etiketler_TB etb = new Etiketler_TB();

            foreach (var item in etiketdizi)
            {
                Etiketler_TB_insert(ID, item);
            }


        }

        public List<Tag_TB> Tag_TB_select(int eid)
        {
            return (from m in db.Tag_TB where m.TagID == eid select m).ToList();

        }


        public string Tag_adiListesiOlustur(string etiketidleri)
        {
            string etikettoplu = "";
            string[] etiketler = etiketidleri.Split(',');
            foreach (var item in etiketler)
            {
                var cek = Tag_TB_select(Convert.ToInt32(item)).First();
                etikettoplu += cek.TagName + ",";
            }

            return etikettoplu;
        }



        //Etiket İşlemleri


        //Diğer Haberler Foto İşlemleri (Haber Detay da)



        public List<Other_News_Foto_TB> Other_News_FotoTB_Select()
        {

            return (from m in db.Other_News_Foto_TB orderby m.FotoID descending select m).ToList();
        }


        public void Other_News_FotoTB_insert(int ID, bool HaberDefaultFoto, string FotoFile, string haberResimAdi)
        {

            var kontrol = NewsTB_Select().Where(m => m.ID == ID).First();

            Other_News_Foto_TB othernewsfoto = new Other_News_Foto_TB();
            othernewsfoto.Default_Haber_Foto = HaberDefaultFoto;
            othernewsfoto.FotoURL = FotoFile;
            othernewsfoto.FotoName = haberResimAdi;
            othernewsfoto.PostID = kontrol.ID;

            db.AddToOther_News_Foto_TB(othernewsfoto);
            db.SaveChanges();
        }




        public void Other_News_FotoTB_delete(int ID)
        {

            var kontrol = Other_News_FotoTB_Select().Where(m => m.FotoID == ID).First();


            db.DeleteObject(kontrol);
            db.SaveChanges();
        }



        //Default Haber FOTO Other

        public List<Other_News_Foto_TB> OtherFotoTB_list_fotoid(int FotoID)
        {

            return (from m in db.Other_News_Foto_TB where m.FotoID == FotoID select m).ToList();
        }

        public List<Other_News_Foto_TB> OtherFotoTB_list(int PostID)
        {
            return (from m in db.Other_News_Foto_TB where m.PostID == PostID select m).ToList();

        }

        public void Other_News_Foto_TB_update(int FotoID, int PosID, bool Default_Haber_Foto)
        {
            var kontrol = Other_News_FotoTB_Select().Where(m => m.PostID == PosID && m.FotoID == FotoID).First();

            kontrol.Default_Haber_Foto = Default_Haber_Foto;
            db.SaveChanges();

        }
        //Default Haber FOTO Other


        //Diğer Haberler Foto İşlemleri END


        //Hakkımızda Animex Nedir ?

       public  List<About_Us_TB>AboutUs_TB_Select()
        {
            return (from m in db.About_Us_TB orderby m.AboutUsID descending select m).ToList();
        }

       public Int64 About_Us_update(int AboutUsID, string AboutUs_Title,string  AboutUs_Detail)
        {
            var kontrol = AboutUs_TB_Select().Where(m => m.AboutUsID == AboutUsID).First();
            kontrol.AboutUs_Title = AboutUs_Title;
            kontrol.AboutUs_Detail = AboutUs_Detail;

            db.SaveChanges();
            return kontrol.AboutUsID;

        }


        //Hakkımızda Animex Nedir ?



        //Kullanım Şartları

       public List<Terms_of_Use_TB> TermsofUse_TB_Select()
       {
           return (from m in db.Terms_of_Use_TB orderby m.SartID descending select m).ToList();
       }

       public Int64 TermofUse_update(int SartID, string KullanimSartlari)
       {
           var kontrol = TermsofUse_TB_Select().Where(m => m.SartID == SartID).First();
           kontrol.Kullanim_Sartlari = KullanimSartlari;

           db.SaveChanges();
           return kontrol.SartID;

       }

        //Kullanım Şartları


        //Gizlilik Politikası

       public List<Privacy_Policy_TB> Privacy_Policy_TB_Select()
       {
           return (from m in db.Privacy_Policy_TB orderby m.PolitikaID descending select m).ToList();

       }

        public Int64 Privacy_Policy_uptade(int PolitikaID,string Gizlilik_Politikasi)
       {
           var kontrol = Privacy_Policy_TB_Select().Where(m => m.PolitikaID == PolitikaID).First();
           kontrol.Gizlilik_Politikasi_Detay = Gizlilik_Politikasi;

           db.SaveChanges();
           return kontrol.PolitikaID;

       }


        //Gizlilik Politikası


        //İkon ve Banner Sayfası

        public List<icon_banner_page_TB> iconbanner_TB_Select()
        {
            return (from m in db.icon_banner_page_TB orderby m.iconbannerID descending select m).ToList();
        }

        public Int64 iconbanner_update(int iconbannerID, string iconbannerdetay)
        {
            var kontrol = iconbanner_TB_Select().Where(m => m.iconbannerID == iconbannerID).First();
            kontrol.iconbannerdetay = iconbannerdetay;

            db.SaveChanges();
            return kontrol.iconbannerID;

        }


        //İkon ve Banner Sayfası


        //Developer Blog
        public List<Developer_Blog_TB> Developer_Blog_Select()
        {

            return (from m in db.Developer_Blog_TB orderby m.DevBlogID descending select m).ToList();
        }

        public void Developer_Blog_insert(string Dev_Blog_Description,string Developer)
        {

            var kontrol = User_TB_Select().Where(m=>m.USER_NAME==Developer).First();

            Developer_Blog_TB devblog = new Developer_Blog_TB();

            devblog.DevBlog_Description = Dev_Blog_Description;

            devblog.Dev_Create_User_Name = kontrol.USER_NAME;

            devblog.Dev_Create_User_Photo = kontrol.USER_IMG;

            devblog.Dev_Blog_Date = DateTime.Now;

            db.AddToDeveloper_Blog_TB(devblog);
            db.SaveChanges();

        }

        public Int64 Developer_Blog_update(int ID,string Dev_Blog_Description,string Developer)
        {
            var kontrol = Developer_Blog_Select().Where(m => m.DevBlogID == ID).First();
           
            kontrol.DevBlog_Description = Dev_Blog_Description;
            kontrol.Dev_Blog_Update_User = Developer;

            db.SaveChanges();
            return (kontrol.DevBlogID);

        }

        public void Developer_Blog_delete(int ID)
        {
            var kontrol = Developer_Blog_Select().Where(m => m.DevBlogID == ID).First();

            db.DeleteObject(kontrol);
            db.SaveChanges();

        }

        //Developer Blog



        //İletişim İşlemleri


        public void AEN_iletisim_insert(string mail)
        {


            MailMessage mailadres = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("mail.animexentertainment.com");

            mailadres.From = new MailAddress("info@animexentertainment.com","Animex Entertainment Network");
            mailadres.To.Add("animexentertainment@gmail.com");
            mailadres.Subject = "Animex Entertainment Network İletişim";
            mailadres.Body = mail;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("info@animexentertainment.com", "60951992aQ");
            //SmtpServer.EnableSsl = true;
            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mailadres);

        }


    }
    
}
