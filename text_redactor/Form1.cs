using System.IO;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using System.Drawing.Printing;

namespace text_redactor
{
    public partial class Form1 : Form
    {
        public float fontSize = 0;
        public System.Drawing.FontStyle fontstyle = System.Drawing.FontStyle.Regular;
        public string font;

        public string filename;
        public bool isFilechanged;

        public FontSettings fontsettings;
        public AboutProgramm aboutprogramm;

        public int pos, line, column;
        public Form1()
        {
            InitializeComponent();

            Init();
        }

        public void Init()
        {
            filename = "";
            isFilechanged = false;
            UpdateTitle();

            InstalledFontCollection fonts = new InstalledFontCollection();
            foreach (FontFamily family in fonts.Families)
            {
                toolStripComboBox1.Items.Add(family.Name);
            }

            int pos = textBox1.SelectionStart;
            int line = textBox1.GetLineFromCharIndex(pos);
            int column = textBox1.SelectionStart - textBox1.GetFirstCharIndexFromLine(line);
            lineColumnStatusLabel.Text = "Line " + (line + 1) + ", Column " + (column + 1);
        }

        public void createNewDocument(object sender, EventArgs e)
        {
            isFileSaved();
            textBox1.Text = "";
            filename = "";
            UpdateTitle();
            isFilechanged = false;
        }

