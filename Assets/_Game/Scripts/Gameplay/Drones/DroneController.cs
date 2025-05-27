namespace Game
{
    public class DroneController : ITickable
    {
        private readonly string _droneId;
        private readonly DroneView _view;
        private readonly DronesModel _model;
        private readonly DroneConfig _config;
        private readonly SpaceResModel _spaceRes;
        private readonly TickManager _tickManager;

        private float _gatherTime = 0;

        public DroneController(
            string droneId,
            DroneView view, DronesModel model, DroneConfig config,
            SpaceResModel spaceRes, TickManager tickManager)
        {
            _droneId = droneId;
            _view = view;
            _model = model;
            _config = config;
            _spaceRes = spaceRes;
            _tickManager = tickManager;
            _tickManager.Register(this);
        }

        public void Tick()
        {
        }
    }
}
