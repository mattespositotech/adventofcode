using day7;

string testInput = "190: 10 19\r\n3267: 81 40 27\r\n83: 17 5\r\n156: 15 6\r\n7290: 6 8 6 15\r\n161011: 16 10 13\r\n192: 17 8 14\r\n21037: 9 7 18 13\r\n292: 11 6 16 20";

List<char[]> GenerateCombinations(int length, char[] operators) {
    List<char[]> results = new List<char[]>();
    GenerateRecursive(new char[length], 0, length, operators, results);
    return results;
}

void GenerateRecursive(char[] current, int position, int length, char[] operators, List<char[]> results) {
    if (position == length) {
        results.Add((char[])current.Clone());
        return;
    }

    foreach (char op in operators) {
        current[position] = op;
        GenerateRecursive(current, position + 1, length, operators, results);
    }
}

long ValidEquation(string equation) {
    var numbers = equation.Split(' ').ToList();

    long num2check = long.Parse(numbers[0][..^1]);

    numbers.RemoveAt(0);

    List<char[]> combinations = GenerateCombinations(numbers.Count - 1, ['+', '*', '|']);

    foreach (var combo in combinations) {
        long total = long.Parse(numbers[0]);
        for (int i = 0; i < combo.Length; i++) {
            if (combo[i] == '+') {
                total += long.Parse(numbers[i + 1]);
            } else if (combo[i] == '*') {
                total *= long.Parse(numbers[i + 1]);
            } else if (combo[i] == '|') {
                string conncat = total.ToString() + numbers[i + 1];
                total = long.Parse(conncat);
            }
        }

        if (total == num2check) {
            return num2check;
        }
    }

    return 0;
}

long Part1() {
    long total = 0;
    var equations = Data.Input.Split("\r\n");
    //var equations = testInput.Split("\r\n");
    foreach (var equation in equations) {
        total += ValidEquation(equation);
    }

    return total;
}

Console.WriteLine(Part1());