using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace TestsProjectLoader
{
    public class DllFileViewModel : ObservableObject
    {
        #region Fields

        private DllFileModel m_ModelObj = new DllFileModel();

        private ICommand m_BrowseCommand;

        private ICommand m_RunDllCommand;

        #endregion

        #region Public Properties/Commands

        public string DllPath
        {
            get
            {
                return m_ModelObj.DllPath;
            }
            set
            {
                m_ModelObj.DllPath = value;
                OnPropertyChanged("DllPath");
            }
        }

        public ICommand BrowseCommand
        {
            get
            {
                if (m_BrowseCommand == null)
                {
                    m_BrowseCommand = new RelayCommand(
                                                     a => this.DoBrowseFolder());
                                                     //p => this.CheckCondition());
                }

                return m_BrowseCommand;
            }

            set
            {
                m_BrowseCommand = value;
                OnPropertyChanged("BrowseCommand");
            }
        }

        public ICommand RunDllCommand
        {
            get
            {
                if (m_RunDllCommand == null)
                {
                    m_RunDllCommand = new RelayCommand(
                                                     a => this.RunDll());
                    //p => this.CheckCondition());
                }

                return m_RunDllCommand;
            }

            set
            {
                m_RunDllCommand = value;
                OnPropertyChanged("RunDllCommand");
            }
        }

        /// <summary>
        /// Returns the MSTest.exe directory path.
        /// </summary>
        public string MsTestDirPath
        {
            get
            {
                return VisualStudioPathFinder.GetVisualStudioInstallationDir(VisualStudioPathFinder.VisualStudioVersion.Vs2015);
            }
        }

        #endregion

        #region Private Helpers

        public bool CheckCondition()
        {
            //Check condition here if needed        
            return true;
        }


        private void DoBrowseFolder()
        {
            var filedialog = new System.Windows.Forms.OpenFileDialog();
            DialogResult fresult = filedialog.ShowDialog();

            if (fresult == System.Windows.Forms.DialogResult.OK)
            {
                DllPath = filedialog.FileName;
            }

        }


        private void RunDll()
        {
            var msTestPath = Path.Combine(MsTestDirPath, "mstest.exe");

            //Console.WriteLine("Insert path to test container (DLL file)");
            //var containerPath = Console.ReadLine();

            if (!string.IsNullOrEmpty(DllPath) && string.Equals(Path.GetExtension(DllPath), ".dll"))
            {
                Process myProcess = new Process();

                //ProcessStartInfo myProcessStartInfo = new ProcessStartInfo($"\"{msTestPath}\"", $"/testcontainer:\"{containerPath}\" /test:{testName}");
                ProcessStartInfo myProcessStartInfo = new ProcessStartInfo($"\"{msTestPath}\"", $"/testcontainer:\"{DllPath}\"");

                try

                {

                    myProcess.StartInfo = myProcessStartInfo;

                    myProcess.Start();

                }

                catch (Exception ex)

                {

                    System.Windows.MessageBox.Show(ex.Message);
                }
            }

            else
            {
                System.Windows.MessageBox.Show("The given path is invalid. Please load .dll file.");
            }
        }

        #endregion
    }
}
