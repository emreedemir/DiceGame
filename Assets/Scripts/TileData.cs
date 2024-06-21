
public class TileData
{
    public int index;

    public MapTileView MapTileView;

    private TileData() { }

    public TileData(int index, MapTileView mapTileView)
    {
        this.index = index;

        this.MapTileView = mapTileView;
    }

    public void SetItem(ItemType itemType)
    {
        MapTileView.SetItemType(itemType);
    }
}