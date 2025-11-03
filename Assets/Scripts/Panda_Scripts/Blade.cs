using System.Collections;
using UnityEngine;

public class Blade : MonoBehaviour
{
  
    
   

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // collided = true;
        // if (collision != null && collided) {
        //     collided = false;
        //         StartCoroutine(RespawnRoutine(collision));
        // }
        IDamageble damageble = collision.collider.GetComponentInParent<IDamageble>();
        if(damageble != null)
        {
            damageble.TakeDamage(1);
        }
    }

   
}
