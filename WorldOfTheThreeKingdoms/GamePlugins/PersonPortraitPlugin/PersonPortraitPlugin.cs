using GameObjects;
using Microsoft.Xna.Framework.Graphics;
using PluginInterface;
using PluginInterface.BaseInterface;
using System;
//using System.Drawing;
using System.Reflection;
using System.Resources;


namespace PersonPortraitPlugin
{

    public class PersonPortraitPlugin : GameObject, IPersonPortrait, IBasePlugin
    {
        private string author = "clip_on";
        private string description = "人物头像";
        private const string Path = @"Content\Textures\GameComponents\PersonPortrait\";
        private PersonPortrait personPortrait = new PersonPortrait();
        private string pluginName = "PersonPortraitPlugin";
        private string version = "1.0.0";

        public void Dispose()
        {
        }

        //public bool HasPortrait(float id)
        //{
        //    return this.personPortrait.HasPortrait(id);
        //}

        //public Image GetImage(float id)
        //{
        //    return this.personPortrait.GetImage(id);
        //}

        //public Texture2D GetPortrait(float id)
        //{
        //    return this.personPortrait.GetPortrait(id);
        //}

        //public Texture2D GetSmallPortrait(float id)
        //{
        //    return this.personPortrait.GetSmallPortrait(id);
        //}

        //public Texture2D GetTroopPortrait(float id)
        //{
        //    return this.personPortrait.GetTroopPortrait(id);
        //}
        //public Texture2D GetFullPortrait(float id)
        //{
        //    return this.personPortrait.GetFullPortrait(id);
        //}
        public void Initialize(Screen screen)
        {
            this.personPortrait.TempImageFileName = @"Content\Textures\GameComponents\PersonPortrait\~tmp.image";
        }

        public void SetGraphicsDevice()
        {

        }

        public string Author
        {
            get
            {
                return this.author;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
        }

        public object Instance
        {
            get
            {
                return this;
            }
        }

        public string PluginName
        {
            get
            {
                return this.pluginName;
            }
        }

        public string Version
        {
            get
            {
                return this.version;
            }
        }
    }
}

