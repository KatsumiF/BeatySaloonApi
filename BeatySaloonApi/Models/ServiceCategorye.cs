using System;
using System.Collections.Generic;

namespace BeatySaloonApi.Models;

public partial class ServiceCategorye
{
    public int CategoryId { get; set; }

    public string? CategoryTitle { get; set; }

    public byte[]? CategoryImage { get; set; }

    public virtual ICollection<Service> Services { get; } = new List<Service>();
}
