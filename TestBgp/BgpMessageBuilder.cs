using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestBgp
{
    public static class BgpMessageBuilder
    {
        public static byte[] BuildOpenMessage(int asn)
        {
            byte[] message = new byte[29];

            int index = 0;

            // Marker (16 bytes = FF)
            for (int i = 0; i < 16; i++)
                message[index++] = 0xFF;

            // Length (2 bytes) = 29
            message[index++] = 0x00;
            message[index++] = 0x1D;

            // Type = OPEN (1)
            message[index++] = 0x01;

            // Version = 4
            message[index++] = 0x04;

            // ASN (2 bytes)
            byte[] asnBytes = BitConverter.GetBytes((ushort)asn);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(asnBytes);
            message[index++] = asnBytes[0];
            message[index++] = asnBytes[1];

            // Hold Time = 90 (0x005A)
            message[index++] = 0x00;
            message[index++] = 0x5A;

            // BGP Identifier (любой IP, например 192.168.1.1)
            byte[] bgpId = IPAddress.Parse("192.168.1.1").GetAddressBytes();
            Array.Copy(bgpId, 0, message, index, 4);
            index += 4;

            // Optional Parameters Length = 0
            message[index++] = 0x00;

            return message;
        }
    }
}
