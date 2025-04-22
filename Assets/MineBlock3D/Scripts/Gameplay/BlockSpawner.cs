using System;
using System.Linq;
using MineBlock3D.Scripts.Service;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace MineBlock3D.Scripts.Gameplay
{
    public class BlockSpawner : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private TextMeshPro[] _sidesText;
        [Space]
        [SerializeField] private float _horizontalRange;
        [SerializeField] private float _inputMultiplier;
        [SerializeField] private Vector3 _spawnVelocity;

        public UnityEvent BlockSpawned;
        
        public IFactory Factory { get; private set; }
        public IAssets Assets { get; private set; }
        public IInput Input { get; private set; }

        public BlockInfo Current => Assets.GetBlockInfo(_currentCost);

        private int _currentCost;
        
        public void Construct(IAssets assets, IFactory factory, IInput input)
        {
            Factory = factory;
            Assets = assets;
            Input = input;
            
            Reblock();
        }

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var createdBlock = Factory.CreateBlock(_currentCost, transform.position, Quaternion.identity);

            createdBlock.Rigidbody.linearVelocity = _spawnVelocity;
            createdBlock.EnableLineMovement();

            Reblock();
            
            BlockSpawned?.Invoke();
        }

        private void Reblock()
        {
            _currentCost = Random.Range(0, 4);

            _renderer.sharedMaterial = Current.material;

            foreach (var sideText in _sidesText)
                sideText.text = Extentions.FormatToShort(_currentCost);
        }

        private void Update()
        {
            if (Game.IsActive == false) return;

            if (Input.Fire == InputState.Pressed || Input.Fire == InputState.Up)
                transform.position = Vector3.right * Mathf.Clamp(Input.Horizontal * _inputMultiplier, -1f, 1f) * _horizontalRange;

            if (Input.Fire == InputState.Up)
                Spawn();
        }

        private int CalculateNewCost()
        {
            var blocks = FindObjectsByType<Block>(FindObjectsSortMode.None)
                .Where( b => b.Cost < 4)
                .OrderBy( b => Vector3.Distance(b.transform.position, transform.position) )
                .ToArray();

            var blocksClampedCount = Mathf.Min(5, blocks.Length);
            
            var index = Random.Range(0, blocksClampedCount + 20);

            if (index < blocksClampedCount)
                return blocks[index].Cost;
            else
                return Random.Range(0,5);
        }
    }
}