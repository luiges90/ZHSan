using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

using TInput = System.String;
using TOutput = System.String;

using FontProcessor = Microsoft.Xna.Framework.Content.Pipeline.Processors.FontDescriptionProcessor;

namespace WorldOfTheThreeKingdoms.Pipeline
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentProcessor attribute to specify the correct
    /// display name for this processor.
    /// </summary>
    [ContentProcessor(DisplayName = "WorldOfTheThreeKingdoms.Pipeline.MessageLProcessor")]
    public class MessageLProcessor : FontProcessor
    {
        public override SpriteFontContent Process(FontDescription input, ContentProcessorContext context)
        {
            string fullPath = Path.GetFullPath(MessageFile);
            context.AddDependency(fullPath);
            string letters = File.ReadAllText(fullPath, System.Text.Encoding.UTF8);
            foreach (char c in letters)
            {
                input.Characters.Add(c);
            }
            return base.Process(input, context);
        }
        [DefaultValue(@"Font\MessagesL.txt")]
        [DisplayName("Message File")]
        //[Description("The characters in this file will be automatically added to the font.")]
        public string MessageFile
        {
            get { return messageFile; }
            set { messageFile = value; }
        }
        private string messageFile = @"Font\MessagesL.txt";

    }

}