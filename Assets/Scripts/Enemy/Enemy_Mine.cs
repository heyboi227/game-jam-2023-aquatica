using UnityEngine;

public class Enemy_Mine : Enemy
{
    void Update()
    {
        transform.Translate(new Vector3(-(enemySpeed * Time.deltaTime), 0, 0));

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.TryGetComponent<Player>(out player))
            {
                player.DamagePlayer();
            }
            transform.GetChild(0).gameObject.SetActive(false);
            audioSource.clip = explosionSoundClip;
            StartCoroutine(AnimationRoutine());
        }
        else if (other.CompareTag("Missile"))
        {
            DamageEnemy(other);
            if (lives <= 0)
            {
                uiManager.UpdateScore(points);
                transform.GetChild(0).gameObject.SetActive(false);
                audioSource.clip = explosionSoundClip;
                StartCoroutine(AnimationRoutine());
            }
        }
    }
}
