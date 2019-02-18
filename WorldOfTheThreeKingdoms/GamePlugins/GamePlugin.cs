using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameObjects;
using Microsoft.Xna.Framework;
using PluginInterface;
using PluginServices;
using PluginInterface.BaseInterface;
using PersonDetailPlugin;
using GameManager;
using WorldOfTheThreeKingdoms.GameScreens;

namespace WorldOfTheThreeKingdoms.GameLogic

{
    public class GamePlugin
    {
        public IAirView AirViewPlugin = null;
        public IArchitectureDetail ArchitectureDetailPlugin = null;
        public IArchitectureSurvey ArchitectureSurveyPlugin = null;
        public IConfirmationDialog ConfirmationDialogPlugin = null;
        public IConmentText ConmentTextPlugin = null;
        public IGameContextMenu ContextMenuPlugin = null;
        public ICreateTroop CreateTroopPlugin = null;
        public IDateRunner DateRunnerPlugin = null;
        public IFactionTechniques FactionTechniquesPlugin = null;
        public IGameFrame GameFramePlugin = null;
        public IGameRecord GameRecordPlugin = null;
        public IGameSystem GameSystemPlugin = null;
        public IHelp HelpPlugin = null;
        public IMapLayer MapLayerPlugin = null;
        public IMapViewSelector MapViewSelectorPlugin = null;
        public IMarshalSectionDialog MarshalSectionDialogPlugin = null;
        public INumberInputer NumberInputerPlugin = null;
        public IOptionDialog OptionDialogPlugin = null;
        public IPersonBubble PersonBubblePlugin = null;
        public IPersonDetail PersonDetailPlugin = null;
        public IPersonPortrait PersonPortraitPlugin = null;

        public Itupianwenzi EventDisplayPlugin = null;

        public IRoutewayEditor RoutewayEditorPlugin = null;
        public IScreenBlind ScreenBlindPlugin = null;
        public ISimpleTextDialog SimpleTextDialogPlugin = null;
        public ITabList TabListPlugin = null;
        public Iyoucelan RightSidePanelPlugin = null;
        public IBianduiLiebiao TroopArrangement = null;
        public IToolBar ToolBarPlugin = null;
        public ITransportDialog TransportDialogPlugin = null;
        public ITreasureDetail TreasureDetailPlugin = null;
        public ITroopDetail TroopDetailPlugin = null;
        public ITroopSurvey TroopSurveyPlugin = null;
        public ITroopTitle TroopTitlePlugin = null;

