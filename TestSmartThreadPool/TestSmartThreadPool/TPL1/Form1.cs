using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace TPL1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnProcessImages_Click(object sender, EventArgs e)
        {
            // in main UI thread
            ProcessFiles();
        }

        private void btnProcessImages_Parallel_Click(object sender, EventArgs e)
        {
            // in main UI thread
            ProcessFiles_Parallel();
        }

        private void btnProcessImages_Task_Click(object sender, EventArgs e)
        {
            string strid = Thread.CurrentThread.ManagedThreadId.ToString();
            MessageBox.Show("Main thread = "+strid);

            // async manner: in 2nd thread
            Task.Factory.StartNew(
                () =>
                {
                    ProcessFiles_Parallel();
                }
                );
        }

        private void ProcessFiles()
        {
            // Load up all *.jpg files, and make a new folder for the modified data.
            string[] files = Directory.GetFiles
               (@"F:\opensim-0.7.5-source\huyu", "*.JPG", SearchOption.AllDirectories);
            string newDir = @"F:\opensim-0.7.5-source\huyu_blocking";
            Directory.CreateDirectory(newDir);

            // Process the image data in a blocking manner.
            foreach (string currentFile in files)
            {
                string filename = Path.GetFileName(currentFile);
                using (Bitmap bitmap = new Bitmap(currentFile))
                {
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(Path.Combine(newDir, filename));

                    this.Text = string.Format("Processing {0} on thread {1}", filename,
                       Thread.CurrentThread.ManagedThreadId);  // main thread
                }
            }

            this.Text = "All done!";
        }

        private void ProcessFiles_Parallel()
        {
            string strid = Thread.CurrentThread.ManagedThreadId.ToString();
            MessageBox.Show("Current thread = "+strid);

            // Load up all *.jpg files, and make a new folder for the modified data.
            string[] files = Directory.GetFiles
               (@"F:\opensim-0.7.5-source\huyu", "*.JPG", SearchOption.AllDirectories);
            string newDir = @"F:\opensim-0.7.5-source\huyu_nonblocking";
            Directory.CreateDirectory(newDir);

            // Process the image data in a parallel manner!
            Parallel.ForEach(files, 
                currentFile =>
            {
                string filename = Path.GetFileName(currentFile);
                using (Bitmap bitmap = new Bitmap(currentFile))
                {       
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(Path.Combine(newDir, filename));
                    
                    this.Text = string.Format("Process {0} on thread {1}.ThreadPool={2}.IsBack={3}", filename,
                       Thread.CurrentThread.ManagedThreadId,
                       Thread.CurrentThread.IsThreadPoolThread,
                       Thread.CurrentThread.IsBackground);
                }
            }
            );

            this.Text = "All done!";
        }

    }
}
