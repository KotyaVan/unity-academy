using System;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class TutorialStepAbstractUI : MonoBehaviour, ITutorialStep
    {
        [SerializeField] protected int id;
        public int Id => id;
        
        public virtual bool InitStep()
        {
            return TutorialController.Instance.Add(this);
        }

        public virtual void StartStep()
        {
            
        }

        public virtual void StopStep()
        {
            TutorialController.Instance.Stop(Id);
        }

        protected void Awake()
        {
            if (!InitStep())
            {
                Destroy(this);
            }
        }
    }
}