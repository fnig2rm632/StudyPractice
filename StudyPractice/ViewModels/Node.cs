using System.Collections.Generic;
using System.Linq;

namespace StudyPractice.ViewModels;

public class Node
{
    public List<Node> Children { get; set; } =  new List<Node>();
    
    public int X { get; set; }
    
    public int Y { get; set; }
    
    public required string Name { get; set; }
    
    public int Weight => Children.Count == 0 ?
        320 : Children.Count == 1 ? Children.First().Weight : 
            Children.Sum(x => x.Weight);
}