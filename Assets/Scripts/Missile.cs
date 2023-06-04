using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private float missileSpeed = MissileConfig.speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(missileSpeed * Time.deltaTime * Vector3.right);

        if (transform.position.y >= MissileConfig.distanceLimit)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
