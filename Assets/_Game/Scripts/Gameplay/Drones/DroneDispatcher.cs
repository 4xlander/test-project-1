using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class DroneDispatcher : ITickable
    {
        private const float ASSIGN_INTERVAL = 1f;
        private readonly DronesModel _dronesModel;
        private readonly SpaceResModel _spaceResModel;
        private readonly TickManager _tickManager;

        private readonly Dictionary<string, string> _assignMap = new();
        private float _assignTimer = 0f;

        public DroneDispatcher(DronesModel dronesModel, SpaceResModel spaceResModel, TickManager tickManager)
        {
            _dronesModel = dronesModel;
            _spaceResModel = spaceResModel;
            _tickManager = tickManager;
        }

        public void Init()
        {
            _spaceResModel.OnResRemoved += OnResourceRemoved;
            _dronesModel.OnDroneRemoved += OnDroneRemoved;
            _tickManager.Register(this);
        }

        public void Tick()
        {
            _assignTimer -= Time.deltaTime;
            if (_assignTimer <= 0)
            {
                _assignTimer = ASSIGN_INTERVAL;
                AssignDrones();
            }
        }

        public void AssignDrones()
        {
            var availableDrones = GetAvailableDrones();
            var freeResources = GetFreeResources();
            AssignResourcesToNearestDrones(availableDrones, freeResources);
        }

        private List<string> GetAvailableDrones()
        {
            return _dronesModel.Drones
                .Where(droneId =>
                {
                    var state = _dronesModel.GetState(droneId);
                    return state == DroneState.Idle;
                })
                .ToList();
        }

        private List<string> GetFreeResources()
        {
            return _spaceResModel.Resources
                .Where(resId => _spaceResModel.IsFree(resId))
                .ToList();
        }

        private void AssignResourcesToNearestDrones(List<string> availableDrones, List<string> freeResources)
        {
            if (freeResources.Count == 0 || availableDrones.Count == 0)
                return;

            var assignments = new List<ResourceDroneAssignment>();

            foreach (var resId in freeResources)
            {
                var resPos = _spaceResModel.GetPosition(resId);

                foreach (var droneId in availableDrones)
                {
                    if (_assignMap.Keys.Contains(droneId))
                        _assignMap.Remove(droneId);

                    var dronePos = _dronesModel.GetPosition(droneId);
                    var distance = Vector3.Distance(dronePos, resPos);

                    assignments.Add(new ResourceDroneAssignment
                    {
                        ResourceId = resId,
                        DroneId = droneId,
                        Distance = distance
                    });
                }
            }

            assignments.Sort((a, b) => a.Distance.CompareTo(b.Distance));

            foreach (var assignment in assignments)
            {
                if (_assignMap.Keys.Contains(assignment.DroneId) ||
                    _assignMap.Values.Contains(assignment.ResourceId))
                    continue;

                AssignDroneToResource(assignment.DroneId, assignment.ResourceId);
                _assignMap[assignment.DroneId] = assignment.ResourceId;
            }
        }

        private void AssignDroneToResource(string droneId, string resourceId)
        {
            _spaceResModel.Reserve(resourceId);
            _dronesModel.SetTargetResource(droneId, resourceId);
        }

        private void OnResourceRemoved(string resourceId)
        {
            var affectedDrones = _dronesModel.Drones
                .Where(droneId => _dronesModel.GetTargetResource(droneId) == resourceId)
                .ToList();

            foreach (var droneId in affectedDrones)
            {
                _dronesModel.SetTargetResource(droneId, null);
                _assignMap.Remove(droneId);
            }

            AssignDrones();
        }

        private void OnDroneRemoved(string droneId)
        {
            if (_assignMap.TryGetValue(droneId, out var resourceId))
            {
                _spaceResModel.Unreserve(resourceId);
                _assignMap.Remove(droneId);
            }

            AssignDrones();
        }

        public void Dispose()
        {
            _spaceResModel.OnResRemoved -= OnResourceRemoved;
            _dronesModel.OnDroneRemoved -= OnDroneRemoved;
            _tickManager.Unregister(this);
        }
    }

    internal class ResourceDroneAssignment
    {
        public string ResourceId { get; set; }
        public string DroneId { get; set; }
        public float Distance { get; set; }
    }
}