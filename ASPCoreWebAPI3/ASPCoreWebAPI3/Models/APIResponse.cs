﻿namespace ASPCoreWebAPI3.Models
{
    public class APIResponse<T>
    {
            public string Status { get; set; }
            public T Data { get; set; }
            public string Message { get; set; }

    }
}
