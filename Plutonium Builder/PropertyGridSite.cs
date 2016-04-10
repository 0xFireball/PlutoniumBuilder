using System;
using System.ComponentModel;

namespace Eto_Builder
{
    /// A nearly empty implementation of ISite, this class merely passes on
    /// service requests to the host. It is required when we add the events
    /// tab to our property grid.
    public class PropertyGridSite : System.ComponentModel.ISite
    {
        private IServiceProvider sp;
        private IComponent component;

        public PropertyGridSite(IServiceProvider sp, IComponent component)
        {
            this.sp = sp;
            this.component = component;
        }

        #region Implementation of ISite

        public System.ComponentModel.IComponent Component
        {
            get
            {
                return component;
            }
        }

        public System.ComponentModel.IContainer Container
        {
            get
            {
                return null;
            }
        }

        public bool DesignMode
        {
            get
            {
                return false;
            }
        }

        public string Name
        {
            get { return null; }
            set { }
        }

        #endregion Implementation of ISite

        #region Implementation of IServiceProvider

        public object GetService(Type serviceType)
        {
            if (sp != null)
            {
                return sp.GetService(serviceType);
            }
            return null;
        }

        #endregion Implementation of IServiceProvider
    }
}