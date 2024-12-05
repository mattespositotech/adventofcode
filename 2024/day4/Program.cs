using day4;

string testInput = "MMMSXXMASM\r\nMSAMXMSMSA\r\nAMXSXMAAMM\r\nMSAMASMSMX\r\nXMASAMXAMM\r\nXXAMMXXAMA\r\nSMSMSASXSS\r\nSAXAMASAAA\r\nMAMMMXMMMM\r\nMXMXAXMASX";

List<string> grid = new List<string>();

const string XMAS = "XMAS";
const string SAMX = "SAMX";

int HorizontalCount(List<string> grid, string substring) {
    int count = 0;
    foreach (var row in grid) { 
        int index = row.IndexOf(substring);

        if (index != -1) {
            count++;
            index = row.IndexOf(substring, index + 1);
        }
    }

    return count;
}

int VerticalCount(List<string> grid, string substring) {
    return HorizontalCount(grid, substring);
}

int DiagonalCount(List<string> grid, string substring) {
    int count = 0;
    int rows = grid.Count;
    int cols = grid[0].Length;

    for (int i = 0; i < rows; i++) {
        for (int j = 0; j < cols; j++) {
            if (i+3 >= rows || j+3 >= cols) {
                continue;
            }

            string diagonal = grid[i][j].ToString() + grid[i+1][j+1].ToString() + grid[i + 2][j+2].ToString() + grid[i + 3][j+3].ToString();
            if (diagonal == substring)
            {
                count++;
            }
        }
    }

    for (int k = 0; k < rows; k++) {
        for (int l = cols-1; l >= 0; l--) {
            if (k+3 >= rows || l-3 < 0) {
                continue;
            }

            string diagonal = grid[k][l].ToString() + grid[k + 1][l - 1].ToString() + grid[k + 2][l - 2].ToString() + grid[k + 3][l - 3].ToString();
            if (diagonal == substring) {
                count++;
            }
        }
    }
    return count;
}

List<string> TransposeList(List<string> grid) {
    int maxLength = grid.Max(s => s.Length);

    List<string> newGrid = new List<string>();

    for (int i = 0; i < maxLength; i++) {
        string charactersAtIndex = new string(grid
            .Where(s => s.Length > i) 
            .Select(s => s[i])
            .ToArray());

        newGrid.Add(charactersAtIndex);
    }

    return newGrid;
}

void Part1(string input) {
    grid = [.. input.Split("\r\n")];
    List<string> transposedGrid = TransposeList(grid);

    int total = 0;

    total += HorizontalCount(grid, XMAS);
    total += HorizontalCount(grid, SAMX);

    total += VerticalCount(transposedGrid, XMAS);
    total += VerticalCount(transposedGrid, SAMX);  

    total += DiagonalCount(grid, XMAS);
    total += DiagonalCount(grid, SAMX);

    Console.WriteLine(total);
}


//Part1(Data.Input);