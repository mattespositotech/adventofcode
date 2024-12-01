test = """
3   4
4   3
2   5
1   3
3   9
3   3
"""

def read_file_as_string(file_path):
    with open(file_path, 'r') as file:
        content = file.read()
    return content

def find_distance(num1, num2):
    return abs(num1 - num2)

### Part 1
def create_lists(input):
    lines = input.strip().split('\n')
    list1, list2 = [], []
    
    for line in lines:
        num1, num2 = map(int, line.split())
        list1.append(num1)
        list2.append(num2)
    
    return list1, list2

def pop_smallest(list):
    smallest = min(list)
    list.remove(smallest)
    return smallest

def find_total_distance(input):
    sum = 0
    list1, list2 = create_lists(input)

    while list1 and list2:
        num1 = pop_smallest(list1)
        num2 = pop_smallest(list2)

        sum += find_distance(num1, num2)
    
    return sum


file_content = read_file_as_string('/Users/mattespositotech/Documents/Personal Projects/adventofcode/2024/day1/input.txt')
#print(find_total_distance(file_content))

### Part 2

def similarity_score(input):
    score = 0

    list1, list2 = create_lists(input)

    for i in list1:
        occurrences = list2.count(i)
        instance_score = i * occurrences
        score += instance_score

    return score

print(similarity_score(file_content))