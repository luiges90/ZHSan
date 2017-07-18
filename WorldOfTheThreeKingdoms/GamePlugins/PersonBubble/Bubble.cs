using GameFreeText;
using GameObjects;
using Microsoft.Xna.Framework;
using System;

namespace PersonBubble
{

    internal class Bubble
    {
        internal bool DrawingStarted;
        internal int LastingTime;
        internal Point Position;
        internal FreeRichText RichText;
        internal Person SpeakingPerson;
        internal DateTime StartingTime;
    }
}

