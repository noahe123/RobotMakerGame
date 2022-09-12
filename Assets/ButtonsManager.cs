using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public static class ButtonExtension
{
	public static void AddEventListener<T>(this Button button, T param, T param1, T param2, T param3, Action<T,T,T,T> OnClick)
	{
		button.onClick.AddListener(delegate () {
			OnClick(param, param1, param2, param3);
		});
	}
}

public class ButtonsManager : MonoBehaviour
{
	public const int UIHeight = 111;

	public enum ButtonCategory
    {
		Input,
		Fun,
		Output,
		Body
    }

	public const float anchorValA = 68f, 
		anchorValB = 193.7f, 
		anchorValC = 319.9f, 
		anchorValD = 165.19f;


	public Sprite roundImageNone,
		roundImageNoneOutline,
		roundImageTop,
		roundImageTopOutline,
		roundImageBottomLeft,
		roundImageBottomLeftOutline,
		roundImageBottomRight,
		roundImageBottomRightOutline,
		roundImageTopLeft,
		roundImageTopLeftOutline,
		roundImageTopRight,
		roundImageTopRightOutline,
		roundImageBottom,
		roundImageBottomOutline;
	public enum Roundedness
    {
		None,//0
		BottomLeft,//1
		BottomRight,//2
		TopLeft,//3
		TopRight,//4
		Top,//5
		Bottom//6
	}
	//struct contains the button properties
	[Serializable]
	public struct MyButton
	{
		public Color MyColor;
		public string Description;
		public Sprite Icon;
		public Roundedness MyRoundedness;
		public bool disableArrow;
	}

	//constants for max amount of buttons
	public const int smallButtonCount = 5, regularButtonCount = 10;

	//create an array of buttons
	MyButton[,,,] allButtons = new MyButton[smallButtonCount, regularButtonCount, regularButtonCount, regularButtonCount];

	//button template
	public GameObject buttonPrefab;

	//vertical layout groups (columns / array dimensions)
	public Transform jTransform, kTransform, mTransform;

	//***** Default Button Values *****
	public Color orange, red, blue, green;
	public Sprite defaultIcon;

	void Start()
	{
		DefineButtonData();

		//TEMPORARY: spawns a basic column, as if the first button is selected. zero is a 'null button' or 'no selection'
		//ButtonClicked(1, 0, 0, 0);
	}

