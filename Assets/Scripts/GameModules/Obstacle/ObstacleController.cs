using UnityEngine;

namespace GameModules.Pipe
{
    public class ObstacleController : MonoBehaviour
    {
        [SerializeField] private RectTransform upObstacle;
        [SerializeField] private RectTransform downObstacle;
        [SerializeField] private OutScreenChecker checker;
        [SerializeField] private Mover mover;

        private IObstacleSpawner _obstacleSpawner;
        private bool _recycled;

        public void Initialize(float rowGap, IObstacleSpawner spawner)
        {
            upObstacle.anchoredPosition = new Vector2(0, rowGap / 2f);
            downObstacle.anchoredPosition = new Vector2(0, -rowGap / 2f);
            _obstacleSpawner = spawner;
            _recycled = false;
        }

        public void StartMove(Vector2 moveSpeed)
        {
            mover.StartMove(moveSpeed);
        }

        public void StopMove()
        {
            mover.StopMove();
        }

        private void Update()
        {
            if (_recycled || !checker.Check()) return;

            _recycled = true;
            _obstacleSpawner.Recycle(gameObject);
        }
    }
}