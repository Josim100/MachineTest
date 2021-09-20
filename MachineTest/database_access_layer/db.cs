using MachineTest.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MachineTest.database_access_layer
{
    public class db

    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

        // Get All Country

        public DataSet Get_Country()

        {

            SqlCommand com = new SqlCommand("Select * from Master_Country", con);

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;

        }



        //Get all State

        public DataSet Get_State(string CountryCode)

        {

            SqlCommand com = new SqlCommand("Select * from Master_Region where CountryCode=@CountryCode", con);

            com.Parameters.AddWithValue("@CountryCode", CountryCode);

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;

        }



        //Get all City

        public DataSet Get_City(string RegionCode)

        {

            SqlCommand com = new SqlCommand("Select * from Master_City where RegionCode=@RegionCode", con);

            com.Parameters.AddWithValue("@RegionCode", RegionCode);

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            int v = da.Fill(ds);

            return ds;

        }
        public DataSet Get_Product()

        {

            SqlCommand com = new SqlCommand("Select * from Master_Product", con);

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;

        }
        public int InsertSales(SalesModel salesModel)
        {

            try
            {
                string str = @"INSERT INTO [dbo].[Master_Sales]
           ([CustomerName],[CountryCode],[RegionCode],[CityCode],[DateofSale],[ProductID],[Quanty])
     VALUES
           ('" + salesModel.CustomerName + "', '" + salesModel.CountryCode + "','" + salesModel.RegionCode + "', '" + salesModel.CityCode + "','" + salesModel.DateofSale + "','" + salesModel.ProductID + "','" + salesModel.Quanty + "')";
                SqlCommand sqlCommand = new SqlCommand(str,con);
                
                con.Open();
                int i=    sqlCommand.ExecuteNonQuery();
                con.Close();
                return i;
            }
            catch (Exception ex)
            {

                return 0;
            }
           
           
        }
        public DataSet Get_Search(SalesModel salesModel)

        {

            SqlCommand cmd = new SqlCommand(@"select SalesID, CustomerName, RegionCode, CityCode, DateofSale, Quanty, CreationDate,mc.CountryName,mp.ProductName 
                    from Master_Sales ms
                      join [dbo].[Master_Country] mc on ms.CountryCode = mc.CountryCode
                      join [dbo].[Master_Product] mp on ms.ProductID = mp.ProductID
                      join [dbo].[Master_City] md on ms.CityCode= md.CityCode

                      where 
                    mc.CountryCode= @CountryCode or
                    mp.ProductID= @ProductID or
                    md.CityCode=@CityCode

                    ", con);

            cmd.Parameters.AddWithValue("@CountryCode", salesModel.CountryCode??"");
            cmd.Parameters.AddWithValue("@ProductID", salesModel.ProductID);
            cmd.Parameters.AddWithValue("@CityCode", salesModel.CityCode);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

           

            return ds;

        }

    }
}