using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "Objects/LevelObject", order = 1)]
public class LevelObjectData : ScriptableObject
{

    public string name = "New Level Objects Name";

    public bool isStatic;

    public int Health = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
