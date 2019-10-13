using UnityEngine;


public class MoveToPlayer : MonoBehaviour
{
    private GameObject Player;
    public float speed;
    private float CoordinateX = 0;
    public Vector2 MaxMinCoordinates;
    private void Awake()
    {
        MainSettings.MainCamera = gameObject.GetComponent<Camera>();
    }

    private void Start()
    {
        Player = MainSettings.Players.Player;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {        
        if (Player != null && MainSettings.NotPause && MainSettings.Players.UnLockController)
        {
            CoordinateX = Player.transform.position.x;
            if (CoordinateX < MaxMinCoordinates.x) { CoordinateX = MaxMinCoordinates.x; }
            if (CoordinateX > MaxMinCoordinates.y) { CoordinateX = MaxMinCoordinates.y; }
            transform.position = Vector3.Lerp(transform.position, new Vector3(CoordinateX, transform.position.y, transform.position.z), Time.deltaTime * speed);
        }
    }
}
