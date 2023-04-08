using Jaml.Wpf.Models.JsonModels;
using Jaml.Wpf.Models.StyleModels;
using Jaml.Wpf.Providers.StyleProvider;

namespace Lolita
{
    public static class Styles
    {
        #region Properties

        internal static StyleProvider StyleProvider { get; } = new StyleProvider();

        #endregion

        #region Methods

        public static void RegisterStyles(StylesModel stylesModel)
        {
            foreach (StyleModel styleModel in stylesModel.StyleModels) StyleProvider.RegisterStyleModel(styleModel.Id, styleModel);
        }

        #endregion
    }
}
