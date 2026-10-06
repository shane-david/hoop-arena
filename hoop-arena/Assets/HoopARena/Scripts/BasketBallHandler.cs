using System.Collections;
using UnityEngine;
using System;

//Written by Zachary
//The basketball handler manages when a basketball should be deleted
public class BasketBallHandler : MonoBehaviour
{
    /// <summary>
    /// Controls the amount of time until the ball destroys itself
    /// </summary>
    [SerializeField] private float _destroyDelay;

    public static Action WhenDestroyed;

    private void Start()
    {
        StartCoroutine(DestroyTimer(_destroyDelay));
    }

    //this coroutine will destroy this object after a certain amount of time
    IEnumerator DestroyTimer(float time)
    {
        //if time is unspecified, set it to a small value
        if (time == 0)
        {
            Debug.Log("Destroy delay unspecified");
            time = 2f;
        }

        //Wait for the destroy delay
        yield return new WaitForSeconds(time);

        DestroySelf();
    }
    private void DestroySelf()
    {
        WhenDestroyed?.Invoke();
        Destroy(this.gameObject);
    }
}
