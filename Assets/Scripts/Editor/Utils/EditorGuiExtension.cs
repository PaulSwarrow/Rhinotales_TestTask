using UnityEngine;

namespace Editor.Utils
{
    public static class EditorGuiExtension
    {
        public static int DrawSwitcher<T>(int value, params (string name, T value)[] options)
        {
            for (var i = 0; i < options.Length; i++)
            {
                var entry = options[i];

                GUI.enabled = value != i;
                if (GUILayout.Button(entry.name))
                {
                    value = i;
                }
            }
            GUI.enabled = true;
            return value;
        }
    }
}