	/// <summary>
	/// Defines the unique properties of each button
	/// </summary>
	void DefineButtonData()
    {
		//button list

		//allButtons[CATEGORY / COLOR (1 -> 4), FIRST COLUMN (9 -> 1), SECOND COLUMN (9 -> 1), THIRD COLUMN (9 -> 1)]

		//***************** RED *********************

		allButtons[1, 9, 0, 0].Description = "Head";
		allButtons[1, 8, 0, 0].Description = "Body";
		allButtons[1, 7, 0, 0].Description = "Eyes";
		allButtons[1, 7, 0, 0].MyRoundedness = Roundedness.TopLeft;
		allButtons[1, 9, 0, 0].MyRoundedness = Roundedness.BottomLeft;


		allButtons[1, 9, 9, 0].Description = "Small Head";
		allButtons[1, 9, 8, 0].Description = "Medium Head";
		allButtons[1, 9, 7, 0].Description = "Big Head";
		allButtons[1, 9, 9, 0].disableArrow = true;
		allButtons[1, 9, 8, 0].disableArrow = true;
		allButtons[1, 9, 7, 0].disableArrow = true;

		allButtons[1, 8, 9, 0].Description = "Small Body";
		allButtons[1, 8, 8, 0].Description = "Medium Body";
		allButtons[1, 8, 7, 0].Description = "Big Body";
		allButtons[1, 8, 6, 0].Description = "Fat Body";
		allButtons[1, 8, 9, 0].disableArrow = true;
		allButtons[1, 8, 8, 0].disableArrow = true;
		allButtons[1, 8, 7, 0].disableArrow = true;
		allButtons[1, 8, 6, 0].disableArrow = true;

		allButtons[1, 7, 9, 0].Description = "Small Eyes";
		allButtons[1, 7, 9, 0].MyRoundedness = Roundedness.BottomRight;
		allButtons[1, 7, 8, 0].Description = "Medium Eyes";
		allButtons[1, 7, 7, 0].Description = "Big Eyes";
		allButtons[1, 7, 9, 0].disableArrow = true;
		allButtons[1, 7, 8, 0].disableArrow = true;
		allButtons[1, 7, 7, 0].disableArrow = true;
		allButtons[1, 7, 6, 0].Description = "Crazy Eyes";
		allButtons[1, 7, 6, 0].MyRoundedness = Roundedness.TopLeft;


		allButtons[1, 7, 6, 7].Description = "Crazy Eyes I";
		allButtons[1, 7, 6, 6].Description = "Crazy Eyes II";
		allButtons[1, 7, 6, 7].disableArrow = true;
		allButtons[1, 7, 6, 6].disableArrow = true;
		allButtons[1, 7, 6, 7].MyRoundedness = Roundedness.BottomRight;
		allButtons[1, 7, 6, 6].MyRoundedness = Roundedness.TopRight;

		//***************** ORANGE *********************

		allButtons[4, 9, 0, 0].Description = "Head";
		allButtons[4, 8, 0, 0].Description = "Body";
		allButtons[4, 7, 0, 0].Description = "Eyes";
		allButtons[4, 7, 0, 0].MyRoundedness = Roundedness.TopLeft;
		allButtons[4, 9, 0, 0].MyRoundedness = Roundedness.BottomLeft;


		allButtons[4, 9, 9, 0].Description = "Small Head";
		allButtons[4, 9, 8, 0].Description = "Medium Head";
		allButtons[4, 9, 7, 0].Description = "Big Head";
		allButtons[4, 9, 9, 0].disableArrow = true;
		allButtons[4, 9, 8, 0].disableArrow = true;
		allButtons[4, 9, 7, 0].disableArrow = true;

		allButtons[4, 8, 9, 0].Description = "Small Body";
		allButtons[4, 8, 8, 0].Description = "Medium Body";
		allButtons[4, 8, 7, 0].Description = "Big Body";
		allButtons[4, 8, 6, 0].Description = "Fat Body";
		allButtons[4, 8, 9, 0].disableArrow = true;
		allButtons[4, 8, 8, 0].disableArrow = true;
		allButtons[4, 8, 7, 0].disableArrow = true;
		allButtons[4, 8, 6, 0].disableArrow = true;

		allButtons[4, 7, 9, 0].Description = "Small Eyes";
		allButtons[4, 7, 9, 0].MyRoundedness = Roundedness.BottomRight;
		allButtons[4, 7, 8, 0].Description = "Medium Eyes";
		allButtons[4, 7, 7, 0].Description = "Big Eyes";
		allButtons[4, 7, 9, 0].disableArrow = true;
		allButtons[4, 7, 8, 0].disableArrow = true;
		allButtons[4, 7, 7, 0].disableArrow = true;
		allButtons[4, 7, 6, 0].Description = "Crazy Eyes";
		allButtons[4, 7, 6, 0].MyRoundedness = Roundedness.TopLeft;


		allButtons[4, 7, 6, 7].Description = "Crazy Eyes I";
		allButtons[4, 7, 6, 6].Description = "Crazy Eyes II";
		allButtons[4, 7, 6, 7].disableArrow = true;
		allButtons[4, 7, 6, 6].disableArrow = true;
		allButtons[4, 7, 6, 7].MyRoundedness = Roundedness.BottomRight;
		allButtons[4, 7, 6, 6].MyRoundedness = Roundedness.TopRight;

	}

