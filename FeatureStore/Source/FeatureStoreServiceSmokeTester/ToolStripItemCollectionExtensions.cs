namespace FeatureStoreServiceSmokeTester
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    ///   Contains extension methods for working with the ToolStripItemCollection class.
    /// </summary>
    public static class ToolStripItemCollectionExtensions
    {
        /// <summary>
        ///   Finds the by name.
        /// </summary>
        /// <param name = "items">The items.</param>
        /// <param name = "name">The name.</param>
        /// <returns></returns>
        public static ToolStripItem FindByName(this ToolStripItemCollection items, string name)
        {
            foreach (ToolStripItem item in items)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        ///   Fors the each drop down button.
        /// </summary>
        /// <param name = "items">The items.</param>
        /// <param name = "action">The action.</param>
        public static void ForEachDropDownButton(this ToolStripItemCollection items,
                                                 Action<ToolStripDropDownButton> action)
        {
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripDropDownButton)
                {
                    action(item as ToolStripDropDownButton);
                }
            }
        }
    }
}