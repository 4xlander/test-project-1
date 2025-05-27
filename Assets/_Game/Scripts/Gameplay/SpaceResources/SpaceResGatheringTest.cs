using UnityEngine;

namespace Game
{
    public class SpaceResGatheringTest : ITickable
    {
        private readonly SpaceResModelService _modelService;

        private SpaceResModel _targetResource;
        private const float GATHER_TIME = 2f;
        private float _gatherTimer = 2;

        public SpaceResGatheringTest(
            SpaceResModelService modelService)
        {
            _modelService = modelService;
        }

        public void Tick()
        {
            _gatherTimer -= Time.deltaTime;
            if (_gatherTimer > 0)
                return;

            _gatherTimer = GATHER_TIME;

            _targetResource ??= FindNearestTarget();

            if (_targetResource != null)
            {
                var gatheredValue = _targetResource.Gather(15);
                Debug.Log($"Gathered value = {gatheredValue}");

                if (_targetResource.Amount <= 0)
                {
                    Debug.Log("Stop gathering");
                    _targetResource = null;
                }
            }
        }

        private SpaceResModel FindNearestTarget()
        {
            Debug.Log("Find resource");

            SpaceResModel result = null;
            var minDistance = float.MaxValue;

            foreach (var model in _modelService.Models)
            {
                if (model.State != SpaceResState.Idle)
                    continue;

                var distance = Vector3.Distance(Vector3.zero, model.Position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    result = model;
                }
            }

            Debug.Log($"New target distance = {minDistance}");
            return result;
        }
    }
}
