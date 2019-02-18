using Platforms;
using System;
using System.Collections.Generic;
using System.IO;
using Tools;

namespace WorldOfTheThreeKingdoms
{

    public class LoadingScreenTooltip
    {
        List<String> text = new List<String>();

        public LoadingScreenTooltip()
        {
            var fileName = "Content/Data/LoadingScreenTooltips.txt";

            var lines = Platform.Current.LoadTexts(fileName);

            text = lines.NullToEmptyList();

            //TextReader tr = new StreamReader("Content/Data/LoadingScreenTooltips.txt");
            //while (tr.Peek() >= 0)
            //{
            //    String line = tr.ReadLine();
            //    text.Add(line);
            //}
        }

        public String getRandomText()
        {
            return text[GameGlobal.StaticMethods.Random(text.Count)];
        }
    }
}
