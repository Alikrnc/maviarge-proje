﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Common.Sms;

public interface ISmsProvider : ITransientService
{
    Task SendAsync(SmsContext sms, CancellationToken cancellationToken);
}