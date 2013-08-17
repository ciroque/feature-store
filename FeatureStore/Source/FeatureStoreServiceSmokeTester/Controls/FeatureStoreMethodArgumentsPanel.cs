namespace FeatureStoreServiceSmokeTester.Controls
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using Controllers;
    using Resources;

    /// <summary>
    ///   Contains the fields necessary to execute all of the methods on the IFeatureStore interface.
    /// </summary>
    public sealed class FeatureStoreMethodArgumentsPanel : Panel, IFeatureStoreMethodArguments
    {
        private readonly Guid SmokeTestOwnerGuid = new Guid("{203EE2A4-2DC1-4C9F-8124-334CCA96C174}");
        private readonly ArgumentFieldsLayout m_Layout;

        /// <summary>
        /// </summary>
        private TextBox m_IdTextBox;

        /// <summary>
        /// </summary>
        private TextBox m_NameTextBox;

        /// <summary>
        /// </summary>
        private TextBox m_OwnerIdTextBox;

        /// <summary>
        /// </summary>
        private TextBox m_SpaceTextBox;

        /// <summary>
        /// </summary>
        private CheckBox m_StateCheckBox;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "FeatureStoreMethodArgumentsPanel" /> class.
        /// </summary>
        /// <param name = "layout"></param>
        private FeatureStoreMethodArgumentsPanel(ArgumentFieldsLayout layout)
        {
            m_Layout = layout;
            Name = ControlTextsResource.CONTROL_NAME_FEATURE_STATE_METHOD_ARGS_PANEL;
        }

        /// <summary>
        ///   Gets a value indicating whether [show feature id].
        /// </summary>
        /// <value><c>true</c> if [show feature id]; otherwise, <c>false</c>.</value>
        private bool ShowFeatureId
        {
            get
            {
                return m_Layout == ArgumentFieldsLayout.CheckFeatureState ||
                       m_Layout == ArgumentFieldsLayout.CreateFeature || m_Layout == ArgumentFieldsLayout.UpdateFeature;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether [show name].
        /// </summary>
        /// <value><c>true</c> if [show name]; otherwise, <c>false</c>.</value>
        private bool ShowName
        {
            get { return m_Layout == ArgumentFieldsLayout.CreateFeature; }
        }

        /// <summary>
        ///   Gets a value indicating whether [show state].
        /// </summary>
        /// <value><c>true</c> if [show state]; otherwise, <c>false</c>.</value>
        private bool ShowState
        {
            get { return m_Layout == ArgumentFieldsLayout.CreateFeature || m_Layout == ArgumentFieldsLayout.UpdateFeature; }
        }

        /// <summary>
        ///   Gets a value indicating whether [owner GUID is read only].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [owner GUID is read only]; otherwise, <c>false</c>.
        /// </value>
        private bool OwnerGuidIsReadOnly
        {
            get { return m_Layout == ArgumentFieldsLayout.CreateFeature || m_Layout == ArgumentFieldsLayout.UpdateFeature; }
        }

        /// <summary>
        ///   Called after the control has been added to another container.
        /// </summary>
        protected override void InitLayout()
        {
            GenerateLabels();
            GenerateTextBoxes();
            GenerateStateCheckBox();
            GenerateNewGuidButtons();
        }

        private void GenerateNewGuidButtons()
        {
            if (m_Layout == ArgumentFieldsLayout.CreateFeature)
            {
                Button newSpaceGuidButton = new Button();
                newSpaceGuidButton.Name = ControlTextsResource.CONTROL_NAME_NEW_OWNERID_GUID_BUTTON_NAME;
                newSpaceGuidButton.Text = "*";
                newSpaceGuidButton.Left = 452;
                newSpaceGuidButton.Top = 90;
                newSpaceGuidButton.Height = m_SpaceTextBox.Height;
                newSpaceGuidButton.Width = m_SpaceTextBox.Height;
                newSpaceGuidButton.Click += (o, e) => { m_SpaceTextBox.Text = Guid.NewGuid().ToString(); };
                Controls.Add(newSpaceGuidButton);
            }
        }

        /// <summary>
        ///   Generates the state check box.
        /// </summary>
        private void GenerateStateCheckBox()
        {
            if (ShowState)
            {
                m_StateCheckBox = new CheckBox();
                m_StateCheckBox.Name = ControlTextsResource.CONTROL_NAME_FEATURE_STATE_CHECKBOX;
                m_StateCheckBox.Left = 150;
                m_StateCheckBox.Top = 170;
                m_StateCheckBox.BackColor = Color.Transparent;
                Controls.Add(m_StateCheckBox);
            }
        }

        /// <summary>
        ///   Generates the text boxes.
        /// </summary>
        private void GenerateTextBoxes()
        {
            if (ShowFeatureId)
            {
                m_IdTextBox = new TextBox();
                m_IdTextBox.Name = ControlTextsResource.CONTROL_NAME_FEATURE_ID_TEXTBOX;
                m_IdTextBox.Left = 150;
                m_IdTextBox.Top = 10;
                m_IdTextBox.Width = 300;
                Controls.Add(m_IdTextBox);
            }

            if (ShowName)
            {
                m_NameTextBox = new TextBox();
                m_NameTextBox.Name = ControlTextsResource.CONTROL_NAME_FEATURE_NAME_TEXTBOX;
                m_NameTextBox.Left = 150;
                m_NameTextBox.Top = 130;
                m_NameTextBox.Width = 300;
                Controls.Add(m_NameTextBox);
            }

            m_OwnerIdTextBox = new TextBox();
            m_OwnerIdTextBox.Name = ControlTextsResource.CONTROL_NAME_FEATURE_OWNER_ID_TEXTBOX;
            m_OwnerIdTextBox.Left = 150;
            m_OwnerIdTextBox.Top = 50;
            m_OwnerIdTextBox.Width = 300;
            Controls.Add(m_OwnerIdTextBox);

            if (OwnerGuidIsReadOnly)
            {
                m_OwnerIdTextBox.ReadOnly = true;
                m_OwnerIdTextBox.Text = SmokeTestOwnerGuid.ToString();
            }

            m_SpaceTextBox = new TextBox();
            m_SpaceTextBox.Name = ControlTextsResource.CONTROL_NAME_FEATURE_SPACE_TEXTBOX;
            m_SpaceTextBox.Left = 150;
            m_SpaceTextBox.Top = 90;
            m_SpaceTextBox.Width = 300;
            Controls.Add(m_SpaceTextBox);
        }

        /// <summary>
        ///   Generates the labels.
        /// </summary>
        private void GenerateLabels()
        {
            if (ShowFeatureId)
            {
                Label idLabel = new Label();
                idLabel.Text = ControlTextsResource.LABEL_TEXT_ID;
                idLabel.Top = 10;
                idLabel.Left = 10;
                idLabel.BackColor = Color.Transparent;
                Controls.Add(idLabel);
            }

            if (ShowName)
            {
                Label nameLabel = new Label();
                nameLabel.Text = ControlTextsResource.LABEL_TEXT_NAME;
                nameLabel.Top = 130;
                nameLabel.Left = 10;
                nameLabel.BackColor = Color.Transparent;
                Controls.Add(nameLabel);
            }

            if (ShowState)
            {
                Label stateLabel = new Label();
                stateLabel.Text = ControlTextsResource.LABEL_TEXT_STATE;
                stateLabel.Top = 170;
                stateLabel.Left = 10;
                stateLabel.BackColor = Color.Transparent;
                Controls.Add(stateLabel);
            }

            Label ownerIdLabel = new Label();
            ownerIdLabel.Text = ControlTextsResource.LABEL_TEXT_OWNER_ID;
            ownerIdLabel.Top = 50;
            ownerIdLabel.Left = 10;
            ownerIdLabel.BackColor = Color.Transparent;
            Controls.Add(ownerIdLabel);

            Label spaceLabel = new Label();
            spaceLabel.Text = ControlTextsResource.LABEL_TEXT_SPACE;
            spaceLabel.Top = 90;
            spaceLabel.Left = 10;
            spaceLabel.BackColor = Color.Transparent;
            Controls.Add(spaceLabel);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            Pen pen = new Pen(Color.Black, 2.0f);

            for (int index = 0; index < 5; index++)
            {
                Rectangle rectangle = new Rectangle(4, (40*index) + 6, Parent.Width - 8, 30);
                e.Graphics.DrawRectangle(pen, rectangle);
                e.Graphics.FillRectangle(
                    new LinearGradientBrush(rectangle, Color.LightCoral, Color.White, LinearGradientMode.ForwardDiagonal),
                    rectangle);
            }
        }

        public static FeatureStoreMethodArgumentsPanel Create(ArgumentFieldsLayout layout)
        {
            return new FeatureStoreMethodArgumentsPanel(layout);
        }

        #region Implementation of IFeatureStoreMethodArguments

        /// <summary>
        ///   Gets the id.
        /// </summary>
        /// <value>The id.</value>
        public long Id
        {
            get
            {
                if (string.IsNullOrEmpty(m_IdTextBox.Text))
                {
                    return -1;
                }

                return long.Parse(m_IdTextBox.Text);
            }
        }

        /// <summary>
        ///   Gets the owner id.
        /// </summary>
        /// <value>The owner id.</value>
        public Guid OwnerId
        {
            get
            {
                if (string.IsNullOrEmpty(m_OwnerIdTextBox.Text))
                {
                    return Guid.Empty;
                }

                return new Guid(m_OwnerIdTextBox.Text);
            }
        }

        /// <summary>
        ///   Gets the space.
        /// </summary>
        /// <value>The space.</value>
        public Guid Space
        {
            get
            {
                if (string.IsNullOrEmpty(m_SpaceTextBox.Text))
                {
                    return Guid.Empty;
                }

                return new Guid(m_SpaceTextBox.Text);
            }
        }

        /// <summary>
        ///   Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string FeatureName
        {
            get { return m_NameTextBox.Text; }
        }

        /// <summary>
        ///   Gets a value indicating whether this <see cref = "IFeatureStoreMethodArguments" /> is state.
        /// </summary>
        /// <value><c>true</c> if state; otherwise, <c>false</c>.</value>
        public bool State
        {
            get { return m_StateCheckBox.Checked; }
        }

        #endregion
    }
}