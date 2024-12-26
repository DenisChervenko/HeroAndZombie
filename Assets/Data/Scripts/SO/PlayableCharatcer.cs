using UnityEngine;

[CreateAssetMenu(fileName = "Character_Name", menuName = "Characters/Playable", order = 0)]
public class PlayableCharatcer : ScriptableObject
{
    public bool isBuyed = false;
    public bool isSelected = false;

    public new string name;
    public string ability;

    public string rarity;
    public int rarityIndex;

    [TextArea(3, 4)]
    public string description;

    public int level;

    public int[] upgradePrice;
    public int[] modifierIndex;

    public int price;

    public float attackSpeed;
    [Range(4, 10)]public float attackRange;

    public float damage;
    public float health;
    public float defend;
    public float speed;
}   
