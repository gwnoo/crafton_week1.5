using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoardSlot : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image image;
    private RectTransform rect;
    private int idx;
    private GameObject slot1;
    private GameObject slot2;
    private GameObject slot3;
    private GameObject tileGenerator;
    public int tileType;

    private void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        slot1 = GameObject.Find("OfferSlot1");
        slot2 = GameObject.Find("OfferSlot2");
        slot3 = GameObject.Find("OfferSlot3");
        tileGenerator = GameObject.Find("TileGenerator");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject tile;
        if(slot1.transform.childCount != 0)
        {
            tile = slot1.transform.GetChild(0).gameObject;
        } else if(slot2.transform.childCount != 0)
        {
            tile = slot2.transform.GetChild(0).gameObject;
        } else
        {
            tile = slot3.transform.GetChild(0).gameObject;
        }
        

        if(transform.childCount == 0)
        {
            tile.transform.SetParent(transform);
            tile.GetComponent<RectTransform>().position = rect.position;

            int type = tile.GetComponent<TileDraggable>().tileType;
            BoardCheck.adj[idx / 5 + 1, idx % 5 + 1] = type;
            tileGenerator.GetComponent<BoardCheck>().displayedTileCount += 1;
            tile.GetComponent<TileDraggable>().enabled = false;

            tileGenerator.GetComponent<TileGenerator>().MinusTileCount();
            tileGenerator.GetComponent<BoardCheck>().Check();

            SoundManager.Instance.PlayDisplaySound();
        } 
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
        }
    }

    

    public void SetIdx(int x)
    {
        idx = x;
    }

    public int GetIdx()
    {
        return idx;
    }
}
