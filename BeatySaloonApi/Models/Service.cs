using System;
using System.Collections.Generic;

namespace BeatySaloonApi.Models;

public partial class Service
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public decimal Cost { get; set; }

    public int DurationInSeconds { get; set; }

    public string? Description { get; set; }

    public double? Discount { get; set; }

    public string? MainImagePath { get; set; }

    public int? CategoryId { get; set; }

    public virtual ServiceCategorye? Category { get; set; }

    public virtual ICollection<ClientService> ClientServices { get; } = new List<ClientService>();

    public virtual ICollection<ServicePhoto> ServicePhotos { get; } = new List<ServicePhoto>();
}
