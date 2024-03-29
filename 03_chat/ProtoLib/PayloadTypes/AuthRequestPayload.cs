﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProtoLib.PayloadTypes
{
    public class AuthRequestPayload : JsonPayload
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public AuthRequestPayload(string login, string password)
        {
            Login = login;
            Password = password;
        }
        public override string GetJson()
        {
            return JsonSerializer.Serialize(this);
        }
        public override MemoryStream GetStream()
        {
            MemoryStream memStream = new MemoryStream();

            string json = GetJson();

            byte[] bytes = Encoding.UTF8.GetBytes(json);
            memStream.Write(bytes, 0, bytes.Length);

            return memStream;
        }

        
    }
}
