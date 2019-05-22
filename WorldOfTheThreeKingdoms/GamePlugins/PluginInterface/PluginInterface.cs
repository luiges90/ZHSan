using System;
using System.Collections.Generic;
using System.Linq;
using GameFreeText;
using GameGlobal;
using GameObjects;
using PluginInterface.BaseInterface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PersonPortraitPlugin;

namespace PluginInterface
{
    public interface IAirView : IBasePlugin, IPluginXML, IPluginGraphics, IScreenDisableRects
    {
        void ReloadAirView();
        void ReloadAirView(string dituwenjianming);
        //void ReloadArchitectureView();
        //void ReloadTroopView();
        void ResetFramePosition(Point viewportSize, int leftEdge, int topEdge, Point totalMapSize);
        void ResetFrameSize(Point viewportSize, Point totalMapSize);
        void ResetMapPosition(Screen screen);
        void SetMapPosition(ShowPosition showPosition);
        void SetScreen(Screen screen);

        bool IsMapShowing { get; }
        Rectangle MapPosition { get; }
        object ToolInstance { get; }
    }

    public interface IArchitectureDetail : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void SetArchitecture(object architecture);
        void SetPosition(ShowPosition showPosition);
        void SetScreen(Screen screen);

        bool IsShowing { get; set; }
    }

    public interface IArchitectureSurvey : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void SetArchitecture(object architecture, Point position);
        void SetFaction(object faction);
        void SetTopLeftPoint(int Left, int Top);
        void Gengxin();
        bool Showing { get; set; }
    }

    public interface IConfirmationDialog : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void AddNoFunction(GameDelegates.VoidFunction noFunction);
        void AddYesFunction(GameDelegates.VoidFunction yesFunction);
        void ClearFunctions();
        void SetDescriptionText(string Text);
        void SetPersonTextDialog(Itupianwenzi iPersonTextDialog);
        void SetPosition(ShowPosition showPosition);
        void SetScreen(Screen screen);
        void SetSimpleTextDialog(ISimpleTextDialog iSimpleTextDialog);

        bool IsShowing { get; set; }
        DialogResult Result { get; set; }
    }

    public interface IConmentText : IBasePlugin, IPluginXML, IPluginGraphics
    {
        string BuildFirstText(string text, bool decoration);
        string BuildSecondText(string text, bool decoration);
        string BuildThirdText(string text, bool decoration);
        void SetView(int width, int height);
    }

    public interface ICreateTroop : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void SetArchitecture(object architecture);
        void SetCreateFunction(GameDelegates.VoidFunction function);
        void SetGameFrame(IGameFrame iGameFrame);
        void SetNumberInputer(INumberInputer iNumberInputer);
        void SetPosition(ShowPosition showPosition);
        void SetScreen(Screen screen);
        void SetShellMilitaryKind(object kind);
        void SetTabList(ITabList iTabList);

        object CreatingArchitecture { get; }
        int CreatingFood { get; }
        int Creatingzijin { get; }
        object CreatingLeader { get; }
        object CreatingMilitary { get; }
        object CreatingPersons { get; }
        bool IsShowing { get; set; }
    }


    public interface IDateRunner : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void DateGo();
        void DateStop();
        void Pause();
        void Reset();
        void Run();
        void RunDays(int Days);
        void SetGameDate(object gameDate);
        void SetScreen(Screen screen);
        void Stop();

        object ToolInstance { get; }
    }
    public interface IFactionTechniques : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void SetArchitecture(object architecture);
        void SetFaction(object faction, bool control);
        void SetPosition(ShowPosition showPosition);
        void SetScreen(Screen screen);

        bool IsShowing { get; set; }
    }

    public interface IGameContextMenu : IBasePlugin, IPluginXML, IPluginGraphics, GameObjects.IScenarioAwarePlugin
    {
        void Prepare(int X, int Y, Point viewportSize);
        void SetCurrentGameObject(object gameObject);
        void SetIHelp(IHelp iHelp);
        void SetMenuKindByID(int ID);
        void SetMenuKindByName(string Name);
        void SetScreen(Screen screen);
        void ShezhiBianduiLiebiaoXinxi(bool Xianshi, Rectangle Weizhi);
        int CurrentParamID { get; }
        bool IsShowing { get; set; }
        ContextMenuKind Kind { get; }
        ContextMenuResult Result { get; }
    }

    public interface IGameFrame : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void Cancel();
        void OK();
        void SetCancelFunction(GameDelegates.VoidFunction function);
        void SetFrameContent(object content, Point viewportSize);
        //void SetFrameyoucelanContent(object content, Point viewportSize);
        void SetOKFunction(GameDelegates.VoidFunction function);
        void SetScreen(Screen screen);

        //bool shiyoucelan { get; set; }
        bool CancelButtonEnabled { get; set; }
        FrameFunction Function { get; set; }
        bool IsShowing { get; set; }
        FrameKind Kind { get; set; }
        bool OKButtonEnabled { get; set; }
        FrameResult Result { get; }
        int LeftEdge { get; }
        int RightEdge { get; }
        int TopEdge { get; }
        int BottomEdge { get; }
    }

    public interface IGameRecord : IBasePlugin, IPluginXML, IPluginGraphics, IScreenDisableRects
    {
        void AddBranch(object gameObject, string branchName, Point position);
        void Clear();
        void ResetRecordShowPosition();
        void SetScreen(Screen screen);

        bool IsRecordShowing { get; }
        object ToolInstance { get; }
    }

    public interface IGameSystem : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void SetOptionDialog(IOptionDialog iOptionDialog);
        void SetScreen(Screen screen);
        void ShowOptionDialog(ShowPosition showPosition);

        object ToolInstance { get; }
    }


    public interface IHelp : IBasePlugin, IPluginXML, IPluginGraphics, IScenarioAwarePlugin
    {
        void DrawButton(float depth);
        void SetButtonDisplayOffset(Point offset);
        void SetButtonSize(Point size);
        bool SetCurrentKey(string key);
        void SetMapPosition(ShowPosition showPosition);
        void SetScreen(Screen screen);

        Rectangle ButtonDisplayPosition { get; }
        bool IsButtonShowing { get; set; }
        bool IsShowing { get; set; }
        FreeRichText RichText { get; }
    }


    public interface IMapLayer : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void SetScreen(Screen screen);

        object ToolInstance { get; }
    }



    public interface IMapViewSelector : IBasePlugin, IPluginXML, IPluginGraphics, IScreenDisableRects
    {
        void SetGameFrame(IGameFrame iGameFrame);
        void SetGameObjectList(object gameObjectList);
        void SetMapPosition(ShowPosition showPosition);
        void SetMultiSelecting(bool multiSelecting);
        void SetOKFunction(GameDelegates.VoidFunction function);
        void SetScreen(Screen screen);
        void SetTabList(ITabList iTabList);

        bool IsShowing { get; set; }
        MapViewSelectorKind Kind { get; set; }
    }

    public interface IMarshalSectionDialog : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void SetFaction(object faction);
        void SetGameFrame(IGameFrame iGameFrame);
        void SetMapPosition(ShowPosition showPosition);
        void SetScreen(Screen screen);
        void SetSection(object section);
        void SetTabList(ITabList iTabList);

        bool IsShowing { get; set; }
    }




    public interface INumberInputer : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void SetDepthOffset(float offset);
        void SetEnterFunction(GameDelegates.VoidFunction function);
        void SetMapPosition(ShowPosition showPosition);
        void SetMax(int max);
        void SetScreen(Screen screen);
        void SetUnit(string unit);
        void SetScale(int scale);
        bool IsShowing { get; set; }
        int Number { get; }
    }



    public interface IOptionDialog : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void AddOption(string Text, object obj, GameDelegates.VoidFunction optionFunction);
        void Clear();
        void EndAddOptions();
        void HideOptionDialog();
        void SetReturnObjectFunction(GameDelegates.ObjectFunction returnobjectFunction);
        void SetScreen(Screen screen);
        void SetStyle(string style);
        void SetTitle(string title);
        void ShowOptionDialog(ShowPosition showPosition);
    }



    public interface IPersonBubble : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void AddPerson(object person, Point position, string branchName);
        void AddPersonText(object person, Point position, string text);
        void AddPerson(object person, Point position, Enum kind, string fallback);
        void SetScreen(Screen screen);

        bool IsShowing { get; set; }
    }


    public interface IPersonDetail : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void SetPerson(object person);
        void SetPosition(ShowPosition showPosition);
        void SetScreen(Screen screen);

        bool IsShowing { get; set; }
    }




    public interface IPersonPortrait : IBasePlugin
    {
        //bool HasPortrait(float id);
        //Image GetImage(float id);
        //Texture2D GetPortrait(float id);
        //Texture2D GetSmallPortrait(float id);
        //Texture2D GetTroopPortrait(float id);
        //Texture2D GetFullPortrait(float id);
        void SetGraphicsDevice();
    }


    public interface Itupianwenzi : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void Close(Screen screen);
        void SetCloseFunction(GameDelegates.VoidFunction closeFunction);
        void SetConfirmationDialog(IConfirmationDialog iConfirmationDialog, GameDelegates.VoidFunction yesFunction, GameDelegates.VoidFunction noFunction);
        void SetContextMenu(IGameContextMenu iContextMenu);
        void SetGameObjectBranch(object person, object gameObject, string branchName);
        void SetGameObjectBranch(object person, object gameObject, string branchName, string tupian, string shengyin, string TryToShowString = "");
        void SetGameObjectBranch(object person, object gameObject, Enum kind, string branchName);
        void SetGameObjectBranch(object person, object gameObject, Enum kind, string branchName, string tupian, string shengyin);

        void SetPosition(ShowPosition showPosition, Screen screen);
        void SetScreen(Screen screen);

        bool IsShowing { get; set; }
        FreeRichText RichText { get; }
    }


    public interface IRoutewayEditor : IBasePlugin, IPluginXML, IPluginGraphics, IScreenDisableRects
    {
        void SetRouteway(object routeway);
        void SetScreen(Screen screen);

        bool IsShowing { get; set; }
    }



    public interface IScreenBlind : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void SetScreen(Screen screen);
    }



    public interface ISideBar : IBasePlugin, IPluginXML
    {
        void SetGraphicsDevice(object graphicsDevice);

        int Align { get; set; }
        object BackGroundTexture { get; }
        object Components { get; }
        int Width { get; set; }
    }



    public interface ISimpleTextDialog : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void SetBranch(string branchName);
        void SetConfirmationDialog(IConfirmationDialog iConfirmationDialog);
        void SetGameObjectBranch(object gameObject, string branchName);
        void SetPosition(ShowPosition showPosition);
        void SetScreen(Screen screen);

        bool IsShowing { get; set; }
        FreeRichText RichText { get; }
    }




    public interface ITabList : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void InitialValues(object gameObjectList, object selectedObjectList, int scrollValue, string title);
        void RefreshEditable();
        void SetArchitectureDetailDialog(IArchitectureDetail iArchitectureDetail);
        void SetFactionTechniquesDialog(IFactionTechniques iFactionTechniques);
        void SetGameFrame(IGameFrame iGameFrame);
        void SetListKindByName(string Name, bool ShowCheckBox, bool MultiSelecting);
        void SetMapViewSelector(IMapViewSelector iMapViewSelector);
        void SetPersonDetailDialog(IPersonDetail iPersonDetail);
        void SetScreen(Screen screen);
        void SetSelectedItemMaxCount(int max);
        void SetSelectedTab(string tabName);
        void SetTreasureDetailDialog(ITreasureDetail iTreasureDetail);
        void SetTroopDetailDialog(ITroopDetail iTroopDetail);
        
        bool IsShowing { get; set; }
        object SelectedItem { get; }
        object SelectedItemList { get; }
        object TabList { get; }
    }


    public interface Iyoucelan : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void InitialValues(object gameObjectList, object selectedObjectList, int scrollValue, string title);
        void RefreshEditable();
        void SetArchitectureDetailDialog(IArchitectureDetail iArchitectureDetail);
        void SetFactionTechniquesDialog(IFactionTechniques iFactionTechniques);
        void SetGameFrame(IGameFrame iGameFrame);
        void SetListKindByName(string Name, bool ShowCheckBox, bool MultiSelecting);
        void SetMapViewSelector(IMapViewSelector iMapViewSelector);
        void SetPersonDetailDialog(IPersonDetail iPersonDetail);
        void SetScreen(Screen screen);
        void SetSelectedItemMaxCount(int max);
        void SetSelectedTab(string tabName);
        void SetTreasureDetailDialog(ITreasureDetail iTreasureDetail);
        void SetTroopDetailDialog(ITroopDetail iTroopDetail);
        void SetyoucelanContent(Point viewportSize);
        bool IsShowing { get; set; }
        object SelectedItem { get; }
        object SelectedItemList { get; }
        object TabList { get; }
        Rectangle FrameRectangle { get; }

        FrameFunction Function { get; set; }

        FrameKind Kind { get; set; }

    }


    public interface IBianduiLiebiao : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void InitialValues(object gameObjectList, object selectedObjectList, int scrollValue, string title);
        void RefreshEditable();
        void SetArchitectureDetailDialog(IArchitectureDetail iArchitectureDetail);
        void SetFactionTechniquesDialog(IFactionTechniques iFactionTechniques);
        void SetGameFrame(IGameFrame iGameFrame);
        void SetListKindByName(string Name, bool ShowCheckBox, bool MultiSelecting);
        void SetMapViewSelector(IMapViewSelector iMapViewSelector);
        void SetPersonDetailDialog(IPersonDetail iPersonDetail);
        void SetScreen(Screen screen);
        void SetSelectedItemMaxCount(int max);
        void SetSelectedTab(string tabName);
        void SetTreasureDetailDialog(ITreasureDetail iTreasureDetail);
        void SetTroopDetailDialog(ITroopDetail iTroopDetail);
        void SetyoucelanContent(Point viewportSize);
        void ShezhiBingyi(int bingyi);
        bool IsShowing { get; set; }
        object SelectedItem { get; }
        object SelectedItemList { get; }
        object TabList { get; }
        Rectangle Weizhi { get; }

        FrameFunction Function { get; set; }

        FrameKind Kind { get; set; }

    }

    public interface IToolBar : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void AddTool(object tool);
        void RemoveTool(object tool);
        void SetContextMenuPlugin(IGameContextMenu contextMenuPlugin);
        void SetRealViewportSize(Point realViewportSize);
        void SetScreen(Screen screen);
        void Draw(GameTime gameTime);
        bool DrawTools { get; set; }
        bool Enabled { get; set; }
        int Height { get; set; }
        bool IsShowing { get; set; }
    }




    public interface ITransportDialog : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void SetGameFrame(IGameFrame iGameFrame);
        void SetKind(TransportKind kind);
        void SetMapPosition(ShowPosition showPosition);
        void SetNumberInputer(INumberInputer iNumberInputer);
        void SetScreen(Screen screen);
        void SetSourceArchiecture(object architecture);
        void SetTabList(ITabList iTabList);
        void SetGameRecord(IGameRecord iGameRecord);
        bool IsShowing { get; set; }
    }




    public interface ITreasureDetail : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void SetPosition(ShowPosition showPosition);
        void SetScreen(Screen screen);
        void SetTreasure(object treasure);

        bool IsShowing { get; set; }
    }


    public interface ITroopDetail : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void SetPosition(ShowPosition showPosition);
        void SetScreen(Screen screen);
        void SetTroop(object troop);

        bool IsShowing { get; set; }
    }


    public interface ITroopSurvey : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void SetFaction(object faction);
        void SetTopLeftPoint(int Left, int Top);
        void SetTroop(object troop);

        bool Showing { get; set; }
    }



    public interface ITroopTitle : IBasePlugin, IPluginXML, IPluginGraphics
    {
        void DrawTroop(object troop, bool playerControlling);

        bool IsShowing { get; set; }
    }

 

 


 








 


 




 

 



 


 


 


 

}
