// Generates random magical items for TTRPGs.

using System.IO;
using System.Linq;
using libAnnaC;

string ProgramName = "itemGen";
string ProgramVersion = "0.0.3";
string[] ItemTypes = { "sword", "axe", "wand", "shield", "tome", "armor", "ring", "amulet" };
string AdjFileName = "english_adjectives.txt";
string VerbFileName = "english_verbs.txt";
string PastVerbFileName = "english_verbs_past.txt";
string IngVerbFileName = "english_verbs_ing.txt";
string NounFileName = "english_nouns.txt";
string[] VerbTypes = { "basic", "past", "ing" };
string[] Combinations = { "AVN", "V", "AN", "VN", "N", "A", "AV", "PROT" };
string[] Consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };
string FinalVerb = "";
string PluralNoun = "";
int CreateCount = 20;
Random random = new Random();

string[] Adjectives = File.ReadAllLines(AdjFileName);
string[] Verbs = File.ReadAllLines(VerbFileName);
string[] Nouns = File.ReadAllLines(NounFileName);
string[] PastVerbs = File.ReadAllLines(PastVerbFileName);
string[] IngVerbs = File.ReadAllLines(IngVerbFileName);

Console.WriteLine("\n" + ProgramName + " " + ProgramVersion + "\n");

void GenerateItems(int row)
{
    string ItemType = ItemTypes[random.Next(ItemTypes.Length)];
    string Verb = Verbs[random.Next(Verbs.Length)];
    string Adjective = Adjectives[random.Next(Adjectives.Length)];
    string Noun = Nouns[random.Next(Nouns.Length)];
    string PastVerb = PastVerbs[random.Next(PastVerbs.Length)];
    string IngVerb = IngVerbs[random.Next(IngVerbs.Length)];
    string VerbType = VerbTypes[random.Next(VerbTypes.Length)];
    string Combination = Combinations[random.Next(Combinations.Length)];
    string LastChar = Noun[Noun.Length - 1].ToString();
    string CurrentRow = String.Format("{0, 2}: ", row.ToString());

    switch (LastChar)
    {
        case "s":
            PluralNoun = Noun;
            break;
        case "y":
            if (Consonants.Any(Noun.Substring(Noun.Length - 2, 1).Contains)) { PluralNoun = Noun.Substring(0, Noun.Length - 1) + "ies"; }
            else { PluralNoun = Noun + "s"; }
            break;
        case "f":
            PluralNoun = Noun.Substring(0, Noun.Length - 1) + "ves";
            break;
        case "x":
        case "h":
            PluralNoun = Noun + "es";
            break;
        default:
            PluralNoun = Noun + "s";
            break;
    }

    switch (VerbType)
    {
        case "basic":
            FinalVerb = Verb;
            break;
        case "past":
            FinalVerb = PastVerb;
            break;
        case "ing":
            FinalVerb = IngVerb;
            break;
    }

    switch (Combination)
    {
        case "AVN":
            Console.WriteLine(CurrentRow + ItemType.Capitalize() + " of " + Adjective.Capitalize() + " " + FinalVerb.Capitalize() + " " + Noun.Capitalize());
            break;
        case "V":
            Console.WriteLine(CurrentRow + ItemType.Capitalize() + " of " + IngVerb.Capitalize());
            break;
        case "AN":
            Console.WriteLine(CurrentRow + ItemType.Capitalize() + " of " + Adjective.Capitalize() + " " + Noun.Capitalize());
            break;
        case "VN":
            Console.WriteLine(CurrentRow + ItemType.Capitalize() + " of " + FinalVerb.Capitalize() + " " + Noun.Capitalize());
            break;
        case "N":
            Console.WriteLine(CurrentRow + ItemType.Capitalize() + " of " + Noun.Capitalize());
            break;
        case "A":
            Console.WriteLine(CurrentRow + Adjective.Capitalize() + " " + ItemType.Capitalize());
            break;
        case "AV":
            Console.WriteLine(CurrentRow + ItemType.Capitalize() + " of " + Adjective.Capitalize() + " " + IngVerb.Capitalize());
            break;
        case "PROT":
            Console.WriteLine(CurrentRow + ItemType.Capitalize() + " of Protection from " + PluralNoun.Capitalize());
            break;
    }
}

for (int i = 0; i < CreateCount; i++)
{
    GenerateItems(i);
}
Console.WriteLine("\n");