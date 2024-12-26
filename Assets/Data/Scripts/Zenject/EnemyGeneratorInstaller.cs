using UnityEngine;
using Zenject;

public class EnemyGeneratorInstaller : MonoInstaller
{
    [SerializeField] private EnemyGenerator _enemyGenerator;
    public override void InstallBindings()
    {
        Container.Bind<EnemyGenerator>().FromInstance(_enemyGenerator).AsSingle().NonLazy();
    }
}