using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float powerupSpeed = PowerupConfig.speed;
    private Player player;

    private enum PowerupTypes
    {
        TripleShot,
        SpeedBoost
    };

    [SerializeField]
    private PowerupTypes powerupType;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(powerupSpeed * Time.deltaTime * Vector3.left);

        if (transform.position.x <= Config.leftLimit)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                switch (powerupType)
                {
                    case PowerupTypes.TripleShot:
                        player = other.transform.GetComponent<Player>();
                        if (player != null)
                        {
                            player.ActivateTripleShot();
                        }
                        break;

                    case PowerupTypes.SpeedBoost:
                        player = other.transform.GetComponent<Player>();
                        if (player != null)
                        {
                            player.ActivateSpeedBoost();
                        }
                        break;
                }
                Destroy(this.gameObject);
                break;

            default:
                break;
        }
    }
}
