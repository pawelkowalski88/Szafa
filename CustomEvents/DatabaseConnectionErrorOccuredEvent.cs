using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEvents
{
    public class DatabaseConnectionErrorOccuredEvent : PubSubEvent<Exception>
    {
    }
}
