using System;
using System.Collections.Generic;
using Data.UnityObjects;
using Data.ValueObjects;
using DG.Tweening;
using Managers;
using Signals;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Controllers.Pool
{
    public class PoolController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private byte stageID;
        [SerializeField] private TextMeshPro poolText;
        [SerializeField] private List<DOTweenAnimation> tweenAnimations = new List<DOTweenAnimation>();

        #endregion

        #region Private Variables

        [ShowInInspector] private PoolData _data;
        [ShowInInspector] private byte _requiredAmount, _collectedCount;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetPoolData();
            SetRequiredAmount();
        }

        private PoolData GetPoolData()
        {
            return Resources.Load<CD_Level>("Data/CD_Level")
                .LevelList[(int)CoreGameSignals.Instance.onGetLevelID?.Invoke()].PoolList[stageID];
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onStageAreaSuccessful += OnActivateAllAnimations;
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onStageAreaSuccessful -= OnActivateAllAnimations;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Start()
        {
            SetRequiredAmountToText();
        }

        private void OnActivateAllAnimations(byte stageID)
        {
            if (stageID != this.stageID) return;
            foreach (var animation in tweenAnimations)
            {
                animation.DOPlay();
            }
        }

        private void SetRequiredAmount()
        {
            _requiredAmount = _data.RequiredObjectCount;
        }

        private void SetRequiredAmountToText()
        {
            poolText.text = $"0/{_data.RequiredObjectCount}";
        }

        private void SetCollectedAmountToText()
        {
            poolText.text = $"{_collectedCount}/{_requiredAmount}";
        }


        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Collectable")) return;
            IncreaseCollectedAmount();
            SetCollectedAmountToText();
        }

        private void IncreaseCollectedAmount() => _collectedCount++;


        private void DecreaseCollectedAmount() => _collectedCount--;

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Collectable")) return;
            DecreaseCollectedAmount();
            SetCollectedAmountToText();
        }

        public bool TakeStageResult(byte managerStageID)
        {
            if (stageID == managerStageID)
            {
                return _collectedCount >= _requiredAmount;
            }

            return false;
        }
    }
}