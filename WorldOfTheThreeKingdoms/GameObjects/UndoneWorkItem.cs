using GameGlobal;
using System;
using System.Runtime.InteropServices;


namespace GameObjects
{

    [StructLayout(LayoutKind.Sequential)]
    public struct UndoneWorkItem
    {
        public UndoneWorkKind Kind;
        public Enum SubKind;
        public UndoneWorkItem(UndoneWorkKind kind, Enum subKind)
        {
            this.Kind = kind;
            this.SubKind = subKind;
        }

        public override string ToString()
        {
            return (this.Kind.ToString() + "_" + this.SubKind.ToString());
        }
    }
}

