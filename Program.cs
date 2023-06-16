/*  A DOOM64 Remaster Launcher
    Copyright (C) 2023 Gibbon

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Xml.Linq;

/* This file mostly deals with private stuff
 * some button clicks that depend on the forms
 * and WinForm stuff */

namespace GameLauncher
{
    public partial class MainForm : Form
    {
        /* DECLARATIONS */
        private Label label1;
        private Label label2;
        private Button closeButton;
        private Button executeButton;
        private GroupBox skillGroupBox;
        private RadioButton skill4RadioButton;
        private RadioButton skill3RadioButton;
        private RadioButton skill2RadioButton;
        private RadioButton skill1RadioButton;
        private CheckBox fastCheckBox;
        private CheckBox respawnCheckBox;
        private TextBox wadFileTextBox;
        private Button wadFileBrowseButton;
        private CheckBox nomonstersCheckBox;
        private Label levelsLabel;
        private ComboBox levelsDropdown;
        private Label dehacked64SelectionLabel;
        private TextBox dehacked64SelectionTextBox;
        private Button dehacked64BrowseButton;
        private RadioButton skill5RadioButton;
        private PictureBox pictureBox1;

        /* MAIN ENTRY POINT + INIT JOBS */
        public MainForm()
        {
            InitializeComponent();
            InitializeLevelsDropdown();
        }

        /* MAP LIST - POPULATES THE DROPDOWN */
        private void InitializeLevelsDropdown()
        {
            levelsDropdown.Items.AddRange(new[]
            {
                "MAP01", "MAP02", "MAP03", "MAP04", "MAP05", "MAP06",
                "MAP07", "MAP08", "MAP09", "MAP10","MAP11", "MAP12",
                "MAP13", "MAP14", "MAP15", "MAP16", "MAP17", "MAP18",
                "MAP19", "MAP20", "MAP21", "MAP22", "MAP23", "MAP24",
                "MAP25", "MAP26", "MAP27", "MAP28", "MAP29", "MAP30",
                "MAP31", "MAP32", "MAP33", "MAP34", "MAP35", "MAP36",
                "MAP37", "MAP38", "MAP39", "MAP40",
            });
        }

        /* STARTS DOOM64 AND ADDS PARAMETERS */
        private void executeButton_Click(object? sender, EventArgs e)
        {
            // Build the command line arguments based on selected options
            string arguments = "";

            if (skill1RadioButton.Checked)
                arguments += "-skill 1 ";
            else if (skill2RadioButton.Checked)
                arguments += "-skill 2 ";
            else if (skill3RadioButton.Checked)
                arguments += "-skill 3 ";
            else if (skill4RadioButton.Checked)
                arguments += "-skill 4 ";
            else if (skill5RadioButton.Checked)
                arguments += "-skill 5 ";

            if (levelsDropdown.SelectedIndex >= 0)
                arguments += $"-warp {levelsDropdown.SelectedIndex + 1} ";

            if (nomonstersCheckBox.Checked)
            {
                arguments += "-nomonsters ";
            }

            if (fastCheckBox.Checked)
            {
                arguments += "-fast ";
            }

            if (respawnCheckBox.Checked)
            {
                arguments += "-respawn ";
            }

            if (!string.IsNullOrEmpty(wadFileTextBox.Text))
            {
                arguments += $"-file {wadFileTextBox.Text} ";
            }

            if (!string.IsNullOrEmpty(dehacked64SelectionTextBox.Text))
            {
                arguments += $"-deh {dehacked64SelectionTextBox.Text} ";
            }

            // Execute the game executable with the generated arguments
            Process.Start(GlobalDeclarations.GameExecutable, arguments.Trim());
        }

        /* PRIVATE BUTTON CLICKS */

        private void wadFileBrowseButton_Click(object? sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "WAD files (*.wad)|*.wad";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                wadFileTextBox.Text = openFileDialog.FileName;
            }
        }

