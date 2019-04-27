using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using WorldOfTheThreeKingdoms;
using Microsoft.Xna.Framework.Graphics;
using GameManager;
using GamePanels;
using Tools;
using Platforms;

namespace WorldOfTheThreeKingdoms.GameScreens.ScreenLayers
{

    public class General
    {
        public Person Person { get; set; }

        public int Force { get; set; }

        public float Life { get; set; }

        public float Skill { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 LandPosition { get; set; }

        public int LimiteWidth = 0;

        public bool LimiteOrder = true;

        AnimatedTexture atCurrent = null;

        public string Style { get; set; }

        public string Status { get; set; }

        Dictionary<string, AnimatedTexture> atGeneralStatus = new Dictionary<string, AnimatedTexture>();

        string[] styles = new string[] { "General01A", "General01B" };

        string[] status = new string[] { "WalkLeft", "WalkRight", "AttackLeft", "AttackRight", "Failure" };

        public string Direction = "";  //Up Down Left Right

        public float Duration = 0f;

        public float Delay = 0f;

        public Vector2 StartPos;

        public float ActionTime = 0f;

        public float Speed = 20f;

        public float SpeedExt = 0f;

        public float SpeedPlus = 0f;

        public float SpeedNow = 0f;

        public float SayTimeTotal = 0f;

        public float SayTime = 0f;

        public bool SayDisappear = true;

        public string SayWords = "";

        public bool IsPaused
        {
            get
            {
                return atCurrent == null ? true : atCurrent.Paused;
            }
        }
        
        public General(Person p)
        {
            Person = p;

            foreach (var style in styles)
            {
                foreach (var gen in status)
                {
                    var genStatus = new AnimatedTexture(@"Content\Textures\Resources\Dantiao\" + style, gen, "", true, 5)
                    {
                        Depth = DantiaoLayer.depth - 0.035f
                    };

                    atGeneralStatus.Add(style + "-" + gen, genStatus);
                }
            }

        }

        public void ChangeStatus(string style, string status)
        {
            Style = style;

            Status = status;

            atCurrent = atGeneralStatus[Style + "-" + Status];
        }

        public void ChangeWalkToAttack()
        {
            Status = Status.Replace("Walk", "Attack");

            ChangeStatus(Style, Status);
        }

        public void ChangeAttackToWalk()
        {
            Status = Status.Replace("Attack", "Walk");

            ChangeStatus(Style, Status);
        }

        public void ChangeStatusDirection()
        {
            if (Status.Contains("Left"))
            {
                Status = Status.Replace("Left", "Right");
                Direction = "Right";
            }
            else if (Status.Contains("Right"))
            {
                Status = Status.Replace("Right", "Left");
                Direction = "Left";
            }

            ChangeStatus(Style, Status);
        }

        public void Pause()
        {
            Direction = "";
            atCurrent.Paused = true;
        }

        public void Start()
        {
            StartPos = LandPosition;
            ActionTime = 0f;
            atCurrent.Paused = false;

            atCurrent.ChangeFrame(Convert.ToInt32(5 * (Speed + SpeedExt + SpeedPlus) / Speed));
        }

        public void ChangePosition(Vector2 landPos)
        {
            LandPosition = landPos;
        }

        public void Update(float gameTime, Vector2 screenPos)
        {
            if (atCurrent == null)
            {

            }
            else
            {
                if (SayTime > 0f)
                {
                    SayTime -= gameTime;

                    if (SayTime < 0f)
                    {
                        SayTime = 0f;
                        //SayWords = "";
                    }
                }

                if (Delay > 0)
                {
                    Delay -= gameTime;

                    if (Delay < 0)
                    {
                        Delay = 0;
                    }

                }

                if (Duration > 0)
                {
                    Duration -= gameTime;

                    if (Duration <= 0)
                    {
                        Duration = 0;

                        Direction = "";

                        if (Status.Contains("Walk"))
                        {
                            Pause();
                        }
                    }
                }

                if (Delay == 0)
                {

                    ActionTime += gameTime;

                    if (String.IsNullOrEmpty(Direction))
                    {

                    }
                    else
                    {
                        SpeedNow = Speed + SpeedExt + ActionTime * SpeedPlus;

                        if (SpeedNow <= 0)
                        {
                            SpeedNow = 0;
                        }

                        //位移s＝Vot + at²/ 2

                        var moveDis = (Speed + SpeedExt) * ActionTime + SpeedPlus * ActionTime * ActionTime / 2;

                        //ActionTime * realSpeed;

                        Vector2 movePos = Vector2.Zero;

                        if (Direction == "Left")
                        {
                            movePos = new Vector2(-moveDis, 0);

                            if (StartPos.X + movePos.X - DantiaoLayer.basePos.X > 0)
                            {
                                LandPosition = StartPos + movePos;
                            }
                        }
                        else if (Direction == "Right")
                        {
                            movePos = new Vector2(moveDis, 0);

                            if (StartPos.X + movePos.X - DantiaoLayer.basePos.X < 2200 - 120)
                            {
                                LandPosition = StartPos + movePos;
                            }
                        }
                        else if (Direction == "Up")
                        {
                            movePos = new Vector2(0, -moveDis);

                            if (StartPos.Y + movePos.Y - DantiaoLayer.basePos.Y > 150)
                            {
                                LandPosition = StartPos + movePos;
                            }
                        }
                        else if (Direction == "Down")
                        {
                            movePos = new Vector2(0, moveDis);

                            if (StartPos.Y + movePos.Y - DantiaoLayer.basePos.Y < 450)
                            {
                                LandPosition = StartPos + movePos;
                            }
                        }


                    }
                }

                Position = LandPosition - screenPos;

                atCurrent.Position = Position;

                atCurrent.LimiteWidth = LimiteWidth;

                atCurrent.LimiteOrder = LimiteOrder;

                atCurrent.UpdateFrame(gameTime);
            }
        }

        public void Draw()
        {
            if (atCurrent == null)
            {

            }
            else
            {
                atCurrent.DrawFrame(null);
            }

        }

    }

    public class DantiaoLayer
    {
        public static List<Person> Persons = null;

        float totalTime = 0f;

        float elapsedTime = 0f;

        float fightTime = 0f;

        Vector2 scale = Vector2.One;

        public float Alpha = 0f;

        public static float depth = 0.1f;

        public bool IsVisible = true;

        public bool IsStart = true;

        public static Vector2 basePos = Vector2.Zero;

        Vector2 cloudPos = new Vector2(15, 10);

        Rectangle cloudRec = new Rectangle(0, 0, 1000 - 22, 222);

        Vector2 treePos = new Vector2(15, 150);

        Rectangle treeRec = new Rectangle(0, 0, 1000 - 22, 80);

        Vector2 landPos = new Vector2(15, 225);

        Rectangle landRec = new Rectangle(0, 0, 1000 - 22, 530);

        Vector2 general1Pos = Vector2.Zero;

        Vector2 general2Pos = Vector2.Zero;

        Vector2 screenPosPre = Vector2.Zero;

        Vector2 screenPos = Vector2.Zero;

        public int round = 0;

        int Speed = 1;

        ButtonTexture btnStory, btnPagePre, btnPageNext, btnSpeed, btnSpeedUp, btnSpeedDown;

        General genLeft, genRight;

        int moveDistance = 0;

        float moveTime = 0f;

        public string Stage = "Cloud";

        public int Result = 0;

        string Title = "";

        bool ViewExit = false;

        public TroopDamage damage = null;

        public DantiaoLayer(Person left, Person right)
        {
            //scale = new Vector2(Convert.ToSingle(Session.ResolutionX) / 800f, Convert.ToSingle(Session.ResolutionY) / 480f);

            basePos = new Vector2((Session.ResolutionX - 1000) / 2, (Session.ResolutionY - 620) / 2);

            btnStory = new ButtonTexture(@"Content\Textures\Resources\Dantiao\Story", "Story", basePos + new Vector2(25, 545))
            {
                Visible = false
            };

            btnStory.OnButtonPress += (sender, e) =>
            {
                btnStory.Selected = !btnStory.Selected;
            };

            btnPagePre = new ButtonTexture(@"Content\Textures\Resources\Dantiao\Page", "Left", basePos + new Vector2(30 + 110, 555));

            btnPagePre.OnButtonPress += (sender, e) =>
            {

            };

            btnPageNext = new ButtonTexture(@"Content\Textures\Resources\Dantiao\Page", "Right", basePos + new Vector2(38 + 228, 555));

            btnPageNext.OnButtonPress += (sender, e) =>
            {

            };

            btnSpeed = new ButtonTexture(@"Content\Textures\Resources\Start\Setting", "Setting", basePos + new Vector2(750, 30))
            {
                Scale = 0.5f,
                Visible = false
            };

            btnSpeed.OnButtonPress += (sender, e) =>
            {

            };

            btnSpeedDown = new ButtonTexture(@"Content\Textures\Resources\Dantiao\Page", "Left", basePos + new Vector2(750 + 110, 35))
            {
                Enable = false,
                Visible = false
            };

            btnSpeedDown.OnButtonPress += (sender, e) =>
            {
                Speed--;
                btnSpeedUp.Enable = true;
                if (Speed == 1)
                {
                    btnSpeedDown.Enable = false;
                }
            };

            btnSpeedUp = new ButtonTexture(@"Content\Textures\Resources\Dantiao\Page", "Right", basePos + new Vector2(750 + 170, 35))
            {
                Visible = false
            };

            btnSpeedUp.OnButtonPress += (sender, e) =>
            {
                Speed++;
                btnSpeedDown.Enable = true;
                if (Speed == 5)
                {
                    btnSpeedUp.Enable = false;
                }
            };

            genLeft = new General(left)
            {
                Force = left.ChallengeStrength,
                Life = 100,
                Skill = 100
            };

            genLeft.ChangeStatus("General01A", "WalkRight");

            genLeft.ChangePosition(basePos + new Vector2(500-100, 250));

            genLeft.Pause();

            genRight = new General(right)
            {
                Force = right.ChallengeStrength,
                Life = 100,
                Skill = 100
            };

            genRight.ChangeStatus("General01B", "WalkLeft");

            genRight.ChangePosition(basePos + new Vector2(2200-1000+500-80, 250));

            genRight.Pause();

            Session.PlayMusic("Battle");
        }

        public void Start()
        {
            elapsedTime = 0f;
            IsVisible = true;
        }

        public void SetScreenPos()
        {
            screenPos.X = (genLeft.LandPosition.X + genRight.LandPosition.X) / 2 + 64 - 500 - basePos.X;
            if (screenPos.X < 0)
            {
                screenPos.X = 0;
            }
            else if (screenPos.X > 2200 - 1000)
            {
                screenPos.X = 2200 - 1000;
            }
            screenPos.Y = (genLeft.LandPosition.Y + genRight.LandPosition.Y) / 2 + 64 - 350 - basePos.Y;
            if (screenPos.Y < 0)
            {
                screenPos.Y = 0;
            }
            //else if (screenPos.Y > 300)
            //{
            //    screenPos.Y = 300;
            //}
        }

        public void Update(float gameTime)
        {
            if (IsVisible && IsStart)
            {
                totalTime += gameTime;

                elapsedTime += gameTime;

                if (Stage == "Cloud")
                {
                    if (elapsedTime <= 1f)
                    {
                        Alpha = 0f;
                    }
                    else
                    {
                        Stage = "Start";
                        elapsedTime = 0f;
                    }
                }
                else
                {
                    if (totalTime >= 120f && Stage != "Over" && Stage != "OverOut")
                    {
                        Stage = "Over";
                        genLeft.SayDisappear = false;
                        genRight.SayDisappear = false;

                        Result = -1;
                        genLeft.SayWords = "棋逢对手！";
                        genLeft.SayTime = 2f;
                        genRight.SayWords = "痛快痛快！";
                        genRight.SayTime = 2f;

                        Title = "双方平局";
                    }

                    if (Stage == "Start")
                    {
                        if (elapsedTime <= 2f)
                        {
                            Alpha = elapsedTime / 2f;
                        }
                        else
                        {
                            Alpha = 1f;
                            Stage = "Gen1Move";
                            elapsedTime = 0f;
                            Platform.Current.PlayEffect(@"Content\Sound\Dantiao\Moving");
                        }
                    }
                    else if (Stage == "Gen1Move")
                    {
                        genLeft.SpeedExt = 30;
                        genLeft.Direction = "Right";
                        genLeft.Start();
                        Stage = "Gen1Moving";
                        elapsedTime = 0f;
                        screenPosPre = screenPos;
                    }
                    else if (Stage == "Gen1Moving")
                    {
                        screenPos = screenPosPre + (genLeft.LandPosition - genLeft.StartPos);

                        if (elapsedTime >= 2f)
                        {
                            Stage = "Gen1Speak";
                            elapsedTime = 0f;
                        }
                    }
                    else if (Stage == "Gen1Speak")
                    {
                        genLeft.Pause();
                        genLeft.SayTimeTotal = 2f;
                        genLeft.SayTime = 2f;
                        genLeft.SayWords = $"吾乃{genLeft.Person.Name}，哪个敢来应战？";

                        if (genLeft.Person.Name == "曹操")
                        {
                            genLeft.SayWords = $"吾乃{genLeft.Person.Name}，将军别来无恙？";
                        }

                        Stage = "Gen1Speaking";
                        elapsedTime = 0f;
                    }
                    else if (Stage == "Gen1Speaking")
                    {
                        if (elapsedTime >= 2f)
                        {
                            Stage = "Gen1Run";
                            elapsedTime = 0f;
                        }
                    }
                    else if (Stage == "Gen1Run")
                    {
                        genLeft.SpeedExt = 15;
                        genLeft.SpeedPlus = 10;
                        genLeft.Direction = "Right";
                        genLeft.Start();
                        Stage = "Gen1Running";
                        elapsedTime = 0f;
                        screenPosPre = screenPos;
                        Platform.Current.PlayEffect(@"Content\Sound\Dantiao\Moving");
                    }
                    else if (Stage == "Gen1Running")
                    {
                        if (elapsedTime <= 2f)
                        {
                            screenPos = screenPosPre + new Vector2(genRight.LandPosition.X + 64 - 530 - basePos.X - screenPosPre.X, 0) * elapsedTime / 2f;
                        }
                        else if (elapsedTime >= 3f)
                        {
                            Stage = "Gen2Speak";
                        }
                    }
                    else if (Stage == "Gen2Speak")
                    {
                        genRight.SayTimeTotal = 2f;
                        genRight.SayTime = 2f;
                        genRight.SayWords = $"上将{genRight.Person.Name}在此，贼将休得猖狂！";
                        Stage = "Gen2Speaking";
                        elapsedTime = 0f;
                    }
                    else if (Stage == "Gen2Speaking")
                    {
                        if (elapsedTime >= 2f)
                        {
                            Stage = "Gen2Run";
                            elapsedTime = 0f;
                        }
                    }
                    else if (Stage == "Gen2Run")
                    {
                        genRight.SpeedExt = 45;
                        genRight.SpeedPlus = 25;
                        genRight.Direction = "Left";
                        genRight.Start();
                        Stage = "Gen2Running";
                        elapsedTime = 0f;
                        screenPosPre = screenPos;
                        Platform.Current.PlayEffect(@"Content\Sound\Dantiao\Moving");
                    }
                    else if (Stage == "Gen2Running")
                    {
                        screenPos = new Vector2(genRight.LandPosition.X + 64 - 530 - basePos.X, screenPos.Y);

                        var centerLeft = genLeft.LandPosition + new Vector2(64, 64);

                        var centerRight = genRight.LandPosition + new Vector2(64, 64);

                        var distance = Math.Sqrt((centerRight.X - centerLeft.X) * (centerRight.X - centerLeft.X) + (centerRight.Y - centerLeft.Y));

                        if (distance <= 100)
                        {
                            Stage = "FightRush";
                        }
                    }
                    else if (Stage == "WaitRush")
                    {
                        SetScreenPos();

                        var centerLeft = genLeft.LandPosition + new Vector2(64, 64);

                        var centerRight = genRight.LandPosition + new Vector2(64, 64);

                        if (Math.Abs(genLeft.LandPosition.Y - genRight.LandPosition.Y) <= 10f)
                        {
                            var distance = Math.Sqrt((centerRight.X - centerLeft.X) * (centerRight.X - centerLeft.X) + (centerRight.Y - centerLeft.Y));

                            if (distance <= 100)
                            {
                                if (genLeft.Duration == 0 && genRight.Duration == 0)
                                {
                                    Stage = "FightRush";
                                }
                            }
                        }
                    }
                    else if (Stage == "FightRush")
                    {
                        SetScreenPos();

                        round++;

                        genLeft.ChangeWalkToAttack();

                        genLeft.Start();

                        genRight.ChangeWalkToAttack();

                        genRight.Start();

                        elapsedTime = 0f;

                        if (!genLeft.IsPaused && !genRight.IsPaused && (genLeft.SpeedNow > 68 || genRight.SpeedNow > 68))
                        {
                            //速度超過一定值，則對沖過去，開始減速
                            Stage = "FightRun";

                            Platform.Current.PlayEffect(@"Content\Sound\Dantiao\Moving");
                        }
                        else
                        {
                            //速度不到一定值，則開始對打
                            genLeft.Direction = "";
                            genRight.Direction = "";

                            Stage = "Fighting";
                        }
                    }
                    else if (Stage == "FightRun")
                    {
                        SetScreenPos();

                        //交匯情況下的武力傷害
                        Fight(gameTime, true);

                        if (elapsedTime > 0.4f)
                        {
                            fightTime = 0f;

                            genLeft.ChangeAttackToWalk();

                            genLeft.SpeedPlus = -new Random().Next(3, 7);

                            genLeft.Start();

                            genRight.ChangeAttackToWalk();

                            genRight.SpeedPlus = -new Random().Next(3, 7);

                            genRight.Start();

                            Stage = "FightRunStop";
                        }
                    }
                    else if (Stage == "FightRunStop")
                    {
                        SetScreenPos();

                        if (genLeft.SpeedNow <= 20 || genRight.SpeedNow <= 20)
                        {
                            genLeft.Pause();
                            genRight.Pause();

                            elapsedTime = 0f;

                            Stage = "FightRunBack";
                        }
                    }
                    else if (Stage == "FightRunBack")
                    {
                        SetScreenPos();

                        if (elapsedTime > 0.3f)
                        {
                            genLeft.ChangeStatusDirection();

                            genLeft.SpeedPlus = new Random().Next(1, 8);

                            genRight.ChangeStatusDirection();

                            genRight.SpeedPlus = new Random().Next(1, 8);

                            genLeft.Start();

                            genRight.Start();

                            Stage = "WaitRush";

                            Platform.Current.PlayEffect(@"Content\Sound\Dantiao\Moving");
                        }
                    }
                    else if (Stage == "Fighting")
                    {
                        //對打情況下的傷害
                        Fight(gameTime, false);

                        if (elapsedTime >= 5f)
                        {
                            General gen1, gen2;

                            var ran = new Random().Next(0, 10);

                            if (ran == 0 || ran == 2 || ran == 4 || ran == 6 || ran == 8)
                            {
                                gen1 = genLeft;
                                gen2 = genRight;
                            }
                            else
                            {
                                gen1 = genRight;
                                gen2 = genLeft;
                            }

                            if (0 <= ran && ran <= 2)
                            {
                                //保持不動

                            }
                            else if (3 <= ran && ran <= 5)
                            {
                                //後退、跟進
                                if (gen1.LandPosition.X <= gen2.LandPosition.X)
                                {
                                    if (gen1.LandPosition.X - basePos.X > 60)
                                    {
                                        gen1.Direction = "Left";
                                        gen1.ChangeStatus(gen1.Style, "WalkRight");
                                        gen1.Duration = 2f;

                                        gen2.Direction = "Left";
                                        gen2.ChangeStatus(gen2.Style, "WalkLeft");
                                        gen2.Delay = 1f;

                                        gen1.Start();

                                        gen2.Start();

                                        Stage = "WaitRush";
                                    }
                                }
                                else
                                {
                                    if (gen1.LandPosition.X - basePos.X < 2200 - 200)
                                    {
                                        gen1.Direction = "Right";
                                        gen1.ChangeStatus(gen1.Style, "WalkLeft");
                                        gen1.Duration = 2f;

                                        gen2.Direction = "Right";
                                        gen2.ChangeStatus(gen2.Style, "WalkRight");
                                        gen2.Delay = 1f;

                                        gen1.Start();

                                        gen2.Start();

                                        Stage = "WaitRush";
                                    }
                                }

                            }
                            else if (6 <= ran && ran <= 7)
                            {
                                if (gen1.LandPosition.Y - basePos.Y > 200 && gen2.LandPosition.Y - basePos.Y > 150)
                                {
                                    //向上
                                    gen1.Direction = "Up";
                                    gen1.ChangeAttackToWalk();
                                    gen1.Duration = 2f;

                                    gen2.Direction = "Up";
                                    gen2.ChangeAttackToWalk();
                                    gen2.Delay = 1f;

                                    gen1.Start();

                                    gen2.Start();

                                    Stage = "WaitRush";

                                    Platform.Current.PlayEffect(@"Content\Sound\Dantiao\Moving");
                                }
                            }
                            else if (8 <= ran && ran <= 10)
                            {
                                if (gen1.LandPosition.Y - basePos.Y < 460 && gen2.LandPosition.Y - basePos.Y < 460)
                                {
                                    //向下
                                    gen1.Direction = "Down";
                                    gen1.ChangeAttackToWalk();
                                    gen1.Duration = 2f;

                                    gen2.Direction = "Down";
                                    gen2.ChangeAttackToWalk();
                                    gen2.Delay = 1f;

                                    gen1.Start();

                                    gen2.Start();

                                    Stage = "WaitRush";


                                    Platform.Current.PlayEffect(@"Content\Sound\Dantiao\Moving");
                                }
                            }

                            elapsedTime = 0f;
                        }
                    }
                    else if (Stage == "Over")
                    {
                        if (InputManager.IsDown)
                        {
                            Stage = "OverOut";
                            elapsedTime = 0f;
                        }
                    }
                    else if (Stage == "OverOut")
                    {
                        if (elapsedTime >= 1f)
                        {
                            Session.MainGame.mainGameScreen.dantiaoLayer = null;

                            if (damage == null)
                            {
                                Session.MainGame.mainGameScreen.ReturnToMainMenu();
                            }
                            else
                            {
                                Session.MainGame.mainGameScreen.cloudLayer.IsStart = false;
                                Session.MainGame.mainGameScreen.cloudLayer.IsVisible = false;
                                Session.MainGame.mainGameScreen.cloudLayer.Reverse = false;

                                damage.ChallengeHappened = true;

                                damage.ChallengeStarted = false;

                                damage.ChallengeResult = Result;
                                damage.ChallengeSourcePerson = DantiaoLayer.Persons[0]; //maxStrengthPerson;
                                damage.ChallengeDestinationPerson = DantiaoLayer.Persons[1];
                                //destination;
                                //if (returnValue >= -4 && returnValue <= 10 && returnValue != 0)
                                //{
                                //    flag = returnValue;
                                //}
                                //else   //返回值出错时避免跳出
                                //{
                                //    flag = (GameObject.Chance(chance) ? 1 : 2);
                                //}

                                Session.MainGame.mainGameScreen.EnableUpdate = true;
                            }

                            DantiaoLayer.Persons = null;

                        }
                        Alpha = 1 - elapsedTime;
                    }

                    float elapsedTime2 = elapsedTime < 1f ? elapsedTime : 1f;

                    btnStory.Update();

                    if (btnStory.Selected)
                    {
                        btnPagePre.Update();

                        btnPageNext.Update();
                    }

                    btnSpeed.Update();

                    btnSpeedUp.Update();

                    btnSpeedDown.Update();

                    landRec.Height = 620 - 10 - Convert.ToInt32(landPos.Y);

                    cloudRec.X = Convert.ToInt32(screenPos.X * 0.2f);

                    treeRec.X = Convert.ToInt32(screenPos.X * 0.5f);

                    landRec.X = Convert.ToInt32(screenPos.X);

                    genLeft.Update(gameTime, screenPos);

                    genRight.Update(gameTime, screenPos);


                    landPos = new Vector2(15, 225 - screenPos.Y);

                    if (String.IsNullOrEmpty(Title))
                    {

                    }
                    else
                    {
                        var time = float.Parse("0." + totalTime.ToString().Split(new string[] { "." }, StringSplitOptions.None)[0]);

                        if (time <= 0.5f)
                        {
                            ViewExit = true;
                        }
                        else
                        {
                            ViewExit = false;
                        }

                    }

                }

            }
        }

        public void Fight(float gameTime, bool rush)
        {
            fightTime += gameTime;

            if (fightTime >= 0.8f || rush && fightTime >= 0.3f)
            {
                fightTime -= (rush ? 0.3f : 0.8f);

                Platform.Current.PlayEffect(@"Content\Sound\Dantiao\NormalAttack");

                genLeft.Life -= Convert.ToSingle(new Random().Next(0, genRight.Force / 5) * 8) / 10f;

                genRight.Life -= Convert.ToSingle(new Random().Next(0, genLeft.Force / 5) * 8) / 10f;

                if (genLeft.Life <= 0 || genRight.Life <= 0)
                {
                    Stage = "Over";
                    if (genLeft.Life <= 0 && genRight.Life <= 0)
                    {
                        Result = -1;
                        genLeft.SayWords = "棋逢对手！";
                        genLeft.SayTime = 2f;
                        genRight.SayWords = "痛快痛快！";
                        genRight.SayTime = 2f;

                        genLeft.SayDisappear = false;
                        genRight.SayDisappear = false;

                        Title = "双方平局";
                    }
                    else if (genRight.Life <= 0)
                    {
                        Result = 1;

                        genRight.ChangeStatus("General01B", "Failure");

                        genLeft.SayWords = "谁敢再战！";
                        genLeft.SayTime = 2f;
                        genLeft.SayDisappear = false;

                        Title = genLeft.Person.Name + "获胜！";
                    }
                    else if (genLeft.Life <= 0)
                    {
                        Result = 2;

                        genLeft.ChangeStatus("General01A", "Failure");

                        genRight.SayWords = "不过如此！";
                        genRight.SayTime = 2f;
                        genRight.SayDisappear = false;

                        Title = genRight.Person.Name + "获胜！";
                    }

                }
            }
        }

        public void Draw()
        {
            if (IsVisible && Stage != "Cloud")
            {

                CacheManager.Draw(@"Content\Textures\Resources\Dantiao\Cloud.png", basePos + cloudPos, cloudRec, Color.White * Alpha, SpriteEffects.None, scale, depth - 0.01f);

                CacheManager.Draw(@"Content\Textures\Resources\Dantiao\Tree.png", basePos + treePos, treeRec, Color.White * Alpha, SpriteEffects.None, scale, depth - 0.02f);

                CacheManager.Draw(@"Content\Textures\Resources\Dantiao\Land.png", basePos + landPos, landRec, Color.White * Alpha, SpriteEffects.None, scale, depth - 0.03f);

                CacheManager.Draw(@"Content\Textures\Resources\Dantiao\Avatar.png", basePos + new Vector2(15 + 10, 10 + 10), null, Color.White * Alpha, SpriteEffects.None, scale, depth - 0.04f);

                CacheManager.DrawZhsanAvatar(genLeft.Person.PictureIndex, 9999, "", new Rectangle(new Point(Convert.ToInt32(basePos.X + 15 + 10), Convert.ToInt32(basePos.Y + 10 + 10)), new Point(150, 150)), Color.White * Alpha, depth - 0.035f);

                CacheManager.DrawString(null, genLeft.Person.Name, basePos + new Vector2(15 + 10 + 10, 10 + 10 + 10), Color.Red * Alpha, 0f, Vector2.Zero, scale.X * 0.8f, SpriteEffects.None, depth - 0.045f);

                CacheManager.DrawString(null, "武力：" + genLeft.Force, basePos + new Vector2(25, 175), Color.DarkRed * Alpha, 0f, Vector2.Zero, scale.X * 0.8f, SpriteEffects.None, depth - 0.036f);

                CacheManager.Draw(@"Content\Textures\Resources\Dantiao\SwordBlackLeft.png", basePos + new Vector2(210, 25), null, Color.White * Alpha, SpriteEffects.None, scale, depth - 0.04f);

                CacheManager.Draw(@"Content\Textures\Resources\Dantiao\SwordBlackLeft.png", basePos + new Vector2(210, 55), null, Color.White * Alpha, SpriteEffects.None, scale, depth - 0.04f);

                CacheManager.Draw(@"Content\Textures\Resources\Dantiao\SwordCyanLeft.png", basePos + new Vector2(210, 25), new Rectangle(0, 0, Convert.ToInt32(288 * Convert.ToSingle(genLeft.Skill) / 100f), 25), Color.White * Alpha, SpriteEffects.None, scale, depth - 0.045f);

                CacheManager.Draw(@"Content\Textures\Resources\Dantiao\SwordRedLeft.png", basePos + new Vector2(210, 55), new Rectangle(0, 0, Convert.ToInt32(288 * Convert.ToSingle(genLeft.Life) / 100f), 25), Color.White * Alpha, SpriteEffects.None, scale, depth - 0.045f);



                CacheManager.Draw(@"Content\Textures\Resources\Dantiao\Avatar.png", basePos + new Vector2(1000 - 15 - 15 - 150, 620 - 20 - 15 - 150), null, Color.White * Alpha, SpriteEffects.None, scale, depth - 0.04f);

                CacheManager.DrawZhsanAvatar(genRight.Person.PictureIndex, 9999, "", new Rectangle(new Point(Convert.ToInt32(basePos.X + 1000 - 15 - 15 - 150), Convert.ToInt32(basePos.Y + 620 - 20 - 15 - 150)), new Point(150, 150)), Color.White * Alpha, depth - 0.035f);

                CacheManager.DrawString(null, genRight.Person.Name, basePos + new Vector2(1000 - 15 - 15 - 140, 620 - 20 - 15 - 140), Color.Red * Alpha, 0f, Vector2.Zero, scale.X * 0.8f, SpriteEffects.None, depth - 0.045f);

                CacheManager.DrawString(null, "武力：" + genRight.Force, basePos + new Vector2(1000 - 140, 620 - 218), Color.DarkRed * Alpha, 0f, Vector2.Zero, scale.X * 0.8f, SpriteEffects.None, depth - 0.036f);

                CacheManager.Draw(@"Content\Textures\Resources\Dantiao\SwordBlackRight.png", basePos + new Vector2(510, 520), null, Color.White * Alpha, SpriteEffects.None, scale, depth - 0.04f);

                CacheManager.Draw(@"Content\Textures\Resources\Dantiao\SwordBlackRight.png", basePos + new Vector2(510, 550), null, Color.White * Alpha, SpriteEffects.None, scale, depth - 0.04f);

                CacheManager.Draw(@"Content\Textures\Resources\Dantiao\SwordCyanRight.png", basePos + new Vector2(510 + Convert.ToInt32(288 * (1 - Convert.ToSingle(genRight.Skill) / 100f)), 520), new Rectangle(Convert.ToInt32(288 * (1 - Convert.ToSingle(genRight.Skill) / 100f)), 0, Convert.ToInt32(288 * Convert.ToSingle(genRight.Skill) / 100), 25), Color.White * Alpha, SpriteEffects.None, scale, depth - 0.045f);

                CacheManager.Draw(@"Content\Textures\Resources\Dantiao\SwordRedRight.png", basePos + new Vector2(510 + Convert.ToInt32(288 * (1 - Convert.ToSingle(genRight.Life) / 100f)), 550), new Rectangle(Convert.ToInt32(288 * (1 - Convert.ToSingle(genRight.Life) / 100f)), 0, Convert.ToInt32(288 * Convert.ToSingle(genRight.Life) / 100), 25), Color.White * Alpha, SpriteEffects.None, scale, depth - 0.045f);

                CacheManager.DrawString(null, "第 " + round + " 回合", basePos + new Vector2(550, 35), Color.Red * Alpha, 0f, Vector2.Zero, scale.X, SpriteEffects.None, depth - 0.036f);

                btnStory.Draw();

                if (btnStory.Selected)
                {
                    btnPagePre.Draw();

                    btnPageNext.Draw();

                    CacheManager.DrawString(null, "1/2 回合", basePos + new Vector2(35 + 138, 557), Color.Blue * Alpha, 0f, Vector2.Zero, scale.X * 0.7f, SpriteEffects.None, depth - 0.036f);
                }

                btnSpeed.Draw();

                //CacheManager.DrawString(null, "X" + Speed, basePos + new Vector2(750 + 50, 35), Color.Black * Alpha, 0f, Vector2.Zero, scale.X, SpriteEffects.None, depth - 0.036f);

                btnSpeedDown.Draw();

                btnSpeedUp.Draw();

                if (basePos.X - 128 + 30 <= genLeft.Position.X && genLeft.Position.X < basePos.X + 1000 - 70)
                {
                    if (basePos.X > genLeft.Position.X)
                    {
                        genLeft.LimiteWidth = Convert.ToInt32(128 - (basePos.X - genLeft.Position.X));

                        genLeft.LimiteOrder = true;
                    }
                    else if (genLeft.Position.X > basePos.X + 1000 - 128)
                    {
                        genLeft.LimiteWidth = Convert.ToInt32(1000 - (genLeft.Position.X - basePos.X));

                        genLeft.LimiteOrder = false;
                    }
                    else
                    {
                        genLeft.LimiteWidth = 0;
                    }

                    genLeft.Draw();
                }

                if (basePos.X - 128 + 30 <= genRight.Position.X && genRight.Position.X < basePos.X + 1000 - 65)
                {
                    if (basePos.X > genRight.Position.X)
                    {
                        genRight.LimiteWidth = Convert.ToInt32(128 - (basePos.X - genRight.Position.X));

                        genRight.LimiteOrder = true;
                    }
                    else if (genRight.Position.X > basePos.X + 1000 - 128)
                    {
                        genRight.LimiteWidth = Convert.ToInt32(1000 - (genRight.Position.X - basePos.X));

                        genRight.LimiteOrder = false;
                    }
                    else
                    {
                        genRight.LimiteWidth = 0;
                    }

                    genRight.Draw();
                }

                if (genLeft.SayTime > 0f || !genLeft.SayDisappear)
                {
                    CacheManager.Draw(@"Content\Textures\Resources\Dantiao\StarLeft.png", basePos + new Vector2(185, 82), null, Color.White * Alpha, SpriteEffects.None, scale, depth - 0.035f);

                    CacheManager.DrawString(null, genLeft.SayWords.WordsSubString(Convert.ToInt32((1 - genLeft.SayTime / genLeft.SayTimeTotal) * genLeft.SayWords.Length), 0).SplitLineString(12), basePos + new Vector2(185 + 45, 82 + 38), Color.Black * Alpha, 0f, Vector2.Zero, scale.X * 0.8f, SpriteEffects.None, depth - 0.036f);
                }

                if (genRight.SayTime > 0f || !genRight.SayDisappear)
                {
                    CacheManager.Draw(@"Content\Textures\Resources\Dantiao\StarRight.png", basePos + new Vector2(470, 405), null, Color.White * Alpha, SpriteEffects.None, scale, depth - 0.035f);

                    CacheManager.DrawString(null, genRight.SayWords.WordsSubString(Convert.ToInt32((1 - genRight.SayTime / genRight.SayTimeTotal) * genRight.SayWords.Length), 0).SplitLineString(12), basePos + new Vector2(470 + 35, 405 + 38), Color.Black * Alpha, 0f, Vector2.Zero, scale.X * 0.8f, SpriteEffects.None, depth - 0.036f);
                }

                if (String.IsNullOrEmpty(Title))
                {
                    
                }
                else
                {
                    CacheManager.DrawString(null, Title, basePos + new Vector2(350, 150), Color.Red * Alpha, 0f, Vector2.Zero, scale.X * 3f, SpriteEffects.None, depth - 0.036f);
                }

                if (ViewExit)
                {
                    string exitWords = "点击任意处以退出。";

                    CacheManager.DrawString(null, exitWords, basePos + new Vector2(300, 450), Color.Black * Alpha, 0f, Vector2.Zero, scale.X * 1.5f, SpriteEffects.None, depth - 0.036f);
                }

                CacheManager.Draw(@"Content\Textures\Resources\Dantiao\Ground.png", basePos, null, Color.White * Alpha, SpriteEffects.None, scale, depth - 0.09f);

            }
        }
    }

}
