using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CommitParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CommitsList = new List<Commit>();
        }

        List<Commit> CommitsList;

        private void button1_Click(object sender, EventArgs e)
        {
            loadCommits();
        }        

        private void authorsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Text = authorsListBox.SelectedItem.ToString() + Environment.NewLine;
            foreach (Commit cmt in CommitsList)
            {
                if (authorsListBox.SelectedIndex == 0)
                {
                    loadCommits();
                    break;
                }
                else if (cmt.Author == authorsListBox.SelectedItem.ToString())
                {
                    textBox1.Text += "\t" + cmt.Date + " \t" + cmt.Description + Environment.NewLine;
                }
            }
        }

        private void loadCommits()
        {
            textBox1.Text = "";
            authorsListBox.Enabled = true;
            button1.Enabled = false;
            StreamReader streamReader = new StreamReader("gitLog.txt", Encoding.Default);
            string s = "";
            Commit commit = new Commit();
            while (!streamReader.EndOfStream)
            {
                s = streamReader.ReadLine();
                
                if (s.StartsWith("commit"))
                {
                    s = s.Replace("commit", "");
                    s = s.Trim();
                    commit.CommitText = s;
                }
                else if (s.StartsWith("Author:"))
                {
                    s = s.Replace("Author:", "");
                    s = s.Trim();
                    commit.Author = s;
                }
                else if (s.StartsWith("Date:"))
                {
                    s = s.Replace("Date:", "");
                    s = s.Trim();
                    commit.Date = s;
                }
                else if (s != "")
                {
                    s = s.Trim();
                    if (commit.Summary == "")
                        commit.Summary = s;
                    else
                    {
                        commit.Description += s;
                        while (s != "" && !streamReader.EndOfStream)
                        {
                            s = streamReader.ReadLine();
                            s = s.Trim();
                            commit.Description += " " + s;
                        }
                    }
                }

                if (commit.isFull())
                {
                    textBox1.Text += commit.Author + Environment.NewLine
                        + commit.Date + Environment.NewLine
                        + commit.Summary + Environment.NewLine
                        + commit.Description + Environment.NewLine
                        + commit.CommitText + Environment.NewLine + Environment.NewLine;
                    CommitsList.Add(commit);
                    commit = new Commit();
                }
            }

            authorsListBox.Items.RemoveAt(1);
            foreach (Commit cmt in CommitsList)
            {
                if (authorsListBox.Items.Contains(cmt.Author) == false)
                    authorsListBox.Items.Add(cmt.Author);
            }
        }
    }
}
