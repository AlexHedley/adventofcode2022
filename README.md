# 🎄 Advent of Code (2022) 🎄

[Advent Of Code](https://adventofcode.com/) [2022](https://adventofcode.com/2022/)

- [Awesome Advent of Code](https://github.com/Bogdanp/awesome-advent-of-code)

---

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)

- [Docs](docs/README.md)
- [Stats](docs/STATS.md)

## Ranking

Private Leaderboard

```bash
   |     |                   1 1 1 1 1 1 1 1 1 1 2 2 2 2 2 2
   |     | 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5
8  | 152 | * * * ■ ° ° ° ° ° ° ° ° ° ° ° ° ° ° ° ° ° ° ° ° ° | 7
```

Key: \* Both | ■ One | ° None |

| Silver ■ | Gold ⭐ |
| -------- | ------- |
| 1        | 3       |

## Solutions

- [Day 1](day01/README.md)
- [Day 2](day02/README.md)
- [Day 3](day03/README.md)
- [Day 4](day04/README.md)
  <!-- - [Day 5](day05/README.md) -->
  <!-- - [Day 6](day06/README.md) -->
  <!-- - [Day 7](day07/README.md) -->
  <!-- - [Day 8](day08/README.md) -->
  <!-- - [Day 9](day09/README.md) -->
  <!-- - [Day 10](day10/README.md) -->
  <!-- - [Day 11](day11/README.md) -->
  <!-- - [Day 12](day12/README.md) -->
  <!-- - [Day 13](day13/README.md) -->
  <!-- - [Day 14](day14/README.md) -->
  <!-- - [Day 15](day15/README.md) -->
  <!-- - [Day 16](day16/README.md) -->
  <!-- - [Day 17](day17/README.md) -->
  <!-- - [Day 18](day18/README.md) -->
  <!-- - [Day 19](day19/README.md) -->
  <!-- - [Day 20](day20/README.md) -->
  <!-- - [Day 21](day21/README.md) -->
  <!-- - [Day 22](day22/README.md) -->
  <!-- - [Day 23](day23/README.md) -->
  <!-- - [Day 24](day24/README.md) -->
  <!-- - [Day 25](day25/README.md) -->

<!-- [![For: Advent Of Code](https://img.shields.io/badge/for-advent_of_code-green.svg)](https://adventofcode.com/) -->
<!-- [![License: MIT](https://img.shields.io/badge/License-MIT-lightgrey.svg)](https://opensource.org/licenses/MIT)  -->

<!-- https://github.com/marketplace/actions/aoc-badges -->
<!-- ![](https://img.shields.io/badge/day%20📅-6-blue) -->
<!-- ![](https://img.shields.io/badge/stars%20⭐-12-yellow) -->
<!-- ![](https://img.shields.io/badge/days%20completed-6-red) -->

This repo contains my solutions to the [Advent of Code 2022](https://adventofcode.com/2022) using primarily [C#](https://learn.microsoft.com/en-us/dotnet/csharp/).

The code will likely be bad. :p

## Setup

`dotnet tool install -g dotnet-script`

Copy the [day](day/) folder and use.

## Running Solutions/Tests

How to run the solution file for each day depends on the language.

### C#

Any C# scripts will require [dotnet-script](https://github.com/filipw/dotnet-script) and [ScriptUnit](https://github.com/seesharper/ScriptUnit).

For C# solutions

- `cd dayXX`
- `dotnet script solution.csx`
- `dotnet script tests.csx`

or

- `dotnet script dayXX/solution.csx`
- `dotnet script dayXX/tests.csx`
