using System;
using _Scripts.Messages;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class LetterUIManager : MonoBehaviour
    {
	    public static LetterUIManager Instance;
	    
	    public Sprite[] lettersSprites;
	    public GameObject letterButtonPrefab;
	    public RectTransform letterPanel;
	    [SerializeField] private float distanceBetweenLetters = 5f;
	    [SerializeField] private float xOffSet = 400f;


	    private void Awake()
	    {
		    if (!Instance || Instance != this)
		    {
			    Destroy(Instance);
		    }
		    Instance = this;
		    
	    }

	    public void Start(){
			Debug.Log("Letter UI Manager Started");	
			if (DoNullChecks()) return;
			CreatLetterButtons();

		}

	    private bool DoNullChecks()
	    {
		    if (lettersSprites == null || lettersSprites.Length == 0)
		    {
			    Debug.LogWarning("No letter sprites assigned in the inspector.");
			    return true;
		    }
		    if (!letterButtonPrefab)
		    {
			    Debug.LogWarning("Letter button prefab is not assigned in the inspector.");
			    return true;
		    }
		    if (!letterPanel)
		    {
			    Debug.LogWarning("Letter panel is not assigned in the inspector.");
			    return true;
		    }

		    return false;
	    }

	    private void CreatLetterButtons()
		{
			for (int i = 0; i < lettersSprites.Length; i++)
			{
				var letterButton = Instantiate(letterButtonPrefab, letterPanel);
				letterButton.name = "LetterButton_" + lettersSprites[i].name;
				letterButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
				letterButton.GetComponent<UnityEngine.UI.Image>().sprite = lettersSprites[i];
				RectTransform rt = letterButton.GetComponent<RectTransform>();
				if (i < 13)
				{
					rt.anchoredPosition = new Vector2(i * (rt.sizeDelta.x + distanceBetweenLetters) - xOffSet, 0);
				}
				else
				{
					rt.anchoredPosition = new Vector2((i - 13) * (rt.sizeDelta.x + distanceBetweenLetters) -xOffSet, -60);
				}
				
				Button btn = letterButton.GetComponent<Button>();
				if (btn != null)
				{
					int index = i; // Capture the index for the lambda
					btn.onClick.AddListener(() => OnLetterClicked(btn, (char)('A' + index)));
				}
				
			}
		}

		private void OnLetterClicked(Button clickedButton, char letter)
		{
			Debug.Log("Letter Clicked: " + letter);
			
			clickedButton.interactable = false;
			if (MessageManager.Instance.HandleLetterSelection(letter))
			{
				clickedButton.GetComponent<UnityEngine.UI.Image>().color = Color.green;
			}
			else
			{
				
			clickedButton.GetComponent<UnityEngine.UI.Image>().color = Color.red;
			}
			GameManager.Instance.CheckForWin();
		}

		public void ResetLetters()
		{
			
			var letterButtons = letterPanel.GetComponentsInChildren<Button>();
			Debug.Log(letterButtons.Length);
			foreach (var button in letterButtons)
			{
				button.interactable = true;
				button.GetComponent<UnityEngine.UI.Image>().color = Color.white;
			}
		}
    }
}