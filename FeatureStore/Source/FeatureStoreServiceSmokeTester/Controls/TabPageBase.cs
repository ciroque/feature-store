namespace FeatureStoreServiceSmokeTester.Controls
{
    using System.Windows.Forms;
    using Controllers;

    public enum ArgumentFieldsLayout
    {
        CheckFeatureState,
        CreateFeature,
        UpdateFeature,
        RetrieveDefinedFeatures
    }

    /// <summary>
    ///   SUMMARY
    /// </summary>
    public abstract class TabPageBase : TabPage, IServiceMethodUiBridge
    {
        /// <summary>
        /// </summary>
        private readonly ArgumentFieldsLayout m_Layout;

        private FeatureStoreMethodArgumentsPanel m_FeatureStoreMethodArguments;

        /// <summary>
        /// </summary>
        private SplitContainer m_MasterSplitContainer;

        /// <summary>
        /// </summary>
        private RichTextBox m_RichTextBox;

        protected TabPageBase(string text, ArgumentFieldsLayout layout)
            : base(text)
        {
            m_Layout = layout;
        }

        /// <summary>
        ///   Called after the control has been added to another container.
        /// </summary>
        protected override void InitLayout()
        {
            CreateMasterSplitContainer();
            CreateRichTextBox();
            CreateFeatureStoreArgumentsControl();

            Dock = DockStyle.Fill;
            base.InitLayout();
        }

        /// <summary>
        ///   Creates the feature store arguments control.
        /// </summary>
        private void CreateFeatureStoreArgumentsControl()
        {
            m_FeatureStoreMethodArguments = FeatureStoreMethodArgumentsPanel.Create(m_Layout);
            m_FeatureStoreMethodArguments.Dock = DockStyle.Fill;
            m_MasterSplitContainer.Panel1.Controls.Add(m_FeatureStoreMethodArguments);
        }

        private void CreateRichTextBox()
        {
            m_RichTextBox = new RichTextBox();
            m_RichTextBox.Dock = DockStyle.Fill;
            m_RichTextBox.Rtf =
                @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fnil\fcharset0 Calibri;}}{\colortbl ;\red0\green176\blue80;}{\*\generator Msftedit 5.41.21.2509;}\viewkind4\uc1\pard\sa200\sl276\slmult1\lang9\f0\fs22 This is some \cf1\b\fs40 TEST TEXT\cf0\b0\fs22\par}";

            m_MasterSplitContainer.Panel2.Controls.Add(m_RichTextBox);
        }

        private void CreateMasterSplitContainer()
        {
            m_MasterSplitContainer = new SplitContainer();
            m_MasterSplitContainer.Orientation = Orientation.Horizontal;
            m_MasterSplitContainer.Dock = DockStyle.Fill;
            m_MasterSplitContainer.Visible = true;
            Controls.Add(m_MasterSplitContainer);
        }

        #region IServiceMethodUiBridge implementation

        /// <summary>
        ///   Displays the results.
        /// </summary>
        /// <param name = "results">The results.</param>
        public void DisplayResults(string results)
        {
            if (results.StartsWith("{\\rtf"))
            {
                m_RichTextBox.Rtf = results;
            }
            else
            {
                m_RichTextBox.Text = results;
            }
        }

        /// <summary>
        ///   Gets the feature store method arguments.
        /// </summary>
        /// <value>The feature store method arguments.</value>
        public IFeatureStoreMethodArguments FeatureStoreMethodArguments
        {
            get { return m_FeatureStoreMethodArguments; }
        }

        #endregion
    }
}