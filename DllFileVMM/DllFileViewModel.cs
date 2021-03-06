﻿using DllFileVMM.HelperClasses;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;

namespace DllFileVMM
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
                    m_BrowseCommand = new RelayCommand(a => this.DoBrowseFolder());
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
                    m_RunDllCommand = new RelayCommand(a => this.RunDll());
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
        
        private void DoBrowseFolder()
        {
            var filedialog = new OpenFileDialog();
            DialogResult fresult = filedialog.ShowDialog();

            if (fresult == System.Windows.Forms.DialogResult.OK)
            {
                DllPath = filedialog.FileName;
            }

        }


        private void RunDll()
        {
            var msTestPath = Path.Combine(MsTestDirPath, "mstest.exe");

            if (!string.IsNullOrEmpty(DllPath) && string.Equals(Path.GetExtension(DllPath), ".dll"))
            {
                Process myProcess = new Process();

                ProcessStartInfo myProcessStartInfo = new ProcessStartInfo($"\"{msTestPath}\"", $"/testcontainer:\"{DllPath}\"");

                try

                {

                    myProcess.StartInfo = myProcessStartInfo;

                    myProcess.Start();

                }

                catch (Exception ex)

                {

                    MessageBox.Show(ex.Message);
                }
            }

            else
            {
                MessageBox.Show("The given path is invalid. Please load .dll file.");
            }
        }

        #endregion
    }
}
