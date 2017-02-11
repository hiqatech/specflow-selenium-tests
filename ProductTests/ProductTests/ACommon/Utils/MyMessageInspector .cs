using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace ProductTests.ACommon.SRC
{
    class MyMessageInspector : IClientMessageInspector
    {
      
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            Console.WriteLine(reply.ToString());
            
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            Console.WriteLine(request.ToString());
            return request;

        }
    }
}
