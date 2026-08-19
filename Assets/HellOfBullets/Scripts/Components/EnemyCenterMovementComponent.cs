using UnityEngine;

public class EnemyCenterMovementComponent : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = FindAnyObjectByType<PlayerMovementComponent>(FindObjectsInactive.Exclude).transform;
        if(!playerTransform)
            Debug.LogError("No player on scene");
    }

    private void Update()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        //Base movement for the enemies is half the base speed of the player
        transform.position += direction * (playerData.MovementSpeed/3) * Time.deltaTime;
    }
}
