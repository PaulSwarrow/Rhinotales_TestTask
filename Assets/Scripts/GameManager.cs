using System;
using DI;
using Model;
using UnityEngine;
using View;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{
    private DependencyContainer dependencies;
    private GameLevelModel model;
    private GameLevelActor view;


    private void Awake()
    {
        dependencies = new DependencyContainer();
        view = FindObjectOfType<GameLevelActor>();
        model = GameLevelModel.Create(view.Read());
        dependencies.Register(model);
        dependencies.Register(view);
        dependencies.Register(Camera.main);
        dependencies.RegisterMultiple(GetComponents<BaseGameController>());
        dependencies.InjectDependencies();
        
    }
}