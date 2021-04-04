using System;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace Ui
{
    [RequireComponent(typeof(Button))]
    public class GameButton : MonoBehaviour
    {
        public event Action<GameButton> ClickEvent;
        private Button btn;

        public bool Active
        {
            set => btn.interactable = value;
        }

        private void Awake()
        {
            btn = GetComponent<Button>();
        }

        private void OnEnable()
        {
            btn.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            ClickEvent?.Invoke(this);
        }

        private void OnDisable()
        {
            btn.onClick.RemoveListener(OnClick);
        }
    }
}