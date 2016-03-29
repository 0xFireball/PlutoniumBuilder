
using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace EtoDesignerHost
{
	/// This service relays requests for filtering a component's exposed
	/// attributes, properties, and events to that component's designer.
	public class EtoTypeDescriptorFilterService : ITypeDescriptorFilterService
	{
		public IDesignerHost host;

		public EtoTypeDescriptorFilterService(IDesignerHost host)
		{
			this.host = host;
		}

		/// Get the designer for the given component and cast it as a designer filter.
		private IDesignerFilter GetDesignerFilter(IComponent component)
		{
			return host.GetDesigner(component) as IDesignerFilter;
		}

		#region Implementation of ITypeDescriptorFilterService
		/// Tell the given component's designer to filter properties.
		public bool FilterProperties(System.ComponentModel.IComponent component, System.Collections.IDictionary properties)
		{
			IDesignerFilter filter = GetDesignerFilter(component);
			if (filter != null)
			{
				filter.PreFilterProperties(properties);
				filter.PostFilterProperties(properties);
				return true;
			}
			return false;
		}

		/// Tell the given component's designer to filter attributes.
		public bool FilterAttributes(System.ComponentModel.IComponent component, System.Collections.IDictionary attributes)
		{
			IDesignerFilter filter = GetDesignerFilter(component);
			if (filter != null)
			{
				filter.PreFilterAttributes(attributes);
				filter.PostFilterAttributes(attributes);
				return true;
			}
			return false;
		}

		/// Tell the given component's designer to filter events.
		public bool FilterEvents(System.ComponentModel.IComponent component, System.Collections.IDictionary events)
		{
			IDesignerFilter filter = GetDesignerFilter(component);
			if (filter != null)
			{
				filter.PreFilterEvents(events);
				filter.PostFilterEvents(events);
				return true;
			}
			return false;
		}
		#endregion
	}
}
