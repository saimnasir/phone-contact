﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneContact.ViewModels.Requests
{
    public class ReadModelRequest
    {
        public long Id { get; set; }
        public Guid UIID { get; set; }
    }
}
