using System;
using System.Collections.Generic;

namespace _01_chat_server.Models;

public partial class Message
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public int UserId { get; set; }

    public int RoomId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
