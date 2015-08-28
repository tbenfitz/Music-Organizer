using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using TagLib;
using System.Configuration; 

namespace MusicOrganizer
{
    public partial class MusicOrganizer : Form
    {
        public MusicOrganizer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(ConfigurationManager.AppSettings["directory-to-organize-path"]);
            FileInfo[] allFiles = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
            
            foreach (FileInfo fileInfoObject in allFiles)
            {
                try
                {
                    TagLib.File tagLibFile = TagLib.File.Create(fileInfoObject.FullName);

                    DirectoryInfo newDirectoryInfo = new DirectoryInfo(ConfigurationManager.AppSettings["directory-organized-path"] + tagLibFile.Tag.Album);

                    if (newDirectoryInfo.Exists == false)
                    {
                        newDirectoryInfo.Create();
                    }

                    fileInfoObject.MoveTo(ConfigurationManager.AppSettings["directory-organized-path"] + tagLibFile.Tag.Album + @"\" + fileInfoObject.Name);
                }
                catch (UnsupportedFormatException)
                {
                    // --- o well
                }
                catch
                {
                    // --- Fucking shit
                }
            }            
        }
    }
}
