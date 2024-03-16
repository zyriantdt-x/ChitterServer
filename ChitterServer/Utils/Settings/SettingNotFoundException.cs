using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Utils.Settings {
    internal class SettingNotFoundException : Exception {
        internal string Setting { get; }
        internal SettingNotFoundException( string setting ) {
            this.Setting = setting;
        }
    }
}
