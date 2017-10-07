using System.Drawing;

namespace PCSX2_Configurator
{
    partial class Form1
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.useIndependantMemoryCardsCHK = new System.Windows.Forms.CheckBox();
            this.useCurrentFileSettingsCHK = new System.Windows.Forms.CheckBox();
            this.useCurrentWindowSettingsCHK = new System.Windows.Forms.CheckBox();
            this.useCurrentLogSettingsCHK = new System.Windows.Forms.CheckBox();
            this.useCurrentFolderSettingsCHK = new System.Windows.Forms.CheckBox();
            this.useIndependantMemoryCardsLBL = new System.Windows.Forms.Label();
            this.useCurrentFileSettingsLBL = new System.Windows.Forms.Label();
            this.useCurrentWindowSettingsLBL = new System.Windows.Forms.Label();
            this.useCurrentLogSettingsLBL = new System.Windows.Forms.Label();
            this.useCurrentFolderSettingsLBL = new System.Windows.Forms.Label();
            this.useCurrentVMSettingsLBL = new System.Windows.Forms.Label();
            this.useCurrentVMSettingsCHK = new System.Windows.Forms.CheckBox();
            this.useCurrentGSdxPluginSettingsLBL = new System.Windows.Forms.Label();
            this.useCurrentGSdxPluginSettingsCHK = new System.Windows.Forms.CheckBox();
            this.useCurrentLilyPadPluginSettingsLBL = new System.Windows.Forms.Label();
            this.useCurrentLilyPadPluginSettingsCHK = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.configDirLBL = new System.Windows.Forms.Label();
            this.configDirBTN = new System.Windows.Forms.Button();
            this.configDirTXT = new System.Windows.Forms.TextBox();
            this.ApplyBTN = new System.Windows.Forms.Button();
            this.configDirDLG = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.useIndependantMemoryCardsCHK, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.useCurrentFileSettingsCHK, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.useCurrentWindowSettingsCHK, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.useCurrentLogSettingsCHK, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.useCurrentFolderSettingsCHK, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.useIndependantMemoryCardsLBL, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.useCurrentFileSettingsLBL, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.useCurrentWindowSettingsLBL, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.useCurrentLogSettingsLBL, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.useCurrentFolderSettingsLBL, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.useCurrentVMSettingsLBL, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.useCurrentVMSettingsCHK, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.useCurrentGSdxPluginSettingsLBL, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.useCurrentGSdxPluginSettingsCHK, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.useCurrentLilyPadPluginSettingsLBL, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.useCurrentLilyPadPluginSettingsCHK, 1, 9);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(49, 29);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.21077F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.15695F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.21076F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.21076F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.21076F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.21076F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.21076F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.15695F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.21076F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.21076F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(228, 246);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // useIndependantMemoryCardsCHK
            // 
            this.useIndependantMemoryCardsCHK.AutoSize = true;
            this.useIndependantMemoryCardsCHK.Location = new System.Drawing.Point(208, 3);
            this.useIndependantMemoryCardsCHK.Name = "useIndependantMemoryCardsCHK";
            this.useIndependantMemoryCardsCHK.Size = new System.Drawing.Size(15, 14);
            this.useIndependantMemoryCardsCHK.TabIndex = 0;
            this.useIndependantMemoryCardsCHK.UseVisualStyleBackColor = true;
            // 
            // useCurrentFileSettingsCHK
            // 
            this.useCurrentFileSettingsCHK.AutoSize = true;
            this.useCurrentFileSettingsCHK.Location = new System.Drawing.Point(208, 42);
            this.useCurrentFileSettingsCHK.Name = "useCurrentFileSettingsCHK";
            this.useCurrentFileSettingsCHK.Size = new System.Drawing.Size(15, 14);
            this.useCurrentFileSettingsCHK.TabIndex = 2;
            this.useCurrentFileSettingsCHK.UseVisualStyleBackColor = true;
            // 
            // useCurrentWindowSettingsCHK
            // 
            this.useCurrentWindowSettingsCHK.AutoSize = true;
            this.useCurrentWindowSettingsCHK.Location = new System.Drawing.Point(208, 69);
            this.useCurrentWindowSettingsCHK.Name = "useCurrentWindowSettingsCHK";
            this.useCurrentWindowSettingsCHK.Size = new System.Drawing.Size(15, 14);
            this.useCurrentWindowSettingsCHK.TabIndex = 3;
            this.useCurrentWindowSettingsCHK.UseVisualStyleBackColor = true;
            // 
            // useCurrentLogSettingsCHK
            // 
            this.useCurrentLogSettingsCHK.AutoSize = true;
            this.useCurrentLogSettingsCHK.Location = new System.Drawing.Point(208, 96);
            this.useCurrentLogSettingsCHK.Name = "useCurrentLogSettingsCHK";
            this.useCurrentLogSettingsCHK.Size = new System.Drawing.Size(15, 14);
            this.useCurrentLogSettingsCHK.TabIndex = 4;
            this.useCurrentLogSettingsCHK.UseVisualStyleBackColor = true;
            // 
            // useCurrentFolderSettingsCHK
            // 
            this.useCurrentFolderSettingsCHK.AutoSize = true;
            this.useCurrentFolderSettingsCHK.Location = new System.Drawing.Point(208, 123);
            this.useCurrentFolderSettingsCHK.Name = "useCurrentFolderSettingsCHK";
            this.useCurrentFolderSettingsCHK.Size = new System.Drawing.Size(15, 14);
            this.useCurrentFolderSettingsCHK.TabIndex = 5;
            this.useCurrentFolderSettingsCHK.UseVisualStyleBackColor = true;
            // 
            // useIndependantMemoryCardsLBL
            // 
            this.useIndependantMemoryCardsLBL.AutoSize = true;
            this.useIndependantMemoryCardsLBL.Location = new System.Drawing.Point(3, 0);
            this.useIndependantMemoryCardsLBL.Name = "useIndependantMemoryCardsLBL";
            this.useIndependantMemoryCardsLBL.Size = new System.Drawing.Size(153, 13);
            this.useIndependantMemoryCardsLBL.TabIndex = 6;
            this.useIndependantMemoryCardsLBL.Text = "Use Indepenant Memory Cards";
            this.useIndependantMemoryCardsLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTips.SetToolTip(this.useIndependantMemoryCardsLBL, "Creates a Memory Card For Each Games (Uses 8MB per Card)");
            // 
            // useCurrentFileSettingsLBL
            // 
            this.useCurrentFileSettingsLBL.AutoSize = true;
            this.useCurrentFileSettingsLBL.Location = new System.Drawing.Point(3, 39);
            this.useCurrentFileSettingsLBL.Name = "useCurrentFileSettingsLBL";
            this.useCurrentFileSettingsLBL.Size = new System.Drawing.Size(163, 13);
            this.useCurrentFileSettingsLBL.TabIndex = 7;
            this.useCurrentFileSettingsLBL.Text = "Use Current Plugin and Bios Files";
            this.useCurrentFileSettingsLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTips.SetToolTip(this.useCurrentFileSettingsLBL, "Plugin & Bios Selection (Recommended)");
            // 
            // useCurrentWindowSettingsLBL
            // 
            this.useCurrentWindowSettingsLBL.AutoSize = true;
            this.useCurrentWindowSettingsLBL.Location = new System.Drawing.Point(3, 66);
            this.useCurrentWindowSettingsLBL.Name = "useCurrentWindowSettingsLBL";
            this.useCurrentWindowSettingsLBL.Size = new System.Drawing.Size(161, 13);
            this.useCurrentWindowSettingsLBL.TabIndex = 8;
            this.useCurrentWindowSettingsLBL.Text = "Use Current GSWindow Settings";
            this.useCurrentWindowSettingsLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTips.SetToolTip(this.useCurrentWindowSettingsLBL, "Window Size & Aspect Ratio");
            // 
            // useCurrentLogSettingsLBL
            // 
            this.useCurrentLogSettingsLBL.AutoSize = true;
            this.useCurrentLogSettingsLBL.Location = new System.Drawing.Point(3, 93);
            this.useCurrentLogSettingsLBL.Name = "useCurrentLogSettingsLBL";
            this.useCurrentLogSettingsLBL.Size = new System.Drawing.Size(167, 13);
            this.useCurrentLogSettingsLBL.TabIndex = 9;
            this.useCurrentLogSettingsLBL.Tag = "Hiding/Showing Console & Debugging";
            this.useCurrentLogSettingsLBL.Text = "Use Current Program Log Settings";
            this.useCurrentLogSettingsLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // useCurrentFolderSettingsLBL
            // 
            this.useCurrentFolderSettingsLBL.AutoSize = true;
            this.useCurrentFolderSettingsLBL.Location = new System.Drawing.Point(3, 120);
            this.useCurrentFolderSettingsLBL.Name = "useCurrentFolderSettingsLBL";
            this.useCurrentFolderSettingsLBL.Size = new System.Drawing.Size(136, 13);
            this.useCurrentFolderSettingsLBL.TabIndex = 10;
            this.useCurrentFolderSettingsLBL.Text = "Use Current Folder Settings";
            this.useCurrentFolderSettingsLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTips.SetToolTip(this.useCurrentFolderSettingsLBL, "For Custom Foler Setup Only (Recommend off)");
            // 
            // useCurrentVMSettingsLBL
            // 
            this.useCurrentVMSettingsLBL.AutoSize = true;
            this.useCurrentVMSettingsLBL.Location = new System.Drawing.Point(3, 147);
            this.useCurrentVMSettingsLBL.Name = "useCurrentVMSettingsLBL";
            this.useCurrentVMSettingsLBL.Size = new System.Drawing.Size(123, 13);
            this.useCurrentVMSettingsLBL.TabIndex = 11;
            this.useCurrentVMSettingsLBL.Text = "Use Current VM Settings";
            this.useCurrentVMSettingsLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTips.SetToolTip(this.useCurrentVMSettingsLBL, "Hacks & Cheats Settings");
            // 
            // useCurrentVMSettingsCHK
            // 
            this.useCurrentVMSettingsCHK.AutoSize = true;
            this.useCurrentVMSettingsCHK.Location = new System.Drawing.Point(208, 150);
            this.useCurrentVMSettingsCHK.Name = "useCurrentVMSettingsCHK";
            this.useCurrentVMSettingsCHK.Size = new System.Drawing.Size(15, 14);
            this.useCurrentVMSettingsCHK.TabIndex = 12;
            this.useCurrentVMSettingsCHK.UseVisualStyleBackColor = true;
            // 
            // useCurrentGSdxPluginSettingsLBL
            // 
            this.useCurrentGSdxPluginSettingsLBL.AutoSize = true;
            this.useCurrentGSdxPluginSettingsLBL.Location = new System.Drawing.Point(3, 186);
            this.useCurrentGSdxPluginSettingsLBL.Name = "useCurrentGSdxPluginSettingsLBL";
            this.useCurrentGSdxPluginSettingsLBL.Size = new System.Drawing.Size(165, 13);
            this.useCurrentGSdxPluginSettingsLBL.TabIndex = 13;
            this.useCurrentGSdxPluginSettingsLBL.Text = "Use Current GSdx Plugin Settings";
            this.useCurrentGSdxPluginSettingsLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTips.SetToolTip(this.useCurrentGSdxPluginSettingsLBL, "Graphics Settings");
            // 
            // useCurrentGSdxPluginSettingsCHK
            // 
            this.useCurrentGSdxPluginSettingsCHK.AutoSize = true;
            this.useCurrentGSdxPluginSettingsCHK.Location = new System.Drawing.Point(208, 189);
            this.useCurrentGSdxPluginSettingsCHK.Name = "useCurrentGSdxPluginSettingsCHK";
            this.useCurrentGSdxPluginSettingsCHK.Size = new System.Drawing.Size(15, 14);
            this.useCurrentGSdxPluginSettingsCHK.TabIndex = 14;
            this.useCurrentGSdxPluginSettingsCHK.UseVisualStyleBackColor = true;
            // 
            // useCurrentLilyPadPluginSettingsLBL
            // 
            this.useCurrentLilyPadPluginSettingsLBL.AutoSize = true;
            this.useCurrentLilyPadPluginSettingsLBL.Location = new System.Drawing.Point(3, 213);
            this.useCurrentLilyPadPluginSettingsLBL.Name = "useCurrentLilyPadPluginSettingsLBL";
            this.useCurrentLilyPadPluginSettingsLBL.Size = new System.Drawing.Size(173, 13);
            this.useCurrentLilyPadPluginSettingsLBL.TabIndex = 15;
            this.useCurrentLilyPadPluginSettingsLBL.Text = "Use Current LilyPad Plugin Settings";
            this.useCurrentLilyPadPluginSettingsLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTips.SetToolTip(this.useCurrentLilyPadPluginSettingsLBL, "Controller Settings");
            // 
            // useCurrentLilyPadPluginSettingsCHK
            // 
            this.useCurrentLilyPadPluginSettingsCHK.AutoSize = true;
            this.useCurrentLilyPadPluginSettingsCHK.Location = new System.Drawing.Point(208, 216);
            this.useCurrentLilyPadPluginSettingsCHK.Name = "useCurrentLilyPadPluginSettingsCHK";
            this.useCurrentLilyPadPluginSettingsCHK.Size = new System.Drawing.Size(15, 14);
            this.useCurrentLilyPadPluginSettingsCHK.TabIndex = 16;
            this.useCurrentLilyPadPluginSettingsCHK.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.22807F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.77193F));
            this.tableLayoutPanel2.Controls.Add(this.configDirLBL, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.configDirBTN, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.configDirTXT, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(49, 281);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.42857F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(228, 59);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // configDirLBL
            // 
            this.configDirLBL.AutoSize = true;
            this.configDirLBL.Location = new System.Drawing.Point(3, 0);
            this.configDirLBL.Name = "configDirLBL";
            this.configDirLBL.Size = new System.Drawing.Size(119, 13);
            this.configDirLBL.TabIndex = 0;
            this.configDirLBL.Text = "Configurations Directory";
            this.configDirLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTips.SetToolTip(this.configDirLBL, "Store per Game Configs Here \\n default Will Use PCSX2 Directory");
            // 
            // configDirBTN
            // 
            this.configDirBTN.Location = new System.Drawing.Point(154, 19);
            this.configDirBTN.Name = "configDirBTN";
            this.configDirBTN.Size = new System.Drawing.Size(71, 23);
            this.configDirBTN.TabIndex = 1;
            this.configDirBTN.Text = "Browse";
            this.configDirBTN.UseVisualStyleBackColor = true;
            this.configDirBTN.Click += new System.EventHandler(this.configDirBTN_Click);
            // 
            // configDirTXT
            // 
            this.configDirTXT.Location = new System.Drawing.Point(3, 19);
            this.configDirTXT.Name = "configDirTXT";
            this.configDirTXT.Size = new System.Drawing.Size(145, 20);
            this.configDirTXT.TabIndex = 2;
            // 
            // ApplyBTN
            // 
            this.ApplyBTN.Location = new System.Drawing.Point(240, 353);
            this.ApplyBTN.Name = "ApplyBTN";
            this.ApplyBTN.Size = new System.Drawing.Size(75, 23);
            this.ApplyBTN.TabIndex = 2;
            this.ApplyBTN.Text = "Apply";
            this.ApplyBTN.UseVisualStyleBackColor = true;
            this.ApplyBTN.Click += new System.EventHandler(this.ApplyBTN_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 386);
            this.Controls.Add(this.ApplyBTN);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = global::PCSX2_Configurator.Properties.Resources.pcsx2;
            this.MinimumSize = new System.Drawing.Size(340, 425);
            this.Name = "Form1";
            this.Text = "PCSX2 Configurator";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox useIndependantMemoryCardsCHK;
        private System.Windows.Forms.CheckBox useCurrentFileSettingsCHK;
        private System.Windows.Forms.CheckBox useCurrentWindowSettingsCHK;
        private System.Windows.Forms.CheckBox useCurrentLogSettingsCHK;
        private System.Windows.Forms.CheckBox useCurrentFolderSettingsCHK;
        private System.Windows.Forms.Label useIndependantMemoryCardsLBL;
        private System.Windows.Forms.Label useCurrentFileSettingsLBL;
        private System.Windows.Forms.Label useCurrentWindowSettingsLBL;
        private System.Windows.Forms.Label useCurrentLogSettingsLBL;
        private System.Windows.Forms.Label useCurrentFolderSettingsLBL;
        private System.Windows.Forms.Label useCurrentVMSettingsLBL;
        private System.Windows.Forms.CheckBox useCurrentVMSettingsCHK;
        private System.Windows.Forms.Label useCurrentGSdxPluginSettingsLBL;
        private System.Windows.Forms.CheckBox useCurrentGSdxPluginSettingsCHK;
        private System.Windows.Forms.Label useCurrentLilyPadPluginSettingsLBL;
        private System.Windows.Forms.CheckBox useCurrentLilyPadPluginSettingsCHK;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label configDirLBL;
        private System.Windows.Forms.Button configDirBTN;
        private System.Windows.Forms.TextBox configDirTXT;
        private System.Windows.Forms.Button ApplyBTN;
        private System.Windows.Forms.FolderBrowserDialog configDirDLG;
        private System.Windows.Forms.ToolTip toolTips;
    }
}