        public void InitializePlugins(MainGameScreen screen)
        {
            IBasePlugin plugin = new HelpPlugin.HelpPlugin(); //  Plugin.Plugins.AvailablePlugins.Find("HelpPlugin");
            if ((plugin != null) && (plugin.Instance is IHelp))
            {
                this.HelpPlugin = plugin.Instance as IHelp;
                this.HelpPlugin.SetGraphicsDevice();
                this.HelpPlugin.SetScreen(screen);
                screen.PluginList.Add(this.HelpPlugin.Instance as GameObject);
            }
            plugin = new PersonDetailPlugin.PersonDetailPlugin();  // Plugin.Plugins.AvailablePlugins.Find("PersonDetailPlugin");
            if ((plugin != null) && (plugin.Instance is IPersonDetail))
            {
                this.PersonDetailPlugin = plugin.Instance as IPersonDetail;
                this.PersonDetailPlugin.SetGraphicsDevice();
                this.PersonDetailPlugin.SetScreen(screen);
                screen.PluginList.Add(this.PersonDetailPlugin.Instance as GameObject);
            }
            plugin = new TroopDetailPlugin.TroopDetailPlugin();  // Plugin.Plugins.AvailablePlugins.Find("TroopDetailPlugin");
            if ((plugin != null) && (plugin.Instance is ITroopDetail))
            {
                this.TroopDetailPlugin = plugin.Instance as ITroopDetail;
                this.TroopDetailPlugin.SetGraphicsDevice();
                this.TroopDetailPlugin.SetScreen(screen);
                screen.PluginList.Add(this.TroopDetailPlugin.Instance as GameObject);
            }
            plugin = new ArchitectureDetail.ArchitectureDetailPlugin(); // Plugin.Plugins.AvailablePlugins.Find("ArchitectureDetailPlugin");
            if ((plugin != null) && (plugin.Instance is IArchitectureDetail))
            {
                this.ArchitectureDetailPlugin = plugin.Instance as IArchitectureDetail;
                this.ArchitectureDetailPlugin. SetGraphicsDevice();
                this.ArchitectureDetailPlugin.SetScreen(screen);
                screen.PluginList.Add(this.ArchitectureDetailPlugin.Instance as GameObject);
            }
            plugin = new FactionTechniquesPlugin.FactionTechniquesPlugin();  // Plugin.Plugins.AvailablePlugins.Find("FactionTechniquesPlugin");
            if ((plugin != null) && (plugin.Instance is IFactionTechniques))
            {
                this.FactionTechniquesPlugin = plugin.Instance as IFactionTechniques;
                this.FactionTechniquesPlugin.SetScreen(screen);
                this.FactionTechniquesPlugin.SetGraphicsDevice();
                screen.PluginList.Add(this.FactionTechniquesPlugin.Instance as GameObject);
            }
            plugin = new TreasureDetailPlugin.TreasureDetailPlugin();  // Plugin.Plugins.AvailablePlugins.Find("TreasureDetailPlugin");
            if ((plugin != null) && (plugin.Instance is ITreasureDetail))
            {
                this.TreasureDetailPlugin = plugin.Instance as ITreasureDetail;
                this.TreasureDetailPlugin.SetGraphicsDevice();
                this.TreasureDetailPlugin.SetScreen(screen);
                screen.PluginList.Add(this.TreasureDetailPlugin.Instance as GameObject);
            }
            plugin = new CommentTextPlugin.CommentTextPlugin();  // Plugin.Plugins.AvailablePlugins.Find("CommentTextPlugin");
            if ((plugin != null) && (plugin.Instance is IConmentText))
            {
                this.ConmentTextPlugin = plugin.Instance as IConmentText;
                this.ConmentTextPlugin.SetGraphicsDevice();
                screen.PluginList.Add(this.ConmentTextPlugin.Instance as GameObject);
            }
            plugin = new ArchitectureSurveyPlugin.ArchitectureSurveyPlugin();  // Plugin.Plugins.AvailablePlugins.Find("ArchitectureSurveyPlugin");
            if ((plugin != null) && (plugin.Instance is IArchitectureSurvey))
            {
                this.ArchitectureSurveyPlugin = plugin.Instance as IArchitectureSurvey;
                this.ArchitectureSurveyPlugin.SetGraphicsDevice();
                screen.PluginList.Add(this.ArchitectureSurveyPlugin.Instance as GameObject);
            }
            plugin = new TroopSurveyPlugin.TroopSurveyPlugin();  // Plugin.Plugins.AvailablePlugins.Find("TroopSurveyPlugin");
            if ((plugin != null) && (plugin.Instance is ITroopSurvey))
            {
                this.TroopSurveyPlugin = plugin.Instance as ITroopSurvey;
                this.TroopSurveyPlugin.SetGraphicsDevice();
                screen.PluginList.Add(this.TroopSurveyPlugin.Instance as GameObject);
            }
            plugin = new ContextMenuPlugin.ContextMenuPlugin();  // Plugin.Plugins.AvailablePlugins.Find("ContextMenuPlugin");
            if ((plugin != null) && (plugin.Instance is IGameContextMenu))
            {
                this.ContextMenuPlugin = plugin.Instance as IGameContextMenu;
                this.ContextMenuPlugin.SetScreen(screen);
                this.ContextMenuPlugin.SetGraphicsDevice();
                this.ContextMenuPlugin.SetIHelp(this.HelpPlugin);
                screen.PluginList.Add(this.ContextMenuPlugin.Instance as GameObject);
            }
            plugin = new GameFormFramePlugin.GameFramePlugin();  // Plugin.Plugins.AvailablePlugins.Find("GameFramePlugin");
            if ((plugin != null) && (plugin.Instance is IGameFrame))
            {
                this.GameFramePlugin = plugin.Instance as IGameFrame;
                this.GameFramePlugin.SetScreen(screen);
                this.GameFramePlugin.SetGraphicsDevice();
                screen.PluginList.Add(this.GameFramePlugin.Instance as GameObject);
            }
            plugin = new ScreenBlindPlugin.ScreenBlindPlugin();  // Plugin.Plugins.AvailablePlugins.Find("ScreenBlindPlugin");
            if ((plugin != null) && (plugin.Instance is IScreenBlind))
            {
                this.ScreenBlindPlugin = plugin.Instance as IScreenBlind;
                this.ScreenBlindPlugin.SetScreen(screen);
                this.ScreenBlindPlugin.SetGraphicsDevice();
                screen.PluginList.Add(this.ScreenBlindPlugin.Instance as GameObject);
            }
            plugin = new MapViewSelectorPlugin.MapViewSelectorPlugin();  // Plugin.Plugins.AvailablePlugins.Find("MapViewSelectorPlugin");
            if ((plugin != null) && (plugin.Instance is IMapViewSelector))
            {
                this.MapViewSelectorPlugin = plugin.Instance as IMapViewSelector;
                this.MapViewSelectorPlugin.SetScreen(screen);
                this.MapViewSelectorPlugin.SetGraphicsDevice();
                this.MapViewSelectorPlugin.SetGameFrame(this.GameFramePlugin);
                screen.PluginList.Add(this.MapViewSelectorPlugin.Instance as GameObject);
            }
            plugin = new TabListPlugin.TabListPlugin();  // Plugin.Plugins.AvailablePlugins.Find("TabListPlugin");
            if ((plugin != null) && (plugin.Instance is ITabList))
            {
                this.TabListPlugin = plugin.Instance as ITabList;
                this.TabListPlugin.SetScreen(screen);
                this.TabListPlugin.SetGraphicsDevice();
                this.TabListPlugin.SetPersonDetailDialog(this.PersonDetailPlugin);
                this.TabListPlugin.SetTroopDetailDialog(this.TroopDetailPlugin);
                this.TabListPlugin.SetArchitectureDetailDialog(this.ArchitectureDetailPlugin);
                this.TabListPlugin.SetFactionTechniquesDialog(this.FactionTechniquesPlugin);
                this.TabListPlugin.SetTreasureDetailDialog(this.TreasureDetailPlugin);
                this.TabListPlugin.SetGameFrame(this.GameFramePlugin);
                this.TabListPlugin.SetMapViewSelector(this.MapViewSelectorPlugin);
                screen.PluginList.Add(this.TabListPlugin.Instance as GameObject);
            }
            plugin = new OptionDialogPlugin.OptionDialogPlugin();  // Plugin.Plugins.AvailablePlugins.Find("OptionDialogPlugin");
            if ((plugin != null) && (plugin.Instance is IOptionDialog))
            {
                this.OptionDialogPlugin = plugin.Instance as IOptionDialog;
                this.OptionDialogPlugin.SetScreen(screen);
                this.OptionDialogPlugin.SetGraphicsDevice();
                screen.PluginList.Add(this.OptionDialogPlugin.Instance as GameObject);
            }
            plugin = new SimpleTextDialogPlugin.SimpleTextDialogPlugin();  // Plugin.Plugins.AvailablePlugins.Find("SimpleTextDialogPlugin");
            if ((plugin != null) && (plugin.Instance is ISimpleTextDialog))
            {
                this.SimpleTextDialogPlugin = plugin.Instance as ISimpleTextDialog;
                this.SimpleTextDialogPlugin.SetScreen(screen);
                this.SimpleTextDialogPlugin.SetGraphicsDevice();
                screen.PluginList.Add(this.SimpleTextDialogPlugin.Instance as GameObject);
            }

            plugin = new EventDisplayPlugin.EventDisplayPlugin();  // Plugin.Plugins.AvailablePlugins.Find("EventDisplayPlugin");
            if ((plugin != null) && (plugin.Instance is Itupianwenzi))
            {
                this.EventDisplayPlugin = plugin.Instance as Itupianwenzi;
                this.EventDisplayPlugin.SetScreen(screen);
                this.EventDisplayPlugin.SetGraphicsDevice();
                this.EventDisplayPlugin.SetContextMenu(this.ContextMenuPlugin);
                screen.PluginList.Add(this.EventDisplayPlugin.Instance as GameObject);
            }

            plugin = new ConfirmationDialogPlugin.ConfirmationDialogPlugin();  // Plugin.Plugins.AvailablePlugins.Find("ConfirmationDialogPlugin");
            if ((plugin != null) && (plugin.Instance is IConfirmationDialog))
            {
                this.ConfirmationDialogPlugin = plugin.Instance as IConfirmationDialog;
                this.ConfirmationDialogPlugin.SetScreen(screen);
                this.ConfirmationDialogPlugin.SetGraphicsDevice();
                screen.PluginList.Add(this.ConfirmationDialogPlugin.Instance as GameObject);
            }
            plugin = new ToolBarPlugin.ToolBarPlugin();  // Plugin.Plugins.AvailablePlugins.Find("ToolBarPlugin");
            if ((plugin != null) && (plugin.Instance is IToolBar))
            {
                this.ToolBarPlugin = plugin.Instance as IToolBar;
                this.ToolBarPlugin.SetScreen(screen);
                this.ToolBarPlugin.SetGraphicsDevice();
                this.ToolBarPlugin.SetContextMenuPlugin(this.ContextMenuPlugin);
                screen.PluginList.Add(this.ToolBarPlugin.Instance as GameObject);
            }
            if (this.ToolBarPlugin != null)
            {
                plugin = new DateRunnerPlugin.DateRunnerPlugin();  // Plugin.Plugins.AvailablePlugins.Find("DateRunnerPlugin");
                if ((plugin != null) && (plugin.Instance is IDateRunner))
                {
                    this.DateRunnerPlugin = plugin.Instance as IDateRunner;
                    this.DateRunnerPlugin.SetScreen(screen);
                    this.DateRunnerPlugin.SetGraphicsDevice();
                    this.DateRunnerPlugin.SetGameDate(Session.Current.Scenario.Date);
                    this.ToolBarPlugin.AddTool(this.DateRunnerPlugin.ToolInstance);
                    screen.PluginList.Add(this.DateRunnerPlugin.Instance as GameObject);
                }
            }
            if (this.ToolBarPlugin != null)
            {
                plugin = new GameRecordPlugin.GameRecordPlugin();  // Plugin.Plugins.AvailablePlugins.Find("GameRecordPlugin");
                if ((plugin != null) && (plugin.Instance is IGameRecord))
                {
                    this.GameRecordPlugin = plugin.Instance as IGameRecord;
                    this.GameRecordPlugin.SetScreen(screen);
                    this.GameRecordPlugin.SetGraphicsDevice();
                    this.ToolBarPlugin.AddTool(this.GameRecordPlugin.ToolInstance);
                    screen.PluginList.Add(this.GameRecordPlugin.Instance as GameObject);
                }
            }
            if (this.ToolBarPlugin != null)
            {
                plugin = new MapLayerPlugin.MapLayerPlugin();  // Plugin.Plugins.AvailablePlugins.Find("MapLayerPlugin");
                if ((plugin != null) && (plugin.Instance is IMapLayer))
                {
                    this.MapLayerPlugin = plugin.Instance as IMapLayer;
                    this.MapLayerPlugin.SetScreen(screen);
                    this.MapLayerPlugin.SetGraphicsDevice();
                    this.ToolBarPlugin.AddTool(this.MapLayerPlugin.ToolInstance);
                    screen.PluginList.Add(this.MapLayerPlugin.Instance as GameObject);
                }
            }
            if (this.ToolBarPlugin != null)
            {
                plugin = new GameSystemPlugin.GameSystemPlugin();  // Plugin.Plugins.AvailablePlugins.Find("GameSystemPlugin");
                if ((plugin != null) && (plugin.Instance is IGameSystem))
                {
                    this.GameSystemPlugin = plugin.Instance as IGameSystem;
                    this.GameSystemPlugin.SetScreen(screen);
                    this.GameSystemPlugin.SetGraphicsDevice();
                    this.GameSystemPlugin.SetOptionDialog(this.OptionDialogPlugin);
                    this.ToolBarPlugin.AddTool(this.GameSystemPlugin.ToolInstance);
                    screen.PluginList.Add(this.GameSystemPlugin.Instance as GameObject);
                }
            }
            if (this.ToolBarPlugin != null)
            {
                plugin = new AirViewPlugin.AirViewPlugin();  // Plugin.Plugins.AvailablePlugins.Find("AirViewPlugin");
                if ((plugin != null) && (plugin.Instance is IAirView))
                {
                    this.AirViewPlugin = plugin.Instance as IAirView;
                    this.AirViewPlugin.SetScreen(screen);
                    this.AirViewPlugin.SetGraphicsDevice();
                    this.ToolBarPlugin.AddTool(this.AirViewPlugin.ToolInstance);
                    screen.PluginList.Add(this.AirViewPlugin.Instance as GameObject);
                }
            }

            plugin = new PersonPortraitPlugin.PersonPortraitPlugin();  // Plugin.Plugins.AvailablePlugins.Find("PersonPortraitPlugin");
            if ((plugin != null) && (plugin.Instance is IPersonPortrait))
            {
                this.PersonPortraitPlugin = plugin.Instance as IPersonPortrait;
                this.PersonPortraitPlugin.SetGraphicsDevice();
                screen.PluginList.Add(this.PersonPortraitPlugin.Instance as GameObject);
            }
            plugin = new PersonBubble.PersonBubblePlugin();  // Plugin.Plugins.AvailablePlugins.Find("PersonBubblePlugin");
            if ((plugin != null) && (plugin.Instance is IPersonBubble))
            {
                this.PersonBubblePlugin = plugin.Instance as IPersonBubble;
                this.PersonBubblePlugin.SetScreen(screen);
                this.PersonBubblePlugin.SetGraphicsDevice();
                screen.PluginList.Add(this.PersonBubblePlugin.Instance as GameObject);
            }
            plugin = new TroopTitlePlugin.TroopTitlePlugin();  // Plugin.Plugins.AvailablePlugins.Find("TroopTitlePlugin");
            if ((plugin != null) && (plugin.Instance is ITroopTitle))
            {
                this.TroopTitlePlugin = plugin.Instance as ITroopTitle;
                this.TroopTitlePlugin.SetGraphicsDevice();
                screen.PluginList.Add(this.TroopTitlePlugin.Instance as GameObject);
            }
            plugin = new RoutewayEditorPlugin.RoutewayEditorPlugin();  // Plugin.Plugins.AvailablePlugins.Find("RoutewayEditorPlugin");
            if ((plugin != null) && (plugin.Instance is IRoutewayEditor))
            {
                this.RoutewayEditorPlugin = plugin.Instance as IRoutewayEditor;
                this.RoutewayEditorPlugin.SetScreen(screen);
                this.RoutewayEditorPlugin.SetGraphicsDevice();
                screen.PluginList.Add(this.RoutewayEditorPlugin.Instance as GameObject);
            }
            plugin = new NumberInputerPlugin.NumberInputerPlugin();  // Plugin.Plugins.AvailablePlugins.Find("NumberInputerPlugin");
            if ((plugin != null) && (plugin.Instance is INumberInputer))
            {
                this.NumberInputerPlugin = plugin.Instance as INumberInputer;
                this.NumberInputerPlugin.SetScreen(screen);
                this.NumberInputerPlugin.SetGraphicsDevice();
                screen.PluginList.Add(this.NumberInputerPlugin.Instance as GameObject);
            }
            plugin = new TransportDialogPlugin.TransportDialogPlugin();  // Plugin.Plugins.AvailablePlugins.Find("TransportDialogPlugin");
            if ((plugin != null) && (plugin.Instance is ITransportDialog))
            {
                this.TransportDialogPlugin = plugin.Instance as ITransportDialog;
                this.TransportDialogPlugin.SetScreen(screen);
                this.TransportDialogPlugin.SetGraphicsDevice();
                this.TransportDialogPlugin.SetGameFrame(this.GameFramePlugin);
                this.TransportDialogPlugin.SetTabList(this.TabListPlugin);
                this.TransportDialogPlugin.SetNumberInputer(this.NumberInputerPlugin);
                screen.PluginList.Add(this.TransportDialogPlugin.Instance as GameObject);
            }
            plugin = new CreateTroopPlugin.CreateTroopPlugin();  // Plugin.Plugins.AvailablePlugins.Find("CreateTroopPlugin");
            if ((plugin != null) && (plugin.Instance is ICreateTroop))
            {
                this.CreateTroopPlugin = plugin.Instance as ICreateTroop;
                this.CreateTroopPlugin.SetGraphicsDevice();
                this.CreateTroopPlugin.SetScreen(screen);
                this.CreateTroopPlugin.SetGameFrame(this.GameFramePlugin);
                this.CreateTroopPlugin.SetTabList(this.TabListPlugin);
                this.CreateTroopPlugin.SetNumberInputer(this.NumberInputerPlugin);
                screen.PluginList.Add(this.CreateTroopPlugin.Instance as GameObject);
            }
            plugin = new MarshalSectionDialogPlugin.MarshalSectionDialogPlugin();  // Plugin.Plugins.AvailablePlugins.Find("MarshalSectionDialogPlugin");
            if ((plugin != null) && (plugin.Instance is IMarshalSectionDialog))
            {
                this.MarshalSectionDialogPlugin = plugin.Instance as IMarshalSectionDialog;
                this.MarshalSectionDialogPlugin.SetGraphicsDevice();
                this.MarshalSectionDialogPlugin.SetScreen(screen);
                this.MarshalSectionDialogPlugin.SetGameFrame(this.GameFramePlugin);
                this.MarshalSectionDialogPlugin.SetTabList(this.TabListPlugin);
                screen.PluginList.Add(this.MarshalSectionDialogPlugin.Instance as GameObject);
            }

            plugin = new RightSidePanelPlugin.TabListPlugin();  // Plugin.Plugins.AvailablePlugins.Find("RightSidePanelPlugin");
            if ((plugin != null) && (plugin.Instance is Iyoucelan))
            {
                this.RightSidePanelPlugin = plugin.Instance as Iyoucelan;
                this.RightSidePanelPlugin.SetScreen(screen);
                this.RightSidePanelPlugin.SetGraphicsDevice();
                this.RightSidePanelPlugin.SetPersonDetailDialog(this.PersonDetailPlugin);
                this.RightSidePanelPlugin.SetTroopDetailDialog(this.TroopDetailPlugin);
                this.RightSidePanelPlugin.SetArchitectureDetailDialog(this.ArchitectureDetailPlugin);
                this.RightSidePanelPlugin.SetFactionTechniquesDialog(this.FactionTechniquesPlugin);
                this.RightSidePanelPlugin.SetTreasureDetailDialog(this.TreasureDetailPlugin);
                this.RightSidePanelPlugin.SetGameFrame(this.GameFramePlugin);
                this.RightSidePanelPlugin.SetMapViewSelector(this.MapViewSelectorPlugin);
                screen.PluginList.Add(this.RightSidePanelPlugin.Instance as GameObject);
            }

            plugin = new TroopArrangementPlugin.TabListPlugin();  // Plugin.Plugins.AvailablePlugins.Find("TroopArrangementPlugin");
            if ((plugin != null) && (plugin.Instance is IBianduiLiebiao))
            {
                this.TroopArrangement = plugin.Instance as IBianduiLiebiao;
                this.TroopArrangement.SetScreen(screen);
                this.TroopArrangement.SetGraphicsDevice();
                this.TroopArrangement.SetPersonDetailDialog(this.PersonDetailPlugin);
                this.TroopArrangement.SetTroopDetailDialog(this.TroopDetailPlugin);
                this.TroopArrangement.SetArchitectureDetailDialog(this.ArchitectureDetailPlugin);
                this.TroopArrangement.SetFactionTechniquesDialog(this.FactionTechniquesPlugin);
                this.TroopArrangement.SetTreasureDetailDialog(this.TreasureDetailPlugin);
                this.TroopArrangement.SetGameFrame(this.GameFramePlugin);
                this.TroopArrangement.SetMapViewSelector(this.MapViewSelectorPlugin);
                screen.PluginList.Add(this.TroopArrangement.Instance as GameObject);
            }
            
        }

    } 

}
