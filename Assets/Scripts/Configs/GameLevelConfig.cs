using System.Collections.Generic;
using Model;
using UnityEngine;

namespace Configs
{
    public class GameLevelConfig : ScriptableObject
    {
        [SerializeField] public List<CellModel> data;
    }
}