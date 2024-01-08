﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public Action OnRoadPlacement, OnHousePlacement, OnSpecialPlacement, OnEdit;
    [SerializeField] private Button placeRoadButton, placeHouseButton, placeSpecialButton, editButton;

    public Color outlineColor;
    List<Button> buttonList;
    public StructureSoundEmitter editTarget;
    public InfoPanel infoPanel;
    public BuildingPanel buildingPanel;
    [SerializeField] private GameObject advancedPanel;
    [SerializeField] private GameObject ConfirmationPanel;
    private void Awake() {
        if (Instance != null) {
            Destroy (gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        buttonList = new List<Button> { placeHouseButton, placeRoadButton, placeSpecialButton, editButton };

        placeRoadButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeRoadButton);
            OnRoadPlacement?.Invoke();

        });
        placeHouseButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeHouseButton);
            OnHousePlacement?.Invoke();

        });
        placeSpecialButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeSpecialButton);
            OnSpecialPlacement?.Invoke();

        });
        editButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(editButton);
            OnEdit?.Invoke();

        });
    }

    private void ModifyOutline(Button button)
    {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = outlineColor;
        outline.enabled = true;
    }

    private void ResetButtonColor()
    {
        foreach (Button button in buttonList)
        {
            button.GetComponent<Outline>().enabled = false;
        }
    }

    public void UpdateInfoPanel() 
    {
        infoPanel.Instrument.text = editTarget.instrument;
        infoPanel.targetGridDropdown.value = editTarget.targetGrid - 1;
        infoPanel.pitchDropdown.value = editTarget.pitch;
        infoPanel.volumeSlider.value = editTarget.objectVolume;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) advancedPanel.SetActive(!advancedPanel.activeSelf);
        
        // Number hotkeys
        if (Input.GetKey(KeyCode.Alpha1)) placeRoadButton.onClick.Invoke();
        else if (Input.GetKey(KeyCode.Alpha2)) placeHouseButton.onClick.Invoke();
        else if (Input.GetKey(KeyCode.Alpha3)) placeSpecialButton.onClick.Invoke();
        else if (Input.GetKey(KeyCode.Alpha4)) editButton.onClick.Invoke();
    }
    public void OpenConfirmationPanel(string message) {
        ConfirmationPanel.SetActive(true);
        ConfirmationPanel.transform.Find("Message").GetComponent<TMP_Text>().SetText(message);
    }
}
