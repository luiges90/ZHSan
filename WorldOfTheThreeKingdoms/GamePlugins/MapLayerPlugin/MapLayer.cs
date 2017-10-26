using GameGlobal;
using GameManager;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using WorldOfTheThreeKingdoms.GameScreens;

namespace MapLayerPlugin
{

    internal class MapLayer : Tool
    {
        internal PlatformTexture NormalLayerActiveTexture;
        internal Rectangle NormalLayerPosition;
        internal PlatformTexture NormalLayerTexture;
        internal PlatformTexture RoutewayLayerActiveTexture;
        internal Rectangle RoutewayLayerPosition;
        internal PlatformTexture RoutewayLayerTexture;
        internal PlatformTexture LastTroopActiveTexture;
        internal Rectangle LastTroopPosition;
        internal PlatformTexture LastTroopTexture;
        internal PlatformTexture NextTroopActiveTexture;
        internal Rectangle NextTroopPosition;
        internal PlatformTexture NextTroopTexture;


        public override void Draw()
        {
#pragma warning disable CS0219 // The variable 'nullable' is assigned but its value is never used
            Rectangle? nullable;
#pragma warning restore CS0219 // The variable 'nullable' is assigned but its value is never used
            switch (Session.GlobalVariables.CurrentMapLayer)
            {
                case MapLayerKind.Normal:
                    nullable = null;
                    CacheManager.Draw(this.RoutewayLayerTexture, this.RoutewayLayerDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
                    break;

                case MapLayerKind.Routeway:
                    nullable = null;
                    CacheManager.Draw(this.RoutewayLayerActiveTexture, this.RoutewayLayerDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
                    break;
            }
            if (Session.GlobalVariables.ShowGrid)
            {
                CacheManager.Draw(this.NormalLayerActiveTexture, this.NormalLayerDisplayPosition, null , Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);

            }
            else
            {
                CacheManager.Draw(this.NormalLayerTexture, this.NormalLayerDisplayPosition, null , Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);

            }
            if (Hastroops())
            {
                CacheManager.Draw(this.LastTroopActiveTexture, this.LastTroopDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
                CacheManager.Draw(this.NextTroopActiveTexture, this.NextTroopDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
            }
            else
            {
                CacheManager.Draw(this.LastTroopTexture, this.LastTroopDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
                CacheManager.Draw(this.NextTroopTexture, this.NextTroopDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
            }
        }

        internal void Initialize(MainGameScreen screen)
        {            
            screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
            screen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
            screen.OnMouseLeftUp += new Screen.MouseLeftUp(this.screen_OnMouseLeftUP);
        }
        private bool over = false;
        private bool Hastroops()
        {
            Faction p = Session.Current.Scenario.CurrentPlayer;
            if (p != null && p.Troops.Count >= 1)
            {
                foreach (Troop t in p.Troops)
                {
                    if (!t.Operated)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        private void screen_OnMouseLeftUP(Point position)
        {
            if (base.Enabled && Hastroops())
            {
                if (StaticMethods.PointInRectangle(position, this.NextTroopDisplayPosition))
                {
                    over = false;
                    Faction p = Session.Current.Scenario.CurrentPlayer;
                    if(p.troopSequence> p.Troops.Count - 1)
                    {
                        p.troopSequence = p.Troops.Count - 1;
                    }
                    if ((p.Troops[p.troopSequence] as Troop) != null && p.troopSequence != -1)
                    {
                        (p.Troops[p.troopSequence] as Troop).DrawSelected = false;
                    }
                    for (int i = p.troopSequence; i < p.Troops.Count - 1; i++)
                     {
                         Troop t = p.Troops[i+1] as Troop;
                         if (!t.Operated)
                         {
                            p.troopSequence = i+1;
                            over = true;
                             break;
                         }
                     }
                    if (!over)
                    {
                        for (int i = 0; i < p.troopSequence; i++)
                        {
                            Troop t = p.Troops[i] as Troop;
                            if (!t.Operated)
                            {
                                p.troopSequence = i;
                                over = true;
                                break;
                            }
                        }
                    }
                    if ((p.Troops[p.troopSequence] as Troop) != null && p.troopSequence!=-1 && !(p.Troops[p.troopSequence] as Troop).Operated)
                    {
                        (p.Troops[p.troopSequence] as Troop).DrawSelected = true;
                        Session.MainGame.mainGameScreen.JumpTo((p.Troops[p.troopSequence] as Troop).Position);
                    }
                }
               else if (StaticMethods.PointInRectangle(position, this.LastTroopDisplayPosition))
                {
                    over = false;
                    Faction p = Session.Current.Scenario.CurrentPlayer;
                    if ((p.Troops[p.troopSequence] as Troop) != null && p.troopSequence != -1)
                    {
                        (p.Troops[p.troopSequence] as Troop).DrawSelected = false;
                    }
                    for (int i = p.troopSequence; i >0; i--)
                    {
                        Troop t = p.Troops[i - 1] as Troop;
                        if (!t.Operated)
                        {
                            p.troopSequence = i - 1;
                            over = true;
                            break;
                        }
                    }
                    if (!over)
                    {
                        for (int i = p.Troops.Count - 1; i > p.troopSequence; i--)
                        {
                            Troop t = p.Troops[i] as Troop;
                            if (!t.Operated)
                            {
                                p.troopSequence = i;
                                over = true;
                                break;
                            }
                        }
                    }
                    if ((p.Troops[p.troopSequence] as Troop) != null && p.troopSequence != -1 && !(p.Troops[p.troopSequence] as Troop).Operated)
                    {
                        (p.Troops[p.troopSequence] as Troop).DrawSelected = true;
                        Session.MainGame.mainGameScreen.JumpTo((p.Troops[p.troopSequence] as Troop).Position);
                    }
                }
            }
        }

        private void screen_OnMouseLeftDown(Point position)
        {
            if (base.Enabled)
            {
                if (StaticMethods.PointInRectangle(position, this.NormalLayerDisplayPosition))
                {
                    Session.GlobalVariables.ShowGrid = !Session.GlobalVariables.ShowGrid;
                    
                }

                else if (StaticMethods.PointInRectangle(position, this.RoutewayLayerDisplayPosition))
                {
                    if (Session.GlobalVariables.CurrentMapLayer == MapLayerKind.Normal)
                    {
                        Session.GlobalVariables.CurrentMapLayer = MapLayerKind.Routeway;
                    }
                    else 
                    {
                        Session.GlobalVariables.CurrentMapLayer = MapLayerKind.Normal;
                    }
                }
            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            if (base.Enabled)
            {
            }
        }

        public override void Update()
        {
        }

        private Rectangle NormalLayerDisplayPosition
        {
            get
            {
                return new Rectangle(this.NormalLayerPosition.X + this.DisplayOffset.X, this.NormalLayerPosition.Y + this.DisplayOffset.Y, this.NormalLayerPosition.Width, this.NormalLayerPosition.Height);
            }
        }

        private Rectangle RoutewayLayerDisplayPosition
        {
            get
            {
                return new Rectangle(this.RoutewayLayerPosition.X + this.DisplayOffset.X, this.RoutewayLayerPosition.Y + this.DisplayOffset.Y, this.RoutewayLayerPosition.Width, this.RoutewayLayerPosition.Height);
            }
        }

        private Rectangle LastTroopDisplayPosition
        {
            get
            {
                return new Rectangle(this.LastTroopPosition.X + this.DisplayOffset.X, this.LastTroopPosition.Y + this.DisplayOffset.Y, this.LastTroopPosition.Width, this.LastTroopPosition.Height);
            }
        }

        private Rectangle NextTroopDisplayPosition
        {
            get
            {
                return new Rectangle(this.NextTroopPosition.X + this.DisplayOffset.X, this.NextTroopPosition.Y + this.DisplayOffset.Y, this.NextTroopPosition.Width, this.NextTroopPosition.Height);
            }
        }
    }
}

