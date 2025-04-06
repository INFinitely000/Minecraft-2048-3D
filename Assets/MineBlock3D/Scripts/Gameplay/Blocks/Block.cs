using System;
using System.Collections;
using System.Linq;
using MineBlock3D.Scripts.Service;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MineBlock3D.Scripts.Gameplay
{
    public class Block : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [Space]
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private TextMeshPro[] _sidesText;
    
        public int Cost { get; private set; }
        public IFactory Factory { get; private set; }
        public BlockInfo BlockInfo { get; private set; }
        public bool IsLineMovementEnabled { get; private set; }
        
        public void Construct(int cost, BlockInfo blockInfo, IFactory factory)
        {
            Cost = cost;
            Factory = factory;
            BlockInfo = blockInfo;

            _renderer.sharedMaterial = blockInfo.material;

            foreach (var sideText in _sidesText)
                sideText.text = Extentions.FormatToShort(cost);
        }

        public void EnableLineMovement()
        {
            if (IsLineMovementEnabled) return;

            Rigidbody.useGravity = false;
            Rigidbody.mass = 3f;
            IsLineMovementEnabled = true;
        }

        public void EnablePulseAnimation()
        {
            StartCoroutine(PlayPulseAnimation());
        }

        public void DisableLineMovement()
        {
            if (IsLineMovementEnabled == false) return;

            Rigidbody.useGravity = true;
            Rigidbody.mass = 1f;
            IsLineMovementEnabled = false;
        }

        private void OnCollisionEnter(Collision other) =>
            DisableLineMovement();

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent<Block>(out var block))
                TryConcat(this, block);
        }

        private void TryConcat(Block first, Block second)
        {
            if (first.Cost != second.Cost || first.enabled == false || second.enabled == false) return;

            var cost = first.Cost + 1;

            var position = (first.transform.position + second.transform.position) / 2f;
            var rotation = Quaternion.identity;

            first.enabled = false;
            second.enabled = false;

            Destroy(first.gameObject);
            Destroy(second.gameObject);

            var closestBlock = FindObjectsByType<Block>(FindObjectsSortMode.None)
                .Where(b => b.Cost == cost)
                .OrderBy(b => Vector3.Distance(b.transform.position, position))
                .FirstOrDefault();

            var velocity = Vector3.up * 5f + (closestBlock ? (closestBlock.transform.position - position) * 0.5f : Vector3.zero);
            
            var createdBlock = Factory.CreateBlock(cost, position, rotation);
                createdBlock.Rigidbody.linearVelocity = velocity;
                createdBlock.Rigidbody.angularVelocity = Random.onUnitSphere * 5f;
                createdBlock.EnablePulseAnimation();
        }

        private IEnumerator PlayPulseAnimation()
        {
            var time = 0.5f;

            while (time < 1)
            {
                transform.localScale = Vector3.one * time;
                time += Time.deltaTime * 4f;

                yield return null;
            }

            transform.localScale = Vector3.one;
        }

        private void FixedUpdate()
        {
            if (Game.IsActive == false) return;
            
            Rigidbody.MovePosition( Rigidbody.position + Vector3.back * 0.1f * Time.fixedDeltaTime );
        }
    }
}