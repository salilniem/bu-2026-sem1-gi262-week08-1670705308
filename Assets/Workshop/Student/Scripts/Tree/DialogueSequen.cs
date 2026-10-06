using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSequen : MonoBehaviour
{
    public DialogueTree tree;
    public Dialogue currentNode;
    public DialogueUI dialogueUI;

    public void Start()
    {
        // 1. call LoadConversations() to set up the dialogue tree
        LoadConversations();

        // 2. set the current node to the root of the tree and print its contents
        if (GetComponent<NPC>() != null)
        {
            dialogueUI = GetComponent<NPC>().dialogueUI;
        }

        currentNode = tree.rootNode;

        if (dialogueUI != null && currentNode != null)
        {
            dialogueUI.Setup(this);
        }
    }

    private void LoadConversations()
    {
        // 3. Create the dialogue nodes
        Dialogue greeting = new Dialogue("Ah, traveler! What brings you to this old place?");
        Dialogue askForQuest = new Dialogue("I have a task for you. There’s a beast in the woods. Can you take care of it?");
        Dialogue questDenied = new Dialogue("You're not ready for this yet. Come back when you're stronger.");
        Dialogue directionsVillage = new Dialogue("Follow the road south, and you’ll reach the village.");
        Dialogue directionsForest = new Dialogue("Head west, into the forest. But beware, it's dangerous.");
        Dialogue goodbye = new Dialogue("Safe travels, adventurer.");
        Dialogue noIdea = new Dialogue("I'm afraid I can't help you with that.");

        // 4. Build the tree, adding custom responses
        // [1]
        greeting.AddNextNode("Can you give me a quest?", askForQuest);

        // [2]
        greeting.AddNextNode("Where is the village?", directionsVillage);

        // [3]
        greeting.AddNextNode("How do I get to the forest?", directionsForest);

        // [4]
        greeting.AddNextNode("Goodbye.", goodbye);

        // [5]
        askForQuest.AddNextNode("I’m ready for anything!", questDenied);

        // [6]
        askForQuest.AddNextNode("Maybe later.", goodbye);

        // 5. Set up the root of the dialogue tree
        tree = new DialogueTree(greeting);
    }

    public void SelectChoice(int index)
    {
        var choiceTextKeys = new List<string>(currentNode.nexts.Keys);

        if (index >= 0 && index < choiceTextKeys.Count)
        {
            string choiceKey = choiceTextKeys[index];

            // 1. เลื่อนไปยัง Dialogue Node ถัดไป
            currentNode = currentNode.nexts[choiceKey];

            // 2. ตรวจสอบว่ามีตัวเลือกถัดไปหรือไม่
            if (currentNode.nexts.Count > 0)
            {
                dialogueUI.ShowDialogue(currentNode);
            }
            else
            {
                // จบบทสนทนา
                dialogueUI.ShowDialogue(currentNode);
                dialogueUI.ShowCloseButtonDialog();
            }
        }
    }
}