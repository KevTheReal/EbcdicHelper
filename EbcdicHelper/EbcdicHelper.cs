using System;
using System.Collections.Generic;
using System.Globalization;

namespace EbcdicHelper
{
    public static class EbcdicHelper
    {
        static readonly byte[] AsciiToEbcdic = new byte[]
        {
            0x00,0x01,0x02,0x03,0x37,0x2D,0x2E,0x2F,0x16,0x05,0x25,0x0B,0x0C,0x0D,0x0E,0x0F,
            0x10,0x11,0x12,0x13,0x3C,0x3D,0x32,0x26,0x18,0x19,0x3F,0x27,0x1C,0x1D,0x1E,0x1F,
            0x40,0x5A,0x7F,0x7B,0x5B,0x6C,0x50,0x7D,0x4D,0x5D,0x5C,0x4E,0x6B,0x60,0x4B,0x61,
            0xF0,0xF1,0xF2,0xF3,0xF4,0xF5,0xF6,0xF7,0xF8,0xF9,0x7A,0x5E,0x4C,0x7E,0x6E,0x6F,
            0x7C,0xC1,0xC2,0xC3,0xC4,0xC5,0xC6,0xC7,0xC8,0xC9,0xD1,0xD2,0xD3,0xD4,0xD5,0xD6,
            0xD7,0xD8,0xD9,0xE2,0xE3,0xE4,0xE5,0xE6,0xE7,0xE8,0xE9,0xBA,0xE0,0xBB,0x5F,0x6D,
            0x79,0x81,0x82,0x83,0x84,0x85,0x86,0x87,0x88,0x89,0x91,0x92,0x93,0x94,0x95,0x96,
            0x97,0x98,0x99,0xA2,0xA3,0xA4,0xA5,0xA6,0xA7,0xA8,0xA9,0xC0,0x4F,0xD0,0xA1,0x07,
            0x20,0x21,0x22,0x23,0x24,0x15,0x06,0x17,0x28,0x29,0x2A,0x2B,0x2C,0x09,0x0A,0x1B,
            0x30,0x31,0x1A,0x33,0x34,0x35,0x36,0x08,0x38,0x39,0x3A,0x3B,0x04,0x14,0x3E,0xE1,
            0x41,0x42,0x4A,0x44,0x45,0x46,0x47,0x48,0x49,0x51,0x52,0x53,0x54,0x55,0x56,0x57,
            0x58,0x59,0x62,0x63,0x64,0x65,0x66,0x67,0x68,0x69,0x70,0x71,0x72,0x73,0x74,0x75,
            0x76,0x77,0x78,0x80,0x8A,0x8B,0x8C,0x8D,0x8E,0x8F,0x90,0x9A,0x9B,0x9C,0x9D,0x9E,
            0x9F,0xA0,0xAA,0xAB,0xAC,0xAD,0xAE,0xAF,0xB0,0xB1,0xB2,0xB3,0xB4,0xB5,0xB6,0xB7,
            0xB8,0xB9,0xBA,0xBB,0xBC,0xBD,0xBE,0xBF,0xCA,0xCB,0xCC,0xCD,0xCE,0xCF,0xDA,0xDB,
            0xDC,0xDD,0xDE,0xDF,0xEA,0xEB,0xEC,0xED,0xEE,0xEF,0xFA,0xFB,0xFC,0xFD,0xFE,0xFF
        };
        static readonly byte[] EbcdicToAscii = new byte[]
        { //將NULL改為空白
            0X20,0X01,0X02,0X03,0X9C,0X09,0X86,0X7F,0X97,0X8D,0X8E,0X0B,0X0C,0X0D,0X0E,0X0F,
            0X10,0X11,0X12,0X13,0X9D,0X85,0X08,0X87,0X18,0X19,0X92,0X8F,0X1C,0X1D,0X1E,0X1F,
            0X80,0X81,0X82,0X83,0X84,0X0A,0X17,0X1B,0X88,0X89,0X8A,0X8B,0X8C,0X05,0X06,0X07,
            0X90,0X91,0X16,0X93,0X94,0X95,0X96,0X04,0X98,0X99,0X9A,0X9B,0X14,0X15,0X9E,0X1A,
            0X20,0XA0,0XA1,0XA2,0XA3,0XA4,0XA5,0XA6,0XA7,0XA8,0XA2,0X2E,0X3C,0X28,0X2B,0X7C,
            0X26,0XA9,0XAA,0XAB,0XAC,0XAD,0XAE,0XAF,0XB0,0XB1,0X21,0X24,0X2A,0X29,0X3B,0X5E,
            0X2D,0X2F,0XB2,0XB3,0XB4,0XB5,0XB6,0XB7,0XB8,0XB9,0X7C,0X2C,0X25,0X5F,0X3E,0X3F,
            0XBA,0XBB,0XBC,0XBD,0XBE,0XBF,0XC0,0XC1,0XC2,0X60,0X3A,0X23,0X40,0X27,0X3D,0X22,
            0XC3,0X61,0X62,0X63,0X64,0X65,0X66,0X67,0X68,0X69,0XC4,0XC5,0XC6,0XC7,0XC8,0XC9,
            0XCA,0X6A,0X6B,0X6C,0X6D,0X6E,0X6F,0X70,0X71,0X72,0XCB,0XCC,0XCD,0XCE,0XCF,0XD0,
            0XD1,0X7E,0X73,0X74,0X75,0X76,0X77,0X78,0X79,0X7A,0XD2,0XD3,0XD4,0X5B,0XD6,0XD7,
            0XD8,0XD9,0XDA,0XDB,0XDC,0XDD,0XDE,0XDF,0XE0,0XE1,0X5B,0X5D,0XE4,0X5D,0XE6,0XE7,
            0X7B,0X41,0X42,0X43,0X44,0X45,0X46,0X47,0X48,0X49,0XE8,0XE9,0XEA,0XEB,0XEC,0XED,
            0X7D,0X4A,0X4B,0X4C,0X4D,0X4E,0X4F,0X50,0X51,0X52,0XEE,0XEF,0XF0,0XF1,0XF2,0XF3,
            0X5C,0X9F,0X53,0X54,0X55,0X56,0X57,0X58,0X59,0X5A,0XF4,0XF5,0XF6,0XF7,0XF8,0XF9,
            0X30,0X31,0X32,0X33,0X34,0X35,0X36,0X37,0X38,0X39,0XFA,0XFB,0XFC,0XFD,0XFE,0XFF
        };

