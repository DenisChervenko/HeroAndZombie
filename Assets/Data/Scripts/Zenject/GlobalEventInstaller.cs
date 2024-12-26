using UnityEngine;
using Zenject;

public class GlobalEventInstaller : MonoInstaller
{
    [SerializeField] private GlobalEvent _globalEvent;
    public override void InstallBindings()
    {
        Container.Bind<GlobalEvent>().FromInstance(_globalEvent).AsSingle().NonLazy();
    }
}