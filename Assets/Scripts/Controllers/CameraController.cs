using DI;
using UnityEngine;

namespace Controllers
{
    public class CameraController : BaseGameController
    {
        [Inject] private Camera camera;
        [SerializeField] private float minZ = 10;
        [SerializeField] private float maxZ = 100;
        [SerializeField] private float MoveSpeed = 10;
        [SerializeField] private float ZoomSpeed = 10;

        private float zoomVelocity = 0;

        private void Update()
        {
            var inputMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * MoveSpeed;

            float desiredZoomVelocity = 0;
            if (Input.GetKey(KeyCode.KeypadPlus))
            {
                desiredZoomVelocity = -ZoomSpeed;
            }
            else if (Input.GetKey(KeyCode.KeypadMinus))
            {
                desiredZoomVelocity = ZoomSpeed;
            }

            var altitude = camera.transform.position.y;
            zoomVelocity = Mathf.Lerp(zoomVelocity, desiredZoomVelocity, 8 * Time.deltaTime);
            zoomVelocity = Mathf.Clamp(zoomVelocity, minZ - altitude, maxZ - altitude);

            var translate = (inputMove + Vector3.up * zoomVelocity) * Time.deltaTime;

            camera.transform.Translate(translate, Space.World);
        }
    }
}