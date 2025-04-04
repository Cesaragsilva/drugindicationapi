﻿using DrugIndication.Domain.Helpers;

namespace DrugIndication.Application.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string GetPasswordHash() 
        {
           return HashHelper.Hash(Password);
        }
    }
}
