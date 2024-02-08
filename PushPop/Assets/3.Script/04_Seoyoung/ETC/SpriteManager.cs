using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class SpriteManager : MonoBehaviour
{
    [SerializeField]
    private SpriteAtlas spriteAtlas;

    //public void setSprite(GameObject obj, int index)
    //{
    //    obj.GetComponent<Image>().sprite = spriteAtlas.GetSprite
    //}

    public Sprite setSprite(string name)
    {
        return spriteAtlas.GetSprite(name);
    }
}
