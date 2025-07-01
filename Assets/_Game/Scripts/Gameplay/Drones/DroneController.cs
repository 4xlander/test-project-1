namespace Game
{
    public class DroneController : ITickable
    {
        private DroneView _view;
        private readonly string _droneId;
        private readonly DronesModel _model;
        private readonly DroneConfig _config;
        private readonly SpaceResModel _resources;
        private readonly StationsModel _stations;
        private readonly TickManager _tickManager;

        private Fsm _fsm;

        public DroneController(
            string droneId,
            DroneView view, DronesModel model, DroneConfig config,
            SpaceResModel resources, StationsModel stations, TickManager tickManager)
        {
            _droneId = droneId;
            _view = view;
            _model = model;
            _config = config;
            _resources = resources;
            _stations = stations;
            _tickManager = tickManager;
        }

        public void Init()
        {
            InitFsm();
            InitView();
            InitSubscribes();
        }

        private void InitFsm()
        {
            _fsm = new();

            _fsm.AddState(new DroneIdleState(_fsm, _view, _model, _resources, _droneId));
            _fsm.AddState(new MoveToResourceState(_fsm, _view, _model, _resources, _droneId));
            _fsm.AddState(new GatheringState(_fsm, _view, _model, _config, _resources, _droneId));
            _fsm.AddState(new GatheringEndState(_fsm, _model, _droneId));
            _fsm.AddState(new MoveToStationState(_fsm, _view, _model, _stations, _droneId));
            _fsm.AddState(new CargoTransferState(_fsm, _view, _model, _stations, _droneId));

            _fsm.SetState<DroneIdleState>();
        }

        private void InitView()
        {
            _view.SetSpeed(_config.MoveSpeedBase);
            _view.SetPathVisibility(_model.GetPathVisibility());
            _view.Tick();
        }

        public void Tick()
        {
            if (_view == null)
                return;

            _fsm.Update();
            _view.Tick();
            _model.SetPosition(_droneId, _view.transform.position);
        }

        private void Model_OnPathVisibilityChanged()
        {
            _view?.SetPathVisibility(_model.GetPathVisibility());
        }

        private void Model_OnSpeedChanged(string obj)
        {
            if (obj != _droneId)
                return;

            var speed = _model.GetSpeed(obj);
            _view?.SetSpeed(_config.MoveSpeedBase * speed);
        }

        private void Model_OnDroneRemoved(string obj)
        {
            if (obj != _droneId)
                return;

            RemoveSubscribes();

            _view?.Destroy();
            _view = null;
        }

        private void InitSubscribes()
        {
            _model.OnSpeedChanged += Model_OnSpeedChanged;
            _model.OnDroneRemoved += Model_OnDroneRemoved;
            _model.OnPathVisibilityChanged += Model_OnPathVisibilityChanged;

            _tickManager.Register(this);
        }

        private void RemoveSubscribes()
        {
            _model.OnSpeedChanged -= Model_OnSpeedChanged;
            _model.OnDroneRemoved -= Model_OnDroneRemoved;
            _model.OnPathVisibilityChanged -= Model_OnPathVisibilityChanged;

            _tickManager.Unregister(this);
        }
    }
}
