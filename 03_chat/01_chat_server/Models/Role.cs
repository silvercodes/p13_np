using System;
using System.Collections.Generic;

namespace _01_chat_server.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<RoomsUser> RoomsUsers { get; set; } = new List<RoomsUser>();
}
