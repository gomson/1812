﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameSlotButton : UIGenericButton {
	private Text gameSlotBtnText;
	private Button gameSlotBtn;
	private Color defaultNormalBtnColor;
	private string defaultSlotText;
	private string unityLastPlayableSceneName = "";
	private string lastPlayableSceneID = "";
	private DateTime lastTime;
	private int numberSlot = 0;
	private bool isSlotEmpty = true;

	public delegate void UnSelectAllGameSlotButtons();
	public static event UnSelectAllGameSlotButtons unSelectAllGameSlots;
	public delegate void OnSavedGameSlotClick(int number);
	public static event OnSavedGameSlotClick onSavedGameSlotClick;


	private void OnEnable(){
		//ModalWindowHandler.onInitialized += DisableButton;
		//ModalWindowHandler.onDisable += EnableButton;
		LocalizedTextManager.currentLanguageChanged += ChangeLanguage;
		SavesGameSlotsManager.unSelectAllGameSlots += UnSelected;
		GameSlotButton.unSelectAllGameSlots += UnSelected;
	}
	
	private void OnDisable(){
		//ModalWindowHandler.onInitialized -= DisableButton;
		//ModalWindowHandler.onDisable += EnableButton;
		LocalizedTextManager.currentLanguageChanged -= ChangeLanguage;
		SavesGameSlotsManager.unSelectAllGameSlots -= UnSelected;
		GameSlotButton.unSelectAllGameSlots -= UnSelected;
	}
	
	private void ChangeLanguage(){
		if(unityLastPlayableSceneName != ""){
			Initialize(numberSlot, unityLastPlayableSceneName, lastTime);
		}
		else{
			defaultSlotText = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "SAVED_GAMES", "EMPTY_SLOT")[0];
			gameSlotBtnText.text = "Slot " + numberSlot + ":\n" + defaultSlotText;
		}
	}
	
	private void Awake () {
		gameSlotBtnText = this.transform.FindChild("Text").GetComponent<Text>();
		gameSlotBtn = this.transform.GetComponent<Button>();
		defaultNormalBtnColor = gameSlotBtn.image.color;
		defaultSlotText = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "SAVED_GAMES", "EMPTY_SLOT")[0];
	}

	protected override void Update() {
		if(isMouseOver){
			if(this.GetComponent<Button>().interactable){
			CustomCursorController.Instance.ChangeCursorOverUIButton();
			}
		}
	}

	public void Initialize(int number){
		numberSlot = number;
		gameSlotBtnText = this.transform.FindChild("Text").GetComponent<Text>();
		gameSlotBtnText.text = "Slot " + numberSlot + "\n" + defaultSlotText;
	}

	public void Initialize(int number, string playableSceneName, DateTime datetime){
		numberSlot = number;
		unityLastPlayableSceneName = playableSceneName;
		lastTime = datetime;
		lastPlayableSceneID = GameController.UnitySceneNameToSceneNameID(unityLastPlayableSceneName);

		string _localizedLastTimeIntroText = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "SAVED_GAMES", "LAST_TIME")[0]; 
		string _localizedLastPlaceIntroText = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "SAVED_GAMES", "LAST_PLACE")[0];
		string _lastPlace = LocalizedTextManager.GetLocalizedText("LIST_PLAYABLE_SCENES", lastPlayableSceneID, "NAME")[0];
		string _lastTimePlayed = lastTime.ToString("dd/MM/yyyy HH:mm");


		if(Application.loadedLevelName == "MainMenu"){
			gameSlotBtnText.text = "Slot " + numberSlot + " \n";
			gameSlotBtnText.text += _localizedLastPlaceIntroText + ": " + _lastPlace + "\n" + _localizedLastTimeIntroText + ": " + _lastTimePlayed;
		}
		else{
			gameSlotBtnText.text = "Slot " + numberSlot + " \n";
			gameSlotBtnText.text += _localizedLastPlaceIntroText + ":\n" + _lastPlace + "\n" + _localizedLastTimeIntroText + ":\n" + _lastTimePlayed;
		}


		isSlotEmpty = false;
	}

	public void OnClick(){
		unSelectAllGameSlots();
		Selected();
		onSavedGameSlotClick(numberSlot);
	}

	public void Selected(){
		gameSlotBtn.image.color = gameSlotBtn.colors.highlightedColor;
	}

	public void UnSelected(){
		gameSlotBtn.image.color = defaultNormalBtnColor;
	}

	private void DisableButton(){
		this.GetComponent<Button>().interactable = false;
	}

	private void EnableButton(){
		this.GetComponent<Button>().interactable = true;
	}

	public void FillSlot(){
		isSlotEmpty = false;
	}

	public void ClearSlot(){
		isSlotEmpty = true;
		Initialize(numberSlot);
	}

	public bool IsSlotEmpty(){
		return isSlotEmpty;
	}

}