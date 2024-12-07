using day6;
using System.Drawing;
using System.Reflection;

string testInput = "....#.....\r\n.........#\r\n..........\r\n..#.......\r\n.......#..\r\n..........\r\n.#..^.....\r\n........#.\r\n#.........\r\n......#...";

const char OBSTRUCTION = '#';
const char NEW_OBSTRUCTION = '%';
const char MARK = 'X';
const char MARK2 = '0';
const char MARK3 = '3';
const string GAURD = "^v><";

char[][] MakeMap(string input) {
    string[] rows = input.Split("\r\n");

    char[][] map = new char[rows.Length][];
    for (int i = 0; i < rows.Length; i++) {
        map[i] = rows[i].ToCharArray();
    }

    return map;
}

void PrintMap(char[][] map) {
    foreach (var row in map) {
        Console.WriteLine(string.Join("", row));
    }
    Console.WriteLine("----------------------------------");
}

Tuple<int, int> FindGuard(char[][] map) {
    for (int i = 0; i < map.Length; i++) {
        for (int j = 0; j < map[i].Length; j++) {
            if (GAURD.Contains(map[i][j])) {
                return new Tuple<int, int>(i, j);
            }
        }
    }

    throw new Exception("Bad Input");
}

void TraverseMap(char[][] map) {
    int x = 0;
    int y = -1;
    int index = 0;

    void Turn90Deg() {
        (int, int)[] directions = { (0, -1), (1, 0), (0, 1), (-1, 0) };

        index++;
        if (index >= directions.Length) {
            index = 0;
        }

        (x, y) = directions[index];
    }

    Tuple<int, int> guardStart = FindGuard(map);


    int bounds = map.Length;
    //Console.WriteLine($"Bounds: {bounds}");

    int guardX = guardStart.Item2;
    int guardY = guardStart.Item1;
    //Console.WriteLine($"Start Index: {guardX}, {guardY}");

    while ((bounds > guardX + x && guardX + x >= 0)
        && (bounds > guardY + y && guardY + y >= 0)) {
        //Console.WriteLine($"Cords to check: {guardX + x}, {guardY + y}, current: {guardX}, {guardY}");
        if (map[guardY + y][guardX + x] == OBSTRUCTION) {
            Turn90Deg();
        }
        map[guardY][guardX] = MARK;
        guardX += x;
        guardY += y;
        //PrintMap(map);
    }

    map[guardY][guardX] = MARK;
}

int CountX(char[][] map) {
    int count = 0;

    for (int i = 0; i < map.Length; i++) {
        for (int j = 0; j < map[i].Length; j++) {
            if (map[i][j] == MARK) {
                count++;
            }
        }
    }

    return count;
}

// Part 1
var map = MakeMap(Data.Input);
//TraverseMap(map);
//Console.WriteLine(CountX(map)); 

// Part 2

// Get current path and then loop that list as possible places for new obstical
// for each add to a dictionary - at starting position check if we have looped threw all coords twice if so return new obstical indexes
// else move to new check

List<(int, int)> GetInitalPath(char[][] map, Tuple<int, int> guardStart) {
    List<(int, int)> path = new List<(int, int)> ();
    int x = 0;
    int y = -1;
    int index = 0;

    void Turn90Deg() {
        (int, int)[] directions = { (0, -1), (1, 0), (0, 1), (-1, 0) };

        index++;
        if (index >= directions.Length) {
            index = 0;
        }

        (x, y) = directions[index];
    }

    int bounds = map.Length;

    int guardX = guardStart.Item2;
    int guardY = guardStart.Item1;

    while ((bounds > guardX + x && guardX + x >= 0)
        && (bounds > guardY + y && guardY + y >= 0)) {
        if (map[guardY + y][guardX + x] == OBSTRUCTION) {
            Turn90Deg();
        }
        path.Add((guardY, guardX));
        guardX += x;
        guardY += y;
    }

    if (! path.Contains((guardY, guardX))){
        path.Add((guardY, guardX));
    }

    // remove guard start
    path.RemoveAt(0);

    //Console.WriteLine($"Path Length: {path.Count}");
    List<(int, int)> uniquePoints = new List<(int, int)>(new HashSet<(int, int)>(path));
    //Console.WriteLine($"Path Length: {uniquePoints.Count}");

    return uniquePoints;
}

bool CheckCircular(char[][] map, Tuple<int, int> guardStart) {
    int x = 0;
    int y = -1;
    int index = 0;
    int loopCount = 0;

    Dictionary<(int, int), int> circlePath = new Dictionary<(int, int), int>();

    void Turn90Deg() {
        (int, int)[] directions = { (0, -1), (1, 0), (0, 1), (-1, 0) };

        index++;
        if (index >= directions.Length) {
            index = 0;
        }

        (x, y) = directions[index];
    }

    //PrintMap(map);

    int bounds = map.Length;

    int guardX = guardStart.Item2;
    int guardY = guardStart.Item1;
    //Console.WriteLine($"Start Index: {guardX}, {guardY}");

    while ((bounds > guardX + x && guardX + x >= 0)
        && (bounds > guardY + y && guardY + y >= 0)) {
        //Console.WriteLine($"Cords to check: {guardX + x}, {guardY + y}, current: {guardX}, {guardY}");
        if (map[guardY + y][guardX + x] == OBSTRUCTION || map[guardY + y][guardX + x] == NEW_OBSTRUCTION) {
            Turn90Deg();
        }

        if (map[guardY][guardX] == MARK) {
            map[guardY][guardX] = MARK2;
        } else if (map[guardY][guardX] == MARK2) {
            map[guardY][guardX] = MARK3;
        }
        else if (map[guardY][guardX] == MARK3) {
            if(circlePath.ContainsKey((guardY, guardX))) {
                circlePath[(guardY, guardX)]++;
                if (circlePath.Values.All(count => count >= 2)) {
                    return true;
                }
            } else {
                circlePath[(guardY, guardX)] = 1;
            }
        }else {
            map[guardY][guardX] = MARK;
        }


        guardX += x;
        guardY += y;
        //PrintMap(map);

        //loopCount++;
        //if (loopCount > 1000) {
        //    PrintMap(map);
        //    var list = circlePath.Where(kvp => kvp.Value < 2).ToList();
        //    Console.WriteLine(string.Join(", ", list));
        //    loopCount = 0;
        //}

    }
    return false;
}



char[][] CreateNewMap(char[][] map, (int, int) newObstruction) {
    char[][] newMap = new char[map.Length][];
    for (int i = 0; i < map.Length; i++) {
        newMap[i] = new char[map[i].Length];
        Array.Copy(map[i], newMap[i], newMap.Length);
    }

    newMap[newObstruction.Item1][newObstruction.Item2] = NEW_OBSTRUCTION;
    return newMap;
}


//map = MakeMap(testInput);
Tuple<int, int> guardStart = FindGuard(map);
var path = GetInitalPath(map, guardStart);

int count = 0;

//var newMap = CreateNewMap(map, (105, 36));
//Console.WriteLine(CheckCircular(newMap, guardStart));
//Console.WriteLine();

foreach (var obstructionCoords in path) {
    Console.WriteLine(obstructionCoords);
    var newMap = CreateNewMap(map, obstructionCoords);
    if (CheckCircular(newMap, guardStart)) {
        count++;
        //Console.WriteLine($"Total: {count}");
        //PrintMap(newMap);
    }
}

Console.WriteLine($"Total: {count}");
