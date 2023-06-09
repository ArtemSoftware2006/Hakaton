﻿using Domain.Enum;

namespace Domain.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public bool IsVIP { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public int Balance { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Proposal> Proposals { get; set; }
        public ICollection<Deal> Deals { get; set; }
    }
}
