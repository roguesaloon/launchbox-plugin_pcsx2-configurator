using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCSX2_Configurator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadFromIniFile();

        }

        private void LoadFromIniFile()
        {
            useIndependantMemoryCardsCHK.Checked =
                IniFileHelper.ReadValue("PCSX2_Configurator", "UseIndependantMemoryCards", Class1.settingsFile) == "true";

            useCurrentFileSettingsCHK.Checked =
                IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentFileSettings", Class1.settingsFile) == "true";

            useCurrentWindowSettingsCHK.Checked =
                IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentWindowSettings", Class1.settingsFile) == "true";

            useCurrentLogSettingsCHK.Checked =
                IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentLogSettings", Class1.settingsFile) == "true";

            useCurrentFolderSettingsCHK.Checked =
                IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentFolderSettings", Class1.settingsFile) == "true";

            useCurrentVMSettingsCHK.Checked =
                IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentVMSettings", Class1.settingsFile) == "true";

            useCurrentGSdxPluginSettingsCHK.Checked =
                IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentGSdxPluginSettings", Class1.settingsFile) == "true";

            useCurrentLilyPadPluginSettingsCHK.Checked =
                IniFileHelper.ReadValue("PCSX2_Configurator", "UseCurrentLilyPadPluginSettings", Class1.settingsFile) == "true";

            configDirTXT.Text =
                IniFileHelper.ReadValue("PCSX2_Configurator", "ConfigsDirectoryPath", Class1.settingsFile);
        }

        private void WriteToIniFile()
        {
            IniFileHelper.WriteValue("PCSX2_Configurator", "UseIndependantMemoryCards", 
                useIndependantMemoryCardsCHK.Checked.ToString().ToLower(), Class1.settingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentFileSettings",
                useCurrentFileSettingsCHK.Checked.ToString().ToLower(), Class1.settingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentWindowSettings",
                useCurrentWindowSettingsCHK.Checked.ToString().ToLower(), Class1.settingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentLogSettings",
                useCurrentLogSettingsCHK.Checked.ToString().ToLower(), Class1.settingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentFolderSettings",
                useCurrentFolderSettingsCHK.Checked.ToString().ToLower(), Class1.settingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentVMSettings",
               useCurrentVMSettingsCHK.Checked.ToString().ToLower(), Class1.settingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentGSdxPluginSettings",
                useCurrentGSdxPluginSettingsCHK.Checked.ToString().ToLower(), Class1.settingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "UseCurrentLilyPadPluginSettings",
                useCurrentLilyPadPluginSettingsCHK.Checked.ToString().ToLower(), Class1.settingsFile);

            IniFileHelper.WriteValue("PCSX2_Configurator", "ConfigsDirectoryPath",
                configDirTXT.Text, Class1.settingsFile);
        }

        private void ApplyBTN_Click(object sender, EventArgs e)
        {
            WriteToIniFile();
        }

        private void configDirBTN_Click(object sender, EventArgs e)
        {
            if(configDirDLG.ShowDialog() == DialogResult.OK)
            {
                configDirTXT.Text = configDirDLG.SelectedPath;
            }
        }
    }
}
