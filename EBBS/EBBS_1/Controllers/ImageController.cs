using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EBBS_1.Data;
using EBBS_1.Models;
using EBBS_1.Service.IService;
using EBBS_1.Service.Service;

namespace EBBS_1.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        private readonly IImageService imageService;
        public ImageController()
        {
            imageService = new ImageService();
        }
        // GET: Image
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? page)

        {
            IEnumerable<Images> model = imageService.ImageList
                .OrderBy(p => p.Id)
                .OrderByDescending(p => p.Create_time);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UploadImage()
        {
            return PartialView("_UploadImage");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadFile()
        {
            string _imgname = string.Empty;

            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);

                    _imgname = Guid.NewGuid().ToString();

                    var _comPath = Server.MapPath("/Upload/Id_") + _imgname + _ext;
                    _imgname = "Id_" + _imgname + _ext;

                    ViewBag.Msg = _comPath;
                    var path = _comPath;

                    // Saving Image in Original Mode
                    pic.SaveAs(path);
                    var _lenght = new FileInfo(path).Length;
                    //here to add Image Path to You Database ,
                    Images data = new Images();
                    data.ImagePath = _imgname;
                    data.Size = Convert.ToInt32(_lenght);
                    data.Create_time = DateTime.Now;

                    bool result;
                    result = AddImage(data);
                    if (result == true)
                    {
                        TempData["message"] = string.Format("Image was Added Successfully");
                    }
                  
                }
            }
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
            Images image = imageService.Details(Id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirm(int? Id)
        {

            Images image = imageService.Delete(Id);
            if (image != null)
            {
                TempData["message"] = string.Format("deleted");
            }
            return RedirectToAction("Index", "Image");
        }
        [Authorize(Roles = "Admin,SuperUser")]
        public bool AddImage(Images data)
        {
            var identity = (HttpContext.User as MyPrincipal).Identity as MyIdentity;
            int _CurrentUserId = Convert.ToInt32(identity.User.UserId);



            bool res = true;

            if (data != null)
            {
                Images obj = GetImageSession();
                obj.Id = data.Id;
                obj.ImagePath = data.ImagePath;
                obj.Size = data.Size;
                obj.Create_time = data.Create_time;
                obj.UserId = _CurrentUserId;
                imageService.Save(obj);
                int? Newid = obj.Id;

                res = true;
            }
            else
            {
                res = false;
            }
            return res;
        }



        ///Sessions
        ///

        private Images GetImageSession()
        {
            if (Session["image"] == null)
            {
                Session["image"] = new Images();
            }
            return (Images)Session["image"];
        }
    }
}