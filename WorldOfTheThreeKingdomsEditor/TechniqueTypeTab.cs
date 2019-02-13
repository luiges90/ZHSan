using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WorldOfTheThreeKingdomsEditor
{
    public class TechniqueTypeTab : ContentPage
    {
        public TechniqueTypeTab()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Welcome to Xamarin.Forms!" }
                }
            };
        }
    }
}