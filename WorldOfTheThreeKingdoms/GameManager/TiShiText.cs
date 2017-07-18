using Platforms;
using System;
using System.Collections.Generic;
using System.IO;
using Tools;

namespace WorldOfTheThreeKingdoms
{

    public class TiShiText
    {
        List<String> text = new List<String>();

        public TiShiText()
        {
            var fileName = "Content/Data/tishiText.txt";

            var lines = Platform.Current.LoadTexts(fileName);

            text = lines.NullToEmptyList();

            //TextReader tr = new StreamReader("Content/Data/tishiText.txt");
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
