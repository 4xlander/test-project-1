using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "SpaceResSpawnerConfig", menuName = "Config/SpaceResSpawnerConfig")]
    public class SpaceResSpawnerConfig : ScriptableObject
    {
        [SerializeField, Range(0, 10)] private float _spawnInterval = 3f;
        [SerializeField, Range(5, 20)] private float _spawnRadius = 20f;
        [SerializeField] private uint _maxCount = 50;
        [Space]
        [SerializeField] private SpaceResConfig[] _spaceResources;

        public float SpawnInterval => _spawnInterval;
        public float SpawnRadius => _spawnRadius;
        public uint MaxCount => _maxCount;
        public IReadOnlyList<SpaceResConfig> SpaceResources => _spaceResources;
    }
}
