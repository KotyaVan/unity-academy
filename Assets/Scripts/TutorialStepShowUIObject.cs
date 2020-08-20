using System;
using System.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;

    public class TutorialStepShowUIObject : TutorialStepAbstractUI
    {
        [SerializeField] private GameObject showObject;
        [SerializeField] private float timer = 2f;
        public override bool InitStep()
        {
            showObject.SetActive(false);
            if (base.InitStep())
            {
                return true;
            }
            
            Destroy(showObject);
            return false;
        }

        public override async void StartStep()
        {
            base.StartStep();
            showObject.SetActive(true);

            await Task.Delay(TimeSpan.FromSeconds(timer));
            
            StopStep();
        }

        public override void StopStep()
        {
            base.StopStep();
            Destroy(showObject);
            Destroy(this);
        }
    }