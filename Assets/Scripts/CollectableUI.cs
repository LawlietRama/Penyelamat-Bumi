using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableUI : MonoBehaviour
{
    private GridLayoutGroup gridLayout;

    public static CollectableUI instance;
    [Header("Trash List")]
    public List<Trash> trashes = new List<Trash>();
    [Space]
    [Header("Public References")]
    public GameObject trashCellPrefab;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        GetComponent<RectTransform>().sizeDelta = new Vector2(gridLayout.cellSize.x * 4.25f, gridLayout.cellSize.y * 2);

        foreach (Trash trash in trashes)
        {
            SpawnTrashCell(trash);
        }
    }

    private void SpawnTrashCell(Trash trash)
    {
        GameObject trashCell = Instantiate(trashCellPrefab, transform);

        trashCell.name = trash.trashName;

        Image icon = trashCell.transform.Find("image").GetComponent<Image>();
        //Text count = trashCell.transform.Find("text").GetComponentInChildren<Text>();

        icon.sprite = trash.trashIcon;
        //count.text = trash.count + "";

        icon.GetComponent<RectTransform>().pivot = uiPivot(icon.sprite);
        /*TextMeshProUGUI name = charCell.transform.Find("nameRect").GetComponentInChildren<TextMeshProUGUI>();

        artwork.sprite = character.characterSprite;
        name.text = character.characterName;

        artwork.GetComponent<RectTransform>().pivot = uiPivot(artwork.sprite);
        artwork.GetComponent<RectTransform>().sizeDelta *= character.zoom;*/
    }

    public Vector2 uiPivot(Sprite sprite)
    {
        Vector2 pixelSize = new Vector2(sprite.texture.width, sprite.texture.height);
        Vector2 pixelPivot = sprite.pivot;
        return new Vector2(pixelPivot.x / pixelSize.x, pixelPivot.y / pixelSize.y);
    }

    void Update()
    {
        
    }

}
