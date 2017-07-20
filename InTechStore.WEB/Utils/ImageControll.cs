using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;



namespace InTechStore.WEB.Utils
{
   public enum ImageType
    {
        Product,
        Producer
    }
    public class ImageControll
    {
        public static string SaveImage(HttpPostedFileBase file, ImageType imType)
        {
            if (file == null)
            {
               return "/Images/Common/noimagefound.jpg";
            }

            string extension = Path.GetExtension(file.FileName);

            if (extension == ".png" || extension == ".jpeg" || extension == ".jpg")
            {

                string fileName = Guid.NewGuid().ToString();
                fileName += extension;

                string path = "/Images/";
                if (imType == ImageType.Product)
                {
                    path += "Products/";
                }
                else if (imType == ImageType.Producer)
                {
                    path += "Producers/";
                }
                path += fileName;
                string filePath = HttpContext.Current.Server.MapPath(path);

                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                file.SaveAs(filePath);

                return path;
            }
            else
            {
                throw new FormatException("Помилка! Невірний формат файла - 'png', 'jpeg'");
            }
        }
    }
}