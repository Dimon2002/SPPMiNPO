import matplotlib.pyplot as plt
import sys

def read_data(file_path):
    with open(file_path, 'r') as file:
        data = file.readlines()
    
    x = []
    y = []
    for line in data:
        values = line.split()
        x.append(float(values[0]))
        y.append(float(values[1]))
    return x,y

file_path = '../out.txt'
x,y = read_data(file_path)

plt.scatter(x, y)
plt.xlabel('X-axis')
plt.ylabel('Y-axis')
plt.title('Grapfics')
plt.grid(True)
plt.show()