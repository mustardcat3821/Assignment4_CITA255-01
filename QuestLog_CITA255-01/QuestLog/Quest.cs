using System;
using System.Collections.Generic;
using System.Text;

namespace QuestLog;

public class Quest
{
    public string Title { get; set; } = "";
    public string Giver { get; set; } = "";
    public int Reward { get; set; }
}