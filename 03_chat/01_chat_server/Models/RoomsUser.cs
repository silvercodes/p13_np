using System;
using System.Collections.Generic;

namespace _01_chat_server.Models;

public partial class RoomsUser
{
    public int UserId { get; set; }

    public int RoomId { get; set; }

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
