using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Jaml.Wpf.Models.CommandModels;
using Jaml.Wpf.Models.JsonModels;
using Jaml.Wpf.Providers.CommandProvider;
using Lolita.Constants;

namespace Lolita
{
    public static class Commands
    {
        #region Properties

        internal static CommandProvider CommandProvider { get; } = new CommandProvider();

        #endregion

        internal static void RegisterCommands()
        {
            Dictionary<string, Action<object, IEnumerable<CommandArgModel>>> commandActions =
                new Dictionary<string, Action<object, IEnumerable<CommandArgModel>>>
                {
                    { CommandNames.LoadPageModel, LoadPageModel },
                    { CommandNames.CloseApp, CloseApp },
                    { CommandNames.ClearPage, ClearPage },
                    { CommandNames.LoopMediaElement, LoopMediaElement },
                    { CommandNames.PlayMediaElement, PlayMediaElement }
                };
            CommandProvider.RegisterCommands(commandActions);
        }

        #region Commands

        private static void LoadPageModel(object sender, IEnumerable<CommandArgModel> args)
        {
            //Clear previous page
            ClearPage();

            //Clear commands dictionary from previous page
            CommandProvider.ClearCommands();

            //Re-register basic commands
            RegisterCommands();

            //Parse new page from .json
            string modelPath = args.FirstOrDefault(a => a.Name == "path")?.Value;
            PageModel pageModel = JsonModel.GetModel<PageModel>(modelPath).Page;
            var grid = pageModel.GridModel.ToUIElement(CommandProvider, Styles.StyleProvider);

            #if DEBUG
            grid.ShowGridLines = true;
            #endif

            //Add all elements to ItemsControl if needed
            //todo use sender?
            Program.MainGrid.Children.Add(grid);
        }

        //todo use sender?
        private static void ClearPage(object sender = null, IEnumerable<CommandArgModel> args = null)
        {
            Program.MainGrid.Children.Clear();
        }

        private static void PlayMediaElement(object sender, IEnumerable<CommandArgModel> args = null) => (sender as MediaElement)?.Play();

        private static void CloseApp(object sender = null, IEnumerable<CommandArgModel> args = null) => Program.Window.Close();

        private static void LoopMediaElement(object sender, IEnumerable<CommandArgModel> args = null)
        {
            MediaElement mediaElement = sender as MediaElement;
            mediaElement?.Stop();
            mediaElement?.Play();
        }

        #endregion
    }
}
