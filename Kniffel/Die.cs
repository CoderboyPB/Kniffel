using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kniffel;

public class Die : IEquatable<Die>
{
    public int Value { get; set; }
    public List<Die> CurrentDice { get; set; } = new List<Die>();
    public void RollDice()
    {
        CurrentDice.Clear();
        var random = new Random();

        for(int i=0; i <= 5; i++)
        {
            CurrentDice.Add(new Die() { Value = random.Next(1, 6) });
        }
    }

    public void ShowDice()
    {
        CurrentDice.ForEach(d => Console.WriteLine($"{d.Value} "));
        Console.WriteLine();
    }

    public bool Kniffel() => CurrentDice.Distinct().Count() == 1;
    
    public bool FullHouse()
    {
        bool result = false;
        var numberOfGroups = CurrentDice.Distinct().Count();
        if(numberOfGroups == 2)
        {
            var groupedByValues = CurrentDice.GroupBy(d => d.Value);
            foreach(var group in groupedByValues)
            {
                if (group.Count() >= 2)
                    result = true;
            }
        }

        return result;
    }

    public int Pasch(int type)
    {
        int result = 0;

        var groupedByValues = CurrentDice.GroupBy(d => d.Value);
        foreach(var group in groupedByValues)
        {
            if (group.Count() >= type)
                result = CurrentDice.Sum(d => d.Value);
        }

        return result;
    }

    public int Tripples(int type)
    {
        int result = CurrentDice.Count(d => d.Value == type) switch
        {
            0 => (-3) * type, 
            1 => (-2) * type,
            2 => -type,
            3 => 0,
            4 => type,
        };

        return result;
    }

    public int Street()
    {
        // 0: no street
        // 1: small street
        // 2: great street

        bool containsOne = CurrentDice.Exists(d => d.Value == 1);
        bool containsThree = CurrentDice.Exists(d => d.Value == 3);
        bool containsFour = CurrentDice.Exists(d => d.Value == 4);
        bool containsSix = CurrentDice.Exists(d => d.Value == 6);

        int result = 0;
        int numberDifferentDice = CurrentDice.Distinct().Count();

        if(numberDifferentDice >= 4)
        {
            if(numberDifferentDice == 4)
            {
                if (containsThree && containsFour)
                    result = 1;
            }
            else
            {
                if (containsOne && containsSix)
                {
                    if (containsThree && containsFour)
                        result = 1;
                }
                else
                    result = 2;
            }
        }

        return result;
    }

    public int Chance() => CurrentDice.Sum(d => d.Value);

    public bool Equals(Die? other)
    {
        return this.Value == other?.Value;
    }
}
