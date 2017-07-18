using GameFreeText;
using GameGlobal;
using GameObjects;
using GameObjects.ArchitectureDetail;
using GameObjects.FactionDetail;
using GameObjects.SectionDetail;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PluginInterface;
using System;
using System.Collections.Generic;

namespace MarshalSectionDialogPlugin
{

    public class MarshalSectionDialog
    {
        private SectionAIDetail AIDetail;
        internal Texture2D AIDetailButtonDisplayTexture;
        internal Rectangle AIDetailButtonPosition;
        internal Texture2D AIDetailButtonSelectedTexture;
        internal Texture2D AIDetailButtonTexture;
        internal Texture2D ArchitectureListButtonDisplayTexture;
        internal Rectangle ArchitectureListButtonPosition;
        internal Texture2D ArchitectureListButtonSelectedTexture;
        internal Texture2D ArchitectureListButtonTexture;
        internal Point BackgroundSize;
        internal Texture2D BackgroundTexture;
        internal Texture2D CancelButtonDisabledTexture;
        internal Texture2D CancelButtonDisplayTexture;
        internal bool CancelButtonEnabled = true;
        internal Rectangle CancelButtonPosition;
        internal Texture2D CancelButtonSelectedTexture;
        internal Texture2D CancelButtonTexture;
        internal Point DisplayOffset;
        private Faction EditingFaction;
        private Section EditingSection;
        internal IGameFrame GameFramePlugin;
        private bool IsNew;
        private bool isShowing;
        internal List<LabelText> LabelTexts = new List<LabelText>();
        internal Texture2D OKButtonDisabledTexture;
        internal Texture2D OKButtonDisplayTexture;
        internal bool OKButtonEnabled;
        internal Rectangle OKButtonPosition;
        internal Texture2D OKButtonSelectedTexture;
        internal Texture2D OKButtonTexture;
        internal Texture2D OrientationButtonDisabledTexture;
        internal Texture2D OrientationButtonDisplayTexture;
        internal bool OrientationButtonEnabled = false;
        internal Rectangle OrientationButtonPosition;
        internal Texture2D OrientationButtonSelectedTexture;
        internal Texture2D OrientationButtonTexture;
        private Faction OrientationFaction;
        private Section OrientationSection;
        private State OrientationState;
        private Section OriginalSection;
        private Screen screen;
        private GameObjectList SectionArchitectureList;
        internal ITabList TabListPlugin;

