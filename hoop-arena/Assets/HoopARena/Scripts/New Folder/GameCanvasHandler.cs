using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameCanvasHandler : MonoBehaviour
{
    [Header("Ball Spawning")]
    [SerializeField] private Camera arCamera;
    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private float distanceFromCamera = 1f;


    private GameObject _currentBall;

    public bool timeOut = false;


    void OnEnable()
    {
        TouchInputHandler.OnTouchBegan += BallCheck;
        BallFlickHandler.Released += ResetTrackedBall;
        EventHandler.OnStopPlayerShooting += TimeOut;
        UIHandler.ResetGame += Reset;
    }


    void OnDisable()
    {
        TouchInputHandler.OnTouchBegan -= BallCheck;
        BallFlickHandler.Released -= ResetTrackedBall;
        EventHandler.OnStopPlayerShooting -= TimeOut;
    }

    void Start()
    {
        
    }


    public void SpawnBall(Vector2 mousePOS)
    {
        // Get the center of the spawn button in screen coordinates.
        Vector2 screenPosition =
            RectTransformUtility.WorldToScreenPoint(
                null, mousePOS

            );


        // Convert the screen position into a point
        // in front of the AR camera.
        Vector3 worldPosition =
            arCamera.ScreenToWorldPoint(
                new Vector3(
                    screenPosition.x,
                    screenPosition.y,
                    distanceFromCamera
                )
            );


        // Spawn the ball as a child of the camera.
        _currentBall =
            Instantiate(
                ballPrefab,
                worldPosition,
                Quaternion.identity,
                arCamera.transform
            );


        // Keep the ball stationary until
        // the player begins dragging it.
        Rigidbody rb = _currentBall.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = true;
        }


        
    }

    public void ResetTrackedBall()
    {
        //reset the parent of the object
        _currentBall.transform.parent = null; 
        
        //Untrack the ball
        _currentBall = null;
    }
    
    public void BallCheck(Vector2 POS)
    {
        if ( _currentBall == null && timeOut == false)
        {
            SpawnBall(POS);
        }
        else
        {
            return;
        }
    }

    public void TimeOut()
    {
        timeOut = !timeOut;
    }

    public void Reset()
    {
        timeOut = false;
    }


}
