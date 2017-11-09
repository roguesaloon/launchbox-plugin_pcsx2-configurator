using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace PCSX2_Configurator
{
    public partial class Form1 : Form
    {
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0XA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), e.ClipRectangle);
            e.Graphics.DrawImage(Image.FromFile(Class1.pluginDir + "\\Assets\\background.png"), new Rectangle(0, 0, 400, 400));
        }

        new protected void Close()
        {
            (this as Form).Close();

        }

        private static Image checkmark = Image.FromFile(Class1.pluginDir + "\\Assets\\checkmark.png");
        private static PrivateFontCollection privateFontCollection;

        public Form1()
        {
            
            InitializeComponent();
            LoadFont();
            LoadFromIniFile();
            this.Icon = Class1.EmulatorIcon();
         }

        private void LoadFont()
        {
            privateFontCollection = new PrivateFontCollection();
            privateFontCollection.AddFontFile(Class1.pluginDir + "\\Assets\\FixedsysExcelsiorAscii.ttf");

            var Fixedsys9 = new Font(privateFontCollection.Families[0], 9, FontStyle.Regular);
            var Fixedsys10 = new Font(privateFontCollection.Families[0], 10, FontStyle.Regular);

            useIndependantMemoryCardsLBL.Font = Fixedsys10;
            useFileSettingsLBL.Font = Fixedsys10;
            useWindowSettingsLBL.Font = Fixedsys10;
            useLogSettingsLBL.Font = Fixedsys10;
            useFolderSettingsLBL.Font = Fixedsys10;
            useVMSettingsLBL.Font = Fixedsys10;
            useGSdxPluginSettingsLBL.Font = Fixedsys10;
            useSPU2xPluginSettingsLBL.Font = Fixedsys10;
            useLilyPadPluginSettingsLBL.Font = Fixedsys10;
            configDirLBL.Font = Fixedsys9;
            configDirTXT.Font = Fixedsys9;
        }

        private void LoadFromIniFile()
        {
           if (IniFileHelper.ReadValue("PCSX2_Configurator", "UseIndependantMemoryCards", Class1.settingsFile) == "true")
                useIndependantMemoryCardsCHK.Image = checkmark;

            if (IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentFileSettings", Class1.settingsFile) == "true")
                useCurrentFileSettingsCHK.Image = checkmark;

            if (IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentWindowSettings", Class1.settingsFile) == "true")
                useCurrentWindowSettingsCHK.Image = checkmark;

            if (IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentLogSettings", Class1.settingsFile) == "true")
                useCurrentLogSettingsCHK.Image = checkmark;

            if (IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentFolderSettings", Class1.settingsFile) == "true")
                useCurrentFolderSettingsCHK.Image = checkmark;

            if(IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentVMSettings", Class1.settingsFile) == "true")
                useCurrentVMSettingsCHK.Image = checkmark;

            if(IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentGSdxPluginSettings", Class1.settingsFile) == "true")
                useCurrentGSdxPluginSettingsCHK.Image = checkmark;

            if (IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentSPU2xPluginSettings", Class1.settingsFile) == "true")
                useCurrentSPU2xPluginSettingsCHK.Image = checkmark;

            if (IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentLilyPadPluginSettings", Class1.settingsFile) == "true")
                useCurrentLilyPadPluginSettingsCHK.Image = checkmark;

            configDirTXT.Text =
                IniFileHelper.ReadValue("PCSX2_Configurator", "ConfigsDirectoryPath", Class1.settingsFile);
        }

        private void WriteToIniFile()
        {
            IniFileHelper.WriteValue("PCSX2_Configurator", "UseIndependantMemoryCards", 
                useIndependantMemoryCardsCHK.Image == checkmark ? "true" : "false", Class1.settingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentFileSettings",
                useCurrentFileSettingsCHK.Image == checkmark ? "true" : "false", Class1.settingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentWindowSettings",
                useCurrentWindowSettingsCHK.Image == checkmark ? "true" : "false", Class1.settingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentLogSettings",
                useCurrentLogSettingsCHK.Image == checkmark ? "true" : "false", Class1.settingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentFolderSettings",
                useCurrentFolderSettingsCHK.Image == checkmark ? "true" : "false", Class1.settingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentVMSettings",
               useCurrentVMSettingsCHK.Image == checkmark ? "true" : "false", Class1.settingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentGSdxPluginSettings",
                useCurrentGSdxPluginSettingsCHK.Image == checkmark ? "true" : "false", Class1.settingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentSPU2xPluginSettings",
                useCurrentSPU2xPluginSettingsCHK.Image == checkmark ? "true" : "false", Class1.settingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentLilyPadPluginSettings",
                useCurrentLilyPadPluginSettingsCHK.Image == checkmark ? "true" : "false", Class1.settingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "ConfigsDirectoryPath",
                configDirTXT.Text, Class1.settingsFile);
        }

        private void configDirBTN_Click(object sender, EventArgs e)
        {
            if (configDirDLG.ShowDialog() == DialogResult.OK)
            {
                configDirTXT.Text = configDirDLG.SelectedPath;
            }
        }

        private void closeBTN_Click(object sender, EventArgs e)
        {
            WriteToIniFile();
            Close();
        }

        private void useIndependantMemoryCardsCHK_Click(object sender, EventArgs e)
        {
            useIndependantMemoryCardsCHK.Image = (useIndependantMemoryCardsCHK.Image != checkmark) ?  checkmark : null;
        }

        private void useCurrentFileSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentFileSettingsCHK.Image = (useCurrentFileSettingsCHK.Image != checkmark) ? checkmark : null;
        }

        private void useCurrentWindowSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentWindowSettingsCHK.Image = (useCurrentWindowSettingsCHK.Image != checkmark) ? checkmark : null;
        }

        private void useCurrentLogSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentLogSettingsCHK.Image = (useCurrentLogSettingsCHK.Image != checkmark) ? checkmark : null;
        }

        private void useCurrentFolderSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentFolderSettingsCHK.Image = (useCurrentFolderSettingsCHK.Image != checkmark) ? checkmark : null;
        }

        private void useCurrentVMSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentVMSettingsCHK.Image = (useCurrentVMSettingsCHK.Image != checkmark) ? checkmark : null;
        }

        private void useCurrentGSdxPluginSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentGSdxPluginSettingsCHK.Image = (useCurrentGSdxPluginSettingsCHK.Image != checkmark) ? checkmark : null;
        }

        private void useCurrentSPU2xPluginSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentSPU2xPluginSettingsCHK.Image = (useCurrentSPU2xPluginSettingsCHK.Image != checkmark) ? checkmark : null;
        }

        private void useCurrentLilyPadPluginSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentLilyPadPluginSettingsCHK.Image = (useCurrentLilyPadPluginSettingsCHK.Image != checkmark) ? checkmark : null;
        }


    }
}
