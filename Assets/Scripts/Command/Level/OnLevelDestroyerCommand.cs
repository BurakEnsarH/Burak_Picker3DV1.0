using UnityEngine;

namespace Command
{
    public class OnLevelDestroyerCommand
    {
        private Transform _levelHolder;

        public OnLevelDestroyerCommand(Transform levelHolder)
        {
            _levelHolder = levelHolder;
        }


        public void Execute()
        {
            Object.Destroy(_levelHolder.GetChild(0).gameObject);
        }
    }
}