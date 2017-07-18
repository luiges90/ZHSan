using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameGlobal;
using GameObjects;
using PluginInterface;
using PluginServices;
using PluginInterface.BaseInterface;
using System.Collections;



//using Microsoft.Xna.Framework;

//using Microsoft.Xna.Framework.Graphics;



namespace PluginServices
{
    public class AvailablePlugins : CollectionBase
    {
        public void Add(AvailablePlugin pluginToAdd)
        {
            base.List.Add(pluginToAdd);
        }

        public AvailablePlugin Find(string pluginNameOrPath)
        {
            foreach (AvailablePlugin plugin2 in base.List)
            {
                if (plugin2.Instance.PluginName.Equals(pluginNameOrPath) || plugin2.AssemblyPath.Equals(pluginNameOrPath))
                {
                    return plugin2;
                }
            }
            return null;
        }

        public void Remove(AvailablePlugin pluginToRemove)
        {
            base.List.Remove(pluginToRemove);
        }

        public AvailablePlugin this[int index]
        {
            get
            {
                return (base.List[index] as AvailablePlugin);
            }
        }
    }

 

}
