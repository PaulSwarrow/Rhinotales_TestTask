using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Model;

namespace DefaultNamespace
{
    public class GameLevelConfig : ScriptableObject
    {
        [SerializeField] public List<CellModel> data;
    }
}