﻿using System;
using System.Runtime.InteropServices;

namespace OBJ_IO.Plugins.Utils
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Int32Converter
    {
        //------------------------------------------------------------------------------------------------------------
        [FieldOffset(0)]
        public int Value;
        [FieldOffset(0)]
        public byte Byte1;
        [FieldOffset(1)]
        public byte Byte2;
        [FieldOffset(2)]
        public byte Byte3;
        [FieldOffset(3)]
        public byte Byte4;

        //------------------------------------------------------------------------------------------------------------
        public Int32Converter(int value)
        {
            Byte1 = Byte2 = Byte3 = Byte4 = 0;
            Value = value;
        }

        //------------------------------------------------------------------------------------------------------------
        public static implicit operator Int32(Int32Converter value)
        {
            return value.Value;
        }

        //------------------------------------------------------------------------------------------------------------
        public static implicit operator Int32Converter(int value)
        {
            return new Int32Converter(value);
        }
    }
}
