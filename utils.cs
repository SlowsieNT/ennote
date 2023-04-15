using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ennote
{
    public class utils
    {
        
        [System.Runtime.InteropServices.DllImport("Shell32.dll")]
        private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

        private const int SHCNE_ASSOCCHANGED = 0x8000000;
        private const int SHCNF_FLUSH = 0x1000;
        public static void RegisterFileAssoc(string aFileExt = ".rte", string aProgName = "Encrypted Note", string aFileTypeDesc = "Encrypted Note", string aProgramPath = "")
        {
            if ("" == aProgramPath) aProgramPath = Application.ExecutablePath;
            bool anychange = false;
            anychange |= RegSetDefKVal(@"Software\Classes\" + aFileExt, aProgName);
            anychange |= RegSetDefKVal(@"Software\Classes\" + aProgName, aFileTypeDesc);
            anychange |= RegSetDefKVal(@"Software\Classes\" + aProgName + @"\shell\open\command", "\"" + aProgramPath + "\" \"%1\"");
            if (anychange)
                SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_FLUSH, IntPtr.Zero, IntPtr.Zero);
        }
        private static bool RegSetDefKVal(string aKey, string aValue, RegistryKey aKeyRoot = null)
        {
            if (aKeyRoot == null) aKeyRoot = Registry.CurrentUser;
            using (var key = aKeyRoot.CreateSubKey(aKey)) {
                if ((string)key.GetValue(null) != aValue) {
                    key.SetValue(null, aValue);
                    return true;
                }
            }
            return false;
        }
    }
}
