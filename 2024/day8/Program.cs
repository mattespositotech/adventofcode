// find all antenna types -> to list
// create a blank map of same size
// foreach antenna of type x find the two frequencies
// -- if in bounds then update second map
// check for all # in second map

using day8;

string input = "............\r\n........0...\r\n.....0......\r\n.......0....\r\n....0.......\r\n......A.....\r\n............\r\n............\r\n........A...\r\n.........A..\r\n............\r\n............";
const char FREQ = '#';

char[][] MakeMap(string input, bool blank = false) {
    string[] rows = input.Split("\r\n");

    char[][] map = new char[rows.Length][];
    for (int i = 0; i < rows.Length; i++) {
        if (blank) {
            map[i] = new char[rows[i].Length];
            for (int j = 0; j < map[i].Length; j++) {
                map[i][j] = '.';
            }
        } else {
            map[i] = rows[i].ToCharArray();
        }
    }

    return map;
}

void PrintMap(char[][] map) {
    foreach (var row in map) {
        Console.WriteLine(string.Join("", row));
    }
    Console.WriteLine("----------------------------------");
}

int CountFrequencies(char[][] frequencies) {
    int count = 0;

    for (int i = 0; i < frequencies.Length; i++) {
        for (int j = 0; j < frequencies[i].Length; ++j) {
            if (frequencies[i][j] == FREQ) {
                count++;
            }
        }
    }

    return count;
}

List<char> GetAttennaTypes(char[][] antennas) { 
    HashSet<char> anttennaTypes = [];

    foreach (char[] row in antennas) {
        foreach (char c in row) { 
            anttennaTypes.Add(c);
        }
    }

    if (anttennaTypes.Contains('.')) {
        anttennaTypes.Remove('.');
    }

    return new List<char>(anttennaTypes);
}

void UpdateFrequencies(char[][] antennas, char[][] frequencies) {
    var types = GetAttennaTypes(antennas);

    foreach (char type in types)
    {
        List<(int, int)> locations = GetAllLocations(antennas, type);
        UpdateFrequencyMap(locations, frequencies);
    }
}

List<(int, int)> GetAllLocations(char[][] antennas, char antennaType) {
    List<(int, int)> antennaLocations = new List<(int, int)>();

    for (int i = 0; i < antennas.Length; ++i) {
        for (int j = 0; j < antennas[i].Length; ++j) {
            if (antennas[i][j] == antennaType) {
                antennaLocations.Add((i, j)); 
            }
        }
    }

    return antennaLocations;
}

void UpdateFrequencyMap(List<(int, int)> locations, char[][] frequencies) {
    (int, int) antenna1 = locations[0];
    locations.RemoveAt(0);

    foreach (var antenna2 in locations) {
        MarkFrequenices(antenna1, antenna2, frequencies);
    }

    if (locations.Count > 0) {
        UpdateFrequencyMap(locations, frequencies);
    }
}
void MarkFrequenices((int, int) antenna1, (int, int) antenna2, char[][] frequencies) {
    int moveY = antenna1.Item1 - antenna2.Item1;
    int moveX = antenna1.Item2 - antenna2.Item2;

    int point1y = antenna1.Item1;
    int point1x = antenna1.Item2;

    //int point2y = antenna2.Item1 + (-1 * moveY);
    //int point2x = antenna2.Item2 + (-1 * moveX);

    int freqX = frequencies.Length;
    int freqY = frequencies[0].Length;


    // Part 1
    //if (point1x >= 0 && point1x < freqX && point1y >= 0 && point1y < freqY) {
    //    frequencies[point1y][point1x] = FREQ;
    //}

    //if (point2x >= 0 && point2x < freqX && point2y >= 0 && point2y < freqY) {
    //    frequencies[point2y][point2x] = FREQ;
    //}

    while (point1x >= 0 && point1x < freqX && point1y >= 0 && point1y < freqY) {
        frequencies[point1y][point1x] = FREQ;

        point1y += moveY;
        point1x += moveX;
    }

    point1y = antenna1.Item1;
    point1x = antenna1.Item2;

    while (point1x >= 0 && point1x < freqX && point1y >= 0 && point1y < freqY) {
        frequencies[point1y][point1x] = FREQ;

        point1y -= moveY;
        point1x -= moveX;
    }

}

void Main(string input) {
    char[][] antennas = MakeMap(input);
    char[][] frequencies = MakeMap(input, true);

    UpdateFrequencies(antennas, frequencies);

    Console.WriteLine(CountFrequencies(frequencies));
}

Main(Data.Input);
