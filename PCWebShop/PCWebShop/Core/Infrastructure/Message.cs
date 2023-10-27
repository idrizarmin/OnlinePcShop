﻿using PCWebShop.Core.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.Core.Infrastructure
{
    public class Message
    {
        public virtual ExceptionCodeEnum Status { get; set; }
        public virtual string Info { get; set; }
        public virtual object Data { get; set; }
        public virtual bool IsValid { get; set; }
        public virtual PagedResult PagedResult { get; set; }

        public Message()
        {
        }

        public Message(bool isValid, string info, ExceptionCodeEnum status, object data = null)
        {
            IsValid = isValid;
            Status = status;
            Info = info;
            Data = data;
        }
    }
}

