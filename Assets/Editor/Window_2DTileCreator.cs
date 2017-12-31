using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class Window_2DTileCreator : EditorWindow
{
    #region Load Variables
    public CheckDirectories directory;
    public LayerAndTags layersandtags;
    public SetTileTextures tileTextures;

    public int unityVersion, setupStage, currentTabOpen, tileSelectedID, firstClick, massTileWidth, massTileHeight, dragDirection;
    public float boxMaxX;
    public bool showConsole, tileSelected, removingTile, createMassTile, holdingControl;
    public static bool windowIsOpen, selectParent, autoGroup, eraseTool, mouseDown;
    public string customTileGroupName = "", RectangleWidth = "1", RectangleHeight = "1";
    public GameObject[] currentTabList;
    public GameObject gizmoCursor, gizmoTile, lowerTileHit, middleTileHit, aboveTileHit, massTileParent;
    public GUIStyle currentTileTex;
    public Vector2 scrollPos, mousePos, startDrag;
    public RaycastHit2D lowerLayerRay, middleLayerRay, aboveLayerRay;
    public Rect windowRect;
    Tool lastTool = Tool.None;
    #endregion

    #region Normal Update & Show Consol Logs
    void Update()
    {
    
    /*************************\
    \****PLAYER PREFRENCES****/
        showConsole = true;
    /****PLAYER PREFRENCES****\
    \*************************/
        
        
        if (tileSelected)
        {
            Tools.current = Tool.None;
            if (gizmoCursor != null)
            {
                gizmoCursor.transform.position = new Vector2(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y));
            }
            if (gizmoTile != null)
            {
                gizmoTile.transform.position = gizmoCursor.transform.position;
                //Debug.Log("gizmoTile Position" + gizmoTile.transform.position);
            }
            EditorApplication.MarkSceneDirty(); //   **** IMPORTANT ****  REMOVE THIS LINE IF NOT USING UNITY 5.
        }
        else
        {
            Tools.current = lastTool;
        }
        if (Selection.activeGameObject != null)
        {
            if (Selection.activeGameObject.transform.parent != null)
            {
                if (selectParent)
                {
                    Selection.activeGameObject = Selection.activeGameObject.transform.parent.gameObject;
                }
            }
        }
        if (!windowIsOpen)
        {
            this.Close();
        }
        Repaint(); // DO NOT DELETE.
    }
    
    #endregion
  
    [MenuItem("Window/2D Tile Map Creator  %m")]
    static void Init()
    {
        #region Open Static Window
        Window_2DTileCreator win = (Window_2DTileCreator)EditorWindow.GetWindow(typeof(Window_2DTileCreator));
        win.minSize = new Vector2(600, 315);
        win.title = " Map Creator";
        win.Show();
        windowIsOpen = !windowIsOpen;
        if(GameObject.Find("CUSTOM GROUP EDIT") != null)
        {
            autoGroup = true;
        }
        #endregion
    }

    #region  Shortcuts
    [MenuItem("Active Selection/Always Select Parent %1")]
    static void ToggleAlwaysSelectParent( )
    {
        selectParent = !selectParent;
    }

    [MenuItem("Active Selection/Select Next Parent #a")]
    static void SelectNextParent()
    {
        Transform t = Selection.activeTransform;
        if (t != null)
        {
            if (t.transform.parent != null)
            {
                GameObject s = t.transform.parent.gameObject;
                if (s != null)
                {
                    Selection.activeGameObject = s;
                }
            }
        }
    }

    [MenuItem("Active Selection/Auto Group %2")]
    static void AutoGroup()
    {
        autoGroup = !autoGroup;
    }
    #endregion

    void OnEnable()
    {
        #region Enable Scene View
        SceneView.onSceneGUIDelegate += SceneGUI;
        lastTool = Tools.current;
        #endregion

        #region Obtain Unity Version
        string aUV = Application.unityVersion;
        string[] uV = new string[aUV.Length];
        for (int a = 0; a < aUV.Length; a++)
        {
            uV[a] = System.Convert.ToString(aUV[a]);
        }
        if(showConsole)
        {
            Debug.Log("Using Unity Version " + uV[0]);
        }
        if (uV[0] == "5")
        {
            unityVersion = 5;
        }
        if (uV[0] == "4")
        {
            unityVersion = 4;
        }
        #endregion
    }

    void OnDisable()
    {
        #region Disable Script
        SceneView.onSceneGUIDelegate -= SceneGUI;
        DestroyImmediate(GameObject.Find("gizmoCursor"));
        DestroyImmediate(GameObject.Find("gizmoTile"));
        windowIsOpen = false;
        //Tools.current = lastTool;
        #endregion
    }

    void OnInspectorUpdate()
    {
        #region Load Scripts
        if (!directory)
        {
            directory = ScriptableObject.CreateInstance<CheckDirectories>();
        }
        if (!layersandtags)
        {
            layersandtags = ScriptableObject.CreateInstance<LayerAndTags>();
        }
        if (!tileTextures)
        {
            tileTextures = ScriptableObject.CreateInstance<SetTileTextures>();
        }
        if (directory.fileCheckClear == 0)
        {
            if(showConsole)
            {
                Debug.Log("Initializing Directory Check...");
            }
            directory.BeginDirectoryCheck();
            setupStage = directory.fileCheckClear;

            #region Clear Up Old Window
            // Occurs on change of scene or restart
            tileSelected = false;
            #endregion
        }
        if (setupStage == 2)
        {
            if (layersandtags.LayerCheckClear == 0)
            {
                if (showConsole)
                {
                    Debug.Log("Directory Check Complete \nInitializing Layer Creator...");
                }
                layersandtags.createTagsandLayers();
            }
            if (layersandtags.LayerCheckClear == 1)
            {
                if(showConsole)
                {
                    Debug.Log("Layer and Tag Creator Complete \nInitializing...");
                }
                tileTextures.LoadListsAndGUIFirst();
                currentTabList = tileTextures.currentTileList;
                if (showConsole)
                {
                    Debug.Log("All Tiles have been formatted and listed. currentObjectList Set.\nWatching Tile Positions...");
                }
                setupStage = 3;
            }
            else if (layersandtags.LayerCheckClear == -1)
            {
                Debug.LogError("Layer Creator Failed. Closing Window.");
                this.Close();
            }
        }
        if (setupStage == 3 && tileTextures != null)
        {
            #region Monitor Window Size & Tab
            windowRect = new Rect(this.position.x, this.position.y, this.position.width, this.position.height);
            int lastTileInRow = tileTextures.maxTileColumn;
            //Change Border Box BG to wrap around tile
            if (tileTextures.tileBoxX[lastTileInRow] == 0)
            {
                boxMaxX = tileTextures.tileBoxX[lastTileInRow - 1] + 80;
            }
            else
            {
                boxMaxX = tileTextures.tileBoxX[lastTileInRow] + 80;
            }
            //If item list is less than windows minimum column length
            if (currentTabList.Length <= lastTileInRow)
            {
                boxMaxX = tileTextures.tileBoxX[currentTabList.Length] + 10;
            }
            //Detect Tab change
            if (currentTabOpen != tileTextures.currentTab)
            {
                tileTextures.LoadListsAndGUIFirst();
                currentTabList = tileTextures.currentTileList; // Load Current Open Tabs Textures
            }
            //Detect window resize
            if (windowRect != tileTextures.windowRect)
            {
                tileTextures.GetTilePositions();
            }
            #endregion
        }
        if (setupStage == -1)
        {
            Debug.LogError("Directory Check Failed. Closing Window.");
            this.Close();
        }
        #endregion

        #region Load Gizmos on Tile Select
        if (tileSelected && setupStage == 3)
        {
            if (gizmoCursor == null)
            {
                gizmoCursor = (GameObject) Instantiate(Resources.Load(directory.resourceFile[4]));
                gizmoCursor.name = "gizmoCursor";
                gizmoCursor.hideFlags = HideFlags.HideInHierarchy;
            }

            if (gizmoTile == null)
            {
                gizmoTile = (GameObject)Instantiate(currentTabList[tileSelectedID]);
                gizmoTile.transform.name = "gizmoTile";
                SpriteRenderer[] tempTile;
                tempTile = gizmoTile.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer sRen in tempTile)
                {
                    sRen.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f); //40% transparent
                }
                gizmoTile.hideFlags = HideFlags.HideInHierarchy;
            }
        }
        else
        {
            DestroyImmediate(GameObject.Find("gizmoCursor"));
            DestroyImmediate(GameObject.Find("gizmoTile"));
        }
        #endregion
    }

    void SceneGUI(SceneView sceneView)
    {
        #region Gain Scene Views Mouse Coordinates
        if (setupStage == 3 && tileTextures != null)
        {
            #region Setup Mouse Coordinates
            Event e = Event.current;
            Ray worldRays = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            mousePos = worldRays.origin;
            #endregion

            #region User Input on Scene
            if (tileSelected && gizmoTile != null)
            {
                if(showConsole)
                {
                    Debug.Log("Tile Position: X = " + gizmoTile.transform.position.x + ", Y = " + gizmoTile.transform.position.y);
                }
                CreateTileGroup(autoGroup);
                int controlID = GUIUtility.GetControlID(FocusType.Passive);
                if (e.type == EventType.layout)
                {
                    HandleUtility.AddDefaultControl(controlID);
                }
                switch (e.type)
                {
                    case EventType.mouseDown:
                        {
                            if (e.button == 0) //LEFT CLICK DOWN
                            {
                                mouseDown = true;
                            }

                            if (e.button == 1) //RIGHT CLICK DOWN
                            {
                                ObtainTileBelow();
                                if (lowerTileHit != null || middleTileHit != null || aboveTileHit != null)
                                {
                                    CheckTile(lowerTileHit, middleTileHit, aboveTileHit, -1);
                                }
                                else if (lowerTileHit == null && middleTileHit == null && aboveTileHit == null)
                                {
                                    tileSelected = false;
                                }
                            }
                            break;
                        }
                    case EventType.mouseUp:
                        {
                            if (e.button == 0) //LEFT CLICK UP
                            {
                                mouseDown = false;
                            }
                            if (e.button == 1) //RIGHT CLICK UP
                            {

                            }
                            break;
                        }
                    case EventType.keyDown:
                        {
                            if (e.keyCode == KeyCode.LeftControl)
                            {
                                holdingControl = true;
                            }
                            if (e.keyCode == KeyCode.Z)
                            {
                                //Debug.Log("Holding Z");
                                eraseTool = true;
                            }
                            if (e.keyCode == KeyCode.Escape)
                            {
                                //Debug.Log("Pressing Escape on scene");
                                tileSelected = false;
                            }
                            Event.current.Use();    // if you don't use the event, the default action will still take place.
                        }
                        break;
                    case EventType.keyUp:
                        {
                            if (e.keyCode == KeyCode.LeftControl)
                            {
                                holdingControl = false;
                            }
                            if (e.keyCode == KeyCode.Z)
                            {
                               // Debug.Log("Z off");
                                eraseTool = false;
                            }
                        }
                        break;
                }
            }
            else
            {
                if (GameObject.Find("CUSTOM GROUP EDIT") != null && GameObject.Find("CUSTOM GROUP EDIT").transform.childCount == 0)
                {
                    DestroyImmediate(GameObject.Find("CUSTOM GROUP EDIT"));
                }
            }
            #endregion
          
            #region Mouse Click Actions
            if (mouseDown)
            {
                if(holdingControl)
                {
                    if (dragDirection == 0)
                    {
                        startDrag = gizmoTile.transform.position;
                        dragDirection = 3;
                    }

                    if (gizmoTile.transform.position.x > startDrag.x && dragDirection == 3 || gizmoTile.transform.position.x < startDrag.x && dragDirection == 3)
                    {
                        dragDirection = 1;

                    }
                    if (gizmoTile.transform.position.y > startDrag.y && dragDirection == 3 || gizmoTile.transform.position.y < startDrag.y && dragDirection == 3)
                    {
                        dragDirection = 2;
                    }
                }
                if (eraseTool && !holdingControl)
                {
                    removingTile = true;
                }
                else
                {
                    //Add Tile On Mouse Down
                    ObtainTileBelow();
                    CheckTile(lowerTileHit, middleTileHit, aboveTileHit, currentTabList[tileSelectedID].transform.FindChild("Editor Hitbox").gameObject.layer);
                    removingTile = false;
                }
            }
            else
            {
                if (eraseTool)
                {
                    removingTile = false;
                }
                dragDirection = 0;
            }
            if (removingTile)
            {
                ObtainTileBelow();
                if (lowerTileHit != null || middleTileHit != null || aboveTileHit != null)
                {
                    CheckTile(lowerTileHit, middleTileHit, aboveTileHit, -1);

                }
            }
            #endregion
        }
        #endregion
    }
    
    void OnGUI()
    {
        #region Editor GUI
        currentTabOpen = GUILayout.Toolbar(currentTabOpen, new string[] { "Lower Layer", "Middle Layer", "Above Layer" });
        Event e = Event.current;
        if (setupStage == 3 && tileTextures != null)
        {
            #region Inside Window
            GUILayout.Box("", GUILayout.Width(windowRect.width - 5), GUILayout.Height(100)); //Setting Box
            GUI.Box(new Rect(30, 30, 64, 64), tileTextures.tileHighLTex, new GUIStyle()); // Current Tile Base Box
            GUI.Label(new Rect(5, 90, 200, 200), "Currently Selected Tile"); //Current Tile Label
            selectParent = GUI.Toggle(new Rect(150, 25, 190, 18), selectParent, "Always Select Parent. Ctrl + 1");
            eraseTool = GUI.Toggle(new Rect(350, 25, 310, 40), eraseTool, "Eraser Tool will erase all layers on a tile. Hold 'Z'.\nRight Click to erase a single layer."); 
            autoGroup = GUI.Toggle(new Rect(150, 40, 190, 18), autoGroup, "Create Groups. Ctrl + 2");
            GUI.Label(new Rect(450, 80, 250, 40), "Hold Left Control to draw in straight lines.");
            if(autoGroup)
            {
                GUI.Label(new Rect(150, 60, 220, 40), "Finish Editing Custom Group; Name:");
                //customTileGroupName = GUI.TextField(new Rect(370, 57, 110, 20), customTileGroupName, 25);
                customTileGroupName = EditorGUI.TextField(new Rect(370, 57, 110, 20), "", customTileGroupName);
                if (GUI.Button(new Rect(500, 57, 50, 25), "Finish."))
                {
                    GameObject.Find("CUSTOM GROUP EDIT").transform.name = customTileGroupName;
                    EditorWindow.GetWindow<SceneView>();
                }
            }

            #region Create Rectangle Button
            GUIStyle tmp = new GUIStyle();
            tmp.normal.textColor = Color.white;
            tmp.fontSize = 10;
            tmp.fontStyle = FontStyle.Bold;
            tmp.contentOffset = new Vector2(0, 10);

            GUI.Label(new Rect(150, 85, 110, 25), "Create Mass Tile:");
            //GUI.Box(new Rect(410, 10, 65, 25), "");
            GUI.Label(new Rect(260, 105, 50, 35), "-Width-");
            RectangleWidth = EditorGUI.TextField(new Rect(270, 80, 22, 22),"", RectangleWidth);
            RectangleWidth = Regex.Replace(RectangleWidth, "[^0-9]", "");
            GUI.Label(new Rect(300, 80, 35, 35), "X", tmp);
            GUI.Label(new Rect(305, 105, 50, 35), "-Height-");
            RectangleHeight = EditorGUI.TextField(new Rect(315, 80, 22, 22), "", RectangleHeight);
            RectangleHeight = Regex.Replace(RectangleHeight, "[^0-9]", "");
            #region Create Mass Tile Button
            if (GUI.Button(new Rect(365, 80, 80, 35), "Create Tile"))
            {
                if (RectangleWidth == "")
                {
                    massTileWidth = 1;
                    RectangleWidth = massTileWidth.ToString();
                }

                if (RectangleHeight == "")
                {
                    massTileHeight = 1;
                    RectangleHeight = massTileHeight.ToString();
                }
                massTileWidth = System.Int32.Parse(RectangleWidth);
                massTileHeight = System.Int32.Parse(RectangleHeight);
                
                if (massTileWidth <= 0)
                {
                    massTileWidth = 1;
                }
                if (massTileHeight <= 0)
                {
                    massTileHeight = 1;
                }
                if (massTileWidth > 99)
                {
                    massTileWidth = 99;
                    RectangleWidth = "99";
                }
                if (massTileHeight > 99)
                {
                    massTileHeight = 99;
                    RectangleHeight = "99";
                }
                if(!createMassTile)
                {
                    createMassTile = true;
                    CreateMassTile();
                }
            }
            #endregion


            #endregion

            scrollPos = GUILayout.BeginScrollView(scrollPos, false, true, GUILayout.Width(windowRect.width), GUILayout.Height(windowRect.height - 110));
            GUIStyle style = new GUIStyle(); //Blank GUIStyle to clear current textures
            GUIStyle styleText = new GUIStyle(); //Blank GUIStyle to clear current textures
            Font myFont = (Font)Resources.Load(directory.resourceFile[5], typeof(Font));
            style.normal.background = tileTextures.tileBaseTex;
            style.hover.background = tileTextures.tileHighLTex;
            styleText.font = myFont;
            styleText.normal.textColor = Color.gray;
            styleText.fontSize = 12;
            styleText.fontStyle = FontStyle.Bold;
            styleText.wordWrap = true;
            styleText.alignment = TextAnchor.MiddleCenter;
            styleText.contentOffset = new Vector2(0, 40);
            GUI.Box(new Rect(15, 15, boxMaxX, tileTextures.tileBoxY[currentTabList.Length - 1] + 65), ""); //BG Box wrapped around Content Box
            GUILayout.Box("", new GUIStyle(), GUILayout.Width(windowRect.width - 50), GUILayout.Height(tileTextures.tileBoxY[currentTabList.Length - 1] + 85)); //Content Box, used for size of window scroll only
            GUI.BeginGroup(new Rect(25, 0, windowRect.width, tileTextures.tileBoxY[currentTabList.Length - 1] + 85));
            for (int x = 0; x < currentTabList.Length; x++)
            {
                if (GUI.Button(new Rect(tileTextures.tileBoxX[x], tileTextures.tileBoxY[x], 64, 64), "", style)) //Tile Background Box
                {
                    if(showConsole)
                    {
                        Debug.Log("Tile " + currentTabList[x].name + " Selected");
                    }
                    tileSelectedID = x;
                    tileSelected = true;
                    if(gizmoTile != null)
                    {
                        DestroyImmediate(gizmoTile); 
                    }
                    currentTileTex = new GUIStyle();
                    currentTileTex = tileTextures.TileListStyle[tileSelectedID];
                }
                GUI.Box(new Rect(tileTextures.tileBoxX[x] + 8, tileTextures.tileBoxY[x] + 8, 48, 48), "", tileTextures.TileListStyle[x]); // Tile Actual Tile Sprite
                GUI.Label(new Rect(tileTextures.tileBoxX[x] - 4, tileTextures.tileBoxY[x], 64, 64), tileTextures.currentTileList[x].name, styleText);
            }
            GUI.EndGroup();
            GUILayout.EndScrollView();
            if (tileSelected)
            {
                GUI.Box(new Rect(37.5f, 37.5f, 48, 48), "", currentTileTex);
                if (EditorWindow.focusedWindow != this && EditorWindow.focusedWindow != EditorWindow.GetWindow<SceneView>())
                {
                    // Debug.Log("Have Clicked Off Editor");
                    tileSelected = false;
                }
            }
            #endregion

            #region User Input on Editor
            switch (e.type)
            {
                case EventType.keyDown:
                    {
                        if (e.keyCode == KeyCode.Escape) //Pressing Escape on Editor Window
                        {
                            tileSelected = false;
                        }
                        if (e.keyCode == KeyCode.Z)
                        {
                            //Debug.Log("Holding Z");
                            eraseTool = true;
                        }
                        e.Use();    // if you don't use the event, the default action will still take place.
                    }
                    break;
                case EventType.keyUp:
                    {
                        if (e.keyCode == KeyCode.Z)
                        {
                            // Debug.Log("Z off");
                            eraseTool = false;
                        }
                    }
                    break;
            }
            #endregion
        }
        #endregion
    }

    void ObtainTileBelow()
    {
        #region Set Tile Hitting Layer
        var lowerLayerBitMask = 1 << (int)layersandtags.requiredLayers[0]; //BitShift into Layer needed to detect.
        var middleLayerBitMask = 1 << (int)layersandtags.requiredLayers[1]; //BitShift into Layer needed to detect.
        var aboveLayerBitMask = 1 << (int)layersandtags.requiredLayers[2]; //BitShift into Layer needed to detect.
        if(gizmoTile!= null)
        {
            if (dragDirection == 1)
            {
                gizmoTile.transform.position = new Vector2(gizmoTile.transform.position.x, startDrag.y);
            }
            if (dragDirection == 2)
            {
                gizmoTile.transform.position = new Vector2(startDrag.x, gizmoTile.transform.position.y);
            }
            lowerLayerRay = Physics2D.Raycast(gizmoTile.transform.position, Vector3.forward, 0, lowerLayerBitMask);
            middleLayerRay = Physics2D.Raycast(gizmoTile.transform.position, Vector3.forward, 0, middleLayerBitMask);
            aboveLayerRay = Physics2D.Raycast(gizmoTile.transform.position, Vector3.forward, 0, aboveLayerBitMask);
        }
        if (lowerLayerRay.collider)
        {
            lowerTileHit = (GameObject)lowerLayerRay.collider.gameObject.transform.parent.gameObject;
        }
        else
        {
            lowerTileHit = null;
        }

        if (middleLayerRay.collider)
        {
            middleTileHit = (GameObject)middleLayerRay.collider.gameObject.transform.parent.gameObject;
        }
        else
        {
            middleTileHit = null;
        }

        if (aboveLayerRay.collider)
        {
            aboveTileHit = (GameObject)aboveLayerRay.collider.gameObject.transform.parent.gameObject;
        }
        else
        {
            aboveTileHit = null;
        }
        //Debug.Log("The layer you are hovering above is " + "Lower = " + lowerTileHit + " Middle = " + middleTileHit + " Above = " + aboveTileHit);
        #endregion
    }

    void CheckTile(GameObject lowerTile, GameObject midTile, GameObject aboveTile, int selectedTileLayer)
    {
        #region Detect which layer to destroy first
        if (selectedTileLayer == layersandtags.requiredLayers[0])
        {
            if (lowerTile != null)
            {
                DestroyImmediate(lowerTile);
            }
            if(firstClick == 0)
            {
                ObtainTileBelow();
                AddTile();
            }
        }
        if (selectedTileLayer == layersandtags.requiredLayers[1])
        {
            if (midTile != null)
            {
                DestroyImmediate(midTile);
            }
            if (firstClick == 0)
            {
                ObtainTileBelow();
                AddTile();
            }
        }
        if (selectedTileLayer == layersandtags.requiredLayers[2])
        {
            if (aboveTile != null)
            {
                DestroyImmediate(aboveTile);
            }
            if (firstClick == 0)
            {
                ObtainTileBelow();
                AddTile();
            }
        }
        if (selectedTileLayer == -1)
        {
            if (aboveTile != null)
            {
                DestroyImmediate(aboveTile);
            }
            else if (midTile != null)
            {
                DestroyImmediate(midTile);
            }
            else if (lowerTile != null)
            {
                DestroyImmediate(lowerTile);
            }
        }
        #endregion
    }

    void AddTile()
    {
        #region Add Tile to scene
        if(firstClick == 0)
        {
            firstClick = 1;
            CheckTile(lowerTileHit, middleTileHit, aboveTileHit, currentTabList[tileSelectedID].transform.FindChild("Editor Hitbox").gameObject.layer);
        }
        GameObject metaTile = (GameObject)Instantiate(currentTabList[tileSelectedID]);
        metaTile.transform.position = gizmoTile.transform.position;
        metaTile.gameObject.AddComponent<EditorHitbox>();
        if (autoGroup)
        {
            metaTile.transform.parent = GameObject.Find("CUSTOM GROUP EDIT").transform;
        }
        metaTile.transform.FindChild("Editor Hitbox").gameObject.SetActive(true);
        firstClick = 0;
        #endregion
    }

    void CreateMassTile()
    {
        #region Create Mass Tile
        if (createMassTile)
        {
            if(massTileParent == null)
            {
                massTileParent = new GameObject();
            }
            massTileParent.transform.name = "New Mass Tile";
            for (int y = 0; y < massTileHeight; y++)
            {
                for (int x = 0; x < massTileWidth; x++)
                {
                    if (tileSelected)
                    {
                        GameObject TileObject = (GameObject)Instantiate(currentTabList[tileSelectedID]);
                        Vector2 TilePos = new Vector2(x, y);
                        TileObject.gameObject.AddComponent<EditorHitbox>();
                        TileObject.transform.FindChild("Editor Hitbox").gameObject.SetActive(true);
                        TileObject.transform.position = TilePos;
                        TileObject.transform.parent = GameObject.Find("New Mass Tile").transform;
                        if (x == massTileWidth - 1 && y == massTileHeight - 1)
                        {
                            Selection.activeObject = SceneView.currentDrawingSceneView;
                            Camera sceneCam = SceneView.currentDrawingSceneView.camera;
                            Vector3 spawnPos = sceneCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
                            GameObject.Find("New Mass Tile").transform.position = new Vector2((int)spawnPos.x, (int)spawnPos.y);
                            createMassTile = false;
                            Selection.activeGameObject = GameObject.Find("New Mass Tile");
                            GameObject.Find("New Mass Tile").transform.name = "Mass Tile " + RectangleWidth + " x " + RectangleHeight;
                        }
                    }
                    else
                    {
                        Debug.LogWarning("There is no tile selected");
                        DestroyImmediate(massTileParent);
                        createMassTile = false;
                    }
                }
            }
        }
        massTileParent = null;
        #endregion

    }
    void CreateTileGroup(bool createGroup)
    {
        #region Create Custom Group Object
        if (createGroup)
        {
            if (GameObject.Find("CUSTOM GROUP EDIT") == null)
            {
                GameObject group = new GameObject();
                group.transform.position = mousePos;
                group.transform.name = "CUSTOM GROUP EDIT";
            }
        }
        #endregion
    }
}