using AnimexEntertainmentNetwork.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnimexEntertainmentNetwork.Web.Models;

namespace AnimexEntertainmentNetwork.Web.Content.Class
{
    public class Kontrol
    {
        public static AnimexService _roleService
        {
            get
            {
                return DependencyResolver.Current.GetService<AnimexService>();
            }
        }


     
    
        public class News_TB
                {

                    public int ID { get; set; }
                    public string News_Title  { get; set; }
                    public string News_Description { get; set; }
                    public DateTime Date{ get; set; }
                    public int Statu  { get; set; }
                    public string News_Foto { get; set; }
                    public int CategoryID { get; set; }
                    public string Category_Name { get; set; }

         
                }

        public static List<News_TB> NewsTB_get()
        {

            List<News_TB> detay = new List<News_TB>();
            var cek = _roleService.HaberTB_selectAnasayfa().Where(m => m.Statu == 1);

            foreach (var item in cek)
	            {

                    News_TB postman = new News_TB
                    {
                        ID=item.ID,
                        News_Title=item.News_Title,
                        News_Description=item.News_Description,
                        Date = item.News_Create_Date.Value,
                        Statu = item.Statu.Value,
                        //News_Foto=item.News_Foto,
                        CategoryID = item.CategoryID.Value,
                        Category_Name=item.Category_Name,
                        

                    };
                            detay.Add(postman);
		 
	            }

            return detay;

        }



        public class CategoryTBSelect
        {

            public int ID { get; set; }
            public string News_Title { get; set; }
            public string News_Description { get; set; }
            public DateTime Date { get; set; }
            public int Statu { get; set; }
            public string News_Foto { get; set; }
            public int CategoryID { get; set; }
            public string Category_Name { get; set; }


        }


        public static List<CategoryTBSelect>Kat_get(int CategoryID)
        {

            List<CategoryTBSelect> detay = new List<CategoryTBSelect>();
            var cek = _roleService.NewsTB_Select().Where(m => m.CategoryID == CategoryID).ToList();
        
           
            foreach (var item in cek)
            {

                CategoryTBSelect postman = new CategoryTBSelect
                {
                    ID=item.ID,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Date = item.News_Create_Date.Value,
                    Statu = item.Statu.Value,
                    CategoryID = item.CategoryID.Value,
                    Category_Name = item.Category_Name,


                };
                detay.Add(postman);

            }

            return detay;

        }










        public static List<News_TB> RandomHaber_Get()
        {

            List<News_TB> detay = new List<News_TB>();



            var cek = _roleService.RastgeleListele().Where(m => m.Statu == 1);




            foreach (var item in cek)
            {

                News_TB postman = new News_TB
                {
                    ID = item.ID,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Date = item.News_Create_Date.Value,
                    Statu = item.Statu.Value,
                    //News_Foto=item.News_Foto,
                    CategoryID = item.CategoryID.Value,
                    Category_Name = item.Category_Name,


                };
                detay.Add(postman);

            }

            return detay;

        }






        public static List<News_TB>Animekat_get()
        {
            List<News_TB> detay = new List<News_TB>();
            var cek = _roleService.HaberTB_selectAnasayfa().Where(m => m.Statu == 1 && m.CategoryID==1);

            
            foreach (var item in cek)
	            {

                    News_TB postman = new News_TB
                    {
                        ID=item.ID,
                        News_Title=item.News_Title,
                        News_Description=item.News_Description,
                        Date = item.News_Create_Date.Value,
                        Statu = item.Statu.Value,
                        //News_Foto=item.News_Foto,
                        CategoryID = item.CategoryID.Value,
                        Category_Name=item.Category_Name,
                        

                    };
                            detay.Add(postman);
		 
	            }

            return detay;

        }



