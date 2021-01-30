public class Tile
{

    public int x;
    public int y;
    public string type;

    public Tile(int posX, int posY, string tileType) {
        x = posX;
        y = posY;
        type = tileType;
    }

    public override string ToString() {
        return "[" + x.ToString() + ", " + y.ToString() + ", " + type + "]";
    }
    
}
