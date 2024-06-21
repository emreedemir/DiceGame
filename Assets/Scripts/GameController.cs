using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameController : MonoBehaviour
{
    public int minumumMapTileCount;

    public int maximumMapTileCount;

    public float tileDistanceEachOther;

    public Vector3 initialPositionMap;

    public List<TileData> tileDatas;

    public CameraFollow cameraFollow;

    public MapTileView mapTileViewPrefab;

    public Mascot mascot;

    public DiceController diceController;

    private Queue<MapTileView> mapTileViewPoll;

    private MapTileView GetMapTileViewFromPoll()
    {
        if (mapTileViewPoll.Count == 0)
        {
            MapTileView mapTileView = Instantiate(mapTileViewPrefab, this.transform);

            return mapTileView;
        }
        else
        {
            return mapTileViewPoll.Dequeue();
        }
    }

    private void Start()
    {
        InitGame(); 
    }

    void InitGame()
    {
        mapTileViewPoll = new Queue<MapTileView>();

        int randomTileCount = UnityEngine.Random.RandomRange(minumumMapTileCount, maximumMapTileCount + 1);

        CreateTiles(randomTileCount);

        List<Bucket<int, ItemType, int>> keyValuePairs = GetRandomItemInfos(tileDatas.Count, UnityEngine.Random.RandomRange(2, tileDatas.Count / 2));

        SpawnItems(keyValuePairs);

        SetMascot();

        diceController.StartToDiceThrow();

        diceController.DiceThrowCompleted += HandleDiceThrow;
    }

    public void SetInitialCamera()
    { 
        cameraFollow.targetTransform = mascot.transform;

        cameraFollow.enabled = true;
    }

    public void CreateNewGame()
    {
        mascot.StopAllCoroutines();

        DePollMapTiles();

        int randomTileCount = UnityEngine.Random.RandomRange(minumumMapTileCount, maximumMapTileCount + 1);

        CreateTiles(randomTileCount);

        List<Bucket<int, ItemType, int>> keyValuePairs = GetRandomItemInfos(tileDatas.Count, UnityEngine.Random.RandomRange(2, randomTileCount / 2));

        SpawnItems(keyValuePairs);

        DiceController.diceTarget1 = 1;

        DiceController.diceTarget2 = 1;

        SetMascot();

        diceController.StartToDiceThrow();
    }

    private void DePollMapTiles()
    {
        for (int i = 0; i < tileDatas.Count; i++)
        {
            MapTileView mapTileView = tileDatas[i].MapTileView;

            mapTileView.SetItemCount(0);

            mapTileView.gameObject.SetActive(false);

            mapTileViewPoll.Enqueue(mapTileView);
        }
    }

    private void SetMascot()
    {
        mascot.InitTarget();
    }

    public bool IsDiceCorret(int dice1Value, int dice2Value,out int stepMove)
    {
        int targetValueOfPlayer1 = DiceController.diceTarget1;

        int targetValueOfPlayer2 = DiceController.diceTarget2;

        if ((dice1Value == targetValueOfPlayer1 && dice2Value == targetValueOfPlayer2))
        {
            stepMove = targetValueOfPlayer1 + targetValueOfPlayer2;
            return true;
        }
        else if ((dice2Value == targetValueOfPlayer1 && dice1Value == targetValueOfPlayer2))
        {
            stepMove = targetValueOfPlayer1 + targetValueOfPlayer2;
            return true;
        }
        else if(dice1Value ==targetValueOfPlayer1||dice1Value ==targetValueOfPlayer2)
        {
            stepMove = dice1Value;
            return true;
        }
        else if(dice2Value == targetValueOfPlayer1 || dice2Value == targetValueOfPlayer2)
        {
            stepMove = dice2Value;
            return true;
        }
        else
        {
            stepMove = 0;
            return false;
        }
    }

    private void GiveReward(int rewardAmount,ItemType itemType)
    {
        if (itemType == ItemType.Hamburger)
        {
            PlayerDataProfile.HambergerAmount += rewardAmount;
        }
        else if (itemType == ItemType.Pizza)
        {
            PlayerDataProfile.PizzaAmount += rewardAmount;
        }
        else if (itemType == ItemType.Pumpkin)
        {
            PlayerDataProfile.PupmkinAmount += rewardAmount;
        }
    }

    public void HandleDiceThrow(int dice1Value, int dice2Value)
    {
        if (IsDiceCorret(dice1Value,dice2Value,out int stepMove))
        {
            int sumOfDices = stepMove;

            TileData currentTileData = mascot.currentTile;

            int index = tileDatas.IndexOf(currentTileData);

            int nextTarget = sumOfDices + index;

            int totalTile = tileDatas.Count;

            int remainMove = nextTarget - totalTile;

            nextTarget = Mathf.Clamp(nextTarget, 0, totalTile - 1);

            TileData tileData = tileDatas[nextTarget];

            mascot.MoveToTarget(tileData, () =>
            {
                if (nextTarget == totalTile - 1)
                {
                    tileData = tileDatas[0];

                    if (remainMove > 0)
                    {
                        mascot.InitTargetWithAnimation(() =>
                        {
                            tileData = tileDatas[remainMove];

                            mascot.MoveToTarget(tileData, () =>
                            {
                                if (tileData.MapTileView.itemType != ItemType.None)
                                {
                                    GiveReward(tileData.MapTileView.itemCount, tileData.MapTileView.itemType);
                                }
                   
                                diceController.LocateDiceInitialPosition();

                                diceController.StartToDiceThrow();

                            });
                        });
                    }
                    else
                    {
                        mascot.MoveToTarget(tileData, () =>
                        {
                            if (tileData.MapTileView.itemType != ItemType.None)
                            {
                                GiveReward(tileData.MapTileView.itemCount, tileData.MapTileView.itemType);
                            }
                           
                            diceController.LocateDiceInitialPosition();

                            diceController.StartToDiceThrow();

                        });
                    }
                }
                else
                {
                    if (tileData.MapTileView.itemType != ItemType.None)
                    {
                        GiveReward(tileData.MapTileView.itemCount, tileData.MapTileView.itemType);
                    }

                    diceController.LocateDiceInitialPosition();

                    diceController.StartToDiceThrow();
                }
            });
        }
        else
        {
            diceController.LocateDiceInitialPosition();

            diceController.StartToDiceThrow();
        }
    }

    private void CreateTiles(int lenghtMap)
    {
        tileDatas = new List<TileData>(lenghtMap);

        for (int i = 0; i < lenghtMap; i++)
        {
            MapTileView mapTileView = GetMapTileViewFromPoll();

            mapTileView.SetItemType(ItemType.None);

            mapTileView.orderNumber.text = (i + 1).ToString();

            mapTileView.transform.position = initialPositionMap + new Vector3(0,0,tileDistanceEachOther * i);

            mapTileView.gameObject.SetActive(true);

            TileData tileData = new TileData(i, mapTileView);

            tileDatas.Add(tileData);
        }
    }

    public void SpawnItems(List<Bucket<int, ItemType, int>> itemBucket)
    {
        foreach (Bucket<int, ItemType, int> keyValuePair in itemBucket)
        {
            int index = keyValuePair.value1;

            ItemType itemType = keyValuePair.value2;

            int amount = keyValuePair.value3;

            tileDatas[index].SetItem(itemType);

            tileDatas[index].MapTileView.SetItemCount(amount);
        }
    }

    public Dictionary<int, ItemType> GetFromJsonItemInfos(TextAsset textAsset)
    {
        return new Dictionary<int, ItemType>();
    }

    public List<Bucket<int, ItemType, int>> GetRandomItemInfos(int lengthOfMap, int itemAmount)
    {
        int[] arrayOfMay = new int[lengthOfMap];

        ItemType[] itemTypesArray = new ItemType[]
        {
            ItemType.Hamburger,
            ItemType.Pumpkin,
            ItemType.Pizza
        };

        for (int i = 0; i < lengthOfMap; i++)
        {
            arrayOfMay[i] = i;
        }

        arrayOfMay.Shuffle();

        List<Bucket<int, ItemType, int>> bucketList = new List<Bucket<int, ItemType, int>>();

        for (int i = 0; i < itemAmount; i++)
        {
            itemTypesArray.Shuffle();

            int amount = UnityEngine.Random.RandomRange(1, 10);

            bucketList.Add(new Bucket<int, ItemType, int>()
            {
                value1 = arrayOfMay[i],
                value2 = itemTypesArray[0],
                value3 = amount

            });
        }

        return bucketList;
    }
}


