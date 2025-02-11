using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitBuy.SmsService
{
    public interface ISmsService
    {
        public void SendSms(string code, string number);

    }
}
