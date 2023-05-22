using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace text_redactor
{
    public partial class FontSettings : Form
    {
        public FontSettings()
        {
            InitializeComponent();
            FontSizeBox.Text = FontData.fontSize.ToString();
            if(FontData.fontSize < 50 & FontData.fontSize > 8)
            {
                FontSize.SelectedItem = Convert.ToString(FontData.fontSize);
            }
            else
            {
                FontSize.SelectedItem = null;
            }
            if(FontData.fontstyle == System.Drawing.FontStyle.Regular)
            {
                FontStyle.SelectedItem = FontStyle.Items[0];
            }
            if (FontData.fontstyle == System.Drawing.FontStyle.Italic)
            {
                FontStyle.SelectedItem = FontStyle.Items[1];
            }
            if (FontData.fontstyle == System.Drawing.FontStyle.Bold)
            {
                FontStyle.SelectedItem = FontStyle.Items[2];
            }
            if (FontData.fontstyle == (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))
            {
                FontStyle.SelectedItem = FontStyle.Items[3];
            }

            InstalledFontCollection fonts = new InstalledFontCollection();
            foreach (FontFamily family in fonts.Families)
            {
                FontFamily.Items.Add(family.Name);
            }

            FontFamily.SelectedItem = FontData.font;
        }

        private void ChangeFontSize(object sender, EventArgs e)
        {  
            if(FontSize.SelectedItem != null)
            {
                FontSizeBox.Text = FontSize.SelectedItem.ToString();
            }
        }

        private void ChangeFontStyle(object sender, EventArgs e)
        {
            switch (FontStyle.SelectedItem.ToString())
            {
                case "обычный":
                    FontStyleBox.Text = "обычный";
                    break;
                case "курсив":
                    FontStyleBox.Text = "курсив";
                    break;
                case "полужирный":
                    FontStyleBox.Text = "полужирный";
                    break;
                case "полужирный курсив":
                    FontStyleBox.Text = "полужирный курсив";
                    break;
            }          
        }

        private void ChangeFontFamily(object sender, EventArgs e)
        {
            FontFamilyBox.Text = FontFamily.SelectedItem.ToString();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            try
            {
                FontData.fontstyle = ExampleText.Font.Style;
                FontData.fontSize = int.Parse(FontSizeBox.Text);
                FontData.font = FontFamilyBox.Text;
                this.Hide();
            }
            catch
            {
                MessageBox.Show("Рамзер шрифта должен быть числом");
            }
        }

        private void FontSizeChange(object sender, EventArgs e)
        {
            for(int i = 0; i < (FontSize.Items.Count); i++)
            {
                if(FontSizeBox.Text == Convert.ToString(FontSize.Items[i]))
                {
                    ExampleText.Font = new Font(ExampleText.Font.FontFamily, int.Parse(FontSizeBox.Text), ExampleText.Font.Style);
                    FontSize.SelectedItem = FontSizeBox.Text;
                }
                try
                {
                    if (int.Parse(FontSizeBox.Text) > 50 | int.Parse(FontSizeBox.Text) < 8)
                    {
                        FontSize.SelectedItem = null;
                    }
                }
                catch { }
            }         
        }

        private void FontStyleChange(object sender, EventArgs e)
        {
            switch (FontStyleBox.Text.ToLower())
            {
                case "обычный":
                    ExampleText.Font = new Font(ExampleText.Font.FontFamily, int.Parse(FontSizeBox.Text), System.Drawing.FontStyle.Regular);
                    FontStyle.SelectedItem = FontStyleBox.Text.ToLower();
                    break;
                case "курсив":
                    ExampleText.Font = new Font(ExampleText.Font.FontFamily, int.Parse(FontSizeBox.Text), System.Drawing.FontStyle.Italic);
                    FontStyle.SelectedItem = FontStyleBox.Text.ToLower();
                    break;
                case "полужирный":
                    ExampleText.Font = new Font(ExampleText.Font.FontFamily, int.Parse(FontSizeBox.Text), System.Drawing.FontStyle.Bold);
                    FontStyle.SelectedItem = FontStyleBox.Text.ToLower();
                    break;
                case "полужирный курсив":
                    ExampleText.Font = new Font(ExampleText.Font.FontFamily, int.Parse(FontSizeBox.Text), System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
                    FontStyle.SelectedItem = FontStyleBox.Text.ToLower();
                    break;
            }
        }

        private void FontFamilyChange(object sender, EventArgs e)
        {
            for(int i = 0; i < FontFamily.Items.Count; i++)
            {
                string fontText1 = FontFamily.Items[i].ToString();
                fontText1 = fontText1.ToLower();

                string fontText2 = FontFamilyBox.Text;
                fontText2 = fontText2.ToLower();
                if (fontText1 == fontText2)
                {
                    ExampleText.Font = new Font(FontFamilyBox.Text, int.Parse(FontSizeBox.Text), ExampleText.Font.Style);
                    FontFamily.SelectedItem = FontFamily.Items[i].ToString();
                }
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }   
    }
}
