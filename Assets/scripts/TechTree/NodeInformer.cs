using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeInformer : MonoBehaviour {

    public static NodeInformer instance;

    public int questHardness;

    public TextMeshProUGUI informerHead;
    public TextMeshProUGUI informerMessage;
    public TextMeshProUGUI informerDescription;
    [Space]
    public float disappearTime = 10;
    public float timer;
    private bool textUpdated;
    [SerializeField]
    private string _currentShowedNodeName;
    private TechNodeControl _techNodeControl;

    //Camera
    public float cameraXWidth;
    public float cameraYWidth;

    #region Quest Panel
    public GameObject questPnael;
    public TextMeshProUGUI questDescription;
    public TextMeshProUGUI questCost;
    public TextMeshProUGUI applyedText;
    public TextMeshProUGUI questBringMoney;
    public bool questSelected;

    [SerializeField]
    private bool questConditionsMet;


    CurrentSelectedQuest currentSelectedQuest;
    #endregion



    private void Awake()
    {
        instance = instance ?? this;
    }

    private void Start()
    {
        instance = instance ?? this;
       // informerMessage= informerObject.GetComponent<TextMeshProUGUI>();
       // informerHead = informerHeadObject.GetComponent<TextMeshProUGUI>();
      //  informerDescription= informerDescriptionObject.GetComponent<TextMeshProUGUI>();
        StartingSetup();
    }
    public void StartingSetup()
    {
        GetCameraSize();
        ResetQuest();
    }

    //Tech node button pressed, Showing description
    public void ShowTechDescription(string head, string fullText, string currentShowedNodeName, TechNodeControl techNodeControl)
    {
        informerDescription.text = fullText;
        informerHead.text = head;
        _currentShowedNodeName = currentShowedNodeName;
        _techNodeControl = techNodeControl;
    }


    //Apply Node Button Clicked
    public void ApplyNodeButtonClicked()
    {
       
        if (String.IsNullOrEmpty(_currentShowedNodeName))
        {
            return;
        }
        _techNodeControl.ResearchButtonClicked(_currentShowedNodeName);
    }

    // Show Node Result
    public void UpdateInformer(string message, string head, string fullText)
    {
        SetTimer();
        informerMessage.text = message;
        informerDescription.text = fullText;
        informerHead.text = head;
      
    }

   
    private void Update()
    {
        timer -= MainCount.instance.deltaTime;
        if ((timer <= 0)&&textUpdated)
        {
            textUpdated = false;
            ClearText();
        }
        if (timer <= -1000)
        {
            timer = 0;
        }
    }


    private void ClearText()
    {
        informerMessage.text = String.Empty;
        informerDescription.text = String.Empty;
        informerHead.text = String.Empty;
    }


    #region //Camera
    public void GetCameraSize()
    {
        Camera camera = Camera.main;
        float halfHeight = Camera.main.orthographicSize;
        cameraXWidth = camera.aspect * halfHeight;
    }
    #endregion

    public void OnGetQuestButtonCkick()
    {
        if (!questSelected)
        {
            currentSelectedQuest = QuestMainEngine.instance.GenerateQuest();
            if (currentSelectedQuest!=null&&!currentSelectedQuest.result)
            {
                return;
            }
            questSelected = true;
            questDescription.text = currentSelectedQuest.currentQuestScriptable.questDescription;
            questCost.text = currentSelectedQuest.questCost.ToString();
            questBringMoney.text = currentSelectedQuest.questBringMoneyIfCompleted.ToString();
            ISAllQuestConditionsMet(currentSelectedQuest.questCost);
        }
        questPnael.SetActive(true);
    }

    public void ResetQuest()
    {
        questSelected = false;
        applyedText.text = string.Empty;
        questConditionsMet = false;
    }

    public void OnApplyQuestButtonClick()
    {
        if (questConditionsMet)
        {
            applyedText.text = "Applyed";
            QuestMainEngine.instance.QuestApplyed(currentSelectedQuest);
            questPnael.SetActive(false);
            questConditionsMet = false;
        }

    }

    public void OnCancelQuestButtonClick()
    {
        questPnael.SetActive(false);
    }

    private void ISAllQuestConditionsMet(int cost)
    {
        if (PlayerStats.instance.playerCash > cost)
        {
            questConditionsMet = true;
        }
    }

    public void ShowMessage(string head, string description)
    {
        SetTimer();
        informerDescription.text = description;
        informerHead.text = head;
    }


    private void SetTimer()
    {
        timer = disappearTime;
        textUpdated = true;
    }
}
