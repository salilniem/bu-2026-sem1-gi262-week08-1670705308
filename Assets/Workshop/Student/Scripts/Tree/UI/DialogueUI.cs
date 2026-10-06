using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI npcText;
    public Transform choiceContainer;
    public Button choiceButtonPrefab;
    public GameObject closeButtonDialogue;
    private DialogueSequen InteractNpcSequen;

    private List<Button> activeButtons = new List<Button>();

    public void Setup(DialogueSequen sequen)
    {
        // 1. Set Dialogue Sequen
        this.InteractNpcSequen = sequen;
        Dialogue currentNode = InteractNpcSequen.tree.rootNode;

        ShowDialogue(currentNode);

        // Show UI
        if (dialoguePanel != null) dialoguePanel.SetActive(true);
        gameObject.SetActive(true);
        if (closeButtonDialogue != null) closeButtonDialogue.SetActive(false);
    }

    public void ShowDialogue(Dialogue node)
    {
        if (node == null) return;

        // 2. set ให้เป็น โหนดปัจจุบัน
        InteractNpcSequen.currentNode = node;

        // 3. แสดงข้อความของ NPC
        if (npcText != null)
        {
            npcText.text = node.text;
        }

        // 4. ล้างปุ่มตัวเลือกเก่า
        ClearChoices();

        // 5. สร้างปุ่มตัวเลือกใหม่ตาม nexts
        int index = 0;
        foreach (KeyValuePair<string, Dialogue> choice in node.nexts)
        {
            CreateChoiceButton(choice.Key, index);
            index++;
        }
    }

    private void CreateChoiceButton(string text, int index)
    {
        Button newButton = Instantiate(choiceButtonPrefab, choiceContainer);

        newButton.GetComponentInChildren<TextMeshProUGUI>().text = text;
        newButton.onClick.AddListener(() => OnChoiceSelected(index));

        activeButtons.Add(newButton);
    }

    private void ClearChoices()
    {
        foreach (Button button in activeButtons)
        {
            if (button != null)
            {
                Destroy(button.gameObject);
            }
        }
        activeButtons.Clear();
    }

    private void OnChoiceSelected(int index)
    {
        if (InteractNpcSequen != null)
        {
            InteractNpcSequen.SelectChoice(index);
        }
    }

    public void ShowCloseButtonDialog()
    {
        if (closeButtonDialogue != null)
        {
            closeButtonDialogue.SetActive(true);
        }
    }

    public void HideDialogue()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        ClearChoices();
    }
}