        public static List<News_TB> Mangakat_get()
        {
            List<News_TB> detay = new List<News_TB>();
            var cek = _roleService.HaberTB_selectAnasayfa().Where(m => m.Statu == 1 && m.CategoryID == 2);


            foreach (var item in cek)
            {

                News_TB postman = new News_TB
                {
                    ID = item.ID,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Date = item.News_Create_Date.Value,
                    Statu = item.Statu.Value,
                    //News_Foto=item.News_Foto,
                    CategoryID = item.CategoryID.Value,
                    Category_Name = item.Category_Name,


                };
                detay.Add(postman);

            }

            return detay;

        }



        public static List<News_TB> Oyunkat_get()
        {
            List<News_TB> detay = new List<News_TB>();
            var cek = _roleService.HaberTB_selectAnasayfa().Where(m => m.Statu == 1 && m.CategoryID == 3);


            foreach (var item in cek)
            {

                News_TB postman = new News_TB
                {
                    ID = item.ID,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Date = item.News_Create_Date.Value,
                    Statu = item.Statu.Value,
                    //News_Foto=item.News_Foto,
                    CategoryID = item.CategoryID.Value,
                    Category_Name = item.Category_Name,


                };
                detay.Add(postman);

            }

            return detay;

        }



        public static List<News_TB> Cosplaykat_get()
        {
            List<News_TB> detay = new List<News_TB>();
            var cek = _roleService.HaberTB_selectAnasayfa().Where(m => m.Statu == 1 && m.CategoryID == 4);


            foreach (var item in cek)
            {

                News_TB postman = new News_TB
                {
                    ID = item.ID,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Date = item.News_Create_Date.Value,
                    Statu = item.Statu.Value,
                    //News_Foto=item.News_Foto,
                    CategoryID = item.CategoryID.Value,
                    Category_Name = item.Category_Name,


                };
                detay.Add(postman);

            }

            return detay;

        }




        public static List<News_TB> Etkinlikkat_get()
        {
            List<News_TB> detay = new List<News_TB>();
            var cek = _roleService.HaberTB_selectAnasayfa().Where(m => m.Statu == 1 && m.CategoryID == 5);


            foreach (var item in cek)
            {

                News_TB postman = new News_TB
                {
                    ID = item.ID,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Date = item.News_Create_Date.Value,
                    Statu = item.Statu.Value,
                    //News_Foto=item.News_Foto,
                    CategoryID = item.CategoryID.Value,
                    Category_Name = item.Category_Name,


                };
                detay.Add(postman);

            }

            return detay;

        }



        public static List<News_TB> Sanatkat_get()
        {
            List<News_TB> detay = new List<News_TB>();
            var cek = _roleService.HaberTB_selectAnasayfa().Where(m => m.Statu == 1 && m.CategoryID == 6);


            foreach (var item in cek)
            {

                News_TB postman = new News_TB
                {
                    ID = item.ID,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Date = item.News_Create_Date.Value,
                    Statu = item.Statu.Value,
                    //News_Foto=item.News_Foto,
                    CategoryID = item.CategoryID.Value,
                    Category_Name = item.Category_Name,


                };
                detay.Add(postman);

            }

            return detay;

        }




        public static List<News_TB> Ozelkat_get()
        {
            List<News_TB> detay = new List<News_TB>();
            var cek = _roleService.HaberTB_selectAnasayfa().Where(m => m.Statu == 1 && m.CategoryID == 7);


            foreach (var item in cek)
            {

                News_TB postman = new News_TB
                {
                    ID = item.ID,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Date = item.News_Create_Date.Value,
                    Statu = item.Statu.Value,
                    //News_Foto=item.News_Foto,
                    CategoryID = item.CategoryID.Value,
                    Category_Name = item.Category_Name,


                };
                detay.Add(postman);

            }

            return detay;

        }






