using day5;

string rules = "47|53\r\n97|13\r\n97|61\r\n97|47\r\n75|29\r\n61|13\r\n75|53\r\n29|13\r\n97|29\r\n53|29\r\n61|53\r\n97|53\r\n61|29\r\n47|13\r\n75|47\r\n97|75\r\n47|61\r\n75|61\r\n47|29\r\n75|13\r\n53|13";

string pages = "75,47,61,53,29\r\n97,61,53,29,13\r\n75,29,13\r\n75,97,47,61,53\r\n61,13,29\r\n97,13,75,29,47";

Dictionary<string, List<string>> RuleDictionary(string rules) {
    Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
    var ruleList = rules.Split("\r\n");

    foreach (var rule in ruleList) { 
        var numbers = rule.Split('|');
        if (dictionary.ContainsKey(numbers[0])) {
            dictionary[numbers[0]].Add(numbers[1]);
        } else {
            dictionary[numbers[0]] = new List<string> { numbers[1] };
        }
    }

    return dictionary;
}

var dictionary = RuleDictionary(Data.rules);
//foreach (var kvp in dictionary) {
//    Console.WriteLine($"Key: {kvp.Key}, Values: {string.Join(", ", kvp.Value)}");
//}

List<string> GetValidPages(string pages) {
    List<string> validPages = new List<string>();
    var pagesList = pages.Split("\r\n");

    foreach (var page in pagesList) { 
        if (ValidPage(page)) {
            validPages.Add(page);
        }
    }

    return validPages;
}

bool ValidPage(string page) { 
    var pageList = page.Split(",");

    for (int i = 0; i < pageList.Length; i++) {
        var validationList = dictionary.TryGetValue(pageList[i], out var values) ? values : new List<string>();

        foreach (var item in validationList) {
            if (!pageList.Contains(item)) { 
                continue;
            }

            int validationIndex = Array.IndexOf(pageList, item);
            if (validationIndex < i)
            {
                return false;
            }
        }
    }

    return true;
}

int GetMiddle(string page) {
    string[] list = page.Split(',');
    int middleIndex = list.Length / 2;
    return int.Parse(list[middleIndex]);
}

int Total(List<string> pages) {
    int total = 0;
    foreach (var page in pages) {
        total += GetMiddle(page);
    }

    return total;
}

// Part 1
//var validPages = GetValidPages(Data.pages);
//Console.WriteLine(Total(validPages));


// Part 2

List<string> GetInValidPages(string pages) {
    List<string> validPages = new List<string>();
    var pagesList = pages.Split("\r\n");

    foreach (var page in pagesList) {
        if (!ValidPage(page)) {
            validPages.Add(page);
        }
    }

    return validPages;
}

string FixPage(string page) {
    var pageList = page.Split(",");

    for (int i = 0; i < pageList.Length; i++) {
        var validationList = dictionary.TryGetValue(pageList[i], out var values) ? values : new List<string>();

        foreach (var item in validationList) {
            if (!pageList.Contains(item)) {
                continue;
            }

            int validationIndex = Array.IndexOf(pageList, item);
            if (validationIndex < i) {
                pageList[validationIndex] = pageList[i];
                pageList[i] = item;
            }
        }
    }

    return string.Join(",", pageList);
}

string FixedPage(string page) {
    var newPage = FixPage(page);
    if (ValidPage(newPage)) {
        return newPage;
    }

    return FixedPage(newPage);
}

var invalid = GetInValidPages(Data.pages);
List<string> newPages = new List<string>();
foreach(var page in invalid) {
    var newPage = FixedPage(page);
    newPages.Add(newPage);
}
Console.WriteLine(Total(newPages));