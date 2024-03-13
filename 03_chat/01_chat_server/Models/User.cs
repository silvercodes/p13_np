using System;
using System.Collections.Generic;

namespace _01_chat_server.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<RoomsUser> RoomsUsers { get; set; } = new List<RoomsUser>();
}
