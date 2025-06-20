using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Player player;
    [Header("Move Settings")]
    [SerializeField] private float moveSpeed;
   
 
   

   
 


    // Update is called once per frame
    void Update()
    {
      

        if (player != null)
        {
            
         FollowPlayer();
        }
     
            
        
    }

    public void StorePlayer(Player _player)
    {
        this.player = _player;
    }


    private void FollowPlayer()
    {
        

        Vector2 targetPostion = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        transform.position = targetPostion;
    }

 


  
}