        public static List<News_TB> Figurkat_get()
        {
            List<News_TB> detay = new List<News_TB>();
            var cek = _roleService.HaberTB_selectAnasayfa().Where(m => m.Statu == 1 && m.CategoryID == 8);


            foreach (var item in cek)
            {

                News_TB postman = new News_TB
                {
                    ID = item.ID,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Date = item.News_Create_Date.Value,
                    Statu = item.Statu.Value,
                    //News_Foto=item.News_Foto,
                    CategoryID = item.CategoryID.Value,
                    Category_Name = item.Category_Name,


                };
                detay.Add(postman);

            }

            return detay;

        }






        public static List<News_TB> Yemekkat_get()
        {
            List<News_TB> detay = new List<News_TB>();
            var cek = _roleService.HaberTB_selectAnasayfa().Where(m => m.Statu == 1 && m.CategoryID == 9);


            foreach (var item in cek)
            {

                News_TB postman = new News_TB
                {
                    ID = item.ID,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Date = item.News_Create_Date.Value,
                    Statu = item.Statu.Value,
                    //News_Foto=item.News_Foto,
                    CategoryID = item.CategoryID.Value,
                    Category_Name = item.Category_Name,


                };
                detay.Add(postman);

            }

            return detay;

        }





        public static List<News_TB> Filmkat_get()
        {
            List<News_TB> detay = new List<News_TB>();
            var cek = _roleService.HaberTB_selectAnasayfa().Where(m => m.Statu == 1 && m.CategoryID == 10);


            foreach (var item in cek)
            {

                News_TB postman = new News_TB
                {
                    ID = item.ID,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Date = item.News_Create_Date.Value,
                    Statu = item.Statu.Value,
                    //News_Foto=item.News_Foto,
                    CategoryID = item.CategoryID.Value,
                    Category_Name = item.Category_Name,


                };
                detay.Add(postman);

            }

            return detay;

        }





        public static List<News_TB> Vocaloidkat_get()
        {
            List<News_TB> detay = new List<News_TB>();
            var cek = _roleService.HaberTB_selectAnasayfa().Where(m => m.Statu == 1 && m.CategoryID == 11);


            foreach (var item in cek)
            {

                News_TB postman = new News_TB
                {
                    ID = item.ID,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Date = item.News_Create_Date.Value,
                    Statu = item.Statu.Value,
                    //News_Foto=item.News_Foto,
                    CategoryID = item.CategoryID.Value,
                    Category_Name = item.Category_Name,


                };
                detay.Add(postman);

            }

            return detay;

        }








        public static List<News_TB> Urunkat_get()
        {
            List<News_TB> detay = new List<News_TB>();
            var cek = _roleService.HaberTB_selectAnasayfa().Where(m => m.Statu == 1 && m.CategoryID == 12);


            foreach (var item in cek)
            {

                News_TB postman = new News_TB
                {
                    ID = item.ID,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Date = item.News_Create_Date.Value,
                    Statu = item.Statu.Value,
                    //News_Foto=item.News_Foto,
                    CategoryID = item.CategoryID.Value,
                    Category_Name = item.Category_Name,


                };
                detay.Add(postman);

            }

            return detay;

        }




        public static List<News_TB> Muzikkat_get()
        {
            List<News_TB> detay = new List<News_TB>();
            var cek = _roleService.HaberTB_selectAnasayfa().Where(m => m.Statu == 1 && m.CategoryID == 13);


            foreach (var item in cek)
            {

                News_TB postman = new News_TB
                {
                    ID = item.ID,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Date = item.News_Create_Date.Value,
                    Statu = item.Statu.Value,
                    //News_Foto=item.News_Foto,
                    CategoryID = item.CategoryID.Value,
                    Category_Name = item.Category_Name,


                };
                detay.Add(postman);

            }

            return detay;

        }




            //Blog Foto Start

        public class Blog_Foto_TB
        {
            public int FotoID { get; set; }
            public int PostID { get; set; }
            public string FotoName { get; set; }
            public string FotoURL { get; set; }
            public bool Default_Haber_Foto { get; set; }
           


        }