        internal void Draw(SpriteBatch spriteBatch)
        {
            Rectangle? sourceRectangle = null;
            spriteBatch.Draw(this.BackgroundTexture, this.BackgroundDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
            foreach (LabelText text in this.LabelTexts)
            {
                text.Label.Draw(spriteBatch, 0.1999f);
                text.Text.Draw(spriteBatch, 0.1999f);
            }
            if (this.OKButtonEnabled)
            {
                sourceRectangle = null;
                spriteBatch.Draw(this.OKButtonDisplayTexture, this.OKButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.199f);
            }
            else
            {
                sourceRectangle = null;
                spriteBatch.Draw(this.OKButtonDisabledTexture, this.OKButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.199f);
            }
            if (this.CancelButtonEnabled)
            {
                sourceRectangle = null;
                spriteBatch.Draw(this.CancelButtonDisplayTexture, this.CancelButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.199f);
            }
            else
            {
                sourceRectangle = null;
                spriteBatch.Draw(this.CancelButtonDisabledTexture, this.CancelButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.199f);
            }
            sourceRectangle = null;
            spriteBatch.Draw(this.ArchitectureListButtonDisplayTexture, this.ArchitectureListButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.199f);
            sourceRectangle = null;
            spriteBatch.Draw(this.AIDetailButtonDisplayTexture, this.AIDetailButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.199f);
            if (this.OrientationButtonEnabled)
            {
                spriteBatch.Draw(this.OrientationButtonDisplayTexture, this.OrientationButtonDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.199f);
            }
            else
            {
                spriteBatch.Draw(this.OrientationButtonDisabledTexture, this.OrientationButtonDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.199f);
            }
        }

        internal void Initialize(Screen screen)
        {
            this.screen = screen;
        }

        private void OK()
        {
            if (this.IsNew)
            {
                foreach (Section section in this.EditingFaction.Sections)
                {
                    foreach (Architecture architecture in this.EditingSection.Architectures)
                    {
                        section.RemoveArchitecture(architecture);
                    }
                }
                foreach (Architecture architecture in this.EditingSection.Architectures)
                {
                    architecture.BelongedSection = this.EditingSection;
                }
                this.EditingFaction.AddSection(this.EditingSection);
                this.EditingFaction.Scenario.Sections.AddSectionWithEvent(this.EditingSection);
            }
            else
            {
                if (this.OriginalSection.AIDetail.AutoRun != this.EditingSection.AIDetail.AutoRun)
                {
                    foreach (Architecture architecture in this.EditingSection.Architectures)
                    {
                        foreach (Routeway routeway in architecture.Routeways.GetList())
                        {
                            if (!(routeway.Building && (routeway.LastActivePointIndex >= 0)))
                            {
                                architecture.Scenario.RemoveRouteway(routeway);
                            }
                        }
                    }
                }
                this.OriginalSection.AIDetail = this.EditingSection.AIDetail;
                this.OriginalSection.OrientationFaction = this.EditingSection.OrientationFaction;
                this.OriginalSection.OrientationSection = this.EditingSection.OrientationSection;
                this.OriginalSection.OrientationState = this.EditingSection.OrientationState;
                this.OriginalSection.OrientationArchitecture = this.EditingSection.OrientationArchitecture;
                GameObjectList list = this.OriginalSection.Architectures.GetList();
                foreach (Architecture architecture in list)
                {
                    this.OriginalSection.RemoveArchitecture(architecture);
                }
                foreach (Section section in this.EditingFaction.Sections)
                {
                    foreach (Architecture architecture in this.EditingSection.Architectures)
                    {
                        section.RemoveArchitecture(architecture);
                    }
                }
                foreach (Architecture architecture in this.EditingSection.Architectures)
                {
                    this.OriginalSection.AddArchitecture(architecture);
                }
                Section anotherSection = this.EditingFaction.GetAnotherSection(this.OriginalSection);
                if (anotherSection != null)
                {
                    foreach (Architecture architecture in list)
                    {
                        if (!this.OriginalSection.HasArchitecture(architecture))
                        {
                            anotherSection.AddArchitecture(architecture);
                        }
                    }
                }
            }
            foreach (Section section in this.EditingFaction.Sections.GetList())
            {
                if (section.ArchitectureCount > 0)
                {
                    section.RefreshSectionName();
                }
                else
                {
                    foreach (Section section3 in this.EditingFaction.Sections.GetList())
                    {
                        if ((section3 != section) && (section3.OrientationSection == section))
                        {
                            foreach (SectionAIDetail detail in this.EditingFaction.Scenario.GameCommonData.AllSectionAIDetails.SectionAIDetails.Values)
                            {
                                if (detail.OrientationKind == SectionOrientationKind.无)
                                {
                                    section3.AIDetail = detail;
                                    break;
                                }
                            }
                            section3.OrientationSection = null;
                        }
                    }
                    this.EditingFaction.RemoveSection(section);
                    this.EditingFaction.Scenario.Sections.Remove(section);
                }
            }
            this.IsShowing = false;
        }

        private void RefreshLabelTextsDisplay()
        {
            foreach (LabelText text in this.LabelTexts)
            {
                text.Text.Text = StaticMethods.GetPropertyValue(this.EditingSection, text.PropertyName).ToString();
            }
        }

        private void RefreshOKButton()
        {
            this.OKButtonEnabled = 
                (
                    ((this.EditingSection.ArchitectureCount > 0) && 
                        (
                            (this.IsNew || (this.EditingFaction.SectionCount > 1)) || 
                            (this.EditingSection.ArchitectureCount == this.EditingFaction.ArchitectureCount)
                        )
                    ) && (
                        ((this.EditingFaction.SectionCount <= 1) || (this.EditingSection.ArchitectureCount < this.EditingFaction.ArchitectureCount)) &&
                        (this.EditingSection.AIDetail != null)
                    )
                ) && (
                    (
                        (this.EditingSection.AIDetail.OrientationKind == SectionOrientationKind.无) || 
                        ((this.EditingSection.AIDetail.OrientationKind == SectionOrientationKind.军区) && (this.EditingSection.OrientationSection != null)) ||
                        ((this.EditingSection.AIDetail.OrientationKind == SectionOrientationKind.势力) && (this.EditingSection.OrientationFaction != null)) ||
                        ((this.EditingSection.AIDetail.OrientationKind == SectionOrientationKind.州域) && (this.EditingSection.OrientationState != null)) ||
                        ((this.EditingSection.AIDetail.OrientationKind == SectionOrientationKind.建筑) && (this.EditingSection.OrientationArchitecture != null))
                    )
                );
        }

        private void RefreshOrientationButton()
        {
            this.OrientationButtonEnabled = (this.EditingSection.AIDetail != null) && (this.EditingSection.AIDetail.OrientationKind != SectionOrientationKind.无);
        }

        private void screen_OnMouseLeftUp(Point position)
        {
            if (this.screen.PeekUndoneWork().Kind == UndoneWorkKind.Dialog)
            {
                if (StaticMethods.PointInRectangle(position, this.OKButtonDisplayPosition))
                {
                    if (this.OKButtonEnabled)
                    {
                        this.OK();
                    }
                }
                else if (StaticMethods.PointInRectangle(position, this.CancelButtonDisplayPosition))
                {
                    if (this.CancelButtonEnabled)
                    {
                        this.IsShowing = false;
                    }
                }
                else if (StaticMethods.PointInRectangle(position, this.ArchitectureListButtonDisplayPosition))
                {
                    this.ShowArchitectureListFrame();
                }
                else if (StaticMethods.PointInRectangle(position, this.AIDetailButtonDisplayPosition))
                {
                    this.ShowAIDetailFrame();
                }
                else if (StaticMethods.PointInRectangle(position, this.OrientationButtonDisplayPosition) && this.OrientationButtonEnabled)
                {
                    this.ShowOrientationFrame();
                }
            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            if (this.screen.PeekUndoneWork().Kind == UndoneWorkKind.Dialog)
            {
                if (StaticMethods.PointInRectangle(position, this.OKButtonDisplayPosition))
                {
                    if (this.OKButtonEnabled)
                    {
                        this.OKButtonDisplayTexture = this.OKButtonSelectedTexture;
                    }
                }
                else if (StaticMethods.PointInRectangle(position, this.CancelButtonDisplayPosition))
                {
                    if (this.CancelButtonEnabled)
                    {
                        this.CancelButtonDisplayTexture = this.CancelButtonSelectedTexture;
                    }
                }
                else if (StaticMethods.PointInRectangle(position, this.ArchitectureListButtonDisplayPosition))
                {
                    this.ArchitectureListButtonDisplayTexture = this.ArchitectureListButtonSelectedTexture;
                }
                else if (StaticMethods.PointInRectangle(position, this.AIDetailButtonDisplayPosition))
                {
                    this.AIDetailButtonDisplayTexture = this.AIDetailButtonSelectedTexture;
                }
                else if (StaticMethods.PointInRectangle(position, this.OrientationButtonDisplayPosition))
                {
                    if (this.OrientationButtonEnabled)
                    {
                        this.OrientationButtonDisplayTexture = this.OrientationButtonSelectedTexture;
                    }
                }
                else
                {
                    this.ArchitectureListButtonDisplayTexture = this.ArchitectureListButtonTexture;
                    this.AIDetailButtonDisplayTexture = this.AIDetailButtonTexture;
                    if (this.OKButtonEnabled)
                    {
                        this.OKButtonDisplayTexture = this.OKButtonTexture;
                    }
                    if (this.CancelButtonEnabled)
                    {
                        this.CancelButtonDisplayTexture = this.CancelButtonTexture;
                    }
                    if (this.OrientationButtonEnabled)
                    {
                        this.OrientationButtonDisplayTexture = this.OrientationButtonTexture;
                    }
                }
            }
        }

        private void screen_OnMouseRightDown(Point position)
        {
            if (this.screen.PeekUndoneWork().Kind == UndoneWorkKind.Dialog)
            {
                this.IsShowing = false;
            }
        }

        internal void SetDisplayOffset(ShowPosition showPosition)
        {
            Rectangle rectDes = new Rectangle(0, 0, this.screen.viewportSize.X, this.screen.viewportSize.Y);
            Rectangle rect = new Rectangle(0, 0, this.BackgroundSize.X, this.BackgroundSize.Y);
            switch (showPosition)
            {
                case ShowPosition.Center:
                    rect = StaticMethods.GetCenterRectangle(rectDes, rect);
                    break;

                case ShowPosition.Top:
                    rect = StaticMethods.GetTopRectangle(rectDes, rect);
                    break;

                case ShowPosition.Left:
                    rect = StaticMethods.GetLeftRectangle(rectDes, rect);
                    break;

                case ShowPosition.Right:
                    rect = StaticMethods.GetRightRectangle(rectDes, rect);
                    break;

                case ShowPosition.Bottom:
                    rect = StaticMethods.GetBottomRectangle(rectDes, rect);
                    break;

                case ShowPosition.TopLeft:
                    rect = StaticMethods.GetTopLeftRectangle(rectDes, rect);
                    break;

                case ShowPosition.TopRight:
                    rect = StaticMethods.GetTopRightRectangle(rectDes, rect);
                    break;

                case ShowPosition.BottomLeft:
                    rect = StaticMethods.GetBottomLeftRectangle(rectDes, rect);
                    break;

                case ShowPosition.BottomRight:
                    rect = StaticMethods.GetBottomRightRectangle(rectDes, rect);
                    break;
            }
            this.DisplayOffset = new Point(rect.X, rect.Y);
            foreach (LabelText text in this.LabelTexts)
            {
                text.Label.DisplayOffset = this.DisplayOffset;
                text.Text.DisplayOffset = this.DisplayOffset;
            }
        }

        internal void SetFaction(Faction faction)
        {
            this.EditingFaction = faction;
        }

        internal void SetSection(Section section)
        {
            this.OriginalSection = section;
            this.EditingSection = new Section();
            this.EditingSection.Scenario = this.EditingFaction.Scenario;
            this.EditingSection.ID = this.EditingSection.Scenario.Sections.GetFreeGameObjectID();
            this.EditingSection.BelongedFaction = this.EditingFaction;
            if (section != null)
            {
                this.IsNew = false;
                foreach (Architecture architecture in section.Architectures)
                {
                    this.EditingSection.Architectures.Add(architecture);
                }
                this.EditingSection.Name = section.Name;
                this.EditingSection.AIDetail = section.AIDetail;
                this.EditingSection.OrientationFaction = section.OrientationFaction;
                this.EditingSection.OrientationSection = section.OrientationSection;
                this.EditingSection.OrientationState = section.OrientationState;
                this.EditingSection.OrientationArchitecture = section.OrientationArchitecture;
                this.RefreshOKButton();
                this.RefreshOrientationButton();
                this.RefreshLabelTextsDisplay();
            }
            else
            {
                this.IsNew = true;
                this.RefreshLabelTextsDisplay();
            }
        }

        private void ShowAIDetailFrame()
        {
            this.TabListPlugin.InitialValues(this.EditingFaction.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailList(), this.EditingSection.AIDetail, this.screen.MouseState.ScrollWheelValue, "");
            this.TabListPlugin.SetListKindByName("SectionAIDetail", true, false);
            this.TabListPlugin.SetSelectedTab("");
            this.GameFramePlugin.Kind = FrameKind.Section;
            this.GameFramePlugin.Function = FrameFunction.GetSection;
            this.GameFramePlugin.SetFrameContent(this.TabListPlugin.TabList, this.screen.viewportSizeFull);
            this.GameFramePlugin.OKButtonEnabled = false;
            this.GameFramePlugin.CancelButtonEnabled = true;
            this.GameFramePlugin.SetOKFunction(delegate {
                this.EditingSection.AIDetail = this.TabListPlugin.SelectedItem as SectionAIDetail;
                switch (this.EditingSection.AIDetail.OrientationKind)
                {
                    case SectionOrientationKind.军区:
                    {
                        SectionList otherSections = this.EditingFaction.GetOtherSections(this.OriginalSection);
                        if (otherSections.Count == 1)
                        {
                            this.EditingSection.OrientationSection = otherSections[0] as Section;
                        }
                        break;
                    }
                    case SectionOrientationKind.势力:
                    {
                        GameObjectList diplomaticRelationListByFactionID = this.EditingFaction.Scenario.DiplomaticRelations.GetDiplomaticRelationListByFactionID(this.EditingFaction.ID);
                        if (diplomaticRelationListByFactionID.Count == 1)
                        {
                            this.EditingSection.OrientationFaction = (diplomaticRelationListByFactionID[0] as DiplomaticRelation).GetDiplomaticFaction(this.EditingFaction.ID);
                        }
                        break;
                    }
                    case SectionOrientationKind.州域:
                    {
                        StateList states = this.EditingFaction.Scenario.States;
                        if (states.Count == 1)
                        {
                            this.EditingSection.OrientationState = states[0] as State;
                        }
                        break;
                    }
                    case SectionOrientationKind.建筑:
                    {
                        ArchitectureList allArch = this.EditingFaction.Scenario.Architectures;
                        ArchitectureList targetArch = new ArchitectureList();
                        foreach (Architecture a in allArch)
                        {
                            if (a.BelongedFaction != this.EditingFaction)
                            {
                                targetArch.Add(a);
                            }
                        }
                        if (targetArch.Count == 1)
                        {
                            this.EditingSection.OrientationArchitecture = targetArch[0] as Architecture;
                        }
                        break;
                    }
                }
                this.RefreshOKButton();
                if (this.IsNew)
                {
                    this.EditingSection.RefreshSectionName();
                }
                this.RefreshOrientationButton();
                this.RefreshLabelTextsDisplay();
            });
            this.GameFramePlugin.IsShowing = true;
        }

        private void ShowArchitectureListFrame()
        {
            this.TabListPlugin.InitialValues(this.EditingFaction.Architectures, this.EditingSection.Architectures, this.screen.MouseState.ScrollWheelValue, "");
            this.TabListPlugin.SetListKindByName("Architecture", true, true);
            this.TabListPlugin.SetSelectedTab("");
            this.GameFramePlugin.Kind = FrameKind.Architecture;
            this.GameFramePlugin.Function = FrameFunction.GetFacilityToDemolish;
            this.GameFramePlugin.SetFrameContent(this.TabListPlugin.TabList, this.screen.viewportSizeFull);
            this.GameFramePlugin.OKButtonEnabled = false;
            this.GameFramePlugin.CancelButtonEnabled = true;
            this.GameFramePlugin.SetOKFunction(delegate {
                this.SectionArchitectureList = this.TabListPlugin.SelectedItemList as GameObjectList;
                this.EditingSection.Architectures.Clear();
                foreach (Architecture architecture in this.SectionArchitectureList)
                {
                    this.EditingSection.Architectures.Add(architecture);
                }
                this.EditingSection.RefreshSectionName();
                this.RefreshOKButton();
                this.RefreshLabelTextsDisplay();
            });
            this.GameFramePlugin.IsShowing = true;
        }

        private void ShowOrientationFrame()
        {
            if (this.EditingFaction == null) return;

            GameDelegates.VoidFunction function = null;
            GameDelegates.VoidFunction function2 = null;
            GameDelegates.VoidFunction function3 = null;
            switch (this.EditingSection.AIDetail.OrientationKind)
            {
                case SectionOrientationKind.军区:
                    this.TabListPlugin.InitialValues(this.EditingFaction.GetOtherSections(this.OriginalSection), null, this.screen.MouseState.ScrollWheelValue, "");
                    this.TabListPlugin.SetListKindByName("Section", true, false);
                    this.TabListPlugin.SetSelectedTab("");
                    this.GameFramePlugin.Kind = FrameKind.TerrainDetail;
                    this.GameFramePlugin.Function = FrameFunction.Transport;
                    this.GameFramePlugin.SetFrameContent(this.TabListPlugin.TabList, this.screen.viewportSizeFull);
                    this.GameFramePlugin.OKButtonEnabled = false;
                    this.GameFramePlugin.CancelButtonEnabled = true;
                    if (function == null)
                    {
                        function = delegate {
                            this.EditingSection.OrientationFaction = null;
                            this.EditingSection.OrientationState = null;
                            this.EditingSection.OrientationArchitecture = null;
                            this.EditingSection.OrientationSection = this.TabListPlugin.SelectedItem as Section;
                            this.RefreshOKButton();
                            this.RefreshLabelTextsDisplay();
                        };
                    }
                    this.GameFramePlugin.SetOKFunction(function);
                    break;

                case SectionOrientationKind.势力:
                    this.TabListPlugin.InitialValues(this.EditingFaction.Scenario.DiplomaticRelations.GetDiplomaticRelationDisplayListByFactionID(this.EditingFaction.ID).GetList(), null, this.screen.MouseState.ScrollWheelValue, "");
                    this.TabListPlugin.SetListKindByName("DiplomaticRelation", true, false);
                    this.TabListPlugin.SetSelectedTab("");
                    this.GameFramePlugin.Kind = FrameKind.CastTargetKind;
                    this.GameFramePlugin.Function = FrameFunction.GetSectionToDemolish;
                    this.GameFramePlugin.SetFrameContent(this.TabListPlugin.TabList, this.screen.viewportSizeFull);
                    this.GameFramePlugin.OKButtonEnabled = false;
                    this.GameFramePlugin.CancelButtonEnabled = true;
                    if (function2 == null)
                    {
                        function2 = delegate {
                            this.EditingSection.OrientationSection = null;
                            this.EditingSection.OrientationState = null;
                            this.EditingSection.OrientationArchitecture = null;
                            DiplomaticRelationDisplay selectedItem = this.TabListPlugin.SelectedItem as DiplomaticRelationDisplay;
                            if (selectedItem.LinkedFaction1 == this.EditingFaction)
                            {
                                this.EditingSection.OrientationFaction = selectedItem.LinkedFaction2;
                            }
                            else if (selectedItem.LinkedFaction2 == this.EditingFaction)
                            {
                                this.EditingSection.OrientationFaction = selectedItem.LinkedFaction1;
                            }
                            this.RefreshOKButton();
                            this.RefreshLabelTextsDisplay();
                        };
                    }
                    this.GameFramePlugin.SetOKFunction(function2);
                    break;

                case SectionOrientationKind.州域:
                    this.TabListPlugin.InitialValues(this.EditingFaction.Scenario.States.GetList(), null, this.screen.MouseState.ScrollWheelValue, "");
                    this.TabListPlugin.SetListKindByName("State", true, false);
                    this.TabListPlugin.SetSelectedTab("");
                    this.GameFramePlugin.Kind = FrameKind.SectionAIDetail;
                    this.GameFramePlugin.Function = FrameFunction.GetFaction;
                    this.GameFramePlugin.SetFrameContent(this.TabListPlugin.TabList, this.screen.viewportSizeFull);
                    this.GameFramePlugin.OKButtonEnabled = false;
                    this.GameFramePlugin.CancelButtonEnabled = true;
                    if (function3 == null)
                    {
                        function3 = delegate {
                            this.EditingSection.OrientationFaction = null;
                            this.EditingSection.OrientationSection = null;
                            this.EditingSection.OrientationState = this.TabListPlugin.SelectedItem as State;
                            this.EditingSection.OrientationArchitecture = null;
                            this.RefreshOKButton();
                            this.RefreshLabelTextsDisplay();
                        };
                    }
                    this.GameFramePlugin.SetOKFunction(function3);
                    break;

                case SectionOrientationKind.建筑:
                    ArchitectureList allArch = this.EditingFaction.Scenario.Architectures;
                    ArchitectureList targetArch = new ArchitectureList();
                    foreach (Architecture a in allArch)
                    {
                        if (a.BelongedFaction != this.EditingFaction)
                        {
                            targetArch.Add(a);
                        }
                    }
                    this.TabListPlugin.InitialValues(targetArch, null, this.screen.MouseState.ScrollWheelValue, "");
                    this.TabListPlugin.SetListKindByName("Architecture", true, false);
                    this.TabListPlugin.SetSelectedTab("");
                    this.GameFramePlugin.Kind = FrameKind.Architecture;
                    this.GameFramePlugin.Function = FrameFunction.GetFaction;
                    this.GameFramePlugin.SetFrameContent(this.TabListPlugin.TabList, this.screen.viewportSizeFull);
                    this.GameFramePlugin.OKButtonEnabled = false;
                    this.GameFramePlugin.CancelButtonEnabled = true;
                    if (function3 == null)
                    {
                        function3 = delegate
                        {
                            this.EditingSection.OrientationFaction = null;
                            this.EditingSection.OrientationSection = null;
                            this.EditingSection.OrientationState = null;
                            this.EditingSection.OrientationArchitecture = this.TabListPlugin.SelectedItem as Architecture;
                            this.RefreshOKButton();
                            this.RefreshLabelTextsDisplay();
                        };
                    }
                    this.GameFramePlugin.SetOKFunction(function3);
                    break;
            }
            this.GameFramePlugin.IsShowing = true;
        }

        internal void Update()
        {
        }

        private Rectangle AIDetailButtonDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + this.AIDetailButtonPosition.X, this.DisplayOffset.Y + this.AIDetailButtonPosition.Y, this.AIDetailButtonPosition.Width, this.AIDetailButtonPosition.Height);
            }
        }

