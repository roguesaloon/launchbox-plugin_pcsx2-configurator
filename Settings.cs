using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using IniParser;
using Unbroken.LaunchBox.Plugins;

namespace PCSX2_Configurator
{
    public partial class SettingsForm : Form
    {
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left) return;
            Capture = false;
            var msg = Message.Create(Handle, 0XA1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref msg);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(BackColor), e.ClipRectangle);
            e.Graphics.DrawImage(Image.FromFile(Utilities.PluginDirectory + "\\Assets\\background.png"), new Rectangle(0, 0, Width, Height));
        }

        private static Image _checkmark;
        private static PrivateFontCollection _privateFontCollection;

        public SettingsForm()
        {
            InitializeComponent();

            _checkmark = new Bitmap(Image.FromFile(Utilities.PluginDirectory + "\\Assets\\checkmark.png"), new Size(16 * Width / 400, 16 * Height / 400));
            LoadFont();
            LoadFromIniFile();
            Icon = Utilities.EmulatorIcon;
         }

        private void LoadFont()
        {
            _privateFontCollection = new PrivateFontCollection();
            _privateFontCollection.AddFontFile(Utilities.PluginDirectory + "\\Assets\\FixedsysExcelsiorAscii.ttf");

            var fixedsys9 = new Font(_privateFontCollection.Families[0], 9, FontStyle.Regular);
            var fixedsys10 = new Font(_privateFontCollection.Families[0], 10, FontStyle.Regular);

            useIndependantMemoryCardsLBL.Font = fixedsys10;
            useFileSettingsLBL.Font = fixedsys10;
            useWindowSettingsLBL.Font = fixedsys10;
            useLogSettingsLBL.Font = fixedsys10;
            useFolderSettingsLBL.Font = fixedsys10;
            useVMSettingsLBL.Font = fixedsys10;
            useGSdxPluginSettingsLBL.Font = fixedsys10;
            useSPU2xPluginSettingsLBL.Font = fixedsys10;
            useLilyPadPluginSettingsLBL.Font = fixedsys10;
            configDirLBL.Font = fixedsys9;
            configDirTXT.Font = fixedsys9;
        }

        private void LoadFromIniFile()
        {
            var iniParser = new FileIniDataParser();
            var pluginSettings = iniParser.ReadFile(Utilities.SettingsFile)["PCSX2_Configurator"];

            if (bool.Parse(pluginSettings["UseIndependantMemoryCards"]))
                useIndependantMemoryCardsCHK.Image = _checkmark;

            if (bool.Parse(pluginSettings["UseCurrentFileSettings"]))
                useCurrentFileSettingsCHK.Image = _checkmark;

            if (bool.Parse(pluginSettings["UseCurrentWindowSettings"]))
                useCurrentWindowSettingsCHK.Image = _checkmark;

            if (bool.Parse(pluginSettings["UseCurrentLogSettings"]))
                useCurrentLogSettingsCHK.Image = _checkmark;

            if (bool.Parse(pluginSettings["UseCurrentFolderSettings"]))
                useCurrentFolderSettingsCHK.Image = _checkmark;

            if (bool.Parse(pluginSettings["UseCurrentVMSettings"]))
                useCurrentVMSettingsCHK.Image = _checkmark;

            if (bool.Parse(pluginSettings["UseCurrentGSdxPluginSettings"]))
                useCurrentGSdxPluginSettingsCHK.Image = _checkmark;

            if (bool.Parse(pluginSettings["UseCurrentSPU2xPluginSettings"]))
                useCurrentSPU2xPluginSettingsCHK.Image = _checkmark;

            if (bool.Parse(pluginSettings["UseCurrentLilyPadPluginSettings"]))
                useCurrentLilyPadPluginSettingsCHK.Image = _checkmark;

            configDirTXT.Text = pluginSettings["ConfigsDirectoryPath"];
        }

        private void WriteToIniFile()
        {
            var iniParser = new FileIniDataParser();
            var pluginSettings = iniParser.ReadFile(Utilities.SettingsFile);

            pluginSettings["PCSX2_Configurator"]["UseIndependantMemoryCards"] = (useIndependantMemoryCardsCHK.Image == _checkmark).ToString();
            pluginSettings["PCSX2_Configurator"]["UseCurrentFileSettings"] = (useCurrentFileSettingsCHK.Image == _checkmark).ToString();
            pluginSettings["PCSX2_Configurator"]["UseCurrentWindowSettings"] = (useCurrentWindowSettingsCHK.Image == _checkmark).ToString();
            pluginSettings["PCSX2_Configurator"]["UseCurrentLogSettings"] = (useCurrentLogSettingsCHK.Image == _checkmark).ToString();
            pluginSettings["PCSX2_Configurator"]["AllowAllSettings"] = (useCurrentFolderSettingsCHK.Image == _checkmark).ToString();
            pluginSettings["PCSX2_Configurator"]["UseCurrentFolderSettings"] = (useCurrentWindowSettingsCHK.Image == _checkmark).ToString();
            pluginSettings["PCSX2_Configurator"]["UseCurrentVMSettings"] = (useCurrentVMSettingsCHK.Image == _checkmark).ToString();
            pluginSettings["PCSX2_Configurator"]["UseCurrentGSdxPluginSettings"] = (useCurrentGSdxPluginSettingsCHK.Image == _checkmark).ToString();
            pluginSettings["PCSX2_Configurator"]["UseCurrentLilyPadPluginSettings"] = (useCurrentLilyPadPluginSettingsCHK.Image == _checkmark).ToString();
            pluginSettings["PCSX2_Configurator"]["UseCurrentSPU2xPluginSettings"] = (useCurrentSPU2xPluginSettingsCHK.Image == _checkmark).ToString();
            pluginSettings["PCSX2_Configurator"]["ConfigsDirectoryPath"] = configDirTXT.Text;

            iniParser.WriteFile(Utilities.SettingsFile, pluginSettings);
        }

        private void ConfigDirBTN_Click(object sender, EventArgs e)
        {
            if (configDirDLG.ShowDialog() == DialogResult.OK)
            {
                configDirTXT.Text = configDirDLG.SelectedPath;
            }
        }

        private void CloseBTN_Click(object sender, EventArgs e)
        {
            WriteToIniFile();
            Close();
        }

        private void UseIndependantMemoryCardsCHK_Click(object sender, EventArgs e)
        {
            useIndependantMemoryCardsCHK.Image = (useIndependantMemoryCardsCHK.Image != _checkmark) ?  _checkmark : null;
        }

        private void UseCurrentFileSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentFileSettingsCHK.Image = (useCurrentFileSettingsCHK.Image != _checkmark) ? _checkmark : null;
        }

        private void UseCurrentWindowSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentWindowSettingsCHK.Image = (useCurrentWindowSettingsCHK.Image != _checkmark) ? _checkmark : null;
        }

        private void UseCurrentLogSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentLogSettingsCHK.Image = (useCurrentLogSettingsCHK.Image != _checkmark) ? _checkmark : null;
        }

        private void UseCurrentFolderSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentFolderSettingsCHK.Image = (useCurrentFolderSettingsCHK.Image != _checkmark) ? _checkmark : null;
        }

        private void UseCurrentVMSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentVMSettingsCHK.Image = (useCurrentVMSettingsCHK.Image != _checkmark) ? _checkmark : null;
        }

        private void UseCurrentGSdxPluginSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentGSdxPluginSettingsCHK.Image = (useCurrentGSdxPluginSettingsCHK.Image != _checkmark) ? _checkmark : null;
        }

        private void UseCurrentSPU2xPluginSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentSPU2xPluginSettingsCHK.Image = (useCurrentSPU2xPluginSettingsCHK.Image != _checkmark) ? _checkmark : null;
        }

        private void UseCurrentLilyPadPluginSettingsCHK_Click(object sender, EventArgs e)
        {
            useCurrentLilyPadPluginSettingsCHK.Image = (useCurrentLilyPadPluginSettingsCHK.Image != _checkmark) ? _checkmark : null;
        }
    }

    public class SettingsPlugin : ISystemMenuItemPlugin
    {
        private static SettingsForm _settingsForm;

        public string Caption => "PCSX2 Configurator Settings";

        public Image IconImage => Utilities.EmulatorIcon?.ToBitmap();

        public bool ShowInLaunchBox => true;

        public bool ShowInBigBox => false;

        public bool AllowInBigBoxWhenLocked => ShowInBigBox;

        public void OnSelected()
        {
            if (Utilities.FullEmulatorPath == null)
            {
                MessageBox.Show($@"PCSX2 Could Not Not Be Found{Environment.NewLine}Please Add PCSX2 as an Emulator and Restart LaunchBox", @"PCSX2 Configurator");
            }
            else
            {
                _settingsForm?.Close();
                _settingsForm = new SettingsForm { StartPosition = FormStartPosition.Manual };
                _settingsForm.Location = new Point(
                    PluginHelper.LaunchBoxMainForm.Location.X + (int)((PluginHelper.LaunchBoxMainForm.Width - _settingsForm.Width) * 0.5f),
                    PluginHelper.LaunchBoxMainForm.Location.Y + (int)((PluginHelper.LaunchBoxMainForm.Height - _settingsForm.Height) * 0.5f));
                _settingsForm.Show();
            }
        }
    }
}
