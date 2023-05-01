using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ennote
{
    public class Themer
    {
        /// <summary>
        /// Returns Hex if 0==aType<br></br>
        /// Returns Color if 1==aType<br></br>
        /// Returns null if error.
        /// </summary>
        public static object HexColor(int aType, object aValue, bool aUseAlpha=false, string aPrefix="#") {
            if (0 == aType) {
                var c = (Color)aValue;
                string defval = string.Format("{0:X2}{1:X2}{2:X2}", c.R, c.G, c.B),
                       retval = defval,
                       ft = retval.Substring(0, 3);
                if (aUseAlpha)
                    retval = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B);
                else if (ft == retval.Substring(3, 3))
                    retval = ft;
                return aPrefix + retval;
            }
            string colorHex = ("" + aValue).Replace("#", "").Trim();
            if (3 == colorHex.Length)
                colorHex = colorHex + colorHex;
            if (aUseAlpha && 6 == colorHex.Length)
                colorHex = "FF" + colorHex;
            try {
                return Color.FromArgb(Convert.ToInt32("0x" + colorHex, 16));
            } catch { }
            return null;
        }
        // System.Web.Extensions.dll
        public static System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

        public static object[] GetCurrentTheme() {
            var xList = new List<object>();
            foreach (var form in Form1.FormsDict) {
                foreach (Control c in form.Value.Controls) {
                    if (c is ContextMenuStrip)
                        continue;
                    xList.Add(c.Name + "@" + form.Key + "=" + HexColor(0, c.BackColor, false, "") + "," + HexColor(0, c.ForeColor, false, ""));
                }
            }
            return xList.ToArray();
        }
        public static void ImportTheme(object[] aJTheme) {
            int maxLen = aJTheme.Length;
            for (int i = 0; i < maxLen; i++) {
                // first divide by separator "="
                var nameValue = ("" + aJTheme[i]).Split('=');
                if (nameValue.Length < 1) continue;
                // next divide by separator "@"
                var nv0cf = nameValue[0].Split('@');
                if (nv0cf.Length < 1) continue;
                // finally divide by separator ","
                var nv1bf = nameValue[1].Split(',');
                string nv0Form = nv0cf[1],
                    nv0Ctrl = nv0cf[0],
                    nv1Bg = nv1bf[0],
                    nv1Fg = nv1bf[1];
                var cf = Form1.FormsDict[nv0Form];
                var k = cf.Controls[nv0Ctrl];
                Color bg = (Color)HexColor(1, nv1Bg, true),
                      fg = (Color)HexColor(1, nv1Fg, true);
                k.BackColor = bg;
                k.ForeColor = fg;
            }
        }
    }
}