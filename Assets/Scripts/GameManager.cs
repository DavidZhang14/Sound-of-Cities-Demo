﻿using SVS;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CameraMovement cameraMovement;
    public RoadManager roadManager;
    public InputManager inputManager;

    public UIController uiController;

    private StructureManager structureManager;
    public PlacementManager placementManager;
    private GameObject character;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        character = GameObject.Find("Character");
        if (StructureManager.instance != null) structureManager = StructureManager.instance;
        else Debug.LogError("StructureManager not found.");
        uiController.OnRoadPlacement += RoadPlacementHandler;
        uiController.OnHousePlacement += HousePlacementHandler;
        uiController.OnSpecialPlacement += SpecialPlacementHandler;
        uiController.OnEdit += EditHandler;
    }

    private void SpecialPlacementHandler()
    {
        ClearInputActions();
        uiController.buildingPanel.gameObject.SetActive(true);
        uiController.buildingPanel.DisplaySpecialList();
        inputManager.OnMouseClick += structureManager.PlaceSpecial;
    }

    private void HousePlacementHandler()
    {
        ClearInputActions();
        uiController.buildingPanel.gameObject.SetActive(true);
        uiController.buildingPanel.DisplayHouseList();
        inputManager.OnMouseClick += structureManager.PlaceHouse;
    }

    private void RoadPlacementHandler()
    {
        ClearInputActions();
        uiController.buildingPanel.gameObject.SetActive(false);
        inputManager.OnMouseClick += roadManager.PlaceRoad;
        inputManager.OnMouseHold += roadManager.PlaceRoad;
        inputManager.OnMouseUp += roadManager.FinishPlacingRoad;
    }

    private void EditHandler() 
    {
        ClearInputActions();
        uiController.buildingPanel.gameObject.SetActive(false);
        inputManager.OnMouseClick += selectEditTarget;
    }

    private void ClearInputActions()
    {
        inputManager.OnMouseClick = null;
        inputManager.OnMouseHold = null;
        inputManager.OnMouseUp = null;
    }

    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x,0, inputManager.CameraMovementVector.y));
    }

    private void selectEditTarget(Vector3Int position)
    {
        if (placementManager.CheckIfPositionIsOfType(position, CellType.House) ||
        placementManager.CheckIfPositionIsOfType(position, CellType.Special)) //检查选择的位置是否有建筑
        {
            UIController.Instance.infoPanel.gameObject.SetActive(true);
            UIController.Instance.editTarget = placementManager.GetSoundEmitter(position);
            UIController.Instance.updateInfoPanel();
        }
        else 
        {
            UIController.Instance.infoPanel.gameObject.SetActive(false);
        }
    }
    public void OpenSavePanel() {
        GameObject savePanel = GameObject.Find("Canvas").transform.Find("SavePanel").gameObject;
        if (savePanel != null) savePanel.SetActive(true);
        else Debug.LogError("GameObject 'SavePanel' not found.");
    }
    public void OpenLoadPanel() {
        //TODO
    }
    public static void SaveButtonClicked() {
        string saveName = GameObject.Find("SaveInput").GetComponent<TMP_InputField>().text;
        SaveSystem.SaveCity(saveName);
        GameObject.Find("SavePanel").SetActive(false);
    }
    public static void LoadButtonClicked() {
        string saveName = "test"; //TODO
        CityData data = SaveSystem.loadCity(saveName);
        data.Deserialize();
    }
}
