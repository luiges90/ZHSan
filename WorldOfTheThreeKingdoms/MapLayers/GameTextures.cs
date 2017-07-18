using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using GameObjects.MapDetail;
using GameObjects.TroopDetail;
using GameObjects.Animations;
using GameObjects.ArchitectureDetail;
using Platforms;
using GameManager;
using Tools;

namespace WorldOfTheThreeKingdoms.Resources

{

    public class GameTextures
    {
        //public Texture2D BackgroundMap;
        public Texture2D[] MapVeilTextures;
        public Texture2D[] MouseArrowTextures;
        public Texture2D[] RoutewayDirectionArrowTextures;
        public Texture2D[] RoutewayDirectionTailTextures;
        public Texture2D[] RoutewayTextures;
        public Texture2D SelectorTexture;
        public PlatformTexture[] TerrainTextures;
        public Texture2D[] TileFrameTextures;
        public Texture2D qizitupian;
        public Texture2D huangditupian;
        //public Texture2D jianzhubiaotibeijing;
        public Texture2D zidongcundangtupian;
        public Dictionary<int, Texture2D> mediumCityImg = new Dictionary<int, Texture2D>();
        public Dictionary<int, Texture2D> largeCityImg = new Dictionary<int, Texture2D>();
        public Texture2D[] guandetupian = new Texture2D[3];
        public Texture2D wanggetupian;
        public Texture2D EditModeGrid;
        public Texture2D LandConnect;
        public Texture2D WaterConnect;
        public Texture2D SingleConnect;

