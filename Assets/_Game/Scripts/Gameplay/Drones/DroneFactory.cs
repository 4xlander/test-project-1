using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class DroneFactory
    {
        private readonly Dictionary<DroneType, DroneConfig> _droneConfigsMap;
        private readonly DronesModel _dronesModel;
        private readonly SpaceResModel _spaceResModel;
        private readonly StationsModel _stationsModel;
        private readonly TickManager _tickManager;

        public DroneFactory(
            IEnumerable<DroneConfig> droneConfigs, DronesModel dronesModel,
            SpaceResModel spaceResModel, StationsModel stationsModel, TickManager tickManager)
        {
            _droneConfigsMap = droneConfigs.ToDictionary(c => c.DroneType, c => c);
            _dronesModel = dronesModel;
            _spaceResModel = spaceResModel;
            _stationsModel = stationsModel;
            _tickManager = tickManager;
        }

        /// <summary>
        /// Creates Basic Drone
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="parent"></param>
        /// <returns> Drone Id </returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public string CreateBasicDrone(string stationId, Transform parent, Color color)
        {
            if (!_droneConfigsMap.TryGetValue(DroneType.Basic, out var config))
                throw new KeyNotFoundException($"{typeof(DroneFactory)}: Config with Key '{DroneType.Basic}' not found!");

            var droneView = Object.Instantiate(config.Prefab, parent);
            droneView.SetColor(color);

            var droneId = _dronesModel.AddDrone(stationId);
            new DroneController(
                droneId, droneView, _dronesModel, config, _spaceResModel, _stationsModel, _tickManager).Init();

            return droneId;
        }
    }
}
