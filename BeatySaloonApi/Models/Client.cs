using System;
using System.Collections.Generic;

namespace BeatySaloonApi.Models;

public partial class Client
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public DateTime? Birthday { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? GenderCode { get; set; }

    public string? PhotoPath { get; set; }

    public string? Login { get; set; }

    public virtual ICollection<ClientService> ClientServices { get; } = new List<ClientService>();

    public virtual Gender? GenderCodeNavigation { get; set; }

    public virtual User? LoginNavigation { get; set; }
}
