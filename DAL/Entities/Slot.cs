namespace DAL.Entities;

public partial class Slot
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public int Duration { get; set; }

    public int Status { get; set; }

    public int? StudentId { get; set; }

    public int? MentorId { get; set; }

    public virtual User? Mentor { get; set; }

    public virtual User? Student { get; set; }
}
