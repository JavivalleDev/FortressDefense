using UnityEngine;

namespace Assets.GameAssets._Scripts.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private int _iBoundary = 50;
        [SerializeField] private int _iMovSpeed = 5;

        private int _iWidth;
        private int _iHeight;

        private void Update()
        {
            _iWidth = Screen.width;
            _iHeight = Screen.height;
            Vector3 pos = Input.mousePosition;

            if (pos.x > _iWidth - _iBoundary)
                transform.position += Vector3.right * _iMovSpeed * Time.unscaledDeltaTime;

            if (pos.x < _iBoundary)
                transform.position += Vector3.left * _iMovSpeed * Time.unscaledDeltaTime;

            if (pos.y > _iHeight - _iBoundary)
                transform.position += Vector3.forward * _iMovSpeed * Time.unscaledDeltaTime;

            if (pos.y < _iBoundary)
                transform.position += Vector3.back * _iMovSpeed * Time.unscaledDeltaTime;
        }
    }
}
