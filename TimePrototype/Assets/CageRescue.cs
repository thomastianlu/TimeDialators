using UnityEngine;
using System.Collections;

public class CageRescue : MonoBehaviour {

    [SerializeField]
    private CageScript _cage2;
    [SerializeField]
    private CageScript _cage3;
    [SerializeField]
    private CageScript _cage4;

    [SerializeField]
    private Animator _bottomLevelAnimator;

    private bool _playOnce = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ManageCageStatus();
    }

    void ManageCageStatus()
    {
        if (!_cage2.GetPrisonerStatus() && !_cage3.GetPrisonerStatus() && !_cage4.GetPrisonerStatus() && !_playOnce)
        {
            _bottomLevelAnimator.enabled = true;
            _playOnce = true;
        }
    }
}
