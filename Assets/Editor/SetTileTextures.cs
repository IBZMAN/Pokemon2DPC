using UnityEditor;
using UnityEngine;
using System.Collections;

public class SetTileTextures : Editor
{
    #region Load Variables
    Window_2DTileCreator mapCreatorWindow;
    public GameObject[] lowerTileList, middleTileList, aboveTileList, currentTileList;
    public Texture2D tileBaseTex, tileHighLTex;
    Sprite tileLostTex, metaTileTex;
    public int maxTile = 999, maxTileColumn;
    public float[] tileBoxX, tileBoxY;
    public int currentTab = 0;
    public GUIStyle[] TileListStyle;
    public Rect windowRect;
    #endregion

    public void LoadListsAndGUIFirst()
    {
        #region Load GUI Files
        if (!mapCreatorWindow)
        {
            mapCreatorWindow = (Window_2DTileCreator)EditorWindow.GetWindow(typeof(Window_2DTileCreator));
        }
        tileBaseTex = Resources.Load(mapCreatorWindow.directory.resourceFile[0]) as Texture2D;
        tileLostTex = Resources.Load<Sprite>(mapCreatorWindow.directory.resourceFile[1]);
        tileHighLTex = Resources.Load(mapCreatorWindow.directory.resourceFile[2]) as Texture2D;
        #endregion

        #region Load Game Lists
        lowerTileList = Resources.LoadAll<GameObject>(mapCreatorWindow.directory.resourcesPath[7]);
        middleTileList = Resources.LoadAll<GameObject>(mapCreatorWindow.directory.resourcesPath[8]);
        aboveTileList = Resources.LoadAll<GameObject>(mapCreatorWindow.directory.resourcesPath[6]);
        #endregion

        #region Find Editor Objects and Set Layers
        for (int a = 0; a < lowerTileList.Length; a++)
        {
            var b = lowerTileList[a].transform.FindChild("Editor Sprite");
            if (b != null)
            {
                b.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("No Editor Sprite Object found on object '" + lowerTileList[a].name + "' please create one.");
            }
            var c = lowerTileList[a].transform.FindChild("Editor Hitbox");
            if (c != null)
            {
                c.gameObject.SetActive(false);
                c.gameObject.layer = (int)mapCreatorWindow.layersandtags.requiredLayers[0];
            }
            else
            {
                Debug.LogError("No Editor Hitbox Object found on object '" + lowerTileList[a].name + "' please create one as this will effect editing.");
            }
        }
        for (int a = 0; a < middleTileList.Length; a++)
        {
            var b = middleTileList[a].transform.FindChild("Editor Sprite");
            if (b != null)
            {
                b.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("No Editor Sprite Object found on object '" + middleTileList[a].name + "' please create one.");
            }
            var c = middleTileList[a].transform.FindChild("Editor Hitbox");
            if (c != null)
            {
                c.gameObject.SetActive(false);
                c.gameObject.layer = (int)mapCreatorWindow.layersandtags.requiredLayers[1];
            }
            else
            {
                Debug.LogError("No Editor Hitbox Object found on object '" + middleTileList[a].name + "' please create one.");
            }
        }
        for (int a = 0; a < aboveTileList.Length; a++)
        {
            var b = aboveTileList[a].transform.FindChild("Editor Sprite");
            if (b != null)
            {
                b.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("No Editor Sprite Object found on object '" + aboveTileList[a].name + "' please create one.");
            }
            var c = aboveTileList[a].transform.FindChild("Editor Hitbox");
            if (c != null)
            {
                c.gameObject.SetActive(false);
                c.gameObject.layer = (int)mapCreatorWindow.layersandtags.requiredLayers[2];
            }
            else
            {
                Debug.LogError("No Editor Hitbox Object found on object '" + aboveTileList[a].name + "' please create one.");                
            }
        }
        #endregion

        #region Load Tile In Window
        LoadCurrentTabList();
        DrawCurrentTiles();
        GetTilePositions();
        #endregion
    }

    public void LoadCurrentTabList()
    {
        #region Change Tab List
        currentTab = mapCreatorWindow.currentTabOpen;
        currentTileList = null;
        if (currentTab == 0)
        {
            currentTileList = lowerTileList;
        }
        if (currentTab == 1)
        {
            currentTileList = middleTileList;
        }
        if (currentTab == 2)
        {
            currentTileList = aboveTileList;
        }
        TileListStyle = null;
        TileListStyle = new GUIStyle[currentTileList.Length];
        #endregion
    }

    public void DrawCurrentTiles()
    {
        for (int a = 0; a < currentTileList.Length; a++)
        {
            #region Obtain Tile GUI Sprite From Editor Sprite
            var b = currentTileList[a].transform.FindChild("Editor Sprite");
            if (b != null)
            {
                metaTileTex = b.gameObject.GetComponent<SpriteRenderer>().sprite;
                b.gameObject.SetActive(false);
            }
            else
            {
                metaTileTex = tileLostTex;
            }
            #endregion

            #region Create GUI Tile Texture
            Color[] pixels = metaTileTex.texture.GetPixels((int)metaTileTex.textureRect.x, (int)metaTileTex.textureRect.y, (int)metaTileTex.textureRect.width, (int)metaTileTex.textureRect.height); //Copys pixles of Sprite.
            Texture2D tileObjectTex = new Texture2D((int)metaTileTex.rect.width, (int)metaTileTex.rect.height); //Create a new Texture to set all of copied pixels to.
            tileObjectTex.filterMode = FilterMode.Point;
            tileObjectTex.SetPixels(pixels);
            tileObjectTex.Apply();
            GUIStyle styleTile = new GUIStyle();
            styleTile.normal.background = tileObjectTex; //Apply Texture to Style BG.
            styleTile.normal.background.name = currentTileList[a].name;
            TileListStyle[a] = styleTile; // Apply temp tile style to Public Main List Tile Style
            #endregion
        }
    }

    public void GetTilePositions()
    {
        #region Load Tiles into Position
        tileBoxX = new float[maxTile];
        tileBoxY = new float[maxTile];
        windowRect = new Rect(mapCreatorWindow.position.x, mapCreatorWindow.position.y, mapCreatorWindow.position.width, mapCreatorWindow.position.height);
        maxTileColumn = (int)((windowRect.width - 115) / 70); //Track number of tiles to start new line

        for (int x = 0; x < maxTile; x++)
        {
            if (x == 0)
            {
                /*Default start position of box Tile0 within the group OnGUI*/
                tileBoxX[x] = 0;
                tileBoxY[x] = 40;
            }
            else
            {
                tileBoxX[x] = tileBoxX[x - 1] + 70;
                tileBoxY[x] = tileBoxY[x - 1]; //Every next tile is set to the previous tiles coordinates, +70 on the X axis(box size<50> + space between box<20>)
                if (tileBoxX[x] >= windowRect.width - 115)//If the next tiles X is greater than the border surrounding the window, move the tile down on Y axis by required space
                {
                    tileBoxX[x] = 0;
                    tileBoxY[x] += 80;
                }
            }
        }
        #endregion
    }
}
