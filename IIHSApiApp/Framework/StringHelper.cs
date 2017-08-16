using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace IIHSApiApp.Framework
{
    public static class StringHelper
    {
        public static void SmartAppend(this StringBuilder builder, string delimiter, params string[] parameters)
        {
            foreach (string str in parameters)
            {
                if (builder.Length > 0)
                {
                    builder.Append(delimiter);
                }
                builder.Append(str);
            }
        }

        public static string SmartAppend(string delimiter, params string[] parameters)
        {
            StringBuilder builder = new StringBuilder();

            foreach (string str in parameters)
            {
                if (!String.IsNullOrEmpty(str))
                {
                    if (builder.Length > 0)
                        builder.Append(delimiter);

                    builder.Append(str);
                }
            }

            return builder.ToString();
        }

        public static string SmartAppend(string delimiter, List<string> parameters)
        {
            return SmartAppend(delimiter, parameters.ToArray());
        }

        public static string SmartAppend(string delimiter, params int[] parameters)
        {
            string[] newParams = new string[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
                newParams[i] = parameters[i].ToString();

            return SmartAppend(delimiter, newParams);
        }

        public static string SmartAppend(string delimiter, params object[] parameters)
        {
            StringBuilder builder = new StringBuilder();

            foreach (object obj in parameters)
            {
                if (obj != null || (obj is string && !string.IsNullOrEmpty((string)obj)))
                {
                    if (builder.Length > 0)
                        builder.AppendFormat("{0}", delimiter);

                    builder.Append(obj.ToString());
                }
            }

            return builder.ToString();
        }
    }
}