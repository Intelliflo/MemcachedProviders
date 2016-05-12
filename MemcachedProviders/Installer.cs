using System.Collections;
using System.ComponentModel;

namespace MemcachedProviders
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            SessionCounters.Create();
            CacheCounters.Create();
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);

            SessionCounters.Remove();
            CacheCounters.Remove();
        }
    }
}
