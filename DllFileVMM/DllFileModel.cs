using System.ComponentModel;

namespace DllFileVMM
{
    public class DllFileModel
    {
        private string m_DllPath = string.Empty;

        public string DllPath
        {
            get
            {
                return m_DllPath;
            }
            set
            {
                if (!string.Equals(value, m_DllPath))
                {
                    m_DllPath = value;
                }
            }
        }
    }
}
