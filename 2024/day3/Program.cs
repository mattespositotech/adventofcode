using day3;

class Program {
    public static void Main(string[] args) {
        //string input = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
        string input = Input.InputString;

        int inputIndex = 0;

        bool doSwitch = true;

        int total = 0;

        ParsedNumber FindNumber(int index, char validChar) {
            string number = "";

            while (char.IsDigit(input[index])) {
                number += input[index];
                index++;
            }

            if (input[index] != validChar) {
                index++;
                return new ParsedNumber(null, index);
            }

            index++;
            return new ParsedNumber(int.Parse(number), index);
        }


        int Multiply(int index) {
            ParsedNumber num1 = FindNumber(index, ',');

            if (num1.value == null) return num1.newIndex;

            ParsedNumber num2 = FindNumber(num1.newIndex, ')');

            if (num2.value != null) {
                int multValue = (int)(num1.value * num2.value);
                total += multValue;
            }

            return num2.newIndex;
        }

        while (inputIndex < input.Length) {
            if (input[inputIndex] != 'm' && input[inputIndex] != 'd') {
                inputIndex++;
                continue;
            }


            string subString4 = input.Substring(inputIndex, 4);
            string subString7 = input.Substring(inputIndex, 7);
            if (subString4 != "mul(" && subString4 != "do()" && subString7 != "don't()") {
                inputIndex++;
                continue;
            }

            if (subString4 == "do()") {
                doSwitch = true;
                inputIndex += 4;
                continue;
            }

            if (subString7 == "don't()") {
                doSwitch = false;
                inputIndex += 7;
                continue;
            }

            if (doSwitch == true) {
                int numSlice = inputIndex + 4;
                inputIndex = Multiply(numSlice);
            } else {
                inputIndex++;
            }
            
        }

        Console.WriteLine(total);
    }
}

public class ParsedNumber {
    public ParsedNumber(int? value, int newIndex) {
        this.value = value;
        this.newIndex = newIndex;
    }

    public int? value { get; set; }

    public int newIndex { get; set; }
}