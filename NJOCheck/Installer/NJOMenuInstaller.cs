using Zenject;

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