        public static List<Blog_Foto_TB> BlogFoto_TB_get(int ID)
    {
         List<Blog_Foto_TB> fotodetay = new List<Blog_Foto_TB>();
            var cek = _roleService.Blog_FotoTB_Select().Where(m=>m.PostID ==ID && m.Default_Haber_Foto==true );

        foreach(var item in cek)


            {

        Blog_Foto_TB postfoto = new Blog_Foto_TB
                {   

                    FotoID= item.FotoID,
                    PostID = item.PostID.Value,
                    FotoName= item.FotoName,
                    FotoURL=item.FotoURL,

                    Default_Haber_Foto = item.Default_Haber_Foto.Value,
                   


                };

         fotodetay.Add(postfoto);

            }
    return fotodetay;
       
    }

        //Blog Foto End

        //Sidebar Foto Start

        public class Sidebar_News_Foto_TB
        {
            public int FotoID { get; set; }
            public int PostID { get; set; }
            public string FotoName { get; set; }
            public string FotoURL { get; set; }
            public bool Default_Haber_Foto { get; set; }



        }


        public static List<Sidebar_News_Foto_TB> SidebarFoto_TB_get(int ID)
        {
            List<Sidebar_News_Foto_TB> fotodetay = new List<Sidebar_News_Foto_TB>();
            var cek = _roleService.Sidebar_News_FotoTB_Select().Where(m => m.PostID == ID && m.Default_Haber_Foto == true);

            foreach (var item in cek)
            {

                Sidebar_News_Foto_TB postfoto = new Sidebar_News_Foto_TB
                {

                    FotoID = item.FotoID,
                    PostID = item.PostID.Value,
                    FotoName = item.FotoName,
                    FotoURL = item.FotoURL,

                    Default_Haber_Foto = item.Default_Haber_Foto.Value,



                };

                fotodetay.Add(postfoto);

            }
            return fotodetay;

        }


        //Sidebar Foto End



        public class Sidebar_Banner_img_TB
        {
            public int FotoID { get; set; }
           
            public string FotoName { get; set; }
            public string FotoURL { get; set; }
            public bool Default_Foto { get; set; }
            public int Statu { get; set; }
            public string FotoNote { get; set; }
            public string Link { get; set; }
        }


        public static List<Sidebar_Banner_img_TB> SidebarBannerFoto_TB_get()
        {
            List<Sidebar_Banner_img_TB> fotodetay = new List<Sidebar_Banner_img_TB>();
            var cek = _roleService.Sidebar_Banner_img_TB_Select().Where(m => m.Statu == 1 && m.Default_Foto == true);

            foreach (var item in cek)
            {

                Sidebar_Banner_img_TB postfoto = new Sidebar_Banner_img_TB
                {

                    FotoID = item.FotoID,
                   
                    FotoName = item.FotoName,
                    FotoURL = item.FotoURL,

                    Default_Foto = item.Default_Foto.Value,
                   Statu = item.Statu.Value,
                   FotoNote=item.FotoNote,
                   Link=item.Link,



                };

                fotodetay.Add(postfoto);

            }
            return fotodetay;

        }



        //Yorum İşlemleri

        public class Comment_TB
        {
            public int CommentID { get; set; }
            public int NewsID { get; set; }
            public string News_Title { get; set; }
            public string User_Name { get; set; }
            public string User_Email { get; set; }
            public string User_IMG { get; set; }
            public string User_Comment { get; set; }
            public int CategoryID { get; set; }
            public string Category_Name { get; set; }
            public DateTime Comment_Date { get; set; }
            public int Status { get; set; }
            public string Commentİp { get; set; }
            public string Browser { get; set; }
            public int Comment_Report { get; set; }
            public int Comment_Like { get; set; }
            public int Comment_Dislike { get; set; }


        }

