using System;
using System.Runtime.InteropServices;


namespace GameObjects.TroopDetail
{

    [StructLayout(LayoutKind.Sequential)]
    public struct TroopSounds
    {
        public string MovingSoundPath;
        public string NormalAttackSoundPath;
        public string CriticalAttackSoundPath;
    }
}

