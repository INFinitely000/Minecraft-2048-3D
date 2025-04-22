using System;
using TMPro;
using UnityEngine;

namespace MineBlock3D.Scripts.Gameplay.UI
{
    public class PointsBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _pointsTextField;


        private void OnEnable() =>
            Game.PointsAdded += Refresh;

        private void OnDisable() =>
            Game.PointsAdded -= Refresh;
        
        private void Refresh() =>
            _pointsTextField.text = Game.Points.ToString();
    }
}