        public static List<Comment_TB>YorumTB_get(string ID)
    {
        List<Comment_TB> detay = new List<Comment_TB>();
        var cek = _roleService.CommentTB_Select().Where(m => m.NewsID == Convert.ToInt32(ID) && m.Status == 1).ToList();

        foreach (var item in cek)
        {
            Comment_TB commentman = new Comment_TB
            {
                CommentID = item.CommentID,
                NewsID = item.NewsID.Value,
                News_Title = item.News_Title,
                User_Name = item.User_Name,
                User_Email = item.User_Email,
                User_IMG = item.User_IMG,
                User_Comment = item.User_Comment,
                CategoryID = item.CategoryID.Value,
                Category_Name = item.Category_Name,
                Comment_Date = item.Comment_Date.Value,
                Status = item.Status.Value,
                Commentİp = item.Comment_IP,
                Browser = item.Browser,
                Comment_Report=item.Comment_Report.Value,
                Comment_Like=item.Comment_Like.Value,
                Comment_Dislike=item.Comment_Dislike.Value,

            };
            detay.Add(commentman);

        }
        return detay;
    }


        //Yorum İşlemleri


        //Slider İşlemleri


        public class Slider_TB
        {
            public int ID { get; set; }
            public int SliderCategoryID { get; set; }
            public int News_ID { get; set; }
            public string News_Title { get; set; }
            public string News_Description { get; set; }
            public int Status { get; set; }
        
        }

        //Alanlar İşlemleri

        public static List<Slider_TB> SliderTB_get_alan1()
        {
            List<Slider_TB> detay = new List<Slider_TB>();
            var cek = _roleService.SliderTB_Select().Where(m => m.Status == 1 && m.SliderCategoryID == 1).ToList();

            foreach (var item in cek)
            {
                Slider_TB sliderman = new Slider_TB
                {
                    ID = item.ID,
                    SliderCategoryID = item.SliderCategoryID.Value,
                    News_ID = item.News_ID.Value,
                    News_Title=item.News_Title,
                    News_Description=item.News_Description, 
                    Status = item.Status.Value,
                   

                };
                detay.Add(sliderman);

            }
            return detay;
        }



        public static List<Slider_TB> SliderTB_get_alan2()
        {
            List<Slider_TB> detay = new List<Slider_TB>();
            var cek = _roleService.SliderTB_Select().Where(m => m.Status == 1 && m.SliderCategoryID == 2).ToList();

            foreach (var item in cek)
            {
                Slider_TB sliderman = new Slider_TB
                {
                    ID = item.ID,
                    SliderCategoryID = item.SliderCategoryID.Value,
                    News_ID = item.News_ID.Value,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Status = item.Status.Value,


                };
                detay.Add(sliderman);

            }
            return detay;
        }



        public static List<Slider_TB> SliderTB_get_alan3()
        {
            List<Slider_TB> detay = new List<Slider_TB>();
            var cek = _roleService.SliderTB_Select().Where(m => m.Status == 1 && m.SliderCategoryID == 1002).ToList();

            foreach (var item in cek)
            {
                Slider_TB sliderman = new Slider_TB
                {
                    ID = item.ID,
                    SliderCategoryID = item.SliderCategoryID.Value,
                    News_ID = item.News_ID.Value,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Status = item.Status.Value,


                };
                detay.Add(sliderman);

            }
            return detay;
        }


        public static List<Slider_TB> SliderTB_get_alan4()
        {
            List<Slider_TB> detay = new List<Slider_TB>();
            var cek = _roleService.SliderTB_Select().Where(m => m.Status == 1 && m.SliderCategoryID == 1003).ToList();

            foreach (var item in cek)
            {
                Slider_TB sliderman = new Slider_TB
                {
                    ID = item.ID,
                    SliderCategoryID = item.SliderCategoryID.Value,
                    News_ID = item.News_ID.Value,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Status = item.Status.Value,


                };
                detay.Add(sliderman);

            }
            return detay;
        }



