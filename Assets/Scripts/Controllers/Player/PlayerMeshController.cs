using Data.ValueObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        [ShowInInspector] private ScaleData _data;

        #endregion

        #endregion

        public void SetMeshData(ScaleData scaleData)
        {
            _data = scaleData;
        }

        public void OnReset()
        {
        }
    }
}