using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRentalService.Models;

namespace CarRentalService.Controllers
{
    public class UserRegisterController : Controller
    {
        // GET: UserRegister
        public ActionResult Index()
        {
            var RegisterModel = new RegisterModel();
            return View();
        }
        public ActionResult UserRegister(RegisterModel model)
        {

            if (model.Email.ToLower() == model.Password.ToLower())
            {
                ModelState.AddModelError("Password", "User and Password cannot be same.");
            }
            //else
            //    ModelState.IsValid = 1;
            try
            {

                //if (ModelState.IsValid)
                //{

                    if (duplicatecheck(model.Email))
                    {
                        //lblmessages.Attributes.Remove("hidden");
                        //lblmessages.Visible = true;

                        //lblmessages.Text = "Email Already Exists";
                    }
                    else
                    {
                        string query = @"Insert into Login_info 
                            Values (@user_email,@user_password,@user_isactive,@user_type,@user_fname,@user_lname)
                            ";
                        string query1 = @"select * from Login_info";
                        string sqlDataSource = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
                        SqlDataReader myReader;
                        using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                        {
                            myCon.Open();
                            using (SqlCommand myCom = new SqlCommand(query, myCon))
                            {
                                myCom.Parameters.AddWithValue("@user_email", model.Email);
                                myCom.Parameters.AddWithValue("@user_password", model.Password);
                                myCom.Parameters.AddWithValue("@user_isactive", false);
                                myCom.Parameters.AddWithValue("@user_type", 1);
                                myCom.Parameters.AddWithValue("@user_fname", model.FirstName);
                                myCom.Parameters.AddWithValue("@user_lname", model.LastName);
                                myReader = myCom.ExecuteReader();
                                myReader.Close();
                                using (SqlCommand myCom1 = new SqlCommand(query1, myCon))
                                {
                                    myReader = myCom1.ExecuteReader();
                                    myReader.Close();
                                }
                            }
                            myCon.Close();
                        }
                    }

                    //Response.Redirect("LandingController/Index");
                    return View(model);
                //}
                //else
                //{
                //    return View(model);
                //}
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }
        protected bool duplicatecheck(string Username)
        {
            string query = "select user_email from Login_info";
            string sqlDataSource = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection con = new SqlConnection(sqlDataSource);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                //lblmessages.Text = "";

                if (rd["user_email"].ToString() == Username.ToString())

                {

                    return true;

                }

            }
            con.Close();
            return false;
        }
    }
}