        public static List<Slider_TB> SliderTB_get_alan5()
        {
            List<Slider_TB> detay = new List<Slider_TB>();
            var cek = _roleService.SliderTB_Select().Where(m => m.Status == 1 && m.SliderCategoryID == 1004).ToList();

            foreach (var item in cek)
            {
                Slider_TB sliderman = new Slider_TB
                {
                    ID = item.ID,
                    SliderCategoryID = item.SliderCategoryID.Value,
                    News_ID = item.News_ID.Value,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Status = item.Status.Value,


                };
                detay.Add(sliderman);

            }
            return detay;
        }



        public static List<Slider_TB> SliderTB_get_alan6()
        {
            List<Slider_TB> detay = new List<Slider_TB>();
            var cek = _roleService.SliderTB_Select().Where(m => m.Status == 1 && m.SliderCategoryID == 1005).ToList();

            foreach (var item in cek)
            {
                Slider_TB sliderman = new Slider_TB
                {
                    ID = item.ID,
                    SliderCategoryID = item.SliderCategoryID.Value,
                    News_ID = item.News_ID.Value,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Status = item.Status.Value,


                };
                detay.Add(sliderman);

            }
            return detay;
        }



        public static List<Slider_TB> SliderTB_get_alan7()
        {
            List<Slider_TB> detay = new List<Slider_TB>();
            var cek = _roleService.SliderTB_Select().Where(m => m.Status == 1 && m.SliderCategoryID == 2003).ToList();

            foreach (var item in cek)
            {
                Slider_TB sliderman = new Slider_TB
                {
                    ID = item.ID,
                    SliderCategoryID = item.SliderCategoryID.Value,
                    News_ID = item.News_ID.Value,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Status = item.Status.Value,


                };
                detay.Add(sliderman);

            }
            return detay;
        }


        public static List<Slider_TB> SliderTB_get_alan8()
        {
            List<Slider_TB> detay = new List<Slider_TB>();
            var cek = _roleService.SliderTB_Select().Where(m => m.Status == 1 && m.SliderCategoryID == 2004).ToList();

            foreach (var item in cek)
            {
                Slider_TB sliderman = new Slider_TB
                {
                    ID = item.ID,
                    SliderCategoryID = item.SliderCategoryID.Value,
                    News_ID = item.News_ID.Value,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Status = item.Status.Value,


                };
                detay.Add(sliderman);

            }
            return detay;
        }


        public static List<Slider_TB> SliderTB_get_alan9()
        {
            List<Slider_TB> detay = new List<Slider_TB>();
            var cek = _roleService.SliderTB_Select().Where(m => m.Status == 1 && m.SliderCategoryID == 2005).ToList();

            foreach (var item in cek)
            {
                Slider_TB sliderman = new Slider_TB
                {
                    ID = item.ID,
                    SliderCategoryID = item.SliderCategoryID.Value,
                    News_ID = item.News_ID.Value,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Status = item.Status.Value,


                };
                detay.Add(sliderman);

            }
            return detay;
        }

        public static List<Slider_TB> SliderTB_get_alan10()
        {
            List<Slider_TB> detay = new List<Slider_TB>();
            var cek = _roleService.SliderTB_Select().Where(m => m.Status == 1 && m.SliderCategoryID == 2006).ToList();

            foreach (var item in cek)
            {
                Slider_TB sliderman = new Slider_TB
                {
                    ID = item.ID,
                    SliderCategoryID = item.SliderCategoryID.Value,
                    News_ID = item.News_ID.Value,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Status = item.Status.Value,


                };
                detay.Add(sliderman);

            }
            return detay;
        }






        public static List<Slider_TB> SliderTB_get_alan11()
        {
            List<Slider_TB> detay = new List<Slider_TB>();
            var cek = _roleService.SliderTB_Select().Where(m => m.Status == 1 && m.SliderCategoryID == 2007).ToList();

            foreach (var item in cek)
            {
                Slider_TB sliderman = new Slider_TB
                {
                    ID = item.ID,
                    SliderCategoryID = item.SliderCategoryID.Value,
                    News_ID = item.News_ID.Value,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Status = item.Status.Value,


                };
                detay.Add(sliderman);

            }
            return detay;
        }



