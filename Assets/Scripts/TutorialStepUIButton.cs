using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TutorialStepUIButton : TutorialStepAbstractUI
    {
        [SerializeField] private Button button;

        public override bool InitStep()
        {
            button.gameObject.SetActive(false);

            if (base.InitStep())
            {
                return true;
            }
            
            button.gameObject.SetActive(true);
            return false;
        }

        public override void StartStep()
        {
            base.StartStep();
            button.gameObject.SetActive(true);
            button.onClick.AddListener(StopStep);
        }

        public override void StopStep()
        {
            base.StopStep();
            Destroy(this);
        }
    }
}