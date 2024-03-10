using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Utils.Settings {
    internal class SettingNotFoundException : Exception {
        internal SettingNotFoundException( string setting ) : base( $"Attempt to locate non-existing setting '{setting}'" ) { }
    }
}