	void SetRoundedness(GameObject g, Roundedness desiredRoundedness)
    {
		switch(desiredRoundedness)
        {
			case Roundedness.None:
				g.GetComponent<Image>().sprite = roundImageNone;
				g.transform.GetChild(1).GetComponent<Image>().sprite = roundImageNoneOutline;
				break;

			case Roundedness.BottomLeft:
				g.GetComponent<Image>().sprite = roundImageBottomLeft;
				g.transform.GetChild(1).GetComponent<Image>().sprite = roundImageBottomLeftOutline;
				break;

			case Roundedness.BottomRight:
				g.GetComponent<Image>().sprite = roundImageBottomRight;
				g.transform.GetChild(1).GetComponent<Image>().sprite = roundImageBottomRightOutline;
				break;

			case Roundedness.Top:
				g.GetComponent<Image>().sprite = roundImageTop;
				g.transform.GetChild(1).GetComponent<Image>().sprite = roundImageTopOutline;
				break;

			case Roundedness.Bottom:
				g.GetComponent<Image>().sprite = roundImageBottom;
				g.transform.GetChild(1).GetComponent<Image>().sprite = roundImageBottomOutline;
				break;

			case Roundedness.TopLeft:
				g.GetComponent<Image>().sprite = roundImageTopLeft;
				g.transform.GetChild(1).GetComponent<Image>().sprite = roundImageTopLeftOutline;
				break;

			case Roundedness.TopRight:
				g.GetComponent<Image>().sprite = roundImageTopRight;
				g.transform.GetChild(1).GetComponent<Image>().sprite = roundImageTopRightOutline;
				break;

		}
    }

	public void CategoryButtonClick(int category)
    {
		Debug.Log((ButtonCategory)category);
		switch (category)
        {
			case 1:
				SetButtonXPos(anchorValA);
				break;

			case 2:
				SetButtonXPos(anchorValB);
				break;

			case 3:
				SetButtonXPos(anchorValC);
				break;

			case 4:
				SetButtonXPos(anchorValD);
				break;
		}
		ButtonClicked(category, 0, 0, 0);
    }

	public void SetButtonXPos(float xPos)
    {
		transform.position = new Vector3(xPos, UIHeight, 0);
	}

	/// <summary>
	/// Called when a MyButton gameobject is clicked.
	/// </summary>
	/// <param name="itemIndex">Index value to pass.</param>
	/// <returns>Returns void.</returns>
	public void ButtonClicked(int pi, int pj, int pk, int pm)
	{
		MyButton mB = allButtons[pi, pj, pk, pm];
		Debug.Log("------------item " + pi + " " + pj + " " + pk + " " + pm + " clicked---------------");
		Debug.Log("desc " + mB.Description);

		if (mB.disableArrow)
        {
			//collapse UI and do something if button is final
			SpawnButtonColumn(0, 0, 0, 0);

			return;
        }
		SpawnButtonColumn(pi, pj, pk, pm);
	}

	/// <summary>
	/// Spawns an entire button column
	/// </summary>
	public void SpawnButtonColumn(int i, int j, int k, int m)
    {

		if (i == 0)
        {
			DestroyAllChildButtons();

			return;
        }
		else if (j == 0)
		{
			DestroyAllChildButtons();

			//first column

			for (int x = 1; x < regularButtonCount; x++)
			{
				//myButton of this index
				MyButton myButtonIndex = allButtons[i, x, 0, 0];

				//check if button exists at index
				if (myButtonIndex.Description == null)
				{
					SpawnPlaceholder(jTransform);
					continue;
				}

				SpawnButton(i, x, 0, 0, myButtonIndex, jTransform);
			}
		}
		else if (k == 0)
        {
			DestroyChildButtons(kTransform);
			DestroyChildButtons(mTransform);

			//second column

			for (int x = 1; x < regularButtonCount; x++)
			{
				//myButton of this index
				MyButton myButtonIndex = allButtons[i, j, x, 0];

				//check if button exists at index
				if (myButtonIndex.Description == null)
				{
					SpawnPlaceholder(kTransform);
					continue;
				}

				SpawnButton(i, j, x, 0, myButtonIndex, kTransform);
			}
		}
		else if (m == 0)
        {
			DestroyChildButtons(mTransform);

			//third column

			for (int x = 1; x < regularButtonCount; x++)
			{
				//myButton of this index
				MyButton myButtonIndex = allButtons[i, j, k, x];

				//check if button exists at index
				if (myButtonIndex.Description == null)
				{
					SpawnPlaceholder(mTransform);
					continue;
				}

				SpawnButton(i, j, k, x, myButtonIndex, mTransform);
			}
		}
	}

