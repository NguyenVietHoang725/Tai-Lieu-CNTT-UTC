using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDrives();
        }

        private void LoadDrives()
        {
            cbDrive.Items.Clear();

            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                if (drive.DriveType == DriveType.Fixed)
                {
                    cbDrive.Items.Add(drive.Name);
                }
            }
        }

        private void LoadRootFolders(string drivePath)
        {
            treeFolders.Nodes.Clear();

            try
            {
                string[] dirs = Directory.GetDirectories(drivePath);

                foreach (string dir in dirs)
                {
                    TreeNode node = new TreeNode(Path.GetFileName(dir));
                    node.Tag = dir;

                    if (HasSubDirectory(dir))
                    {
                        node.Nodes.Add("...");
                    }

                    treeFolders.Nodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private bool HasSubDirectory(string path)
        {
            try
            {
                return Directory.GetDirectories(path).Length > 0;
            }
            catch { return false; }
        }

        private void cbDrive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDrive = cbDrive.SelectedItem.ToString();
            LoadRootFolders(selectedDrive);
        }

        private void treeFolders_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            node.Nodes.Clear();

            string path = node.Tag.ToString();

            try
            {
                string[] subDirs = Directory.GetDirectories(path);

                foreach (string subDir in subDirs)
                {
                    TreeNode subNode = new TreeNode(Path.GetFileName(subDir));
                    subNode.Tag = subDir;

                    if (HasSubDirectory(subDir))
                    {
                        subNode.Nodes.Add("...");
                    }
                    node.Nodes.Add(subNode);
                }
            }
            catch { }
        }

        private void treeFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string selectedPath = e.Node.Tag.ToString();
            LoadFiles(selectedPath);
        }

        private void LoadFiles(string folderPath)
        {
            lstFiles.Items.Clear();

            try
            {
                string[] files = Directory.GetFiles(folderPath);
                foreach (string file in files)
                {
                    lstFiles.Items.Add(Path.GetFileName(file));
                }
            }
            catch (UnauthorizedAccessException)
            {
                lstFiles.Items.Add("[Không có quyền truy cập]");
            }
            catch (Exception ex)
            {
                lstFiles.Items.Add("[Lỗi]: " + ex.Message);
            }
        }

        private void lstFiles_DoubleClick(object sender, EventArgs e)
        {
            if (lstFiles.SelectedItem == null) return;

            string currentFolder = treeFolders.SelectedNode?.Tag.ToString();

            string fileName = lstFiles.SelectedItem.ToString();
            string filePath = Path.Combine(currentFolder, fileName);

            PlayMusic(filePath);
            LoadLyrics(filePath);
        }

        private void PlayMusic(string path)
        {
            wmpPlayer.URL = path;
            wmpPlayer.Ctlcontrols.play();
        }

        private void LoadLyrics(string mp3Path)
        {
            rtbLyrics.Clear();

            string txtPath = Path.ChangeExtension(mp3Path, ".txt");
            if (File.Exists(txtPath))
            {
                string lyrics = File.ReadAllText(txtPath);
                rtbLyrics.Text = lyrics;
            }
            else
            {
                rtbLyrics.Text = "Không tìm thấy lời bài hát.";
            }
        }
    }
}
