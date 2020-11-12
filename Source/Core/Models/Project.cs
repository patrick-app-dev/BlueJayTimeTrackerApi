namespace Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }

    }
}
