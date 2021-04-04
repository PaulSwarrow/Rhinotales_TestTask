using System;
using Ui;

namespace Controllers
{
    public class UserInputController : BaseGameController
    {
        private enum PointType
        {
            A = 1,
            B = 2
        }

        private static readonly (string name, PointType value)[] options = new (string name, PointType value)[]
        {
            ("Set A", PointType.A),
            ("Set B", PointType.B),
        };

        private PointType currentPointType;

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }

        private void OnGUI()
        {
            currentPointType = (PointType) GuiExtension.DrawSwitcher((int) currentPointType, options);
            
        }
    }
}