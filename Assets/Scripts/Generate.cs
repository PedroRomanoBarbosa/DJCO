using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate {
	public enum Types {
		Empty,
		Player,
		Column,
		Bench,
		Door,
		Coin,
		BenchCoin,
		Beer,
		BenchBeer
	}
	private enum Moves {
		Up,
		Left,
		Right
	}
	public enum Difficulties {
		NoBrainer,
		Easy,
		Medium,
		Hard
	}
	private int numPlayerSpaces;
	private List<KeyValuePair<int,int>> columnValidPositions, benchValidPositions, doorValidPositions, coinValidPositions, beerValidPositions;
	private int difficulty, numObstacles, numCoins, numBeers;
	private int playerY;
	private Types[,] matrix;

	private int columns, lines;
	public bool FirstSection;
	public int PlayerX;

	public Generate (int c, int l) {
		columns = c;
		lines = l;
	}

	public Types[,] getMatrix() {
		return matrix;
	}

	public int getColumns() {
		return columns;
	}

	public int getLines() {
		return lines;
	}

	public void GenerateSection (Difficulties difficulty) {
		matrix = new Types[columns, lines];

		// Decide where to start
		if (FirstSection) {
			PlayerX = Random.Range (0, lines);
		}

		// Create player path
		playerY = columns - 1;
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

		setDifficulty (difficulty);

		assignObstacles ();

		assignCoins ();

		assignBeers ();
	}

	private Moves[] GetPossibleMoves () {
		List<Moves> moves = new List<Moves> ();

		// It can always go up
		moves.Add(Moves.Up);

		// If the player is in the first position it can only go up
		if (playerY < columns - 1) {
			// Can it go left?
			if (PlayerX > 0 && matrix[playerY, PlayerX - 1] != Types.Player) {
				if (playerY == columns - 1) {
					moves.Add(Moves.Left);
				} else if (matrix[playerY + 1, PlayerX - 1] != Types.Player) {
					moves.Add(Moves.Left);
				}
			}

			// Can it go right?
			if (PlayerX < lines - 1 && matrix[playerY, PlayerX + 1] != Types.Player) {
				if (playerY == columns - 1) {
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

		// Iterate the position matrix
		for (int y = 0; y < columns; y++) {
			for (int x = 0; x < lines; x++) {
				KeyValuePair<int, int> position = new KeyValuePair<int,int> (x, y);

				// Benches can be put in player positions only if there are player positions adjacent at least in one direction
				// For first and last columns
				if (x == 0 || x == lines - 1) {
					if (y != 0 && y != columns - 1) {
						if (matrix [y - 1, x] == Types.Player && matrix [y + 1, x] == Types.Player) {
							benchValidPositions.Add (position);
						}
					}
				// For first and last rows
				} else if (y == 0 || y == columns - 1) {
					if (x != 0 && x != lines - 1) {
						if (matrix [y, x - 1] == Types.Player && matrix [y, x + 1] == Types.Player) {
							benchValidPositions.Add (position);
						}
					}
				// For the rest of the positions that are not on the edges of the matrix
				} else {
					if ( (matrix [y, x - 1] == Types.Player && matrix [y, x + 1] == Types.Player) || (matrix [y - 1, x] == Types.Player && matrix [y + 1, x] == Types.Player) ) {
						benchValidPositions.Add (position);
					}
				}

				// Empty spaces
				if (matrix [y, x] == Types.Empty) {
					benchValidPositions.Add (position);

					// Left side columns
					if (x == 0) {
						columnValidPositions.Add (position);
					}

					// Door and right side columns
					if (x == lines - 1) {
						columnValidPositions.Add (position);
						doorValidPositions.Add (position);
					}
				}
			}
		}
	}

	private void getValidCoinPositions () {
		coinValidPositions = new List<KeyValuePair<int, int>> ();
		for(int y = 0; y < columns - 1; y++) {
			for(int x = 0; x < lines - 1; x++) {
				if (matrix [y, x] == Types.Empty || matrix [y, x] == Types.Bench || matrix [y, x] == Types.Player) {
					coinValidPositions.Add (new KeyValuePair<int, int>(x, y));
				}
			}
		}
	}

	private void getValidBeerPositions () {
		beerValidPositions = new List<KeyValuePair<int, int>> ();
		for(int y = 0; y < columns - 1; y++) {
			for(int x = 0; x < lines - 1; x++) {
				if (matrix [y, x] == Types.Empty || matrix [y, x] == Types.Bench || matrix [y, x] == Types.Player) {
					beerValidPositions.Add (new KeyValuePair<int, int>(x, y));
				}
			}
		}
	}

	private void setDifficulty(Difficulties difficulty) {
		switch ((int)difficulty) {
		case (int) Difficulties.NoBrainer:
			numObstacles = Random.Range (0, 1 + 1);
			numCoins = 1;
			numBeers = 0;
			break;
		case (int) Difficulties.Easy:
			numObstacles = Random.Range (1, 2 + 1);
			numCoins = 1;
			numBeers = Random.Range (0, 1 + 1);
			break;
		case (int) Difficulties.Medium:
			numObstacles = Random.Range (2, 3 + 1);
			numCoins = Random.Range (1, 2 + 1);
			numBeers = Random.Range (0, 1 + 1);
			break;
		case (int) Difficulties.Hard:
			numObstacles = Random.Range (3, 4 + 1);
			numCoins = Random.Range (2, 3 + 1);
			numBeers = Random.Range (0, 2 + 1);
			break;
		}
	}

	private void AssignPosition () {
		matrix [playerY, PlayerX] = Types.Player;
		numPlayerSpaces++;
	}

	/* TODO check random algorithm(it's causing errors) */
	private void assignObstacles() {
		int counter = 0;
		while (counter < numObstacles) {
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

	// TODO change this to be like the assignBeers method 
	private void assignCoins() {
		getValidCoinPositions ();
		int counter = 0;
		while (counter < numCoins && coinValidPositions.Count > 0) {
			int total = coinValidPositions.Count;
			int rand = Random.Range (0, total);
			KeyValuePair<int, int> position = coinValidPositions[rand];
			if (matrix [position.Value, position.Key] == Types.Bench) {
				matrix [position.Value, position.Key] = Types.BenchCoin;
			} else {
				matrix [position.Value, position.Key] = Types.Coin;
			}
			coinValidPositions.RemoveAt (rand);
			counter++;
		}
	}

	private void assignBeers() {
		int counter = 0;
		bool valid = true;
		while (counter < numBeers && valid) {
			getValidBeerPositions ();
			if (beerValidPositions.Count <= 0) {
				valid = false;
			} else {
				int total = beerValidPositions.Count;
				int rand = Random.Range (0, total);
				KeyValuePair<int, int> position = beerValidPositions[rand];
				if (matrix [position.Value, position.Key] == Types.Bench) {
					matrix [position.Value, position.Key] = Types.BenchBeer;
				} else {
					matrix [position.Value, position.Key] = Types.Beer;
				}
				counter++;
			}
		}
	}

}
