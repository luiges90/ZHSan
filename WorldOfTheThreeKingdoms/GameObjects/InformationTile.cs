using GameGlobal;
using System;
using System.Runtime.InteropServices;


namespace GameObjects
{

    [StructLayout(LayoutKind.Sequential)]
    public struct InformationTile
    {
        private int lowCount;
        private int middleCount;
        private int highCount;
        private int fullCount;
        public InformationLevel Level
        {
            get
            {
                if (this.fullCount > 0)
                {
                    return InformationLevel.全;
                }
                if (this.highCount > 0)
                {
                    return InformationLevel.高;
                }
                if (this.middleCount > 0)
                {
                    return InformationLevel.中;
                }
                if (this.lowCount > 0)
                {
                    return InformationLevel.低;
                }
                return InformationLevel.无;
            }
        }
        public void AddInformationLevel(InformationLevel level)
        {
            switch (level)
            {
                case InformationLevel.低:
                    this.lowCount++;
                    break;

                case InformationLevel.中:
                    this.middleCount++;
                    break;

                case InformationLevel.高:
                    this.highCount++;
                    break;

                case InformationLevel.全:
                    this.fullCount++;
                    break;
            }
        }

        public void RemoveInformationLevel(InformationLevel level)
        {
            switch (level)
            {
                case InformationLevel.低:
                    this.lowCount--;
                    break;

                case InformationLevel.中:
                    this.middleCount--;
                    break;

                case InformationLevel.高:
                    this.highCount--;
                    break;

                case InformationLevel.全:
                    this.fullCount--;
                    break;
            }
        }

        public static string InformationString(InformationLevel level)
        {
            return level.ToString();
        }

        public override string ToString()
        {
            return InformationString(this.Level);
        }
    }
}

