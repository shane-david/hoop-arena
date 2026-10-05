using UnityEngine;


//Written by Zachary
//The job of the score handler is to keep track and update the player's score.
public class ScoreHandler : MonoBehaviour
{
    //variables to keep track of 
    public int Score;


    private void Start()
    {
        Score = 0;
        
    }


    //If a ball leaves the hitbox of this collider, then update the score.
    private void OnTriggerExit(Collider collision)
    {
        //if the collided object is not a basketball, return
        if (!collision.gameObject.GetComponent<BasketBallHandler>()) return;

        //if the collided object has the basketballhandler script, score a point
        if (collision.gameObject.GetComponent<BasketBallHandler>())
        {
            Score += 1;
        }
    }


}