        public void LoadTextures(GraphicsDevice device, GameScenario scenario)
        {
            Exception exception;
            string str;
            //this.BackgroundMap = CacheManager.LoadTempTexture("Content/Textures/Resources/bg.jpg");

            try
            {
                this.MouseArrowTextures = new Texture2D[Enum.GetValues(typeof(MouseArrowKind)).Length];
                this.MouseArrowTextures[0] = CacheManager.LoadTempTexture("Content/Textures/Resources/MouseArrow/Normal.png");
                this.MouseArrowTextures[1] = CacheManager.LoadTempTexture("Content/Textures/Resources/MouseArrow/Left.png");
                this.MouseArrowTextures[2] = CacheManager.LoadTempTexture("Content/Textures/Resources/MouseArrow/Right.png");
                this.MouseArrowTextures[3] = CacheManager.LoadTempTexture("Content/Textures/Resources/MouseArrow/Top.png");
                this.MouseArrowTextures[4] = CacheManager.LoadTempTexture("Content/Textures/Resources/MouseArrow/Bottom.png");
                this.MouseArrowTextures[5] = CacheManager.LoadTempTexture("Content/Textures/Resources/MouseArrow/TopLeft.png");
                this.MouseArrowTextures[6] = CacheManager.LoadTempTexture("Content/Textures/Resources/MouseArrow/TopRight.png");
                this.MouseArrowTextures[7] = CacheManager.LoadTempTexture("Content/Textures/Resources/MouseArrow/BottomLeft.png");
                this.MouseArrowTextures[8] = CacheManager.LoadTempTexture("Content/Textures/Resources/MouseArrow/BottomRight.png");
                this.MouseArrowTextures[9] = CacheManager.LoadTempTexture("Content/Textures/Resources/MouseArrow/Selecting.png");
            }
            catch (Exception exception1)
            {
                exception = exception1;
                throw new Exception("The MouseArrow Textures are not completely loaded.\n" + exception.ToString());
            }
            try
            {
                foreach (TerrainDetail detail in scenario.GameCommonData.AllTerrainDetails.TerrainDetails.Values)
                {
                    detail.Textures = new GameObjects.MapDetail.TerrainTextures();
                    str = "Content/Textures/Resources/Terrain/" + detail.ID.ToString() + "/";
                    //if (Directory.Exists(str))
                    detail.Textures.BasicTextures.Clear();

                    foreach (string str2 in Platform.Current.GetFiles(str))   // Directory.GetFiles(str))
                    {
                        //FileInfo info = new FileInfo(str2);
                        if (str2.ToLower().EndsWith(".png") && str2.Contains("Basic"))
                        {
                            var platformTexture = new PlatformTexture()
                            {
                                Name = str2
                            };
                            detail.Textures.BasicTextures.Add(platformTexture);
                        }
                        /*    
                        else if (info.Extension.Equals(".png") && info.Name.Contains("TopLeftCorner"))
                        {
                            detail.Textures.TopLeftCornerTextures.Add(Platform.Current.LoadTexture(str2));
                        }
                        else if (info.Extension.Equals(".png") && info.Name.Contains("TopRightCorner"))
                        {
                            detail.Textures.TopRightCornerTextures.Add(Platform.Current.LoadTexture(str2));
                        }
                        else if (info.Extension.Equals(".png") && info.Name.Contains("BottomLeftCorner"))
                        {
                            detail.Textures.BottomLeftCornerTextures.Add(Platform.Current.LoadTexture(str2));
                        }
                        else if (info.Extension.Equals(".png") && info.Name.Contains("BottomRightCorner"))
                        {
                            detail.Textures.BottomRightCornerTextures.Add(Platform.Current.LoadTexture(str2));
                        }
                        else if (info.Extension.Equals(".png") && info.Name.Contains("Centre"))
                        {
                            detail.Textures.CentreTextures.Add(Platform.Current.LoadTexture(str2));
                        }
                        else if (info.Extension.Equals(".png") && info.Name.Contains("LeftEdge"))
                        {
                            detail.Textures.LeftEdgeTextures.Add(Platform.Current.LoadTexture(str2));
                        }
                        else if (info.Extension.Equals(".png") && info.Name.Contains("TopEdge"))
                        {
                            detail.Textures.TopEdgeTextures.Add(Platform.Current.LoadTexture(str2));
                        }
                        else if (info.Extension.Equals(".png") && info.Name.Contains("RightEdge"))
                        {
                            detail.Textures.RightEdgeTextures.Add(Platform.Current.LoadTexture(str2));
                        }
                        else if (info.Extension.Equals(".png") && info.Name.Contains("BottomEdge"))
                        {
                            detail.Textures.BottomEdgeTextures.Add(Platform.Current.LoadTexture(str2));
                        }
                        else if (info.Extension.Equals(".png") && info.Name.Contains("Left"))
                        {
                            detail.Textures.LeftTextures.Add(Platform.Current.LoadTexture(str2));
                        }
                        else if (info.Extension.Equals(".png") && info.Name.Contains("Top"))
                        {
                            detail.Textures.TopTextures.Add(Platform.Current.LoadTexture(str2));
                        }
                        else if (info.Extension.Equals(".png") && info.Name.Contains("Right"))
                        {
                            detail.Textures.RightTextures.Add(Platform.Current.LoadTexture(str2));
                        }
                        else if (info.Extension.Equals(".png") && info.Name.Contains("Bottom"))
                        {
                            detail.Textures.BottomTextures.Add(Platform.Current.LoadTexture(str2));
                        }
                        */
                    }

                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw new Exception("The Terrain Textures are not completely loaded.\n" + exception.ToString());
            }
            //try         //加载建筑
            //{
            //    foreach (ArchitectureKind kind in scenario.GameCommonData.AllArchitectureKinds.ArchitectureKinds.Values)
            //    {
            //        kind.Device = device;
            //    }
            //}
            //catch (Exception exception3)
            //{
            //    exception = exception3;
            //    throw new Exception("The Architecture Textures are not completely loaded.\n" + exception.ToString());
            //}
            try
            {
                foreach (MilitaryKind kind2 in scenario.GameCommonData.AllMilitaryKinds.MilitaryKinds.Values)
                {
                    str = "Content/Textures/Resources/Troop/" + kind2.ID.ToString() + "/";
                    string soundDir = @"Content\Sound\Troop\" + kind2.ID.ToString() + "/";
                    //if (Platform.Current.DirectoryExists(str))  // Directory.Exists(str))
                    //{
                        TroopTextures textures = new TroopTextures
                        {
                            //Device = device,
                            MoveTextureFileName = str + "Move.png",
                            AttackTextureFileName = str + "Attack.png",
                            BeAttackedTextureFileName = str + "BeAttacked.png",
                            CastTextureFileName = str + "Cast.png",
                            BeCastedTextureFileName = str + "BeCasted.png"
                        };
                        //if (!Platform.Current.FileExists(textures.CastTextureFileName))
                        //{
                        //    textures.CastTextureFileName = textures.AttackTextureFileName;
                        //}
                        //textures.BeCastedTextureFileName = str + "BeCasted.png";
                        //if (!Platform.Current.FileExists(textures.BeCastedTextureFileName))
                        //{
                        //    textures.BeCastedTextureFileName = textures.BeAttackedTextureFileName;
                        //}
                        kind2.Textures = textures;                        
                        TroopSounds sounds = new TroopSounds
                        {
                            MovingSoundPath = soundDir + "Moving",
                            NormalAttackSoundPath = soundDir + "NormalAttack",
                            CriticalAttackSoundPath = soundDir + "CriticalAttack"
                        };
                        kind2.Sounds = sounds;
                    //}
                }
            }
            catch (Exception exception4)
            {
                exception = exception4;
                throw new Exception("The Troop Textures are not completely loaded.\n" + exception.ToString());
            }
            try
            {
                this.MapVeilTextures = new Texture2D[Enum.GetValues(typeof(MapVeilKind)).Length];
                this.MapVeilTextures[0] = CacheManager.LoadTempTexture("Content/Textures/Resources/MapVeil/Gray.png");
            }
            catch (Exception exception5)
            {
                exception = exception5;
                throw new Exception("The MapVeil Textures are not completely loaded.\n" + exception.ToString());
            }
            try
            {
                this.TileFrameTextures = new Texture2D[Enum.GetValues(typeof(TileFrameKind)).Length];
                this.TileFrameTextures[0] = CacheManager.LoadTempTexture("Content/Textures/Resources/TileFrame/White.png");
                this.TileFrameTextures[1] = CacheManager.LoadTempTexture("Content/Textures/Resources/TileFrame/Black.png");
                this.TileFrameTextures[2] = CacheManager.LoadTempTexture("Content/Textures/Resources/TileFrame/Red.png");
                this.TileFrameTextures[3] = CacheManager.LoadTempTexture("Content/Textures/Resources/TileFrame/Blue.png");
                this.TileFrameTextures[4] = CacheManager.LoadTempTexture("Content/Textures/Resources/TileFrame/Green.png");
                this.TileFrameTextures[5] = CacheManager.LoadTempTexture("Content/Textures/Resources/TileFrame/Purple.png");
                this.TileFrameTextures[6] = CacheManager.LoadTempTexture("Content/Textures/Resources/TileFrame/Yellow.png");
            }
            catch (Exception exception6)
            {
                exception = exception6;
                throw new Exception("The TileFrame Textures are not completely loaded.\n" + exception.ToString());
            }
            try
            {
                this.RoutewayTextures = new Texture2D[Enum.GetValues(typeof(RoutewayState)).Length];
                this.RoutewayTextures[0] = CacheManager.LoadTempTexture("Content/Textures/Resources/Routeway/Planning.png");
                this.RoutewayTextures[1] = CacheManager.LoadTempTexture("Content/Textures/Resources/Routeway/Active.png");
                this.RoutewayTextures[2] = CacheManager.LoadTempTexture("Content/Textures/Resources/Routeway/Inefficiency.png");
                this.RoutewayTextures[3] = CacheManager.LoadTempTexture("Content/Textures/Resources/Routeway/Building.png");
                this.RoutewayTextures[4] = CacheManager.LoadTempTexture("Content/Textures/Resources/Routeway/NoFood.png");
                this.RoutewayTextures[5] = CacheManager.LoadTempTexture("Content/Textures/Resources/Routeway/Hostile.png");
                this.RoutewayDirectionArrowTextures = new Texture2D[Enum.GetValues(typeof(SimpleDirection)).Length];
                this.RoutewayDirectionArrowTextures[0] = CacheManager.LoadTempTexture("Content/Textures/Resources/Routeway/DirectionArrowNone.png");
                this.RoutewayDirectionArrowTextures[1] = CacheManager.LoadTempTexture("Content/Textures/Resources/Routeway/DirectionArrowLeft.png");
                this.RoutewayDirectionArrowTextures[2] = CacheManager.LoadTempTexture("Content/Textures/Resources/Routeway/DirectionArrowUp.png");
                this.RoutewayDirectionArrowTextures[3] = CacheManager.LoadTempTexture("Content/Textures/Resources/Routeway/DirectionArrowRight.png");
                this.RoutewayDirectionArrowTextures[4] = CacheManager.LoadTempTexture("Content/Textures/Resources/Routeway/DirectionArrowDown.png");
                this.RoutewayDirectionTailTextures = new Texture2D[Enum.GetValues(typeof(SimpleDirection)).Length];
                this.RoutewayDirectionTailTextures[1] = CacheManager.LoadTempTexture("Content/Textures/Resources/Routeway/DirectionTailLeft.png");
                this.RoutewayDirectionTailTextures[2] = CacheManager.LoadTempTexture("Content/Textures/Resources/Routeway/DirectionTailUp.png");
                this.RoutewayDirectionTailTextures[3] = CacheManager.LoadTempTexture("Content/Textures/Resources/Routeway/DirectionTailRight.png");
                this.RoutewayDirectionTailTextures[4] = CacheManager.LoadTempTexture("Content/Textures/Resources/Routeway/DirectionTailDown.png");
            }
            catch (Exception exception7)
            {
                exception = exception7;
                throw new Exception("The Routeway Textures are not completely loaded.\n" + exception.ToString());
            }
            //try
            //{
                foreach (Animation animation in scenario.GameCommonData.AllTileAnimations.Animations.Values)
                {
                    //animation.Device = device;
                    animation.TextureFileName = "Content/Textures/Resources/Effects/TileEffect/" + animation.Name + ".png";

                    animation.MaleSoundPath = "Content/Sound/Animation/Male/" + animation.Name;
                    //if (!Platform.Current.FileContentExists(animation.MaleSoundPath, ""))
                    //{
                    //    animation.MaleSoundPath = "Content/Sound/Animation/" + animation.Name;
                    //}

                    animation.FemaleSoundPath = "Content/Sound/Animation/Female/" + animation.Name;
                    //if (!Platform.Current.FileContentExists(animation.FemaleSoundPath, ""))
                    //{
                    //    animation.FemaleSoundPath = "Content/Sound/Animation/" + animation.Name;
                    //}

                }
            //}
            //catch (Exception exception8)
            //{
            //    exception = exception8;
            //    throw new Exception("The TileAnimation Textures are not completely loaded.\n" + exception.ToString());
            //}
            try
            {
                scenario.GameCommonData.NumberGenerator.Device = device;
                scenario.GameCommonData.NumberGenerator.TextureFileName = "Content/Textures/Resources/Effects/CombatNumber/CombatNumber.png";
            }
            catch (Exception exception9)
            {
                exception = exception9;
                throw new Exception("The NumberGenerator Textures are not completely loaded.\n" + exception.ToString());
            }
            try
            {
                this.SelectorTexture = CacheManager.LoadTempTexture("Content/Textures/Resources/Effects/Selector/Selector.png");
            }
            catch (Exception exception10)
            {
                exception = exception10;
                throw new Exception("The NumberGenerator Textures are not completely loaded.\n" + exception.ToString());
            }

            try
            {
                this.qizitupian = CacheManager.LoadTempTexture("Content/Textures/Resources/Architecture/qizi.png");
            }
            catch (Exception exception11)
            {
                exception = exception11;
                throw new Exception("The qizi Textures are not completely loaded.\n" + exception.ToString());
            }

            try
            {
                this.huangditupian = CacheManager.LoadTempTexture("Content/Textures/Resources/Architecture/huangdi.png");
            }
            catch (Exception exception11)
            {
                exception = exception11;
                throw new Exception("The huangdi Textures are not completely loaded.\n" + exception.ToString());
            }

            try
            {
                this.LandConnect = CacheManager.LoadTempTexture("Content/Textures/Resources/Architecture/LandConnect.png");
                this.WaterConnect = CacheManager.LoadTempTexture("Content/Textures/Resources/Architecture/WaterConnect.png");
                this.SingleConnect = CacheManager.LoadTempTexture("Content/Textures/Resources/Architecture/SingleConnect.png");
            }
            catch (Exception exception12)
            {
                exception = exception12;
                throw new Exception("The ArchitectureConnect Textures are not completely loaded.\n" + exception.ToString());
            }

            //this.jianzhubiaotibeijing = CacheManager.LoadTempTexture("Content/Textures/Resources/Architecture/jianzhubiaotibeijing.png");

            mediumCityImg.Clear();
            largeCityImg.Clear();
            string[] filePaths = Platform.Current.GetFiles("Content/Textures/Resources/Architecture/").NullToEmptyList().Where(fi => fi.EndsWith("*.png")).NullToEmptyArray();
            foreach (String s in filePaths)
            {
                string fileName = s.Substring(s.LastIndexOf('/') + 1, s.LastIndexOf('.') - s.LastIndexOf('/') - 1);
                if (fileName.IndexOf('-') < 0)
                {
                    continue;
                }
                string archIdStr = fileName.Substring(0, fileName.IndexOf('-'));
                string size = fileName.Substring(fileName.IndexOf('-') + 1);

                int archId;
                if (int.TryParse(archIdStr, out archId) && (size.Equals("5") || size.Equals("13")))
                {
                    if (size.Equals("5"))
                    {
                        mediumCityImg.Add(archId, Platform.Current.LoadTexture(s, false));
                    }
                    else
                    {
                        largeCityImg.Add(archId, Platform.Current.LoadTexture(s, false));
                    }
                }
            }

            this.guandetupian[0] = CacheManager.LoadTempTexture("Content/Textures/Resources/Architecture/hengguan3.png");
            this.guandetupian[1] = CacheManager.LoadTempTexture("Content/Textures/Resources/Architecture/shuguan3.png");
            this.guandetupian[2] = CacheManager.LoadTempTexture("Content/Textures/Resources/Architecture/shuguan5.png");
            this.wanggetupian = CacheManager.LoadTempTexture("Content/Textures/Resources/TileFrame/wangge.png");
            this.EditModeGrid = CacheManager.LoadTempTexture("Content/Textures/Resources/TileFrame/Blue.png");
            this.zidongcundangtupian = CacheManager.LoadTempTexture("Content/Textures/Resources/Effects/zidongcundang.png");
        }
    }

}
