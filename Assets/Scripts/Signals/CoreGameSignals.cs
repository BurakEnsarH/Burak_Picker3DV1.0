using Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoBehaviour
    {
        #region Singleton

        public static CoreGameSignals Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning("this: " + this.GetInstanceID());
                Debug.LogWarning("Singleton: " + Instance.GetInstanceID());
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        #endregion

        public UnityAction<GameStates> onChangeGameState = delegate { };
        public UnityAction<int> onLevelInitialize = delegate { };
        public UnityAction onClearActiveLevel = delegate { };
        public UnityAction onLevelFailed = delegate { };
        public UnityAction onLevelSuccessful = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onRestartLevel = delegate { };
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };
    }
}