        public static byte[] ConvertAsciiToEbcdic(byte[] asciiBytes)
        {
            byte[] ebcdicBytes = new byte[asciiBytes.Length];

            for (int i = 0; i < asciiBytes.Length; i++)
            {
                ebcdicBytes[i] = AsciiToEbcdic[(int)asciiBytes[i]];
            }

            return ebcdicBytes;
        }

        public static string ConvertAsciiToEbcdic(string asciiString)
        {
            byte[] asciiBytes = new byte[asciiString.Length / 2];
            byte[] ebcdicBytes = new byte[asciiBytes.Length];

            for (int index = 0; index < asciiBytes.Length; index++)
            {
                string byteValue = asciiString.Substring(index * 2, 2);
                asciiBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber);
            }

            for (int i = 0; i < asciiBytes.Length; i++)
            {
                ebcdicBytes[i] = AsciiToEbcdic[(int)asciiBytes[i]];
            }

            return BitConverter.ToString(ebcdicBytes).Replace("-", "");
        }

        public static byte[] ConvertEbcdicToAscii(byte[] ebcdicBytes)
        {
            byte[] asciiBytes = new byte[ebcdicBytes.Length];

            for (int i = 0; i < asciiBytes.Length; i++)
            {
                asciiBytes[i] = EbcdicToAscii[(int)ebcdicBytes[i]];
            }

            return asciiBytes;
        }

        public static byte[] ConvertEbcdicToAscii(string ebcdicString)
        {
            byte[] ebcdicBytes = new byte[ebcdicString.Length / 2];
            byte[] asciiBytes = new byte[ebcdicBytes.Length];

            for (int index = 0; index < ebcdicBytes.Length; index++)
            {
                string byteValue = ebcdicString.Substring(index * 2, 2);
                ebcdicBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber);
            }

            for (int i = 0; i < asciiBytes.Length; i++)
            {
                asciiBytes[i] = EbcdicToAscii[(int)ebcdicBytes[i]];
            }

            return asciiBytes;
        }
    }
}
