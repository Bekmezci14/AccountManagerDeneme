using System;

namespace AccountManager.Api.Entities;

public class Account
{
    public int Id { get; set; }
    

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public required string UserName { get; set; }
    public required string Password { get; set; }
    public string Email { get; set; } = "";
    public string IpAddress { get; set; } = "";
}