        public void openFile(object sender, EventArgs e)
        {
            isFileSaved();
            openFileDialog1.FileName = "";         
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName);
                    textBox1.Text = sr.ReadToEnd();
                    sr.Close();
                    filename = openFileDialog1.FileName;
                }
                catch
                {
                    MessageBox.Show("Не удалось открыть файл");
                }
            }
            UpdateTitle();
        }

        public void saveFile(string _filename)
        {
            if(_filename == "")
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _filename = saveFileDialog1.FileName;
                }
            }
            try
            {
                StreamWriter sw = new StreamWriter(_filename);
                sw.Write(textBox1.Text);
                sw.Close();
                filename = _filename;
                isFilechanged = false;
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить файл");
            }
            UpdateTitle();
        }

        public void save(object sender, EventArgs e)
        {
            saveFile(filename);
        }

        public void saveAs(object sender, EventArgs e)
        {
            saveFile("");
        }

        private void TextChanged(object sender, EventArgs e)
        {
            if (!isFilechanged)
            {
                this.Text = this.Text.Replace("*", " ");
                isFilechanged = true;
                this.Text = "*" + this.Text;
            }

            pos = textBox1.SelectionStart;
            line = textBox1.GetLineFromCharIndex(pos);
            column = textBox1.SelectionStart - textBox1.GetFirstCharIndexFromLine(line);
            lineColumnStatusLabel.Text = "Line " + (line + 1) + ", Column " + (column + 1);
        }

        public void UpdateTitle()
        {
            if(filename != "")
            {
                this.Text = filename + "- eText";
            }
            else this.Text = "Безымянный - eText";
        }
        public void isFileSaved()
        {
            if (isFilechanged)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения?", "Сохранение файла", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if(result == DialogResult.Yes)
                {
                    saveFile(filename);
                }
            }
        }
        public void CopyText()
        {
            try
            {
                Clipboard.SetText(textBox1.SelectedText);
            }
            catch
            {
                
            }
        }
        public void CutText()
        {
            try
            {
                Clipboard.SetText(textBox1.Text.Substring(textBox1.SelectionStart, textBox1.SelectionLength));
                textBox1.Text = textBox1.Text.Remove(textBox1.SelectionStart, textBox1.SelectionLength);
            }
            catch
            {

            }
        }
        public void PasteText()
        {
            textBox1.Text = textBox1.Text.Substring(0, textBox1.SelectionStart) + Clipboard.GetText() + textBox1.Text.Substring(textBox1.SelectionStart, textBox1.Text.Length-textBox1.SelectionStart);
            textBox1.Select(textBox1.Text.Length, 0);
        }

        public void DeleteText()
        {
            try
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.SelectionStart, textBox1.SelectionLength);
                textBox1.Select(textBox1.Text.Length, 0);
            }
            catch
            {

            }
        }
        private void CopyClick(object sender, EventArgs e)
        {
            CopyText();
        }
        private void CutClick(object sender, EventArgs e)
        {
            CutText();
        }
        private void PasteClick(object sender, EventArgs e)
        {
            PasteText();
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            DeleteText();
        }

        private void SelectAllClick(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void CloseWindow(object sender, FormClosingEventArgs e)
        {
            isFileSaved();
        }

        private void FontClick(object sender, EventArgs e)
        {
            fontsettings = new FontSettings();
            fontsettings.ShowDialog();
        }

        private void PrintPreviewClick(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void PrintClick(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print(); 
            }
        }

        private void OnFocus(object sender, EventArgs e)
        {
            if(fontsettings != null)
            {
                fontSize = FontData.fontSize;
                fontstyle = FontData.fontstyle;
                font = FontData.font;

                if (textBox1.Font.Size != fontSize)
                {
                    textBox1.Font = new Font(textBox1.Font.FontFamily, fontSize);
                }

                if (textBox1.Font.Style != fontstyle)
                {
                    textBox1.Font = new Font(textBox1.Font.FontFamily, textBox1.Font.Size, fontstyle);
                }

                if(textBox1.Font.Name != font)
                {
                    textBox1.Font = new Font(font, fontSize, fontstyle);
                }
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AboutProgrammClick(object sender, EventArgs e)
        {
            aboutprogramm = new AboutProgramm();
            aboutprogramm.ShowDialog();
        }

        private void UndoClick(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void RedoClick(object sender, EventArgs e)
        {
            textBox1.Redo();
        }

        private void save_icon_click(object sender, EventArgs e)
        {
            saveFile(filename);
        }

        private void open_icon_click(object sender, EventArgs e)
        {
            isFileSaved();
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName);
                    textBox1.Text = sr.ReadToEnd();
                    sr.Close();
                    filename = openFileDialog1.FileName;
                }
                catch
                {
                    MessageBox.Show("Не удалось открыть файл");
                }
            }
            UpdateTitle();
        }

        private void back_icon_click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void forward_icon_click(object sender, EventArgs e)
        {
            textBox1.Redo();
        }

        private void FontSizeForm1(object sender, EventArgs e)
        {
            if (textBox1.SelectionFont == null)
            {
                return;
            }
            textBox1.SelectionFont = new Font(textBox1.SelectionFont.FontFamily, Convert.ToInt32(toolStripComboBox2.Text), textBox1.SelectionFont.Style);
        }

        private void FontFamilyForm1(object sender, EventArgs e)
        {
            if (textBox1.SelectionFont == null)
            {
                textBox1.Font = new Font(toolStripComboBox1.Text, textBox1.Font.Size);
            }
            textBox1.SelectionFont = new Font(toolStripComboBox1.Text, textBox1.SelectionFont.Size);
        }

        private void left_align_click(object sender, EventArgs e)
        {
            centerAlignStripButton.Checked = false;
            rightAlignStripButton.Checked = false;
            if (leftAlignStripButton.Checked == false)
            {
                leftAlignStripButton.Checked = true;    
            }
            else if (leftAlignStripButton.Checked == true)
            {
                leftAlignStripButton.Checked = false;    
            }
            textBox1.SelectionAlignment = HorizontalAlignment.Left;    
        }

        private void center_align_click(object sender, EventArgs e)
        {
            leftAlignStripButton.Checked = false;
            rightAlignStripButton.Checked = false;
            if (centerAlignStripButton.Checked == false)
            {
                centerAlignStripButton.Checked = true;    
            }
            else if (centerAlignStripButton.Checked == true)
            {
                centerAlignStripButton.Checked = false;    
            }
            textBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void right_align_click(object sender, EventArgs e)
        {
            leftAlignStripButton.Checked = false;
            centerAlignStripButton.Checked = false;

            if (rightAlignStripButton.Checked == false)
            {
                rightAlignStripButton.Checked = true;    
            }
            else if (rightAlignStripButton.Checked == true)
            {
                rightAlignStripButton.Checked = false;    
            }
            textBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void Bold_click(object sender, EventArgs e)
        {
            if (boldStripButton.Checked == false)
            {
                boldStripButton.Checked = true; 
            }
            else if (boldStripButton.Checked == true)
            {
                boldStripButton.Checked = false;    
            }

            if (textBox1.SelectionFont == null)
            {
                return;
            }

            FontStyle style = textBox1.SelectionFont.Style;

            if (textBox1.SelectionFont.Bold)
            {
                style &= ~FontStyle.Bold;
            }
            else
            {
                style |= FontStyle.Bold;

            }
            textBox1.SelectionFont = new Font(textBox1.SelectionFont, style);
        }

        private void Italic_click(object sender, EventArgs e)
        {
            if (italicStripButton.Checked == false)
            {
                italicStripButton.Checked = true;    
            }
            else if (italicStripButton.Checked == true)
            {
                italicStripButton.Checked = false;    
            }

            if (textBox1.SelectionFont == null)
            {
                return;
            }

            FontStyle style = textBox1.SelectionFont.Style;

            if (textBox1.SelectionFont.Italic)
            {
                style &= ~FontStyle.Italic;
            }
            else
            {
                style |= FontStyle.Italic;
            }
            textBox1.SelectionFont = new Font(textBox1.SelectionFont, style);
        }

        private void Underline_click(object sender, EventArgs e)
        {
            if (underlineStripButton.Checked == false)
            {
                underlineStripButton.Checked = true;     
            }
            else if (underlineStripButton.Checked == true)
            {
                underlineStripButton.Checked = false;    
            }

            if (textBox1.SelectionFont == null)
            {
                return;
            }

            FontStyle style = textBox1.SelectionFont.Style;

            if (textBox1.SelectionFont.Underline)
            {
                style &= ~FontStyle.Underline;
            }
            else
            {
                style |= FontStyle.Underline;
            }
            textBox1.SelectionFont = new Font(textBox1.SelectionFont, style);
        }   

        private void Key_up(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    pos = textBox1.SelectionStart;    
                    line = textBox1.GetLineFromCharIndex(pos);    
                    column = textBox1.SelectionStart - textBox1.GetFirstCharIndexFromLine(line);    
                    lineColumnStatusLabel.Text = "Line " + (line + 1) + ", Column " + (column + 1);
                    break;
                case Keys.Right:
                    pos = textBox1.SelectionStart; 
                    line = textBox1.GetLineFromCharIndex(pos); 
                    column = textBox1.SelectionStart - textBox1.GetFirstCharIndexFromLine(line);   
                    lineColumnStatusLabel.Text = "Line " + (line + 1) + ", Column " + (column + 1);
                    break;
                case Keys.Up:
                    pos = textBox1.SelectionStart; 
                    line = textBox1.GetLineFromCharIndex(pos);
                    column = textBox1.SelectionStart - textBox1.GetFirstCharIndexFromLine(line);    
                    lineColumnStatusLabel.Text = "Line " + (line + 1) + ", Column " + (column + 1);
                    break;
                case Keys.Left:
                    pos = textBox1.SelectionStart; 
                    line = textBox1.GetLineFromCharIndex(pos); 
                    column = textBox1.SelectionStart - textBox1.GetFirstCharIndexFromLine(line);    
                    lineColumnStatusLabel.Text = "Line " + (line + 1) + ", Column " + (column + 1);
                    break;
            }
        }

        private void Mouse_down(object sender, MouseEventArgs e)
        {
            int pos = textBox1.SelectionStart;    
            int line = textBox1.GetLineFromCharIndex(pos);    
            int column = textBox1.SelectionStart - textBox1.GetFirstCharIndexFromLine(line);    
            lineColumnStatusLabel.Text = "Line " + (line + 1) + ", Column " + (column + 1);
        }
    }
}