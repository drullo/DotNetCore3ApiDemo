using System.ComponentModel.DataAnnotations;
using dotnetWebApi.DTOs;

namespace dotnetWebApi.Models
{
    public class Command : CommandCreateDto
    {
        public int Id { get; set; }
    }
}