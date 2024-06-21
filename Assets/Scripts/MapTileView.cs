using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileView : MonoBehaviour
{

    public TMPro.TMP_Text orderNumber;

    public TMPro.TMP_Text itemCountText;

    public ItemType itemType;

    public SpriteRenderer spriteRenderer;

    public List<Pair<ItemType,Sprite>> textures;

    public int itemCount;

    Dictionary<int, Texture> keyValuePairsTexture = new Dictionary<int, Texture>();

    public void SetItemType(ItemType itemType)
    {
        this.itemType = itemType;

        spriteRenderer.sprite = textures.Find(a => a.key == itemType).value;
    }

    public void SetItemCount(int item)
    {
        if (item == 0)
            itemCountText.text = "";
        else
            itemCountText.text = item.ToString();

        this.itemCount = item;
    }
}

[System.Serializable]
public class Pair<K, V>
{
    public K key;

    public V value;

}
