using SMSApi.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitBuy.SmsService
{
    public class SmsService : ISmsService
    {
        public void SendSms(string code, string number)
        {
            try
            {
                IClient client = new ClientOAuth("SMSAPI_ACCESS_TOKEN");

                var smsApi = new SMSFactory(client, ProxyAddress.SmsApiPl);

                var result =
                    smsApi.ActionSend()
                        .SetText($"this is your code{code}")
                        .SetTo(number)
                        .SetSender("Test")
                        .Execute();
            }
            catch (ActionException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ClientException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (HostException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ProxyException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
