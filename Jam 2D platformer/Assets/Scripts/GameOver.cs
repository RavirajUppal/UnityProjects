using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
	UnityEngine.UI.Button replay, exit;

	// Start is called before the first frame update
	void Start()
	{
		replay = transform.GetChild(2).GetComponent<UnityEngine.UI.Button>();
		replay.onClick.AddListener(()=> 
		{
			SceneManager.LoadScene("SampleScene");
		});

		exit = transform.GetChild(3).GetComponent<UnityEngine.UI.Button>();
		exit.onClick.AddListener(() =>
		{
			Application.Quit();
		});
	}

	void Replay()
	{

	}
}
