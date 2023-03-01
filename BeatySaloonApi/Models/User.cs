using System;
using System.Collections.Generic;

namespace BeatySaloonApi.Models;

public partial class User
{
    public string UserLogin { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public int? Idrole { get; set; }

    public virtual ICollection<Client> Clients { get; } = new List<Client>();

    public virtual Role? IdroleNavigation { get; set; }
}
