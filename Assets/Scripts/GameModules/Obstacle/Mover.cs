using UnityEngine;

namespace GameModules.Obstacle
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

        public void PauseMove()
        {
            _isMoving = false;
        }

        public void ResumeMove()
        {
            _isMoving = true;
        }

        private void Update()
        {
            if (!_isMoving) return;
            transform.Translate(_moveSpeed * Time.deltaTime, Space.Self);
        }
    }
}