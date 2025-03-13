using System;
using System.Collections.Generic;

namespace StudyPractice.Models;

public partial class ResumeLibrary
{
    public int ResumeLibraryId { get; set; }

    public string CandidateName { get; set; } = null!;

    public int? PositionId { get; set; }

    public DateOnly? ReceivedDate { get; set; }

    public string? Resume { get; set; }

    public virtual Position? Position { get; set; }
}
