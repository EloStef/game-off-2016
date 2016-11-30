using UnityEngine;
using System.Collections;

public class GuiStartPosition : MonoBehaviour {

    public Transform pos;
	// Use this for initialization
	void Start () {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(pos.position);
        //GetComponent<RectTransform>().position = screenPos;
        GetComponent<RectTransform>().position = pos.position;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
