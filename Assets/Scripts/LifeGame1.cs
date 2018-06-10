using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGame1 : MonoBehaviour {

	// Use this for initialization
	public Image image;
	public int MapSize;
	private int [ , ] LifeMap;
	private ArrayList ImageMap = new ArrayList();
	private Vector2 [] RoundCell = new Vector2[8];
    private int GridMax = 20;
	private int distance = 0;
    private int WorldLife;
    private int cellindtent = 2;
	void Start () {
		//定义cell四周的向量
		RoundCell[0] = new Vector2(-1,1);
		RoundCell[1] = new Vector2(0,1);
		RoundCell[2] = new Vector2(1,1);
		RoundCell[3] = new Vector2(-1,0);
		RoundCell[4] = new Vector2(1,0);
		RoundCell[5] = new Vector2(-1,-1);
		RoundCell[6] = new Vector2(0,-1);
		RoundCell[7] = new Vector2(1,-1);

		LifeMap = new int[MapSize,MapSize];
        WorldLife = MapSize*MapSize*GridMax;
		//初始化每个格子的生命
		int row = LifeMap.GetLength(0);
		int col = LifeMap.GetLength(1);
        int leaveLife = WorldLife;
		for(int i = 0 ; i <row;i++)
		{
			for(int j = 0;j<col;j++)
			{
                if (leaveLife> 0)
                {   
                    int max = leaveLife > GridMax ? GridMax : leaveLife;
                    LifeMap[i,j] = Random.Range(0,max);
                    leaveLife-=LifeMap[i,j];
                }
                else
                {
                    LifeMap[i,j] = 0;
                }
				Debug.Log(i+"行"+j+"列得到的随机数："+LifeMap[i,j]);  
				createImage(i,j);
				setImageColor(i,j,LifeMap[i,j]);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		updateLifeMap();
	}
	
	bool compareList(int [ , ] preLifeMap,int [ , ] curLifeMap)
	{
		int row = LifeMap.GetLength(0);
		int col = LifeMap.GetLength(1);
		for(int i = 0 ; i <row;i++)
		{
			for(int j = 0;j<col;j++)
			{
				if(preLifeMap[i,j]!=curLifeMap[i,j])
				{
					return false;
				}
			}
		}

		return true;
	}
	void updateLifeMap()
	{
		int row = LifeMap.GetLength(0);
		int col = LifeMap.GetLength(1);

		for(int i = 0 ; i <row;i++)
		{
			for(int j = 0;j<col;j++)
			{
				CheckRoundCell(i,j);
                setImageColor(i,j,LifeMap[i,j]);
			}
		}
	}

	void CheckRoundCell(int cellcow,int cellRol){
		int row = LifeMap.GetLength(0);
		int col = LifeMap.GetLength(1);
		Vector2 cell = new Vector2(cellcow,cellRol);
		for(int i = 0 ;i< RoundCell.Length;i++)
		{
			Vector2 checkVector = RoundCell[i];
			Vector2 lifeCell = new Vector2(cellcow,cellRol)+checkVector;
			if (lifeCell.x>=0 && lifeCell.y>= 0 &&lifeCell.x <row && lifeCell.y<col)
			{
                int roundCell = LifeMap[(int)lifeCell.x,(int)lifeCell.y];
                int mycell = LifeMap[cellcow,cellRol];

                int indent = Mathf.Abs(mycell - roundCell);
                int cellLife = indent/2;
                if(mycell - roundCell>=cellindtent)
                {
                    LifeMap[(int)lifeCell.x,(int)lifeCell.y]+=cellLife;
                    LifeMap[cellcow,cellRol]-=cellLife;
                }
                else if (roundCell - mycell >=cellindtent)
                {
                    LifeMap[(int)lifeCell.x,(int)lifeCell.y]-=cellLife;
                    LifeMap[cellcow,cellRol]+=cellLife;
                }
			}
		}
	}

	void createImage(int i, int j)
	{
		int row = LifeMap.GetLength(0);
		int col = LifeMap.GetLength(1);

		int size = Screen.height < Screen.width ? Screen.height : Screen.width;
		int num = row < col ? row : col ;
		 
		float scale = size/num;

		if(ImageMap.Count < row*col)
		{
			Image tempImage = (Image) Instantiate (image);
			tempImage.GetComponent<Transform>().SetParent(GameObject.Find("Canvas").GetComponent<Transform>(),true);
			tempImage.GetComponent<RectTransform>().sizeDelta = new Vector2(scale,scale); 
			tempImage.GetComponent<Transform>().localPosition = new Vector3((i-1-row/2)*(scale+distance),(j-1-col/2)*(scale+distance),0);
			
			ImageMap.Insert(ImageMap.Count,tempImage);
			
		}
	}

	void setImageColor(int i, int j,int color)
	{
		int row = LifeMap.GetLength(0);
		Image tempImage = (Image)ImageMap[i*row+j];
        Color newColor = Color.Lerp(Color.white, Color.black, (float)color/GridMax);
		if (tempImage!=null)
		{
			tempImage.color = newColor;
		}
	}
}

