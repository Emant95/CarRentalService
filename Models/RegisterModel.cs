using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRentalService.Models
{
    public class RegisterModel
    {

        private int _userid;
        private string _firstname;
        private string _lastname;
        private string _phone;
        private string _email;
        private string _country;
        private string _state;
        private string _city;
        private string _address;
        private string _title;
        private int? _countrycode;

        [Display(Name = "User Name")]
        [Required]
        [EmailAddress]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Is Activated")]
        public bool IsActivated { get; set; }

        public string[] UserRoles { get; set; }

        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }

        public string FirstName
        {
            set { _firstname = value; }
            get { return _firstname; }
        }

        public string LastName
        {
            set { _lastname = value; }
            get { return _lastname; }
        }

        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        public int? CountryCode
        {
            set { _countrycode = value; }
            get { return _countrycode; }
        }
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
    }
}