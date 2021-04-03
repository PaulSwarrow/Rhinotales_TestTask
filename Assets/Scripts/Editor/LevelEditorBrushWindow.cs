using Editor.Utils;
using UnityEditor;

namespace Editor
{
    public class LevelEditorBrushWindow : EditorWindow
    {

        [MenuItem ("LevelEditor/BrushWindow")]

        public static void  ShowWindow () {
            GetWindow(typeof(LevelEditorBrushWindow));
        }
        
        private enum BrushMode
        {
            DrawWalkable,
            DrawObstacles,
            Erase
        
        }
        
        private static (string name, BrushMode mode)[] BrushOptions = {
            ("Draw walkable", BrushMode.DrawWalkable),
            ("Draw obstacles", BrushMode.DrawObstacles),
            ("Erase", BrushMode.Erase)
        };
    
        private bool active;
        private int brushMode;
        void OnGUI () {
            
            active = EditorGUILayout.Toggle("Brush active", active);
            brushMode = EditorTools.DrawSwitcher(brushMode, BrushOptions);

        }
    }
}