using System;
using System.Collections.Generic;
using System.Text;
using CamBuild.Core.Exceptions;

namespace CamBuild.Core
{
	public class BuildFileElementExecutor
	{
		private List<string> compsExecuted = new List<string>();

		public void ExecuteElements(ICollection<IBuildFileElement> elements)
		{
			foreach (IBuildFileElement element in elements)
			{
				ExecuteElement(element);
			}
		}

		public void ExecuteElement(IBuildFileElement element)
		{
			if (element is IAction)
			{
				ExecuteAction((IAction)element);
			}
			else if (element is BuildComponent)
			{
				ExecuteBuildComponent((BuildComponent)element);
			}
		}

		private void ExecuteBuildComponent(BuildComponent bc)
		{
			if (!compsExecuted.Contains(bc.Name))
			{
				compsExecuted.Add(bc.Name);

				Logging.Instance.LogEntry("BuildComponentExecute", (bc.Name));

				List<BuildComponent> deps = (List<BuildComponent>)bc.ParentBuildFile.GetBuildComponents((List<string>)bc.Dependencies);

				this.ExecuteElements(deps.ConvertAll(new Converter<BuildComponent, IBuildFileElement>(Utility.ConvertBuildComponent)));
				this.ExecuteElements(((List<IAction>)bc.Actions).ConvertAll(new Converter<IAction, IBuildFileElement>(Utility.ConvertBaseAction)));
			}
		}

		private void ExecuteAction(IAction action)
		{
			ActionPropertySetter.SetProperties((IAction)action);

			try
			{
				Logging.Instance.LogEntry("ActionExecute", ((IAction)action).GetType().Name);
				((IAction)action).Execute();
			}
			catch (ActionNotExecutedException ex)
			{
				Logging.Instance.LogEntry("FailureActionExecute", ex.Action.GetType().Name + " failed to execute; Exception Message: " +
					ex.Message);
			}
		}
	}
}
