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
    private GameLevelView view;
    private BaseGameController[] controllers;


    private void Awake()
    {
        controllers = GetComponents<BaseGameController>();
        dependencies = new DependencyContainer();
        view = FindObjectOfType<GameLevelView>();
        model = GameLevelModel.Create(view.Read());
        dependencies.Register(model);
        dependencies.Register(view);
        dependencies.Register(Camera.main);
        dependencies.RegisterMultiple(controllers);
        dependencies.InjectDependencies();
        
        foreach (var controller in controllers)
        {
            controller.Subscribe();
        }
        
    }
}