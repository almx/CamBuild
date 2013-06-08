using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using CamBuild.Core.Exceptions;
using CamBuild.Core.Attributes;

namespace CamBuild.Core
{
	public class ActionPropertySetter
	{
		public static void SetProperties(IAction action)
		{
			foreach (PropertyInfo pi in action.GetType().GetProperties())
			{
				object[] propertyAttributes = pi.GetCustomAttributes(typeof(ActionPropertyAttribute), false);

				if (propertyAttributes.Length == 1)
				{
					if (action.Fields.ContainsKey(pi.Name))
					{
						pi.SetValue(action, Convert.ChangeType(ValueStringEvaluator.Evaluate(action.Fields[pi.Name],
							action.ParentBuildFile), pi.PropertyType), null);
						action.Fields.Remove(pi.Name);
					}
					else
					{
						ActionPropertyAttribute apa = (ActionPropertyAttribute)propertyAttributes[0];

						if (apa.IsRequired)
							throw new ActionPropertyNotSetException(action, pi, "The value for a required property was not set.");
					}
				}
			}

			EvaluateFields(action);
		}

		private static void EvaluateFields(IAction action)
		{
			Dictionary<string, string> newFields = new Dictionary<string, string>();

			foreach (string key in action.Fields.Keys)
			{
				newFields.Add(key, ValueStringEvaluator.Evaluate(action.Fields[key], action.ParentBuildFile));
			}

			action.Fields.Clear();

			foreach (KeyValuePair<string, string> kvp in newFields)
			{
				action.Fields.Add(kvp.Key, kvp.Value);
			}
		}
	}
}
