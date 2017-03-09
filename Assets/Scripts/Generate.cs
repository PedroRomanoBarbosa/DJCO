using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour {
	private enum Types {
		Empty,
		Player,
		Column,
		Bench,
		Door
	}
	private enum Moves {
		Up,
		Left,
		Right
	}
	private enum Difficulties {
		NoBrainer,
		Easy,
		Medium,
		Hard
	}
	private int numPlayerSpaces;
	private List<KeyValuePair<int,int>> columnValidPositions, benchValidPositions, doorValidPositions;
	private int difficulty, numOfObstacles, numCoins;
	private int playerY;
	private Types[,] matrix;
	public int Columns, Lines;
	public bool FirstSection;
	public int PlayerX;
	public GameObject Player;
	public GameObject Column;
	public GameObject Bench;
	public GameObject Door;
	public GameObject Coin;

	void Start () {
		matrix = new Types[Columns, Lines];

		// Decide where to start
		if (FirstSection) {
			PlayerX = Random.Range (0, Lines);
		}

		// Create player path
		playerY = Columns - 1;
		AssignPosition ();
		while (playerY > 0) {
			Moves[] moves = GetPossibleMoves ();
			Moves nextMove = moves [Random.Range (0, moves.Length)];
			switch (nextMove) {
			case Moves.Up:
				playerY -= 1;
				break;
			case Moves.Left:
				PlayerX -= 1;
				break;
			case Moves.Right:
				PlayerX += 1;
				break;
			}
			AssignPosition ();
		}

		setDifficulty ();
		assignObstacles ();
		instantiateObstacles ();

	}

	private Moves[] GetPossibleMoves () {
		List<Moves> moves = new List<Moves> ();

		// It can always go up
		moves.Add(Moves.Up);

		// If the player is in the first position it can only go up
		if (playerY < Columns - 1) {
			// Can it go left?
			if (PlayerX > 0 && matrix[playerY, PlayerX - 1] != Types.Player) {
				if (playerY == Columns - 1) {
					moves.Add(Moves.Left);
				} else if (matrix[playerY + 1, PlayerX - 1] != Types.Player) {
					moves.Add(Moves.Left);
				}
			}

			// Can it go right?
			if (PlayerX < Lines - 1 && matrix[playerY, PlayerX + 1] != Types.Player) {
				if (playerY == Columns - 1) {
					moves.Add(Moves.Right);
				} else if (matrix[playerY + 1, PlayerX + 1] != Types.Player) {
					moves.Add(Moves.Right);
				}
			}
		}
		return moves.ToArray ();
	}

	private void getValidObstaclePositions () {
		columnValidPositions = new List<KeyValuePair<int, int>> ();
		benchValidPositions = new List<KeyValuePair<int, int>> ();
		doorValidPositions = new List<KeyValuePair<int, int>> ();
		for (int y = 0; y < Columns; y++) {
			for (int x = 0; x < Lines; x++) {
				if (matrix [y, x] == Types.Empty) {
					KeyValuePair<int, int> position = new KeyValuePair<int,int> (x, y);
					benchValidPositions.Add (position);
					// Left side columns
					if (x == 0) {
						columnValidPositions.Add (position);
					}
					// Door and right side columns
					if (x == Lines - 1) {
						columnValidPositions.Add (position);
						doorValidPositions.Add (position);
					}
				}
			}
		}
	}

	private void setDifficulty() {
		difficulty = Random.Range ((int)Difficulties.Easy, (int)Difficulties.Hard + 1);
		switch (difficulty) {
		case (int) Difficulties.NoBrainer:
			numOfObstacles = Random.Range (0, 1 + 1);
			break;
		case (int) Difficulties.Easy:
			numOfObstacles = Random.Range (1, 2 + 1);
			numCoins = Random.Range (0, 1 + 1);
			break;
		case (int) Difficulties.Medium:
			numOfObstacles = Random.Range (2, 3 + 1);
			numCoins = 1;
			break;
		case (int) Difficulties.Hard:
			numOfObstacles = Random.Range (3, 4 + 1);
			numCoins = Random.Range (1, 2 + 1);
			break;
		}
	}

	private void assignObstacles() {
		int counter = 0;
		while (counter < numOfObstacles) {
			getValidObstaclePositions ();
			int totalValid = columnValidPositions.Count + benchValidPositions.Count + doorValidPositions.Count;
			int rand = Random.Range(0, totalValid);
			int randPos;
			if (rand >= 0 && rand <= columnValidPositions.Count) {
				randPos = Random.Range (0, columnValidPositions.Count);
				matrix [columnValidPositions [randPos].Value, columnValidPositions [randPos].Key] = Types.Column;
			} else if (rand >= columnValidPositions.Count && rand <= doorValidPositions.Count + columnValidPositions.Count) {
				randPos = Random.Range (0, doorValidPositions.Count);
				matrix [doorValidPositions [randPos].Value, doorValidPositions [randPos].Key] = Types.Door;;
			} else {
				randPos = Random.Range (0, benchValidPositions.Count);
				matrix [benchValidPositions [randPos].Value, benchValidPositions [randPos].Key] = Types.Bench;
			}
			counter++;
		}
	}

	/**
	 * TODO: Put obstacles in the right position
	 */
	private void instantiateObstacles () {
		for (int y = 0; y < Columns; y++) {
			for (int x = 0; x < Lines; x++) {
				Types type = matrix [y, x];
				switch(type){
				case Types.Column:
					Instantiate (Column, new Vector3 (5 * x - 5, 1, 5 * y), Quaternion.identity);
					break;
				case Types.Bench:
					Debug.Log ("bench");
					Instantiate (Bench, new Vector3 (5 * x - 5, 2, 5 * y), Quaternion.identity);
					break;
				case Types.Door:
					Instantiate (Door, new Vector3 (5 * x - 5, 4, 5 * y), Quaternion.identity);
					break;
				}
			}
		}
	}

	private void AssignPosition () {
		matrix [playerY, PlayerX] = Types.Player;
		Instantiate (Player, new Vector3 (5 * PlayerX - 5, 0, 5 * playerY), Quaternion.identity);
		numPlayerSpaces++;
	}

}
