using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour {

    private float nextFire = 0.0f;

    public void Fire(GameObject bulletType, bool facingRight, Transform spawnPoint, string shotBy)
    {
        if (Time.time > nextFire)
        {
            GameObject bullet = Instantiate(bulletType, spawnPoint.position, Quaternion.identity) as GameObject;
            Bullet bulletObj = bullet.GetComponent<Bullet>();

            bulletObj.shotBy = shotBy;

            if (facingRight)
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletObj.bulletSpeed, 0);
            else
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletObj.bulletSpeed * -1, 0);

            Flip(facingRight, bulletObj.transform);
            
            Destroy(bullet, 2.0f);

            nextFire = Time.time + bulletObj.fireRate;
        }
    }

    private void Flip(bool facingRight, Transform bulletTransform)
    {
        if (facingRight)
            return;

        Vector3 theScale = bulletTransform.localScale;
        theScale.x *= -1;
        bulletTransform.localScale = theScale;
    }

}
