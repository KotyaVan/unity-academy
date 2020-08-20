using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialController
{
    private static TutorialController instance;
    public static TutorialController Instance => instance ?? (instance = new TutorialController());
    private Dictionary<int, ITutorialStep> steps = new Dictionary<int, ITutorialStep>();

    private static int? CurrentStep
    {
        get
        {
            if (PlayerPrefs.HasKey("Tutorial"))
            {
                return PlayerPrefs.GetInt("Tutorial");
            }

            return null;
        }
        set
        {
            if (value != null)
            {
                PlayerPrefs.SetInt("Tutorial", value.Value);
            }
        }
    }

    public bool Add(ITutorialStep tutorialStep)
    {
        if (steps.ContainsKey(tutorialStep.Id))
        {
            return false;
        }

        if (CurrentStep != null && tutorialStep.Id < CurrentStep)
        {
            return false;
        }

        steps[tutorialStep.Id] = tutorialStep;

        if (CurrentStep == null && tutorialStep.Id == 0)
        {
            StartNext();
            return true;
        }

        if (CurrentStep == tutorialStep.Id)
        {
            tutorialStep.StartStep();
        }

        return true;
    }

    public void Stop(int id)
    {
        if (steps.ContainsKey(id))
        {
            steps.Remove(id);
        }
        
        StartNext();
    }

    private void StartNext()
    {
        if (steps == null || steps.Count == 0)
        {
            CurrentStep++;
            return;
        }

        CurrentStep = CurrentStep == null ? 0 : CurrentStep + 1;
        var finalStepId = steps.Max(s => s.Key);

        while (!steps.ContainsKey(CurrentStep.Value))
        {
            CurrentStep++;
            if (CurrentStep > finalStepId)
            {
                return;
            }
        }
        
        steps[CurrentStep.Value].StartStep();
    }
}

