namespace linkedinwebscraper.modules;

public class User
{
    public string title { get; set; }
    public string fullName { get; set; }
    public int repositories { get; set; }
    public int stars { get; set; }
    public string bio { get; set; }
    public string location { get; set; }
    public string[] social { get; set; }
    public bool readme { get; set; }
    public int contributions { get; set; }
    public bool pinned { get; set; }
    public bool achievements { get; set; }
    public bool highlights { get; set; }
    public bool sponsors { get; set; }
    public bool organizations { get; set; }
}