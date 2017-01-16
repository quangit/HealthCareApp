﻿using Cirrious.MvvmCross.Localization;
using System.Globalization;
using System.Resources;

namespace HealthCare.Core.Resources
{
    public class ResxTextProvider : IMvxTextProvider
    {
        private readonly ResourceManager _resourceManager;

        public ResxTextProvider(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;

			CurrentLanguage = CultureInfo.CurrentUICulture;
        }

        public static CultureInfo CurrentLanguage { get; set; }

        public string GetText(string namespaceKey, string typeKey, string name)
        {
            string resolvedKey = name;

            if (!string.IsNullOrEmpty(typeKey))
            {
                resolvedKey = string.Format("{0}.{1}", typeKey, resolvedKey);
            }

            if (!string.IsNullOrEmpty(namespaceKey))
            {
                resolvedKey = string.Format("{0}.{1}", namespaceKey, resolvedKey);
            }

            return _resourceManager.GetString(resolvedKey, CurrentLanguage);
        }

        public string GetText(string namespaceKey, string typeKey, string name, params object[] formatArgs)
        {
            string baseText = GetText(namespaceKey, typeKey, name);

            if (string.IsNullOrEmpty(baseText))
            {
                return baseText;
            }

            return string.Format(baseText, formatArgs);
        }
    }
}
