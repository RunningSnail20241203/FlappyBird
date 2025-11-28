using UnityEngine;

namespace GameModules.Pipe
{
    public class Mover : MonoBehaviour
    {
        private Vector2 _moveSpeed;
        private bool _isMoving;

        public void StartMove(Vector2 speed)
        {
            _isMoving = true;
            _moveSpeed = speed;
        }

        public void StopMove()
        {
            _isMoving = false;
        }

        private void Update()
        {
            if (!_isMoving) return;
            transform.Translate(_moveSpeed * Time.deltaTime, Space.Self);
        }
    }
}