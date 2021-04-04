using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class UiPointTypeSelector : MonoBehaviour
    {
        private LocalFactory<GameButton> btnFactory;
        private List<GameButton> btns;

        public int CurrentSelection { get; private set; } = -1;
        private void Awake()
        {
            btns = new List<GameButton>();
            btnFactory = new LocalFactory<GameButton>(transform);
        }
        
        public void DrawOptions(params (string name, NavigationPoint value)[] options)
        {
            for (var i = 0; i < options.Length; i++)
            {
                var entry = options[i];
                var btn = btnFactory.Create();
                btn.GetComponentInChildren<Text>().text = entry.name;
                btn.ClickEvent += OnSelect;
                btns.Add(btn);
            }
            OnSelect(btns.First());
        }

        private void OnSelect(GameButton button)
        {
            if (CurrentSelection >= 0) btns[CurrentSelection].Active = true;
            CurrentSelection = btns.IndexOf(button);
            button.Active = false;
        }
    }
}