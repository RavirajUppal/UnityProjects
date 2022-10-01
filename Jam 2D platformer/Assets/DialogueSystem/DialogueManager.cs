using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> Sentences;

    public List<Text> messageText;

    private float typingSpeed;
    private int messageCount = 0;
    private bool isTyping, isSkipped = false;
    string sentence;

    private void Start()
    {
        Sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        // Sentences.Clear();
        if (dialogue == null)
        {
            Debug.Log("OUT OF SENETENCES");
            return;
        }

        foreach (var sentence in dialogue.sentences)
        {
            Sentences.Enqueue(sentence);
        }

        DisplaySentences();
    }

    public void DisplaySentences()
    {
        if (Sentences.Count == 0)
        {
            Debug.LogError("NO DIALOGUE TO SHOW");
            return;
        }
       
        if (isTyping)
        {
            isSkipped = true;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence, messageCount - 1));
        }
       
        sentence = Sentences.Dequeue();
        if (messageCount >= messageText.Count)
        {
            SceneManager.LoadScene("SampleScene");
            return;
        }
        StartCoroutine(TypeSentence(sentence, messageCount));
        messageCount++;

    }

    IEnumerator TypeSentence(string sentences, int id)
    {
        if (!isSkipped)
        {
            isTyping = true;
            messageText[id].text = " ";
            foreach (var sentence in sentences)
            {
                messageText[id].text += sentence;
                yield return new WaitForSeconds(.1f);
            }
            isTyping = false;
        }
        else
        {
            isTyping = true;
            messageText[id].text = sentences;
            isTyping = false;
            isSkipped = false;
        }
    }
}