        public static List<Slider_TB> SliderTB_get_alan12()
        {
            List<Slider_TB> detay = new List<Slider_TB>();
            var cek = _roleService.SliderTB_Select().Where(m => m.Status == 1 && m.SliderCategoryID == 2008).ToList();

            foreach (var item in cek)
            {
                Slider_TB sliderman = new Slider_TB
                {
                    ID = item.ID,
                    SliderCategoryID = item.SliderCategoryID.Value,
                    News_ID = item.News_ID.Value,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Status = item.Status.Value,


                };
                detay.Add(sliderman);

            }
            return detay;
        }


        public static List<Slider_TB> SliderTB_get_alan13()
        {
            List<Slider_TB> detay = new List<Slider_TB>();
            var cek = _roleService.SliderTB_Select().Where(m => m.Status == 1 && m.SliderCategoryID == 2009).ToList();

            foreach (var item in cek)
            {
                Slider_TB sliderman = new Slider_TB
                {
                    ID = item.ID,
                    SliderCategoryID = item.SliderCategoryID.Value,
                    News_ID = item.News_ID.Value,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Status = item.Status.Value,


                };
                detay.Add(sliderman);

            }
            return detay;
        }





        public static List<Slider_TB> SliderTB_get_alan14()
        {
            List<Slider_TB> detay = new List<Slider_TB>();
            var cek = _roleService.SliderTB_Select().Where(m => m.Status == 1 && m.SliderCategoryID == 2010).ToList();

            foreach (var item in cek)
            {
                Slider_TB sliderman = new Slider_TB
                {
                    ID = item.ID,
                    SliderCategoryID = item.SliderCategoryID.Value,
                    News_ID = item.News_ID.Value,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Status = item.Status.Value,


                };
                detay.Add(sliderman);

            }
            return detay;
        }

        public static List<Slider_TB> SliderTB_get_alan15()
        {
            List<Slider_TB> detay = new List<Slider_TB>();
            var cek = _roleService.SliderTB_Select().Where(m => m.Status == 1 && m.SliderCategoryID == 2011).ToList();

            foreach (var item in cek)
            {
                Slider_TB sliderman = new Slider_TB
                {
                    ID = item.ID,
                    SliderCategoryID = item.SliderCategoryID.Value,
                    News_ID = item.News_ID.Value,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Status = item.Status.Value,


                };
                detay.Add(sliderman);

            }
            return detay;
        }






        public static List<Slider_TB> SliderTB_get_alan16()
        {
            List<Slider_TB> detay = new List<Slider_TB>();
            var cek = _roleService.SliderTB_Select().Where(m => m.Status == 1 && m.SliderCategoryID == 2012).ToList();

            foreach (var item in cek)
            {
                Slider_TB sliderman = new Slider_TB
                {
                    ID = item.ID,
                    SliderCategoryID = item.SliderCategoryID.Value,
                    News_ID = item.News_ID.Value,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Status = item.Status.Value,


                };
                detay.Add(sliderman);

            }
            return detay;
        }





        public static List<Slider_TB> SliderTB_get_alan17()
        {
            List<Slider_TB> detay = new List<Slider_TB>();
            var cek = _roleService.SliderTB_Select().Where(m => m.Status == 1 && m.SliderCategoryID == 2013).ToList();

            foreach (var item in cek)
            {
                Slider_TB sliderman = new Slider_TB
                {
                    ID = item.ID,
                    SliderCategoryID = item.SliderCategoryID.Value,
                    News_ID = item.News_ID.Value,
                    News_Title = item.News_Title,
                    News_Description = item.News_Description,
                    Status = item.Status.Value,


                };
                detay.Add(sliderman);

            }
            return detay;
        }

        //Alanlar İşlemleri





