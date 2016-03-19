using UnityEngine;
using System.Collections;

public class InceptionRecorder : MonoBehaviour {

    [SerializeField]
    int _generationIteration = 1;
    

    public void IncreaseGeneration()
    {
        _generationIteration++;
    }

    public void DecreaseGeneration()
    {
        _generationIteration--;
    }

    public int GetGeneration()
    {
        return _generationIteration;
    }
}
