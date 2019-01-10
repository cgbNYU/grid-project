using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SlideObScript : MonoBehaviour
{
   
    //Set from level loader
    private float speed; //how fast the tween happens
    private Ease easing; //the style of movement that the obstacle should use
    private Vector3 target; //where the obstacle moves to
    private float startTime; //when the obstacle should fire
    private bool isTransformTween; //determines which aspects of the object get tweened
    private bool isRotationTween; //determines which aspects of the object get tweened
    private bool isScaleTween; //determines which aspects of the object get tweened
    
    //Internal variables
    private float timer; //counts up to startTime
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Called from level loader
    public void ObSetup(float obSpeed, Vector3 obTarget, float obStartTime = 0, Ease obEasing = Ease.Linear)
    {
        speed = obSpeed;
        easing = obEasing;
        target = obTarget;
        startTime = obStartTime;

        StartCoroutine(CountDown());
    }
    
    //Counts up timer to startTime and then launches ObActivate
    IEnumerator CountDown()
    {
        while (timer < startTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(ObActivate());
    }

    //This gets called when it is time for the obstacle to fire
    //Should hold all the tweens for the obstacle
    IEnumerator ObActivate()
    {
        Tweener moveTween = transform.DOMove(target, speed);
        moveTween.SetEase(easing);

        yield return moveTween.WaitForCompletion();
        Destroy(gameObject);
    }  
}
