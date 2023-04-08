using System;
using System.Windows.Controls;
using Jaml.Wpf.Models.JsonModels;
using Jaml.Wpf.Models.UIElementModels;
using MaterialDesignExtensions.Controls;

namespace Lolita
{
    internal static class Program
    {
        private const string RootJsonPath = "Resources/Root.json";

        internal static Grid MainGrid { get; private set; } = new Grid();

        internal static MaterialWindow Window { get; private set; } = new MaterialWindow();

        [STAThread]
        private static void Main(string[] args)
        {
            string rootJsonPath = args.Length > 0 ? args[0] : RootJsonPath;

            RootModel rootModel = GetRootModel(rootJsonPath);
            StylesModel stylesModel = GetStylesModel(rootModel.Styles);
            WindowModel<MaterialWindow> windowModel = JsonModel.GetModel<MainWindowModel<MaterialWindow>>(rootModel.MainWindow).MainWindow.WindowModel;

            Commands.RegisterCommands();
            Styles.RegisterStyles(stylesModel);

            StartMainWindow(windowModel);
        }

        private static RootModel GetRootModel(string rootJsonPath) =>
            JsonModel.GetModel<RootModel>(rootJsonPath).Root;

        private static StylesModel GetStylesModel(string stylesJsonPath) =>
            JsonModel.GetModel<StylesModel>(stylesJsonPath);

        private static void StartMainWindow(WindowModel<MaterialWindow> windowModel)
        {
            MaterialWindow mainWindow = windowModel.ToUIElement(Commands.CommandProvider, Styles.StyleProvider);

            //Bind local reference to window's grid
            MainGrid = mainWindow.Content as Grid;

            Window = mainWindow;
            Window.ShowDialog();
        }
    }
}
