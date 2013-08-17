namespace FeatureStoreServiceSmokeTester.Controls
{
    using System.Windows.Forms;
    using Resources;

    /// <summary>
    ///   SUMMARY
    /// </summary>
    public class HelpTabPage : TabPage
    {
        public HelpTabPage() : base(ControlTextsResource.HELP_TAB_PAGE_TEXT)
        {
        }

        protected override void InitLayout()
        {
            RichTextBox richTextBox = new RichTextBox();
            richTextBox.Dock = DockStyle.Fill;
            richTextBox.Rtf =
                @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fnil\fcharset0 Calibri;}}
{\colortbl ;\red0\green176\blue80;\red79\green129\blue189;}
{\*\generator Msftedit 5.41.21.2509;}\viewkind4\uc1\pard\sa200\sl276\slmult1\cf1\lang9\b\f0\fs32 Feature Store Service\par
\cf2\fs24 Smoke Testing Application\cf0\b0\fs22\par
Guidance\par
}";
            richTextBox.Visible = true;
            Controls.Add(richTextBox);

            Dock = DockStyle.Fill;
            base.InitLayout();
        }
    }
}