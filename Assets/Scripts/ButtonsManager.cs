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
		anchorValC = 318.3f, 
		anchorValD = 165.19f;

		Sprite roundImageNone,
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
		roundImageBottomOutline,
		roundImage,
		roundImageOutline;

	public enum Roundedness
    {
		None,//0
		BottomLeft,//1
		BottomRight,//2
		TopLeft,//3
		TopRight,//4
		Top,//5
		Bottom,//6
		All
	}
	//struct contains the button properties
	[Serializable]
	public struct MyButton
	{
		public Color MyColor;
		public string Description;
		public Sprite Icon;
		public Roundedness MyRoundedness;
		public Roundedness RoundedSelect;
		public Roundedness RoundedDefault;
		public bool finalButton;
		public bool selected;

		/*
		public MyButton()
        {
			MyColor = Color.grey;
			Description = null;
			Icon = null;
			MyRoundedness = Roundedness.None;
			disableArrow = false;
		}*/
	}

	//constants for max amount of buttons
	public const int smallButtonCount = 5, regularButtonCount = 10;

	//create an array of buttons
	MyButton[,,,] allButtons = new MyButton[smallButtonCount, regularButtonCount, regularButtonCount, regularButtonCount];

	//button template
	public GameObject buttonPrefab;

	//vertical layout groups (columns / array dimensions)
	Transform jTransform, kTransform, mTransform;

	//***** Default Button Values *****
	public Color orange, red, blue, green, highlighterYellow;
	public Sprite defaultIcon;

	SoundManager mySoundManager;

	//************************************************************ TEMPORARY VARIABLES **************************************************************

	//int testElement;

	void Start()
	{
		AddEvents();

		DefineButtonResources();

		DefineAllButtonData();

		//TEMPORARY: spawns a basic column, as if the first button is selected. zero is a 'null button' or 'no selection'
		//ButtonClicked(1, 0, 0, 0);
	}

    private void Update()
    {
		//Used for testing purposes.
		//SpawnTestElement();

	}

	/*
	/// <summary>
	/// Function used for testing purposes.
	/// </summary>
	private void SpawnTestElement()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			testElement++;
			DefineButtonData("Body-Spawn Object-Basic 3D Shape-TEST ELEMENT #" + testElement);
		}
	}*/

    public void AddEvents()
    {
		GameEvents.current.onSampleEvent += TriggerOnSampleEvent;
    }

	public void DefineButtonResources()
    {
		mySoundManager = FindObjectOfType<SoundManager>();

		string buttonsPath = "Images/UI/Buttons/";
		roundImageNone = Resources.Load<Sprite>(buttonsPath + "Button_Flat");
		roundImageNoneOutline = Resources.Load<Sprite>(buttonsPath + "Button_Flat_Outline");
		roundImageBottom = Resources.Load<Sprite>(buttonsPath + "Button_Round_Bottom");
		roundImageBottomOutline = Resources.Load<Sprite>(buttonsPath + "Button_Round_Bottom_Outline");
		roundImageTop = Resources.Load<Sprite>(buttonsPath + "Button_Round_Top");
		roundImageTopOutline = Resources.Load<Sprite>(buttonsPath + "Button_Round_Top_Outline");
		roundImageTopLeft = Resources.Load<Sprite>(buttonsPath + "Button_Round_Top_Left");
		roundImageTopLeftOutline = Resources.Load<Sprite>(buttonsPath + "Button_Round_Top_Left_Outline");
		roundImageTopRight = Resources.Load<Sprite>(buttonsPath + "Button_Round_Top_Right");
		roundImageTopRightOutline = Resources.Load<Sprite>(buttonsPath + "Button_Round_Top_Right_Outline");
		roundImageBottomLeft = Resources.Load<Sprite>(buttonsPath + "Button_Round_Bottom_Left");
		roundImageBottomLeftOutline = Resources.Load<Sprite>(buttonsPath + "Button_Round_Bottom_Left_Outline");
		roundImageBottomRight = Resources.Load<Sprite>(buttonsPath + "Button_Round_Bottom_Right");
		roundImageBottomRightOutline = Resources.Load<Sprite>(buttonsPath + "Button_Round_Bottom_Right_Outline");
		roundImage = Resources.Load<Sprite>(buttonsPath + "Button_Round");
		roundImageOutline = Resources.Load<Sprite>(buttonsPath + "Button_Round_Outline");


		jTransform = transform.GetChild(0);
		kTransform = transform.GetChild(1);
		mTransform = transform.GetChild(2);
	}

	public void DefineCategoryData()
    {
		allButtons[1, 0, 0, 0].Description = "Input";
		allButtons[2, 0, 0, 0].Description = "Fun";
		allButtons[3, 0, 0, 0].Description = "Output";
		allButtons[4, 0, 0, 0].Description = "Objects";
	}

	/// <summary>
	/// Defines the unique properties of each button
	/// </summary>
	void DefineAllButtonData()
    {
		//button list

		DefineCategoryData();

		//Test New Function

		DefineButtonData("Input-Spawn Object-Basic 3D Shape-Cube");
		DefineButtonData("Input-Spawn Object-Basic 3D Shape-Sphere");

		DefineButtonData("Fun-Spawn Object-Basic 3D Shape-Cube");
		DefineButtonData("Fun-Spawn Object-Basic 3D Shape-Sphere");

		DefineButtonData("Output-Spawn Object-Basic 3D Shape-Cube");
		DefineButtonData("Output-Spawn Object-Basic 3D Shape-Sphere");

		//******************* OBJECTS *******************

		DefineButtonData("Objects-Templates--");
		DefineButtonData("Objects-Templates-Farm Animals-");
		DefineButtonData("Objects-Templates-Farm Animals-Cow");
		DefineButtonData("Objects-Templates-Farm Animals-Cow");
		DefineButtonData("Objects-Templates-Farm Animals-Cow");
		DefineButtonData("Objects-Templates-Farm Animals-Goose");
		DefineButtonData("Objects-Templates-Farm Animals-Duck");

		DefineButtonData("Objects-Templates-People-");
		DefineButtonData("Objects-Templates-People-Astronaut");
		DefineButtonData("Objects-Templates-People-Businessperson");
		DefineButtonData("Objects-Templates-People-Ballerina");

		DefineButtonData("Objects-Templates-Monsters-Ghoul");
		DefineButtonData("Objects-Templates-Monsters-Zombie");

		DefineButtonData("Objects-Simple Objects--");
		DefineButtonData("Objects-Simple Objects-Cube-");
		DefineButtonData("Objects-Simple Objects-Cube-Cuboid 1");
		DefineButtonData("Objects-Simple Objects-Cube-Cuboid 2");
		DefineButtonData("Objects-Simple Objects-Cube-Cuboid 3");
		DefineButtonData("Objects-Simple Objects-Cube-Cuboid 4");
		DefineButtonData("Objects-Simple Objects-Cube-Cuboid 5");
		DefineButtonData("Objects-Simple Objects-Cube-Cuboid 6");


		/*
		DefineButtonData("Objects-Simple Objects-Sphere-Spheroid 1");
		DefineButtonData("Objects-Simple Objects-Sphere-Spheroid 2");
		DefineButtonData("Objects-Simple Objects-Sphere-Spheroid 3");
		DefineButtonData("Objects-Simple Objects-Sphere-Spheroid 4");
		DefineButtonData("Objects-Simple Objects-Sphere-Spheroid 5");*/


	}

	/// <summary>
	///  Loops through the allButtons array and inserts button data at the desired directory.
	/// </summary>
	/// <param name="a">Category</param>
	/// <param name="b">Description of a new button in the 1st column.</param>
	/// <param name="c">Description of a new button in the 2nd column.</param>
	/// <param name="d">Description of a new button in the 3rd column.</param>
	public void DefineButtonData(string myDirectory)
    {
		//separate directory string into individual strings
		string[] s = myDirectory.Split("-");
		string a = s[0];
		string b = s[1];
		string c = s[2];
		string d = s[3];

		//storing the button's array location in a Vector4
		Vector4 buttonLocation = new Vector4(smallButtonCount, regularButtonCount, regularButtonCount, regularButtonCount);

		//flag variable to check if button is 'final'
		bool finalButtonFlag = true;

		//return and give an error if no description was provided
		if (a == "" || b == "")
        {
			Debug.Log("DefineButtonData() error: no button description provided!!!");
			return;
        }

		//initialize a MyButton variable as empty, for now
		MyButton mB = allButtons[0, 0, 0, 0];

		//i loops thru the 1st column of the array
		for (int i = 1; i < smallButtonCount; i++)
		{
			//have we found a previous button with a matching description?
			if (allButtons[i, 0, 0, 0].Description == a && c == "")
			{
				//since we're adding a new button, the button to the left is no longer the final button in the sequence.
				allButtons[i, 0, 0, 0].finalButton = false;

				//look for an empty slot in the column
				for (int w = regularButtonCount - 1; w >= 0; w--)
				{
					//is the slot empty?
					if (allButtons[i, w, 0, 0].Description == null || allButtons[i, w, 0, 0].Description == "")
					{
						//define the button data at this location!
						mB = allButtons[i, w, 0, 0];
						mB.Description = b;
						buttonLocation = new Vector4(i, w, 0, 0);

						/*
						//make the button at the top of the column rounded
						mB.RoundedDefault = Roundedness.None;
						mB.RoundedSelect = Roundedness.None;*/

						break;
					}
					/*
					else if (w == regularButtonCount - 1) //make the button at the bottom of the column rounded
					{
						allButtons[i, w, 0, 0].RoundedDefault = Roundedness.None;
						allButtons[i, w, 0, 0].RoundedSelect = Roundedness.None;

					}
					else if (w != regularButtonCount - 1) //make the buttons in between the top and bottom of the column flat
					{
						allButtons[i, w, 0, 0].RoundedDefault = Roundedness.None;
						allButtons[i, w, 0, 0].RoundedSelect = Roundedness.None;
					}*/
				}
			}


			//j loops thru the 2nd column of the array
			for (int j = 1; j < regularButtonCount; j++)
			{
				//have we found previous buttons with matching descriptions?
				if (allButtons[i, 0, 0, 0].Description == a && allButtons[i, j, 0, 0].Description == b && c != "" && d == "")
				{
					//since we're adding a new button, the button to the left is no longer the final button in the sequence.
					allButtons[i, j, 0, 0].finalButton = false;

					//look for an empty slot in the column
					for (int w = j; w >= 0; w--)
					{
						//is the slot empty?
						if (allButtons[i, j, w, 0].Description == null || allButtons[i, j, w, 0].Description == "")
						{
							//define the button data at this location!
							mB = allButtons[i, j, w, 0];
							mB.Description = c;
							buttonLocation = new Vector4(i, j, w, 0);

							/*
							//make the button at the top of the column rounded
							mB.RoundedDefault = Roundedness.None;
							mB.RoundedSelect = Roundedness.None;*/

							break;
						}
						/*
						else if (w == j) //make the button at the bottom of the column rounded
						{
							allButtons[i, j, w, 0].RoundedDefault = Roundedness.None;
							allButtons[i, j, w, 0].RoundedSelect = Roundedness.None;
						}
						else if (w != j) //make the buttons in between the top and bottom of the column flat
						{
							allButtons[i, j, w, 0].RoundedDefault = Roundedness.None;
							allButtons[i, j, w, 0].RoundedSelect = Roundedness.None;
						}*/
					}
				}


				//k loops thru the 3rd column of the array
				for (int k = 1; k < regularButtonCount; k++)
				{
					//have we found previous buttons with matching descriptions?
					if (allButtons[i, 0, 0, 0].Description == a && allButtons[i, j, 0, 0].Description == b && allButtons[i, j, k, 0].Description == c && d != "")
					{

						//since we're adding a new button, the button to the left is no longer the final button in the sequence.
						allButtons[i, j, k, 0].finalButton = false;

						//look for an empty slot in the column
						for (int w = k; w >= 0; w--)
						{
							//is the slot empty?
							if (allButtons[i, j, k, w].Description == null || allButtons[i, j, k, w].Description == "")
							{
								//define the button data at this location!
								mB = allButtons[i, j, k, w];
								mB.Description = d;
								buttonLocation = new Vector4(i, j, k, w);

								/*
								//make the button at the top of the column rounded
								mB.RoundedDefault = Roundedness.None;
								mB.RoundedSelect = Roundedness.None;*/

								break;
							}
							/*
							else if (w == k) //make the button at the bottom of the column rounded
							{
								allButtons[i, j, k, w].RoundedDefault = Roundedness.None;
								allButtons[i, j, k, w].RoundedSelect = Roundedness.None;
							}
							else if (w != k) //make the buttons in between the top and bottom of the column flat
							{
								allButtons[i, j, k, w].RoundedDefault = Roundedness.None;
								allButtons[i, j, k, w].RoundedSelect = Roundedness.None;

							}*/
						}

					}

					//m loops thru the 4th column of the array to determine if the path has more buttons ahead
					for (int m = 1; m < regularButtonCount; m++)
					{
						//does our location have an empty third column? then there could be more buttons ahead!
						if ((int)buttonLocation.z == 0)
						{
							//is there a button at this location?
							if (allButtons[(int)buttonLocation.x, (int)buttonLocation.y, k, m].Description != null)
							{
								//we found a button!
								finalButtonFlag = false;
							}
						}
						//does our location have an occupied third column, and an empty fourth column? then there could be more buttons ahead!
						else if ((int)buttonLocation.z != 0 && (int)buttonLocation.w == 0)
                        {
							//is there a button at this location?
							if (allButtons[(int)buttonLocation.x, (int)buttonLocation.y, (int)buttonLocation.z, m].Description != null)
							{
								//we found a button!
								finalButtonFlag = false;
							}
						}
					}
				}
			}
		}

		//final button flag is true, so the index is a final button.
		if (finalButtonFlag)
        {
			mB.finalButton = true;
		}

		mB.MyRoundedness = mB.RoundedDefault;

		//set the buttonLocation index of allButtons to mB
		if (mB.Description != null && mB.Description != "")
		{
			allButtons[(int)buttonLocation.x, (int)buttonLocation.y, (int)buttonLocation.z, (int)buttonLocation.w] = mB;
		}/*
        else
        {
			//add button data for other columns if they don't already exist
			if (c != "")
            {
				Debug.Log(a + "-" + b + "-" + c + "-" + d);
				DefineButtonData(a + "-" + b + "-" + "" + "-" + "");
				DefineButtonData(a + "-" + b + "-" + c + "-" + "");
				if (d != "")
				{
					DefineButtonData(a + "-" + b + "-" + c + "-" + d);
				}
			}
		}*/

		return;
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

			case Roundedness.All:
				g.GetComponent<Image>().sprite = roundImage;
				g.transform.GetChild(1).GetComponent<Image>().sprite = roundImageOutline;
				break;

		}
    }

	public void TriggerOnSampleEvent(Vector4 id)
    {
		if (allButtons[(int)id.x, 0, 0, 0].Description == "Body")
		{
			if (allButtons[(int)id.x, (int)id.y, 0, 0].Description == "Spawn Object")
			{
				if (allButtons[(int)id.x, (int)id.y, (int)id.z, 0].Description == "Basic 3D Shape")
				{
					if (allButtons[(int)id.x, (int)id.y, (int)id.z, (int)id.w].Description == "Cube")
					{
						Debug.Log("Spawn Cube!");
					}
				}
			}
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
	public void ButtonClicked(int pi, int pj, int pk, int pm)
	{
		MyButton mB = allButtons[pi, pj, pk, pm];
		Debug.Log("------------item " + pi + " " + pj + " " + pk + " " + pm + " clicked---------------");
		Debug.Log("desc " + mB.Description);

		//mark button as selected
		allButtons[pi, pj, pk, pm].selected = true;

		allButtons[pi, pj, pk, pm].MyRoundedness = mB.RoundedSelect;

		if (mB.finalButton)
        {
			//collapse UI
			SpawnButtonColumn(0, 0, 0, 0);

			FindObjectOfType<SoundManager>().Play("Spawn");

			Vector4 id = new Vector4(pi, pj, pk, pm);

			//button functionality
			GameEvents.current.SampleEvent(id);

			return;
        }
		SpawnButtonColumn(pi, pj, pk, pm);


		if (pj == 0)
		{
			mySoundManager.Play("ButtonClick");
		}
		else if (pk == 0)
		{
			mySoundManager.Play("Select 1");
		}
		else if(pm == 0)
		{
			mySoundManager.Play("Select 2");
		}
		else
		{
			mySoundManager.Play("Select 3");
		}
	}

	/// <summary>
	/// Refreshes the button columns up to the desired column. Effectively, spawning a new button column.
	/// </summary>
	public void SpawnButtonColumn(int i, int j, int k, int m)
    {
		if (i == 0)
        {

			DestroyAllChildButtons();
			return;
        }

		if (i != 0)
		{
			DestroyAllChildButtons();

			//first column

			for (int x = 1; x < regularButtonCount; x++)
			{
				//clear selected buttons
				if (k == 0 && x != j)
				{
					allButtons[i, x, 0, 0].selected = false;

					allButtons[i, x, 0, 0].MyRoundedness = allButtons[i, x, 0, 0].RoundedDefault;
				}

				//myButton of this index.
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
		 if (j != 0)
        {
			DestroyChildButtons(kTransform);
			DestroyChildButtons(mTransform);

			//second column

			for (int x = 1; x < regularButtonCount; x++)
			{
				//reset OTHER 'selected' values of buttons of the same column
				if (m == 0 && x != k)
				{
					allButtons[i, j, x, 0].selected = false;

					allButtons[i, j, x, 0].MyRoundedness = allButtons[i, x, 0, 0].RoundedDefault;

				}

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
		 if (k != 0)
        {
			DestroyChildButtons(mTransform);

			//third column

			for (int x = 1; x < regularButtonCount; x++)
			{
				/*
				//reset OTHER 'selected' values of buttons of the same column
				if (m == 0)
				{
					allButtons[i, j, k, x].selected = false;

					allButtons[i, j, k, x].MyRoundedness = allButtons[i, x, 0, 0].RoundedDefault;

				}
				*/
				allButtons[i, j, k, x].selected = false;

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

		SetButtonSelectedState(b, mB.selected);

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
		if (mB.finalButton == true)
        {
			ArrowActive(b, false);

			//enable icon, since we're disabling arrow, and this is the last button!
			IconActive(b, true);
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

	public void IconActive(GameObject g, bool iconState)
	{
		g.transform.GetChild(3).GetChild(0).gameObject.SetActive(iconState);
	}

	public void SetButtonSelectedState(GameObject g, bool selectedState)
	{
		if (selectedState)
		{
			g.GetComponent<Image>().color = highlighterYellow;
		}
        else
        {
			g.GetComponent<Image>().color = Color.white;
		}
	}


	/// <summary>
	/// Changes the color of a MyButton gameobject.
	/// </summary>
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
	/// <param name="g">Button GameObject.</param>
	/// <param name="myText">Desired text.</param>
	public void SetButtonText(GameObject g, string myText)
	{
		g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = myText;
	}

	/// <summary>
	/// Updates the icon of a given button GameObject
	/// </summary>
	public void SetButtonIcon(GameObject g, Sprite myIcon)
	{
		g.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>().sprite = myIcon;
	}
}