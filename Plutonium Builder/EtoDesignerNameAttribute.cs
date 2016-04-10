namespace EtoDesignerHost
{
    using System;

    /// This attribute is used to mark "Name" properties that have been provided
    /// by our IExtenderProvider in order to differentiate them from other Name properties
    /// which we are not interested in. It's just a bool set to true if it's what we want.
    /// The attribute target is a Method, since the IExtenderProvider uses methods to specify
    /// the get/set for our "Name" property.
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    internal sealed class EtoDesignerNameAttribute : Attribute
    {
        private bool designerName;

        public static EtoDesignerNameAttribute Default = new EtoDesignerNameAttribute(false);

        public EtoDesignerNameAttribute() : this(false)
        {
        }

        public EtoDesignerNameAttribute(bool designerName)
        {
            this.designerName = designerName;
        }

        public override bool Equals(object obj)
        {
            EtoDesignerNameAttribute da = obj as EtoDesignerNameAttribute;

            if (da == null)
            {
                return false;
            }
            return da.designerName == this.designerName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}