# Mars Rovers - Deltatre Technical Challenge - .NET Backend Lead Engineer

A .NET solution to the classic Mars Rovers navigation problem.

## Problem Description

A squad of robotic rovers are landed by NASA on a rectangular plateau on Mars. Each rover's position is represented by an `x` and `y` coordinate and a cardinal compass direction (`N`, `E`, `S`, `W`).

The plateau is divided into a grid. The square directly North of `(x, y)` is `(x, y+1)`.

### Commands

Each rover receives a string of instructions made up of the following characters:

| Command | Description |
|---------|-------------|
| `L` | Spin 90° to the left without moving |
| `R` | Spin 90° to the right without moving |
| `M` | Move forward one grid point in the current heading |

Rovers are processed **sequentially** — the next rover does not move until the previous one has finished.

## Input Format

```
<plateau-max-x> <plateau-max-y>
<rover-x> <rover-y> <rover-heading>
<instructions>
<rover-x> <rover-y> <rover-heading>
<instructions>
...
```

- The first line defines the **upper-right coordinates** of the plateau (lower-left is assumed to be `0 0`)
- Each subsequent pair of lines defines a rover's **starting position** and its **movement instructions**

## Output Format

For each rover, output its final position and heading:

```
<x> <y> <heading>
```

## Example

**Input:**
```
5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM
```

**Output:**
```
1 3 N
5 1 E
```