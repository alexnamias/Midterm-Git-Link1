using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerScript Player;

    void Awake()
    {
        Player = GetComponentInParent<PlayerScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Player == null || Player.Sword == false)
        {
            return;
        }   
        
        if (other.transform == Player.transform || other.transform.IsChildOf(Player.transform))
        {
            return;
        }

        Destroy(other.gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