        public class Slider_img_TB
        {
            public int FotoID { get; set; }
            public int SliderID { get; set; }
            public string FotoName { get; set; }
            public string FotoURL { get; set; }
            public bool Default_Haber_Foto { get; set; }

        }


        public static List<Slider_img_TB> SliderFoto_TB_get(int ID)
        {
            List<Slider_img_TB> fotodetay = new List<Slider_img_TB>();
            var cek = _roleService.SliderTB_Foto_Select().Where(m =>m.SliderID==ID && m.Default_Haber_Foto == true);

            foreach (var item in cek)
            {

                Slider_img_TB postfoto = new Slider_img_TB
                {

                    FotoID = item.FotoID,
                    SliderID=item.SliderID.Value,
                    FotoName = item.FotoName,
                    FotoURL = item.FotoURL,

                    Default_Haber_Foto = item.Default_Haber_Foto.Value,
                    



                };

                fotodetay.Add(postfoto);

            }
            return fotodetay;

        }

        //Slider İşlemleri


        //Rastgele Haber Foto İşlemleri


        public class Other_News_Foto_TB
        {
            public int FotoID { get; set; }
            public int PostID { get; set; }
            public string FotoName { get; set; }
            public string FotoURL { get; set; }
            public bool Default_Haber_Foto { get; set; }



        }


        public static List<Other_News_Foto_TB> OtherFoto_TB_get(int ID)
        {
            List<Other_News_Foto_TB> fotodetay = new List<Other_News_Foto_TB>();
            var cek = _roleService.Other_News_FotoTB_Select().Where(m => m.PostID == ID && m.Default_Haber_Foto == true);

            foreach (var item in cek)
            {

                Other_News_Foto_TB postfoto = new Other_News_Foto_TB
                {

                    FotoID = item.FotoID,
                    PostID = item.PostID.Value,
                    FotoName = item.FotoName,
                    FotoURL = item.FotoURL,

                    Default_Haber_Foto = item.Default_Haber_Foto.Value,



                };

                fotodetay.Add(postfoto);

            }
            return fotodetay;

        }



        //Rastgele Haber Foto İşlemleri


        //Developer Blog İşlemleri

        public class Developer_Blog_TB
        {

            public int DevBlogID { get; set; }
            public string DevBlog_Description { get; set; }
            public string Dev_Create_User_Name { get; set; }
            public string Dev_Create_User_Photo { get; set; }
            public string Dev_Blog_Update_User { get; set; }
            public DateTime Dev_Blog_Date { get;set; }
        }

        public static List<Developer_Blog_TB>Developer_Blog_TB_get()
        {

            List<Developer_Blog_TB> devdetay = new List<Developer_Blog_TB>();
            var cek = _roleService.Developer_Blog_Select();

            foreach (var item in cek)
            {
                Developer_Blog_TB devblog = new Developer_Blog_TB
                {
                    DevBlogID=item.DevBlogID,
                    DevBlog_Description=item.DevBlog_Description,
                    Dev_Create_User_Name=item.Dev_Create_User_Name,
                    Dev_Create_User_Photo=item.Dev_Create_User_Photo,
                    Dev_Blog_Update_User=item.Dev_Blog_Update_User,
                    Dev_Blog_Date=item.Dev_Blog_Date.Value,
                

                };
                devdetay.Add(devblog);


            }

            return devdetay;
        }


        //Developer Blog İşlemleri






        //Kullanıcı Çekme ve Rol
        public class UserTB
        {
            public int ID { get; set; }
            public string User_Role { get; set; }
        }

        public static List<UserTB> UserTB_get(int ID)
        {
            List<UserTB> detay = new List<UserTB>();
            var cek = _roleService.User_TB_Select().Where(m => m.ID == ID).ToList();

            foreach (var item in cek)
            {
                UserTB userman = new UserTB
                {
                    ID = item.ID,
                    User_Role = item.USER_ROLE,

                };
                detay.Add(userman);

            }

            return detay;
        }

        //Kullanıcı Çekme ve Rol

    }
}


