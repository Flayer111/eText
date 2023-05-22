using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace text_redactor
{
    partial class FontSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontSettings));
            this.ExampleLabel = new System.Windows.Forms.Label();
            this.ExampleText = new System.Windows.Forms.Label();
            this.FontFamilyBox = new System.Windows.Forms.TextBox();
            this.FontStyleBox = new System.Windows.Forms.TextBox();
            this.FontSizeBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.FontSize = new System.Windows.Forms.ListBox();
            this.FontStyle = new System.Windows.Forms.ListBox();
            this.FontFamily = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // ExampleLabel
            // 
            resources.ApplyResources(this.ExampleLabel, "ExampleLabel");
            this.ExampleLabel.Name = "ExampleLabel";
            // 
            // ExampleText
            // 
            resources.ApplyResources(this.ExampleText, "ExampleText");
            this.ExampleText.Name = "ExampleText";
            // 
            // FontFamilyBox
            // 
            resources.ApplyResources(this.FontFamilyBox, "FontFamilyBox");
            this.FontFamilyBox.Name = "FontFamilyBox";
            this.FontFamilyBox.TextChanged += new System.EventHandler(this.FontFamilyChange);
            // 
            // FontStyleBox
            // 
            resources.ApplyResources(this.FontStyleBox, "FontStyleBox");
            this.FontStyleBox.Name = "FontStyleBox";
            this.FontStyleBox.TextChanged += new System.EventHandler(this.FontStyleChange);
            // 
            // FontSizeBox
            // 
            resources.ApplyResources(this.FontSizeBox, "FontSizeBox");
            this.FontSizeBox.Name = "FontSizeBox";
            this.FontSizeBox.TextChanged += new System.EventHandler(this.FontSizeChange);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Ok_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // FontSize
            // 
            this.FontSize.FormattingEnabled = true;
            resources.ApplyResources(this.FontSize, "FontSize");
            this.FontSize.Items.AddRange(new object[] {
            resources.GetString("FontSize.Items"),
            resources.GetString("FontSize.Items1"),
            resources.GetString("FontSize.Items2"),
            resources.GetString("FontSize.Items3"),
            resources.GetString("FontSize.Items4"),
            resources.GetString("FontSize.Items5"),
            resources.GetString("FontSize.Items6"),
            resources.GetString("FontSize.Items7"),
            resources.GetString("FontSize.Items8"),
            resources.GetString("FontSize.Items9"),
            resources.GetString("FontSize.Items10"),
            resources.GetString("FontSize.Items11"),
            resources.GetString("FontSize.Items12"),
            resources.GetString("FontSize.Items13"),
            resources.GetString("FontSize.Items14"),
            resources.GetString("FontSize.Items15"),
            resources.GetString("FontSize.Items16"),
            resources.GetString("FontSize.Items17"),
            resources.GetString("FontSize.Items18"),
            resources.GetString("FontSize.Items19"),
            resources.GetString("FontSize.Items20"),
            resources.GetString("FontSize.Items21"),
            resources.GetString("FontSize.Items22"),
            resources.GetString("FontSize.Items23")});
            this.FontSize.Name = "FontSize";
            this.FontSize.SelectedIndexChanged += new System.EventHandler(this.ChangeFontSize);
            // 
            // FontStyle
            // 
            this.FontStyle.FormattingEnabled = true;
            resources.ApplyResources(this.FontStyle, "FontStyle");
            this.FontStyle.Items.AddRange(new object[] {
            resources.GetString("FontStyle.Items"),
            resources.GetString("FontStyle.Items1"),
            resources.GetString("FontStyle.Items2"),
            resources.GetString("FontStyle.Items3")});
            this.FontStyle.Name = "FontStyle";
            this.FontStyle.SelectedIndexChanged += new System.EventHandler(this.ChangeFontStyle);
            // 
            // FontFamily
            // 
            this.FontFamily.FormattingEnabled = true;
            resources.ApplyResources(this.FontFamily, "FontFamily");
            this.FontFamily.Name = "FontFamily";
            this.FontFamily.SelectedIndexChanged += new System.EventHandler(this.ChangeFontFamily);
            // 
            // FontSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FontFamily);
            this.Controls.Add(this.FontStyle);
            this.Controls.Add(this.FontSize);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FontSizeBox);
            this.Controls.Add(this.FontStyleBox);
            this.Controls.Add(this.FontFamilyBox);
            this.Controls.Add(this.ExampleText);
            this.Controls.Add(this.ExampleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FontSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label ExampleLabel;
        private Label ExampleText;
        private TextBox FontFamilyBox;
        private TextBox FontStyleBox;
        private TextBox FontSizeBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button button1;
        private Button button2;
        private ListBox FontSize;
        private ListBox FontStyle;
        private ListBox FontFamily;
    }
}