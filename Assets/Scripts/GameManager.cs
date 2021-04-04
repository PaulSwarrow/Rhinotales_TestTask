using System;
using DI;
using Model;
using UnityEngine;
using View;

public class GameManager : MonoBehaviour
{
    private DependencyContainer dependencies;
    private GameLevelModel model;
    private GameLevelActor view;

    public event Action InitEvent;
    public event Action DisposeEvent;

    private void Awake()
    {
        dependencies = new DependencyContainer();
        view = FindObjectOfType<GameLevelActor>();
        model = GameLevelModel.Create(view.Read());
        dependencies.Register(model);
        dependencies.Register(view);
        dependencies.Register(GetComponents<BaseGameController>());
        dependencies.InjectDependencies();
            
            
    }
}