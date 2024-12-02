test = """
7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9
"""


### Part 1
def read_file_as_string(file_path):
    with open(file_path, 'r') as file:
        content = file.read()
    return content

def parse_rows(data):
    rows = data.strip().split('\n')
    return [list(map(int, row.split())) for row in rows]

def safe_row(row):
    last_direction = 0
    for i in range(len(row) - 1):
        val = row[i + 1] - row[i]
        if abs(val) > 3 or val == 0 or (val > 0 and last_direction == -1) or (val < 0 and last_direction == 1):
            return 0
        last_direction = 1 if val > 0 else -1
    return 1

def safe_rows_count(rows):
    count = 0
    for row in rows:
        count += safe_row(row)
    return count

string_input = read_file_as_string('2024\day2\input.txt')
data = parse_rows(string_input)
# print(safe_rows_count(data))

### Part 2

def remove_and_check(row):
    for i in range(len(row)):
        new_row = row[:i] + row[i+1:]
        safe = safe_row(new_row)
        if (safe == 1):
            return 1
    return 0

def safe_rows_removable(rows):
    count = 0 
    for row in rows:
        count += remove_and_check(row)
    return count

print(safe_rows_removable(data))