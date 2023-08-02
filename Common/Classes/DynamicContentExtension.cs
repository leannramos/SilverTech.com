using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;

namespace Common.Classes
{
    public static class DynamicContentExtension
    {
        #region getters
        public static string GetStringValue(this DynamicContent item, string columnName, string defaultValue = "")
        {
            var value = item.GetObjectValue(columnName);
            if (value != null)
            {
                return value.ToString();
            }
            return defaultValue;
        }

        public static bool GetBooleanValue(this DynamicContent item, string columnName, bool defaultValue = false)
        {
            var value = item.GetObjectValue(columnName);
            bool boolVal;
            if (value != null && Boolean.TryParse(value.ToString(), out boolVal))
            {
                return boolVal;
            }
            return defaultValue;
        }

        public static int GetIntegerValue(this DynamicContent item, string columnName, int defaultValue = 0)
        {
            var value = item.GetObjectValue(columnName);
            decimal decimalValue;
            if (value != null && Decimal.TryParse(value.ToString(), out decimalValue))
            {
                return (int)decimalValue;
            }
            return defaultValue;
        }

        public static decimal GetDecimalValue(this DynamicContent item, string columnName, decimal defaultValue = 0.0m)
        {
            var value = item.GetObjectValue(columnName);
            decimal decimalValue;
            if (value != null && Decimal.TryParse(value.ToString(), out decimalValue))
            {
                return decimalValue;
            }
            return defaultValue;
        }

        public static T GetEnum<T>(this DynamicContent item, string columnName, T defaultValue)
        {
            var typeString = item.GetStringValue(columnName);
            T typeEnum;
            try
            {
                typeEnum = (T)Enum.Parse(typeof(T), typeString);
                if (typeEnum != null)
                {
                    return typeEnum;
                }
            }
            catch (Exception ex)
            {
                //do something
            }

            return defaultValue;

        }

        public static Guid GetGuidValue(this DynamicContent item, string columnName, Guid defaultValue)
        {
            var value = item.GetObjectValue(columnName);
            if (value != null)
            {
                Guid guid;
                if (Guid.TryParse(value.ToString(), out guid))
                {
                    return guid;
                }
                return defaultValue;
            }
            return defaultValue;
        }

        public static DateTime GetDateTimeValue(this DynamicContent item, string columnName, DateTime defaultValue)
        {
            var value = item.GetObjectValue(columnName);
            if (value != null)
            {
                DateTime datetime;
                if (DateTime.TryParse(value.ToString(), out datetime))
                {
                    return datetime;
                }
                return defaultValue;
            }
            return defaultValue;
        }

        private static object GetObjectValue(this DynamicContent item, string columnName)
        {
            return item.GetValue(columnName);
        }

        public static string GetChoicesFieldSelection(this DynamicContent item, string fieldname)
        {
            var itemValue = item.GetValue(fieldname);

            if (itemValue is ChoiceOption[])
            {
                List<string> types = new List<string>();
                var itemTypes = itemValue as ChoiceOption[];

                foreach (ChoiceOption type in itemTypes)
                {
                    types.Add(type.Text);
                }
                return string.Join(", ", types);
            }
            else if (itemValue is ChoiceOption)
            {
                return (itemValue as ChoiceOption).PersistedValue;
            }
            else
            {
                return itemValue.ToString();
            }
        }

        public static Dictionary<string, string> GetFieldChoicesForModule(this DynamicContent item, string fieldName)
        {
            return GetFieldChoicesForModule(item.GetModuleName(), fieldName);
        }

        public static Dictionary<string, string> GetFieldChoicesForModule(string moduleName, string fieldName)
        {
            var manager = ModuleBuilderManager.GetManager();
            var field = manager.Provider.GetDynamicModuleFields().Where(f => f.ModuleName == moduleName && f.Name == fieldName).SingleOrDefault();

            Dictionary<string, string> choices = new Dictionary<string, string>();
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(field.Choices);
            foreach (XmlElement choice in xml.SelectNodes("/choices/choice"))
            {
                choices.Add(choice.Attributes["value"].Value, choice.Attributes["text"].Value);
            }
            return choices;
        }

        #endregion

    }
}
