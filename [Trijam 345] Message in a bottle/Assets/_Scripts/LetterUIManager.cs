using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class LetterUIManager : MonoBehaviour
    {
	    public Sprite[] lettersSprites;
	    public GameObject letterButtonPrefab;
	    public RectTransform letterPanel;
	    [SerializeField] private float distanceBetweenLetters = 0.4f;
	    
		public void Start(){
			Debug.Log("Letter UI Manager Started");	
			if (lettersSprites == null || lettersSprites.Length == 0)
			{
				Debug.LogWarning("No letter sprites assigned in the inspector.");
				return;
			}
			if (letterButtonPrefab == null)
			{
				Debug.LogWarning("Letter button prefab is not assigned in the inspector.");
				return;
			}
			if (letterPanel == null)
			{
				Debug.LogWarning("Letter panel is not assigned in the inspector.");
				return;
			}

			CreatLetterButtons();

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
					rt.anchoredPosition = new Vector2(i * (rt.sizeDelta.x + distanceBetweenLetters), 0);
				}
				else
				{
					rt.anchoredPosition = new Vector2((i - 13) * (rt.sizeDelta.x + distanceBetweenLetters), -50);
				}
				
				Button btn = letterButton.GetComponent<Button>();
				if (btn != null)
				{
					int index = i; // Capture the index for the lambda
					btn.onClick.AddListener(() => OnLetterClicked(btn, index));
				}
				
			}
		}

		private void OnLetterClicked(Button clickedButton, int index)
		{
			Debug.Log("Letter Clicked: " + ('A' + index));
			
			clickedButton.interactable = false;
			clickedButton.GetComponent<UnityEngine.UI.Image>().color = Color.gray;
			
		}
    }
}