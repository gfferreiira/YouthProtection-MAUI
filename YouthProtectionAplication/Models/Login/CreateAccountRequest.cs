﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouthProtectionAplication.Models.Login
{
    public class CreateAccountRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
