using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpriteManager : MonoBehaviour
{
    public PlayerAbbr id;

    public SpriteRenderer head;
    public SpriteRenderer torso;
    public SpriteRenderer forearm_r;
    public SpriteRenderer upperarm_r;
    public SpriteRenderer hand_r;
    public SpriteRenderer forearm_l;
    public SpriteRenderer upperarm_l;
    public SpriteRenderer hand_l;
    public SpriteRenderer lowerleg_r;
    public SpriteRenderer upperleg_r;
    public SpriteRenderer foot_r;
    public SpriteRenderer lowerleg_l;
    public SpriteRenderer upperleg_l;
    public SpriteRenderer foot_l;

    private void Start()
    {
        PlayerData player = GameManager.instance.GetPlayerData(id);

        if (player.character == null)
            playerSkinRandomizerScript.AssignRandomCharacters();

        Character character = player.character;


        head.sprite = character.head;
        torso.sprite = character.torso;
        forearm_r.sprite = character.forearm;
        upperarm_r.sprite = character.upperarm;
        hand_r.sprite = character.hand;
        forearm_l.sprite = character.forearm;
        upperarm_l.sprite = character.upperarm;
        hand_l.sprite = character.hand;
        lowerleg_l.sprite = character.lowerleg;
        upperleg_l.sprite = character.upperleg;
        foot_l.sprite = character.foot;
        lowerleg_r.sprite = character.lowerleg;
        upperleg_r.sprite = character.upperleg;
        foot_r.sprite = character.foot;

        
    }
}
