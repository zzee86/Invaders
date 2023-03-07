// This script is responsible for detecting collision with the player and letting the 
// Game Manager know

using UnityEngine;

public class WinZone : MonoBehaviour
{
    int playerLayer;    //The layer the player game object is on
    [SerializeField] private int levelIndex;

    void Start()
    {
        //Get the integer representation of the "Player" layer
        playerLayer = LayerMask.NameToLayer("Player");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //If the collision wasn't with the player, exit
        if (collision.gameObject.layer != playerLayer)
            return;



        PlayerPrefs.SetInt("Lv" + levelIndex, levelIndex);
        Debug.Log(PlayerPrefs.GetInt("Lv" + levelIndex, levelIndex));

        //Write "Player Won" to the console and tell the Game Manager that the player
        //won
        GameManager.PlayerWon();
    }
}
