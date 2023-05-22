using System.IO;
using System.Windows.Forms;
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
    public partial class Form1 : Form
    {
        public float fontSize = 0;
        public System.Drawing.FontStyle fontstyle = System.Drawing.FontStyle.Regular;
        public string font;

        public string filename;
        public bool isFilechanged;

        public FontSettings fontsettings;
        public AboutProgramm aboutprogramm;
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

        private void CloseWindow(object sender, FormClosingEventArgs e)
        {
            isFileSaved();
        }

        private void FontClick(object sender, EventArgs e)
        {
            fontsettings = new FontSettings();
            fontsettings.ShowDialog();
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
    }
}