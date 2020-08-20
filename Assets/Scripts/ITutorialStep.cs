using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITutorialStep 
{
    int Id { get; }
    bool InitStep();
    void StartStep();
    void StopStep();
}
