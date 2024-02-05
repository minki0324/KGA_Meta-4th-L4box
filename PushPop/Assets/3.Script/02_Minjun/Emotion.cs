using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Emotion : NetworkBehaviour
{
    public static Emotion instance;
    [SerializeField] private Sprite[] emotions;
    [SerializeField] private SpriteRenderer spriterenderer;
    public PlayerEmotionControl playerEmotion;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [Client]
    public void onEmotion(int index)
    {
        if (spriterenderer == null)
        {
            spriterenderer = playerEmotion.GetComponent<SpriteRenderer>();
        }
        spriterenderer.sprite = emotions[index];
        playerEmotion.PlayEmotion();
    }
    
  
}
