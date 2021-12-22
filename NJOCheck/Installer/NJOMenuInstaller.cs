using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using SiraUtil;

namespace NJOCheck.Installer
{
    public class NJOMenuInstaller : Zenject.Installer
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<NJOCheckController>().FromNewComponentOnNewGameObject().AsCached().NonLazy();
        }
    }
}
