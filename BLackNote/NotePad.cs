using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace BLackNote
{
    public partial class NotePad : Form
    {
        string filePath = "";
        private bool _isDataModified = false;
        public NotePad()
        {
            InitializeComponent();
        }

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filePath = "";
            richTextBoxMain.Text = "";
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog txt = new OpenFileDialog() { Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*", Multiselect = false })
            {
                if (txt.ShowDialog() == DialogResult.OK)
                {
                    string filePath = txt.FileName;
                    richTextBoxMain.Text = File.ReadAllText(filePath);
                    MessageBox.Show("Файл успешно открыт!");
                }
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                File.WriteAllText(filePath, richTextBoxMain.Text);
                MessageBox.Show("Файл успешно сохранен!");
            }
            else
            {
                сохранитьКакToolStripMenuItem_Click(sender, e);
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog txt = new SaveFileDialog() { Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*" })
            {
                if (txt.ShowDialog() == DialogResult.OK)
                {

                    filePath = txt.FileName;
                    File.WriteAllText(filePath, richTextBoxMain.Text);
                    MessageBox.Show("Файл успешно сохранен!");

                }
            }
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (s, ev) => ev.Graphics.DrawString(richTextBoxMain.Text, richTextBoxMain.Font, new SolidBrush(richTextBoxMain.ForeColor), ev.MarginBounds);
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_isDataModified)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения перед выходом?", "Выход", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    сохранитьToolStripMenuItem_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    Application.Exit();
                }
            }
            else
            {
                Application.Exit();
            }
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                if (richTextBoxMain.SelectedText.Length > 0)
                {
                    richTextBoxMain.SelectionFont = fontDialog.Font;
                }
                else
                {
                    richTextBoxMain.Font = fontDialog.Font;
                }
            }
        }

        private void времяИДатаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxMain.Text += DateTime.Now;
        }

        private void richTextBoxMain_TextChanged(object sender, EventArgs e)
        {
            _isDataModified =true;
            if (richTextBoxMain.Text.Length > 0)
            {
                вырезатьToolStripMenuItem.Enabled = true;
                копироватьToolStripMenuItem.Enabled = true;
            }
            else
            {
                вырезатьToolStripMenuItem.Enabled = false;
                копироватьToolStripMenuItem.Enabled = false;
            }
       
           
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxMain.Undo();
        }

        private void вкрнутьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxMain.Redo();
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxMain.Cut();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxMain.Copy();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxMain.Paste();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxMain.SelectedText = "";
        }

        private void выделитьВсёToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxMain.SelectAll();
        }

        private void переносТекстаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (переносТекстаToolStripMenuItem.Checked==true)
            {
                переносТекстаToolStripMenuItem.Checked = false;
                richTextBoxMain.WordWrap = false;
            }
            else
            {
                переносТекстаToolStripMenuItem.Checked = true;
                richTextBoxMain.WordWrap = true;
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void выделитьТекстToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxMain.SelectionBackColor = Color.PaleGreen;
        }

        private void изменитьЦветТекстаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                if (richTextBoxMain.SelectedText.Length > 0)
                {
                    richTextBoxMain.SelectionColor = colorDialog.Color;
                }
                else
                {
                    richTextBoxMain.ForeColor = colorDialog.Color;
                }
            }
        }
    }
}
