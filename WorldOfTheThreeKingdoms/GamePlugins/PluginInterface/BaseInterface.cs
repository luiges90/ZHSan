using System;
using System.Collections.Generic;
using System.Linq;
using GameFreeText;
using GameGlobal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameObjects;

namespace PluginInterface.BaseInterface
{
    public interface IBaseGame
    {
        object DataFeedBack();
    }

    public interface IBasePlugin
    {
        void Dispose();
        void Initialize(Screen screen);

        string Author { get; }
        string Description { get; }
        object Instance { get; }
        string PluginName { get; }
        string Version { get; }
    }

    public interface IPluginGraphics
    {
        void Draw();

        void SetGraphicsDevice();
        void Update(GameTime gameTime);
    }

    public interface IPluginXML
    {
        void LoadDataFromXMLDocument(string filename);
    }

    public interface IScreenDisableRects
    {
        void AddDisableRects();
        void RemoveDisableRects();
    }

 

 



 


 


 

}
