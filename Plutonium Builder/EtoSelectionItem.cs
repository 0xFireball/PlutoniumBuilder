
namespace EtoDesignerHost {

    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Windows.Forms;

    // This class represents a single selected object.
    internal class EtoSelectionItem {

        // Public objects this selection deals with
        private Component                 component;      // the component that's selected
        private EtoSelectionService    selectionMgr;   // host interface
        private bool                      primary;        // is this the primary selection?

        ///  Constructor
        internal EtoSelectionItem(EtoSelectionService selectionMgr, Component component) {
            this.component = component;
            this.selectionMgr = selectionMgr;
        }

        internal Component Component {
            get {
                return component;
            }
        }

        ///     Determines if this is the primary selection.  The primary selection uses a
        ///     different set of grab handles and generally supports sizing. The caller must
        ///     verify that there is only one primary object; this merely updates the
        ///     UI.
        internal virtual bool Primary {
            get {
                return primary;
            }
            set {
                if (this.primary != value) {
                    this.primary = value;
                    if (SelectionItemInvalidate != null)
                        SelectionItemInvalidate(this, EventArgs.Empty);
                }
            }
        }

        internal event EventHandler EtoSelectionItemDispose ;
        internal event EventHandler SelectionItemInvalidate ;

        ///     Disposes of this selection.  We dispose of our region object if it still exists and we
        ///     invalidate our UI so that we don't leave any turds laying around.
        internal virtual void Dispose() {
            if (primary) {
                selectionMgr.SetPrimarySelection((EtoSelectionItem)null);
            }

            if (EtoSelectionItemDispose != null)
                EtoSelectionItemDispose(this, EventArgs.Empty);
        }
    }

}
