using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class FieldMarkView : MonoBehaviour
    {
        [SerializeField] private Text label;

        public string Label
        {
            get => label.text;
            set => label.text = value;
        }
    }
}