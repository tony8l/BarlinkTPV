using BarlinkTPV.Models.DTOs;
using BarlinkTPV.Resources.Languages;
using BarlinkTPV.Resources.Styles;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarlinkTPV.Services
{
    public class SettingsService
    {
        public void AplicarAjustes(Ajustes ajustes)
        {
            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries == null)
            {
                return;
            }

            mergedDictionaries.Clear();
            mergedDictionaries.Add(ajustes.Tema switch
            {
                Tema.Claro => new LightTheme(),
                Tema.Oscuro => new DarkTheme(),
                Tema.Predeterminado => new MainTheme(),
                _ => new MainTheme()
            });

            mergedDictionaries.Add(ajustes.Idioma switch
            {
                Idioma.ENG => new English(),
                _ => new Spanish()
            });
        }
    }
}
