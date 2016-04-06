using UnityEngine;
using System.Collections;

public class DoNotDelete : MonoBehaviour {


    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
