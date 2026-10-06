using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameCanvasHandler : MonoBehaviour
{
    [Header("Ball Spawning")]
    [SerializeField] private Camera arCamera;
    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private float distanceFromCamera = 1f;

    [Header("Game State")]
    [SerializeField] private GameObject reloadButton;
    [SerializeField] private SpawnHandler spawnHandler;

    private GameObject _currentBall;


    void OnEnable()
    {
        TouchInputHandler.OnTouchBegan += BallCheck;
        BasketBallHandler.WhenDestroyed += ResetBall;
    }


    void OnDisable()
    {
        TouchInputHandler.OnTouchBegan -= BallCheck;
        BasketBallHandler.WhenDestroyed -= ResetBall;
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

    public void ResetBall()
    {
        _currentBall = null; 
    }
    
    public void BallCheck(Vector2 POS)
    {
        if ( _currentBall == null )
        {
            SpawnBall(POS);
        }
        else
        {
            return;
        }
    }

    


    
}
