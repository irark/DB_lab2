using System;
using System.Collections.Generic;

#nullable disable

namespace DB_lab2
{
    public partial class FilmUserRelationship
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public string UserName { get; set; }

        public virtual Film Film { get; set; }
    }
}
