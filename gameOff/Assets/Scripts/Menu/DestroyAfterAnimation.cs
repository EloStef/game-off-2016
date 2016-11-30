using UnityEngine;
using System.Collections;

public class DestroyAfterAnimation : MonoBehaviour {

    public string animationName = "";
    Animation animation;

    // Use this for initialization
    void Start()
    {
        animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!animation.IsPlaying(animationName))
        {
            Destroy(this.gameObject);
        }
    }
}
