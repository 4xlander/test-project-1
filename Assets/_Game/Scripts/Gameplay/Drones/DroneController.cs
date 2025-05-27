using UnityEngine;

namespace Game
{
    public class DroneController : ITickable
    {
        private const float GATHER_TIME = 1f;
        private const float WAIT_TIME = 2f;

        private readonly string _droneId;
        private readonly DroneView _view;
        private readonly DronesModel _model;
        private readonly DroneConfig _config;
        private readonly SpaceResModel _spaceResModel;
        private readonly StationsModel _stationsModel;
        private readonly TickManager _tickManager;

        private float _gatherTimer = 0;
        private float _waitTimer = 0;
        private string _targetResId = string.Empty;

        public DroneController(
            string droneId,
            DroneView view, DronesModel model, DroneConfig config,
            SpaceResModel spaceResModel, StationsModel stationsModel, TickManager tickManager)
        {
            _droneId = droneId;
            _view = view;
            _model = model;
            _config = config;

            _spaceResModel = spaceResModel;
            _stationsModel = stationsModel;

            _tickManager = tickManager;
            _tickManager.Register(this);
        }

        public void Tick()
        {
            ProceedState();
        }

        private void ProceedState()
        {
            var state = _model.GetState(_droneId);
            switch (state)
            {
                case DroneState.Idle:
                    _waitTimer = WAIT_TIME;
                    FindNearestFreeRes();

                    if (!string.IsNullOrEmpty(_targetResId))
                        MoveToTargetRes();
                    break;

                case DroneState.Gather:
                    _gatherTimer -= Time.deltaTime;
                    if (_gatherTimer <= 0)
                    {
                        _gatherTimer = GATHER_TIME;
                        GatherResource();
                    }
                    break;

                case DroneState.Wait:
                    _waitTimer -= Time.deltaTime;
                    if (_waitTimer <= 0)
                    {
                        _waitTimer = WAIT_TIME;
                        MoveBackToStation();
                    }
                    break;

                case DroneState.CargoTransfer:
                    // TODO implement transfer
                    break;
            }
        }

        private void MoveBackToStation()
        {
            _spaceResModel.RemoveRes(_targetResId);

            var stationId = _model.GetStationId(_droneId);
            var stationPos = _stationsModel.GetPosition(stationId);
            _view.MoveTo(stationPos, () => _model.ChangeState(_droneId, DroneState.CargoTransfer));

            _model.ChangeState(_droneId, DroneState.Move);
        }

        private void GatherResource()
        {
            var gatheredValue = _spaceResModel.Gather(_targetResId, _config.GatherSpeedBase);
            if (gatheredValue > 0)
                _model.AddCargo(_droneId, _spaceResModel.GetResType(_targetResId), gatheredValue);
            else
                _model.ChangeState(_droneId, DroneState.Wait);
        }

        private void MoveToTargetRes()
        {
            _spaceResModel.SetTarget(_targetResId);

            _model.SetTargetResource(_droneId, _targetResId);
            _model.ChangeState(_droneId, DroneState.Move);

            var targetPos = _spaceResModel.GetPosition(_targetResId);
            _view.MoveTo(targetPos, () => _model.ChangeState(_droneId, DroneState.Gather));
        }

        private void FindNearestFreeRes()
        {
            var minDistance = float.MaxValue;
            _targetResId = string.Empty;

            foreach (var resId in _spaceResModel.Resources)
            {
                if (!_spaceResModel.IsFree(resId))
                    continue;

                var resPos = _spaceResModel.GetPosition(resId);
                var dronePos = _view.transform.position;

                var dist = Vector3.Distance(dronePos, resPos);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    _targetResId = resId;
                }
            }
        }
    }
}
