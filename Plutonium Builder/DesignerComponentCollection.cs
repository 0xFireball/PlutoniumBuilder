namespace EtoDesignerHost
{
    using System.ComponentModel;
    using System.Diagnostics;

    /// This collection keeps a list of components parallel with the host's list of
    /// sites for those components.
    internal class DesignerComponentCollection : ComponentCollection
    {
        private EtoDesignerHost host;

        internal DesignerComponentCollection(EtoDesignerHost host) : base(new IComponent[0])
        {
            this.host = host;

            // Initially fill the list with site data.  After the initial fill it is up to
            // those who modify the Sites hash to update us.
            //
            if (host.Sites != null)
            {
                foreach (ISite site in host.Sites.Values)
                {
                    InnerList.Add(site.Component);
                }
            }
        }

        /// Access components from sites by name.
        public override IComponent this[string name]
        {
            get
            {
                Debug.Assert(name != null, "name is null");
                if (name == null)
                {
                    return null;
                }

                if (name.Length == 0)
                {
                    Debug.Assert(host.RootComponent != null, "base component is null");
                    return host.RootComponent;
                }

                ISite site = (ISite)host.Sites[name];
                return (site == null) ? null : site.Component;
            }
        }

        internal void Add(ISite site)
        {
            InnerList.Add(site.Component);
        }

        internal void Clear()
        {
            InnerList.Clear();
        }

        internal void Remove(ISite site)
        {
            InnerList.Remove(site.Component);
        }
    }
}