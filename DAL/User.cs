using System;
using System.Collections.Generic;

namespace DAL;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? Role { get; set; }

    public double? Wallet { get; set; }

    public virtual ICollection<Slot> SlotMentors { get; set; } = new List<Slot>();

    public virtual ICollection<Slot> SlotStudents { get; set; } = new List<Slot>();
}
