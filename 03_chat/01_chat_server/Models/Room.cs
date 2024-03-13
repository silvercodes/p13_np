using System;
using System.Collections.Generic;

namespace _01_chat_server.Models;

public partial class Room
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public byte? Status { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<RoomsUser> RoomsUsers { get; set; } = new List<RoomsUser>();
}
