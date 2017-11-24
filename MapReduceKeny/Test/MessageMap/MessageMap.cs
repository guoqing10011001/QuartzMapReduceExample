using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Test.MessageMap
{
    enum MessageMap
    {
        [Description("开始调度")]
        Start = 0,
        OK,
        ERROR,
        INVALIDTYPE
    }
}
