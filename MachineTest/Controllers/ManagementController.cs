
using MachineTest.database_access_layer;
using MachineTest.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MachineTest.Controllers
{
    public class ManagementController : BaseController
    {
        db dblayer = new db();
        // GET: Management
        public ActionResult Index()
        {
            Country_Bind();
            Product_Bind();

            ViewBag.RegionCode =  new List<SelectListItem>() ;
            ViewBag.CityCode =  new List<SelectListItem>();

            return View();
        }
        public void Country_Bind()

        {

            DataSet ds = dblayer.Get_Country();

            List<SelectListItem> coutrylist = new List<SelectListItem>();

            foreach (DataRow dr in ds.Tables[0].Rows)

            {

                coutrylist.Add(new SelectListItem { Text = dr["CountryName"].ToString(), Value = dr["CountryCode"].ToString() });

            }

            ViewBag.CountryCode = coutrylist;

        }
        public JsonResult State_Bind(string CountryCode)
        {

            DataSet ds = dblayer.Get_State(CountryCode);

            List<SelectListItem> statelist = new List<SelectListItem>();

            foreach (DataRow dr in ds.Tables[0].Rows)

            {

                statelist.Add(new SelectListItem { Text = dr["RegionName"].ToString(), Value = dr["RegionCode"].ToString() });

            }

            return Json(statelist, JsonRequestBehavior.AllowGet);

        }


        public JsonResult City_Bind(string RegionCode)

        {

            DataSet ds = dblayer.Get_City(RegionCode);

            List<SelectListItem> citylist = new List<SelectListItem>();

            foreach (DataRow dr in ds.Tables[0].Rows)

            {

                citylist.Add(new SelectListItem { Text = dr["CityName"].ToString(), Value = dr["CityCode"].ToString() });

            }

            return Json(citylist, JsonRequestBehavior.AllowGet);

        }
    public void Product_Bind()

    {

        DataSet ds = dblayer.Get_Product();

        List<SelectListItem> productlist = new List<SelectListItem>();

        foreach (DataRow dr in ds.Tables[0].Rows)

        {

            productlist.Add(new SelectListItem { Text = dr["ProductName"].ToString(), Value = dr["ProductID"].ToString() });

        }

        ViewBag.ProductID = productlist;

    }
    [HttpPost]
    public ActionResult Index(SalesModel salesModel)
    {
     
            if (dblayer.InsertSales(salesModel) > 0)
            {
                TempData["msg"] = "Data Saved successfully!";


            }
            else
            {
                TempData["msg"] = "Something went wrong please try again!";

            }
            return RedirectToAction("Index");
    }

        public ActionResult Search(SalesModel salesModel)
        {
            Country_Bind();
            Product_Bind();
            

            DataSet ds = dblayer.Get_Search(salesModel);
            ViewBag.search = ds.Tables[0];
            return View();
        }

       
    }
}