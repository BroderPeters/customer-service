using System;
using System.ComponentModel.DataAnnotations;
using CodeChallenge.CustomerService.Helpers.Exceptions;
using static CodeChallenge.CustomerService.Data.Enums;

namespace CodeChallenge.CustomerService.Data
{
    public class Customer
    {
        public long Id { get; set; }

        [Required]
        [StringLength(128)]
        public string? Email { get; private set; }

        [Required]
        [StringLength(128)]
        public string? Name { get; private set; }

        public int? Code { get; set; }

        public CustomerStatus Status { get; set; }

        public bool IsBlocked { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public void SetEmail(string email)
        {
            if (email.Length > 128)
            {
                throw new MaxStringLengthExceededException("The Email cannot exceed 128 characters.");
            }

            Email = email;
        }

        public void SetName(string name)
        {
            if (name.Length > 128)
            {
                throw new MaxStringLengthExceededException($"The Name cannot exceed 128 characters.");
            }

            Name = name;
        }
    }
}