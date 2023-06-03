using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = PlayerConfig.speed;
    [SerializeField]
    private int lives = PlayerConfig.lives;
    private UIManager uIManager;
    private GameManager gameManager;
    [SerializeField]
#pragma warning disable IDE0052 // Remove unread private members
    private bool canSpeedBoost = false;
#pragma warning restore IDE0052 // Remove unread private members
    private bool isSpeedBoost = false;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip powerupPickupSoundClip;
    [SerializeField]
    private AudioClip lifeDownSoundClip;

    // Start is called before the first frame update
    void Start()
    {
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();

        if (uIManager == null)
        {
            Debug.LogError("UIManager is null!");
        }
        if (gameManager == null)
        {
            Debug.LogError("GameManager is null!");
        }
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is null!");
        }

            transform.position = new Vector3(PlayerConfig.startXPosition, PlayerConfig.startYPosition, PlayerConfig.startZPosition);

        uIManager.UpdateLives(lives);
    }

    // Update is called on each frame
    void Update()
    {
        MovementLogic();
    }

    void OnDestroy()
    {
    }

    public void PlaySound(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopSound(AudioSource audioSource)
    {
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = null;
    }

    void MovementLogic()
    {
        Vector3 direction;
        // Inputs for horizontal and vertical movement (W, A, S and D keys)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        direction = new(horizontalInput, verticalInput, 0);

        // Move the player to the given direction with 5.0 speed
        transform.Translate(speed * Time.deltaTime * direction);

        // Limit player movement on vertical axis
        // upper and lower limit
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, PlayerConfig.lowerLimit, PlayerConfig.upperLimit), transform.position.z);

        // Limit player movement on horizontal axis

        // left and right limit
        if (transform.position.x <= PlayerConfig.leftLimit)
        {
            transform.position = new Vector3(PlayerConfig.rightLimit, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= PlayerConfig.rightLimit)
        {
            transform.position = new Vector3(PlayerConfig.leftLimit, transform.position.y, transform.position.z);
        }
    }

    public void DamagePlayer()
    {
        audioSource.clip = lifeDownSoundClip;
        audioSource.Play();
        lives--;

        uIManager.UpdateLives(lives);

        if (lives <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void ActivateSpeedBoost()
    {
        audioSource.clip = powerupPickupSoundClip;
        canSpeedBoost = true;
        if (!isSpeedBoost)
        {
            speed *= 2;
        }
        audioSource.Play();
        isSpeedBoost = true;
        StartCoroutine(DeactivateSpeedBoost());
    }

    private IEnumerator DeactivateSpeedBoost()
    {
        yield return new WaitForSeconds(PowerupConfig.timeLimit);
        speed /= 2;
        isSpeedBoost = false;
        canSpeedBoost = false;
    }

    public void IncreaseScore(int points)
    {
        uIManager.UpdateScore(points);
    }
}
