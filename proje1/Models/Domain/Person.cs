using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
namespace proje1.Models.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? yaş { get; set; }
        public string? telefon { get; set; }
        public string? resim { get; set; }

    }
}
