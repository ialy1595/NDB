using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour {

    public int slotX;
    public int slotY;
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    public GUISkin skin;

    public Sprite inventorySprite;

    [HideInInspector] public bool inventoryButtonClicked = false;

    private ItemDatabase database;
    private int inventorySize;
    public bool showInventory = true;

    public float slotPosX = 40f;
    public float slotPosY = 40f;
    //public float sizeOffset = 50f;

    private bool showTooltip = false;
    private string tooltip;

    private bool isDragging = false;
    private Item clickedItem;
    private int clickedItemIndex;
    private bool isOneClicked = false;
    private double prevClickTime;
    private double doubleClickTime = 0.25f;

    void Start ()
    {
        inventorySize = slotX * slotY;
        if (slots.Count == 0 && inventory.Count == 0)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                slots.Add(new Item());
                inventory.Add(new Item());
            }
        }
        database = GameObject.Find("Database").GetComponent<ItemDatabase>();
	}

    void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "Stage1" || SceneManager.GetActiveScene().name == "Stage2")
        {
            showInventory = false;
            slotPosX = 2010f;
            slotPosY = 1530f;
        }
        else if (SceneManager.GetActiveScene().name == "InterRound")
        {
            showInventory = false;
            slotPosX = 20f;
            slotPosY = 20f;
        }
        else showInventory = false;
    }

    void Update()
    {
        if (/*Input.GetButtonDown("Inventory") && */!GameManager.gm.isInterRound && !GameManager.gm.isPaused)
        {
            showInventory = true;
        }

        else if (GameManager.gm.isInterRound && SceneManager.GetActiveScene().name == "InterRound")
        {
            showInventory = inventoryButtonClicked;
        }
        else if (GameManager.gm.isInterRound && !(SceneManager.GetActiveScene().name == "InterRound"))
        {
            showInventory = false;
        }
        else if (GameManager.gm.isPaused) showInventory = false;
    }

    void OnGUI()
    {

        GUI.skin = skin;
        tooltip = "";
        if (showInventory)
        {
            DrawInventory();

            if (showTooltip)
            {
                GUI.Box(new Rect(Event.current.mousePosition.x + 10f, Event.current.mousePosition.y, 200, 200), tooltip, skin.GetStyle("Tooltip"));
            }
        }

        if (isDragging)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), clickedItem.itemImage);
        }


    }

    void DrawInventory()
    {
        int i = 0;
        if (!(SceneManager.GetActiveScene().name == "InterRound"))
            GUI.DrawTexture(new Rect(slotPosX / 2f + 0f, slotPosY / 2f + 0f, (float)((54 + 10) * slotX) + 32f, (float)((54 + 10) * slotY) + 32f), inventorySprite.texture);
        else GUI.DrawTexture(new Rect(slotPosX / 2f + 0f, slotPosY / 2f + 0f, (float)((40 + 6) * slotX) + 18f, (float)((40 + 6) * slotY) + 18f), inventorySprite.texture);


        //GUI.Box(new Rect(slotPosX/2 + 0f, slotPosY/2 + 0f, ((50+8) * slotX) + slotPosX, ((50+8) * slotY) + slotPosY), "", skin.GetStyle("InventoryBackground"));
        for (int y = 0; y < slotY; y++)
        {
            for (int x = 0; x < slotX; x++)
            {
                Rect slotRect;
                if(!(SceneManager.GetActiveScene().name == "InterRound"))
                    slotRect = new Rect(slotPosX / 2f + 21f + x * 64f, slotPosY / 2f + 21f + y * 64f, 54f, 54f);
                else slotRect = new Rect(slotPosX / 2f + 12f + x * 46f, slotPosY / 2f + 12f + y * 46f, 40f, 40f);
                GUI.Box(slotRect, "", skin.GetStyle("Slots"));

                slots[i] = inventory[i];

                if (slots[i].itemName != null && slots[i].itemName != "")
                {
                    GUI.DrawTexture(slotRect, slots[i].itemImage);

                    if (slotRect.Contains(Event.current.mousePosition))
                    {
                        if (!isDragging)
                        {
                            showTooltip = true;
                            tooltip = CreateTooltip(slots[i]);
                        }

                        if (Event.current.button == 0 /*leftMouseButton*/)
                        {


                            //아이템 드래그
                            if (Event.current.type == EventType.MouseDrag && !isDragging)
                            {
                                clickedItem = slots[i];
                                clickedItemIndex = i;
                                isDragging = true;
                                inventory[i] = new Item();
                                isOneClicked = false;
                            }

 

                            //클릭(아이템 사용)
                            if (Input.GetMouseButtonDown(0) && !GameManager.gm.isInterRound)
                            {
                                clickedItem = slots[i];
                                clickedItemIndex = i;

               
                                    Debug.Log("doubleClicked");

                                    if(database.useItem(clickedItem) == true)
                                        RemoveItem(clickedItemIndex);
                  

                                isOneClicked = false;
                            }

                        }

                        if (Event.current.type == EventType.MouseUp && isDragging)
                        {
                            inventory[clickedItemIndex] = inventory[i];
                            inventory[i] = clickedItem;
                            isDragging = false;
                            clickedItem = null;
                        }
                    }
                }
                else
                {
                    if (slotRect.Contains(Event.current.mousePosition))
                    {
                        if (Event.current.type == EventType.MouseUp && isDragging)
                        {
                            inventory[clickedItemIndex] = inventory[i];
                            inventory[i] = clickedItem;
                            isDragging = false;
                            clickedItem = null;
                        }
                    }
                }

                if (tooltip == "") showTooltip = false;
                i++;
            }
        }
    }

    public string CreateTooltip(Item item)
    {
        string tooltip = "<color=#ffffff>" + item.itemName + "</color>\n\n";
        tooltip += "<color=#000000>" + item.itemDescription + "</color>\n\n";
        tooltip += "<color=#990282>" + "가격 : " + item.itemPrice + "</color>";
        return tooltip;
    }

    public void AddItem(Item item)
    {
        if (GameManager.gm.Money() < item.itemPrice)
        {
            GameManager.gm.ShowMessageBox("돈이 부족합니다.");
            return;
        }

        for (int i = 0; i < inventorySize; i++)
        {
            if (inventory[i].itemName == null || inventory[i].itemName == "")
            {
                Debug.Log("null");
                for (int j = 0; j < database.itemDatabase.Count; j++)
                {
                    if (database.itemDatabase[j].itemName == item.itemName)
                    {
                        inventory[i] = database.itemDatabase[j];
                        GameManager.gm.ChangeMoneyInterRound(-item.itemPrice);
                        break;
                    }
                }

                for (int j = 0; j < database.rivalItemDatabase.Count; j++)
                {
                    if (database.rivalItemDatabase[j].itemName == item.itemName)
                    {
                        inventory[i] = database.rivalItemDatabase[j];
                        GameManager.gm.ChangeMoneyInterRound(-item.itemPrice);
                        break;
                    }
                }
                return;
            }
        }
        GameManager.gm.ShowMessageBox("인벤토리가 꽉 찼습니다.");
    }

    bool IsInventoryContains(int id)
    {
        bool res = false;

        for (int i = 0; i < inventorySize; i++)
        {
            res = (inventory[i].itemID == id);

            if (res)
                break;
        }

        return res;
    }

    void RemoveItem(int index)
    {
        inventory[index] = new Item();
    }
}