        private Rectangle ArchitectureListButtonDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + this.ArchitectureListButtonPosition.X, this.DisplayOffset.Y + this.ArchitectureListButtonPosition.Y, this.ArchitectureListButtonPosition.Width, this.ArchitectureListButtonPosition.Height);
            }
        }

        private Rectangle BackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X, this.DisplayOffset.Y, this.BackgroundSize.X, this.BackgroundSize.Y);
            }
        }

        private Rectangle CancelButtonDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + this.CancelButtonPosition.X, this.DisplayOffset.Y + this.CancelButtonPosition.Y, this.CancelButtonPosition.Width, this.CancelButtonPosition.Height);
            }
        }

        public bool IsShowing
        {
            get
            {
                return this.isShowing;
            }
            set
            {
                this.isShowing = value;
                if (value)
                {
                    this.screen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Dialog, UndoneWorkSubKind.None));
                    this.screen.OnMouseLeftUp += new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                    this.screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
                    this.screen.OnMouseRightDown += new Screen.MouseRightDown(this.screen_OnMouseRightDown);
                }
                else
                {
                    if (this.screen.PopUndoneWork().Kind != UndoneWorkKind.Dialog)
                    {
                        throw new Exception("The UndoneWork is not a TransportDialog.");
                    }
                    this.screen.OnMouseLeftUp -= new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                    this.screen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                    this.screen.OnMouseRightDown -= new Screen.MouseRightDown(this.screen_OnMouseRightDown);
                }
            }
        }

        private Rectangle OKButtonDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + this.OKButtonPosition.X, this.DisplayOffset.Y + this.OKButtonPosition.Y, this.OKButtonPosition.Width, this.OKButtonPosition.Height);
            }
        }

        private Rectangle OrientationButtonDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + this.OrientationButtonPosition.X, this.DisplayOffset.Y + this.OrientationButtonPosition.Y, this.OrientationButtonPosition.Width, this.OrientationButtonPosition.Height);
            }
        }
    }
}

