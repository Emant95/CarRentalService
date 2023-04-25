using CarRentalService.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRentalService.Controllers
{
    public class LandingController : Controller
    {
        // GET: Landing
        public ActionResult Index()
        {
            var MyLandingModel = new LandingModel();
            return View(MyLandingModel);
        }

        [HttpPost]
        public ActionResult UserLogin(LandingModel myModel)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);
            string username = myModel.UserName;
            string Password = myModel.Password;
            try
            {
                con.Open();

                string qry = "select * from Login_info where user_email=@Username and user_password=@Password";

                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", Password);
                SqlDataReader rd = cmd.ExecuteReader();

                bool flg = false;

                while (rd.Read())
                {
                    //checking given email address with database
                    if (string.Equals(username.ToString().Trim(), rd["user_email"].ToString().Trim()))
                    {
                        //checking given password
                        if (string.Equals(Password.ToString(), rd["user_password"].ToString()))
                        {
                            //user is active or not?
                            if (rd["user_isactive"].ToString() == "1" || rd["user_isactive"].ToString() == "True")
                            {
                                //user type checking
                                if (rd["user_type"].ToString() == "1")
                                {
                                    //perform admin Type user login

                                    Session["New"] = rd["SN"].ToString();
                                    con.Close();
                                    return View("Landingview");
                                    
                                }
                                else
                                {
                                    //perform normal user type login
                                    Session["New"] = rd["SN"].ToString();
                                    con.Close();
                                    return View("Landingview");
                                    
                                }

                            }
                            else
                            {
                                //user is not active
                                //LBL_errormessage.Text = "User is inactive";
                                return null;

                                break;
                            }

                        }
                        else
                        {
                            // wrong password
                            //LBL_errormessage.Text = "*Wrong password";
                            return null;
                            break;
                        }

                    }
                    else
                    {
                        // this mean wrong email address
                        //LBL_errormessage.Text = "*Wrong Email address";
                        return null;

                    }

                }
            }
            catch (Exception ex)
            {
                return null;
                //LBL_errormessage.Text = "Internal error\n" + ex.Message;
            }
            return null;

        }

       

    }
}