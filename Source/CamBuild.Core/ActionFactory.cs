using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CamBuild.Core.Exceptions;

namespace CamBuild.Core
{
	public class ActionFactory
	{
		public static IAction Create(XmlElement xe)
		{
			List<Type> actionTypes = (List<Type>)Utility.ActionTypesFromAssemblyFiles;

			foreach (Type type in actionTypes)
			{
				// TODO: Implemented sure-fire check of whether a Type inherits from BaseAction
				if (type.GetInterface("IAction") == typeof(IAction) && type.Name == xe.Name)
				{
					IAction ba = (IAction)type.Assembly.CreateInstance(type.FullName);

					XmlNodeList xnl = xe.ChildNodes;

					foreach (XmlNode xn in xnl)
					{
						ba.Fields.Add(xn.Name, xn.InnerText);
					}

					return ba;
				}
			}

			throw new UnrecognizedActionException("Action '" + xe.Name + "' not found in any searched DLL plugin files", xe.Name, xe);
		}

	}
}
