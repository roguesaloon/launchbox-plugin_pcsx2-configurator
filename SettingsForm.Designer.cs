using System.Drawing;

namespace PCSX2_Configurator
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.configDirDLG = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.closeBTN = new System.Windows.Forms.PictureBox();
            this.useIndependantMemoryCardsLBL = new System.Windows.Forms.Label();
            this.useFileSettingsLBL = new System.Windows.Forms.Label();
            this.useWindowSettingsLBL = new System.Windows.Forms.Label();
            this.useLogSettingsLBL = new System.Windows.Forms.Label();
            this.useFolderSettingsLBL = new System.Windows.Forms.Label();
            this.useVMSettingsLBL = new System.Windows.Forms.Label();
            this.useGSdxPluginSettingsLBL = new System.Windows.Forms.Label();
            this.useSPU2xPluginSettingsLBL = new System.Windows.Forms.Label();
            this.useLilyPadPluginSettingsLBL = new System.Windows.Forms.Label();
            this.configDirBTN = new System.Windows.Forms.PictureBox();
            this.useIndependantMemoryCardsCHK = new System.Windows.Forms.PictureBox();
            this.useCurrentFileSettingsCHK = new System.Windows.Forms.PictureBox();
            this.useCurrentWindowSettingsCHK = new System.Windows.Forms.PictureBox();
            this.useCurrentLogSettingsCHK = new System.Windows.Forms.PictureBox();
            this.useCurrentFolderSettingsCHK = new System.Windows.Forms.PictureBox();
            this.useCurrentVMSettingsCHK = new System.Windows.Forms.PictureBox();
            this.useCurrentGSdxPluginSettingsCHK = new System.Windows.Forms.PictureBox();
            this.useCurrentSPU2xPluginSettingsCHK = new System.Windows.Forms.PictureBox();
            this.useCurrentLilyPadPluginSettingsCHK = new System.Windows.Forms.PictureBox();
            this.configDirLBL = new System.Windows.Forms.GroupBox();
            this.configDirTXT = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.closeBTN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.configDirBTN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.useIndependantMemoryCardsCHK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.useCurrentFileSettingsCHK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.useCurrentWindowSettingsCHK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.useCurrentLogSettingsCHK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.useCurrentFolderSettingsCHK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.useCurrentVMSettingsCHK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.useCurrentGSdxPluginSettingsCHK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.useCurrentSPU2xPluginSettingsCHK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.useCurrentLilyPadPluginSettingsCHK)).BeginInit();
            this.configDirLBL.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeBTN
            // 
            this.closeBTN.BackColor = System.Drawing.Color.Transparent;
            this.closeBTN.ErrorImage = null;
            this.closeBTN.InitialImage = null;
            this.closeBTN.Location = new System.Drawing.Point(332, 19);
            this.closeBTN.Name = "closeBTN";
            this.closeBTN.Size = new System.Drawing.Size(34, 35);
            this.closeBTN.TabIndex = 3;
            this.closeBTN.TabStop = false;
            this.closeBTN.Click += new System.EventHandler(this.closeBTN_Click);
            // 
            // useIndependantMemoryCardsLBL
            // 
            this.useIndependantMemoryCardsLBL.AutoSize = true;
            this.useIndependantMemoryCardsLBL.BackColor = System.Drawing.Color.Transparent;
            this.useIndependantMemoryCardsLBL.Font = new System.Drawing.Font("Fixedsys Excelsior 3.01", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useIndependantMemoryCardsLBL.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.useIndependantMemoryCardsLBL.Location = new System.Drawing.Point(85, 28);
            this.useIndependantMemoryCardsLBL.Name = "useIndependantMemoryCardsLBL";
            this.useIndependantMemoryCardsLBL.Size = new System.Drawing.Size(210, 15);
            this.useIndependantMemoryCardsLBL.TabIndex = 4;
            this.useIndependantMemoryCardsLBL.Text = "Use Independant Memory Cards?";
            this.toolTips.SetToolTip(this.useIndependantMemoryCardsLBL, "A Seperate Memory Card for Each Game");
            // 
            // useFileSettingsLBL
            // 
            this.useFileSettingsLBL.AutoSize = true;
            this.useFileSettingsLBL.BackColor = System.Drawing.Color.Transparent;
            this.useFileSettingsLBL.Font = new System.Drawing.Font("Fixedsys Excelsior 3.01", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useFileSettingsLBL.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.useFileSettingsLBL.Location = new System.Drawing.Point(85, 62);
            this.useFileSettingsLBL.Name = "useFileSettingsLBL";
            this.useFileSettingsLBL.Size = new System.Drawing.Size(231, 15);
            this.useFileSettingsLBL.TabIndex = 5;
            this.useFileSettingsLBL.Text = "Use Current Plugin && Bios Files?";
            this.toolTips.SetToolTip(this.useFileSettingsLBL, "Keep Plugin & Bios Settings");
            // 
            // useWindowSettingsLBL
            // 
            this.useWindowSettingsLBL.AutoSize = true;
            this.useWindowSettingsLBL.BackColor = System.Drawing.Color.Transparent;
            this.useWindowSettingsLBL.Font = new System.Drawing.Font("Fixedsys Excelsior 3.01", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useWindowSettingsLBL.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.useWindowSettingsLBL.Location = new System.Drawing.Point(85, 98);
            this.useWindowSettingsLBL.Name = "useWindowSettingsLBL";
            this.useWindowSettingsLBL.Size = new System.Drawing.Size(224, 15);
            this.useWindowSettingsLBL.TabIndex = 6;
            this.useWindowSettingsLBL.Text = "Use Current GS Window Settings?";
            this.toolTips.SetToolTip(this.useWindowSettingsLBL, "Keep Vsync/Ratio/Size Settings");
            // 
            // useLogSettingsLBL
            // 
            this.useLogSettingsLBL.AutoSize = true;
            this.useLogSettingsLBL.BackColor = System.Drawing.Color.Transparent;
            this.useLogSettingsLBL.Font = new System.Drawing.Font("Fixedsys Excelsior 3.01", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useLogSettingsLBL.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.useLogSettingsLBL.Location = new System.Drawing.Point(85, 134);
            this.useLogSettingsLBL.Name = "useLogSettingsLBL";
            this.useLogSettingsLBL.Size = new System.Drawing.Size(238, 15);
            this.useLogSettingsLBL.TabIndex = 7;
            this.useLogSettingsLBL.Text = "Use Current Program Log Settings?";
            this.toolTips.SetToolTip(this.useLogSettingsLBL, "Keep Debug/Log Settings (Show/Hide Console)");
            // 
            // useFolderSettingsLBL
            // 
            this.useFolderSettingsLBL.AutoSize = true;
            this.useFolderSettingsLBL.BackColor = System.Drawing.Color.Transparent;
            this.useFolderSettingsLBL.Font = new System.Drawing.Font("Fixedsys Excelsior 3.01", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useFolderSettingsLBL.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.useFolderSettingsLBL.Location = new System.Drawing.Point(85, 168);
            this.useFolderSettingsLBL.Name = "useFolderSettingsLBL";
            this.useFolderSettingsLBL.Size = new System.Drawing.Size(203, 15);
            this.useFolderSettingsLBL.TabIndex = 8;
            this.useFolderSettingsLBL.Text = "Use Current Folder Settings?";
            this.toolTips.SetToolTip(this.useFolderSettingsLBL, "Keep Folder Settings (for Custom Folders)");
            // 
            // useVMSettingsLBL
            // 
            this.useVMSettingsLBL.AutoSize = true;
            this.useVMSettingsLBL.BackColor = System.Drawing.Color.Transparent;
            this.useVMSettingsLBL.Font = new System.Drawing.Font("Fixedsys Excelsior 3.01", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useVMSettingsLBL.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.useVMSettingsLBL.Location = new System.Drawing.Point(85, 206);
            this.useVMSettingsLBL.Name = "useVMSettingsLBL";
            this.useVMSettingsLBL.Size = new System.Drawing.Size(175, 15);
            this.useVMSettingsLBL.TabIndex = 9;
            this.useVMSettingsLBL.Text = "Use Current VM Settings?";
            this.toolTips.SetToolTip(this.useVMSettingsLBL, "Keep Hack/Cheat/Patch Settings (May be Overidden)");
            // 
            // useGSdxPluginSettingsLBL
            // 
            this.useGSdxPluginSettingsLBL.AutoSize = true;
            this.useGSdxPluginSettingsLBL.BackColor = System.Drawing.Color.Transparent;
            this.useGSdxPluginSettingsLBL.Font = new System.Drawing.Font("Fixedsys Excelsior 3.01", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useGSdxPluginSettingsLBL.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.useGSdxPluginSettingsLBL.Location = new System.Drawing.Point(85, 240);
            this.useGSdxPluginSettingsLBL.Name = "useGSdxPluginSettingsLBL";
            this.useGSdxPluginSettingsLBL.Size = new System.Drawing.Size(238, 15);
            this.useGSdxPluginSettingsLBL.TabIndex = 10;
            this.useGSdxPluginSettingsLBL.Text = "Use Current GSdx Plugin Settings?";
            this.toolTips.SetToolTip(this.useGSdxPluginSettingsLBL, "Keep Graphics Settings (May be Overridden)");
            // 
            // useSPU2xPluginSettingsLBL
            // 
            this.useSPU2xPluginSettingsLBL.AutoSize = true;
            this.useSPU2xPluginSettingsLBL.BackColor = System.Drawing.Color.Transparent;
            this.useSPU2xPluginSettingsLBL.Font = new System.Drawing.Font("Fixedsys Excelsior 3.01", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useSPU2xPluginSettingsLBL.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.useSPU2xPluginSettingsLBL.Location = new System.Drawing.Point(85, 278);
            this.useSPU2xPluginSettingsLBL.Name = "useSPU2xPluginSettingsLBL";
            this.useSPU2xPluginSettingsLBL.Size = new System.Drawing.Size(252, 15);
            this.useSPU2xPluginSettingsLBL.TabIndex = 11;
            this.useSPU2xPluginSettingsLBL.Text = "Use Current SPU2-X Plugin Settings?";
            this.toolTips.SetToolTip(this.useSPU2xPluginSettingsLBL, "Keep Sound Settings (May Be Overridden)");
            // 
            // useLilyPadPluginSettingsLBL
            // 
            this.useLilyPadPluginSettingsLBL.AutoSize = true;
            this.useLilyPadPluginSettingsLBL.BackColor = System.Drawing.Color.Transparent;
            this.useLilyPadPluginSettingsLBL.Font = new System.Drawing.Font("Fixedsys Excelsior 3.01", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useLilyPadPluginSettingsLBL.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.useLilyPadPluginSettingsLBL.Location = new System.Drawing.Point(85, 312);
            this.useLilyPadPluginSettingsLBL.Name = "useLilyPadPluginSettingsLBL";
            this.useLilyPadPluginSettingsLBL.Size = new System.Drawing.Size(259, 15);
            this.useLilyPadPluginSettingsLBL.TabIndex = 12;
            this.useLilyPadPluginSettingsLBL.Text = "Use Current LilyPad Plugin Settings?";
            this.toolTips.SetToolTip(this.useLilyPadPluginSettingsLBL, "Keep Controller/Input Settings");
            // 
            // configDirBTN
            // 
            this.configDirBTN.BackColor = System.Drawing.Color.Transparent;
            this.configDirBTN.ErrorImage = null;
            this.configDirBTN.InitialImage = null;
            this.configDirBTN.Location = new System.Drawing.Point(294, 346);
            this.configDirBTN.Name = "configDirBTN";
            this.configDirBTN.Size = new System.Drawing.Size(57, 35);
            this.configDirBTN.TabIndex = 13;
            this.configDirBTN.TabStop = false;
            this.configDirBTN.Click += new System.EventHandler(this.configDirBTN_Click);
            // 
            // useIndependantMemoryCardsCHK
            // 
            this.useIndependantMemoryCardsCHK.BackColor = System.Drawing.Color.Transparent;
            this.useIndependantMemoryCardsCHK.ErrorImage = null;
            this.useIndependantMemoryCardsCHK.InitialImage = null;
            this.useIndependantMemoryCardsCHK.Location = new System.Drawing.Point(63, 29);
            this.useIndependantMemoryCardsCHK.Name = "useIndependantMemoryCardsCHK";
            this.useIndependantMemoryCardsCHK.Size = new System.Drawing.Size(16, 16);
            this.useIndependantMemoryCardsCHK.TabIndex = 14;
            this.useIndependantMemoryCardsCHK.TabStop = false;
            this.useIndependantMemoryCardsCHK.Click += new System.EventHandler(this.useIndependantMemoryCardsCHK_Click);
            // 
            // useCurrentFileSettingsCHK
            // 
            this.useCurrentFileSettingsCHK.BackColor = System.Drawing.Color.Transparent;
            this.useCurrentFileSettingsCHK.ErrorImage = null;
            this.useCurrentFileSettingsCHK.InitialImage = null;
            this.useCurrentFileSettingsCHK.Location = new System.Drawing.Point(63, 61);
            this.useCurrentFileSettingsCHK.Name = "useCurrentFileSettingsCHK";
            this.useCurrentFileSettingsCHK.Size = new System.Drawing.Size(16, 16);
            this.useCurrentFileSettingsCHK.TabIndex = 15;
            this.useCurrentFileSettingsCHK.TabStop = false;
            this.useCurrentFileSettingsCHK.Click += new System.EventHandler(this.useCurrentFileSettingsCHK_Click);
            // 
            // useCurrentWindowSettingsCHK
            // 
            this.useCurrentWindowSettingsCHK.BackColor = System.Drawing.Color.Transparent;
            this.useCurrentWindowSettingsCHK.ErrorImage = null;
            this.useCurrentWindowSettingsCHK.InitialImage = null;
            this.useCurrentWindowSettingsCHK.Location = new System.Drawing.Point(63, 97);
            this.useCurrentWindowSettingsCHK.Name = "useCurrentWindowSettingsCHK";
            this.useCurrentWindowSettingsCHK.Size = new System.Drawing.Size(16, 16);
            this.useCurrentWindowSettingsCHK.TabIndex = 16;
            this.useCurrentWindowSettingsCHK.TabStop = false;
            this.useCurrentWindowSettingsCHK.Click += new System.EventHandler(this.useCurrentWindowSettingsCHK_Click);
            // 
            // useCurrentLogSettingsCHK
            // 
            this.useCurrentLogSettingsCHK.BackColor = System.Drawing.Color.Transparent;
            this.useCurrentLogSettingsCHK.ErrorImage = null;
            this.useCurrentLogSettingsCHK.InitialImage = null;
            this.useCurrentLogSettingsCHK.Location = new System.Drawing.Point(63, 133);
            this.useCurrentLogSettingsCHK.Name = "useCurrentLogSettingsCHK";
            this.useCurrentLogSettingsCHK.Size = new System.Drawing.Size(16, 16);
            this.useCurrentLogSettingsCHK.TabIndex = 17;
            this.useCurrentLogSettingsCHK.TabStop = false;
            this.useCurrentLogSettingsCHK.Click += new System.EventHandler(this.useCurrentLogSettingsCHK_Click);
            // 
            // useCurrentFolderSettingsCHK
            // 
            this.useCurrentFolderSettingsCHK.BackColor = System.Drawing.Color.Transparent;
            this.useCurrentFolderSettingsCHK.ErrorImage = null;
            this.useCurrentFolderSettingsCHK.InitialImage = null;
            this.useCurrentFolderSettingsCHK.Location = new System.Drawing.Point(63, 169);
            this.useCurrentFolderSettingsCHK.Name = "useCurrentFolderSettingsCHK";
            this.useCurrentFolderSettingsCHK.Size = new System.Drawing.Size(16, 16);
            this.useCurrentFolderSettingsCHK.TabIndex = 18;
            this.useCurrentFolderSettingsCHK.TabStop = false;
            this.useCurrentFolderSettingsCHK.Click += new System.EventHandler(this.useCurrentFolderSettingsCHK_Click);
            // 
            // useCurrentVMSettingsCHK
            // 
            this.useCurrentVMSettingsCHK.BackColor = System.Drawing.Color.Transparent;
            this.useCurrentVMSettingsCHK.ErrorImage = null;
            this.useCurrentVMSettingsCHK.InitialImage = null;
            this.useCurrentVMSettingsCHK.Location = new System.Drawing.Point(63, 205);
            this.useCurrentVMSettingsCHK.Name = "useCurrentVMSettingsCHK";
            this.useCurrentVMSettingsCHK.Size = new System.Drawing.Size(16, 16);
            this.useCurrentVMSettingsCHK.TabIndex = 19;
            this.useCurrentVMSettingsCHK.TabStop = false;
            this.useCurrentVMSettingsCHK.Click += new System.EventHandler(this.useCurrentVMSettingsCHK_Click);
            // 
            // useCurrentGSdxPluginSettingsCHK
            // 
            this.useCurrentGSdxPluginSettingsCHK.BackColor = System.Drawing.Color.Transparent;
            this.useCurrentGSdxPluginSettingsCHK.ErrorImage = null;
            this.useCurrentGSdxPluginSettingsCHK.InitialImage = null;
            this.useCurrentGSdxPluginSettingsCHK.Location = new System.Drawing.Point(63, 241);
            this.useCurrentGSdxPluginSettingsCHK.Name = "useCurrentGSdxPluginSettingsCHK";
            this.useCurrentGSdxPluginSettingsCHK.Size = new System.Drawing.Size(16, 16);
            this.useCurrentGSdxPluginSettingsCHK.TabIndex = 20;
            this.useCurrentGSdxPluginSettingsCHK.TabStop = false;
            this.useCurrentGSdxPluginSettingsCHK.Click += new System.EventHandler(this.useCurrentGSdxPluginSettingsCHK_Click);
            // 
            // useCurrentSPU2xPluginSettingsCHK
            // 
            this.useCurrentSPU2xPluginSettingsCHK.BackColor = System.Drawing.Color.Transparent;
            this.useCurrentSPU2xPluginSettingsCHK.ErrorImage = null;
            this.useCurrentSPU2xPluginSettingsCHK.InitialImage = null;
            this.useCurrentSPU2xPluginSettingsCHK.Location = new System.Drawing.Point(63, 277);
            this.useCurrentSPU2xPluginSettingsCHK.Name = "useCurrentSPU2xPluginSettingsCHK";
            this.useCurrentSPU2xPluginSettingsCHK.Size = new System.Drawing.Size(16, 16);
            this.useCurrentSPU2xPluginSettingsCHK.TabIndex = 21;
            this.useCurrentSPU2xPluginSettingsCHK.TabStop = false;
            this.useCurrentSPU2xPluginSettingsCHK.Click += new System.EventHandler(this.useCurrentSPU2xPluginSettingsCHK_Click);
            // 
            // useCurrentLilyPadPluginSettingsCHK
            // 
            this.useCurrentLilyPadPluginSettingsCHK.BackColor = System.Drawing.Color.Transparent;
            this.useCurrentLilyPadPluginSettingsCHK.ErrorImage = null;
            this.useCurrentLilyPadPluginSettingsCHK.InitialImage = null;
            this.useCurrentLilyPadPluginSettingsCHK.Location = new System.Drawing.Point(63, 313);
            this.useCurrentLilyPadPluginSettingsCHK.Name = "useCurrentLilyPadPluginSettingsCHK";
            this.useCurrentLilyPadPluginSettingsCHK.Size = new System.Drawing.Size(16, 16);
            this.useCurrentLilyPadPluginSettingsCHK.TabIndex = 22;
            this.useCurrentLilyPadPluginSettingsCHK.TabStop = false;
            this.useCurrentLilyPadPluginSettingsCHK.Click += new System.EventHandler(this.useCurrentLilyPadPluginSettingsCHK_Click);
            // 
            // configDirLBL
            // 
            this.configDirLBL.BackColor = System.Drawing.Color.Transparent;
            this.configDirLBL.Controls.Add(this.configDirTXT);
            this.configDirLBL.Font = new System.Drawing.Font("Fixedsys Excelsior 3.01", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.configDirLBL.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.configDirLBL.Location = new System.Drawing.Point(88, 342);
            this.configDirLBL.Name = "configDirLBL";
            this.configDirLBL.Size = new System.Drawing.Size(200, 39);
            this.configDirLBL.TabIndex = 23;
            this.configDirLBL.TabStop = false;
            this.configDirLBL.Text = "Configurations Directory";
            this.toolTips.SetToolTip(this.configDirLBL, "Store per-game Configs in Custom Directory");
            // 
            // configDirTXT
            // 
            this.configDirTXT.AutoSize = true;
            this.configDirTXT.Location = new System.Drawing.Point(7, 20);
            this.configDirTXT.Name = "configDirTXT";
            this.configDirTXT.Size = new System.Drawing.Size(37, 13);
            this.configDirTXT.TabIndex = 0;
            this.configDirTXT.Text = "label";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(30)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Controls.Add(this.configDirLBL);
            this.Controls.Add(this.useCurrentLilyPadPluginSettingsCHK);
            this.Controls.Add(this.useCurrentSPU2xPluginSettingsCHK);
            this.Controls.Add(this.useCurrentGSdxPluginSettingsCHK);
            this.Controls.Add(this.useCurrentVMSettingsCHK);
            this.Controls.Add(this.useCurrentFolderSettingsCHK);
            this.Controls.Add(this.useCurrentLogSettingsCHK);
            this.Controls.Add(this.useCurrentWindowSettingsCHK);
            this.Controls.Add(this.useCurrentFileSettingsCHK);
            this.Controls.Add(this.useIndependantMemoryCardsCHK);
            this.Controls.Add(this.configDirBTN);
            this.Controls.Add(this.useLilyPadPluginSettingsLBL);
            this.Controls.Add(this.useSPU2xPluginSettingsLBL);
            this.Controls.Add(this.useGSdxPluginSettingsLBL);
            this.Controls.Add(this.useVMSettingsLBL);
            this.Controls.Add(this.useFolderSettingsLBL);
            this.Controls.Add(this.useLogSettingsLBL);
            this.Controls.Add(this.useWindowSettingsLBL);
            this.Controls.Add(this.useFileSettingsLBL);
            this.Controls.Add(this.useIndependantMemoryCardsLBL);
            this.Controls.Add(this.closeBTN);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(400, 400);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "Form1";
            this.Text = "PCSX2 Configurator";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(30)))), ((int)(((byte)(50)))));
            ((System.ComponentModel.ISupportInitialize)(this.closeBTN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.configDirBTN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.useIndependantMemoryCardsCHK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.useCurrentFileSettingsCHK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.useCurrentWindowSettingsCHK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.useCurrentLogSettingsCHK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.useCurrentFolderSettingsCHK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.useCurrentVMSettingsCHK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.useCurrentGSdxPluginSettingsCHK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.useCurrentSPU2xPluginSettingsCHK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.useCurrentLilyPadPluginSettingsCHK)).EndInit();
            this.configDirLBL.ResumeLayout(false);
            this.configDirLBL.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog configDirDLG;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.PictureBox closeBTN;
        private System.Windows.Forms.Label useIndependantMemoryCardsLBL;
        private System.Windows.Forms.Label useFileSettingsLBL;
        private System.Windows.Forms.Label useWindowSettingsLBL;
        private System.Windows.Forms.Label useLogSettingsLBL;
        private System.Windows.Forms.Label useFolderSettingsLBL;
        private System.Windows.Forms.Label useVMSettingsLBL;
        private System.Windows.Forms.Label useGSdxPluginSettingsLBL;
        private System.Windows.Forms.Label useSPU2xPluginSettingsLBL;
        private System.Windows.Forms.Label useLilyPadPluginSettingsLBL;
        private System.Windows.Forms.PictureBox configDirBTN;
        private System.Windows.Forms.PictureBox useIndependantMemoryCardsCHK;
        private System.Windows.Forms.PictureBox useCurrentFileSettingsCHK;
        private System.Windows.Forms.PictureBox useCurrentWindowSettingsCHK;
        private System.Windows.Forms.PictureBox useCurrentLogSettingsCHK;
        private System.Windows.Forms.PictureBox useCurrentFolderSettingsCHK;
        private System.Windows.Forms.PictureBox useCurrentVMSettingsCHK;
        private System.Windows.Forms.PictureBox useCurrentGSdxPluginSettingsCHK;
        private System.Windows.Forms.PictureBox useCurrentSPU2xPluginSettingsCHK;
        private System.Windows.Forms.PictureBox useCurrentLilyPadPluginSettingsCHK;
        private System.Windows.Forms.GroupBox configDirLBL;
        private System.Windows.Forms.Label configDirTXT;
    }
}