	/// <summary>
	/// Spawns a single button, and updates its unique properties according to MyButton data
	/// </summary>
	public void SpawnButton(int i, int j, int k, int m, MyButton mB, Transform t)
    {
		GameObject b;

		//instantiates a template button gameobject
		b = Instantiate(buttonPrefab, t);

		//adds button functionality: this button will now call the ButtonClicked() function if clicked.
		b.GetComponent<Button>().AddEventListener(i, j, k, m, ButtonClicked);

		//make button default
		MakeButtonDefault(b);

		//updates the button text
		SetButtonText(b, mB.Description);

		//default color
		switch (i)
        {
			case 1:
				SetButtonColor(b, red);
				break;

			case 2:
				SetButtonColor(b, green);
				break;

			case 3:
				SetButtonColor(b, blue);
				break;

			case 4:
				SetButtonColor(b, orange);
				break;
		}

		//disables arrow if prompted
		if (mB.disableArrow == true)
        {
			ArrowActive(b, false);
        }

		SetRoundedness(b, mB.MyRoundedness);
	}

	public void SpawnPlaceholder(Transform t)
    {
		GameObject b = new GameObject("Placeholder");
		b.AddComponent<RectTransform>();
		b.transform.SetParent(t, false);
	}

	/// <summary>
	/// Destroys all child buttons of every column / array dimension
	/// </summary>
	public void DestroyAllChildButtons()
	{
		//clear existing buttons
		DestroyChildButtons(jTransform);
		DestroyChildButtons(kTransform);
		DestroyChildButtons(mTransform);
	}

	/// <summary>
	/// destroys the child buttons of a single column / array dimension
	/// </summary>
	/// <param name="parent"></param>
	public void DestroyChildButtons(Transform parent)
	{
		foreach (Transform child in parent)
		{
			Destroy(child.gameObject);
		}
	}

	/// <summary>
	/// sets a button to its default properties
	/// </summary>
	/// <param name="g"></param>
	public void MakeButtonDefault(GameObject g)
    {
		//default icon
		SetButtonIcon(g, defaultIcon);
		//set arrowEnabled to default
		ArrowActive(g, true);
	}

	public void ArrowActive(GameObject g, bool arrowState)
    {
		g.transform.GetChild(4).gameObject.SetActive(arrowState);
	}

	/// <summary>
	/// Changes the color of a MyButton gameobject.
	/// </summary>
	/// <param name="myButtonParent"></param>
	/// <param name="desiredColor"></param>
	public void SetButtonColor(GameObject g, Color myColor)
    {
		//description text color
		g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = myColor;
		//outline color
		g.transform.GetChild(1).GetComponent<Image>().color = myColor;
		//icon outline color
		g.transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<Image>().color = myColor;
		//arrow symbol color
		g.transform.GetChild(4).GetComponent<Image>().color = myColor;
	}

	/// <summary>
	/// Updates the text of a given button GameObject
	/// </summary>
	/// <param name="g">button gameobject</param>
	/// <param name="myText">desired text</param>
	public void SetButtonText(GameObject g, string myText)
	{
		g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = myText;
	}

	/// <summary>
	/// Updates the icon of a given button GameObject
	/// </summary>
	/// <param name="g"></param>
	/// <param name="myIcon"></param>
	public void SetButtonIcon(GameObject g, Sprite myIcon)
	{
		g.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>().sprite = myIcon;
	}
}