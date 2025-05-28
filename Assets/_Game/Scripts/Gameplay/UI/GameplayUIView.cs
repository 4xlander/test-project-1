using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameplayUIView : MonoBehaviour
    {
        public event Action<float> OnMaxDronesCountChanged;
        public event Action<float> OnDroneSpeedChanged;
        public event Action<float> OnSpaceResSpawnIntervalChanged;
        public event Action<bool> OnDronePathVisibilityChanged;

        [SerializeField] private Slider _maxDronesCount;
        [SerializeField] private Slider _droneSpeed;
        [SerializeField] private TMP_InputField _spaceResSpawnInterval;
        [SerializeField] private Toggle _showDronePath;
        [Space]
        [SerializeField] private Transform _resInfoContainer;
        [SerializeField] private ResourceInfoView _resInfoTemplate;

        private Dictionary<string, ResourceInfoView> _resourceInfoMap = new();
        private int _stationNumber;

        private void Start()
        {
            _maxDronesCount.onValueChanged.AddListener(MaxDronesCount_OnChanged);
            _droneSpeed.onValueChanged.AddListener(DroneSpeed_OnValueChanged);
            _spaceResSpawnInterval.onEndEdit.AddListener(SpawnInterval_OnEditEnd);
            _spaceResSpawnInterval.characterValidation = TMP_InputField.CharacterValidation.Decimal;
            _showDronePath.onValueChanged.AddListener(ShowDronePath_OnToggle);
        }

        private void ShowDronePath_OnToggle(bool arg0)
        {
            OnDronePathVisibilityChanged?.Invoke(arg0);
        }

        public void SetDroneSpeed(float normalizedValue)
        {
            var value = Mathf.Clamp01(normalizedValue);
            _droneSpeed.value = value * _droneSpeed.maxValue;
        }

        public void SetSpawnInterval(float value)
        {
            _spaceResSpawnInterval.SetTextWithoutNotify(value.ToString());
        }

        public void AddResourceInfo(string stationId, float amount)
        {
            var infoView = Instantiate(_resInfoTemplate, _resInfoContainer);
            infoView.SetAmount(amount);
            infoView.SetCaption($"Station {++_stationNumber} Resources");
            infoView.gameObject.SetActive(true);

            _resourceInfoMap[stationId] = infoView;
        }

        public void UpdateResourceInfo(string staionId, float amount)
        {
            if (_resourceInfoMap.TryGetValue(staionId, out var infoView))
                infoView.SetAmount(amount);
        }

        private void SpawnInterval_OnEditEnd(string arg0)
        {
            if (float.TryParse(_spaceResSpawnInterval.text, out var value))
                OnSpaceResSpawnIntervalChanged?.Invoke(value);
        }

        private void MaxDronesCount_OnChanged(float arg0)
        {
            OnMaxDronesCountChanged?.Invoke(arg0);
        }

        private void DroneSpeed_OnValueChanged(float arg0)
        {
            var speedNormalized = arg0 / _droneSpeed.maxValue;
            OnDroneSpeedChanged?.Invoke(speedNormalized);
        }
    }
}
