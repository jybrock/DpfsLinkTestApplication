using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DpfsLinkTestApplication.__Console__ {

    [Serializable]
    [ComVisible(true)]
    [Flags]
    public enum ClientSize {
        tcp = 4096,             // Standard ByteArray Size
        dpfs = 512,             // Single IP Packet
        powerserver = 1024,     // Two IP Packets
    }
}