        private void dehacked64FileBrowseButton_Click(object? sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DEH files (*.deh)|*.deh";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                dehacked64SelectionTextBox.Text = openFileDialog.FileName;
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            closeButton = new Button();
            executeButton = new Button();
            skillGroupBox = new GroupBox();
            skill5RadioButton = new RadioButton();
            skill4RadioButton = new RadioButton();
            skill3RadioButton = new RadioButton();
            skill2RadioButton = new RadioButton();
            skill1RadioButton = new RadioButton();
            fastCheckBox = new CheckBox();
            respawnCheckBox = new CheckBox();
            wadFileTextBox = new TextBox();
            wadFileBrowseButton = new Button();
            nomonstersCheckBox = new CheckBox();
            levelsLabel = new Label();
            levelsDropdown = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            dehacked64SelectionLabel = new Label();
            dehacked64SelectionTextBox = new TextBox();
            dehacked64BrowseButton = new Button();
            skillGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // closeButton
            // 
            closeButton.Location = new Point(394, 317);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(75, 37);
            closeButton.TabIndex = 0;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += CloseButtonListener.closeButton_Click;
            // 
            // executeButton
            // 
            executeButton.Location = new Point(475, 317);
            executeButton.Name = "executeButton";
            executeButton.Size = new Size(197, 37);
            executeButton.TabIndex = 1;
            executeButton.Text = "Start DOOM64 EX+";
            executeButton.UseVisualStyleBackColor = true;
            executeButton.Click += executeButton_Click;
            // 
            // skillGroupBox
            // 
            skillGroupBox.BackColor = Color.Transparent;
            skillGroupBox.Controls.Add(skill5RadioButton);
            skillGroupBox.Controls.Add(skill4RadioButton);
            skillGroupBox.Controls.Add(skill3RadioButton);
            skillGroupBox.Controls.Add(skill2RadioButton);
            skillGroupBox.Controls.Add(skill1RadioButton);
            skillGroupBox.ForeColor = SystemColors.Control;
            skillGroupBox.Location = new Point(12, 180);
            skillGroupBox.Name = "skillGroupBox";
            skillGroupBox.Size = new Size(170, 179);
            skillGroupBox.TabIndex = 2;
            skillGroupBox.TabStop = false;
            skillGroupBox.Text = "Skill";
            // 
            // skill5RadioButton
            // 
            skill5RadioButton.AutoSize = true;
            skill5RadioButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            skill5RadioButton.Location = new Point(22, 149);
            skill5RadioButton.Name = "skill5RadioButton";
            skill5RadioButton.Size = new Size(97, 25);
            skill5RadioButton.TabIndex = 4;
            skill5RadioButton.TabStop = true;
            skill5RadioButton.Text = "Hardcore!";
            skill5RadioButton.UseVisualStyleBackColor = true;
            // 
            // skill4RadioButton
            // 
            skill4RadioButton.AutoSize = true;
            skill4RadioButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            skill4RadioButton.Location = new Point(22, 122);
            skill4RadioButton.Name = "skill4RadioButton";
            skill4RadioButton.Size = new Size(129, 25);
            skill4RadioButton.TabIndex = 3;
            skill4RadioButton.TabStop = true;
            skill4RadioButton.Text = "Watch Me Die!";
            skill4RadioButton.UseVisualStyleBackColor = true;
            // 
            // skill3RadioButton
            // 
            skill3RadioButton.AutoSize = true;
            skill3RadioButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            skill3RadioButton.Location = new Point(22, 91);
            skill3RadioButton.Name = "skill3RadioButton";
            skill3RadioButton.Size = new Size(121, 25);
            skill3RadioButton.TabIndex = 2;
            skill3RadioButton.TabStop = true;
            skill3RadioButton.Text = "I Own Doom!";
            skill3RadioButton.UseVisualStyleBackColor = true;
            // 
            // skill2RadioButton
            // 
            skill2RadioButton.AutoSize = true;
            skill2RadioButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            skill2RadioButton.Location = new Point(22, 60);
            skill2RadioButton.Name = "skill2RadioButton";
            skill2RadioButton.Size = new Size(108, 25);
            skill2RadioButton.TabIndex = 1;
            skill2RadioButton.TabStop = true;
            skill2RadioButton.Text = "Bring it On!";
            skill2RadioButton.UseVisualStyleBackColor = true;
            // 
            // skill1RadioButton
            // 
            skill1RadioButton.AutoSize = true;
            skill1RadioButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            skill1RadioButton.Location = new Point(22, 28);
            skill1RadioButton.Name = "skill1RadioButton";
            skill1RadioButton.Size = new Size(99, 25);
            skill1RadioButton.TabIndex = 0;
            skill1RadioButton.TabStop = true;
            skill1RadioButton.Text = "Be Gentle!";
            skill1RadioButton.UseVisualStyleBackColor = true;
            // 
            // fastCheckBox
            // 
            fastCheckBox.BackColor = Color.Transparent;
            fastCheckBox.ForeColor = SystemColors.Control;
            fastCheckBox.Location = new Point(215, 239);
            fastCheckBox.Name = "fastCheckBox";
            fastCheckBox.Size = new Size(197, 29);
            fastCheckBox.TabIndex = 9;
            fastCheckBox.Text = "Fast Monsters";
            fastCheckBox.UseVisualStyleBackColor = false;
            // 
            // respawnCheckBox
            // 
            respawnCheckBox.BackColor = Color.Transparent;
            respawnCheckBox.ForeColor = SystemColors.Control;
            respawnCheckBox.Location = new Point(215, 208);
            respawnCheckBox.Name = "respawnCheckBox";
            respawnCheckBox.Size = new Size(177, 28);
            respawnCheckBox.TabIndex = 0;
            respawnCheckBox.Text = "Repawning Monsters";
            respawnCheckBox.UseVisualStyleBackColor = false;
            // 
            // wadFileTextBox
            // 
            wadFileTextBox.Location = new Point(12, 148);
            wadFileTextBox.Name = "wadFileTextBox";
            wadFileTextBox.Size = new Size(197, 29);
            wadFileTextBox.TabIndex = 6;
            // 
            // wadFileBrowseButton
            // 
            wadFileBrowseButton.Location = new Point(215, 148);
            wadFileBrowseButton.Name = "wadFileBrowseButton";
            wadFileBrowseButton.Size = new Size(75, 29);
            wadFileBrowseButton.TabIndex = 0;
            wadFileBrowseButton.Text = "Browse";
            wadFileBrowseButton.UseVisualStyleBackColor = true;
            wadFileBrowseButton.Click += wadFileBrowseButton_Click;
            // 
            // nomonstersCheckBox
            // 
            nomonstersCheckBox.BackColor = Color.Transparent;
            nomonstersCheckBox.ForeColor = SystemColors.Control;
            nomonstersCheckBox.Location = new Point(215, 272);
            nomonstersCheckBox.Name = "nomonstersCheckBox";
            nomonstersCheckBox.Size = new Size(134, 24);
            nomonstersCheckBox.TabIndex = 0;
            nomonstersCheckBox.Text = "No Monsters";
            nomonstersCheckBox.UseVisualStyleBackColor = false;
            // 
            // levelsLabel
            // 
            levelsLabel.AutoSize = true;
            levelsLabel.BackColor = Color.Transparent;
            levelsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            levelsLabel.ForeColor = SystemColors.Control;
            levelsLabel.Location = new Point(12, 9);
            levelsLabel.Name = "levelsLabel";
            levelsLabel.Size = new Size(113, 21);
            levelsLabel.TabIndex = 3;
            levelsLabel.Text = "Level Selection";
            // 
            // levelsDropdown
            // 
            levelsDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            levelsDropdown.FormattingEnabled = true;
            levelsDropdown.Location = new Point(12, 36);
            levelsDropdown.Name = "levelsDropdown";
            levelsDropdown.Size = new Size(113, 29);
            levelsDropdown.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(211, 180);
            label1.Name = "label1";
            label1.Size = new Size(136, 21);
            label1.TabIndex = 11;
            label1.Text = "Game Parameters:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(12, 124);
            label2.Name = "label2";
            label2.Size = new Size(195, 21);
            label2.TabIndex = 12;
            label2.Text = "DOOM64 PWAD Selection:";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(394, 9);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(278, 272);
            pictureBox1.TabIndex = 16;
            pictureBox1.TabStop = false;
            // 
            // dehacked64SelectionLabel
            // 
            dehacked64SelectionLabel.AutoSize = true;
            dehacked64SelectionLabel.BackColor = Color.Transparent;
            dehacked64SelectionLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dehacked64SelectionLabel.ForeColor = SystemColors.Control;
            dehacked64SelectionLabel.Location = new Point(12, 68);
            dehacked64SelectionLabel.Name = "dehacked64SelectionLabel";
            dehacked64SelectionLabel.Size = new Size(170, 21);
            dehacked64SelectionLabel.TabIndex = 17;
            dehacked64SelectionLabel.Text = "DeHackED64 Selection:";
            // 
            // dehacked64SelectionTextBox
            // 
            dehacked64SelectionTextBox.Location = new Point(12, 92);
            dehacked64SelectionTextBox.Name = "dehacked64SelectionTextBox";
            dehacked64SelectionTextBox.Size = new Size(197, 29);
            dehacked64SelectionTextBox.TabIndex = 18;
            // 
            // dehacked64BrowseButton
            // 
            dehacked64BrowseButton.Location = new Point(215, 92);
            dehacked64BrowseButton.Name = "dehacked64BrowseButton";
            dehacked64BrowseButton.Size = new Size(75, 29);
            dehacked64BrowseButton.TabIndex = 19;
            dehacked64BrowseButton.Text = "Browse";
            dehacked64BrowseButton.UseVisualStyleBackColor = true;
            dehacked64BrowseButton.Click += dehacked64FileBrowseButton_Click;
            // 
            // MainForm
            // 
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(684, 370);
            Controls.Add(dehacked64BrowseButton);
            Controls.Add(dehacked64SelectionTextBox);
            Controls.Add(dehacked64SelectionLabel);
            Controls.Add(pictureBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(levelsDropdown);
            Controls.Add(levelsLabel);
            Controls.Add(skillGroupBox);
            Controls.Add(executeButton);
            Controls.Add(closeButton);
            Controls.Add(fastCheckBox);
            Controls.Add(nomonstersCheckBox);
            Controls.Add(respawnCheckBox);
            Controls.Add(wadFileBrowseButton);
            Controls.Add(wadFileTextBox);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DOOM64 EX+ Launcher";
            skillGroupBox.ResumeLayout(false);
            skillGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        static class Program
        {
            [STAThread]
            static void Main()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }
}
