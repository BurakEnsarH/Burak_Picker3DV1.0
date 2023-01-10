using Command;
using Data.UnityObjects;
using Data.ValueObjects;
using Signals;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Transform levelHolder;
        [SerializeField] private int levelID;

        #endregion

        #region Private Variables

        private LevelData _data;
        private int _totalLevelCount;

        private OnLevelLoaderCommand _levelLoader;
        private OnLevelDestroyerCommand _levelDestroyer;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetLevelData();
            _totalLevelCount = GetTotalLevelCount();
            levelID = GetLevelID();

            Init();
        }

        private void Init()
        {
            _levelDestroyer = new OnLevelDestroyerCommand(levelHolder);
            _levelLoader = new OnLevelLoaderCommand(levelHolder);
        }

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += _levelLoader.Execute;
            CoreGameSignals.Instance.onClearActiveLevel += _levelDestroyer.Execute;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
            CoreGameSignals.Instance.onGetLevelID += OnGetLevelID;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= _levelLoader.Execute;
            CoreGameSignals.Instance.onClearActiveLevel -= _levelDestroyer.Execute;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
            CoreGameSignals.Instance.onGetLevelID -= OnGetLevelID;
        }

        private void OnDisable() => UnsubscribeEvents();

        private void Start()
        {
            CoreGameSignals.Instance.onLevelInitialize?.Invoke(levelID);
        }

        private LevelData GetLevelData() => Resources.Load<CD_Level>("Data/CD_Level").LevelList[levelID];

        private int GetTotalLevelCount() => Resources.Load<CD_Level>("Data/CD_Level").LevelList.Count;

        private int GetLevelID()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("Level") ? ES3.Load<int>("Level") : 0;
        }

        private int OnGetLevelID()
        {
            return levelID;
        }

        private void OnNextLevel()
        {
            levelID++;
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke(levelID);
        }

        private void OnRestartLevel()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke(levelID);
        }
    }
}