using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public GameObject dialogueBox;
    public Animator animator;

    private Queue<string> sentences;

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        //dialogueBox.SetActive(true);
        nameText.text = dialogue.name;
        sentences.Clear();
        Time.timeScale = 0;

        foreach (String sentence in dialogue.sentences)
            sentences.Enqueue(sentence);
        
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        //dialogueText.text = sentence;
    }

    private void EndDialogue()
    {
        Debug.Log("Dialogue ended");
        animator.SetBool("IsOpen", false);
        //dialogueBox.SetActive(false);
        Time.timeScale = 1;
    }

    void Start()
    {
        sentences = new Queue<string>();
        //dialogueBox.SetActive(false);
    }

    IEnumerator TypeSentence(String sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void Update()
    {
           
    }
}
