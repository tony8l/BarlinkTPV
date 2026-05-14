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
            // Obtenemos todos los diciconarios cargados
            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries == null)
            {
                return;
            }

            // Limpiamos la lista para quitar todos los diccionarios
            mergedDictionaries.Clear();

            // Añadimos un diccionario de tema según el switch
            mergedDictionaries.Add(ajustes.Tema switch
            {
                Tema.Claro => new LightTheme(),
                Tema.Oscuro => new DarkTheme(),
                Tema.Predeterminado => new MainTheme(),
                _ => new MainTheme()
            });

            // Añadimos un diccionario de idioma según el switch
            mergedDictionaries.Add(ajustes.Idioma switch
            {
                Idioma.ENG => new English(),
                _ => new Spanish()
            });
        }
    }
}
