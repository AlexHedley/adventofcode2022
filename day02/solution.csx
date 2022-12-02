#load "../utils/utils.csx"

public class Day02 {
    // Rock defeats Scissors
    // Scissors defeats Paper
    // Paper defeats Rock

    // A for Rock
    // B for Paper
    // C for Scissors

    // X for Rock
    // Y for Paper
    // Z for Scissors
    List<Action> actions = new List<Action>();
    Action rock = new Action() {
        Letter = "R",
        Name = "Rock",
        Alternatives = new List<String>() { "A", "X" },
        Score = 1
    };
    Action paper = new Action() {
        Letter = "P",
        Name = "Paper",
        Alternatives = new List<String>() { "B", "Y" },
        Score = 2
    };
    Action scissors = new Action() {
        Letter = "S",
        Name = "Scissors",
        Alternatives = new List<String>() { "C", "Z" },
        Score = 3
    };
    
    public Day02() {
        actions.Add(rock);
        actions.Add(paper);
        actions.Add(scissors);

        rock.Beats = scissors;
        paper.Beats = rock;
        scissors.Beats = paper;
    }

    public int CalculateRound(string line) {
        (string player1, string player2) round = line.Split(" ") switch { var a => (a[0], a[1]) };;
        var p1Action = GetAction(round.player1);
        var p2Action = GetAction(round.player2);
        var points = CalculatePoints(p1Action, p2Action);
        return points;
    }

    public Action GetAction(string letter) {
        return actions.FirstOrDefault(a => a.Alternatives.Any(l => l == letter));
    }

    // 1 for Rock
    // 2 for Paper
    // 3 for Scissors

    // 0 if you lost
    // 3 if the round was a draw
    // 6 if you won
    public int CalculatePoints(Action p1, Action p2) {
        var points = 0;
        points += p2.Score;

        var p1Beats = actions.FirstOrDefault(a => a.Beats == p1);
        var p2Beats = actions.FirstOrDefault(a => a.Beats == p2);

        if (p1Beats == p2) {
            points += 6;
        }
        else if (p2Beats == p1) {
        }
        else {
            points += 3;
        }

        return points;
    }
}

public class Battle {
    public string Winner;
    public string Loser;
    public Battle(string winner, string loser) {
        Winner = winner;
        Loser = loser;
    }

    public override string ToString() {
        return $"Winner: {Winner} | Loser: {Loser}";
    }
}

public class Action {
    public string Letter;
    public string Name;
    public List<string> Alternatives = new List<String>();
    public int Score = 0;
    public Action Beats;

    public override string ToString() {
        return $"Letter: {Letter} | Name: {Name} | Alternatives: {String.Join(',', Alternatives)} | Score: {Score} | Beats: {Beats.Letter}";
    }
}

Console.WriteLine("-- Day 2 --");

// Part 1
Console.WriteLine("Part 1.");

// string fileName = @"input-sample.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

var day02 = new Day02();

var total = 0;
lines.ToList().ForEach(x => { 
        var points = day02.CalculateRound(x);
        total += points;
    });
Console.WriteLine(total);

// Part 2
// Console.WriteLine("Part 2.");

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();