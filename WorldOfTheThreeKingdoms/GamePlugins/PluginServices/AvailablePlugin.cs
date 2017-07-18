using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PluginInterface;
using PluginInterface.BaseInterface;




namespace PluginServices
{
    public class AvailablePlugin
    {
        private string myAssemblyPath = "";
        private IBasePlugin myInstance = null;

        public string AssemblyPath
        {
            get
            {
                return this.myAssemblyPath;
            }
            set
            {
                this.myAssemblyPath = value;
            }
        }

        public IBasePlugin Instance
        {
            get
            {
                return this.myInstance;
            }
            set
            {
                this.myInstance = value;
            }
        }
    }

 

}
