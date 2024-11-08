# import matplotlib.pyplot as plt
# import sys

# def read_data(file_path):
#     with open(file_path, 'r') as file:
#         data = file.readlines()
    
#     x = []
#     y = []
#     for line in data:
#         values = line.split()
#         x.append(float(values[0]))
#         y.append(float(values[1]))
#     return x,y

# file_path = '../out.txt'
# x,y = read_data(file_path)

# plt.scatter(x, y)
# plt.xlabel('X-axis')
# plt.ylabel('Y-axis')
# plt.title('Grapfics')
# plt.grid(True)
# plt.show()

from cProfile import label
import numpy as np
import matplotlib.pyplot as plt
import scipy.stats as sts

from numpy import sign
from math import pi, exp

NU = 0.05
K = 1.398

def f(sprd: np.ndarray, nu=NU, k=K) -> np.ndarray:
    flist = []
    for x in sprd:
        if abs(x) <= k:
            flist.append((1 - nu) / (2 * pi) ** (1 / 2) * exp(-x ** 2 / 2))
        else:
            flist.append((1 - nu) / (2 * pi) ** (1 / 2) * exp(1 / 2 * k ** 2- k * abs(x)))
    return np.array(flist)

def IF_mean(y: np.ndarray, theta):
    return y - theta

def IF_median(y: np.ndarray, theta, lamb):
    return lamb * sign(y - theta) / (2 * f(np.array([0]), NU, K)[0])

def IF_truncated_mean(y: np.ndarray, theta, lamb, alpha):
    k = {
            0.05 : 2.011802293672519, 
            0.1  : 1.5032469544798932, 
            0.15 : 1.1445077882604557
        }[alpha]
    
    res = []
    for p in y:
        if (p - theta) / lamb <= -k:
            res.append(-k)
        elif abs((p - theta) / lamb) <= k:
            res.append((p - theta) / lamb)
            
        else:
            res.append(k)
    return np.array(res)

x = np.linspace(-6, 6, 1000)
fig, ax = plt.subplots(figsize=(15, 10))
ax.plot(x, IF_mean(x, 0), label='Mean')
ax.plot(x, IF_median(x, 0, 1), label='Median')
ax.plot(x, IF_truncated_mean(x, 0, 1, 0.05), "--", label='Truncated mean 5%')
ax.plot(x, IF_truncated_mean(x, 0, 1, 0.1), "--", label='Truncated mean 10%')
ax.plot(x, IF_truncated_mean(x, 0, 1, 0.15), "--", label='Truncated mean 15%') 
plt.grid(True)
plt.legend()

plt.show()


# q 5% 2.011802293672519
# q 10% 1.5032469544798932
# q 15% 1.1445077882604557